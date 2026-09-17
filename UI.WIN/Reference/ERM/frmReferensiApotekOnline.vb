Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.IO
Imports DevExpress.XtraPdfViewer
Imports DevExpress.XtraPdfViewer.Commands
Imports DevExpress.XtraSplashScreen

Public Class frmReferensiApotekOnline
    Private sKategori As Integer = 0
    Private sKartu As String = String.Empty
    Private UrlApotekOnline As String = String.Empty
    Private ConsumerSecret As String = String.Empty
    Private ConsumerID As String = String.Empty
    Private UserKey As String = String.Empty
    Private KodeApotek As String = String.Empty
    Private oUser As New Setting.clsUser

#Region "Function"
    Public Sub fn_LoadKategori(ByVal Kategori As Integer, ByVal KartuBPJS As String)
        If KartuBPJS <> "" Then
            sKategori = Kategori
            sKartu = KartuBPJS
        End If
    End Sub
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATEFrom.DateTime = Now.AddYears(-2)
        deDateTo.DateTime = Now

        cboType.SelectedIndex = sKategori
        If cboType.SelectedIndex = 8 Then
            txtCARI.Text = sKartu
            fn_LoadSecurity()
        Else
            fn_LoadSecurity()
            fn_LoadDataItem()
        End If

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim dsKoneksiApotekOnline = oUser.GetDataKoneksiBPJS("APOTEKONLINE")
            If dsKoneksiApotekOnline Is Nothing Then
                MsgBox("Url Apotek Online masih kosong, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                picRefresh.Enabled = False
            Else
                UrlApotekOnline = dsKoneksiApotekOnline.ALAMATWEB
                ConsumerSecret = dsKoneksiApotekOnline.SECREATKEY
                ConsumerID = dsKoneksiApotekOnline.CONSID
                UserKey = dsKoneksiApotekOnline.REMARKS
                KodeApotek = dsKoneksiApotekOnline.PPKPELAYANAN

                Dim oOtority As New Setting.clsOtority
                Dim oUser As New Setting.clsUser

                Dim ds = (From x In oOtority.GetDataDetail
                          Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                          Where x.MODUL = "REQUESTRESEP_R" _
                      And y.KDUSER = sUserID
                          Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

                Try
                    picRefresh.Enabled = ds.ISVIEW
                    If ds.ISVIEW = True Then
                        fn_Preview()
                    End If
                Catch ex As Exception
                    MsgBox("Keamanan belum dipasang, tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)

                    picRefresh.Enabled = False
                End Try

            End If
        Catch ex As Exception
            MsgBox("Load Security : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True
    End Function
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing
                grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                Select Case cboType.SelectedIndex
                    Case 0
                        fn_Load01()
                    Case 1
                        fn_Load02()
                    Case 2
                        fn_Load03()
                    Case 3
                        fn_Load04()
                    Case 4
                        fn_Load05()
                    Case 5
                        fn_Load06()
                    Case 6
                        fn_Load07()
                    Case 7
                        fn_Load08()
                    Case 8
                        fn_Load09()
                    Case 9
                        fn_Load10()
                    Case 10
                        fn_Load11()
                    Case 11
                        fn_Load12()
                    Case 12
                        fn_Load13()
                    Case 13
                        fn_Load14()
                End Select
            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_Load01()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            Dim Response As String = oBrigging.Referensi_DPHO(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kodeobat")
                table.Columns.Add("namaobat")
                table.Columns.Add("prb")
                table.Columns.Add("kronis")
                table.Columns.Add("kemo")
                table.Columns.Add("harga")
                table.Columns.Add("restriksi")
                table.Columns.Add("generik")
                table.Columns.Add("aktif")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {IIf(IsDBNull(item("kodeobat").ToString()), "", item("kodeobat").ToString()), IIf(IsDBNull(item("namaobat").ToString()), "", item("namaobat").ToString()), IIf(IsDBNull(item("prb").ToString()), "", item("prb").ToString()), IIf(IsDBNull(item("kronis").ToString()), "", item("kronis").ToString()), IIf(IsDBNull(item("kemo").ToString()), "", item("kemo").ToString()), IIf(IsDBNull(item("harga").ToString()), "", item("harga")), IIf(IsDBNull(item("restriksi")), "", item("restriksi").ToString()), IIf(IsDBNull(item("generik").ToString()), "", item("generik").ToString()), IIf(IsDBNull(item("aktif").ToString()), "", item("aktif").ToString())})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load02()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Kode atau Nama Poli Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.Referensi_Poli(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("nama")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {item("kode"), item("nama")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load03()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Nama Faskes Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.Referensi_FasilitasKesehatan(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, "1", txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("nama")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {item("kode"), item("nama")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load04()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Nama Faskes Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.Referensi_FasilitasKesehatan(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, "2", txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("nama")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {item("kode"), item("nama")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load05()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Kode Apotek Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.Referensi_SettingApotek(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("namaapoteker")
                table.Columns.Add("namakepala")
                table.Columns.Add("jabatankepala")
                table.Columns.Add("nipkepala")
                table.Columns.Add("siup")
                table.Columns.Add("alamat")
                table.Columns.Add("kota")
                table.Columns.Add("namaverifikator")
                table.Columns.Add("nppverifikator")
                table.Columns.Add("namapetugasapotek")
                table.Columns.Add("nippetugasapotek")
                table.Columns.Add("checkstock")

                table.Rows.Add(New String() {DataDecrypt.Item("kode").ToString(), DataDecrypt.Item("namaapoteker").ToString(), DataDecrypt.Item("namakepala").ToString(), DataDecrypt.Item("jabatankepala").ToString(), DataDecrypt.Item("nipkepala").ToString(), DataDecrypt.Item("siup").ToString(), DataDecrypt.Item("alamat").ToString(), DataDecrypt.Item("kota").ToString(), DataDecrypt.Item("namaverifikator").ToString(), DataDecrypt.Item("nppverifikator").ToString(), DataDecrypt.Item("namapetugasapotek").ToString(), DataDecrypt.Item("nippetugasapotek").ToString(), DataDecrypt.Item("checkstock").ToString()})

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()

                'MsgBox(CodeResponse & " " & messageResponse & vbCrLf & DataDecrypt.Item("kode").ToString() _
                '       & vbCrLf & DataDecrypt.Item("kode").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namaapoteker").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namakepala").ToString() _
                '       & vbCrLf & DataDecrypt.Item("jabatankepala").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nipkepala").ToString() _
                '       & vbCrLf & DataDecrypt.Item("siup").ToString() _
                '       & vbCrLf & DataDecrypt.Item("alamat").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namaverifikator").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nppverifikator").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namapetugasapotek").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nippetugasapotek").ToString() _
                '       & vbCrLf & DataDecrypt.Item("checkstock").ToString() _
                '       , MsgBoxStyle.Information, Me.Text)

            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load06()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            Dim Response As String = oBrigging.Referensi_spesialistik(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("nama")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {item("kode"), item("nama")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load07()
        Try
            If cboJENISRESEP.Text = "" Then
                MsgBox("Jenis Obat Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty


            If txtCARI.Text = "" Then
                MsgBox("Filter Pencarian Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.Referensi_Obat(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, IIf(cboJENISRESEP.Text = "Obat PRB", "1", IIf(cboJENISRESEP.Text = "Obat Kronis Belum Stabil", "2", "3")), deDATEFrom.DateTime.ToString("yyyy-MM-dd"), txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)
            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("kode")
                table.Columns.Add("nama")
                table.Columns.Add("harga")

                For Each item In DataDecrypt("list")
                    table.Rows.Add(New String() {item("kode"), item("nama"), item("harga")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load08()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Nomor Kunjungan/SEP Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.DaftarPelayananObat(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("noSepApotek")
                table.Columns.Add("noSepAsal")
                table.Columns.Add("noresep")
                table.Columns.Add("nokartu")
                table.Columns.Add("nmpst")
                table.Columns.Add("kdjnsobat")
                table.Columns.Add("nmjnsobat")
                table.Columns.Add("tglpelayanan")
                table.Columns.Add("kodeobat")

                For Each item In DataDecrypt("detailsep")
                    table.Rows.Add(New String() {
                        item("noSepApotek") _
                        , item("noSepAsal") _
                        , item("noresep") _
                        , item("nokartu") _
                        , item("nmpst") _
                        , item("kdjnsobat") _
                        , item("nmjnsobat") _
                        , item("tglpelayanan") _
                        , item("listobat")("kodeobat")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load09()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("NoKartu Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.RiwayatPelayananObat(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, deDATEFrom.DateTime.ToString("yyyy-MM-dd"), deDateTo.DateTime.ToString("yyyy-MM-dd"), txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("nosjp")
                table.Columns.Add("tglpelayanan")
                table.Columns.Add("noresep")
                table.Columns.Add("kodeobat")
                table.Columns.Add("namaobat")
                table.Columns.Add("jmlobat")

                For Each item In DataDecrypt("list")("history")
                    table.Rows.Add(New String() {
                     item("nosjp") _
                        , item("tglpelayanan") _
                        , item("noresep") _
                        , item("kodeobat") _
                        , item("namaobat") _
                        , item("jmlobat")})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load10()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            If txtCARI.Text = "" Then
                MsgBox("Nomor Kunjungan/SEP : 19 digit Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim Response As String = oBrigging.CariNoKunjungan(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, txtCARI.Text)

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("noSep")
                table.Columns.Add("faskesasalresep")
                table.Columns.Add("nmfaskesasalresep")
                table.Columns.Add("nokartu")
                table.Columns.Add("namapeserta")
                table.Columns.Add("jnskelamin")
                table.Columns.Add("tgllhr")
                table.Columns.Add("pisat")
                table.Columns.Add("kdjenispeserta")
                table.Columns.Add("nmjenispeserta")
                table.Columns.Add("kodebu")
                table.Columns.Add("namabu")
                table.Columns.Add("tglsep")
                table.Columns.Add("tglplgsep")
                table.Columns.Add("jnspelayanan")
                table.Columns.Add("nmdiag")
                table.Columns.Add("poli")
                table.Columns.Add("flagprb")
                table.Columns.Add("namaprb")
                table.Columns.Add("kodedokter")
                table.Columns.Add("namadokter")

                table.Rows.Add(New String() {
                DataDecrypt.Item("noSep").ToString(),
                DataDecrypt.Item("faskesasalresep").ToString(),
                DataDecrypt.Item("nmfaskesasalresep").ToString(),
                DataDecrypt.Item("nokartu").ToString(),
                DataDecrypt.Item("namapeserta").ToString(),
                DataDecrypt.Item("jnskelamin").ToString(),
                DataDecrypt.Item("tgllhr").ToString(),
                DataDecrypt.Item("pisat").ToString(),
                DataDecrypt.Item("kdjenispeserta").ToString(),
                DataDecrypt.Item("nmjenispeserta").ToString(),
                DataDecrypt.Item("kodebu").ToString(),
                DataDecrypt.Item("namabu").ToString(),
                DataDecrypt.Item("tglsep").ToString(),
                DataDecrypt.Item("tglplgsep").ToString(),
                DataDecrypt.Item("jnspelayanan").ToString(),
                DataDecrypt.Item("nmdiag").ToString(),
                DataDecrypt.Item("poli").ToString(),
                DataDecrypt.Item("flagprb").ToString(),
                DataDecrypt.Item("namaprb").ToString(),
                DataDecrypt.Item("kodedokter").ToString(),
                DataDecrypt.Item("namadokter").ToString()})

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()

                'MsgBox(CodeResponse & " " & messageResponse & vbCrLf _
                '       & vbCrLf & DataDecrypt.Item("noSep").ToString() _
                '       & vbCrLf & DataDecrypt.Item("faskesasalresep").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nmfaskesasalresep").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nokartu").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namapeserta").ToString() _
                '       & vbCrLf & DataDecrypt.Item("jnskelamin").ToString() _
                '       & vbCrLf & DataDecrypt.Item("tgllhr").ToString() _
                '       & vbCrLf & DataDecrypt.Item("pisat").ToString() _
                '       & vbCrLf & DataDecrypt.Item("kdjenispeserta").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nmjenispeserta").ToString() _
                '       & vbCrLf & DataDecrypt.Item("kodebu").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namabu").ToString() _
                '       & vbCrLf & DataDecrypt.Item("tglsep").ToString() _
                '       & vbCrLf & DataDecrypt.Item("tglplgsep").ToString() _
                '       & vbCrLf & DataDecrypt.Item("jnspelayanan").ToString() _
                '       & vbCrLf & DataDecrypt.Item("nmdiag").ToString() _
                '       & vbCrLf & DataDecrypt.Item("poli").ToString() _
                '       & vbCrLf & DataDecrypt.Item("flagprb").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namaprb").ToString() _
                '       & vbCrLf & DataDecrypt.Item("kodedokter").ToString() _
                '       & vbCrLf & DataDecrypt.Item("namadokter").ToString() _
                '       , MsgBoxStyle.Information, Me.Text)
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load11()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty
            Dim jsonRequestObat As String = String.Empty

            jsonRequestObat = " { "
            jsonRequestObat &= """kdppk"": """ & txtCARI.Text & ""","
            jsonRequestObat &= """KdJnsObat"": """ & IIf(cboJENISRESEP.Text = "Obat PRB", "1", IIf(cboJENISRESEP.Text = "Obat Kronis Belum Stabil", "2", "3")) & ""","
            jsonRequestObat &= """JnsTgl"": """ & "TGLPELSJP" & ""","
            jsonRequestObat &= """TglMulai"": """ & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & ""","
            jsonRequestObat &= """TglAkhir"": """ & deDateTo.DateTime.ToString("yyyy-MM-dd") & """ "
            jsonRequestObat &= "}  "

            Dim allData = JObject.Parse(oBrigging.DaftarResep(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, jsonRequestObat))

            Try
                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString
            Catch ex As Exception
                MsgBox(allData, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            If CodeResponse = "200" Then
                Dim DataDecrypt = JArray.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("NORESEP")
                table.Columns.Add("NOAPOTIK")
                table.Columns.Add("NOSEP_KUNJUNGAN")
                table.Columns.Add("NOKARTU")
                table.Columns.Add("NAMA")
                table.Columns.Add("TGLENTRY")
                table.Columns.Add("TGLRESEP")
                table.Columns.Add("TGLPELRSP")
                table.Columns.Add("BYTAGRSP")
                table.Columns.Add("BYVERRSP")
                table.Columns.Add("KDJNSOBAT")
                table.Columns.Add("FASKESASAL")

                For Each item In DataDecrypt
                    table.Rows.Add(New String() {IIf(IsDBNull(item("NORESEP").ToString()), "", item("NORESEP").ToString()), IIf(IsDBNull(item("NOAPOTIK").ToString()), "", item("NOAPOTIK").ToString()), IIf(IsDBNull(item("NOSEP_KUNJUNGAN").ToString()), "", item("NOSEP_KUNJUNGAN").ToString()), IIf(IsDBNull(item("NOKARTU").ToString()), "", item("NOKARTU").ToString()), IIf(IsDBNull(item("NAMA").ToString()), "", item("NAMA").ToString()), IIf(IsDBNull(item("TGLENTRY").ToString()), "", item("TGLENTRY")), IIf(IsDBNull(item("TGLRESEP")), "", item("TGLRESEP").ToString()), IIf(IsDBNull(item("TGLPELRSP").ToString()), "", item("TGLPELRSP").ToString()), IIf(IsDBNull(item("BYTAGRSP").ToString()), "", item("BYTAGRSP").ToString()), IIf(IsDBNull(item("BYVERRSP").ToString()), "", item("BYVERRSP").ToString()), IIf(IsDBNull(item("KDJNSOBAT").ToString()), "", item("KDJNSOBAT").ToString()), IIf(IsDBNull(item("FASKESASAL").ToString()), "", item("FASKESASAL").ToString())})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load12()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty
            Dim jsonRequestObat As String = String.Empty

            jsonRequestObat = " { "
            jsonRequestObat &= """kdppk"": """ & txtCARI.Text & ""","
            jsonRequestObat &= """KdJnsObat"": """ & IIf(cboJENISRESEP.Text = "Obat PRB", "1", IIf(cboJENISRESEP.Text = "Obat Kronis Belum Stabil", "2", "3")) & ""","
            jsonRequestObat &= """JnsTgl"": """ & "TGLRSP" & ""","
            jsonRequestObat &= """TglMulai"": """ & deDATEFrom.DateTime.ToString("yyyy-MM-dd") & ""","
            jsonRequestObat &= """TglAkhir"": """ & deDateTo.DateTime.ToString("yyyy-MM-dd") & """ "
            jsonRequestObat &= "}  "

            Dim allData = JObject.Parse(oBrigging.DaftarResep(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, jsonRequestObat))

            Try
                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString
            Catch ex As Exception
                MsgBox(allData, MsgBoxStyle.Exclamation, Me.Text)
            End Try

            If CodeResponse = "200" Then
                Dim DataDecrypt = JArray.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("NORESEP")
                table.Columns.Add("NOAPOTIK")
                table.Columns.Add("NOSEP_KUNJUNGAN")
                table.Columns.Add("NOKARTU")
                table.Columns.Add("NAMA")
                table.Columns.Add("TGLENTRY")
                table.Columns.Add("TGLRESEP")
                table.Columns.Add("TGLPELRSP")
                table.Columns.Add("BYTAGRSP")
                table.Columns.Add("BYVERRSP")
                table.Columns.Add("KDJNSOBAT")
                table.Columns.Add("FASKESASAL")

                For Each item In DataDecrypt
                    table.Rows.Add(New String() {IIf(IsDBNull(item("NORESEP").ToString()), "", item("NORESEP").ToString()), IIf(IsDBNull(item("NOAPOTIK").ToString()), "", item("NOAPOTIK").ToString()), IIf(IsDBNull(item("NOSEP_KUNJUNGAN").ToString()), "", item("NOSEP_KUNJUNGAN").ToString()), IIf(IsDBNull(item("NOKARTU").ToString()), "", item("NOKARTU").ToString()), IIf(IsDBNull(item("NAMA").ToString()), "", item("NAMA").ToString()), IIf(IsDBNull(item("TGLENTRY").ToString()), "", item("TGLENTRY")), IIf(IsDBNull(item("TGLRESEP")), "", item("TGLRESEP").ToString()), IIf(IsDBNull(item("TGLPELRSP").ToString()), "", item("TGLPELRSP").ToString()), IIf(IsDBNull(item("BYTAGRSP").ToString()), "", item("BYTAGRSP").ToString()), IIf(IsDBNull(item("BYVERRSP").ToString()), "", item("BYVERRSP").ToString()), IIf(IsDBNull(item("KDJNSOBAT").ToString()), "", item("KDJNSOBAT").ToString()), IIf(IsDBNull(item("FASKESASAL").ToString()), "", item("FASKESASAL").ToString())})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load13()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            Dim Response As String = oBrigging.DataKlaim(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, CInt(deDATEFrom.DateTime.ToString("MM")), deDATEFrom.DateTime.ToString("yyyy"), IIf(cboJENISRESEP.Text = "", "0", IIf(cboJENISRESEP.Text = "Obat PRB", "1", IIf(cboJENISRESEP.Text = "Obat Kronis Belum Stabil", "2", "3"))), "0")

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("nosepapotek")
                table.Columns.Add("nosepaasal")
                table.Columns.Add("nokartu")
                table.Columns.Add("namapeserta")
                table.Columns.Add("noresep")
                table.Columns.Add("jnsobat")
                table.Columns.Add("tglpelayanan")
                table.Columns.Add("biayapengajuan")
                table.Columns.Add("biayasetuju")

                txtJumlahData.Text = CDec(DataDecrypt.Item("jumlahdata").ToString())
                txtBiayaPengajuan.Text = CDec(DataDecrypt.Item("totalbiayapengajuan").ToString())
                txtBiayaSetujui.Text = CDec(DataDecrypt.Item("totalbiayasetuju").ToString())

                For Each item In DataDecrypt("listsep")
                    table.Rows.Add(New String() {
                                   IIf(IsDBNull(item("nosepapotek").ToString()), "", item("nosepapotek").ToString()),
                                   IIf(IsDBNull(item("nosepaasal").ToString()), "", item("nosepaasal").ToString()),
                                   IIf(IsDBNull(item("nokapst").ToString()), "", item("nokapst").ToString()),
                                   IIf(IsDBNull(item("nmpst").ToString()), "", item("nmpst").ToString()),
                                   IIf(IsDBNull(item("noresep").ToString()), "", item("noresep").ToString()),
                                   IIf(IsDBNull(item("nmjnsobat").ToString()), "", item("nmjnsobat").ToString()),
                                   CDate(IIf(IsDBNull(item("tglpelayanan").ToString()), "", item("tglpelayanan").ToString())),
                                   CDec(IIf(IsDBNull(item("biayapengajuan").ToString()), 0, item("biayapengajuan").ToString())),
                                   CDec(IIf(IsDBNull(item("biayasetujui").ToString()), 0, item("biayasetujui").ToString()))})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Load14()
        Try
            Dim oBrigging As New Brigging.clsSetKoneksi
            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
            Dim CodeResponse As String = String.Empty
            Dim messageResponse As String = String.Empty

            Dim Response As String = oBrigging.DataKlaim(uTime, UrlApotekOnline, ConsumerSecret, ConsumerID, UserKey, CInt(deDATEFrom.DateTime.ToString("MM")), deDATEFrom.DateTime.ToString("yyyy"), IIf(cboJENISRESEP.Text = "", "0", IIf(cboJENISRESEP.Text = "Obat PRB", "1", IIf(cboJENISRESEP.Text = "Obat Kronis Belum Stabil", "2", "3"))), "1")

            If Response.Contains("No Mapping Rule matched") Then
                MsgBox("Not Found", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim allData = JObject.Parse(Response)

            CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
            messageResponse = allData("metaData")("message").ToString

            If CodeResponse = "200" Then
                Dim DataDecrypt = JObject.Parse(oBrigging.Decrypt(allData("response"), ConsumerID & ConsumerSecret & uTime))

                Dim table As DataTable

                table = New DataTable("DPHO")

                table.Columns.Add("nosepapotek")
                table.Columns.Add("nosepaasal")
                table.Columns.Add("nokartu")
                table.Columns.Add("namapeserta")
                table.Columns.Add("noresep")
                table.Columns.Add("jnsobat")
                table.Columns.Add("tglpelayanan")
                table.Columns.Add("biayapengajuan")
                table.Columns.Add("biayasetuju")

                txtJumlahData.Text = CDec(DataDecrypt.Item("jumlahdata").ToString())
                txtBiayaPengajuan.Text = CDec(DataDecrypt.Item("totalbiayapengajuan").ToString())
                txtBiayaSetujui.Text = CDec(DataDecrypt.Item("totalbiayasetuju").ToString())

                For Each item In DataDecrypt("listsep")
                    table.Rows.Add(New String() {
                                   IIf(IsDBNull(item("nosepapotek").ToString()), "", item("nosepapotek").ToString()),
                                   IIf(IsDBNull(item("nosepaasal").ToString()), "", item("nosepaasal").ToString()),
                                   IIf(IsDBNull(item("nokapst").ToString()), "", item("nokapst").ToString()),
                                   IIf(IsDBNull(item("nmpst").ToString()), "", item("nmpst").ToString()),
                                   IIf(IsDBNull(item("noresep").ToString()), "", item("noresep").ToString()),
                                   IIf(IsDBNull(item("nmjnsobat").ToString()), "", item("nmjnsobat").ToString()),
                                   CDate(IIf(IsDBNull(item("tglpelayanan").ToString()), "", item("tglpelayanan").ToString())),
                                   CDec(IIf(IsDBNull(item("biayapengajuan").ToString()), 0, item("biayapengajuan").ToString())),
                                   CDec(IIf(IsDBNull(item("biayasetujui").ToString()), 0, item("biayasetujui").ToString()))})
                Next

                grd.DataSource = table
                grd.ForceInitialize()

                fn_SetFormat()
            Else
                MsgBox(CodeResponse & " " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Load " & cboType.Text & " : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                grv.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                                     "{0:n2}")
                grv.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                grv.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

            End If
        Next
    End Sub
    Private Sub fn_LoadDataItem()
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
            SQL &= "A.KDITEM "
            SQL &= ",A.KDOBATDPHO "
            SQL &= ",A.NMITEM2 "
            SQL &= ",CEKDPHO = ISNULL((SELECT aktif FROM M_ITEM_KDDPHO WHERE A.KDITEM = KDITEM), 'bukan dpho') "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_ITEM")

            grdKDITEM.Properties.DataSource = ds.Tables("M_ITEM")
            grdKDITEM.Properties.ValueMember = "KDITEM"
            grdKDITEM.Properties.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_Preview()
    End Sub
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        lblCari.Text = "Cari :"
        lblDateFrom.Text = "Tanggal Awal :"
        deDATEFrom.ReadOnly = True
        lDateTo.Text = "Tanggal Akhir :"
        deDateTo.ReadOnly = True
        txtCARI.ReadOnly = True
        grdKDITEM.ReadOnly = True
        cboJENISRESEP.ReadOnly = True
        grdKDITEM.Reset()
        txtCARI.ResetText()
        cboJENISRESEP.SelectedIndex = 1
        lblJENISOBAT.Text = "Kode Jenis Obat :"
        lblJumlahData.Visible = False
        lbltotalbiayapengajuan.Visible = False
        lbltotalbiayasetujui.Visible = False
        txtJumlahData.Visible = False
        txtBiayaPengajuan.Visible = False
        txtBiayaSetujui.Visible = False
        txtJumlahData.Text = CDec(0)
        txtBiayaPengajuan.Text = CDec(0)
        txtBiayaSetujui.Text = CDec(0)

        If cboType.SelectedIndex = 0 Then
            grdKDITEM.ReadOnly = False
            fn_LoadDataItem()
        ElseIf cboType.SelectedIndex = 1 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Kode atau Nama Poli :"
        ElseIf cboType.SelectedIndex = 2 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Nama Faskes :"
        ElseIf cboType.SelectedIndex = 3 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Nama Faskes :"
        ElseIf cboType.SelectedIndex = 4 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Kode Apotek :"
            txtCARI.Text = KodeApotek
        ElseIf cboType.SelectedIndex = 6 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Filter Pencarian :"
            deDATEFrom.ReadOnly = False
            lblDateFrom.Text = "Tanggal Resep :"
            cboJENISRESEP.ReadOnly = False
            grdKDITEM.ReadOnly = False
            fn_LoadDataItem()
        ElseIf cboType.SelectedIndex = 7 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Nomor Kunjungan/SEP :"
        ElseIf cboType.SelectedIndex = 8 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "NoKartu :"
            deDATEFrom.ReadOnly = False
            lblDateFrom.Text = "Tanggal Awal :"
            deDateTo.ReadOnly = False
            lDateTo.Text = "Tanggal Akhir :"
        ElseIf cboType.SelectedIndex = 9 Then
            txtCARI.ReadOnly = False
            lblCari.Text = "Nomor Kunjungan/SEP 19 Digit :"
        ElseIf cboType.SelectedIndex = 10 Or cboType.SelectedIndex = 11 Then
            deDATEFrom.ReadOnly = False
            lblDateFrom.Text = "Tanggal Awal :"
            deDateTo.ReadOnly = False
            lDateTo.Text = "Tanggal Akhir :"
            cboJENISRESEP.ReadOnly = False
            txtCARI.ReadOnly = False
            lblCari.Text = "Kode Apotek :"
            txtCARI.Text = KodeApotek
        ElseIf cboType.SelectedIndex = 12 Or cboType.SelectedIndex = 13 Then
            lblJENISOBAT.Text = "Kode Jenis Obat (Jika Kosong Semua Kode Jenis Obat):"
            deDATEFrom.ReadOnly = False
            lblDateFrom.Text = "Bulan dan Tahun :"
            cboJENISRESEP.ReadOnly = False
            cboJENISRESEP.ResetText()
            lblJumlahData.Visible = True
            lbltotalbiayapengajuan.Visible = True
            lbltotalbiayasetujui.Visible = True
            txtJumlahData.Visible = True
            txtBiayaPengajuan.Visible = True
            txtBiayaSetujui.Visible = True
        End If
    End Sub
    Private Sub UpdateKodeObatDPHOToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UpdateKodeObatDPHOToolStripMenuItem.Click
        Try
            If grdKDITEM.Text = "" Then
                MsgBox("Silahkan Pilih Item", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If grv.GetFocusedRowCellValue("kodeobat") Is Nothing Then
                MsgBox("Silahkan Pilih Kode DPHO", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim oFarmasi As New Reference.clsItem_KDDPHO

            If MsgBox("Apakah yakin data sudah sesuai?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            If oFarmasi.UpdateItem(grdKDITEM.EditValue, grv.GetFocusedRowCellValue("kodeobat"), grv.GetFocusedRowCellValue("namaobat"), grv.GetFocusedRowCellValue("prb"), grv.GetFocusedRowCellValue("kronis"), grv.GetFocusedRowCellValue("kemo"), grv.GetFocusedRowCellValue("harga"), grv.GetFocusedRowCellValue("restriksi"), grv.GetFocusedRowCellValue("generik"), grv.GetFocusedRowCellValue("aktif")) = True Then
                oFarmasi.UpdateItemaktifdphoMaster(grdKDITEM.EditValue, grv.GetFocusedRowCellValue("kodeobat"))
                MsgBox("Berhasil Update", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            MsgBox("Update Obat " & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Sub
    Private Sub btnTidakAdaHarga_Click(sender As Object, e As EventArgs) Handles btnTidakAdaHarga.Click
        Try
            If grdKDITEM.Text = "" Then
                MsgBox("Silahkan Pilih Item", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim oFarmasi As New Reference.clsItem_KDDPHO

            Dim dsItem = oFarmasi.GetData(grdKDITEM.EditValue)

            If dsItem IsNot Nothing Then
                If MsgBox("Apakah Akan di aktif/non aktif", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

                If dsItem.aktif = "aktif" Then
                    oFarmasi.UpdateItemaktifdpho(dsItem.KDITEM, "non aktif")
                Else
                    oFarmasi.UpdateItemaktifdpho(dsItem.KDITEM, "aktif")
                End If

                fn_LoadDataItem()
            Else
                MsgBox("Item belum di Update", MsgBoxStyle.Information, Me.Text)
            End If
        Catch ex As Exception
            MsgBox("Update Obat " & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        Try
            If grdKDITEM.Text = "" Then
                MsgBox("Silahkan Pilih Item", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If MsgBox("Apakah yakin data akan di hapus?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

            Dim oFarmasi As New Reference.clsItem_KDDPHO

            Dim dsCek = oFarmasi.GetData(grdKDITEM.EditValue)

            If dsCek IsNot Nothing Then
                If oFarmasi.DeleteData(dsCek.KDITEM) = True Then
                    oFarmasi.UpdateItemaktifdphoMaster(dsCek.KDITEM, "")
                    MsgBox("Berhasil Update", MsgBoxStyle.Information, Me.Text)
                End If
            Else
                MsgBox("data tidak ditemukan", MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception
            MsgBox("Update Obat " & ex.Message, MsgBoxStyle.Information, Me.Text)
        End Try
    End Sub
#End Region
End Class