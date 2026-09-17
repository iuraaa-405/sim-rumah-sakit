Public Class xtraSET_BOOKING_JADWALOPERASI
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lblALAMAT.Text = sAlamatPasien_Asesmen
        'lblKESATUAN.Text = sKesatuan_Asesmen
        'lblPANGKAT.Text = sPangkat
        lblHARI.Text = sHARIOPERASI
    End Sub
End Class