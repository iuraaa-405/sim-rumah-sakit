Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing

Public Class xtraHasilLab_New
    Inherits XtraReport

    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lblUSIA.Text = sUSIA
    End Sub
    Private Sub Detail1_BeforePrint(sender As Object, e As Printing.PrintEventArgs)
        Dim value1 As Object = GetCurrentColumnValue("WARNA")

        If value1 IsNot Nothing Then
            If value1.ToString() = "H" Then
                XrTableCell47.ForeColor = Color.Black
            ElseIf value1.ToString() = "M" Then
                XrTableCell47.ForeColor = Color.Red
            Else
                XrTableCell47.ForeColor = Color.Black
            End If
        End If
    End Sub
End Class