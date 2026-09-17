Imports QRCoder

Public Class xtraReportEMedrekRI_38
    Private sStatusFormSkriningGizi As String = "2024-07-29 00:00:00"
    Dim sDateCreated As Date = Now()

    Private Sub xtraReportEMedrekRI_38_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If Report.GetCurrentColumnValue("ASESMEN_01") IsNot Nothing Then sDateCreated = Report.GetCurrentColumnValue("ASESMEN_01")

        If sDateCreated >= CDate(sStatusFormSkriningGizi) Then
            SubBand2.Visible = True
        Else
            SubBand1.Visible = True
        End If
    End Sub

    Private Sub picPerawat_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPerawat.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(Report.GetCurrentColumnValue("DOKTER2_NAMEDISPLAY"), QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPerawat.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub picPasien_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPasien.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(Report.GetCurrentColumnValue("PASIEN_KELUARGA"), QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picPasien.Image = code.GetGraphic(6)
        Catch ex As Exception

        End Try
    End Sub
End Class