Imports DataAccess

Public Class xtraRencanaKontrol
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

        If sPictureLogoSEP IsNot Nothing Then
            XrPictureBox2.Image = sPictureLogoSEP
        End If

        deDate.Text = Now.ToString("dd/MM/yyyy")
        Jam.Text = Now.ToString("HH:mm:ss") & " Wib"

        lblLabel_2.Text = sNAMARS

        Try
            Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")

            If value1 IsNot Nothing Then
                Dim oDoctor As New Reference.clsDoctor

                Dim dsDoctor = oDoctor.GetData(value1.ToString())
                If dsDoctor IsNot Nothing Then
                    If dsDoctor.KODETTD <> "" Then
                        Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                        If FileIO.FileSystem.FileExists(Alamat) Then
                            XrPictureBox3.Image = Image.FromFile(Alamat)
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub xtraTrackingPasien_PrintProgress(sender As System.Object, e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles MyBase.PrintProgress
        If e.PrintAction = Printing.PrintAction.PrintToFile Or e.PrintAction = Printing.PrintAction.PrintToPrinter Then
            sCetakSEP = True
        End If
    End Sub

End Class