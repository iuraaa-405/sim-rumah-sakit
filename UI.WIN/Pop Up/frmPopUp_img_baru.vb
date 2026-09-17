Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPopUp_img_baru
    Private down As Boolean = False

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sALAMATTTDGAMBAR_SIMPANFOLDER = String.Empty
        sALAMATTTDGAMBAR_SIMPANFOLDER_SEMENTARA = String.Empty
        sPicture = Nothing
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        Try
            If Not IO.Directory.Exists("C:\SimpanGambar\") Then
                IO.Directory.CreateDirectory("C:\SimpanGambar\")
            Else
                Dim oRME As New RME.clsRME
                oRME.DeleteDirectory("C:\SimpanGambar\")
                IO.Directory.CreateDirectory("C:\SimpanGambar\")
            End If
        Catch ex As Exception

        End Try
        Try
            sALAMATTTDGAMBAR_SIMPANFOLDER = "C:\SimpanGambar\" & Now.ToString("yyyyMMddHHmmss") & ".png"
            sALAMATTTDGAMBAR_SIMPANFOLDER_SEMENTARA = "C:\SimpanGambar\" & Now.ToString("yyyyMMddHHmmss") & "x" & ".png"

            picIMAGE.Image.Save(sALAMATTTDGAMBAR_SIMPANFOLDER, Imaging.ImageFormat.Png)
            picIMAGE.Image.Save(sALAMATTTDGAMBAR_SIMPANFOLDER_SEMENTARA, Imaging.ImageFormat.Png)

            If FileIO.FileSystem.FileExists(sALAMATTTDGAMBAR_SIMPANFOLDER) Then
                Me.Close()
            Else
                sALAMATTTDGAMBAR_SIMPANFOLDER = ""
                MsgBox("Gambar Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch ex As Exception
            sALAMATTTDGAMBAR_SIMPANFOLDER = ""
            MsgBox("Simpan Gambar " & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Public Sub fn_LoadLanguage()

    End Sub
    Private Sub fn_Load()
        Try
            Dim ImagePath As String = sALAMATTTDGAMBAR & "default.png"
            Dim img1 As Bitmap
            Dim newImage As Image = Image.FromFile(sALAMATTTDGAMBAR & "default.png")

            img1 = New Bitmap(ImagePath)
            picIMAGE.ImageLocation = ImagePath

            picIMAGE.Image = newImage
        Catch ex As Exception
            sPicture = Nothing
            'MsgBox("Load List Data Gambar tidak ditemukan dialamat : " & .SIMPANGAMBAR_1, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        sPicture = Nothing
        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
    Private Sub btnTelinga_Click() Handles btnTelinga.ItemClick
        picIMAGE.Image = CType(My.Resources.ResourceManager.GetObject("image2"), Image)
    End Sub
    'Private Sub picGAMBAR2_MouseDown(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseDown
    '    down = True
    'End Sub
    'Private Sub picGAMBAR2_MouseUp(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseUp
    '    down = False
    'End Sub
    'Private Sub picGAMBAR2_MouseMove(sender As Object, e As MouseEventArgs) Handles picGAMBAR2.MouseMove
    '    If down = True Then
    '        Dim ImageToDrawOn As Image
    '        Dim g As Graphics
    '        Dim Brush1 As New SolidBrush(Color.Black)
    '        g = picGAMBAR2.CreateGraphics()
    '        ImageToDrawOn = picGAMBAR2.Image
    '        g = Graphics.FromImage(ImageToDrawOn)
    '        g.FillEllipse(Brush1, e.X, e.Y, 5, 5)
    '        picGAMBAR2.Image = ImageToDrawOn
    '        g.Dispose()
    '        Brush1.Dispose()
    '    End If

    'End Sub
    Private _Previous As System.Nullable(Of Point) = Nothing
    Private Sub pictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseDown
        _Previous = e.Location
        pictureBox1_MouseMove(sender, e)
    End Sub

    Private Sub pictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseMove
        If _Previous IsNot Nothing Then
            Dim GridColor As Color = Color.Red
            Dim GridPen As New Pen(GridColor)
            GridPen.Width = 2

            Using g As Graphics = Graphics.FromImage(picIMAGE.Image)
                g.DrawLine(GridPen, _Previous.Value, e.Location)
                'g.DrawString("TEst", RichTextBox1.Font, Brushes.Black, New PointF(10, 10))

            End Using
            picIMAGE.Invalidate()
            _Previous = e.Location
        End If
    End Sub

    Private Sub pictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles picIMAGE.MouseUp
        _Previous = Nothing
    End Sub
    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        sPicture = Nothing
        'picIMAGE.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
        fn_Load()
        'ResizeBitmap("C:\somefolder\somebitmap.bmp", 500, 500)
    End Sub
    Private Sub TelingaToolStripMenuItem_Click(sender As Object, e As EventArgs)
        picIMAGE.Image = CType(My.Resources.ResourceManager.GetObject("image1"), Image)
    End Sub
    'Function ResizeBitmap(ByVal bitmapToResize As Bitmap, ByVal width As Integer, ByVal height As Integer)
    '    'make a blank bitmap the correct size
    '    Dim NewBitmap As New Bitmap(width, height)
    '    'make an instance of graphics that will draw on "NewBitmap"
    '    Dim BitmpGraphics As Graphics = Graphics.FromImage(NewBitmap)
    '    'work out the scale factor
    '    Dim scaleFactorX As Integer = bitmapToResize.Width / width
    '    Dim scaleFactorY As Integer = bitmapToResize.Height / width
    '    'resize the graphics
    '    BitmpGraphics.ScaleTransform(scaleFactorX, scaleFactorY)
    '    'draw the bitmap to NewBitmap
    '    BitmpGraphics.DrawImage(bitmapToResize, 0, 0)
    '    Return NewBitmap
    'End Function

#End Region
End Class