Imports DataAccess

Public Class frmIdentifikasiPasien
    Private oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
    Private Kategori As Integer = 0

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        btnKARTUBEROBAT.LookAndFeel.UseDefaultLookAndFeel = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Sub txtSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSEARCH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_CariPasien(txtSEARCH.Text)
            txtSEARCH.ResetText()
        End If
    End Sub
    Private Sub fn_CariPasien(ByVal Paramater As String)
        Dim oAntrian As New SettingAntrian.clsSetAntrian

        If Kategori = 0 Then
            Dim dsAntrian = oAntrian.GetDataByRMTanggal(Paramater, Now)
            If dsAntrian IsNot Nothing Then
                If dsAntrian.ISPANGGIL < 1 Then
                    fn_PrintStruk7(dsAntrian.KODEBOOKING)
                    oAntrian.UpdateDataIsCheked(dsAntrian.KODEBOOKING, 1, "")

                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
                    Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(dsAntrian.KODEBOOKING, 1, uTime)
                    If JsonRequest <> "" Then
                        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime)
                        If CodeMessage <> "200" Then
                            MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
                        End If
                    End If
                Else
                    MsgBox("Anda sudah Chekin Silahkan, Silahkan menunggu pemanggilan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                Me.Close()
            Else
                MsgBox("Nomor Kartu Berobat " & Paramater & " Tidak ditemukan untuk Kontrol Hari ini, Silahkan Sesuaikan Tanggal Kontrol atau Pilih Antrian Manual", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Else
            Dim dsAntrian = oAntrian.GetData(Paramater, Now)

            If dsAntrian IsNot Nothing Then
                If dsAntrian.ISPANGGIL < 1 Then
                    fn_PrintStruk7(dsAntrian.KODEBOOKING)
                    oAntrian.UpdateDataIsCheked(dsAntrian.KODEBOOKING, 1, "")

                    Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim oUpdateWaktuAntrian As New SettingAntrian_UI.clsSetAntrian_UI
                    Dim JsonRequest As String = oUpdateWaktuAntrian.fn_RequestUpdateWaktuAntrean(dsAntrian.KODEBOOKING, 1, uTime)
                    If JsonRequest <> "" Then
                        Dim CodeMessage As String = oUpdateWaktuAntrian.fn_UpdateWaktuAntrean(JsonRequest, uTime)
                        If CodeMessage <> "200" Then
                            MsgBox("Update Waktu Ke BPJS" & vbCrLf & CodeMessage, MsgBoxStyle.Information, Me.Text)
                        End If
                    End If
                Else
                    MsgBox("Anda sudah Chekin Silahkan, Silahkan menunggu pemanggilan", MsgBoxStyle.Exclamation, Me.Text)
                End If

                Me.Close()
            Else
                MsgBox("Nomor Kartu Berobat " & Paramater & " Tidak ditemukan untuk Kontrol Hari ini, Silahkan Sesuaikan Tanggal Kontrol atau Pilih Antrian Manual", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean
        Try
            Dim rpt As New xtraAntrianManual
            Dim ds = oSet_Antrian_Simpan.GetData(sCode)
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub picConfirm_Click(sender As Object, e As EventArgs) Handles picConfirm.Click
        Me.Close()
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
        txtSEARCH.Clear()
        txtSEARCH.Focus()
    End Sub
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        txtSEARCH.Focus()
        txtSEARCH.Select(txtSEARCH.Text.Length, 0)
        SendKeys.Send("{ENTER}")
    End Sub
    Private Sub SimpleButtonJudul_Click(sender As Object, e As EventArgs) Handles SimpleButtonJudul.Click
        txtSEARCH.Focus()
    End Sub
#End Region
    Private Sub rbCategory_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSEARCH.Focus()
    End Sub
    Private Sub btnKARTUBEROBAT_Click(sender As Object, e As EventArgs) Handles btnKARTUBEROBAT.Click
        Kategori = 0
        btnKARTUBEROBAT.LookAndFeel.UseDefaultLookAndFeel = False
        btnNOMORSKD.LookAndFeel.UseDefaultLookAndFeel = True
        txtSEARCH.Focus()
    End Sub
    Private Sub btnNOMORSKD_Click(sender As Object, e As EventArgs) Handles btnNOMORSKD.Click
        Kategori = 1
        btnKARTUBEROBAT.LookAndFeel.UseDefaultLookAndFeel = True
        btnNOMORSKD.LookAndFeel.UseDefaultLookAndFeel = False
        txtSEARCH.Focus()
    End Sub
End Class