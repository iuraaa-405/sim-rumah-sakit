Imports DataAccess
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmAntrianOnline
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtSEARCH.Focus()
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub txtSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSEARCH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_CariBookingAntrian(txtSEARCH.Text.ToString.Trim)
            txtSEARCH.ResetText()
        End If
    End Sub
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        fn_CariBookingAntrian(txtSEARCH.Text.ToString.Trim)
        txtSEARCH.ResetText()
    End Sub
    Private Function fn_CariKartuBPJS(ByVal NomorKartu As String) As Boolean
        Try
            fn_CariKartuBPJS = False

            Dim ocustomer As New Reference.clsCustomer

            Dim oCek As Integer = 0

            For Each xloop In ocustomer.GetDataListKartu(NomorKartu)
                oCek += 1
            Next

            If oCek > 1 Then
                fn_CariKartuBPJS = False
                MsgBox("Nomor Kartu BPJS Duplikasi, Silahkan Perbaiki kartu BPJS anda ke Bagian Admisi", MsgBoxStyle.Exclamation, Me.Text)
            Else
                fn_CariKartuBPJS = True
            End If
        Catch oErr As Exception
            fn_CariKartuBPJS = False
            MsgBox("fn_CariKartuBPJS" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_CariBookingAntrian(ByVal Parameter As String)
        Try
            If Parameter = "" Then Exit Sub

            Dim oAntrian As New SettingAntrian.clsSetAntrian
            Dim oCustomer As New Reference.clsCustomer

            Dim dsAntrian = oAntrian.GetData(Parameter)
            If dsAntrian IsNot Nothing Then
                If fn_CariKartuBPJS(dsAntrian.NOMORKARTU) = True Then
                    If dsAntrian.ISONLINE = True Then
                        If dsAntrian.TANGGALPERIKSA.ToString("ddMMyyyy") = Now.ToString("ddMMyyyy") Then
                            Dim dsCustomer = oCustomer.GetData(dsAntrian.NORM)
                            If dsCustomer IsNot Nothing Then
                                Dim frmAntrianCekFinger As New frmAntrianCekFinger
                                Try
                                    frmAntrianCekFinger.fn_LoadKodebooking(dsAntrian.KODEBOOKING)
                                    frmAntrianCekFinger.ShowDialog(Me)
                                Catch oErr As Exception
                                    MsgBox("Load Pop Keyboard" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                End Try
                            Else
                                MsgBox("Merupakan Pasien Baru/Update Data, Silahkan Ke Bagian Admisi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            MsgBox("Tidak Sesuai Tanggal Kunjungan " & dsAntrian.TANGGALPERIKSA.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        MsgBox("Nomor Kode Booking " & dsAntrian.KODEBOOKING & " Merupakan Antrian Onsite" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                End If

            Else
                Dim dsAntrianKartuBPJS = oAntrian.GetDataByKartuBPJSTanggal(Parameter, Now)

                If dsAntrianKartuBPJS IsNot Nothing Then
                    If fn_CariKartuBPJS(dsAntrianKartuBPJS.NOMORKARTU) = True Then
                        If dsAntrianKartuBPJS.ISONLINE = True Then
                            If dsAntrianKartuBPJS.TANGGALPERIKSA.ToString("ddMMyyyy") = Now.ToString("ddMMyyyy") Then
                                Dim dsCustomer = oCustomer.GetData(dsAntrianKartuBPJS.NORM)
                                If dsCustomer IsNot Nothing Then
                                    Dim frmAntrianCekFinger As New frmAntrianCekFinger
                                    Try
                                        frmAntrianCekFinger.fn_LoadKodebooking(dsAntrianKartuBPJS.KODEBOOKING)
                                        frmAntrianCekFinger.ShowDialog(Me)
                                    Catch oErr As Exception
                                        MsgBox("Load Pop Keyboard" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                    End Try
                                Else
                                    MsgBox("Merupakan Pasien Baru, Silahkan Ke Bagian Admisi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            Else
                                MsgBox("Tidak Sesuai Tanggal Kunjungan " & dsAntrianKartuBPJS.TANGGALPERIKSA.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        Else
                            MsgBox("Nomor Kode Booking " & dsAntrianKartuBPJS.KODEBOOKING & " Merupakan Antrian Onsite" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If

                Else
                    Dim dsAntrianNoRM = oAntrian.GetDataByRMTanggalContains(Parameter, Now)
                    If dsAntrianNoRM IsNot Nothing Then
                        If fn_CariKartuBPJS(dsAntrianNoRM.NOMORKARTU) = True Then
                            If dsAntrianNoRM.ISONLINE = True Then
                                If dsAntrianNoRM.TANGGALPERIKSA.ToString("ddMMyyyy") = Now.ToString("ddMMyyyy") Then
                                    Dim dsCustomer = oCustomer.GetData(dsAntrianNoRM.NORM)
                                    If dsCustomer IsNot Nothing Then
                                        Dim frmAntrianCekFinger As New frmAntrianCekFinger
                                        Try
                                            frmAntrianCekFinger.fn_LoadKodebooking(dsAntrianNoRM.KODEBOOKING)
                                            frmAntrianCekFinger.ShowDialog(Me)
                                        Catch oErr As Exception
                                            MsgBox("Load Pop Keyboard" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                        End Try
                                    Else
                                        MsgBox("Merupakan Pasien Baru, Silahkan Ke Bagian Admisi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                                    End If
                                Else
                                    MsgBox("Tidak Sesuai Tanggal Kunjungan " & dsAntrianNoRM.TANGGALPERIKSA.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            Else
                                MsgBox("Nomor Kode Booking " & dsAntrianNoRM.KODEBOOKING & " Merupakan Antrian Onsite" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                            End If
                        End If

                    Else
                        Dim dsAntrianKTP = oAntrian.GetDataByKTPTanggal(Parameter, Now)
                        If dsAntrianKTP IsNot Nothing Then
                            If fn_CariKartuBPJS(dsAntrianKTP.NOMORKARTU) = True Then
                                If dsAntrianKTP.ISONLINE = True Then
                                    If dsAntrianKTP.TANGGALPERIKSA.ToString("ddMMyyyy") = Now.ToString("ddMMyyyy") Then
                                        Dim dsCustomer = oCustomer.GetData(dsAntrianKTP.NORM)
                                        If dsCustomer IsNot Nothing Then
                                            Dim frmAntrianCekFinger As New frmAntrianCekFinger
                                            Try
                                                frmAntrianCekFinger.fn_LoadKodebooking(dsAntrianKTP.KODEBOOKING)
                                                frmAntrianCekFinger.ShowDialog(Me)
                                            Catch oErr As Exception
                                                MsgBox("Load Pop Keyboard" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                                            End Try
                                        Else
                                            MsgBox("Merupakan Pasien Baru, Silahkan Ke Bagian Admisi Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)
                                        End If
                                    Else
                                        MsgBox("Tidak Sesuai Tanggal Kunjungan " & dsAntrianKTP.TANGGALPERIKSA.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                                    End If
                                Else
                                    MsgBox("Nomor Kode Booking " & dsAntrianKTP.KODEBOOKING & " Merupakan Antrian Onsite" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                                End If
                            End If

                        Else
                            MsgBox("Data Tidak ditemukan !!!" & vbCrLf & Parameter, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox("Cari Booking Antrian" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "ButtonNumber"
    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        txtSEARCH.Text = txtSEARCH.Text + "1"
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        txtSEARCH.Text = txtSEARCH.Text + "2"
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        txtSEARCH.Text = txtSEARCH.Text + "3"
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        txtSEARCH.Text = txtSEARCH.Text + "4"
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        txtSEARCH.Text = txtSEARCH.Text + "5"
    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        txtSEARCH.Text = txtSEARCH.Text + "6"
    End Sub
    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        txtSEARCH.Text = txtSEARCH.Text + "7"
    End Sub
    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        txtSEARCH.Text = txtSEARCH.Text + "8"
    End Sub
    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        txtSEARCH.Text = txtSEARCH.Text + "9"
    End Sub
    Private Sub SimpleButton0_Click(sender As Object, e As EventArgs) Handles SimpleButton0.Click
        txtSEARCH.Text = txtSEARCH.Text + "0"
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        txtSEARCH.Text = ""
        txtSEARCH.Focus()
    End Sub
    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        If txtSEARCH.Text < " " Then
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1 + 1)
        Else
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1)
        End If
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
#End Region
End Class