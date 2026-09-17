Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmPrice
    Public iQuantity As Decimal

    Public Sub fn_Load(ByVal sItem As String)
        txtItemName.Text = sItem
    End Sub
    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If fn_Login() = True Then
            iQuantity = CDbl(txtQuantity.Text)
            Me.Close()
        End If
    End Sub
    Private Sub frmQuantity_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.Escape Then
            Me.Close()
        End If
    End Sub
    Public Function fn_Login() As Boolean
        fn_Login = False

        Try
            Dim oUser As New DataAccess.Setting.clsUser
            Dim oOtority As New Setting.clsOtority

            If oUser.GetData.Where(Function(x) x.KDUSER = txtUsername.Text.Trim.ToUpper).Count > 0 Then
                For Each iLoop In oUser.GetData.Where(Function(x) x.KDUSER = txtUsername.Text.Trim.ToUpper)
                    With iLoop
                        If txtUsername.Text.Trim.ToUpper = .KDUSER And txtPassword.Text = .PASSWORD Then
                            Try

                                Dim ds = (From x In oOtority.GetDataDetail _
                                         Join y In oUser.GetData _
                                         On x.KDOTORITY Equals y.KDOTORITY _
                                         Where x.MODUL = "ADMIN" _
                                         And y.KDUSER = txtUsername.Text.Trim.ToUpper _
                                         Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

                                Try
                                    If ds.ISVIEW = True Then
                                        fn_Login = True
                                        Exit Function
                                    Else
                                        MsgBox("You can't access this! Please contact your administrator!", MsgBoxStyle.Information, Me.Text)
                                        txtUsername.Focus()
                                        Exit Function
                                    End If
                                Catch oErr As Exception
                                    MsgBox("You can't access this! Please contact your administrator!", MsgBoxStyle.Information, Me.Text)
                                    txtUsername.Focus()
                                    Exit Function
                                End Try
                            Catch oErr As Exception
                                MsgBox("You can't access this! Please contact your administrator!", MsgBoxStyle.Information, Me.Text)
                                txtUsername.Focus()
                                Exit Function
                            End Try
                        Else
                            MsgBox("Username or Password is not correct! Please contact your administrator!", MsgBoxStyle.Information, Me.Text)
                            txtUsername.Focus()
                            Exit Function
                        End If
                    End With
                Next
            Else
                MsgBox("Username or Password is not correct! Please contact your administrator!", MsgBoxStyle.Information, Me.Text)
                txtUsername.Focus()
                Exit Function
            End If
        Catch ex As Exception

        End Try
    End Function
End Class