Imports DataAccess
Imports System.Data.SqlClient

Public Class frmLayarAntrian
    Private oSet_Panggilan As New AntrianRS.clsSetPanggilSET_PANGGIL_ANTRIAN
    Private sAlamatFolder As String = "D:\Suara\"
    Private sNOMORANTRIAN As String = ""
    Private sLOKET As String = ""
    Private sKODE As String = ""
#Region "Function"
    Private Sub frmLayarAntrian_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If sAlamatFolder <> "" Then
            Timer1.Start()
            Timer2.Start()
        Else
            Timer1.Stop()
            Timer2.Stop()
            MsgBox("Folder Suara Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

        lblTanggal.Text = Now.ToString("dd-MM-yyyy")

    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        fn_Timer()
        fn_LoadLoket()
        fn_LoadDataPemetaanRuangan()
    End Sub
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        fn_LoadPanggil()
    End Sub
    Private Sub fn_LoadDataPemetaanRuangan()
        Dim oConn As New SqlConnection
        Dim oComm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Dim SQL As String

        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

        oConn = New SqlConnection(sConn)

        If oConn.State = ConnectionState.Closed Then
            oConn.Open()
        End If

        SQL = "SELECT "
        SQL &= "A.KDKELASAPLICARE "
        SQL &= ",Kelas = B.MEMO "
        SQL &= ",NamaRuangan = C.NAME_DISPLAY "
        SQL &= ",A.KAPASITAS "
        SQL &= ",A.TERSEDIA "
        SQL &= ",A.TERSEDIA_LAKI "
        SQL &= ",A.TERSEDIA_PEREMPUAN "
        SQL &= ",A.TERSEDIA_LAKIPEREMPUAN "
        SQL &= "FROM "
        SQL &= "M_KELASAPLICARE_DEPARTMENT A "
        SQL &= "INNER JOIN M_KELASAPLICARE B "
        SQL &= "ON A.KDKELASAPLICARE = B.KDKELASAPLICARE "
        SQL &= "INNER JOIN M_DEPARTMENT C "
        SQL &= "ON A.KDDEPARTMENT = C.KDDEPARTMENT "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "I_MONITRING_H")

        lblVVIP.Text = 0
        lblVIP.Text = 0
        lblUTAMA.Text = 0
        lblKELASI.Text = 0
        lblKELASII.Text = 0
        lblKELASIII.Text = 0
        lblICU.Text = 0
        lblICCU.Text = 0
        lblNICU.Text = 0
        lblPICU.Text = 0
        lblIGD.Text = 0
        lblUGD.Text = 0
        lblBERSALIN.Text = 0
        lblHCU.Text = 0
        lblISOLASI.Text = 0
        lblNONKELAS.Text = 0

        For iLoop As Integer = 0 To ds.Tables("I_MONITRING_H").Rows.Count - 1
            With ds.Tables("I_MONITRING_H")
                If .Rows(iLoop)("KDKELASAPLICARE") = "VVP" Then
                    lblVVIP.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "VIP" Then
                    lblVIP.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "UTM" Then
                    lblUTAMA.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "KL1" Then
                    lblKELASI.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "KL2" Then
                    lblKELASII.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "KL3" Then
                    lblKELASIII.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "ICU" Then
                    lblICU.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "ICC" Then
                    lblICCU.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "NIC" Then
                    lblNICU.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "PIC" Then
                    lblPICU.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "IGD" Then
                    lblIGD.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "UGD" Then
                    lblUGD.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "SAL" Then
                    lblBERSALIN.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "HCU" Then
                    lblHCU.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "ISO" Then
                    lblISOLASI.Text += .Rows(iLoop)("TERSEDIA")
                End If
                If .Rows(iLoop)("KDKELASAPLICARE") = "NON" Then
                    lblNONKELAS.Text += .Rows(iLoop)("TERSEDIA")
                End If
            End With
        Next

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If
    End Sub
    Private Sub fn_LoadLoket()
        sNOMORANTRIAN = ""
        sLOKET = ""
        sKODE = ""

        Dim oConn As New SqlConnection
        Dim oComm As New SqlCommand
        Dim da As SqlDataAdapter
        Dim ds As New DataSet
        Dim SQL As String
        Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

        oConn = New SqlConnection(sConn)
        If oConn.State = ConnectionState.Closed Then
            oConn.Open()
        End If

        SQL = "SELECT "
        SQL &= "* "
        SQL &= "FROM "
        SQL &= "SET_PANGGIL_ANTRIAN "
        SQL &= "WHERE "
        SQL &= "ISPANGGIL = 0 "

        oComm.Connection = oConn
        oComm.CommandText = SQL
        oComm.CommandTimeout = 120
        oComm.CommandType = CommandType.Text

        da = New SqlDataAdapter(oComm)
        da.Fill(ds, "SET_LOKET")

        If oConn.State = ConnectionState.Open Then
            oConn.Close()
        End If

        For iLoop As Integer = 0 To ds.Tables("SET_LOKET").Rows.Count - 1
            With ds.Tables("SET_LOKET")
                If .Rows(iLoop)("LOKET") = "1" Then
                    lblLoket1.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                End If
                If .Rows(iLoop)("LOKET") = "2" Then
                    lblLoket2.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                End If
                If .Rows(iLoop)("LOKET") = "3" Then
                    lblLoket3.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                End If
                If .Rows(iLoop)("LOKET") = "4" Then
                    lblLoket4.Text = .Rows(iLoop)("KODE") & .Rows(iLoop)("NOMORANTRIAN").ToString.PadLeft(3, "0")
                End If

                sNOMORANTRIAN = .Rows(iLoop)("NOMORANTRIAN")
                sLOKET = .Rows(iLoop)("LOKET")
                sKODE = .Rows(iLoop)("KODE")
            End With
        Next
    End Sub
    Private Sub fn_LoadPanggil()
        If sNOMORANTRIAN <> "" Then
            Panggil(sNOMORANTRIAN, sLOKET, sKODE)
            fn_UpdateIsPanggil(sLOKET)
        End If

        sNOMORANTRIAN = ""

    End Sub
    Private Sub Panggil(ByVal nilai As Long, ByVal LOKET As String, ByVal KODE As String)
        My.Computer.Audio.Play(sAlamatFolder & "opening.wav", AudioPlayMode.WaitToComplete)
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
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan & "m" & ".wav", AudioPlayMode.WaitToComplete)
                        Else
                            My.Computer.Audio.Play(sAlamatFolder & Int(Puluhan / 10) & "0m" & ".wav", AudioPlayMode.WaitToComplete)
                            My.Computer.Audio.Play(sAlamatFolder & Puluhan Mod 10 & ".wav", AudioPlayMode.WaitToComplete)
                        End If
                    End If
                End If
        End Select
    End Sub
    Private Sub fn_UpdateIsPanggil(ByVal Loket As String)
        Dim dsSet_Panggilan = oSet_Panggilan.GetData(Loket)
        If dsSet_Panggilan IsNot Nothing Then
            oSet_Panggilan.UpdateIsPanggil(Loket)
        End If
    End Sub
    Private Sub fn_Timer()
        lblJam.Text = Now.ToString("HH:mm:ss")
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Timer1.Stop()
        Timer2.Stop()
        Me.Close()
    End Sub
#End Region
End Class