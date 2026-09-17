Public Class xtraRujukan
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint

        If sPictureLogoSEP IsNot Nothing Then
            XrPictureBox3.Image = sPictureLogoSEP
        End If

        lblLabel_2.Text = sNAMARS

        deDate.Text = Now.ToString("dd/MM/yyyy")
        Jam.Text = Now.ToString("HH:mm:ss")
    End Sub
    'Private Sub xtraTrackingPasien_PrintProgress(sender As System.Object, e As DevExpress.XtraPrinting.PrintProgressEventArgs) Handles MyBase.PrintProgress
    '    If e.PrintAction = Printing.PrintAction.PrintToFile Or e.PrintAction = Printing.PrintAction.PrintToPrinter Then
    '        sCetakSEP = True
    '    End If
    'End Sub

End Class