Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing

Public Class xtraGerdQ
    Private Sub xtraHasilLabAwal_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox1.Image = sPictureLogo
        End If

        Dim sKDCUSTOMER As String = String.Empty

        Dim value1 As Object = GetCurrentColumnValue("KDCUSTOMER")
        Dim value2 As Object = GetCurrentColumnValue("DATE")
        Dim value3 As Object = GetCurrentColumnValue("KDPENDAFTARAN")
        Dim value4 As Object = GetCurrentColumnValue("KDDOCTOR")

        If value1 IsNot Nothing Then
            Dim oCustomer As New Reference.clsCustomer
            Dim oRME As New RME.clsRME
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsCustomer = oCustomer.GetData(value1.ToString())
            If dsCustomer IsNot Nothing Then
                lblNIK.Text = dsCustomer.KTP
                lblNama.Text = dsCustomer.NAME_DISPLAY
                lblJenisKelamin.Text = IIf(dsCustomer.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                lblTanggalLahir.Text = dsCustomer.TANGGALLAHIR.ToString("dd-MM-yyyy")
                lblUsia.Text = oRME.GetUmurPasien(CDate(value2.ToString()), dsCustomer.TANGGALLAHIR)
                lblAGAMA.Text = dsCustomer.M_AGAMA.MEMO

                Dim ds = oPendaftaran.GetData(value3.ToString())
                If ds IsNot Nothing Then
                    lblRuangan.Text = ds.M_DEPARTMENT.NAME_DISPLAY
                End If
            End If

            If value4 IsNot Nothing Then
                Try
                    Dim oDoctor As New Reference.clsDoctor

                    Dim dsDoctor = oDoctor.GetData(value4.ToString())
                    If dsDoctor IsNot Nothing Then
                        If dsDoctor.KODETTD <> "" Then
                            Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
                            If FileIO.FileSystem.FileExists(Alamat) Then
                                picDokter.Image = Image.FromFile(Alamat)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
End Class