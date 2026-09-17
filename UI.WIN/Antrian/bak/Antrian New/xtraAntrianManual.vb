Imports DataAccess

Public Class xtraAntrianManual
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lblJudul.Text = sCompany & vbCrLf & sAddress
        lblJAM.Text = Now.ToString("HH:mm")

        Try
            Dim value1 As Object = GetCurrentColumnValue("TANGGALPERIKSA")
            Dim value2 As Object = GetCurrentColumnValue("JENISPASIEN_RS")

            If value1 IsNot Nothing Then
                Dim oAntrian As New SettingAntrian.clsSetAntrian

                lblBelumPanggil.Text = "Antrian yang Belum dipanggil " & oAntrian.GetDataSisaSetBooking(CDate(value1.ToString()), value2.ToString(), False)

            End If
        Catch ex As Exception
            lblBelumPanggil.Text = "Antrian yang Belum dipanggil " & 0
        End Try

    End Sub
End Class