Imports DataAccess

Public Class frmTanggalCPPT

    Public Sub fn_loadMe(ByVal Noid As String)
        Dim oCPPT As New Transaksi.clsCPPT

        Dim dsCPPT = oCPPT.GetData(Noid)

        If dsCPPT IsNot Nothing Then
            deDATE.DateTime = dsCPPT.DATE
        Else
            deDATE.DateTime = Now
        End If
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        'If deDATE.DateTime = "" Then Exit Sub

        sTanggalCPPT = deDATE.DateTime
        Me.Close()
    End Sub
End Class