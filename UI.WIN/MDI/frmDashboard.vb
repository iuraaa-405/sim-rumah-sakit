Imports DataAccess

Public Class frmDashboard
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Path to the local image file

        Dim oImage As New Setting.clsUser
        Dim ds = oImage.GetDataImageKode("MDI")

        If ds IsNot Nothing Then
            Try
                Dim img = ds.GAMBAR
                PictureEdit1.Image = ByteArrayToImage(img.ToArray())
            Catch oErr As Exception
            End Try
        End If
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
End Class