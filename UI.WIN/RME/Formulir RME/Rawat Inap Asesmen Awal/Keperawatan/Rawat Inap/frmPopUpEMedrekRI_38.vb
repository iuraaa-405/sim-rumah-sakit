Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmPopUpEMedrekRI_38
    Private oLab As String = ""
    Private oRad As String = ""
    Private oDiagnostik As String = ""


    Public Sub fn_LoadMe(ByVal sLab As String, ByVal sRad As String, ByVal sDiagnostik As String)
        oLab = sLab
        oRad = sRad
        oDiagnostik = sDiagnostik
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadLanguage()
    End Sub
    Private Sub cmdSelect_Click()
        If txtLAB.Text = String.Empty And txtRAD.Text = String.Empty And txtDiagnostik.Text = String.Empty Then

            MsgBox("Dibutuhkan Keterangan Laboratorium, Radiologi atau Diagnostik", MsgBoxStyle.Exclamation, Me.Text)
            txtLAB.Focus()
            Exit Sub
        End If


        sFind1 = txtLAB.Text
        sFind2 = txtRAD.Text
        sFind3 = txtDiagnostik.Text

        Me.Close()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Public Sub fn_LoadLanguage()

    End Sub
    Private Sub fn_Load()
        txtLAB.Text = oLab
        txtRAD.Text = oRad
        txtDiagnostik.Text = oDiagnostik
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        cmdSelect_Click()
    End Sub
#End Region
End Class