Imports UI.WIN.MAIN.My.Resources

Public Class xtraPaymentType

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = PaymentType.TITLE.ToUpper
            lTITLE.Text = PaymentType.TITLE.ToUpper

            lMEMO.Text = PaymentType.MEMO.ToUpper
            lISACTIVE.Text = PaymentType.ISACTIVE.ToUpper

            chkISACTIVE.Text = PaymentType.ISACTIVE.ToUpper.Replace("?", ".")
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class