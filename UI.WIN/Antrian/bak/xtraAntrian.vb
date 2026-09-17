Public Class xtraAntrian
    Private Sub xtra_BeforePrint(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lblAntrian.Text = sNOMORANTRIAN
        'lblSisaAntrian.Text = "Sisa Antrian Pendaftaran: " & sSISAANTRIAN
        'lblAntrianPoli.Text = sNOMORANTRIAN_POLI
    End Sub
    Private Sub GroupHeader2_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        'txtNo.Text = 1 + sNo & "."
    End Sub

End Class