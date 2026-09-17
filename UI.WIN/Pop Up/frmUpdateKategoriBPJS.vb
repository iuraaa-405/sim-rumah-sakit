Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmUpdateKategoriBPJS
    Private oItem As New Reference.clsItem

    Public Sub LoadMe(ByVal Kode As String, ByVal Nama As String)
        txtKDITEM.Text = Kode
        txtNMITEM2.Text = Nama
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadITEM_L3()
    End Sub
    Private Sub fn_LoadITEM_L3()
        Dim oItem_L3 As New Reference.clsItem_L3
        Try
            grdITEM_L3.Properties.DataSource = oItem_L3.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdITEM_L3.Properties.ValueMember = "KDITEM_L3"
            grdITEM_L3.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Try
            If grdITEM_L3.Text = "" Then
                MsgBox("Kelompok Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdITEM_L3.Focus()
                Exit Sub
            End If

            If oItem.UpdateDataKDITEM_L3(txtKDITEM.Text, grdITEM_L3.EditValue) = True Then
                Me.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
End Class