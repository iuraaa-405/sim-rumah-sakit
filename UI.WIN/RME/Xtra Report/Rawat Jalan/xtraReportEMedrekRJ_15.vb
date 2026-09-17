Imports QRCoder

Public Class xtraReportEMedrekRJ_15
    Dim isAsmedAnak As Boolean
    Dim isAsmedDewasa As Boolean

    Private Sub GroupHeader1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupHeader1.BeforePrint
        isAsmedAnak = Report.GetCurrentColumnValue("ISPASIENANAK")
        If isAsmedAnak Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub

    Private Sub GroupFooter1_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles GroupFooter1.BeforePrint
        isAsmedDewasa = Report.GetCurrentColumnValue("ISPASIENDEWASA")
        If isAsmedDewasa Then
            e.Cancel = False
        Else
            e.Cancel = True
        End If
    End Sub



    Private Sub picDokter_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picDokter.BeforePrint
        Try
            Dim gen As New QRCodeGenerator
            Dim data = gen.CreateQrCode(sTandaTanganUser, QRCodeGenerator.ECCLevel.Q)
            Dim code As New QRCode(data)
            picDokter.Image = code.GetGraphic(6)
        Catch ex As Exception
            picDokter.Visible = False
        End Try
    End Sub


End Class