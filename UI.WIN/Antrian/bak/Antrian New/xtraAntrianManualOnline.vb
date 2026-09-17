Public Class xtraAntrianManualOnline
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lblJAM.Text = Now.ToString("HH:mm")
    End Sub
End Class