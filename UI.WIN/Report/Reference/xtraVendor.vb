Imports UI.WIN.MAIN.My.Resources

Public Class xtraVendor

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Vendor.TITLE.ToUpper
            lTITLE.Text = Vendor.TITLE.ToUpper

            lNAME_DISPLAY.Text = Vendor.NAME_DISPLAY.ToUpper

            lPHONE.Text = Vendor.PHONE.ToUpper
            lFAX.Text = Vendor.FAX.ToUpper
            lMOBILE.Text = Vendor.MOBILE.ToUpper
            lOTHER.Text = Vendor.OTHER.ToUpper
            lEMAIL.Text = Vendor.EMAIL.ToUpper
            lWEBSITE.Text = Vendor.WEBSITE.ToUpper

            lBILL_STREET.Text = Vendor.BILL_STREET.ToUpper
            lBILL_CITY.Text = Vendor.BILL_CITY.ToUpper
            lBILL_STATE.Text = Vendor.BILL_STATE.ToUpper
            lBILL_ZIP.Text = Vendor.BILL_ZIP.ToUpper
            lBILL_COUNTRY.Text = Vendor.BILL_COUNTRY.ToUpper

            lKDCOA.Text = Vendor.KDCOA.ToUpper

            lINFORMATION_CONTACT.Text = Vendor.TAB_CONTACT.ToUpper
            lINFORMATION_ADDRESS.Text = Vendor.TAB_BILL.ToUpper
            lINFORMATION_ACCOUNT.Text = Vendor.TAB_ACCOUNT.ToUpper
            lINFORMATION_OTHER.Text = Vendor.TAB_OTHER.ToUpper
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class