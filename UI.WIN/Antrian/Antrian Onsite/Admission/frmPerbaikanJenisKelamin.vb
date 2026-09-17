Imports DataAccess

Public Class frmPerbaikanJenisKelamin
    Private sNomorRM As String = ""

    Public Sub fn_cariKartu(ByVal NomorRM As String)
        sNomorRM = NomorRM
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'sPOSTRAWAT = False
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Sub btnBayar_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        'Laki-laki
        Dim oCUSTOMER As New Reference.clsCustomer
        Dim ds = oCUSTOMER.GetData(sNomorRM)
        If ds IsNot Nothing Then
            If oCUSTOMER.UpdatejenisKelamin(ds.KDCUSTOMER, "0") = True Then
                Me.Close()
            Else
                MsgBox("Gagal Update Jenis Kelamin", MsgBoxStyle.Critical, Me.Text)
            End If
        Else
            Me.Close()
        End If
    End Sub
    Private Sub btnBayarTidak_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Dim oCUSTOMER As New Reference.clsCustomer
        Dim ds = oCUSTOMER.GetData(sNomorRM)
        If ds IsNot Nothing Then
            If oCUSTOMER.UpdatejenisKelamin(ds.KDCUSTOMER, "1") = True Then
                Me.Close()
            Else
                MsgBox("Gagal Update Jenis Kelamin", MsgBoxStyle.Critical, Me.Text)
            End If
        Else
            Me.Close()
        End If
    End Sub
    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles LabelControl1.Click
        Me.Close()
    End Sub
End Class