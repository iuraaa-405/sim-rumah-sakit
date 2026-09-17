Imports UI.WIN.MAIN.My.Resources
Imports DataAccess
Imports Newtonsoft.Json.Linq

Public Class frmPencarianNomorRujukanMultiRecord
    Private sKartuBPJS As String = ""

    Public Sub fn_cariKartu(ByVal KartuBPJS As String)
        sKartuBPJS = KartuBPJS
    End Sub
    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        sFind1_NomorRujukanAsalRujukan = "1"
        sFind1_NomorRujukan = ""
        SimpleButton1.Text = sCompany & vbCrLf & sAddress
    End Sub
    Private Sub btnBATAL_Click(sender As Object, e As EventArgs) Handles btnBATAL.Click
        Me.Close()
    End Sub
    Private Sub grv1Click_Click(sender As Object, e As EventArgs) Handles grv1.Click
        If grv1.GetFocusedRowCellValue("noKunjungan") Is Nothing Then
            Exit Sub
        End If

        sFind1_NomorRujukan = grv1.GetFocusedRowCellValue("noKunjungan")
        Me.Close()
    End Sub
    Private Sub btnBPJSKESEHATAN_Click(sender As Object, e As EventArgs) Handles btnBPJSKESEHATAN.Click
        sFind1_NomorRujukanAsalRujukan = "1"
        fn_LoadKartuBPJSMultiRecord(sKartuBPJS)
    End Sub
    Private Sub btnUMUM_Click(sender As Object, e As EventArgs) Handles btnUMUM.Click
        sFind1_NomorRujukanAsalRujukan = "2"
        fn_LoadKartuBPJSMultiRecord(sKartuBPJS)
    End Sub
    Private Sub fn_LoadKartuBPJSMultiRecord(ByVal sKARTUBPS As String)
        Try
            If sKARTUBPS = String.Empty Then Exit Sub
            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukanKartuMultiRecord(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sKARTUBPS, IIf(sFind1_NomorRujukanAsalRujukan = "1", 0, 1))

                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                    Dim table As DataTable

                    table = New DataTable("M_RUJUKAN")

                    table.Columns.Add("noKunjungan")
                    table.Columns.Add("tglKunjungan")
                    table.Columns.Add("noKartu")
                    table.Columns.Add("nama")
                    table.Columns.Add("provPerujuk")
                    table.Columns.Add("poliRujukan")

                    For Each item In DataDecrypt("rujukan")
                        table.Rows.Add(New String() {item("noKunjungan"), item("tglKunjungan"), item("peserta")("noKartu"), item("peserta")("nama"), item("provPerujuk")("nama"), item("poliRujukan")("nama")})
                    Next

                    grd1.MainView = grv1
                    grd1.DataSource = table
                    grd1.ForceInitialize()

                Else
                    'grv1.Columns.Clear()
                    'grd1.Properties.DataSource = Nothing
                    'grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class