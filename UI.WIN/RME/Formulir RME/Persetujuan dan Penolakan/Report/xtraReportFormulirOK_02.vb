Imports QRCoder

Public Class xtraReportFormulirOK_02
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        If sFind1 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind1, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic1.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
        If sFind2 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind2, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic2.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
        If sFind3 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind3, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic3.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
        If sFind4 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind4, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic4.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
        If sFind5 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind5, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic5.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
        If sFind6 <> "" Then
            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sFind6, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                pic6.Image = code.GetGraphic(6)
            Catch ex As Exception
            End Try
        End If
    End Sub
End Class