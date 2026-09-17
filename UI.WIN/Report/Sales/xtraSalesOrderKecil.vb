Public Class xtraSalesOrderKecil
    Private Sub xtraSalesOrder_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lbl1.Text = sCompany
        lbl1.Text = sAddress & " " & sPhone
        'lCOMPANY.Text = sCompany
        'lADDRESS.Text = sAddress
        'lPHONE.Text = sPhone
        'lNPWP.Text = sNPWP
    End Sub
End Class