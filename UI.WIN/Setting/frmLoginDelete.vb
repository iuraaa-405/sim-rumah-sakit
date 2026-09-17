Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmLoginDelete
    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oConnection As New Setting.clsConnectionUser

        If Not oConnection.GetConnection() Then
            frmDatabaseUser.ShowDialog()
        End If
    End Sub
#Region "Function"
    Public Function fn_Login() As Boolean
        fn_Login = False
        Try
            Dim oUser As New Setting.clsUser
            If oUser.GetData(txtUsername.Text.Trim.ToUpper) IsNot Nothing Then
                Dim ds = oUser.GetData(txtUsername.Text.Trim.ToUpper)
                With ds
                    If txtUsername.Text.Trim.ToUpper = .KDUSER And txtPassword.Text = .PASSWORD Then
                        fn_Login = True
                        Exit Function
                    Else
                        MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                        txtUsername.Focus()
                        Exit Function
                    End If
                End With
            Else
                MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
                txtUsername.Focus()
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorUserPassword, MsgBoxStyle.Information, Me.Text)
        End Try
    End Function
#End Region

#Region "Command Button"
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        If fn_Login() Then
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Environment.Exit(1)
    End Sub
#End Region
End Class