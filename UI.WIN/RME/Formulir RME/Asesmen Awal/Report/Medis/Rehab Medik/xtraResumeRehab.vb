Imports DataAccess

Public Class xtraResumeRehab
    Private TTDKosong As String = sALAMATTTDPASIEN & "default.png"

    Private Sub xtraResumeRehab_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'Try
        '    Dim value2 As Object = GetCurrentColumnValue("KDKUNJUNGAN")
        '    Dim oRehabMedik As New EMedrek.clsFisioterafi_1
        '    Dim oDoctor As New Reference.clsDoctor
        '    Dim oDaftar As New Admission.clsPendaftaran

        '    If value2 IsNot Nothing Then
        '        Dim ds = oRehabMedik.GetData(value2.ToString())
        '        If ds IsNot Nothing Then
        '            lblTANGGAL.Text = "Garut, " & ds.DATE.ToString("dd-MM-yyyy")

        '            Dim dsKunjungan = oRehabMedik.GetDatabykodeKunjungan(ds.KDKUNJUNGAN)
        '            If dsKunjungan IsNot Nothing Then
        '                lblNAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
        '                lblNAMAPASIENDIBAWAH.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
        '                lblTANGGALLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
        '                lblNIK.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
        '                lblCARABAYAR.Text = dsKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
        '                lblUMUR.Text = oDaftar.GetUmurPasien(dsKunjungan.DATE, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR) & " / " & IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "L", "P")
        '            End If

        '            Dim dsDoctor = oDoctor.GetData(ds.KDDOCTOR)
        '            If dsDoctor IsNot Nothing Then
        '                'lblDOKTER.Text = dsDoctor.NAME_DISPLAY
        '                lblNAMADOKTERDIBAWAH.Text = dsDoctor.NAME_DISPLAY

        '                If dsDoctor.KODETTD <> "" Then
        '                    Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                    If FileIO.FileSystem.FileExists(Alamat) Then
        '                        picDokterDiBawah.Image = Image.FromFile(Alamat)
        '                    Else
        '                        If FileIO.FileSystem.FileExists(TTDKosong) Then
        '                            picDokterDiBawah.Image = Image.FromFile(TTDKosong)
        '                        End If
        '                    End If
        '                Else
        '                    If FileIO.FileSystem.FileExists(TTDKosong) Then
        '                        picDokterDiBawah.Image = Image.FromFile(TTDKosong)
        '                    End If
        '                End If
        '            Else
        '                If FileIO.FileSystem.FileExists(TTDKosong) Then
        '                    picDokterDiBawah.Image = Image.FromFile(TTDKosong)
        '                End If
        '            End If

        '            If FileIO.FileSystem.FileExists(ds.ALAMATSIMPANTTDPASIEN) Then
        '                picPasienDiBawah.Image = Image.FromFile(ds.ALAMATSIMPANTTDPASIEN)
        '            Else
        '                If FileIO.FileSystem.FileExists(TTDKosong) Then
        '                    picPasienDiBawah.Image = Image.FromFile(TTDKosong)
        '                End If
        '            End If
        '        End If
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub Detail_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles Detail.BeforePrint
        Try
            Dim value1 As Object = GetCurrentColumnValue("ALAMATSIMPANTTDPASIEN")
            Dim value2 As Object = GetCurrentColumnValue("KDDOCTOR")
            Dim value3 As Object = GetCurrentColumnValue("KDUSER")

            Dim oDoctor As New Reference.clsDoctor

            If value1 IsNot Nothing Then
                If value1.ToString() <> "" Then
                    If FileIO.FileSystem.FileExists(value1.ToString()) Then
                        picPasien.Image = Image.FromFile(value1.ToString())
                    Else
                        If FileIO.FileSystem.FileExists(TTDKosong) Then
                            picPasien.Image = Image.FromFile(TTDKosong)
                        End If
                    End If
                Else
                    If FileIO.FileSystem.FileExists(TTDKosong) Then
                        picPasien.Image = Image.FromFile(TTDKosong)
                    End If
                End If
            Else
                If FileIO.FileSystem.FileExists(TTDKosong) Then
                    picPasien.Image = Image.FromFile(TTDKosong)
                End If
            End If
            If value2 IsNot Nothing Then
                If value2.ToString() <> "" Then
                    Dim dsDoctor = oDoctor.GetData(value2.ToString())
                    If dsDoctor IsNot Nothing Then
                        Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                        If FileIO.FileSystem.FileExists(Alamat) Then
                            picDoctor.Image = Image.FromFile(Alamat)
                        Else
                            If FileIO.FileSystem.FileExists(TTDKosong) Then
                                picDoctor.Image = Image.FromFile(TTDKosong)
                            End If
                        End If
                    Else
                        If FileIO.FileSystem.FileExists(TTDKosong) Then
                            picDoctor.Image = Image.FromFile(TTDKosong)
                        End If
                    End If
                Else
                    If FileIO.FileSystem.FileExists(TTDKosong) Then
                        picDoctor.Image = Image.FromFile(TTDKosong)
                    End If
                End If
            Else
                If FileIO.FileSystem.FileExists(TTDKosong) Then
                    picDoctor.Image = Image.FromFile(TTDKosong)
                End If
            End If

            If value3 IsNot Nothing Then
                If value3.ToString() <> "" Then
                    Dim Alamat As String = sALAMATTTD & value3.ToString() & ".jpg"

                    If FileIO.FileSystem.FileExists(Alamat) Then
                        picTerafis.Image = Image.FromFile(Alamat)
                    Else
                        If FileIO.FileSystem.FileExists(TTDKosong) Then
                            picTerafis.Image = Image.FromFile(TTDKosong)
                        End If
                    End If
                Else
                    If FileIO.FileSystem.FileExists(TTDKosong) Then
                        picTerafis.Image = Image.FromFile(TTDKosong)
                    End If
                End If
            Else
                If FileIO.FileSystem.FileExists(TTDKosong) Then
                    picTerafis.Image = Image.FromFile(TTDKosong)
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub
End Class