Imports Newtonsoft.Json.Linq
Imports Newtonsoft.Json

Public Class frmPesanSatuSehat
    Private sJson As String
    Private sKodeRespon As String
    Public Sub fn_LoadJson(ByVal Parameter As String, ByVal KodeRespon As String)
        sJson = Parameter
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lblKodeRespon.Text = sKodeRespon

        If sJson.Contains("Exception") Then
            txtJson.Text = sJson
        Else
            txtJson.Text = FormatJson(sJson)
        End If
    End Sub
    Private Function FormatJson(json As String) As String
        Try
            ' Parse the JSON string
            Dim parsedJson = JToken.Parse(json)

            ' Format with indentation
            Dim formattedJson = parsedJson.ToString(Formatting.Indented)

            Return formattedJson
        Catch ex As Exception
            Return "Invalid JSON: " & ex.Message
        End Try
    End Function
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub
End Class