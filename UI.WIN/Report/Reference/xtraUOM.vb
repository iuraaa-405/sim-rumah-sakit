Imports UI.WIN.MAIN.My.Resources

Public Class xtraUOM

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        fn_LoadLanguage()
    End Sub

    Public Sub fn_LoadLanguage()
        Try
            Me.Text = UOM.TITLE.ToUpper
            lTITLE.Text = UOM.TITLE.ToUpper

            lMEMO.Text = UOM.MEMO.ToUpper
            lISACTIVE.Text = UOM.ISACTIVE.ToUpper

            chkISACTIVE.Text = UOM.ISACTIVE.ToUpper.Replace("?", ".")
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class