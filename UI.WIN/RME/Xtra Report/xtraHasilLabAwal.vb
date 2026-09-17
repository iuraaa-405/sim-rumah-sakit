Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing

Public Class xtraHasilLabAwal
    Private Sub xtraHasilLabAwal_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint

    End Sub
    Private Sub Detail_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim value1 As Object = GetCurrentColumnValue("WARNA")
        Dim value2 As Object = GetCurrentColumnValue("APPROVE")
        Dim value3 As Object = GetCurrentColumnValue("HASIL")

        If value1 IsNot Nothing Then
            If value1.ToString() = "M" Then
                lblHasil.ForeColor = Color.Red
            Else
                lblHasil.ForeColor = Color.Black
            End If
        End If

        If value2 IsNot Nothing Then
            If CBool(value2.ToString()) = True Then
                XrTableCell48.Font = New Font("Arial", 10, FontStyle.Bold Or FontStyle.Underline)
            Else
                XrTableCell48.Font = New Font("Arial", 10)
            End If
        End If
    End Sub
End Class