Imports DataAccess

Public Class xtraExpertise
    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox3.Image = sPictureLogo
        End If

        lNPWP.Text = sCompany
        lblJudul1.Text = sAddress
        lblJudul2.Text = sPhone

        Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")
        'If value1 IsNot Nothing Then
        '    Dim oDoctor As New Reference.clsDoctor
        '    Dim dsDoctor = oDoctor.GetData(value1.ToString())
        '    If dsDoctor IsNot Nothing Then
        '        XrPictureBox1.Image = CType(My.Resources.ResourceManager.GetObject(dsDoctor.VCLAIM_KDDPJP), Image)
        '    End If
        'End If

        'Try
        '    If value1 IsNot Nothing Then
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
        '    End If
        'Catch ex As Exception
        '    'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
End Class