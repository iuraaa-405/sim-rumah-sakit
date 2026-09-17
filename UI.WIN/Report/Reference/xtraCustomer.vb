Imports UI.WIN.MAIN.My.Resources

Public Class xtraCustomer

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Customer.TITLE.ToUpper
            lTITLE.Text = Customer.TITLE.ToUpper

            lNAME_DISPLAY.Text = Customer.NAME_DISPLAY.ToUpper

            lPHONE.Text = Customer.PHONE.ToUpper
            lFAX.Text = Customer.FAX.ToUpper
            lMOBILE.Text = Customer.MOBILE.ToUpper
            lOTHER.Text = Customer.OTHER.ToUpper
            lEMAIL.Text = Customer.EMAIL.ToUpper
            lWEBSITE.Text = Customer.WEBSITE.ToUpper

            lBILL_STREET.Text = Customer.BILL_STREET.ToUpper
            lBILL_CITY.Text = Customer.BILL_CITY.ToUpper
            lBILL_STATE.Text = Customer.BILL_STATE.ToUpper
            lBILL_ZIP.Text = Customer.BILL_ZIP.ToUpper
            lBILL_COUNTRY.Text = Customer.BILL_COUNTRY.ToUpper

            lKDCOA.Text = Customer.KDCOA.ToUpper

            lINFORMATION_CONTACT.Text = Customer.TAB_CONTACT.ToUpper
            lINFORMATION_ADDRESS.Text = Customer.TAB_BILL.ToUpper
            'lINFORMATION_ACCOUNT.Text = Customer.TAB_ACCOUNT.ToUpper
            lINFORMATION_OTHER.Text = Customer.TAB_OTHER.ToUpper
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class