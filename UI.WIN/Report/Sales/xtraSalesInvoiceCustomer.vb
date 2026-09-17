Imports DataAccess
Public Class xtraSalesInvoiceCustomer
    Private Sub xtraSalesOrder_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'lCOMPANY.Text = sCompany
        'lADDRESS.Text = sAddress
        'lPHONE.Text = sPhone
        'lNPWP.Text = sNPWP
        lTANGGAL.Text = Now.ToString("dd/MM/yyyy")
        lDATEFROM.Text = sDATE1.ToString("dd/MM/yyyy")
        lDATETO.Text = sDATE2.ToString("dd/MM/yyyy")
        Dim oCUSTOMER As New Reference.clsCustomer
        Dim ds = oCUSTOMER.GetData(sCustomer)
        lCUSTOMERNAME.Text = ds.NAME_DISPLAY
        lCUSTOMERADDRESS.Text = ds.BILL_STREET
        lCUSTOMERCITY.Text = ds.BILL_CITY
        lPHONE.Text = ds.PHONE
        lFAX.Text = ds.FAX
    End Sub
End Class