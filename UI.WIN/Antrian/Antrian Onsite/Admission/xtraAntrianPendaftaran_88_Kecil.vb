Imports DataAccess

Public Class xtraAntrianPendaftaran_88_Kecil
    Private Sub xtraAntrianManualPoli_BeforePrint(sender As Object, e As Printing.PrintEventArgs) Handles MyBase.BeforePrint
        'lblCetak.Text = "Jam Cetak " & Now.ToString("HH:mm")
        'If sCetakDariMesin = True Then
        '    lJUDUL.Text = "Registrasi - Mesin"
        'End If

        'lblESTIMASI.Text = IIf(ESTIMASI_CETAK = "", "-", "Estimasi Dilayani: " & ESTIMASI_CETAK)

        lblJudul.Text = sCompany & vbCrLf & sAddress
        lblUsia.Text = sUmur

        'Dim sKODEBOOKING As String = String.Empty

        'Dim value1 As Object = GetCurrentColumnValue("KODEBOOKING")
        'Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian

        'If value1 IsNot Nothing Then
        '    sKODEBOOKING = value1.ToString()
        'End If

        'Dim dsKodeBooking = oSet_Antrian_Simpan.GetData(sKODEBOOKING)
        'If dsKodeBooking IsNot Nothing Then
        '    XrTableCell1.Text = dsKodeBooking.NOMORANTREAN
        'End If

    End Sub
End Class