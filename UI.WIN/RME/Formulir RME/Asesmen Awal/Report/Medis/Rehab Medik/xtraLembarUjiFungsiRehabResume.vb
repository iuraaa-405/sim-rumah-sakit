Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class xtraLembarUjiFungsiRehabResume
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        Try
            If sPictureLogo IsNot Nothing Then
                XrPictureBox4.Image = sPictureLogo
            End If

            Dim value1 As Object = GetCurrentColumnValue("DATE")
            Dim value2 As Object = GetCurrentColumnValue("KDKUNJUNGAN")
            Dim value3 As Object = GetCurrentColumnValue("KDDOCTOR")
            Dim value4 As Object = GetCurrentColumnValue("ALAMATSIMPANGAMBAR")
            Dim oRehabMedik As New EMedrek.clsFisioterafi_1
            Dim oDoctor As New Reference.clsDoctor
            Dim oDAFTAR As New Admission.clsPendaftaran

            Dim SKALANYERI00 As Object = GetCurrentColumnValue("SKALANYERI00")
            Dim SKALANYERI01 As Object = GetCurrentColumnValue("SKALANYERI01")
            Dim SKALANYERI02 As Object = GetCurrentColumnValue("SKALANYERI02")
            Dim SKALANYERI03 As Object = GetCurrentColumnValue("SKALANYERI03")
            Dim SKALANYERI04 As Object = GetCurrentColumnValue("SKALANYERI04")
            Dim SKALANYERI05 As Object = GetCurrentColumnValue("SKALANYERI05")
            Dim SKALANYERI06 As Object = GetCurrentColumnValue("SKALANYERI06")
            Dim SKALANYERI07 As Object = GetCurrentColumnValue("SKALANYERI07")
            Dim SKALANYERI08 As Object = GetCurrentColumnValue("SKALANYERI08")
            Dim SKALANYERI09 As Object = GetCurrentColumnValue("SKALANYERI09")
            Dim SKALANYERI10 As Object = GetCurrentColumnValue("SKALANYERI10")
            Dim LGS As Object = GetCurrentColumnValue("LGS")
            Dim MMT As Object = GetCurrentColumnValue("MMT")
            Dim UJIFUNGSILAIN As Object = GetCurrentColumnValue("UJIFUNGSILAIN")

            Dim DIAGNOSAMEDIS As Object = GetCurrentColumnValue("DIAGNOSAMEDIS")
            Dim DIAGNOSAFUNGSIONAL As Object = GetCurrentColumnValue("DIAGNOSAFUNGSIONAL")

            If value1 IsNot Nothing Then
                lblTANGGAL.Text = "Garut, " & CDate(value1.ToString()).ToString("dd-MM-yyyy")
            End If
            If value2 IsNot Nothing Then
                Dim dsKunjungan = oRehabMedik.GetDatabykodeKunjungan(value2.ToString())
                If dsKunjungan IsNot Nothing Then
                    lblNIK.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                    lblNAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    'lblNAMAPASIENDIBAWAH.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    JenisKelamin.Text = IIf(dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                    TanggalLahir.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
                    XrTableCell48.Text = oDAFTAR.GetUmurPasien(dsKunjungan.DATE, dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                    lblAGAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                    XrTableCell24.Text = dsKunjungan.M_DEPARTMENT.NAME_DISPLAY

                    'Dim Alamat As String = sALAMATTTDPASIEN & "Fisio_" & dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER & ".png"
                    'If FileIO.FileSystem.FileExists(Alamat) Then
                    '    XrPictureBox2.Image = Image.FromFile(Alamat)
                    'End If
                End If
            End If
            If value3 IsNot Nothing Then
                Dim dsDoctor = oDoctor.GetData(value3.ToString())
                If dsDoctor IsNot Nothing Then
                    lblDokter.Text = dsDoctor.NAME_DISPLAY
                    'lblNAMADOKTERDIBAWAH.Text = dsDoctor.NAME_DISPLAY

                    If dsDoctor.KODETTD <> "" Then
                        Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                        If FileIO.FileSystem.FileExists(Alamat) Then
                            XrPictureBox1.Image = Image.FromFile(Alamat)
                        End If
                    End If
                End If
            End If
            'If value4 IsNot Nothing Then
            '    If FileIO.FileSystem.FileExists(value4.ToString()) Then
            '        XrPictureBox3.Image = Image.FromFile(value4.ToString())
            '    Else
            '        XrPictureBox3.Image = Image.FromFile(sALAMATTTDGAMBAR & "default.png")
            '    End If
            'End If
            Dim SkalaNyeri As String = "Skala Nyeri "
            If SKALANYERI00 IsNot Nothing Then
                If CBool(SKALANYERI00.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 0"
                End If
            End If
            If SKALANYERI01 IsNot Nothing Then
                If CBool(SKALANYERI01.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 1"
                End If
            End If
            If SKALANYERI02 IsNot Nothing Then
                If CBool(SKALANYERI02.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 2"
                End If
            End If
            If SKALANYERI03 IsNot Nothing Then
                If CBool(SKALANYERI03.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 3"
                End If
            End If
            If SKALANYERI04 IsNot Nothing Then
                If CBool(SKALANYERI04.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 4"
                End If
            End If
            If SKALANYERI05 IsNot Nothing Then
                If CBool(SKALANYERI05.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 5"
                End If
            End If
            If SKALANYERI06 IsNot Nothing Then
                If CBool(SKALANYERI06.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 6"
                End If
            End If
            If SKALANYERI07 IsNot Nothing Then
                If CBool(SKALANYERI07.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 7"
                End If
            End If
            If SKALANYERI08 IsNot Nothing Then
                If CBool(SKALANYERI08.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 8"
                End If
            End If
            If SKALANYERI09 IsNot Nothing Then
                If CBool(SKALANYERI09.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 9"
                End If
            End If
            If SKALANYERI10 IsNot Nothing Then
                If CBool(SKALANYERI10.ToString()) = True Then
                    SkalaNyeri = SkalaNyeri & " 10"
                End If
            End If

            If LGS IsNot Nothing Then
                If LGS.ToString() <> "" Then
                    SkalaNyeri = SkalaNyeri & vbCrLf & "LGS " & LGS
                End If
            End If

            If MMT IsNot Nothing Then
                If MMT.ToString() <> "" Then
                    SkalaNyeri = SkalaNyeri & vbCrLf & "MMT " & MMT
                End If
            End If

            If UJIFUNGSILAIN IsNot Nothing Then
                If UJIFUNGSILAIN.ToString() <> "" Then
                    SkalaNyeri = SkalaNyeri & vbCrLf & "Uji Fungsi " & UJIFUNGSILAIN
                End If
            End If

            lblObjective.Text = SkalaNyeri.Trim


            Dim sAssesmen As String = ""
            If DIAGNOSAMEDIS IsNot Nothing Then
                If DIAGNOSAMEDIS.ToString() <> "" Then
                    sAssesmen = "Diagnosa Medis " & DIAGNOSAMEDIS
                End If
            End If

            If DIAGNOSAFUNGSIONAL IsNot Nothing Then
                If DIAGNOSAFUNGSIONAL.ToString() <> "" Then
                    sAssesmen = sAssesmen & vbCrLf & "Diagnosa Fungsional " & DIAGNOSAFUNGSIONAL
                End If
            End If

            lblAssesment.Text = sAssesmen.Trim

        Catch ex As Exception

        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
End Class