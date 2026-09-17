Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmIdentifikasiPasienNew
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sPOSTRAWAT = False
        sKDCUSTOMER_ANTRIAN = String.Empty
        sJENISKUNJUNGAN_ANTRIAN = String.Empty
        sKODEANTRIAN_ANTRIAN = String.Empty
        sNOMORREFERENSI_ANTRIAN = String.Empty
        sASALRUJUKAN_ANTRIAN = String.Empty
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_LoadData()
        If sJENISKUNJUNGAN_ANTRIAN <> "" Then
            If sJENISKUNJUNGAN_ANTRIAN = "2" Then
                Dim frmPilihFaskes As New frmPilihFaskes

                frmPilihFaskes.WindowState = FormWindowState.Normal
                frmPilihFaskes.ShowDialog()
            End If

            Dim oCustomer As New Reference.clsCustomer
            Dim dsCustomer = oCustomer.GetData(txtSEARCH.Text)

            If dsCustomer IsNot Nothing Then
                sKDCUSTOMER_ANTRIAN = txtSEARCH.Text.Trim
            Else
                Dim dsBPJS = oCustomer.GetDataKARTUBPJS(txtSEARCH.Text)
                If dsBPJS Is Nothing Then
                    MsgBox("Nomor Rekam Medis Pasien/Kartu BPJS Tidak ditemukan, Silahkan Ulangi !!", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    sKDCUSTOMER_ANTRIAN = dsBPJS.KDCUSTOMER
                End If
            End If

            If sKDCUSTOMER_ANTRIAN <> "" Then
                Me.Close()
            End If
        Else
            MsgBox("Jenis Kunjungan Kosong, Silahkan Pilih Jenis Kunjungan !!", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub txtSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSEARCH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            fn_LoadData()
        End If
    End Sub
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        'txtSEARCH.Focus()
        'txtSEARCH.Select(txtSEARCH.Text.Length, 0)
        'SendKeys.Send("{ENTER}")

        fn_LoadData()
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
    Private Sub SimpleButton10_Click(sender As Object, e As EventArgs) Handles SimpleButton10.Click
        txtSEARCH.Text = txtSEARCH.Text + "P"
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        txtSEARCH.Clear()
        txtSEARCH.Focus()
    End Sub
    Private Sub SimpleButtonJudul_Click(sender As Object, e As EventArgs) Handles SimpleButtonJudul.Click
        Me.Close()
        'txtSEARCH.Focus()
    End Sub
    Private Sub lbl1_Click(sender As Object, e As EventArgs) Handles lbl1.Click
        lbl1.BackColor = Color.Green
        lbl2.BackColor = Color.Yellow
        lbl3.BackColor = Color.Yellow
        lbl4.BackColor = Color.Yellow

        sASALRUJUKAN_ANTRIAN = "0"
        sJENISKUNJUNGAN_ANTRIAN = "1"
    End Sub
    Private Sub lbl2_Click(sender As Object, e As EventArgs) Handles lbl2.Click
        lbl1.BackColor = Color.Yellow
        lbl2.BackColor = Color.Green
        lbl3.BackColor = Color.Yellow
        lbl4.BackColor = Color.Yellow

        sASALRUJUKAN_ANTRIAN = "0"
        sJENISKUNJUNGAN_ANTRIAN = "2"
    End Sub
    Private Sub lbl3_Click(sender As Object, e As EventArgs) Handles lbl3.Click
        lbl1.BackColor = Color.Yellow
        lbl2.BackColor = Color.Yellow
        lbl3.BackColor = Color.Green
        lbl4.BackColor = Color.Yellow

        sASALRUJUKAN_ANTRIAN = "0"
        sJENISKUNJUNGAN_ANTRIAN = "3"
    End Sub
    Private Sub lbl4_Click(sender As Object, e As EventArgs) Handles lbl4.Click
        lbl1.BackColor = Color.Yellow
        lbl2.BackColor = Color.Yellow
        lbl3.BackColor = Color.Yellow
        lbl4.BackColor = Color.Green

        sASALRUJUKAN_ANTRIAN = "1"
        sJENISKUNJUNGAN_ANTRIAN = "4"
    End Sub
    Private Sub picConfirm_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub
    Private Sub lblA_Click(sender As Object, e As EventArgs) Handles lblA.Click
        sKODEANTRIAN_ANTRIAN = "A"
        TableLayoutPanel1.Visible = False
        P_JENISRUJUKAN.Visible = True
        P_LABEL1.Visible = True
        txtSEARCH.Visible = True
        SimpleButton0.Visible = True
        SimpleButton1.Visible = True
        SimpleButton2.Visible = True
        SimpleButton3.Visible = True
        SimpleButton4.Visible = True
        SimpleButton5.Visible = True
        SimpleButton6.Visible = True
        SimpleButton7.Visible = True
        SimpleButton8.Visible = True
        SimpleButton9.Visible = True
        SimpleButton10.Visible = True
        SimpleButton11.Visible = True
        btnHapus.Visible = True
        btnEnter.Visible = True
        txtSEARCH.Focus()
    End Sub
    Private Sub lblB_Click(sender As Object, e As EventArgs) Handles lblB.Click
        sKODEANTRIAN_ANTRIAN = "B"
        TableLayoutPanel1.Visible = False
        P_JENISRUJUKAN.Visible = True
        P_LABEL1.Visible = True
        txtSEARCH.Visible = True
        SimpleButton0.Visible = True
        SimpleButton1.Visible = True
        SimpleButton2.Visible = True
        SimpleButton3.Visible = True
        SimpleButton4.Visible = True
        SimpleButton5.Visible = True
        SimpleButton6.Visible = True
        SimpleButton7.Visible = True
        SimpleButton8.Visible = True
        SimpleButton9.Visible = True
        SimpleButton10.Visible = True
        SimpleButton11.Visible = True
        btnHapus.Visible = True
        btnEnter.Visible = True
        txtSEARCH.Focus()
    End Sub
    Private Sub lblC_Click(sender As Object, e As EventArgs) Handles lblC.Click
        sKODEANTRIAN_ANTRIAN = "C"
        TableLayoutPanel1.Visible = False
        P_JENISRUJUKAN.Visible = True
        P_LABEL1.Visible = True
        txtSEARCH.Visible = True
        SimpleButton0.Visible = True
        SimpleButton1.Visible = True
        SimpleButton2.Visible = True
        SimpleButton3.Visible = True
        SimpleButton4.Visible = True
        SimpleButton5.Visible = True
        SimpleButton6.Visible = True
        SimpleButton7.Visible = True
        SimpleButton8.Visible = True
        SimpleButton9.Visible = True
        SimpleButton10.Visible = True
        SimpleButton11.Visible = True
        btnHapus.Visible = True
        btnEnter.Visible = True
        txtSEARCH.Focus()
    End Sub
    Private Sub lblD_Click(sender As Object, e As EventArgs)
        sKODEANTRIAN_ANTRIAN = "D"
        TableLayoutPanel1.Visible = False
        P_JENISRUJUKAN.Visible = True
        P_LABEL1.Visible = True
        txtSEARCH.Visible = True
        SimpleButton0.Visible = True
        SimpleButton1.Visible = True
        SimpleButton2.Visible = True
        SimpleButton3.Visible = True
        SimpleButton4.Visible = True
        SimpleButton5.Visible = True
        SimpleButton6.Visible = True
        SimpleButton7.Visible = True
        SimpleButton8.Visible = True
        SimpleButton9.Visible = True
        SimpleButton10.Visible = True
        SimpleButton11.Visible = True
        btnHapus.Visible = True
        btnEnter.Visible = True
        txtSEARCH.Focus()
    End Sub
    Private Sub SimpleButton11_Click(sender As Object, e As EventArgs) Handles SimpleButton11.Click
        If txtSEARCH.Text < " " Then
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1 + 1)
        Else
            txtSEARCH.Text = Mid(txtSEARCH.Text, 1, Len(txtSEARCH.Text) - 1)
        End If
    End Sub
    Private Sub SimpleButton12_Click(sender As Object, e As EventArgs) Handles SimpleButton12.Click
        Me.Close()
    End Sub
#End Region
End Class