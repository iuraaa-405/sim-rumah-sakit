Imports QRCoder

Public Class xtraReportSummaryList

    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim sUserTandatangan As String = String.Empty
        Dim UserTandatangan As Object = GetCurrentColumnValue("DPJP")

        If UserTandatangan IsNot Nothing Then
            sUserTandatangan = UserTandatangan.ToString()

            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picTTD.Image = code.GetGraphic(6)
            Catch ex As Exception
                picTTD.Visible = False
            End Try
        End If
        
        

    End Sub
End Class