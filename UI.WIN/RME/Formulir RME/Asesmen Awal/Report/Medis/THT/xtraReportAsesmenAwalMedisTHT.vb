'Imports QRCoder

Public Class xtraReportAsesmenAwalMedisTHT
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class