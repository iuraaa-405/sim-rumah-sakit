Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing

Public Class xtraHasilLabAkhir
    Private Sub xtraHasilLabAkhir_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox2.Image = sPictureLogo
        End If

        lNPWP.Text = sCompany
        lbl1.Text = sAddress
        lbl2.Text = sPhone

        lblUSIA.Text = sUSIA

        Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")
        Dim value2 As Object = GetCurrentColumnValue("KDSOTRANSAKSI") & GetCurrentColumnValue("KDITEM")

        'If value1 IsNot Nothing Then
        '    Try
        '        Dim oDoctor As New Reference.clsDoctor

        '        Dim dsDoctor = oDoctor.GetData(value1.ToString())
        '        If dsDoctor IsNot Nothing Then
        '            If dsDoctor.KODETTD <> "" Then
        '                Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                If FileIO.FileSystem.FileExists(Alamat) Then
        '                    XrPictureBox1.Image = Image.FromFile(Alamat)
        '                End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        '    End Try

        '    lblUSIA.Text = sUSIA
        'End If

        If value2 IsNot Nothing Then
            Dim oADokumen As New Digital.clsDigital_A_Dokumen

            Dim ds = oADokumen.GetData(value2.ToString())
            If ds IsNot Nothing Then
                Dim text As String = ds.MEMO
                XrRichText1.Rtf = text
            End If
        End If
    End Sub
End Class