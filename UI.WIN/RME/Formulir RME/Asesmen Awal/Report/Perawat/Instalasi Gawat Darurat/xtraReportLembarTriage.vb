'Imports QRCoder
'Imports DataAccess
'Imports System.Linq

Public Class xtraReportLembarTriage
    Private Sub XrPictureBox2_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles picPetugas.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox1.Image = sPictureLogo
            End If
        Catch ex As Exception

        End Try

        'Dim sUserTandatangan As String = String.Empty
        'Dim UserTandatangan As Object = Me.GetCurrentColumnValue("KDUSER")

        'If UserTandatangan IsNot Nothing Then
        '    sUserTandatangan = UserTandatangan.ToString()

        '    Try
        '        Dim gen As New QRCodeGenerator
        '        Dim data = gen.CreateQrCode(sTandaTanganUser & sUserTandatangan, QRCodeGenerator.ECCLevel.Q)
        '        Dim code As New QRCode(data)
        '        picPetugas.Image = code.GetGraphic(6)
        '    Catch ex As Exception
        '        picPetugas.Visible = False
        '    End Try
        'End If
    End Sub
End Class