Public Class xtraKartuStatus
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        lCOMPANY.Text = sCompany
        lADDRESS.Text = sAddress
        lPHONE.Text = sPhone
        'lNPWP.Text = sNPWP
        lblJenisDaftar.Text = sJenisDaftar

        'lCETAK.Text = "Tanggal Cetak : " & Now.ToString("dd-MM-yyyy")
        'lTerbilang.Text = "TERBILANG : " & Terbilang(sPrintGrandTotal).ToUpper & " RUPIAH"
    End Sub
    Public Function Terbilang(ByVal x As Decimal) As String
        Dim bilangan As String() = {"", "satu", "dua", "tiga", "empat", "lima", "enam", "tujuh", "delapan", "sembilan", "sepuluh", "sebelas"}
        Dim temp As String = ""

        If x < 12 Then
            temp = " " + bilangan(x)
        ElseIf x < 20 Then
            temp = Terbilang(x - 10).ToString + " belas"
        ElseIf x < 100 Then
            temp = Terbilang(Math.Floor(x / 10)) + " puluh" + Terbilang(x Mod 10)
        ElseIf x < 200 Then
            temp = " seratus" + Terbilang(x - 100)
        ElseIf x < 1000 Then
            temp = Terbilang(Math.Floor(x / 100)) + " ratus" + Terbilang(x Mod 100)
        ElseIf x < 2000 Then
            temp = " seribu" + Terbilang(x - 1000)
        ElseIf x < 1000000 Then
            temp = Terbilang(Math.Floor(x / 1000)) + " ribu" + Terbilang(x Mod 1000)
        ElseIf x < 1000000000 Then
            temp = Terbilang(Math.Floor(x / 1000000)) + " juta" + Terbilang(x Mod 1000000)
        End If

        Return temp
    End Function
End Class