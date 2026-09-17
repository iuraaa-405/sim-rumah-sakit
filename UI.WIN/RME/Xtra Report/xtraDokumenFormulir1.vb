Imports DataAccess

Public Class xtraDokumenFormulir1
    Private Sub xtraOpname_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'If sPictureLogoSEP IsNot Nothing Then
        '    XrPictureBox3.Image = sPictureLogoSEP
        'End If

        'lblJudul1.Text = sNPWP
        'lblJudul2.Text = sCompany
        'lblJudul3.Text = sAddress & " Tlp. " & sPhone
        'lblJudukcl4.Text = sCompany

        'Dim value1 As Object = GetCurrentColumnValue("KDLAPROANPERSALINAN")
        'Try
        '    If value1 IsNot Nothing Then
        '        Dim oDigitalLaporanPersalinan As New EMedrek.clsLaporanPersalinan
        '        Dim ds = oDigitalLaporanPersalinan.GetData(value1.ToString())
        '        If ds IsNot Nothing Then
        '            Dim oDoctor As New Reference.clsDoctor
        '            Dim dsDoctor = oDoctor.GetData(ds.A_IDENTITASPASIEN_LIST.KDDOCTOR)
        '            If dsDoctor IsNot Nothing Then
        '                Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                If FileIO.FileSystem.FileExists(Alamat) Then
        '                    XrPictureBox1.Image = Image.FromFile(Alamat)
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
End Class