Imports UI.WIN.MAIN.My.Resources

Public Class xtraWarehouse

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Warehouse.TITLE.ToUpper
            lTITLE.Text = Warehouse.TITLE.ToUpper

            lNAME_DISPLAY.Text = Warehouse.NAME_DISPLAY.ToUpper

            lPHONE.Text = Warehouse.PHONE.ToUpper
            lFAX.Text = Warehouse.FAX.ToUpper
            lMOBILE.Text = Warehouse.MOBILE.ToUpper
            lOTHER.Text = Warehouse.OTHER.ToUpper
            lEMAIL.Text = Warehouse.EMAIL.ToUpper
            lWEBSITE.Text = Warehouse.WEBSITE.ToUpper

            lBILL_STREET.Text = Warehouse.BILL_STREET.ToUpper
            lBILL_CITY.Text = Warehouse.BILL_CITY.ToUpper
            lBILL_STATE.Text = Warehouse.BILL_STATE.ToUpper
            lBILL_ZIP.Text = Warehouse.BILL_ZIP.ToUpper
            lBILL_COUNTRY.Text = Warehouse.BILL_COUNTRY.ToUpper


            lINFORMATION_CONTACT.Text = Warehouse.TAB_CONTACT.ToUpper
            lINFORMATION_ADDRESS.Text = Warehouse.TAB_BILL.ToUpper
            lINFORMATION_OTHER.Text = Warehouse.TAB_OTHER.ToUpper
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class