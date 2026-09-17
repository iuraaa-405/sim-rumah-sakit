Public Class xtraRincianCasmix
    Private Sub xtraCashIn_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        'lCOMPANY.Text = sCompany
        'lADDRESS.Text = sAddress
        'lPHONE.Text = sPhone
        'lNPWP.Text = sNPWP
        'lCETAK.Text = "Tanggal Cetak : " & Now.ToString("dd-MM-yyyy")
        'lTerbilang.Text = "TERBILANG : " & Terbilang(sPrintGrandTotal).ToUpper & " RUPIAH"
        'lblUSER.Text = sUserID

        lblJudul1.Text = sCompany
        lblJudul2.Text = sAddress

        If sPrintPotongan = 0 Then
            lblSubtotal_1.Visible = False
            lblSubtotal_2.Visible = False
            txtSubtotal.Visible = False
            lblPotongan_1.Visible = False
            lblPotongan_2.Visible = False
            txtPotongan.Visible = False
            lblGrandTotal_4.Visible = False
            lblGrandTotal_5.Visible = False
            txtGrandtotal.Visible = False

            lblGrandTotal_1.Visible = True
            lblGrandTotal_2.Visible = True
            lblGrandTotal_3.Visible = True
        Else
            lblSubtotal_1.Visible = True
            lblSubtotal_2.Visible = True
            txtSubtotal.Visible = True
            lblPotongan_1.Visible = True
            lblPotongan_2.Visible = True
            txtPotongan.Visible = True
            lblGrandTotal_4.Visible = True
            lblGrandTotal_5.Visible = True
            txtGrandtotal.Visible = True

            lblGrandTotal_1.Visible = False
            lblGrandTotal_2.Visible = False
            lblGrandTotal_3.Visible = False
        End If

        txtSubtotal.Text = FormatNumber(sPrintSubtotal, 2)
        txtPotongan.Text = FormatNumber(sPrintPotongan, 2)
        txtGrandtotal.Text = FormatNumber(sPrintGrandTotal, 2)

        Dim value2 As Object = GetCurrentColumnValue("KDUSER")

        Try
            If value2 IsNot Nothing Then
                Dim Alamat As String = sALAMATTTD & value2.ToString() & ".jpg"
                If FileIO.FileSystem.FileExists(Alamat) Then
                    XrPictureBox3.Image = Image.FromFile(Alamat)
                End If
            End If
        Catch ex As Exception

        End Try
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