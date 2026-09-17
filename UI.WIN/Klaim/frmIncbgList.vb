Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
'Imports DevExpress.XtraPrinting
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports DevExpress.XtraSplashScreen
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient
Imports Npgsql
Imports System.IO
Imports Newtonsoft.Json
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Imports System.Net

Public Class frmIncbgList

#Region "Function"
    Private oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
    Private oGrouperRawatJalan As New Grouper.clsR_Identitas_Grouper
    Private oGrouperCPPT As New Grouper.clsR_CPPT
    Private oGrouperData As New Grouper.clsR_Identitas_Grouper_Data
    Private oGetGrouper As New Brigging.clsSetKoneksi
    Private oRingkasanKeluar As New EMedrek.clsRingkasanKeluar
    Private oDigitalAsesmenMedisIGD As New Transaksi.clsDigital_IGD_01
    Private oDigitalAsesmenHemodialisa As New EMedrek.clsDigital_RJ_38
    Private oDigitalLaporanPersalinan As New EMedrek.clsLaporanPersalinan
    Private oDigitalLaporanOperasi As New EMedrek.clsS_DIGITAL_OK_LAPORANOPERASI
    Private oPendaftaran As New Admission.clsPendaftaran
    Private oKunjungan As New Admission.clsPendaftaran_Kunjungan
    Private oSKD As New Admission.clsSKD
    'Private oHasilLab As New Grouper.clsHasiLab
    Private oADokumen As New Digital.clsDigital_A_Dokumen
    'Private oRME As New RME.clsRME
    Private oExpertise As New Grouper.clsExpertise
    Private sLoad As Boolean
    Private sTab As Integer = 0

    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Incabg-List"
        picUpload.Enabled = False

        deDATEFrom.DateTime = Now.AddDays((-Now.Day) + 1)
        deDATETo.DateTime = Now
        cboTYPE.SelectedIndex = 0
        fn_LoadSecurity()

        sLoad = True

        'ListDiagnosa.Clear()
        'ListProsedur.Clear()

        'fn_LoadDiagnosa()
        'fn_LoadProsedur()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        PdfViewer1.CloseDocument()
        PdfViewerCPPTRawatJalan.CloseDocument()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "INACBG" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
                picGrouper.Enabled = False
                picUpload.Enabled = False
                picKirimOnline.Enabled = False
                picSimpanPdf.Enabled = False
                picHasilScan.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As DevExpress.XtraPrinting.PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, DevExpress.XtraPrinting.PageHeaderFooter)
            phf.Header.Content.Clear()
            'phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            'phf.Header.LineAlignment = BrickAlignment.Center
            'phf.Footer.Font = New Font("Times New Roman", 9.75)
            'phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Inacbg" & vbCrLf & cboTYPE.Text, ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            If sLoad = False Then Exit Sub

            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If cboTYPE.SelectedIndex = 0 Then
                fn_LoadHistoryPasien("R.Jalan", False)
            ElseIf cboTYPE.SelectedIndex = 1 Then
                fn_LoadHistoryPasien("R.Inap", False)
            ElseIf cboTYPE.SelectedIndex = 2 Then
                fn_LoadHistoryPasien("", False)
                'ElseIf cboTYPE.SelectedIndex = 3 Then
                'fn_LoadDataDGCare()
            ElseIf cboTYPE.SelectedIndex = 3 Then
                fn_LoadData01(2)
            ElseIf cboTYPE.SelectedIndex = 4 Then
                fn_LoadData01(1)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_UploadDataGrouper()
        Dim sGagal As Integer = 0
        Dim oFaskes As New Reference.clsPPK
        Dim oDokter As New Reference.clsDoctor
        Dim oDiagnosa As New Reference.clsDiagnosa
        Dim oPoli As New Reference.clsDepartment
        Dim noRMEror As String = ""
        Dim listEror As New List(Of String)

        Try
            Dim sTotal As Integer = 0
            Dim sProcess As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    sTotal += 1
                End If
            Next

            If sTotal = 0 Then Exit Sub

            If MsgBox("Apakah Akan Upload data Sebanyak " & sTotal & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoRec(grv.GetRowCellValue(i, "norec"))

                    Dim ds = oGrouperRawatJalan.GetStructureHeader
                    With ds
                        If dsCariNosep Is Nothing Then
                            .kodegrouper = 0
                            .datecreated = Now
                        Else
                            .kodegrouper = dsCariNosep.kodegrouper
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = Now
                        .jeniskelompokpasien = If(IsDBNull(grv.GetRowCellValue(i, "jeniskelompokpasien")), "", grv.GetRowCellValue(i, "jeniskelompokpasien").ToString())
                        .statusenabled = If(IsDBNull(grv.GetRowCellValue(i, "statusenabled")), False, grv.GetRowCellValue(i, "statusenabled").ToString())
                        .norec = If(IsDBNull(grv.GetRowCellValue(i, "norec")), "", grv.GetRowCellValue(i, "norec").ToString())
                        .nostruklastfk = If(IsDBNull(grv.GetRowCellValue(i, "nostruklastfk")), "", grv.GetRowCellValue(i, "nostruklastfk").ToString())

                        'Clear data
                        .dpjp = ""
                        .dpjpkodevclaim = ""
                        .poli = ""
                        .polikodevclaim = ""
                        'akhir Clear data

                        Dim json As String = If(IsDBNull(grv.GetRowCellValue(i, "jsonpost")), "", grv.GetRowCellValue(i, "jsonpost").ToString())
                        If json <> "" Then
                            Dim person As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)

                            .jnsPelayanan = If(IsDBNull(person("response")("sep")("jnsPelayanan").ToString()), "", person("response")("sep")("jnsPelayanan").ToString())
                            Dim kelas As String = If(IsDBNull(person("response")("sep")("kelasRawat").ToString()), "", person("response")("sep")("kelasRawat").ToString())
                            .kelasRawat = IIf(kelas = "-", "-", "Kelas " & kelas)

                            Try
                                Dim dsFaskes = oFaskes.GetDataKodeFaskes(person("request")("data")("request")("t_sep")("rujukan")("ppkRujukan").ToString())
                                If dsFaskes IsNot Nothing Then
                                    .Faskes = dsFaskes.KODEFASKES & " " & dsFaskes.MEMO
                                Else
                                    Dim dsfaskes2 = oFaskes.GetDataKodeFaskes(If(IsDBNull(grv.GetRowCellValue(i, "ppkrujukan")), "", grv.GetRowCellValue(i, "ppkrujukan").ToString()))
                                    If dsfaskes2 IsNot Nothing Then
                                        .Faskes = dsfaskes2.KODEFASKES & " " & dsfaskes2.MEMO
                                    Else
                                        .Faskes = "-"
                                    End If
                                End If
                            Catch ex As Exception
                                Dim dsfaskes4 = oFaskes.GetDataKodeFaskes(If(IsDBNull(grv.GetRowCellValue(i, "ppkrujukan")), "", grv.GetRowCellValue(i, "ppkrujukan").ToString()))
                                If dsfaskes4 IsNot Nothing Then
                                    .Faskes = dsfaskes4.KODEFASKES & " " & dsfaskes4.MEMO
                                Else
                                    .Faskes = "-"
                                End If
                            End Try

                            .dpjpkodevclaim = If(IsDBNull(person("request")("data")("request")("t_sep")("dpjpLayan")), If(IsDBNull(person("request")("data")("request")("t_sep")("skdp")("kodeDPJP")), "", person("request")("data")("request")("t_sep")("skdp")("kodeDPJP").ToString()), person("request")("data")("request")("t_sep")("dpjpLayan").ToString())

                            If .dpjpkodevclaim <> "" Then
                                Dim dsDokter = oDokter.GetDataByKodeVclaim(.dpjpkodevclaim)
                                If dsDokter IsNot Nothing Then
                                    .dpjp = dsDokter.NAME_DISPLAY
                                Else
                                    .dpjp = ""
                                End If
                            Else
                                .dpjp = ""
                            End If

                            .polikodevclaim = If(IsDBNull(person("request")("data")("request")("t_sep")("poli")("tujuan")), "", person("request")("data")("request")("t_sep")("poli")("tujuan").ToString())
                            If .polikodevclaim <> "" Then
                                Dim dsPoli = oPoli.GetDataByKodeVclaim(.polikodevclaim)
                                If dsPoli IsNot Nothing Then
                                    .poli = dsPoli.NAME_DISPLAY
                                Else
                                    .poli = ""
                                End If
                            Else
                                .poli = ""
                            End If

                            .catatan = If(IsDBNull(person("request")("data")("request")("t_sep")("catatan")), "", person("request")("data")("request")("t_sep")("catatan").ToString())
                            .infromasiprb = If(IsDBNull(person("response")("sep")("informasi")("prolanisPRB")), "", person("response")("sep")("informasi")("prolanisPRB").ToString())
                            .peserta = If(IsDBNull(person("response")("sep")("peserta")("jnsPeserta").ToString()), "", person("response")("sep")("peserta")("jnsPeserta").ToString())
                            .diagnosaawal = If(IsDBNull(person("response")("sep")("diagnosa").ToString()), "", person("response")("sep")("diagnosa").ToString())
                            .cob = "-"
                            .nomortelepon = If(IsDBNull(person("request")("data")("request")("t_sep")("noTelp")), "", person("request")("data")("request")("t_sep")("noTelp").ToString())
                            Try
                                .asalrujukan = If(IsDBNull(person("request")("data")("request")("t_sep")("rujukan")("asalRujukan")), "", person("request")("data")("request")("t_sep")("rujukan")("asalRujukan").ToString())
                            Catch ex As Exception
                                .asalrujukan = If(IsDBNull(grv.GetRowCellValue(i, "asalrujukanfk")), "", grv.GetRowCellValue(i, "asalrujukanfk").ToString())
                            End Try
                            Try
                                .noRujukan = If(IsDBNull(person("request")("data")("request")("t_sep")("rujukan")("noRujukan")), "", person("request")("data")("request")("t_sep")("rujukan")("noRujukan").ToString())
                            Catch ex As Exception
                                .noRujukan = If(IsDBNull(grv.GetRowCellValue(i, "norujukan")), "", grv.GetRowCellValue(i, "norujukan").ToString())
                            End Try
                        Else
                            Dim dsJKN = oGrouperRawatJalan.GetDataByNoSepJkn(If(IsDBNull(grv.GetRowCellValue(i, "nosep")), "", grv.GetRowCellValue(i, "nosep").ToString()))
                            If dsJKN IsNot Nothing Then
                                .jnsPelayanan = dsJKN.jnsPelayanan
                                .kelasRawat = .kelasRawat = IIf(dsJKN.kelasRawat = "-", "-", "Kelas " & dsJKN.kelasRawat)
                                .polikodevclaim = dsJKN.poli
                            Else
                                .jnsPelayanan = If(IsDBNull(grv.GetRowCellValue(i, "jnspelayanan")), "", grv.GetRowCellValue(i, "jnspelayanan").ToString())
                                .kelasRawat = If(IsDBNull(grv.GetRowCellValue(i, "kelasrawat")), "", grv.GetRowCellValue(i, "kelasrawat").ToString())
                                .polikodevclaim = ""
                            End If

                            Dim dsfaskes3 = oFaskes.GetDataKodeFaskes(If(IsDBNull(grv.GetRowCellValue(i, "ppkrujukan")), "", grv.GetRowCellValue(i, "ppkrujukan").ToString()))
                            If dsfaskes3 IsNot Nothing Then
                                .Faskes = dsfaskes3.KODEFASKES & " " & dsfaskes3.MEMO
                            Else
                                .Faskes = "-"
                            End If
                            .catatan = If(IsDBNull(grv.GetRowCellValue(i, "catatan")), "", grv.GetRowCellValue(i, "catatan").ToString())
                            .infromasiprb = If(IsDBNull(grv.GetRowCellValue(i, "prolanisprb")), "", grv.GetRowCellValue(i, "prolanisprb").ToString())
                            .peserta = If(IsDBNull(grv.GetRowCellValue(i, "jeniskelompokpasien")), "", grv.GetRowCellValue(i, "jeniskelompokpasien").ToString())
                            .diagnosaawal = If(IsDBNull(grv.GetRowCellValue(i, "diagnosaawal")), "", grv.GetRowCellValue(i, "diagnosaawal").ToString())
                            .cob = "-"
                            .nomortelepon = If(IsDBNull(grv.GetRowCellValue(i, "notelepon")), "", grv.GetRowCellValue(i, "notelepon").ToString())
                            .asalrujukan = If(IsDBNull(grv.GetRowCellValue(i, "asalrujukanfk")), "", grv.GetRowCellValue(i, "asalrujukanfk").ToString())
                            .noRujukan = If(IsDBNull(grv.GetRowCellValue(i, "norujukan")), "", grv.GetRowCellValue(i, "norujukan").ToString())
                        End If

                        .noregistrasi = If(IsDBNull(grv.GetRowCellValue(i, "noregistrasi")), "", grv.GetRowCellValue(i, "noregistrasi").ToString())

                        If .dpjpkodevclaim = "" Then
                            .dpjpkodevclaim = If(IsDBNull(grv.GetRowCellValue(i, "kodedpjp")), If(IsDBNull(grv.GetRowCellValue(i, "kodedpjpmelayani")), "", grv.GetRowCellValue(i, "kodedpjpmelayani").ToString()), grv.GetRowCellValue(i, "kodedpjp").ToString())
                        End If
                        If .dpjp = "" Then
                            .dpjp = If(IsDBNull(grv.GetRowCellValue(i, "namadpjp")), If(IsDBNull(grv.GetRowCellValue(i, "namadjpjpmelayanni")), "", grv.GetRowCellValue(i, "namadjpjpmelayanni").ToString()), grv.GetRowCellValue(i, "namadpjp").ToString())
                        End If

                        .noRm = If(IsDBNull(grv.GetRowCellValue(i, "norm")), "", grv.GetRowCellValue(i, "norm").ToString())
                        .nama = If(IsDBNull(grv.GetRowCellValue(i, "nama")), "", grv.GetRowCellValue(i, "nama").ToString())
                        .noKartu = If(IsDBNull(grv.GetRowCellValue(i, "nokartu")), "", grv.GetRowCellValue(i, "nokartu").ToString())

                        If .noKartu = "" Then
                            .noKartu = If(IsDBNull(grv.GetRowCellValue(i, "nobpjs")), "", grv.GetRowCellValue(i, "nobpjs").ToString())
                        End If

                        If .polikodevclaim = "" Then
                            .polikodevclaim = If(IsDBNull(grv.GetRowCellValue(i, "polirujukankode")), "", grv.GetRowCellValue(i, "polirujukankode").ToString())
                        End If
                        If .poli = "" Then
                            .poli = If(IsDBNull(grv.GetRowCellValue(i, "poli")), "", grv.GetRowCellValue(i, "poli").ToString())
                        End If

                        .tglPlgSep = If(IsDBNull(grv.GetRowCellValue(i, "tglplgsep")), Now, grv.GetRowCellValue(i, "tglplgsep").ToString())
                        .tglSep = If(IsDBNull(grv.GetRowCellValue(i, "tglregistrasi")), Now, grv.GetRowCellValue(i, "tglregistrasi").ToString())

                        .tgl_lahir = If(IsDBNull(grv.GetRowCellValue(i, "tgllahir")), Now, grv.GetRowCellValue(i, "tgllahir").ToString())
                        Dim datetes As String = .tgl_lahir.ToString("yyyy-MM-dd")
                        If datetes = "0001-01-01" Then
                            Dim dsCekData = oGrouperRawatJalan.GetDataByKartuByTglLahir(.noKartu)
                            If dsCekData IsNot Nothing Then
                                .tgl_lahir = dsCekData.tgl_lahir
                            Else
                                Dim databpjs As String = fn_LoadKartuBPJS(.noKartu)
                                If databpjs <> "" Then
                                    .tgl_lahir = fn_LoadKartuBPJS(.noKartu)
                                End If
                            End If
                        End If

                        .gender = IIf(If(IsDBNull(grv.GetRowCellValue(i, "gender")), "", grv.GetRowCellValue(i, "gender").ToString()) = "1", "L", "P")
                        .status = ""
                        .jsonpost = If(IsDBNull(grv.GetRowCellValue(i, "jsonpost")), "", grv.GetRowCellValue(i, "jsonpost").ToString())
                        .statuspasien = If(IsDBNull(grv.GetRowCellValue(i, "statuspasien")), "", grv.GetRowCellValue(i, "statuspasien").ToString())
                        .noSep = If(IsDBNull(grv.GetRowCellValue(i, "nosep")), "", grv.GetRowCellValue(i, "nosep").ToString())
                        .ruangan = If(IsDBNull(grv.GetRowCellValue(i, "ruangan")), "", grv.GetRowCellValue(i, "ruangan").ToString())

                        If .noSep = "" Then
                            If .noKartu <> "" Then
                                Dim dsSep = oGrouperRawatJalan.GetDataByKartuTanggal(.noKartu, .tglSep.ToString("yyy-MM-dd"))
                                If dsSep IsNot Nothing Then
                                    .noSep = dsSep.noSep

                                    If .diagnosaawal = "" Then
                                        Dim dsDiagnosa = oDiagnosa.GetData(dsSep.diagnosa)
                                        If dsDiagnosa IsNot Nothing Then
                                            .diagnosaawal = dsDiagnosa.MEMO
                                        End If
                                    ElseIf .diagnosaawal = "-" Then
                                        Dim dsDiagnosa = oDiagnosa.GetData(dsSep.diagnosa)
                                        If dsDiagnosa IsNot Nothing Then
                                            .diagnosaawal = dsDiagnosa.MEMO
                                        End If
                                    End If

                                    If .noRujukan = "" Then
                                        .noRujukan = dsSep.noRujukan
                                    End If

                                    .jnsPelayanan = dsSep.jnsPelayanan
                                    .kelasRawat = IIf(dsSep.kelasRawat = "-", "-", "Kelas " & dsSep.kelasRawat)
                                End If
                            End If
                        End If

                        If IsDBNull(grv.GetRowCellValue(i, "tglpulang")) Then
                            'null
                        Else
                            .tglpulang = grv.GetRowCellValue(i, "tglpulang").ToString()
                        End If
                        .alamat = If(IsDBNull(grv.GetRowCellValue(i, "alamatrmh")), "", grv.GetRowCellValue(i, "alamatrmh").ToString())
                        .kduser = sUserID

                        .pangkat = ""
                        .kesatuan = ""
                        .pendidikan = ""
                        .statusmenikah = ""
                        .propinsi = ""
                        .kabupaten = ""
                        .kecamatan = ""
                        .kelurahan = ""

                        noRMEror = .noRm & " " & .nama & " Tgl Sep " & .tglSep.ToString("dd-MM-yyyy")
                    End With

                    Try
                        If dsCariNosep Is Nothing Then
                            If oGrouperRawatJalan.InsertData(ds) = False Then
                                sGagal += 1
                                listEror.Add(sGagal & " " & noRMEror)
                            End If
                        Else
                            If dsCariNosep.status = "" Then
                                If oGrouperRawatJalan.UpdateData(ds) = False Then
                                    sGagal += 1
                                    listEror.Add(sGagal & " " & noRMEror)
                                End If
                            End If
                        End If
                    Catch ex As Exception
                        sGagal += 1
                        listEror.Add(sGagal & " " & noRMEror & " " & ex.Message)
                    End Try

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " dari " & sTotal & "")

                    sProcess += 1
                End If
            Next

            SplashScreenManager.CloseForm(False)

            If sGagal = 0 Then
                MsgBox("Upload Selesai", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Upload Selesai Gagal " & String.Join(vbCrLf, listEror.ToArray), MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & noRMEror & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_UploadDataJKN()
        Dim sGagal As Integer = 0

        Try
            Dim sTotal As Integer = 0
            Dim sProcess As Integer = 0

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    sTotal += 1
                End If
            Next

            If sTotal = 0 Then Exit Sub

            If MsgBox("Apakah Akan Upload data Sebanyak " & sTotal & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoSepJkn(grv.GetRowCellValue(i, "noSep"))

                    Dim ds = oGrouperRawatJalan.GetStructureHeaderJkn
                    With ds
                        If dsCariNosep Is Nothing Then
                            .datecreated = Now
                        Else
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = Now
                        .diagnosa = If(IsDBNull(grv.GetRowCellValue(i, "diagnosa")), "", grv.GetRowCellValue(i, "diagnosa").ToString())
                        .jnsPelayanan = If(IsDBNull(grv.GetRowCellValue(i, "jnsPelayanan")), "", grv.GetRowCellValue(i, "jnsPelayanan").ToString())
                        .kelasRawat = If(IsDBNull(grv.GetRowCellValue(i, "kelasRawat")), "", grv.GetRowCellValue(i, "kelasRawat").ToString())
                        .nama = If(IsDBNull(grv.GetRowCellValue(i, "nama")), "", grv.GetRowCellValue(i, "nama").ToString())
                        .noKartu = If(IsDBNull(grv.GetRowCellValue(i, "noKartu")), "", grv.GetRowCellValue(i, "noKartu").ToString())
                        .noSep = If(IsDBNull(grv.GetRowCellValue(i, "noSep")), "", grv.GetRowCellValue(i, "noSep").ToString())
                        .noRujukan = If(IsDBNull(grv.GetRowCellValue(i, "noRujukan")), "", grv.GetRowCellValue(i, "noRujukan").ToString())
                        .poli = If(IsDBNull(grv.GetRowCellValue(i, "poli")), "", grv.GetRowCellValue(i, "poli").ToString())
                        .tglPlgSep = If(IsDBNull(grv.GetRowCellValue(i, "tglPlgSep")), "", grv.GetRowCellValue(i, "tglPlgSep").ToString())
                        .tglSep = If(IsDBNull(grv.GetRowCellValue(i, "tglSep")), Now, grv.GetRowCellValue(i, "tglSep").ToString())
                        .tglSep_Text = If(IsDBNull(grv.GetRowCellValue(i, "tglSep")), Now, grv.GetRowCellValue(i, "tglSep").ToString())
                        .kduser = sUserID
                    End With

                    If dsCariNosep Is Nothing Then
                        If oGrouperRawatJalan.InsertDataJKN(ds) = False Then
                            sGagal += 1
                        End If
                    Else
                        If oGrouperRawatJalan.UpdateDataJKN(ds) = False Then
                            sGagal += 1
                        End If
                    End If

                    SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " dari " & sTotal & "")

                    sProcess += 1
                End If
            Next

            SplashScreenManager.CloseForm(False)

            If sGagal = 0 Then
                MsgBox("Upload Selesai", MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Upload Selesai Gagal " & sGagal, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
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
    Private Function MergeFilesByte(ByVal sourceFiles As List(Of Byte())) As Byte()
        Try
            Dim mergedPdf As Byte() = Nothing
            Using ms As New MemoryStream()
                Using document As New Document()
                    Using copy As New PdfCopy(document, ms)
                        document.Open()
                        For i As Integer = 0 To sourceFiles.Count - 1
                            Dim reader As New PdfReader(sourceFiles(i))
                            ' loop over the pages in that document
                            Dim n As Integer = reader.NumberOfPages
                            Dim page As Integer = 0
                            While page < n
                                page = page + 1
                                copy.AddPage(copy.GetImportedPage(reader, page))
                            End While
                        Next
                    End Using
                End Using

                mergedPdf = ms.ToArray()

                Return mergedPdf

            End Using
        Catch ex As Exception
            MergeFilesByte = Nothing
            MsgBox("Load Merge Data : " & vbCrLf & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
    Private Function AppendArray(Of T)(ByVal thisArray() As T, ByVal itemToAppend As T) As T()
        If thisArray Is Nothing Then thisArray = New T() {}
        Dim tempList As List(Of T) = thisArray.ToList
        tempList.Add(itemToAppend)
        Return tempList.ToArray
    End Function
#End Region
#Region "Query"
    Private Function fn_LoadKartuBPJS(ByVal KARTUBPJS As String) As String
        fn_LoadKartuBPJS = ""

        Try
            If KARTUBPJS = String.Empty Then Exit Function

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimPesertaNoKartuBPJS(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, KARTUBPJS, Now.ToString("yyyy-MM-dd"))

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                    fn_LoadKartuBPJS = DataDecrypt("peserta")("tglLahir").ToString()
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadData01(ByVal JeniRawat As String)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimMonitoringDataKunjungan(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), JeniRawat)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                        Dim table As DataTable

                        table = New DataTable("DATAKUNJUNGAN")
                        table.Columns.Add("diagnosa")
                        table.Columns.Add("jnsPelayanan")
                        table.Columns.Add("kelasRawat")
                        table.Columns.Add("nama")
                        table.Columns.Add("noKartu")
                        table.Columns.Add("noSep")
                        table.Columns.Add("noRujukan")
                        table.Columns.Add("poli")
                        table.Columns.Add("tglPlgSep")
                        table.Columns.Add("tglSep")

                        For Each item In DataDecrypt("sep")
                            table.Rows.Add(New String() {item("diagnosa"), item("jnsPelayanan"), item("kelasRawat"), item("nama"), item("noKartu"), item("noSep"), item("noRujukan"), item("poli"), item("tglPlgSep"), item("tglSep")})
                        Next

                        grd.MainView = grv
                        grd.DataSource = table
                        grd.ForceInitialize()

                        SplashScreenManager.CloseForm(False)

                        fn_LoadFormatData()
                    Else
                        SplashScreenManager.CloseForm(False)
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                SplashScreenManager.CloseForm(False)
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadHistoryPasien(ByVal JenisRawat As String, ByVal Cek As Boolean)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

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
            SQL &= "* "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "CekGrouper = ISNULL((SELECT CASE MEMO WHEN '' THEN 'BELUM GROUPER' ELSE MEMO END FROM R_IDENTITAS_GROUPER_DATA WHERE A.kodegrouper = kodegrouper), 'BELUM GROUPER') "
            SQL &= ",* "
            SQL &= ",CekData = CASE A.propinsi WHEN '' THEN 'Y' ELSE ISNULL((SELECT CASE CATEGORY WHEN 0 THEN CASE KDPENDAFTARAN_AWAL WHEN '' THEN 'Y' ELSE 'N' END ELSE 'Y' END FROM S_PENDAFTARAN_H WHERE A.norec = KDPENDAFTARAN) , 'N') END "
            SQL &= "FROM "
            SQL &= "R_IDENTITAS_GROUPER A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.tglSep, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.tglSep, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            If JenisRawat <> "" Then
                SQL &= "AND A.jnsPelayanan = '" & JenisRawat & "' "
            End If
            SQL &= ") X "
            SQL &= "WHERE "
            SQL &= "X.CekData = 'Y' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_IDENTITAS_GROUPER")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_IDENTITAS_GROUPER")
            grd.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            'grv.Columns("norec").VisibleIndex = -1
            grv.Columns("nostruklastfk").VisibleIndex = -1
            grv.Columns("jsonpost").VisibleIndex = -1
            grv.Columns("CekData").VisibleIndex = -1

            fn_LoadFormatData()

            If Cek = True Then
                If MsgBox("Apkah Yakin akan Update Nomor SEP?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                Dim sJumlah As Integer = 0
                Dim sJumlahTidakAdaSEP As Integer = 0

                For iLoop As Integer = 0 To ds.Tables("R_IDENTITAS_GROUPER").Rows.Count - 1
                    With ds.Tables("R_IDENTITAS_GROUPER")
                        If .Rows(iLoop)("propinsi") <> "" Then
                            If .Rows(iLoop)("jeniskelompokpasien") = "BPJS" Then
                                If .Rows(iLoop)("noSep") = "" Then
                                    sJumlahTidakAdaSEP = +1

                                    Dim dsDaftar = oPendaftaran.GetData(.Rows(iLoop)("norec"))
                                    If dsDaftar IsNot Nothing Then
                                        'If dsDaftar.KDPENDAFTARAN = "RJ2025B000002" Then
                                        '    MsgBox("")
                                        'End If
                                        Dim dsCekSEP = oGrouperRawatJalan.GetDataByKartuTanggal(dsDaftar.KARTUBPJS, dsDaftar.DATE.ToString("yyyy-MM-dd"))
                                        If dsCekSEP IsNot Nothing Then
                                            If dsCekSEP.jnsPelayanan = "R.Jalan" Then
                                                If dsDaftar.CATEGORY = 0 Then
                                                    oPendaftaran.UpdateSEPTransaksi(dsDaftar.KDPENDAFTARAN, dsCekSEP.noSep)
                                                    oPendaftaran.UpdateSEPTransaksiGrouper(dsDaftar.KDPENDAFTARAN, dsCekSEP.noSep)
                                                    sJumlah = +1
                                                End If
                                            Else
                                                If dsDaftar.CATEGORY = 1 Then
                                                    oPendaftaran.UpdateSEPTransaksi(dsDaftar.KDPENDAFTARAN, dsCekSEP.noSep)
                                                    oPendaftaran.UpdateSEPTransaksiGrouper(dsDaftar.KDPENDAFTARAN, dsCekSEP.noSep)
                                                    sJumlah = +1
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                    End With
                Next

                fn_LoadHistoryPasien(JenisRawat, False)

                MsgBox("Jumlah Ke Update SEP " & sJumlah & " Jumlah SEP Kosong " & sJumlahTidakAdaSEP, MsgBoxStyle.Information, Me.Text)
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_LoadDataDGCare()
    '    Try
    '        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
    '        SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

    '        Dim oConnNpgsql As New NpgsqlConnection
    '        Dim oCommNpgsql As New NpgsqlCommand
    '        Dim daNpgsql As NpgsqlDataAdapter
    '        Dim dsNpgsql As New DataSet
    '        Dim SQLNpgsql As String

    '        Dim sConnNpgsql As String = sConnNpgsqlproduction

    '        oConnNpgsql = New NpgsqlConnection(sConnNpgsql)

    '        If oConnNpgsql.State = ConnectionState.Closed Then
    '            oConnNpgsql.Open()
    '        End If

    '        SQLNpgsql = "select "
    '        SQLNpgsql &= "b.jeniskelompokpasien "
    '        SQLNpgsql &= ",b.statusenabled "
    '        SQLNpgsql &= ",b.norec "
    '        SQLNpgsql &= ",b.noregistrasi "
    '        SQLNpgsql &= ",b.nostruklastfk "
    '        SQLNpgsql &= ",b.jenispelayanan as jnspelayanan "
    '        SQLNpgsql &= ",c.namakelas as kelasrawat "
    '        SQLNpgsql &= ",d.nocm as norm "
    '        SQLNpgsql &= ",d.namapasien as nama "
    '        SQLNpgsql &= ",a.nokepesertaan as nokartu "
    '        SQLNpgsql &= ",d.nobpjs "
    '        SQLNpgsql &= ",a.nosep as nosep "
    '        SQLNpgsql &= ",a.norujukan as norujukan "
    '        SQLNpgsql &= ",a.polirujukannama as poli "
    '        SQLNpgsql &= ",a.polirujukankode "
    '        SQLNpgsql &= ",f.reportdisplay as ruangan "
    '        SQLNpgsql &= ",b.tglregistrasi "
    '        SQLNpgsql &= ",a.tanggalsep as tglplgsep "
    '        SQLNpgsql &= ",CAST(d.tgllahir AS date) AS tgllahir "
    '        SQLNpgsql &= ",d.objectjeniskelaminfk as gender "
    '        SQLNpgsql &= ",CONCAT(e.kddiagnosa, ' - ', e.namadiagnosa) as diagnosaawal "
    '        SQLNpgsql &= ",b.statuspasien "
    '        SQLNpgsql &= ",b.jeniskelompokpasien "
    '        SQLNpgsql &= ",a.ppkrujukan "
    '        SQLNpgsql &= ",a.kodedpjp "
    '        SQLNpgsql &= ",a.namadpjp "
    '        SQLNpgsql &= ",a.kodedpjpmelayani "
    '        SQLNpgsql &= ",a.namadjpjpmelayanni "
    '        SQLNpgsql &= ",a.catatan "
    '        SQLNpgsql &= ",a.prolanisprb "
    '        SQLNpgsql &= ",a.asalrujukanfk "
    '        SQLNpgsql &= ",d.notelepon "
    '        SQLNpgsql &= ",b.tglpulang "
    '        SQLNpgsql &= ",d.alamatrmh "
    '        SQLNpgsql &= ",a.jsonpost "
    '        SQLNpgsql &= "FROM "
    '        SQLNpgsql &= "pasiendaftar_t b "
    '        SQLNpgsql &= "left join pemakaianasuransi_t a "
    '        SQLNpgsql &= "on a.noregistrasifk = b.norec "
    '        SQLNpgsql &= "inner join kelas_m c "
    '        SQLNpgsql &= "on b.objectkelasfk = c.id "
    '        SQLNpgsql &= "inner join pasien_m d "
    '        SQLNpgsql &= "on b.nocmfk = d.id "
    '        SQLNpgsql &= "left join diagnosa_m e "
    '        SQLNpgsql &= "on a.diagnosisfk = e.id "
    '        SQLNpgsql &= "left join ruangan_m f "
    '        SQLNpgsql &= "on b.objectruanganasalfk = f.id "
    '        SQLNpgsql &= "where b.tglregistrasi BETWEEN '" & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & " 00:00:00" & "' and '" & deDATETo.DateTime.ToString("yyyy-MM-dd") & " 23:59:59" & "' "
    '        'SQLNpgsql &= "and b.statusenabled = 't' "

    '        oCommNpgsql.Connection = oConnNpgsql
    '        oCommNpgsql.CommandText = SQLNpgsql
    '        oCommNpgsql.CommandTimeout = 120
    '        oCommNpgsql.CommandType = CommandType.Text

    '        daNpgsql = New NpgsqlDataAdapter(oCommNpgsql)
    '        daNpgsql.Fill(dsNpgsql, "HISTORY")

    '        If oConnNpgsql.State = ConnectionState.Open Then
    '            oConnNpgsql.Close()
    '        End If

    '        grd.MainView = grv
    '        grd.DataSource = dsNpgsql.Tables("HISTORY")
    '        grd.ForceInitialize()

    '        grv.Columns("norec").VisibleIndex = -1
    '        grv.Columns("nostruklastfk").VisibleIndex = -1
    '        grv.Columns("jsonpost").VisibleIndex = -1

    '        SplashScreenManager.CloseForm(False)

    '        fn_LoadFormatData()

    '    Catch oErr As Exception
    '        SplashScreenManager.CloseForm(False)
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Public Function fn_LoadQuey(ByVal norec As String) As String
        Try
            fn_LoadQuey = ""
            Dim SQLNpgsql As String

            Dim SQL As String = String.Empty

            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oCashin As New Finance.clsCashIn
            Dim dsPendaftaran = oPendaftaran.GetData(norec)
            Dim TanggalPulang As DateTime = Now

            Dim sCekInvoice As Boolean = False

            If dsPendaftaran IsNot Nothing Then
                If dsPendaftaran.CATEGORY = 1 Then
                    Dim dsCekKwitansi = oCashin.GetDataBypendaftaran(dsPendaftaran.KDPENDAFTARAN)
                    If dsCekKwitansi IsNot Nothing Then
                        sCekInvoice = True
                    End If

                    Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                    Dim dsPulang = oPulang.GetDatabyKD(dsPendaftaran.KDPENDAFTARAN)
                    If dsPulang IsNot Nothing Then
                        TanggalPulang = dsPulang.DATE
                    Else
                        TanggalPulang = dsPendaftaran.DATE
                        MsgBox("Pasien Belum di Pulangkan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If

                SQL = " SELECT  "
                SQL &= " D.KDPENDAFTARAN "
                SQL &= " ,DPJP = (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) "
                SQL &= " ,PASIEN = (SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE D.KDCUSTOMER = KDCUSTOMER) "
                SQL &= " ,D.KDCUSTOMER "
                SQL &= " ,C.ALAMAT "
                SQL &= " ,KELAS = (SELECT MEMO FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT) "
                SQL &= " ,TANGGAL_DATANG = D.DATE "
                SQL &= " ,D.CATEGORY "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,NAMAKUNJUNGAN = (SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE C.KDDEPARTMENT = KDDEPARTMENT) "
                SQL &= " ,A.KDSOTRANSAKSI "
                SQL &= " ,TANGGAL_INPUT = A.DATE "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,namaproduk = CASE WHEN E.NMITEM2 LIKE '%VISITE%' THEN E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' ELSE E.NMITEM2 END   "
                'If dsPendaftaran.M_DAFTAR_L2.MEMO = "BPJS" Then
                '    SQL &= " ,namaproduk = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
                'Else
                '    SQL &= " ,namaproduk = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
                'End If
                SQL &= " ,jumlah = CONVERT(decimal(12,2), SUM(B.QTY)) "
                If sHargaApotik = True Then
                    SQL &= " ,hargajual = CONVERT(decimal(12,2), SUM(B.GRANDTOTAL)) "
                Else
                    SQL &= " ,hargajual = CASE WHEN G.MEMO LIKE '%PROGRAM NOM%' THEN CONVERT(decimal(12,2), ISNULL((SELECT PRICEPURCHASESTANDARD * SUM(B.QTY) FROM M_ITEM_UOM WHERE B.KDITEM = KDITEM AND B.KDUOM = KDUOM), 0))  ELSE CONVERT(decimal(12,2), SUM(B.GRANDTOTAL)) END "
                End If

                SQL &= " ,produkfk = B.KDITEM "
                SQL &= " ,Obat = E.ISSTOK "
                If sCekInvoice = True Then
                    If dsPendaftaran.CATEGORY = 1 Then
                        SQL &= " ,H.KDUSER "
                        SQL &= " ,H.SUBTOTAL "
                        SQL &= " ,H.ADMIN "
                        SQL &= " ,GRANDTOTALHEADER = H.GRANDTOTAL "
                    Else
                        SQL &= " ,KDUSER = '' "
                        SQL &= " ,SUBTOTAL = 0 "
                        SQL &= " ,ADMIN = 0 "
                        SQL &= " ,GRANDTOTALHEADER = 0 "
                    End If
                Else
                    SQL &= " ,KDUSER = '' "
                    SQL &= " ,SUBTOTAL = 0 "
                    SQL &= " ,ADMIN = 0 "
                    SQL &= " ,GRANDTOTALHEADER = 0 "
                End If
                SQL &= " FROM "
                SQL &= " S_SO_TRANSAKSI_H A "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D B "
                SQL &= " ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
                SQL &= " ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
                SQL &= " INNER JOIN S_PENDAFTARAN_H D "
                SQL &= " ON C.KDPENDAFTARAN = D.KDPENDAFTARAN "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON B.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L3 F "
                SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3 "
                SQL &= " INNER JOIN M_ITEM_L1 G "
                SQL &= " ON E.KDITEM_L1 = G.KDITEM_L1 "

                If sCekInvoice = True Then
                    If dsPendaftaran.CATEGORY = 1 Then
                        SQL &= " INNER JOIN F_CASHIN_D G "
                        SQL &= " ON A.KDSOTRANSAKSI = G.NOINVOICE "
                        SQL &= " INNER JOIN F_CASHIN_H H "
                        SQL &= " ON G.KDCASHIN = H.KDCASHIN "
                    End If
                End If

                SQL &= " WHERE  "
                SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN & "' "
                If dsPendaftaran.CATEGORY = 1 Then
                    SQL &= " OR "
                    SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN_AWAL & "' "
                End If
                SQL &= " GROUP BY "
                SQL &= " D.KDPENDAFTARAN "
                SQL &= " ,D.KDDOCTOR "
                SQL &= " ,D.KDCUSTOMER "
                SQL &= " ,C.ALAMAT "
                SQL &= " ,D.KDKELASRAWAT "
                SQL &= " ,D.DATE "
                SQL &= " ,D.CATEGORY "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,C.KDDEPARTMENT  "
                SQL &= " ,A.KDSOTRANSAKSI "
                SQL &= " ,F.MEMO "
                SQL &= " ,E.NMITEM2 "
                SQL &= " ,A.DATE "
                SQL &= " ,B.KDDOCTOR "
                SQL &= " ,B.KDITEM "
                SQL &= " ,B.KDUOM "
                SQL &= " ,G.MEMO "
                SQL &= " ,E.ISSTOK "
                If sCekInvoice = True Then
                    If dsPendaftaran.CATEGORY = 1 Then
                        SQL &= " ,H.KDUSER "
                        SQL &= " ,H.SUBTOTAL "
                        SQL &= " ,H.ADMIN "
                        SQL &= " ,H.GRANDTOTAL "
                    End If
                End If

                SQL &= " ORDER BY F.MEMO "
            End If

            SQLNpgsql = SQL

            fn_LoadQuey = SQLNpgsql

        Catch oErr As Exception
            fn_LoadQuey = ""
            MsgBox("Load Query Rincian DgCare" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.BestFitColumns()
    End Sub
    Private Sub fn_LoadFormatDataRincian()
        For iLoop As Integer = 0 To grvRincian.Columns.Count - 1
            If grvRincian.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grvRincian.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                grvRincian.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grvRincian.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n0}"
            ElseIf grvRincian.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvRincian.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvRincian.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grvRincian.BestFitColumns()
    End Sub
#End Region
#Region "xtra Report"
    Private Sub fn_LoadMergePDF(ByVal FolderSimpan As String, ByVal FolderSimpanPDF As String, ByVal norec As String, ByVal NotUpload As Boolean, ByVal isKronis As Boolean)
        Try
            Dim dataList As New List(Of Byte())
            Dim dsLoad() As Byte

            Dim dsPendaftaran = oPendaftaran.GetData(norec)
            If dsPendaftaran IsNot Nothing Then
                Dim alamatsep As String = FolderSimpan & "1" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                If dsPendaftaran.M_DAFTAR_L1.MEMO = "BPJS" Then
                    If dsPendaftaran.NOMORSEP <> "" Then
                        If isKronis = False Then
                            '--------------------1 CETAK LIP KLAIM

                            Dim Cetak As String = fn_Cetakklaim(dsPendaftaran.NOMORSEP, NotUpload)

                            If Cetak <> "" Then
                                Dim Base64Byte() As Byte
                                Base64Byte = Convert.FromBase64String(Cetak)
                                dataList.Add(Base64Byte)
                            End If
                        End If
                        '--------------------2 CETAK SEP
                        If dsPendaftaran.M_DEPARTMENT.OTHER = "ANTRIAN" Then
                            Dim a, b, c As String
                            a = Year(dsPendaftaran.DATE)
                            b = Year(dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
                            c = a - b
                            sUmur = c & " tahun"

                            Dim rpt As New xtraSEPNomorAntrian
                            rpt.bindingSource.DataSource = dsPendaftaran
                            rpt.ExportToPdf(alamatsep)
                        Else
                            Dim a, b, c As String
                            a = Year(dsPendaftaran.DATE)
                            b = Year(dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
                            c = a - b
                            sUmur = c & " tahun"

                            Dim rpt As New xtraSEP
                            rpt.bindingSource.DataSource = dsPendaftaran
                            rpt.ExportToPdf(alamatsep)
                        End If

                        If alamatsep <> "" Then
                            If FileIO.FileSystem.FileExists(alamatsep) Then
                                dataList.Add(File.ReadAllBytes(alamatsep))
                            End If
                        End If
                    Else
                        MsgBox("Nomor Sep Kosong, Silahkan Lakukan Update Nomor SEP", MsgBoxStyle.Critical, Me.Text)
                    End If

                    If isKronis = False Then
                        'SKD
                        Dim dsSKD = oSKD.GetData(dsPendaftaran.NOMORSKDP)
                        If dsSKD IsNot Nothing Then
                            Dim alamatskd As String = FolderSimpan & "1SKD" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf"
                            Dim rptskd As New xtraRencanaKontrol
                            rptskd.bindingSource.DataSource = dsSKD
                            rptskd.ExportToPdf(alamatskd)

                            If FileIO.FileSystem.FileExists(alamatskd) Then
                                dataList.Add(File.ReadAllBytes(alamatskd))
                            End If
                        End If
                    End If
                Else
                    Dim rpt As New xtraUmum
                    Dim a, b, c As String
                    a = Year(dsPendaftaran.DATE)
                    b = Year(dsPendaftaran.M_CUSTOMER.TANGGALLAHIR)
                    c = a - b

                    sUmur = c & " tahun"
                    rpt.bindingSource.DataSource = dsPendaftaran
                    rpt.ExportToPdf(alamatsep)

                    If alamatsep <> "" Then
                        If FileIO.FileSystem.FileExists(alamatsep) Then
                            dataList.Add(File.ReadAllBytes(alamatsep))
                        End If
                    End If
                End If

                Dim listKDIDENTITASKODE_1 As New List(Of Integer)
                Dim listKDIDENTITASKODE_2 As New List(Of Integer)
                Dim listKDKUNJUNGAN_1 As New List(Of String)
                Dim listKDKUNJUNGAN_2 As New List(Of String)

                If isKronis = False Then
                    Try
                        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Processing data Identitas.....")

                        For Each xloop In oKunjungan.GetDataListByKodePendaftaran(dsPendaftaran.KDPENDAFTARAN)
                            listKDKUNJUNGAN_1.Add(xloop.KDKUNJUNGAN)
                            Dim dsIdentitas = oGrouperRawatJalan.GetDataByKodeKunjungan(xloop.KDKUNJUNGAN)
                            If dsIdentitas IsNot Nothing Then
                                listKDIDENTITASKODE_1.Add(dsIdentitas.KDIDENTITAS)
                            End If
                        Next
                        If dsPendaftaran.CATEGORY = 1 Then
                            For Each xloop In oKunjungan.GetDataListByKodePendaftaran(dsPendaftaran.KDPENDAFTARAN_AWAL)
                                listKDKUNJUNGAN_2.Add(xloop.KDKUNJUNGAN)

                                Dim dsIdentitas = oGrouperRawatJalan.GetDataByKodeKunjungan(xloop.KDKUNJUNGAN)
                                If dsIdentitas IsNot Nothing Then
                                    listKDIDENTITASKODE_2.Add(dsIdentitas.KDIDENTITAS)
                                End If
                            Next
                        End If

                        SplashScreenManager.CloseForm(False)
                    Catch ex As Exception
                        SplashScreenManager.CloseForm(False)
                    End Try

                    Try
                        '--------------------3 RESUME RAWAT JALAN DAN RAWAT INAP
                        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Processing data Resume.....")

                        If dsPendaftaran.CATEGORY = 0 Then
                            For Each xloop In listKDIDENTITASKODE_1
                                Dim dsCPPT = oGrouperCPPT.GetDataValidasiRawatJalanUntukResume(xloop)
                                If dsCPPT IsNot Nothing Then
                                    Dim cetakresumerj As String = fn_CetakResumeRawatJalan(dsCPPT.KDCPPT, FolderSimpan & "3" & dsCPPT.KDCPPT & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                                    If cetakresumerj <> "" Then
                                        If FileIO.FileSystem.FileExists(cetakresumerj) Then
                                            dataList.Add(File.ReadAllBytes(cetakresumerj))
                                        End If
                                    End If
                                End If
                            Next
                        Else
                            Dim cetakresumeri As String = fn_CetakResumeRawatInap(norec, FolderSimpan & "3" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                            If cetakresumeri <> "" Then
                                If FileIO.FileSystem.FileExists(cetakresumeri) Then
                                    dataList.Add(File.ReadAllBytes(cetakresumeri))
                                End If
                            End If
                        End If

                        SplashScreenManager.CloseForm(False)
                    Catch ex As Exception
                        SplashScreenManager.CloseForm(False)
                        MsgBox("Resume eror" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                End If

                If isKronis = False Then
                    Try
                        SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
                        SplashScreenManager.Default.SetWaitFormCaption("Processing data Dokumen RME.....")

                        '--------------------5, 6, 7 ASESMEN

                        For Each xloop In oDigitalAsesmenMedisIGD.GetDataDetailbynorec(dsPendaftaran.KDPENDAFTARAN)
                            If xloop.ISDELETE = False Then
                                Dim alamatasesmen As String = xtraReportAsesmenAwalMedisIGD(xloop.KODE, FolderSimpan & "5" & xloop.KODE & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                                If FileIO.FileSystem.FileExists(alamatasesmen) Then
                                    dataList.Add(File.ReadAllBytes(alamatasesmen))
                                End If
                            End If
                        Next

                        Dim alamatasesmenhemodialisa As String = xtraReportAsesmenHemodialisa(dsPendaftaran.KDPENDAFTARAN, FolderSimpan & "5.1" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                        If FileIO.FileSystem.FileExists(alamatasesmenhemodialisa) Then
                            dataList.Add(File.ReadAllBytes(alamatasesmenhemodialisa))
                        End If

                        Dim alamatasesmenDischarge1 As String = xtraReportDischaregPlanning(dsPendaftaran.KDPENDAFTARAN, FolderSimpan & "6" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                        If FileIO.FileSystem.FileExists(alamatasesmenDischarge1) Then
                            dataList.Add(File.ReadAllBytes(alamatasesmenDischarge1))
                        End If

                        For Each xloop In listKDIDENTITASKODE_1
                            For Each yloop In oDigitalLaporanPersalinan.GetDataDetailbykdidentitasList(xloop)
                                Dim alamatasesmenLaporanPersalinan1 As String = xtraReportLaporanPersalinan(yloop.KDLAPROANPERSALINAN, FolderSimpan & "7" & yloop.KDLAPROANPERSALINAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")
                                If FileIO.FileSystem.FileExists(alamatasesmenLaporanPersalinan1) Then
                                    dataList.Add(File.ReadAllBytes(alamatasesmenLaporanPersalinan1))
                                End If
                            Next
                        Next

                        For Each xloop In listKDIDENTITASKODE_2
                            For Each yloop In oDigitalLaporanPersalinan.GetDataDetailbykdidentitasList(xloop)
                                Dim alamatasesmenLaporanPersalinan1 As String = xtraReportLaporanPersalinan(yloop.KDLAPROANPERSALINAN, FolderSimpan & "7" & yloop.KDLAPROANPERSALINAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")
                                If FileIO.FileSystem.FileExists(alamatasesmenLaporanPersalinan1) Then
                                    dataList.Add(File.ReadAllBytes(alamatasesmenLaporanPersalinan1))
                                End If
                            Next
                        Next

                        For Each xloop In oDigitalLaporanOperasi.GetDataByKodePendaftaranList(dsPendaftaran.KDPENDAFTARAN)
                            Dim alamatDigital As String = xtraReportLaporanOperasi(xloop.KDLAPORANOPERASI, FolderSimpan & "8" & xloop.KDLAPORANOPERASI & Now.ToString("yyyyMMdd HHmmss") & ".pdf")
                            If FileIO.FileSystem.FileExists(alamatDigital) Then
                                dataList.Add(File.ReadAllBytes(alamatDigital))
                            End If
                        Next

                        If dsPendaftaran.KDPENDAFTARAN_AWAL <> "" Then
                            For Each xloop In oDigitalAsesmenMedisIGD.GetDataDetailbynorec(dsPendaftaran.KDPENDAFTARAN_AWAL)
                                Dim alamatasesmen As String = xtraReportAsesmenAwalMedisIGD(xloop.KODE, FolderSimpan & "5" & xloop.KODE & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                                If FileIO.FileSystem.FileExists(alamatasesmen) Then
                                    dataList.Add(File.ReadAllBytes(alamatasesmen))
                                End If
                            Next

                            Dim alamatasesmenhemodialisa2 As String = xtraReportAsesmenHemodialisa(dsPendaftaran.KDPENDAFTARAN_AWAL, FolderSimpan & "5.1" & dsPendaftaran.KDPENDAFTARAN_AWAL & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                            If FileIO.FileSystem.FileExists(alamatasesmenhemodialisa2) Then
                                dataList.Add(File.ReadAllBytes(alamatasesmenhemodialisa2))
                            End If

                            Dim alamatasesmenDischarge2 As String = xtraReportDischaregPlanning(dsPendaftaran.KDPENDAFTARAN_AWAL, FolderSimpan & "6" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf")

                            If FileIO.FileSystem.FileExists(alamatasesmenDischarge2) Then
                                dataList.Add(File.ReadAllBytes(alamatasesmenDischarge2))
                            End If

                            For Each xloop In oDigitalLaporanOperasi.GetDataByKodePendaftaranList(dsPendaftaran.KDPENDAFTARAN_AWAL)
                                Dim alamatDigital As String = xtraReportLaporanOperasi(xloop.KDLAPORANOPERASI, FolderSimpan & "8" & xloop.KDLAPORANOPERASI & Now.ToString("yyyyMMdd HHmmss") & ".pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital))
                                End If
                            Next
                        End If

                        If dsPendaftaran.M_DEPARTMENT.VCLAIM_KODEPOLI = "IRM" Then
                            For Each xloop In listKDKUNJUNGAN_1
                                Dim alamatDigital1_ As String = xtraReportLembarUjiFungsi1(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Uji1.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital1_) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital1_))
                                End If
                                'Dim alamatDigital1 As String = xtraReportLembarUjiFungsi2(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Uji.pdf")
                                'If FileIO.FileSystem.FileExists(alamatDigital1) Then
                                '    dataList.Add(File.ReadAllBytes(alamatDigital1))
                                'End If
                                Dim alamatDigital2 As String = xtraReportCatatanKlinisRehab(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Catatan.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital2) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital2))
                                End If
                                Dim alamatDigital3 As String = xtraReportResumeRehab(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Resume.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital3) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital3))
                                End If

                                For Each zloop In oGrouperCPPT.GetDataDetailCPPTByKDKUNJUNGAN(xloop)
                                    Dim alamatDigital4 As String = xtraReportLaporanLembarProgramTerapi(zloop.KDCPPT, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "LembarTerapi.pdf")
                                    If FileIO.FileSystem.FileExists(alamatDigital4) Then
                                        dataList.Add(File.ReadAllBytes(alamatDigital4))
                                    End If
                                Next
                            Next

                            For Each xloop In listKDKUNJUNGAN_2
                                Dim alamatDigital1_ As String = xtraReportLembarUjiFungsi1(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Uji1.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital1_) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital1_))
                                End If
                                'Dim alamatDigital1 As String = xtraReportLembarUjiFungsi2(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Uji.pdf")
                                'If FileIO.FileSystem.FileExists(alamatDigital1) Then
                                '    dataList.Add(File.ReadAllBytes(alamatDigital1))
                                'End If
                                Dim alamatDigital2 As String = xtraReportCatatanKlinisRehab(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Catatan.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital2) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital2))
                                End If
                                Dim alamatDigital3 As String = xtraReportResumeRehab(xloop, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "Resume.pdf")
                                If FileIO.FileSystem.FileExists(alamatDigital3) Then
                                    dataList.Add(File.ReadAllBytes(alamatDigital3))
                                End If
                                For Each zloop In oGrouperCPPT.GetDataDetailCPPTByKDKUNJUNGAN(xloop)
                                    Dim alamatDigital4 As String = xtraReportLaporanLembarProgramTerapi(zloop.KDCPPT, FolderSimpan & "9" & xloop & Now.ToString("yyyyMMdd HHmmss") & "LembarTerapi.pdf")
                                    If FileIO.FileSystem.FileExists(alamatDigital4) Then
                                        dataList.Add(File.ReadAllBytes(alamatDigital4))
                                    End If
                                Next
                            Next
                        End If

                        SplashScreenManager.CloseForm(False)


                        '--------------------4 CETAK RINCIAN

                        Dim cetakrincian As String = fn_Rincian(True, 0, dsPendaftaran.KDPENDAFTARAN, FolderSimpan & "4" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf", NotUpload)

                        If cetakrincian <> "" Then
                            If FileIO.FileSystem.FileExists(cetakrincian) Then
                                dataList.Add(File.ReadAllBytes(cetakrincian))
                            End If
                        End If
                    Catch ex As Exception
                        SplashScreenManager.CloseForm(False)
                        MsgBox("Dokumen RME Eror" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    'Hasil Scan PDF
                    'Dim kodegrouper1 As Integer = 0
                    'Dim kodegrouper2 As Integer = 0

                    'Dim dsData = oGrouperRawatJalan.GetDataByNoRec(dsPendaftaran.KDPENDAFTARAN)
                    'If dsData IsNot Nothing Then
                    '    kodegrouper1 = dsData.kodegrouper
                    'End If

                    'If dsPendaftaran.KDPENDAFTARAN_AWAL <> "" Then
                    '    Dim dsData2 = oGrouperRawatJalan.GetDataByNoRec(dsPendaftaran.KDPENDAFTARAN_AWAL)
                    '    If dsData2 IsNot Nothing Then
                    '        kodegrouper2 = dsData2.kodegrouper
                    '    End If
                    'End If

                    If dsPendaftaran.KDPENDAFTARAN_AWAL <> "" Then
                        For Each xloop In oGrouperRawatJalan.GetDataGrouperPDFByKdPendaftaaranList(dsPendaftaran.KDPENDAFTARAN_AWAL)
                            If xloop.ISCHEKED = True Then
                                If xloop.ALAMAT_UPLOAD <> "" Then
                                    If xloop.TYPEFILE.Contains(".pdf") Then
                                        If FileIO.FileSystem.FileExists(xloop.ALAMAT_UPLOAD) Then
                                            dataList.Add(File.ReadAllBytes(xloop.ALAMAT_UPLOAD))
                                        End If
                                    Else
                                        Dim FolderSimpaGambar = "C:/SIMRS/UPLOADCASEMIX/"

                                        Try
                                            If Not Directory.Exists(FolderSimpaGambar) Then
                                                Directory.CreateDirectory(FolderSimpaGambar)
                                            Else
                                                DeleteDirectory(FolderSimpaGambar)
                                                Directory.CreateDirectory(FolderSimpaGambar)
                                            End If
                                        Catch ex As Exception

                                        End Try

                                        Dim Simpan As String = Now.ToString("yyyyMMddHHmmss") & "Upload.pdf"

                                        ConvertImageToPDF(xloop.ALAMAT_UPLOAD, FolderSimpaGambar & Simpan)

                                        If FileIO.FileSystem.FileExists(FolderSimpaGambar & Simpan) Then
                                            dataList.Add(File.ReadAllBytes(FolderSimpaGambar & Simpan))
                                        End If
                                    End If
                                End If
                            End If
                        Next
                    End If

                    For Each xloop In oGrouperRawatJalan.GetDataGrouperPDFByKdPendaftaaranList(dsPendaftaran.KDPENDAFTARAN)
                        If xloop.ISCHEKED = True Then
                            If xloop.ALAMAT_UPLOAD <> "" Then
                                If xloop.TYPEFILE.Contains(".pdf") Then
                                    If FileIO.FileSystem.FileExists(xloop.ALAMAT_UPLOAD) Then
                                        dataList.Add(File.ReadAllBytes(xloop.ALAMAT_UPLOAD))
                                    End If
                                Else
                                    Dim FolderSimpaGambar = "C:/SIMRS/UPLOADCASEMIX/"

                                    Try
                                        If Not Directory.Exists(FolderSimpaGambar) Then
                                            Directory.CreateDirectory(FolderSimpaGambar)
                                        Else
                                            DeleteDirectory(FolderSimpaGambar)
                                            Directory.CreateDirectory(FolderSimpaGambar)
                                        End If
                                    Catch ex As Exception

                                    End Try

                                    Dim Simpan As String = Now.ToString("yyyyMMddHHmmss") & "Upload.pdf"

                                    ConvertImageToPDF(xloop.ALAMAT_UPLOAD, FolderSimpaGambar & Simpan)

                                    If FileIO.FileSystem.FileExists(FolderSimpaGambar & Simpan) Then
                                        dataList.Add(File.ReadAllBytes(FolderSimpaGambar & Simpan))
                                    End If
                                End If
                            End If
                        End If
                    Next

                    If chkCPPT.Checked = True Then
                        If dsPendaftaran.CATEGORY = 0 Then
                            Dim FolderSimpancppt = "C:/SIMRS/CPPTRAJALCASEMIX/"

                            If Not Directory.Exists(FolderSimpancppt) Then
                                Directory.CreateDirectory(FolderSimpancppt)
                            Else
                                DeleteDirectory(FolderSimpancppt)
                                Directory.CreateDirectory(FolderSimpancppt)
                            End If

                            Dim simpancpptrajal As String = FolderSimpancppt & dsPendaftaran.KDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"
                            fn_LoadHistoryPasienCPPTRawatJalan(dsPendaftaran.KDPENDAFTARAN, dsPendaftaran.KDCUSTOMER, dsPendaftaran.M_CUSTOMER.NAME_DISPLAY, dsPendaftaran.M_CUSTOMER.TANGGALLAHIR, simpancpptrajal)

                            If FileIO.FileSystem.FileExists(simpancpptrajal) Then
                                dataList.Add(File.ReadAllBytes(simpancpptrajal))
                            End If
                        Else
                            Dim FolderSimpancpptRanap = "C:/SIMRS/CPPTRANAPCASEMIX/"

                            If Not Directory.Exists(FolderSimpancpptRanap) Then
                                Directory.CreateDirectory(FolderSimpancpptRanap)
                            Else
                                DeleteDirectory(FolderSimpancpptRanap)
                                Directory.CreateDirectory(FolderSimpancpptRanap)
                            End If

                            Dim simpancpptranap As String = FolderSimpancpptRanap & dsPendaftaran.KDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                            fn_LoadPdfViewerCPPTRAJALNAIKRANAP(dsPendaftaran.KDPENDAFTARAN_AWAL, dsPendaftaran.KDPENDAFTARAN, simpancpptranap)

                            If FileIO.FileSystem.FileExists(simpancpptranap) Then
                                dataList.Add(File.ReadAllBytes(simpancpptranap))
                            End If
                        End If
                    End If
                Else
                    If dsPendaftaran.CATEGORY = 0 Then
                        '--------------------4 CETAK RINCIAN

                        Dim cetakrincian As String = fn_Rincian(True, 0, dsPendaftaran.KDPENDAFTARAN, FolderSimpan & "4" & dsPendaftaran.KDPENDAFTARAN & Now.ToString("yyyyMMdd HHmmss") & ".pdf", NotUpload)

                        If cetakrincian <> "" Then
                            If FileIO.FileSystem.FileExists(cetakrincian) Then
                                dataList.Add(File.ReadAllBytes(cetakrincian))
                            End If
                        End If

                        'Cetak Resep
                        For Each yloop In oSalesOrderTransaksi.GetDataByKDPendaftaranFarmasiList(dsPendaftaran.KDPENDAFTARAN)
                            Dim oCPPT As New Grouper.clsR_CPPT

                            Dim ds = oSalesOrderTransaksi.GetData(yloop.KDSOTRANSAKSI)
                            If ds IsNot Nothing Then
                                Dim dsResep = oSalesOrderTransaksi.GetStructureHeaderResep
                                Dim DiganosaPasien As String = String.Empty
                                Dim BeratBadan As String = "-"
                                Dim Alergi As String = String.Empty

                                Dim dsCPPTByKunjungan = oCPPT.GetDataByKdKunjungan(ds.KDKUNJUNGAN)
                                If dsCPPTByKunjungan IsNot Nothing Then
                                    Dim dsCPPT = oCPPT.GetDataCPPTByIdentitas(dsCPPTByKunjungan.KDIDENTITAS)
                                    If dsCPPT IsNot Nothing Then
                                        BeratBadan = dsCPPT.OBJEKTIF_BERATBADAN & " Kg"
                                        DiganosaPasien = dsCPPT.ASSEMENT_TEXT

                                        If dsCPPT.SUBJEKTIF_ALERGI_TIDAK = True Then
                                            Alergi = "Alergi Obat: Tidak"
                                        End If
                                        If dsCPPT.SUBJEKTIF_ALERGI_YA = True Then
                                            Alergi = "Alergi Obat: Ya " & dsCPPT.SUBJEKTIF_ALERGI_YA_TEXT
                                        End If
                                    Else
                                        Alergi = "Alergi Obat: -"
                                        DiganosaPasien = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                                    End If
                                Else
                                    Alergi = "Alergi Obat: -"
                                End If

                                With dsResep
                                    .NORESEP = ds.KDSOTRANSAKSI
                                    .TANGGAL = ds.DATE
                                    .KLINIK = ds.S_PENDAFTARAN_KUNJUNGAN.M_DEPARTMENT.NAME_DISPLAY
                                    .DOKTER = ds.M_DOCTOR.NAME_DISPLAY
                                    .NOMORSIP = ds.M_DOCTOR.SIP
                                    .NORM = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER
                                    .NAMAPASIEN = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                                    .TANGGALLAHIR = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                                    .BERATBADAN = BeratBadan
                                    .DIAGNOSA = DiganosaPasien
                                    .ALAMAT = ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                                    .ALERGI = Alergi

                                    Dim list As New List(Of String)

                                    For Each xloop In oSalesOrderTransaksi.GetDataDetail(ds.KDSOTRANSAKSI)
                                        'list.Add("R/ " & xloop.M_ITEM.NMITEM2 & " No " & IntegerToRoman(xloop.QTY) & vbCrLf & "   ʃ " & IIf(xloop.M_SIGNA.CETAKDIETIKET = "", xloop.M_SIGNA.MEMO, xloop.M_SIGNA.CETAKDIETIKET) & " " & xloop.M_CARAPAKAI.MEMO & (" " & IIf(xloop.REMARKS = "-", "", xloop.REMARKS)).ToString.Trim)
                                        list.Add("R/ " & xloop.M_ITEM.NMITEM2 & " No " & IntegerToRoman(xloop.QTY) & vbCrLf & "   ʃ " & IIf(xloop.M_SIGNA.CETAKDIETIKET = "", "", xloop.M_SIGNA.CETAKDIETIKET) & IIf(xloop.M_CARAPAKAI.MEMO = "-", "", " " & xloop.M_CARAPAKAI.MEMO) & IIf(xloop.M_SIGNA.MEMO = "-", "", " " & xloop.M_SIGNA.MEMO) & (" " & xloop.REMARKS.ToString.Trim).Trim)
                                    Next

                                    .RESEP = String.Join(vbCrLf & "   --------------------------------------------------------" & vbCrLf, list.ToArray) & vbCrLf & "   --------------------------------------------------------"

                                    Dim dsTelaahResep = oSalesOrderTransaksi.GetDataTelaahResep(ds.KDSOTRANSAKSI)
                                    If dsTelaahResep IsNot Nothing Then
                                        .TELAAH_01_01_01 = dsTelaahResep.TELAAH_01
                                        .TELAAH_01_01_02 = dsTelaahResep.TELAAH_02
                                        .TELAAH_01_01_03 = dsTelaahResep.TELAAH_03
                                        .TELAAH_01_01_04 = dsTelaahResep.TELAAH_04
                                        .TELAAH_01_01_05 = dsTelaahResep.TELAAH_05
                                        .TELAAH_01_01_06 = dsTelaahResep.TELAAH_06
                                        .TELAAH_01_01_07 = dsTelaahResep.TELAAH_07
                                        .TELAAH_01_01_08 = dsTelaahResep.TELAAH_08
                                        .TELAAH_01_01_09 = dsTelaahResep.TELAAH_09
                                        .TELAAH_01_01_10 = dsTelaahResep.TELAAH_10
                                        .TELAAH_01_01_11 = dsTelaahResep.TELAAH_11
                                        .TELAAH_01_01_12 = dsTelaahResep.TELAAH_12
                                        .TELAAH_01_01_13 = dsTelaahResep.TELAAH_13
                                        .TELAAH_01_01_14 = dsTelaahResep.TELAAH_14
                                        .TELAAH_01_01_15 = dsTelaahResep.TELAAH_15
                                        .TELAAH_01_01_16 = dsTelaahResep.TELAAH_16
                                        .TELAAH_01_01_17 = dsTelaahResep.TELAAH_17
                                        .TELAAH_01_01_18 = dsTelaahResep.TELAAH_18
                                        .TELAAH_01_01_19 = dsTelaahResep.TELAAH_19
                                        .TELAAH_01_01_20 = dsTelaahResep.TELAAH_20
                                        .TELAAH_01_01_21 = dsTelaahResep.TELAAH_21
                                        .TELAAH_01_01_22 = dsTelaahResep.TELAAH_22
                                        .TELAAH_01_01_23 = dsTelaahResep.TELAAH_23
                                        .TELAAH_01_01_24 = dsTelaahResep.TELAAH_24
                                        .PETUGASFARMASI = dsTelaahResep.KDUSER
                                    Else
                                        .TELAAH_01_01_01 = False
                                        .TELAAH_01_01_02 = False
                                        .TELAAH_01_01_03 = False
                                        .TELAAH_01_01_04 = False
                                        .TELAAH_01_01_05 = False
                                        .TELAAH_01_01_06 = False
                                        .TELAAH_01_01_07 = False
                                        .TELAAH_01_01_08 = False
                                        .TELAAH_01_01_09 = False
                                        .TELAAH_01_01_10 = False
                                        .TELAAH_01_01_11 = False
                                        .TELAAH_01_01_12 = False
                                        .TELAAH_01_01_13 = False
                                        .TELAAH_01_01_14 = False
                                        .TELAAH_01_01_15 = False
                                        .TELAAH_01_01_16 = False
                                        .TELAAH_01_01_17 = False
                                        .TELAAH_01_01_18 = False
                                        .TELAAH_01_01_19 = False
                                        .TELAAH_01_01_20 = False
                                        .TELAAH_01_01_21 = False
                                        .TELAAH_01_01_22 = False
                                        .TELAAH_01_01_23 = False
                                        .TELAAH_01_01_24 = False
                                        .PETUGASFARMASI = ds.KDUSER
                                    End If

                                    Dim dsTelaahObat1 = oSalesOrderTransaksi.GetDataTelaahObat1(ds.KDSOTRANSAKSI)
                                    If dsTelaahObat1 IsNot Nothing Then
                                        .TELAAH_02_01_01 = dsTelaahObat1.TELAAH_01
                                        .TELAAH_02_01_02 = dsTelaahObat1.TELAAH_02
                                        .TELAAH_02_01_03 = dsTelaahObat1.TELAAH_03
                                        .TELAAH_02_01_04 = dsTelaahObat1.TELAAH_04
                                        .TELAAH_02_01_05 = dsTelaahObat1.TELAAH_05
                                    Else
                                        .TELAAH_02_01_01 = False
                                        .TELAAH_02_01_02 = False
                                        .TELAAH_02_01_03 = False
                                        .TELAAH_02_01_04 = False
                                        .TELAAH_02_01_05 = False
                                    End If
                                    Dim dsTelaahObat2 = oSalesOrderTransaksi.GetDataTelaahObat2(ds.KDSOTRANSAKSI)
                                    If dsTelaahObat2 IsNot Nothing Then
                                        .TELAAH_03_01_01 = dsTelaahObat2.TELAAH_01
                                        .TELAAH_03_01_02 = dsTelaahObat2.TELAAH_02
                                        .TELAAH_03_01_03 = dsTelaahObat2.TELAAH_03
                                        .TELAAH_03_01_04 = dsTelaahObat2.TELAAH_04
                                        .TELAAH_03_01_05 = dsTelaahObat2.TELAAH_05
                                    Else
                                        .TELAAH_03_01_01 = False
                                        .TELAAH_03_01_02 = False
                                        .TELAAH_03_01_03 = False
                                        .TELAAH_03_01_04 = False
                                        .TELAAH_03_01_05 = False
                                    End If

                                    .PERUBAHANRESEP_01 = ""
                                    .PERUBAHANRESEP_02 = ""

                                    .KODETTD = ds.M_DOCTOR.KODETTD

                                    Dim alamatresep As String = FolderSimpan & "ZRESEP" & yloop.KDSOTRANSAKSI & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                                    Dim rpt As New xtraResep
                                    rpt.ShowPrintMarginsWarning = False
                                    rpt.Watermark.Text = sWATERMARK
                                    rpt.bindingSource.DataSource = dsResep

                                    rpt.ExportToPdf(alamatresep)

                                    If FileIO.FileSystem.FileExists(alamatresep) Then
                                        dataList.Add(File.ReadAllBytes(alamatresep))
                                    End If
                                End With
                            End If
                        Next
                    End If
                End If

                'Laboratorium

                Dim oSalesOrder As New Sales.clsSalesOrderTransaksi
                Dim oRME As New RME.clsRME

                For Each zloop In listKDKUNJUNGAN_1

                    For Each yloop In oSalesOrderTransaksi.GetDataByKDkunjunganLabList(zloop)
                        Dim ds = oSalesOrder.GetData(yloop.KDSOTRANSAKSI)
                        If ds IsNot Nothing Then
                            If sHargaApotik = False Then
                                Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                                Dim rpt As New xtraHasilLabSementara
                                Try
                                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP

                                Catch ex As Exception
                                    sSIPNIP = "KOSONG"

                                End Try
                                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                                rpt.ShowPrintMarginsWarning = False
                                rpt.Watermark.Text = sWATERMARK
                                rpt.bindingSource.DataSource = ds
                                rpt.ExportToPdf(alamatlab)

                                If sSIPNIP <> "KOSONG" Then
                                    If FileIO.FileSystem.FileExists(alamatlab) Then
                                        dataList.Add(File.ReadAllBytes(alamatlab))
                                    End If
                                End If

                            Else
                                Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                                Dim rpt As New xtraHasilLabSementaraVersi2
                                Try
                                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP

                                Catch ex As Exception
                                    sSIPNIP = "KOSONG"

                                End Try
                                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                                rpt.ShowPrintMarginsWarning = False
                                rpt.Watermark.Text = sWATERMARK
                                rpt.bindingSource.DataSource = ds
                                rpt.ExportToPdf(alamatlab)

                                If sSIPNIP <> "KOSONG" Then
                                    If FileIO.FileSystem.FileExists(alamatlab) Then
                                        dataList.Add(File.ReadAllBytes(alamatlab))
                                    End If
                                End If

                            End If
                        End If

                        'Dim dsHasil = From x In oHasilLab.GetDataDetailTransaksi(yloop.KDSOTRANSAKSI)
                        '              Group x By x.KDSOTRANSAKSI Into seq = Sum(x.SEQ)

                        'For Each xloop In dsHasil
                        '    'Dim ds = oADokumen.GetData(xloop.KDSOTRANSAKSI & xloop.KDITEM)
                        '    'If ds IsNot Nothing Then
                        '    '    Dim dsHasilItem = oHasilLab.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)
                        '    '    If dsHasilItem IsNot Nothing Then
                        '    '        Dim alamatlab As String = FolderSimpan & "ZLAB" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        '    '        sUSIA = oRME.GetUmurPasien(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                        '    '        Dim rpt As New xtraHasilLabAkhir

                        '    '        rpt.ShowPrintMarginsWarning = False
                        '    '        rpt.Watermark.Text = sWATERMARK
                        '    '        rpt.bindingSource.DataSource = dsHasilItem

                        '    '        rpt.ExportToPdf(alamatlab)

                        '    '        If FileIO.FileSystem.FileExists(alamatlab) Then
                        '    '            dataList.Add(File.ReadAllBytes(alamatlab))
                        '    '        End If
                        '    '    End If
                        '    'Else
                        '    '    Dim dsHasilItem = oHasilLab.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)
                        '    '    If dsHasilItem IsNot Nothing Then
                        '    '        Dim alamatlab As String = FolderSimpan & "ZLAB" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        '    '        sUSIA = oRME.GetUmurPasien(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                        '    '        Dim rpt As New xtraHasilLabSementara

                        '    '        rpt.ShowPrintMarginsWarning = False
                        '    '        rpt.Watermark.Text = sWATERMARK
                        '    '        rpt.bindingSource.DataSource = dsHasilItem

                        '    '        rpt.ExportToPdf(alamatlab)

                        '    '        If FileIO.FileSystem.FileExists(alamatlab) Then
                        '    '            dataList.Add(File.ReadAllBytes(alamatlab))
                        '    '        End If
                        '    '    End If
                        '    'End If
                        'Next
                    Next
                Next

                For Each zloop In listKDKUNJUNGAN_2
                    For Each yloop In oSalesOrderTransaksi.GetDataByKDkunjunganLabList(zloop)
                        Dim ds = oSalesOrder.GetData(yloop.KDSOTRANSAKSI)
                        If ds IsNot Nothing Then
                            If sHargaApotik = False Then
                                Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"
                                Dim rpt As New xtraHasilLabSementara

                                ' sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                                Try
                                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP

                                Catch ex As Exception
                                    sSIPNIP = "KOSONG"

                                End Try

                                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                                rpt.ShowPrintMarginsWarning = False
                                rpt.Watermark.Text = sWATERMARK
                                rpt.bindingSource.DataSource = ds
                                rpt.ExportToPdf(alamatlab)

                                If sSIPNIP <> "KOSONG" Then
                                    If FileIO.FileSystem.FileExists(alamatlab) Then
                                        dataList.Add(File.ReadAllBytes(alamatlab))
                                    End If
                                End If

                            Else
                                Dim alamatlab As String = FolderSimpan & "ZLAB" & ds.KDSOTRANSAKSI & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                                Dim rpt As New xtraHasilLabSementaraVersi2
                                Try
                                    sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP

                                Catch ex As Exception
                                    sSIPNIP = "KOSONG"

                                End Try
                                'sSIPNIP = ds.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault.M_DOCTOR.SIP
                                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                                rpt.ShowPrintMarginsWarning = False
                                rpt.Watermark.Text = sWATERMARK
                                rpt.bindingSource.DataSource = ds
                                rpt.ExportToPdf(alamatlab)

                                If sSIPNIP <> "KOSONG" Then
                                    If FileIO.FileSystem.FileExists(alamatlab) Then
                                        dataList.Add(File.ReadAllBytes(alamatlab))
                                    End If
                                End If

                            End If
                        End If

                        'Dim dsHasil = From x In oHasilLab.GetDataDetailTransaksi(yloop.KDSOTRANSAKSI)
                        '              Group x By x.KDSOTRANSAKSI Into seq = Sum(x.SEQ)

                        'For Each xloop In dsHasil


                        '    'Dim ds = oADokumen.GetData(xloop.KDSOTRANSAKSI & xloop.KDITEM)
                        '    'If ds IsNot Nothing Then
                        '    '    Dim dsHasilItem = oHasilLab.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)
                        '    '    If dsHasilItem IsNot Nothing Then
                        '    '        Dim alamatlab As String = FolderSimpan & "ZLAB" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        '    '        sUSIA = oRME.GetUmurPasien(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                        '    '        Dim rpt As New xtraHasilLabAkhir

                        '    '        rpt.ShowPrintMarginsWarning = False
                        '    '        rpt.Watermark.Text = sWATERMARK
                        '    '        rpt.bindingSource.DataSource = dsHasilItem

                        '    '        rpt.ExportToPdf(alamatlab)

                        '    '        If FileIO.FileSystem.FileExists(alamatlab) Then
                        '    '            dataList.Add(File.ReadAllBytes(alamatlab))
                        '    '        End If
                        '    '    End If
                        '    'Else
                        '    '    Dim dsHasilItem = oHasilLab.GetDataDetailTransaksikditem(xloop.KDSOTRANSAKSI, xloop.KDITEM)
                        '    '    If dsHasilItem IsNot Nothing Then
                        '    '        Dim alamatlab As String = FolderSimpan & "ZLAB" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        '    '        sUSIA = oRME.GetUmurPasien(dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.DATE, dsHasilItem.FirstOrDefault.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                        '    '        Dim rpt As New xtraHasilLabSementara

                        '    '        rpt.ShowPrintMarginsWarning = False
                        '    '        rpt.Watermark.Text = sWATERMARK
                        '    '        rpt.bindingSource.DataSource = dsHasilItem

                        '    '        rpt.ExportToPdf(alamatlab)

                        '    '        If FileIO.FileSystem.FileExists(alamatlab) Then
                        '    '            dataList.Add(File.ReadAllBytes(alamatlab))
                        '    '        End If
                        '    '    End If
                        '    'End If
                        'Next
                    Next
                Next

                'Radiologi
                For Each zloop In listKDKUNJUNGAN_1
                    For Each xloop In oSalesOrderTransaksi.GetDataByKDkunjunganDetailRadiologiList(zloop)
                        Dim dsRadiologi = oExpertise.GetData(xloop.KDSOTRANSAKSI, xloop.SEQ)

                        If dsRadiologi IsNot Nothing Then
                            Dim alamatRad As String = FolderSimpan & "ZRAD" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                            sUSIA = oRME.GetUmurPasien(dsRadiologi.DATE, dsRadiologi.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                            Dim rpt As New xtraExpertise

                            rpt.ShowPrintMarginsWarning = False
                            rpt.Watermark.Text = sWATERMARK

                            rpt.bindingSource.DataSource = dsRadiologi
                            rpt.ExportToPdf(alamatRad)

                            If FileIO.FileSystem.FileExists(alamatRad) Then
                                dataList.Add(File.ReadAllBytes(alamatRad))
                            End If
                        End If
                    Next
                Next

                For Each zloop In listKDKUNJUNGAN_2
                    For Each xloop In oSalesOrderTransaksi.GetDataByKDkunjunganDetailRadiologiList(zloop)
                        Dim dsRadiologi = oExpertise.GetData(xloop.KDSOTRANSAKSI, xloop.SEQ)

                        If dsRadiologi IsNot Nothing Then
                            Dim alamatRad As String = FolderSimpan & "ZRAD" & xloop.KDSOTRANSAKSI & xloop.KDITEM & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                            sUSIA = oRME.GetUmurPasien(dsRadiologi.DATE, dsRadiologi.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                            Dim rpt As New xtraExpertise

                            rpt.ShowPrintMarginsWarning = False
                            rpt.Watermark.Text = sWATERMARK

                            rpt.bindingSource.DataSource = dsRadiologi
                            rpt.ExportToPdf(alamatRad)

                            If FileIO.FileSystem.FileExists(alamatRad) Then
                                dataList.Add(File.ReadAllBytes(alamatRad))
                            End If
                        End If
                    Next
                Next

                For Each zloop In listKDKUNJUNGAN_1
                    For Each dokumen In oADokumen.GetDataByKunjungan(zloop)
                        Dim alamatDokumen As String = FolderSimpan & "Dokumen" & dokumen.KDDKUMEN & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        xtraReportDokumen(dokumen.KDDKUMEN, alamatDokumen)

                        If FileIO.FileSystem.FileExists(alamatDokumen) Then
                            dataList.Add(File.ReadAllBytes(alamatDokumen))
                        End If
                    Next
                Next

                For Each zloop In listKDKUNJUNGAN_2
                    For Each dokumen In oADokumen.GetDataByKunjungan(zloop)
                        Dim alamatDokumen As String = FolderSimpan & "Dokumen" & dokumen.KDDKUMEN & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        xtraReportDokumen(dokumen.KDDKUMEN, alamatDokumen)

                        If FileIO.FileSystem.FileExists(alamatDokumen) Then
                            dataList.Add(File.ReadAllBytes(alamatDokumen))
                        End If
                    Next
                Next


                If dataList.Count > 0 Then
                    dsLoad = MergeFilesByte(dataList)
                    Dim stream As New MemoryStream(dsLoad)
                    If NotUpload = True Then
                        PdfViewer1.LoadDocument(stream)
                    Else
                        File.WriteAllBytes(FolderSimpanPDF & "\" & If(dsPendaftaran.M_DAFTAR_L1.MEMO = "BPJS", dsPendaftaran.NOMORSEP, dsPendaftaran.KDPENDAFTARAN) & ".pdf", dsLoad)
                    End If
                End If
            Else
                'DgCare
                Dim dsData = oGrouperRawatJalan.GetDataByNoRec(norec)
                If dsData IsNot Nothing Then
                    If dsData.noSep <> "" Then
                        Try
                            '--------------------1 CETAK LIP KLAIM

                            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Cetakklaim(sEklaim_Url, sEklaim_Generate, dsData.noSep))
                            Dim sDataDuplicate As String = String.Empty
                            Dim smessage As String = String.Empty

                            sDataDuplicate = jsonDecode("metadata")("code").ToString
                            smessage = jsonDecode("metadata")("message").ToString

                            If sDataDuplicate = "200" Then
                                Dim Base64Byte() As Byte
                                Base64Byte = Convert.FromBase64String(jsonDecode("data").ToString)
                                dataList.Add(Base64Byte)
                            Else
                                If NotUpload = True Then
                                    SplashScreenManager.CloseForm(False)
                                    MsgBox("Cetak Klaim : " & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            End If
                        Catch oErr As Exception
                            If NotUpload = True Then
                                MsgBox("Merger PDF" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End Try

                        '--------------------2 CETAK SEP
                        Dim alamatsep As String = FolderSimpan & "1" & dsData.noSep & Now.ToString("yyyyMMdd HHmmss") & ".pdf"

                        Dim rpt As New xtraSEPNomorAntrianCasmix
                        rpt.bindingSource.DataSource = dsData
                        rpt.ExportToPdf(alamatsep)

                        If alamatsep <> "" Then
                            If FileIO.FileSystem.FileExists(alamatsep) Then
                                dataList.Add(File.ReadAllBytes(alamatsep))
                            End If
                        End If
                    End If

                    If NotUpload = False Then
                        Dim ds = oGrouperData.GetData(dsData.kodegrouper)
                        Dim alamacetakrincian As String = FolderSimpan & "4" & dsData.kodegrouper & Now.ToString("yyyyMMdd HHmmss") & ".pdf"
                        If ds IsNot Nothing Then
                            Dim rpt As New xtraCashInCasemix
                            sPrintGrandTotal = ds.TARIFRUMAHSAKIT
                            rpt.ShowPrintMarginsWarning = False
                            rpt.Watermark.Text = sWATERMARK
                            rpt.bindingSource.DataSource = ds
                            rpt.ExportToPdf(alamacetakrincian)

                            If FileIO.FileSystem.FileExists(alamacetakrincian) Then
                                dataList.Add(File.ReadAllBytes(alamacetakrincian))
                            End If
                        End If
                    Else
                        Dim cetakrincian As String = fn_Rincian(False, dsData.kodegrouper, "", FolderSimpan & "4" & dsData.kodegrouper & Now.ToString("yyyyMMdd HHmmss") & ".pdf", NotUpload)
                        If cetakrincian <> "" Then
                            If FileIO.FileSystem.FileExists(cetakrincian) Then
                                dataList.Add(File.ReadAllBytes(cetakrincian))
                            End If
                        End If
                    End If

                    If dataList.Count > 0 Then
                        dsLoad = MergeFilesByte(dataList)
                        Dim stream As New MemoryStream(dsLoad)
                        If NotUpload = True Then
                            PdfViewer1.LoadDocument(stream)
                        Else
                            File.WriteAllBytes(FolderSimpanPDF & "\" & If(dsData.noSep <> "", dsData.noSep, dsData.norec) & ".pdf", dsLoad)
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            If NotUpload = True Then
                MsgBox("Merger PDF" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Sub
    Private Sub ConvertImageToPDF(ByVal alamat As String, ByVal outputPDF As String)
        If Not File.Exists(alamat) Then
            MessageBox.Show("File gambar tidak ditemukan: " & alamat)
            Exit Sub
        End If

        ' Buat dokumen PDF baru
        Dim doc As New Document(PageSize.A4)
        PdfWriter.GetInstance(doc, New FileStream(outputPDF, FileMode.Create))

        doc.Open()

        ' Tambahkan gambar dari file
        Dim img As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(alamat)

        ' Atur ukuran agar pas halaman (opsional)
        img.Alignment = Element.ALIGN_CENTER
        img.ScaleToFit(PageSize.A4.Width - 40, PageSize.A4.Height - 40)

        ' Tambahkan gambar ke halaman
        doc.Add(img)

        doc.Close()
        'MessageBox.Show("Berhasil membuat PDF di: " & outputPDF)
    End Sub
    Private Sub fn_LoadHistoryPasienCPPT(ByVal sKDCUSTOMER As String, ByVal namapasien As String, ByVal tanggallahir As DateTime)
        Try
            PdfViewerCPPTRawatJalan.CloseDocument()

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

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
            'SQL &= "AND A.CATEGORY = " & Category & " "
            If chkCPPTDokter.Checked = False Then
                SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0

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
                    dsRekap.CATATAN = .Rows(iLoop)("CATATAN")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")
                    dsRekap.USERDELETE = .Rows(iLoop)("USERDELETE")

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If chkCek.Checked = False Then
                Try

                    Dim oConnMySql As New MySql.Data.MySqlClient.MySqlConnection
                    Dim oCommMySql As New MySql.Data.MySqlClient.MySqlCommand
                    Dim daMySql As MySql.Data.MySqlClient.MySqlDataAdapter
                    Dim dsMySql As New DataSet
                    Dim MYSQL As String
                    dsMySql = New DataSet

                    oConnMySql = New MySqlConnection(sMySQL_Url)

                    If oConnMySql.State = ConnectionState.Closed Then
                        oConnMySql.Open()
                    End If

                    MYSQL = "SELECT "
                    MYSQL &= "a.KUNJUNGAN "
                    MYSQL &= ",a.TANGGAL as tanggal "
                    MYSQL &= ",a.SUBYEKTIF "
                    MYSQL &= ",a.OBYEKTIF "
                    MYSQL &= ",a.ASSESMENT "
                    MYSQL &= ",a.PLANNING "
                    MYSQL &= ",a.INSTRUKSI "
                    MYSQL &= ",d.NAMA "
                    MYSQL &= "From medicalrecord.cppt as a "
                    MYSQL &= "INNER Join pendaftaran.kunjungan as b "
                    MYSQL &= "On a.KUNJUNGAN = b.NOMOR "
                    MYSQL &= "INNER Join pendaftaran.pendaftaran as c "
                    MYSQL &= "On b.NOPEN = c.NOMOR "
                    MYSQL &= "INNER JOIN aplikasi.pengguna as d "
                    MYSQL &= "On a.OLEH = d.ID "
                    MYSQL &= "WHERE c.NORM = '" & CInt(sKDCUSTOMER) & "' "

                    oCommMySql.Connection = oConnMySql
                    oCommMySql.CommandText = MYSQL
                    oCommMySql.CommandTimeout = 120
                    oCommMySql.CommandType = CommandType.Text

                    daMySql = New MySql.Data.MySqlClient.MySqlDataAdapter(oCommMySql)
                    daMySql.Fill(dsMySql, "medicalrecordcppt")

                    If oConnMySql.State = ConnectionState.Open Then
                        oConnMySql.Close()
                    End If

                    For iLoop As Integer = 0 To dsMySql.Tables("medicalrecordcppt").Rows.Count - 1
                        Dim dsRekap As New DataAccess.R_CPPT
                        With dsMySql.Tables("medicalrecordcppt")
                            dsRekap.DATECREATED = CDate(.Rows(iLoop)("tanggal"))
                            dsRekap.DATEUPDATED = CDate(.Rows(iLoop)("tanggal"))
                            dsRekap.DATE = CDate(.Rows(iLoop)("tanggal"))
                            dsRekap.KDIDENTITAS = Identitas
                            dsRekap.KDCPPT = .Rows(iLoop)("KUNJUNGAN")
                            dsRekap.KDPROFESI = "DOKTER"
                            dsRekap.SUBJEKTIF_KELUHANUTAMA = 0
                            dsRekap.SUBJEKTIF_ALERGI_TIDAK = 0
                            dsRekap.SUBJEKTIF_ALERGI_YA = 0
                            dsRekap.SUBJEKTIF_ALERGI_YA_TEXT = ""
                            dsRekap.SUBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("SUBYEKTIF"))
                            dsRekap.OBJEKTIF_KESADARAN = 0
                            dsRekap.OBJEKTIF_GCS = 0
                            dsRekap.OBJEKTIF_TAMPAKSAKIT = 0
                            dsRekap.OBJEKTIF_VISUALANALOGSCORE = 0
                            dsRekap.OBJEKTIF_BERATBADAN = 0
                            dsRekap.OBJEKTIF_TINGGIBADAN = 0
                            dsRekap.OBJEKTIF_SPO2 = 0
                            dsRekap.OBJEKTIF_SISTOLE = 0
                            dsRekap.OBJEKTIF_DIASTOLE = 0
                            dsRekap.OBJEKTIF_HR = 0
                            dsRekap.OBJEKTIF_RR = 0
                            dsRekap.OBJEKTIF_SUHU = 0
                            dsRekap.OBJEKTIF_PEMERIKSAAN = 0
                            dsRekap.OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = 0
                            dsRekap.OBJEKTIF_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("OBYEKTIF"))
                            dsRekap.ASSEMENT_INDIKASI = 0
                            dsRekap.ASSEMENT_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("ASSESMENT"))
                            dsRekap.PLANNING_ISTINDAKLANJUT_PULANG = 0
                            dsRekap.PLANNING_ISTINDAKLANJUT_RAWAT = 0
                            dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL = 0
                            dsRekap.PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = 0
                            dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK = 0
                            dsRekap.PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = 0
                            dsRekap.PLANNING_ALASAN = 0
                            dsRekap.PLANNING_TEXT = CleanHtmlToPlainText(.Rows(iLoop)("PLANNING")) & vbCrLf & CleanHtmlToPlainText(.Rows(iLoop)("INSTRUKSI"))
                            dsRekap.CATATAN = 0
                            dsRekap.KDUSER = .Rows(iLoop)("NAMA")
                            dsRekap.ISDELETE = 0
                            dsRekap.DATEDELETE = CDate(.Rows(iLoop)("TANGGAL"))
                            dsRekap.USERDELETE = 0

                            listCPPT.Add(dsRekap)
                        End With
                    Next
                Catch oErr As Exception
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Koneksi CPPT Aplikasi Lama" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            If listCPPT.Count > 0 Then
                sFind1_cppt = sKDCUSTOMER
                sFind2_cppt = namapasien
                sFind3_cppt = tanggallahir.ToString("dd-MM-yyyy")

                Dim FolderSimpan = "C:/SIMRS/CPPT/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim AlamatCPPT As String = FolderSimpan & sKDCUSTOMER & Now.ToString("yyyyMMddHHmmss") & ".pdf"

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    PdfViewerCPPTRawatJalan.LoadDocument(AlamatCPPT)
                End If

            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadHistoryPasienCPPTRawatJalan(ByVal sKDPENDAFTARAN As String, ByVal sKDCUSTOMER As String, ByVal namapasien As String, ByVal tanggallahir As DateTime, ByVal AlamatCPPT As String) As String
        Try
            fn_LoadHistoryPasienCPPTRawatJalan = ""

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT.....")

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
            SQL &= "WHERE A.KDPENDAFTARAN = '" & sKDPENDAFTARAN & "' "
            SQL &= "AND B.ISDELETE = 0 "
            'SQL &= "AND A.CATEGORY = " & Category & " "
            If chkCPPTDokter.Checked = False Then
                SQL &= "AND ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE B.KDPROFESI = KDPROFESI), '') LIKE '%DOKTER%' "
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.R_CPPT)
            Dim Identitas As Integer = 0

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
                    dsRekap.CATATAN = .Rows(iLoop)("CATATAN")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISDELETE = .Rows(iLoop)("ISDELETE")
                    dsRekap.DATEDELETE = .Rows(iLoop)("DATEDELETE")
                    dsRekap.USERDELETE = .Rows(iLoop)("USERDELETE")

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
                sFind1_cppt = sKDCUSTOMER
                sFind2_cppt = namapasien
                sFind3_cppt = tanggallahir.ToString("dd-MM-yyyy")

                Dim rpt As New xtraDigital_CPPT_01_QR

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATE)
                rpt.ExportToPdf(AlamatCPPT)

                If FileIO.FileSystem.FileExists(AlamatCPPT) Then
                    fn_LoadHistoryPasienCPPTRawatJalan = AlamatCPPT
                End If

            End If

            sFind1_cppt = String.Empty
            sFind2_cppt = String.Empty
            sFind3_cppt = String.Empty

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            fn_LoadHistoryPasienCPPTRawatJalan = ""
            SplashScreenManager.CloseForm(False)
            'MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_LoadPdfViewerCPPTRAJALNAIKRANAP(ByVal RegisterRawatJalan As String, ByVal RegisterRawatInap As String, ByVal Alamat As String) As String
        Try
            fn_LoadPdfViewerCPPTRAJALNAIKRANAP = ""

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data CPPT Naik Ranap.....")

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

            SQL = "SELECT "
            SQL &= "* "
            SQL &= ",PROFESI = ISNULL((SELECT MEMO FROM R_CPPT_M_PROFESI WHERE A.KDPROFESI = KDPROFESI), '') "
            SQL &= "FROM "
            SQL &= "R_CPPT A "
            SQL &= "INNER JOIN A_IDENTITASPASIEN_LIST B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "WHERE B.KDPENDAFTARAN = '" & IIf(RegisterRawatJalan = "", "KOSONGTRANSAKSI", RegisterRawatJalan) & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_CPPT")

            Dim listCPPT As New List(Of DataAccess.S_REQ_CPPT)

            For iLoop As Integer = 0 To ds.Tables("HISTORY_CPPT").Rows.Count - 1
                Dim dsRekap As New DataAccess.S_REQ_CPPT
                With ds.Tables("HISTORY_CPPT")
                    dsRekap.DATECREATED = .Rows(iLoop)("DATECREATED")
                    dsRekap.DATEUPDATED = .Rows(iLoop)("DATEUPDATED")
                    dsRekap.DATE = .Rows(iLoop)("DATE")
                    dsRekap.KDCPPT = .Rows(iLoop)("KDCPPT")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.PROFESI = .Rows(iLoop)("PROFESI")
                    dsRekap.NAMAPASIEN = .Rows(iLoop)("NAMAPASIEN")
                    dsRekap.JK = .Rows(iLoop)("JENISKELAMIN")
                    dsRekap.NIK = .Rows(iLoop)("NIK")
                    dsRekap.TEMPATLAHIR = ""
                    dsRekap.TANGGALLAHIR = .Rows(iLoop)("TANGGALLAHIR")
                    dsRekap.AGAMA = ""
                    dsRekap.PENJAMIN = .Rows(iLoop)("KDDAFTAR_L1_NAMA")
                    dsRekap.NOTELEPON = ""
                    dsRekap.SUKU = ""
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.SUBJEKTIF = .Rows(iLoop)("SUBJEKTIF_TEXT")
                    dsRekap.OBJEKTIF = .Rows(iLoop)("OBJEKTIF_TEXT")
                    dsRekap.ASSEMENT = .Rows(iLoop)("ASSEMENT_TEXT")
                    dsRekap.PLANNING = .Rows(iLoop)("PLANNING_TEXT")
                    dsRekap.KDUSER = .Rows(iLoop)("KDUSER")
                    dsRekap.ISCHEKED = True

                    listCPPT.Add(dsRekap)
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            'If listCPPT.Count > 0 Then
            '    Dim FolderSimpanrj = "C:/SIMRS/CPPTNAIKRANAP/"

            '    If Not Directory.Exists(FolderSimpanrj) Then
            '        Directory.CreateDirectory(FolderSimpanrj)
            '    Else
            '        DeleteDirectory(FolderSimpanrj)
            '        Directory.CreateDirectory(FolderSimpanrj)
            '    End If

            '    Dim AlamatCPPT As String = FolderSimpanrj & sKDREGRAWATJALAN & Now.ToString("yyyyMMddHHmmss") & ".pdf"

            '    Dim rpt As New xtraDigital_CPPT_01_RawatInap

            '    rpt.ShowPrintMarginsWarning = False
            '    rpt.Watermark.Text = sWATERMARK

            '    rpt.bindingSource.DataSource = listCPPT.OrderByDescending(Function(x) x.DATECREATED)
            '    rpt.ExportToPdf(AlamatCPPT)

            '    If FileIO.FileSystem.FileExists(AlamatCPPT) Then
            '        dataList.Add(File.ReadAllBytes(AlamatCPPT))
            '    End If
            'End If

            '
            Dim oCPPTRanap As New Transaksi.clsCPPT

            Dim dsRawat = oCPPTRanap.GetDataByRMkdpendaftaran(RegisterRawatInap)

            Dim hasilUnion = dsRawat.Union(listCPPT).OrderByDescending(Function(x) x.DATECREATED)

            If hasilUnion.Count > 0 Then
                Dim FolderSimpan = "C:/CPPTRANAP/"

                If Not Directory.Exists(FolderSimpan) Then
                    Directory.CreateDirectory(FolderSimpan)
                Else
                    DeleteDirectory(FolderSimpan)
                    Directory.CreateDirectory(FolderSimpan)
                End If

                Dim rpt As New xtraDigital_CPPT_01_RawatInap

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = hasilUnion
                rpt.ExportToPdf(Alamat)

                'If FileIO.FileSystem.FileExists(Alamat) Then
                '    fn_LoadPdfViewerCPPTRAJALNAIKRANAP = Alamat
                'End If
            End If


            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            fn_LoadPdfViewerCPPTRAJALNAIKRANAP = ""
            SplashScreenManager.CloseForm(False)
            'MsgBox("Load Report CPPT" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function xtraReportDokumen(ByVal Kode As String, ByVal Alamat As String) As String
        Try
            xtraReportDokumen = ""

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            SplashScreenManager.Default.SetWaitFormCaption("Processing data Asesmen Awal Nakes.....")

            Dim dataList As New List(Of Byte())

            Dim ds = oADokumen.GetData(Kode)

            If ds IsNot Nothing Then
                Dim rpt As New xtraDokumenFormulir

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(Alamat)

                'If FileIO.FileSystem.FileExists(Alamat) Then
                '    xtraReportDokumen = Alamat
                'End If

            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            xtraReportDokumen = ""
            SplashScreenManager.CloseForm(False)
            'MsgBox("Cetak Asesmen Awal Nakes" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Public Function CleanHtmlToPlainText(html As String) As String
        Try
            If String.IsNullOrWhiteSpace(html) Then Return String.Empty

            Dim s As String = html

            ' 1) Remove script/style blocks
            s = Regex.Replace(s, "(?is)<(script|style)\b.*?>.*?</\1>", String.Empty)

            ' 2) Replace common block tags with newlines
            s = Regex.Replace(s, "(?i)</?(div|p|h[1-6]|section|article)[^>]*>", vbCrLf)

            ' 3) Replace <br> and <br/> with newline
            s = Regex.Replace(s, "(?i)<br\s*/?>", vbCrLf)

            ' 4) Replace <li> with bullet
            s = Regex.Replace(s, "(?i)<li[^>]*>", vbCrLf & "- ")
            s = Regex.Replace(s, "(?i)</li>", String.Empty)

            ' 5) Remove all remaining tags
            s = Regex.Replace(s, "<[^>]+>", String.Empty)

            ' 6) Decode HTML entities
            s = WebUtility.HtmlDecode(s)

            ' 7) Normalize whitespace: collapse multiple newlines and spaces
            s = Regex.Replace(s, "\r\n[\s\r\n]+", vbCrLf)           ' collapse blank lines
            s = Regex.Replace(s, "[ \t]{2,}", " ")                 ' collapse repeated spaces
            s = Regex.Replace(s, "(?:\r\n){3,}", vbCrLf & vbCrLf)   ' limit successive newlines

            ' Trim
            s = s.Trim()

            Return s
        Catch ex As Exception
            CleanHtmlToPlainText = html
        End Try
    End Function
    Private Function fn_CetakSEPLama(ByVal norec As String, ByVal SimpanAlamat As String, ByVal Pesan As Boolean) As String
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data SEP Lama.....")

            fn_CetakSEPLama = ""

            Dim ds = oGrouperRawatJalan.GetDataByNoRec(norec)

            If ds IsNot Nothing Then
                fn_CetakSEPLama = SimpanAlamat
                Dim rpt As New xtraSEPNomorAntrianCasmix
                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(fn_CetakSEPLama)
            End If

            SplashScreenManager.CloseForm(False)

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)

            fn_CetakSEPLama = ""
            If Pesan = True Then
                MsgBox("Cetak SEP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Public Sub fn_RincianKegridList(ByVal NoRegister1 As String)
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data.....")

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String = fn_LoadQuey(NoRegister1)

            If SQL = "" Then
                SplashScreenManager.CloseForm(False)
                Exit Sub
            End If

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "HISTORY_DETIL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim listDetil As New List(Of DataAccess.R_IDENTITAS_GROUPER_ITEM)
            Dim oItem As New Reference.clsItem

            For iLoop As Integer = 0 To ds.Tables("HISTORY_DETIL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_IDENTITAS_GROUPER_ITEM
                With ds.Tables("HISTORY_DETIL")
                    dsRekap.produkfk = .Rows(iLoop)("produkfk")

                    Dim dsItem = oItem.GetData(.Rows(iLoop)("produkfk"))
                    If dsItem IsNot Nothing Then
                        dsRekap.kategoribpjs = dsItem.M_ITEM_L3.MEMO
                    Else
                        dsRekap.kategoribpjs = ""
                    End If

                    'If .Rows(iLoop)("Obat") = False Then
                    '    Dim dsItem = oItem.GetData(.Rows(iLoop)("produkfk"))
                    '    If dsItem IsNot Nothing Then
                    '        dsRekap.kategoribpjs = dsItem.M_ITEM_L3.MEMO
                    '    Else
                    '        dsRekap.kategoribpjs = ""
                    '    End If
                    'Else
                    '    dsRekap.kategoribpjs = "OBAT-OBATAN DAN ALKES"
                    'End If

                    dsRekap.isobat = .Rows(iLoop)("Obat")
                    dsRekap.namaproduk = .Rows(iLoop)("namaproduk")
                    dsRekap.jumlah = .Rows(iLoop)("jumlah")
                    dsRekap.hargajual = .Rows(iLoop)("hargajual")

                    listDetil.Add(dsRekap)
                End With
            Next

            grdRincian.MainView = grvRincian
            grdRincian.DataSource = listDetil
            grdRincian.ForceInitialize()

            SplashScreenManager.CloseForm(False)

            grvRincian.Columns("produkfk").VisibleIndex = -1

            fn_LoadFormatDataRincian()

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Rincian(ByVal RincianNew As Boolean, ByVal kodegrouper As Integer, ByVal norec As String, ByVal SimpanAlamat As String, ByVal Pesan As Boolean) As String
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data Rincian.....")

            fn_Rincian = ""

            If RincianNew = False Then
                If kodegrouper <> 0 Then
                    Dim ds = oGrouperData.GetData(kodegrouper)
                    If ds IsNot Nothing Then
                        fn_Rincian = SimpanAlamat
                        Dim rpt As New xtraCashInCasemix
                        sPrintGrandTotal = ds.TARIFRUMAHSAKIT
                        rpt.ShowPrintMarginsWarning = False
                        rpt.Watermark.Text = sWATERMARK
                        rpt.bindingSource.DataSource = ds
                        rpt.ExportToPdf(fn_Rincian)
                    End If
                End If
            Else
                Try
                    Dim oPendaftaran As New Admission.clsPendaftaran
                    Dim dsPendaftaran = oPendaftaran.GetData(norec)
                    Dim TanggalPulang As DateTime = Now

                    If dsPendaftaran Is Nothing Then
                        Exit Function
                    Else
                        If dsPendaftaran.CATEGORY = 1 Then
                            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                            Dim dsPulang = oPulang.GetDatabyKD(dsPendaftaran.KDPENDAFTARAN)
                            If dsPulang IsNot Nothing Then
                                TanggalPulang = dsPulang.DATE
                            Else
                                TanggalPulang = dsPendaftaran.DATE
                                'MsgBox("Pasien Belum di Pulangkan", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            TanggalPulang = dsPendaftaran.DATE
                        End If
                    End If

                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String = fn_LoadQuey(dsPendaftaran.KDPENDAFTARAN)

                    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                    oConn = New SqlConnection(sConn)
                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "ALLL")

                    Dim listTranskasi As New List(Of R_BILLING)

                    For iLoop As Integer = 0 To ds.Tables("ALLL").Rows.Count - 1
                        Dim dsRekap As New DataAccess.R_BILLING

                        With ds.Tables("ALLL")
                            dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                            dsRekap.DPJP = .Rows(iLoop)("DPJP")
                            dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                            dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                            dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                            dsRekap.KELAS = .Rows(iLoop)("KELAS")
                            dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                            dsRekap.TANGGAL_PULANG = TanggalPulang
                            dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                            dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                            dsRekap.NAMAKUNJUNGAN = .Rows(iLoop)("NAMAKUNJUNGAN")
                            dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                            dsRekap.TANGGAL_INPUT = .Rows(iLoop)("TANGGAL_INPUT")
                            dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                            dsRekap.ITEM = .Rows(iLoop)("namaproduk")
                            dsRekap.QTY = .Rows(iLoop)("jumlah")
                            dsRekap.GRANDTOTAL = .Rows(iLoop)("hargajual")
                            dsRekap.TUJUAN = dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY
                            dsRekap.KELOMPOKPASIEN = dsPendaftaran.M_DAFTAR_L2.MEMO

                            dsRekap.KDUSER = .Rows(iLoop)("KDUSER")

                            listTranskasi.Add(dsRekap)

                            sPrintSubtotal = .Rows(iLoop)("SUBTOTAL")
                            sPrintPotongan = .Rows(iLoop)("ADMIN")
                            sPrintGrandTotal = .Rows(iLoop)("GRANDTOTALHEADER")
                        End With
                    Next

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If

                    Dim rpt As New xtraRincianCasmix

                    rpt.ShowPrintMarginsWarning = False
                    'rpt.Watermark.Text = "SIMRS"

                    fn_Rincian = SimpanAlamat

                    rpt.bindingSource.DataSource = listTranskasi
                    rpt.ExportToPdf(fn_Rincian)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            SplashScreenManager.CloseForm(False)
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            fn_Rincian = ""
            If Pesan = True Then
                MsgBox("Cetak Rincian" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function IntegerToRoman(IntNumberValue As Integer) As String
        Dim RomanNumbers As New Dictionary(Of String, Integer)()
        RomanNumbers.Add("M", 1000)
        RomanNumbers.Add("CM", 900)
        RomanNumbers.Add("D", 500)
        RomanNumbers.Add("CD", 400)
        RomanNumbers.Add("C", 100)
        RomanNumbers.Add("XC", 90)
        RomanNumbers.Add("L", 50)
        RomanNumbers.Add("XL", 40)
        RomanNumbers.Add("X", 10)
        RomanNumbers.Add("IX", 9)
        RomanNumbers.Add("V", 5)
        RomanNumbers.Add("IV", 4)
        RomanNumbers.Add("I", 1)

        Dim result As String = ""

        For Each pair As KeyValuePair(Of String, Integer) In RomanNumbers
            While IntNumberValue >= pair.Value
                IntNumberValue -= pair.Value
                result += pair.Key
            End While
        Next
        Return result
    End Function
    Private Function fn_Cetakklaim(ByVal NOSEP As String, ByVal Pesan As Boolean) As String
        Try
            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)
            SplashScreenManager.Default.SetWaitFormCaption("Processing data LIP .....")

            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_Cetakklaim(sEklaim_Url, sEklaim_Generate, NOSEP))
            Dim sDataDuplicate As String = String.Empty
            Dim smessage As String = String.Empty

            sDataDuplicate = jsonDecode("metadata")("code").ToString
            smessage = jsonDecode("metadata")("message").ToString

            If sDataDuplicate = "200" Then
                SplashScreenManager.CloseForm(False)
                fn_Cetakklaim = jsonDecode("data").ToString
            Else
                fn_Cetakklaim = ""
                If Pesan = True Then
                    SplashScreenManager.CloseForm(False)
                    MsgBox("Cetak Klaim : " & sDataDuplicate & "-" & smessage, MsgBoxStyle.Exclamation, Me.Text)
                End If
            End If
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            fn_Cetakklaim = ""

            If Pesan = True Then
                MsgBox("Cetak Klaim : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End If
        End Try
    End Function
    Private Function fn_CetakResumeRawatJalan(ByVal kdcppt As String, ByVal SimpanAlamat As String) As String
        Try
            fn_CetakResumeRawatJalan = ""

            Dim ds = oGrouperCPPT.GetData(kdcppt)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    fn_CetakResumeRawatJalan = SimpanAlamat
                    Dim rpt As New xtraResumeRawatJalan

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(SimpanAlamat)

                    rpt.ExportToPdf(fn_CetakResumeRawatJalan)
                End If
            End If
        Catch oErr As Exception
            fn_CetakResumeRawatJalan = ""
            'If Pesan = True Then
            '    MsgBox("Cetak SEP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function fn_CetakResumeRawatInap(ByVal norec As String, ByVal SimpanAlamat As String) As String
        Try
            fn_CetakResumeRawatInap = ""

            Dim ds = oRingkasanKeluar.GetData(norec)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    fn_CetakResumeRawatInap = SimpanAlamat
                    Dim rpt As New xtraRingkasanKeluarRawatInap

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(fn_CetakResumeRawatInap)
                End If
            End If
        Catch oErr As Exception
            fn_CetakResumeRawatInap = ""
            'If Pesan = True Then
            '    MsgBox("Cetak SEP" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportAsesmenAwalMedisIGD(ByVal kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportAsesmenAwalMedisIGD = ""

            Dim ds = oDigitalAsesmenMedisIGD.GetDataByKodeIGD(kode)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    xtraReportAsesmenAwalMedisIGD = SimpanAlamat

                    Dim rpt As New xtraReportFormulirIGD1

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(xtraReportAsesmenAwalMedisIGD)
                End If
            End If
        Catch oErr As Exception
            xtraReportAsesmenAwalMedisIGD = ""
            'If Pesan = True Then
            '    MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportAsesmenHemodialisa(ByVal kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportAsesmenHemodialisa = ""

            Dim ds = oDigitalAsesmenHemodialisa.GetData(kode)

            If ds IsNot Nothing Then
                xtraReportAsesmenHemodialisa = SimpanAlamat

                Dim rpt As New xtraReportEMedrekRJ_38

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.BindingSource1.DataSource = ds
                rpt.ExportToPdf(xtraReportAsesmenHemodialisa)
            End If
        Catch oErr As Exception
            xtraReportAsesmenHemodialisa = ""
            'If Pesan = True Then
            '    MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportDischaregPlanning(ByVal kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportDischaregPlanning = ""

            Dim oDigital As New EMedrek.clsDigital_DischargePlanning
            Dim ds = oDigital.GetData(kode)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    xtraReportDischaregPlanning = SimpanAlamat

                    Dim rpt As New xtraReportEMedrekRI_13

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource.DataSource = ds
                    rpt.ExportToPdf(xtraReportDischaregPlanning)
                End If
            End If
        Catch oErr As Exception
            xtraReportDischaregPlanning = ""
            'If Pesan = True Then
            '    MsgBox("Cetak Asesmen Medis" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportLaporanPersalinan(ByVal kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportLaporanPersalinan = ""

            Dim ds = oDigitalLaporanPersalinan.GetData(kode)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    xtraReportLaporanPersalinan = SimpanAlamat

                    Dim rpt As New xtraLaporanPersalian

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.bindingSource.DataSource = ds
                    rpt.ExportToPdf(xtraReportLaporanPersalinan)
                End If
            End If
        Catch oErr As Exception
            xtraReportLaporanPersalinan = ""
            'If Pesan = True Then
            '    MsgBox("Cetak Laporan Persalinan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportLaporanOperasi(ByVal kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportLaporanOperasi = ""

            Dim ds = oDigitalLaporanOperasi.GetDataKode(kode)

            If ds IsNot Nothing Then
                If ds.ISDELETE = False Then
                    xtraReportLaporanOperasi = SimpanAlamat

                    Dim rpt As New xtraReportLAPORANOPERASI

                    rpt.ShowPrintMarginsWarning = False
                    rpt.Watermark.Text = sWATERMARK

                    rpt.BindingSource1.DataSource = ds
                    rpt.ExportToPdf(xtraReportLaporanOperasi)
                End If
            End If
        Catch oErr As Exception
            xtraReportLaporanOperasi = ""
            'If Pesan = True Then
            '    MsgBox("Cetak Laporan Persalinan" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'End If
            Throw oErr
        End Try
    End Function
    Private Function xtraReportLembarUjiFungsi1(ByVal Kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportLembarUjiFungsi1 = ""

            Dim oGrouperDataCppt As New Grouper.clsR_CPPT

            Dim ds = oGrouperDataCppt.GetDataByKdKunjunganCPPTDokter(Kode)

            If ds IsNot Nothing Then
                xtraReportLembarUjiFungsi1 = SimpanAlamat

                Dim rpt As New xtraLembarUjiFungsiRehabResumeNew

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(xtraReportLembarUjiFungsi1)
            End If

        Catch oErr As Exception
            Throw oErr
        End Try
    End Function
    'Private Function xtraReportLembarUjiFungsi2(ByVal Kode As String, ByVal SimpanAlamat As String) As String
    '    Try
    '        xtraReportLembarUjiFungsi2 = ""

    '        Dim oDigital As New EMedrek.clsFisioterafi_1
    '        Dim ds = oDigital.GetData(Kode)

    '        If ds IsNot Nothing Then
    '            xtraReportLembarUjiFungsi2 = SimpanAlamat

    '            Dim rpt As New xtraLembarUjiFungsiRehabnew

    '            rpt.ShowPrintMarginsWarning = False
    '            rpt.Watermark.Text = sWATERMARK

    '            rpt.bindingSource.DataSource = ds
    '            rpt.ExportToPdf(xtraReportLembarUjiFungsi2)
    '        End If
    '    Catch oErr As Exception
    '        xtraReportLembarUjiFungsi2 = ""
    '        Throw oErr
    '    End Try
    'End Function
    Private Function xtraReportCatatanKlinisRehab(ByVal Kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportCatatanKlinisRehab = ""

            Dim oDigital As New EMedrek.clsFisioterafi_2
            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                xtraReportCatatanKlinisRehab = SimpanAlamat

                Dim rpt As New xtraCatatanKlinisRehab

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(xtraReportCatatanKlinisRehab)
            End If
        Catch oErr As Exception
            xtraReportCatatanKlinisRehab = ""
            Throw oErr
        End Try
    End Function
    Private Function xtraReportResumeRehab(ByVal Kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportResumeRehab = ""

            Dim oDigital As New EMedrek.clsFisioterafi_3
            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                xtraReportResumeRehab = SimpanAlamat

                Dim rpt As New xtraResumeRehab

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = oDigital.GetDataDetail(Kode)
                rpt.ExportToPdf(xtraReportResumeRehab)
            End If
        Catch oErr As Exception
            xtraReportResumeRehab = ""
            Throw oErr
        End Try
    End Function
    Private Function xtraReportLaporanLembarProgramTerapi(ByVal Kode As String, ByVal SimpanAlamat As String) As String
        Try
            xtraReportLaporanLembarProgramTerapi = ""

            Dim dataList As New List(Of Byte())
            Dim oDigital As New Grouper.clsR_CPPT

            Dim ds = oDigital.GetData(Kode)

            If ds IsNot Nothing Then
                xtraReportLaporanLembarProgramTerapi = SimpanAlamat

                Dim rpt As New xtraLembarUjiFungsiRehabResumePerawat

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = ds
                rpt.ExportToPdf(xtraReportLaporanLembarProgramTerapi)
            End If

        Catch oErr As Exception
            xtraReportLaporanLembarProgramTerapi = ""
            Throw oErr
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F5
                If picGrouper.Enabled = True Then
                    picGrouper_Click()
                End If
        End Select
    End Sub
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If grv.GetRowCellValue(e.RowHandle, "CekGrouper") = "GROUPER" Then
            e.Appearance.BackColor = Color.Violet
        ElseIf grv.GetRowCellValue(e.RowHandle, "CekGrouper") = "FINAL KLAIM" Then
            e.Appearance.BackColor = Color.Yellow
        ElseIf grv.GetRowCellValue(e.RowHandle, "CekGrouper") = "KIRIM ONLINE" Then
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
    Private Sub fn_CekData(ByVal norec As String)
        If sTab = 0 Then
            fn_RincianKegridList(norec)
        ElseIf sTab = 1 Then
            Dim FolderSimpan = "C:/Source/MergeCasemix/Cetak/"
            Dim FolderSimpanCasmix = "C:/Source/MergeCasemix/Hasil/"

            If Not IO.Directory.Exists(FolderSimpan) Then
                IO.Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                IO.Directory.CreateDirectory(FolderSimpan)
            End If

            If Not IO.Directory.Exists(FolderSimpanCasmix) Then
                IO.Directory.CreateDirectory(FolderSimpanCasmix)
            Else
                DeleteDirectory(FolderSimpanCasmix)
                IO.Directory.CreateDirectory(FolderSimpanCasmix)
            End If

            PdfViewer1.CloseDocument()

            fn_LoadMergePDF(FolderSimpan, FolderSimpanCasmix, norec, True, chkTampilkanResep.Checked)
        ElseIf sTab = 2
            Dim ds = oGrouperRawatJalan.GetData(grv.GetFocusedRowCellValue("kodegrouper"))
            If ds IsNot Nothing Then
                fn_LoadHistoryPasienCPPT(ds.noRm, ds.nama, ds.tgl_lahir)
            Else
                PdfViewerCPPTRawatJalan.CloseDocument()
                MsgBox("data tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub XtraTabControl1_SelectedPageChanged() Handles XtraTabControl1.SelectedPageChanged
        If grv.GetFocusedRowCellValue("norec") Is Nothing Then
            Exit Sub
        End If

        sTab = XtraTabControl1.SelectedTabPageIndex


        fn_CekData(grv.GetFocusedRowCellValue("norec"))
    End Sub
    Private Sub grv_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grv.FocusedRowChanged
        Try
            PdfViewer1.CloseDocument()
            PdfViewerCPPTRawatJalan.CloseDocument()

            grvRincian.Columns.Clear()
            grdRincian.DataSource = Nothing
            grvRincian.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            If grv.GetFocusedRowCellValue("norec") Is Nothing Then
                Exit Sub
            End If

            fn_CekData(grv.GetFocusedRowCellValue("norec"))

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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
    Private Sub picGrouper_Click() Handles picGrouper.Click
        If grv.GetFocusedRowCellValue("kodegrouper") Is Nothing Then
            Exit Sub
        End If

        Dim oGrouper As New Grouper.clsR_Identitas_Grouper_Data

        Dim dsCekKartuBPJS = oGrouperRawatJalan.GetDataByNoSepJkn(grv.GetFocusedRowCellValue("noSep"))

        If dsCekKartuBPJS Is Nothing Then
            MsgBox("Validasi SEP Nomor SEP Tidak di Temukan" & vbCrLf & "Silahkan Upload Type Rawat Jalan Aplikasi JKN dan Rawat Inap Aplikasi JKN terlebih dahulu, Untuk Pengecekan Kartu BPJS", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim dsCekValidasiKartu = oGrouperRawatJalan.GetData(grv.GetFocusedRowCellValue("kodegrouper"))

            If dsCekValidasiKartu Is Nothing Then
                MsgBox(Statement.ErrorStatement & vbCrLf & "Data Tidak di Temukan di Tabel R_IDENTITAS_GROUPER", MsgBoxStyle.Exclamation, Me.Text)
            Else
                If dsCekKartuBPJS.noKartu <> dsCekValidasiKartu.noKartu Then
                    MsgBox(Statement.ErrorStatement & vbCrLf & "NOMOR KARTU BPJS TIDAK SAMA", MsgBoxStyle.Critical, Me.Text)
                Else
                    If dsCekValidasiKartu.tglSep.ToString("yyyy-MM-dd") <> dsCekKartuBPJS.tglSep Then
                        MsgBox(Statement.ErrorStatement & vbCrLf & "TANGGAL PELAYANAN SEP TIDAK SAMA DENGAN SEP DI SIMRS, SILAHKAN LAKUKAN PERBAIKAN REGISTRASI", MsgBoxStyle.Critical, Me.Text)
                    Else
                        Dim ds = oGrouper.GetData(grv.GetFocusedRowCellValue("kodegrouper"))

                        If ds Is Nothing Then
                            Dim frmGrouperBPJSiDRG As New frmGrouperBPJSiDRG_Awal
                            Try
                                frmGrouperBPJSiDRG.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("kodegrouper"))
                                frmGrouperBPJSiDRG.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmGrouperBPJSiDRG Is Nothing Then frmGrouperBPJSiDRG.Dispose()
                                frmGrouperBPJSiDRG = Nothing
                            End Try
                        Else
                            Dim frmGrouperBPJSiDRG As New frmGrouperBPJSiDRG_Awal
                            Try
                                frmGrouperBPJSiDRG.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.kodegrouper)
                                frmGrouperBPJSiDRG.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If Not frmGrouperBPJSiDRG Is Nothing Then frmGrouperBPJSiDRG.Dispose()
                                frmGrouperBPJSiDRG = Nothing
                            End Try
                        End If

                        Dim dsCekUlang = oGrouper.GetData(grv.GetFocusedRowCellValue("kodegrouper"))
                        If dsCekUlang IsNot Nothing Then
                            grv.SetFocusedRowCellValue("CekGrouper", dsCekUlang.MEMO)
                            grv.UpdateCurrentRow()
                            grv.RefreshRow(grv.GetFocusedDataSourceRowIndex())
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub cboTYPE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTYPE.SelectedIndexChanged
        picUpdate.Visible = False
        lUpdateSEP.Visible = False
        chknorec.Checked = False
        picUpload.Enabled = False
        picGrouper.Enabled = False
        deDATETo.Enabled = True
        picKirimOnline.Enabled = False
        picSimpanPdf.Enabled = False
        picHasilScan.Enabled = False

        If cboTYPE.SelectedIndex = 0 Then
            picUpdate.Visible = True
            lUpdateSEP.Visible = True
            picGrouper.Enabled = True
            picKirimOnline.Enabled = True
            picSimpanPdf.Enabled = True
            picHasilScan.Enabled = True
        ElseIf cboTYPE.SelectedIndex = 1 Then
            picUpdate.Visible = True
            lUpdateSEP.Visible = True
            picGrouper.Enabled = True
            picKirimOnline.Enabled = True
            picSimpanPdf.Enabled = True
            picHasilScan.Enabled = True
            chknorec.Checked = True
        ElseIf cboTYPE.SelectedIndex = 2 Then
            picGrouper.Enabled = True
            'ElseIf cboTYPE.SelectedIndex = 3 Then
            '    picUpload.Enabled = True
        ElseIf cboTYPE.SelectedIndex = 3 Then
            picUpload.Enabled = True
            deDATETo.Enabled = False
        ElseIf cboTYPE.SelectedIndex = 4 Then
            picUpload.Enabled = True
            deDATETo.Enabled = False
        End If

        grv.Columns.Clear()
        grd.DataSource = Nothing
        grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

        grvRincian.Columns.Clear()
        grdRincian.DataSource = Nothing
        grvRincian.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If cboTYPE.SelectedIndex = 0 Then
            fn_LoadHistoryPasien("R.Jalan", True)
        ElseIf cboTYPE.SelectedIndex = 1 Then
            fn_LoadHistoryPasien("R.Inap", True)
        End If
    End Sub
    Private Sub picUpload_Click() Handles picUpload.Click
        If picUpload.Enabled = False Then Exit Sub

        If cboTYPE.SelectedIndex = 3 Then
            fn_UploadDataJKN()
        ElseIf cboTYPE.SelectedIndex = 4 Then
            fn_UploadDataJKN()
        End If
    End Sub
    Private Sub picKirimOnline_Click() Handles picKirimOnline.Click
        If picKirimOnline.Enabled = False Then Exit Sub

        Try
            Dim sTotal As Integer = 0
            Dim sProcess As Integer = 0
            Dim listEror As New List(Of String)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    If grv.GetRowCellValue(i, "CekGrouper") <> "BELUM GROUPER" Then
                        sTotal += 1
                    End If
                End If
            Next

            If sTotal = 0 Then
                MsgBox("Tidak Ada Data yang dikirim Online", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            If MsgBox("Apakah Akan Kirim Online data Sebanyak " & sTotal & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    If grv.GetRowCellValue(i, "CekGrouper") <> "BELUM GROUPER" Then
                        Try
                            Dim jsonDecode = JObject.Parse(oGetGrouper.fn_MengirimKlaimIndividualKeDataCenter(sEklaim_Url, sEklaim_Generate, grv.GetRowCellValue(i, "noSep")))
                            Dim sDataDuplicate As String = String.Empty
                            Dim smessage As String = String.Empty

                            sDataDuplicate = jsonDecode("metadata")("code").ToString
                            smessage = jsonDecode("metadata")("message").ToString

                            If sDataDuplicate <> "200" Then
                                listEror.Add(grv.GetRowCellValue(i, "noSep") & " : " & sDataDuplicate & "-" & smessage)
                            Else
                                oGrouperData.UpdateDataMEMO(grv.GetRowCellValue(i, "kodegrouper"), "KIRIM ONLINE")
                            End If
                        Catch oErr As Exception
                            listEror.Add(grv.GetRowCellValue(i, "noSep") & " : " & "Kirim Online: " & oErr.Message)
                        End Try

                        SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " dari " & sTotal & "")

                        sProcess += 1
                    End If
                End If
            Next

            SplashScreenManager.CloseForm(False)

            If listEror.Count > 0 Then
                MsgBox("Kirim Online Selesai " & String.Join(vbCrLf, listEror.ToArray), MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Kirim Online Selesai", MsgBoxStyle.Information, Me.Text)
            End If

            fn_LoadSecurity()
        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picSimpanPdf_Click() Handles picSimpanPdf.Click
        If picSimpanPdf.Enabled = False Then Exit Sub

        Try
            Dim sTotal As Integer = 0
            Dim sProcess As Integer = 0
            Dim listEror As New List(Of String)

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    If grv.GetRowCellValue(i, "noSep") <> "" Then
                        If grv.GetRowCellValue(i, "CekGrouper") = "KIRIM ONLINE" Then
                            sTotal += 1
                        End If
                    End If
                End If
            Next

            If sTotal = 0 Then
                MsgBox("Tidak Ada Data yag dikirim pdf", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            Dim FolderSimpanCasmix As String = String.Empty

            Using folderDialog As New FolderBrowserDialog()
                ' Pengaturan dialog
                folderDialog.Description = "Pilih folder tempat Anda ingin bekerja:"
                folderDialog.ShowNewFolderButton = True ' Mengizinkan pengguna membuat folder baru

                ' Menampilkan dialog dan menangani hasil
                Dim result As DialogResult = folderDialog.ShowDialog()

                If result = DialogResult.OK Then
                    ' Mengambil path folder yang dipilih
                    FolderSimpanCasmix = folderDialog.SelectedPath
                Else
                    MsgBox("Folder Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
            End Using

            If MsgBox("Apakah Akan Kirim Online data Sebanyak " & sTotal & " Ke Folder " & FolderSimpanCasmix & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            SplashScreenManager.ShowForm(Me, GetType(frmLoadingPanel), True, True, False)

            Dim FolderSimpan = "C:/Source/MergeCasemix/"

            If Not IO.Directory.Exists(FolderSimpan) Then
                IO.Directory.CreateDirectory(FolderSimpan)
            Else
                DeleteDirectory(FolderSimpan)
                IO.Directory.CreateDirectory(FolderSimpan)
            End If

            For i As Integer = 0 To grv.RowCount - 1
                If grv.IsRowSelected(i) = True Then
                    If grv.GetRowCellValue(i, "noSep") <> "" Then
                        If grv.GetRowCellValue(i, "CekGrouper") = "KIRIM ONLINE" Then
                            Try
                                Dim nosep As String = If(IsDBNull(grv.GetRowCellValue(i, "noSep")), "", grv.GetRowCellValue(i, "noSep"))
                                Dim kodegrouper As Integer = 0
                                Dim norec As String = grv.GetRowCellValue(i, "norec")

                                fn_LoadMergePDF(FolderSimpan, FolderSimpanCasmix, norec, False, chkTampilkanResep.Checked)

                            Catch oErr As Exception
                                listEror.Add(grv.GetRowCellValue(i, "noSep") & " : " & "Kirim PDF: " & oErr.Message)
                            End Try

                            'SplashScreenManager.Default.SetWaitFormCaption("Processing data " & sProcess & " dari " & sTotal & "")

                            sProcess += 1
                        End If
                    End If
                End If
            Next

            SplashScreenManager.CloseForm(False)

            If listEror.Count > 0 Then
                MsgBox("Kirim PDF Selesai " & String.Join(vbCrLf, listEror.ToArray), MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox("Kirim PDF Selesai", MsgBoxStyle.Information, Me.Text)
            End If

        Catch oErr As Exception
            SplashScreenManager.CloseForm(False)
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picHasilScan_Click() Handles picHasilScan.Click
        If grv.GetFocusedRowCellValue("kodegrouper") Is Nothing Then
            Exit Sub
        End If

        Dim frmGrouperPDFList As New frmGrouperPDFList
        Try
            'frmGrouperPDFList.fn_LoadMe(grv.GetFocusedRowCellValue("kodegrouper"))
            frmGrouperPDFList.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmGrouperPDFList Is Nothing Then frmGrouperPDFList.Dispose()
            frmGrouperPDFList = Nothing
        End Try
    End Sub
    Private Sub AddItemToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AddItemToolStripMenuItem.Click
        If grvRincian.GetFocusedRowCellValue("produkfk") Is Nothing Then
            Exit Sub
        End If
        If CBool(grvRincian.GetFocusedRowCellValue("isobat")) = True Then
            MsgBox("Item Merupakan Obat-Obatan dan Alkes, Silhakan Pilih Item Lain", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oItem As New Reference.clsItem

        Dim ds = oItem.GetData(grvRincian.GetFocusedRowCellValue("produkfk"))
        If ds Is Nothing Then
            MsgBox("Item Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            'Dim frmItem As New frmItem
            'Try
            '    frmItem.fn_LoadItemData(grvRincian.GetFocusedRowCellValue("produkfk"), grvRincian.GetFocusedRowCellValue("namaproduk"))
            '    frmItem.LoadMe(FORM_MODE.FORM_MODE_ADD)
            '    frmItem.ShowDialog(Me)
            'Catch oErr As Exception
            '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            'Finally
            '    If Not frmItem Is Nothing Then frmItem.Dispose()
            '    frmItem = Nothing
            'End Try
        Else
            Dim frmUpdateKategoriBPJS As New frmUpdateKategoriBPJS
            Try
                frmUpdateKategoriBPJS.LoadMe(ds.KDITEM, ds.NMITEM2)
                frmUpdateKategoriBPJS.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmUpdateKategoriBPJS Is Nothing Then frmUpdateKategoriBPJS.Dispose()
                frmUpdateKategoriBPJS = Nothing
            End Try
        End If

        If grv.GetFocusedRowCellValue("norec") Is Nothing Then
            Exit Sub
        End If

        fn_RincianKegridList(grv.GetFocusedRowCellValue("norec"))
    End Sub
    Private Sub btnCariPDFRincian_Click(sender As Object, e As EventArgs) Handles btnCariPDFRincian.Click
        XtraTabControl1_SelectedPageChanged()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If grv.GetFocusedRowCellValue("kodegrouper") Is Nothing Then
            Exit Sub
        End If

        Dim oGrouper As New Grouper.clsR_Identitas_Grouper_Data

        Dim ds = oGrouper.GetData(grv.GetFocusedRowCellValue("kodegrouper"))

        If ds Is Nothing Then
            Dim frmGrouperBPJSiDRG As New frmGrouperBPJSiDRG_Awal
            Try
                frmGrouperBPJSiDRG.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("kodegrouper"))
                frmGrouperBPJSiDRG.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperBPJSiDRG Is Nothing Then frmGrouperBPJSiDRG.Dispose()
                frmGrouperBPJSiDRG = Nothing
            End Try
        Else
            Dim frmGrouperBPJSiDRG As New frmGrouperBPJSiDRG_Awal
            Try
                frmGrouperBPJSiDRG.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.kodegrouper)
                frmGrouperBPJSiDRG.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmGrouperBPJSiDRG Is Nothing Then frmGrouperBPJSiDRG.Dispose()
                frmGrouperBPJSiDRG = Nothing
            End Try
        End If
    End Sub
#End Region
#Region "Lookup"
    'Private Sub fn_LoadDiagnosa()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String

    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)
    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT * "
    '        SQL &= "FROM "
    '        SQL &= "( "
    '        SQL &= "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_DIAGNOSA A "

    '        SQL &= "UNION "

    '        SQL &= "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_DIAGNOSA_IM A "

    '        SQL &= ") X "

    '        SQL &= "WHERE  "
    '        SQL &= "X.ISACTIVE = 1 "
    '        SQL &= "ORDER BY X.KDDIAGNOSA "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "M_DIAGNOSA")

    '        For iLoop As Integer = 0 To ds.Tables("M_DIAGNOSA").Rows.Count - 1
    '            Dim dsRekap As New DataAccess.M_DIAGNOSA
    '            With ds.Tables("M_DIAGNOSA")
    '                dsRekap.DATECREATED = Now
    '                dsRekap.DATEUPDATED = Now
    '                dsRekap.KDDIAGNOSA = .Rows(iLoop)("KDDIAGNOSA")
    '                dsRekap.MEMO = .Rows(iLoop)("MEMO")
    '                dsRekap.ISDEFAULT = .Rows(iLoop)("ISDEFAULT")
    '                dsRekap.ISACTIVE = .Rows(iLoop)("ISACTIVE")

    '                ListDiagnosa.Add(dsRekap)
    '            End With
    '        Next

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    'Private Sub fn_LoadProsedur()
    '    Try
    '        Dim oConn As New SqlConnection
    '        Dim oComm As New SqlCommand
    '        Dim da As SqlDataAdapter
    '        Dim ds As New DataSet
    '        Dim SQL As String

    '        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

    '        oConn = New SqlConnection(sConn)
    '        If oConn.State = ConnectionState.Closed Then
    '            oConn.Open()
    '        End If

    '        SQL = "SELECT * "
    '        SQL &= "FROM "
    '        SQL &= "( "
    '        SQL &= "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_PROSEDUR A "

    '        SQL &= "UNION "

    '        SQL &= "SELECT "
    '        SQL &= "* "
    '        SQL &= "FROM "
    '        SQL &= "M_PROSEDUR_IM A "
    '        SQL &= ") X "
    '        SQL &= "WHERE X.ISACTIVE = 1 "
    '        SQL &= "ORDER BY X.KDPROSEDUR "

    '        oComm.Connection = oConn
    '        oComm.CommandText = SQL
    '        oComm.CommandTimeout = 120
    '        oComm.CommandType = CommandType.Text

    '        da = New SqlDataAdapter(oComm)
    '        da.Fill(ds, "M_PROSEDUR")

    '        For iLoop As Integer = 0 To ds.Tables("M_PROSEDUR").Rows.Count - 1
    '            Dim dsRekap As New DataAccess.M_PROSEDUR
    '            With ds.Tables("M_PROSEDUR")
    '                dsRekap.DATECREATED = Now
    '                dsRekap.DATEUPDATED = Now
    '                dsRekap.KDPROSEDUR = .Rows(iLoop)("KDPROSEDUR")
    '                dsRekap.MEMO = .Rows(iLoop)("MEMO")
    '                dsRekap.ISDEFAULT = .Rows(iLoop)("ISDEFAULT")
    '                dsRekap.ISACTIVE = .Rows(iLoop)("ISACTIVE")
    '                ListProsedur.Add(dsRekap)
    '            End With
    '        Next

    '        If oConn.State = ConnectionState.Open Then
    '            oConn.Close()
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
End Class