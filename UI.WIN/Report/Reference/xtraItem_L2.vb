Imports UI.WIN.MAIN.My.Resources

Public Class xtraItem_L2

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Item_L2.TITLE.ToUpper
            lTITLE.Text = Item_L2.TITLE.ToUpper

            lMEMO.Text = Item_L2.MEMO.ToUpper
            lISACTIVE.Text = Item_L2.ISACTIVE.ToUpper

            chkISACTIVE.Text = Item_L2.ISACTIVE.ToUpper.Replace("?", ".")
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class