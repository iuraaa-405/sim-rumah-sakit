Imports DataAccess

Public Class xtraGelangCetak
    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            '
            '
            'lCOMPANY.Text = sCompany
            'lADDRESS.Text = sAddress & " / Telp " & sPhone
        Catch ex As Exception

        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class