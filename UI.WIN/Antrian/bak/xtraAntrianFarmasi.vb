Public Class xtraAntrianFarmasi
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        lblJudul.Text = sCompany & vbCrLf & sAddress

    End Sub
End Class