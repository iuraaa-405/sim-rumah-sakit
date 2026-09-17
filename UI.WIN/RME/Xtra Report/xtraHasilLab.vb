Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing

Public Class xtraHasilLab
    Inherits XtraReport

    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")
        If value1 IsNot Nothing Then
            'Dim oDoctor As New Reference.clsDoctor

            'Dim dsDoctor = oDoctor.GetData(value1.ToString())
            'If dsDoctor IsNot Nothing Then
            '    XrPictureBox1.Image = CType(My.Resources.ResourceManager.GetObject(dsDoctor.VCLAIM_KDDPJP), Image)
            'End If

            Try
                Dim oDoctor As New Reference.clsDoctor

                Dim dsDoctor = oDoctor.GetData(value1.ToString())
                If dsDoctor IsNot Nothing Then
                    If dsDoctor.KODETTD <> "" Then
                        Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                        If FileIO.FileSystem.FileExists(Alamat) Then
                            XrPictureBox1.Image = Image.FromFile(Alamat)
                        End If
                    End If
                End If
            Catch ex As Exception
                'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
            End Try

            lblUSIA.Text = sUSIA
        End If
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