Public Class FormPopUpPdf
    Public sub LoadMe(ByVal filename As String)
        Me.Text = filename
        If String.IsNullOrEmpty(filename) Then
            PdfViewer1.CloseDocument()
        Else
            PdfViewer1.LoadDocument(filename)
        End If
    End sub
End Class