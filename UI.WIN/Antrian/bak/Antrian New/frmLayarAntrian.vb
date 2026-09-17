Imports System.Data.SqlClient
Imports DataAccess

Public Class frmLayarAntrian
    Private sAlamatFolder As String = "D:\Suara\"

#Region "Function"
    Private Sub frmLayarAntrian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
        fn_SaveSisaAntrian("A", oSet_Antrian_Simpan.GetDataAllData(Now, "A"))
        fn_SaveSisaAntrian("B", oSet_Antrian_Simpan.GetDataAllData(Now, "B"))
        fn_SaveSisaAntrian("C", oSet_Antrian_Simpan.GetDataAllData(Now, "C"))
        fn_SaveSisaAntrian("D", oSet_Antrian_Simpan.GetDataAllData(Now, "D"))
        fn_SaveSisaAntrian("O", oSet_Antrian_Simpan.GetDataAllData(Now, "O"))
        fn_LoadSisaAntrian()

        lblTanggal.Text = Now.ToString("dd-MM-yyyy")
        Timer1.Start()
    End Sub
    Private Function fn_SaveSisaAntrian(ByVal KODE As String, ByVal Lastnumber As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSetAntrianSisa As New SettingAntrian.clsSet_Antrian_Sisa
            Dim ds = oSetAntrianSisa.GetStructureHeader
            With ds
                .KODE = KODE
                .NOMORANTRIAN = Lastnumber
            End With

            If oSetAntrianSisa.GetData(KODE) IsNot Nothing Then
                fn_SaveSisaAntrian = oSetAntrianSisa.UpdateData(ds)
            Else
                fn_SaveSisaAntrian = oSetAntrianSisa.InsertData(ds)
            End If

        Catch oErr As Exception
            fn_SaveSisaAntrian = False
            MsgBox("Load Sisa Data", MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblJam.Text = Now.ToString("HH:mm:ss")
        fn_LoadLoket()
        fn_LoadSisaAntrian()
    End Sub
    Private Sub fn_LoadSisaAntrian()
        Dim oSet_Antrian_Sisa As New SettingAntrian.clsSet_Antrian_Sisa

        For Each xloop In oSet_Antrian_Sisa.GetData()

            If xloop.KODE = "A" Then
                lblSISA_A.Text = xloop.NOMORANTRIAN
            End If
            If xloop.KODE = "B" Then
                lblSISA_B.Text = xloop.NOMORANTRIAN
            End If
            If xloop.KODE = "C" Then
                lblSISA_C.Text = xloop.NOMORANTRIAN
            End If
            If xloop.KODE = "D" Then
                lblSISA_O.Text = xloop.NOMORANTRIAN
            End If
        Next
    End Sub
    Private Sub fn_LoadLoket()
        Dim oSetAntriangPanggil As New SettingAntrian.clsSet_Antrian_Panggil

        For Each xloop In oSetAntriangPanggil.GetDataByIschekd
            If xloop.LOKET = 1 Then
                lblLoket1.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 2 Then
                lblLoket2.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 3 Then
                lblLoket3.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 4 Then
                lblLoket4.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 5 Then
                lblLoket5.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 6 Then
                lblLoket6.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 7 Then
                lblLoket7.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 8 Then
                lblLoket8.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.LOKET = 9 Then
                lblLoket9.Text = xloop.KODE & "-" & xloop.NOMORANTRIAN
            End If
            If xloop.ISPANGGIL = 0 Then
                Panggil(xloop.NOMORANTRIAN, xloop.LOKET, xloop.KODE, xloop.ISULANG)
                fn_UpdateIsPanggil(xloop.LOKET)
            End If
        Next
    End Sub
    Private Sub Panggil(ByVal nilai As Long, ByVal LOKET As String, ByVal KODE As String, ByVal sPanggilUlang As Boolean)
        If sPanggilUlang = True Then
            Try
                My.Computer.Audio.Play(sAlamatFolder & "Ulang.wav", AudioPlayMode.WaitToComplete)
            Catch ex As Exception

            End Try
        End If

        My.Computer.Audio.Play(sAlamatFolder & "Antrian Nomor.wav", AudioPlayMode.WaitToComplete)
        My.Computer.Audio.Play(sAlamatFolder & KODE & ".wav", AudioPlayMode.WaitToComplete)

        Terbilang(nilai)

        My.Computer.Audio.Play(sAlamatFolder & "diloket.wav", AudioPlayMode.WaitToComplete)
        My.Computer.Audio.Play(sAlamatFolder & LOKET & ".wav", AudioPlayMode.WaitToComplete)
    End Sub
    Private Sub Terbilang(ByVal i As Integer)
        Select Case i
            Case 1 To 20
                My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
            Case 21 To 99
                If i = 20 Or i = 30 Or i = 40 Or i = 50 Or i = 60 Or i = 70 Or i = 80 Or i = 90 Then
                    My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
                Else
                    My.Computer.Audio.Play(sAlamatFolder & Int(i / 10) & "0m" & ".wav", AudioPlayMode.WaitToComplete)
                    My.Computer.Audio.Play(sAlamatFolder & i Mod 10 & ".wav", AudioPlayMode.WaitToComplete)
                End If
            Case 100 To 999
                If i = 100 Or i = 200 Or i = 300 Or i = 400 Or i = 500 Or i = 600 Or i = 700 Or i = 800 Or i = 900 Then
                    My.Computer.Audio.Play(sAlamatFolder & i & ".wav", AudioPlayMode.WaitToComplete)
                Else
                    My.Computer.Audio.Play(sAlamatFolder & Int(i / 100) & "00m" & ".wav", AudioPlayMode.WaitToComplete)

                    Dim Puluhan = i Mod 100

                    If Puluhan <= 20 Then
                        My.Computer.Audio.Play(sAlamatFolder & Puluhan & ".wav", AudioPlayMode.WaitToComplete)
                    Else
                        If Puluhan = 20 Or Puluhan = 30 Or Puluhan = 40 Or Puluhan = 50 Or Puluhan = 60 Or Puluhan = 70 Or Puluhan = 80 Or Puluhan = 90 Then
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan & ".wav", AudioPlayMode.WaitToComplete)
                        Else
                            My.Computer.Audio.Play(sAlamatFolder & Int(Puluhan / 10) & "0m" & ".wav", AudioPlayMode.WaitToComplete)
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan Mod 10 & ".wav", AudioPlayMode.WaitToComplete)
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub fn_UpdateIsPanggil(ByVal Loket As String)
        Dim oSetAntriangPanggil As New SettingAntrian.clsSet_Antrian_Panggil
        Dim dsSetAntrianPanggil = oSetAntriangPanggil.GetData(Loket)
        If dsSetAntrianPanggil IsNot Nothing Then
            oSetAntriangPanggil.UpdateDataIsCheked(Loket)
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Stop()
        Timer2.Stop()
        Me.Close()
    End Sub
#End Region
End Class