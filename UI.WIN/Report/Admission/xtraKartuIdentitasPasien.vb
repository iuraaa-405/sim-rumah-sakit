Public Class xtraKartuIdentitasPasien
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lblREPORT.Text = sNPWP
        lCOMPANY.Text = sCompany
        lblUmur.Text = sUMURPASIEN
    End Sub
End Class