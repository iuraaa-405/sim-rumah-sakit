Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.Globalization

Public Class frmReportLoadPasienPACS
#Region "Function"
    Private sKDCUSTOMER As String = String.Empty
    Private sKDSOTRANSAKSI As String = String.Empty
    Private sSEQ As Integer = 0

    Public Sub fn_LoadRM(ByVal RM As String, ByVal KDSOTRANSAKSI As String, ByVal SEQ As Integer)
        sKDCUSTOMER = RM
        sKDSOTRANSAKSI = KDSOTRANSAKSI
        sSEQ = SEQ
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "List Pasien"
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "REQUEST_RADIOLOGI" _
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
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "List Pasien PACS", ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
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

            fn_LoadData()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Master Detail"
    Private Sub fn_LoadData()
        Try
            Dim oUser As New Setting.clsUser
            Dim dsUrlPACS = oUser.GetDataKoneksiBPJS("PACS")
            Dim sURLDATA As String = String.Empty

            If dsUrlPACS IsNot Nothing Then
                sURLDATA = dsUrlPACS.ALAMATWEB
            Else
                MsgBox("Url PACS Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If sURLDATA = "" Then
                MsgBox("Url PACS Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim jsonResponse As String = SatusehatAuth.CariPasienPACS(sKDCUSTOMER, sURLDATA)

            Try
                Dim table As DataTable
                table = New DataTable("DATAKUNJUNGAN")
                table.Columns.Add("idStudie")
                table.Columns.Add("tglStudies")
                table.Columns.Add("idSeries")
                table.Columns.Add("idInstances")

                ' Parse JSON array
                Dim jsonArray As JArray = JArray.Parse(jsonResponse)

                ' Loop melalui setiap patient
                For Each patient As JObject In jsonArray
                    ' Ambil array Studies
                    Dim studies As JArray = patient("Studies")

                    ' Tambahkan setiap Study ID ke list
                    For Each study In studies
                        Dim jsonStudy As String = SatusehatAuth.GetIDPACS(study.ToString(), sURLDATA)

                        Try
                            Dim studyID As JObject = JObject.Parse(jsonStudy)

                            ' Ambil LastUpdate string
                            Dim lastUpdateStr As String = studyID("LastUpdate")?.ToString()
                            Dim tgl As String = "-"

                            If Not String.IsNullOrEmpty(lastUpdateStr) Then
                                ' Konversi ke DateTime (format: yyyyMMddTHHmmss)
                                Dim lastUpdateDate As DateTime = DateTime.ParseExact(lastUpdateStr, "yyyyMMdd\THHmmss", CultureInfo.InvariantCulture)
                                ' Format ke yy-MM-dd HH:mm:ss
                                tgl = lastUpdateDate.ToString("yyyy-MM-dd HH:mm:ss")
                            End If

                            Dim series As JArray = studyID("Series")

                            Dim seriesList As New List(Of String)
                            Dim instancesList As New List(Of String)

                            If series IsNot Nothing AndAlso series.Count > 0 Then
                                For Each seriesItem In series
                                    seriesList.Add(seriesItem.ToString())
                                Next
                            End If

                            Try
                                For Each xloop In seriesList
                                    Dim jsonSeries As String = SatusehatAuth.GetIDSeries(xloop, sURLDATA)
                                    Dim InstancesData As JObject = JObject.Parse(jsonSeries)
                                    Dim instances As JArray = InstancesData("Instances")

                                    If instances IsNot Nothing AndAlso instances.Count > 0 Then
                                        For Each instance In instances
                                            instancesList.Add(instance.ToString())
                                        Next
                                    End If
                                Next
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & "Cari ID Series " & jsonStudy, MsgBoxStyle.Exclamation, Me.Text)
                            End Try

                            table.Rows.Add(New String() {study.ToString(), tgl, String.Join("|", seriesList.ToArray), String.Join("|", instancesList.ToArray)})

                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & "Cari ID Studies " & jsonStudy, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    Next
                Next

                grd.MainView = grv
                grd.DataSource = table
                grd.ForceInitialize()
                fn_LoadFormatData()

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & "Cari PAS By RM " & jsonResponse, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        Catch oErr As Exception
            MsgBox("Eror Pencarian PACS Semua" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
    End Sub
    Private Sub AmbilDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilDataToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("idStudie") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("idSeries") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("idInstances") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("tglStudies") Is Nothing Then
            Exit Sub
        End If

        If sKDSOTRANSAKSI <> "" Then
            Dim oPacs As New Reference.clsSimpanPACS

            Dim dsCek = oPacs.GetDataByStudi(grv.GetFocusedRowCellValue("idStudie"))

            If dsCek IsNot Nothing Then
                MsgBox("Sudah Pernah disimpan ke no Transaksi " & dsCek.KDSOTRANSAKSI, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            Dim isAdd As Boolean = False

            ' ***** HEADER *****
            Dim ds = oPacs.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPacs.GetData(sKDSOTRANSAKSI, sSEQ).DATECREATED
                    isAdd = True
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = sKDSOTRANSAKSI
                .SEQ = sSEQ
                .KDCUSTOMER = sKDCUSTOMER
                .TANGGAL = grv.GetFocusedRowCellValue("tglStudies")
                .ID = grv.GetFocusedRowCellValue("idStudie")
                .Series = grv.GetFocusedRowCellValue("idSeries")
                .Instances = grv.GetFocusedRowCellValue("idInstances")
                .NOIDUSER = sUserID
                .REMARKS = ""
            End With

            If isAdd = False Then
                Try
                    If oPacs.InsertData(ds) = True Then
                        MsgBox("berhasil simpan", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox("gaga simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Simpan" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    If oPacs.UpdateData(ds) = True Then
                        MsgBox("berhasil simpan", MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        MsgBox("gagal simpan", MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Catch ex As Exception
                    MsgBox("Simpan" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub PriviewToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PriviewToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("idStudie") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("idSeries") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("idInstances") Is Nothing Then
            Exit Sub
        End If
        If grv.GetFocusedRowCellValue("tglStudies") Is Nothing Then
            Exit Sub
        End If

        Dim instancesList As New List(Of String)

        Dim Data As String = grv.GetFocusedRowCellValue("idInstances")

        If Data <> "" Then
            Dim oUser As New Setting.clsUser
            Dim dsUrlPACS = oUser.GetDataKoneksiBPJS("PACS")
            Dim sURLDATA As String = String.Empty

            If dsUrlPACS IsNot Nothing Then
                sURLDATA = dsUrlPACS.ALAMATWEB
            Else
                MsgBox("Url PACS Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            If Not String.IsNullOrEmpty(Data) Then
                ' Split string berdasarkan tanda |
                Dim instances As String() = Data.Split("|"c)

                For Each instance In instances
                    If Not String.IsNullOrEmpty(instance) Then
                        instancesList.Add(instance)
                        'SatusehatAuth.GetIDInstances(instance, sURLDATA, "preview")

                        'Dim DataDecrypt = JObject.Parse(oKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))
                        Dim variabel As String = "CMD /c Start chrome /profile-directory=""Default"" """ & sURLDATA & "instances/" & instance & "/" & "preview" & """"
                        Shell(variabel, vbNormalFocus)
                    End If
                Next
            End If

            'Return instancesList
        End If

    End Sub
#End Region
End Class