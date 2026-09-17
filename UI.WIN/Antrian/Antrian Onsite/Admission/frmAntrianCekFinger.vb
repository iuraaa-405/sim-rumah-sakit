Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq
Imports System.Diagnostics
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Text

Public Class frmAntrianCekFinger
    Private sKodebooking As String
    Private oPendaftaran As New Admission.clsPendaftaran
    Private oSetKoneksi As New Brigging.clsSetKoneksi
    Private sTanggalRujukan As String = String.Empty
    Private sTanggalRujukan_Date As DateTime = Now
    Private sCatatanBPJS As String = String.Empty
    Private sUserdaftarAnjungan As String = String.Empty
    Private sAlamatAnjungan As String = String.Empty
    Private sasalrujukan As String = String.Empty
    Private sFingerDaftar_UserDaftar As String = String.Empty
    Private sFingerDaftar_Alamat As String = String.Empty
    Private sFingerDaftar_UserName As String = String.Empty
    Private sFingerDaftar_Password As String = String.Empty

    <DllImport("user32.dll", SetLastError:=True, CharSet:=CharSet.Auto)>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As Boolean
    End Function

    Private Function FindWindow(lpClassName As String, lpWindowName As String) As IntPtr
    End Function

    Private Function GetWindowText(hWnd As IntPtr, lpString As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    Public Sub fn_LoadKodebooking(ByVal KodeBooking As String)
        sKodebooking = KodeBooking
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oUserFinger As New Setting.clsUser_Finger

        Dim dsUser = oUserFinger.GetData().FirstOrDefault()

        If dsUser IsNot Nothing Then
            sFingerDaftar_UserDaftar = dsUser.KDUSER
            sFingerDaftar_Alamat = dsUser.ALAMATSIMPAN
            sFingerDaftar_UserName = dsUser.USERNAME
            sFingerDaftar_Password = dsUser.PASSWORD
        End If

        sFind1_NomorRujukan = ""
        fn_LoadPOLI()
        fn_LoadDOKTER()
        fn_LoadFaskes()
        fn_LoadDaftar2()
        fn_LoadIdentitas(sKodebooking)

        FocusWindow()


    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Function fn_LoadIdentitas(ByVal sCode As String) As Boolean
        Try
            Dim oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
            Dim dsAntrian = oSet_Antrian_Simpan.GetData(sCode)

            If dsAntrian IsNot Nothing Then
                txtKODEBOOKING.Text = dsAntrian.KODEBOOKING
                txtANTRIANPOLI.Text = dsAntrian.NOMORANTREAN
                Dim oCustomer As New Reference.clsCustomer
                txtJENISPASIEN.Text = dsAntrian.JENISPASIEN
                txtKARTUBPJS.Text = dsAntrian.NOMORKARTU
                txtNIK.Text = dsAntrian.NIK
                txtKDCUSTOMER.Text = dsAntrian.NORM
                Dim dsCustomer = oCustomer.GetData(dsAntrian.NORM.ToString)
                If dsCustomer IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY

                    If dsCustomer.KDJENISKELAMIN = 0 Then

                    ElseIf dsCustomer.KDJENISKELAMIN = 1 Then

                    Else
                        txtKDCUSTOMER.Text = dsCustomer.KDCUSTOMER
                        'MsgBox("Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Exclamation, Me.Text)

                        Dim frmPerbaikanJenisKelamin As New frmPerbaikanJenisKelamin
                        Try
                            frmPerbaikanJenisKelamin.fn_cariKartu(txtKDCUSTOMER.Text)
                            frmPerbaikanJenisKelamin.ShowDialog(Me)
                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        End Try

                        Dim dsCek = oCustomer.GetData(txtKDCUSTOMER.Text)
                        If dsCek IsNot Nothing Then
                            If dsCek.KDJENISKELAMIN = 0 Then

                            ElseIf dsCek.KDJENISKELAMIN = 1 Then

                            End If
                        Else
                            MsgBox("Data Belum di Perbaiki, Silahkan Perbaiki Data Pasien Terlebih dahulu", MsgBoxStyle.Critical, Me.Text)
                            Exit Function
                        End If
                    End If
                End If

                txtNOTELEPON.Text = dsAntrian.NOHP

                Dim oPoli As New Reference.clsDepartment
                Dim oDokter As New Reference.clsDoctor
                Dim dsPoli = oPoli.GetDataByKodeVclaim(dsAntrian.KODEPOLI)
                If dsPoli IsNot Nothing Then
                    grdKDDEPARTMENT.Text = dsPoli.KDDEPARTMENT

                    If grdKDDEPARTMENT.Text.Contains("MATA") Then
                        lKATARAK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                    Else
                        lKATARAK.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                    End If
                End If
                Dim dsDokter = oDokter.GetDataByKodeVclaim(dsAntrian.KODEDOKTER)
                If dsDokter IsNot Nothing Then
                    grdKDDOCTOR.Text = dsDokter.KDDOCTOR
                End If

                If dsAntrian.JENISKUNJUNGAN = "4" Then
                    sasalrujukan = "2"
                Else
                    sasalrujukan = "1"
                End If

                If dsAntrian.JENISKUNJUNGAN = "3" Then
                    txtSKD.Text = dsAntrian.NOMORREFERENSI

                    Dim oSKD As New Admission.clsSKD
                    Dim dsSkd = oSKD.GetDataKonsul(txtSKD.Text)
                    If dsSkd IsNot Nothing Then
                        If dsSkd.S_PENDAFTARAN_H.CATEGORY = 0 Then
                            lblCatatan.Text = "Kontrol Paska Rawat Jalan"

                            txtNOMORREFERNSI.Text = dsSkd.S_PENDAFTARAN_H.NOMORRUJUKAN
                            Dim Rujukan_date As DateTime = dsSkd.S_PENDAFTARAN_H.DATE_RUJUKAN
                            sTanggalRujukan = Rujukan_date.ToString("yyyy-MM-dd")

                            sTanggalRujukan_Date = dsSkd.S_PENDAFTARAN_H.DATE_RUJUKAN
                        Else
                            lblCatatan.Text = "Kontrol Paska Rawat Inap"

                            txtNOMORREFERNSI.Text = dsSkd.S_PENDAFTARAN_H.NOMORSEP
                            Dim Rujukan_date As DateTime = dsSkd.S_PENDAFTARAN_H.DATE
                            sTanggalRujukan = Rujukan_date.ToString("yyyy-MM-dd")

                            sTanggalRujukan_Date = dsSkd.S_PENDAFTARAN_H.DATE
                        End If

                        sCatatanBPJS = dsSkd.S_PENDAFTARAN_H.CATATAN
                        txtKDDIAGNOSA.Text = dsSkd.S_PENDAFTARAN_H.KDDIAGNOSA
                        grdKDDAFTAR_L2.Text = dsSkd.S_PENDAFTARAN_H.KDDAFTAR_L2
                        grdKDPPK.Text = dsSkd.S_PENDAFTARAN_H.KDPPK
                    Else
                        lCariRujukan.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                        lblCatatan.Text = "Data Nomor Referensi Kosong-Tidak ada data SKD dari SIMRS"

                        If txtKARTUBPJS.Text = "" Then
                            MsgBox("Nomor Kartu BPJS Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
                        Else
                            Dim frmPencarianNomorRujukanMultiRecord As New frmPencarianNomorRujukanMultiRecord
                            Try
                                frmPencarianNomorRujukanMultiRecord.fn_cariKartu(txtKARTUBPJS.Text)
                                frmPencarianNomorRujukanMultiRecord.ShowDialog(Me)
                            Catch oErr As Exception
                                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                            Finally
                                If sFind1_NomorRujukanAsalRujukan = "" Then
                                    MsgBox("Nomor Kunjungan Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                                Else
                                    sasalrujukan = sFind1_NomorRujukanAsalRujukan

                                    txtNOMORREFERNSI.Text = sFind1_NomorRujukan

                                    fn_LoadNomorRujukan(txtNOMORREFERNSI.Text, sasalrujukan)

                                End If
                            End Try
                        End If
                    End If
                Else
                    lblCatatan.Text = "Kunjungan Pertama Paska Rawat Jalan"

                    txtNOMORREFERNSI.Text = dsAntrian.NOMORREFERENSI

                    fn_LoadNomorRujukan(txtNOMORREFERNSI.Text, sasalrujukan)
                End If
            Else
                fn_Reset()
            End If
        Catch oErr As Exception
            MsgBox("Load Identitas : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_Reset()
        sTanggalRujukan = ""
        sCatatanBPJS = ""
        sasalrujukan = ""
        txtKODEBOOKING.ResetText()
        txtANTRIANPOLI.ResetText()
        txtKARTUBPJS.ResetText()
        txtNIK.ResetText()
        txtKDCUSTOMER.ResetText()
        txtNAMAPASIEN.ResetText()
        txtNOTELEPON.ResetText()
        txtNOMORREFERNSI.ResetText()
        grdKDDEPARTMENT.ResetText()
        grdKDDOCTOR.ResetText()
        txtSKD.ResetText()
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
    Private Sub btnFingerUlang_Click(sender As Object, e As EventArgs) Handles btnFingerUlang.Click
        FocusWindow()
    End Sub
    Private Sub btnDaftar_Click(sender As Object, e As EventArgs) Handles btnDaftar.Click
        If fn_GetFingerPrint(txtKARTUBPJS.Text, Now.ToString("yyyy-MM-dd")) = True Then
            If txtKODEBOOKING.Text <> "" Then
                If fn_Validate() = True Then
                    fn_Save()
                End If
            Else
                MsgBox("Kode Booking Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        End If
    End Sub
    Private Sub fn_CariFingerData()
        Try
            If sFingerDaftar_UserDaftar <> "" Then
                For Each proc As Process In Process.GetProcessesByName("After")
                    Try
                        proc.CloseMainWindow()  ' coba tutup dengan cara normal
                        proc.WaitForExit(2000)  ' tunggu 2 detik
                        If Not proc.HasExited Then
                            proc.Kill()         ' kalau belum tertutup, paksa kill
                        End If
                    Catch ex As Exception
                        ' bisa di-log kalau ada error
                    End Try
                Next

                Dim p = Process.Start(sFingerDaftar_Alamat)

                Thread.Sleep(2000) ' tunggu notepad siap

                ' Tunggu Notepad benar-benar siap
                p.WaitForInputIdle()

                ' Aktifkan jendela Notepad
                SetForegroundWindow(p.MainWindowHandle)

                Thread.Sleep(2000) ' tunggu notepad siap

                ' Ketik teks pertama
                SendKeys.SendWait(sFingerDaftar_UserName)
                ' TAB (di notepad akan jadi spasi TAB)
                SendKeys.SendWait("{TAB}")
                ' Ketik teks kedua
                SendKeys.SendWait(sFingerDaftar_Password)

                SendKeys.SendWait("{TAB}")
                SendKeys.SendWait("{ENTER}")

                Thread.Sleep(1000) ' tunggu notepad siap

                SendKeys.SendWait(txtKARTUBPJS.Text)
            Else
                MsgBox("User Finger Tidak diTemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Finger : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Function"
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDCUSTOMER.Text = "" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "0000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            ElseIf txtKDCUSTOMER.Text = "00000000000" Then
                MsgBox("Rekam medis tidak boleh " & txtKDCUSTOMER.Text & " Silahkan perbaiki Nomor Rekam Medis", MsgBoxStyle.Exclamation, Me.Text)

                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired

                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtKODEBOOKING.Text = "" Then
                txtKODEBOOKING.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKODEBOOKING.ErrorText = Statement.ErrorRequired
                txtKODEBOOKING.Focus()
                MsgBox("Kode Booking Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If txtKDCUSTOMER.Text = String.Empty Then
                txtKDCUSTOMER.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCUSTOMER.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor Rekam Medis Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNIK.Text = String.Empty Then
                txtNIK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNIK.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor NIK/KTP Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKARTUBPJS.Text = String.Empty Then
                txtKARTUBPJS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKARTUBPJS.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor Kartu BPJS Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDCUSTOMER.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired
                MsgBox("Poli Tujuan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired
                MsgBox("Nama Dokter Kosong", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOTELEPON.Text = String.Empty Then
                txtNOTELEPON.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOTELEPON.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor Telepon Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtNOTELEPON.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORREFERNSI.Text = String.Empty Then
                txtNOMORREFERNSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORREFERNSI.ErrorText = Statement.ErrorRequired
                MsgBox("Nomor Referensi Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtNOMORREFERNSI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPPK.Text = "" Then
                grdKDPPK.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPPK.ErrorText = Statement.ErrorRequired
                grdKDPPK.Focus()
                MsgBox("Kode Faskes Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If txtKDDIAGNOSA.Text = "" Then
                txtKDDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDDIAGNOSA.ErrorText = Statement.ErrorRequired
                txtKDDIAGNOSA.Focus()
                MsgBox("Kode Diagnosa Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If grdKDDAFTAR_L2.Text = "" Then
                grdKDDAFTAR_L2.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDAFTAR_L2.ErrorText = Statement.ErrorRequired
                grdKDDAFTAR_L2.Focus()
                MsgBox("Jenis Peserta Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If sTanggalRujukan = "" Then
                txtNOMORREFERNSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORREFERNSI.ErrorText = Statement.ErrorRequired
                txtNOMORREFERNSI.Focus()
                MsgBox("Tanggal Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If sasalrujukan = "" Then
                txtNOMORREFERNSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORREFERNSI.ErrorText = Statement.ErrorRequired
                txtNOMORREFERNSI.Focus()
                MsgBox("Asal Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If sFingerDaftar_UserDaftar = "" Then
                txtNOMORREFERNSI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORREFERNSI.ErrorText = Statement.ErrorRequired
                txtNOMORREFERNSI.Focus()
                MsgBox("User Daftar Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            Dim oPOLI As New Reference.clsDepartment
            If oPOLI.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI <> "IGD" Then
                Dim dsKunjunganSama = oPendaftaran.GetDataByRMUnitDate(txtKDCUSTOMER.Text, grdKDDEPARTMENT.EditValue, Now)
                If dsKunjunganSama IsNot Nothing Then
                    If MsgBox("Pasien hari ini sudah berkunjung ke Poli yg sama dengan No Pendaftaran " & dsKunjunganSama.KDPENDAFTARAN & " Apakah Akan lanjut Transaksi ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then
                        CetakRegister(dsKunjunganSama.KDPENDAFTARAN)
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim KodeKelasrawat As String = String.Empty

            Dim oKelas As New Reference.clsKelasRawat
            Dim dskelas = oKelas.GetData("KELASRAWAT_0000000004")
            If dskelas IsNot Nothing Then
                KodeKelasrawat = "KELASRAWAT_0000000004"
            Else
                Dim dskelas3 = oKelas.GetDataByKode("3")
                If dskelas3 IsNot Nothing Then
                    KodeKelasrawat = dskelas3.KDKELASRAWAT
                Else
                    MsgBox("Kode Kelas 3 Tidak diTemukan", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Function
                    fn_Save = False
                End If
            End If

            Dim oCustomer As New Reference.clsCustomer
            Dim jsonRequest As String = String.Empty
            Dim jsonResponse As String = String.Empty
            Dim INFORMASIPRB As String = String.Empty
            Dim sNOMORSEP_TERBIT As String = String.Empty
            Dim Pasienbaru As Boolean = False

            Dim dsCustomer = oCustomer.GetData(txtKDCUSTOMER.Text)

            If dsCustomer Is Nothing Then
                MsgBox("Pasien Tidak diTemukan di SIMRS", MsgBoxStyle.Exclamation, Me.Text)
                Exit Function
                fn_Save = False
            Else
                If dsCustomer.KDCUSTOMER_LAMA = "" Then
                    If dsCustomer.DATECREATED.ToString("yyyyMMdd") = Now.ToString("yyyyMMdd") Then
                        Pasienbaru = True
                    End If
                End If
            End If

            ' ***** SAVE SEP V2

            Dim uTime As Integer = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds

            jsonRequest = fn_jsonRequestInsertSEPV2()

            If jsonRequest <> "" Then
                jsonResponse = fn_CreateSEPv2(jsonRequest, uTime)
                If jsonResponse <> "" Then
                    Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(jsonResponse, sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                    Try
                        jsonResponse = DataDecrypt.ToString()
                        sNOMORSEP_TERBIT = DataDecrypt.Item("sep")("noSep").ToString()
                    Catch oErr As Exception
                        MsgBox("Decript " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                        fn_Save = False
                        Exit Function
                    End Try

                    Dim dsSEP = oPendaftaran.GetDataNomorSEP(sNOMORSEP_TERBIT)
                    If dsSEP IsNot Nothing Then
                        MsgBox("Rujukan Internal" & vbCrLf & "Sep Sudah Ada pada tanggal " & dsSEP.DATE.ToString("dd-MM-yyyy"), MsgBoxStyle.Exclamation, Me.Text)
                        CetakRegister(dsSEP.KDPENDAFTARAN)
                        fn_Save = False
                        Exit Function
                    End If
                    INFORMASIPRB = DataDecrypt.Item("sep")("informasi")("prolanisPRB").ToString()
                Else
                    fn_Save = False
                    Exit Function
                End If
            Else
                fn_Save = False
                Exit Function
            End If

            ' ****************************************************

            ' ***** HEADER *****
            Dim ds = oPendaftaran.GetStructureHeader
            With ds
                .KDPENDAFTARAN = ""
                .KDPENDAFTARAN_AWAL = ""
                .NOMORSEP = sNOMORSEP_TERBIT
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text.ToString.Trim.ToUpper
                .KARTUBPJS = txtKARTUBPJS.Text.ToString.Trim.ToUpper
                .KTP = txtNIK.Text.ToString.Trim.ToUpper
                .KDDAFTAR_L1 = oPendaftaran.Daftar_L1_Default
                .KDDAFTAR_L2 = grdKDDAFTAR_L2.EditValue
                .KDDAFTAR_L3 = oPendaftaran.Daftar_L3_Default
                .KDDAFTAR_L4 = oPendaftaran.Daftar_L4_Default
                .KDDAFTAR_L5 = oPendaftaran.Daftar_L5_Default
                .KDDAFTAR_L6 = "DAFTAR_L6_0000000001"
                .CATEGORY = 0
                .STATUSDAFTAR = 0
                .KDUSER = sFingerDaftar_UserDaftar
                .ISEKSEKUTIF = False
                .ISKATARAK = chkKatarak.Checked
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .ASALRUJUKAN = IIf(sasalrujukan = "1", 0, 1)
                .DATE_RUJUKAN = sTanggalRujukan_Date
                .NOMORRUJUKAN = txtNOMORREFERNSI.Text
                .NOMORSKDP = txtSKD.Text
                .KDDOCTOR_SKD = grdKDDOCTOR.EditValue
                .KDDIAGNOSA = txtKDDIAGNOSA.Text
                .NOMORTELEPON = txtNOTELEPON.Text.ToString.Trim.ToUpper
                .CATATAN = sCatatanBPJS
                .ISOFFLINE = False
                .KDCOB = oPendaftaran.Daftar_COB_Default
                .KDPPK = grdKDPPK.EditValue
                .KDKELASRAWAT = KodeKelasrawat
                .REQUEST = jsonRequest
                .RESPON = jsonResponse
                .ISCOB = False
                .JAMINAN_ISLAKALANTAS = False
                .JAMINAN_PENJAMIN_PENJAMIN1 = False
                .JAMINAN_PENJAMIN_PENJAMIN2 = False
                .JAMINAN_PENJAMIN_PENJAMIN3 = False
                .JAMINAN_PENJAMIN_PENJAMIN4 = False
                .JAMINAN_PENJAMIN_TGLKEJADIAN = Now
                .JAMINAN_PENJAMIN_KETERANGAN = ""
                .JAMINAN_PENJAMIN_SUPLESI_ISSUPLESI = False
                .JAMINAN_PENJAMIN_SUPLESI_NOSEPSUPLESI = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDPROPONSI = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKABUPATEN = ""
                .JAMINAN_PENJAMIN_SUPLESI_LOKASILAKA_KDKECAMATAN = ""
                .INFORMASIPRB = INFORMASIPRB
                .CETAK = 0
                .TERSEDIA = 0
                .KDSHIFT = sSHIFT
                .KDUPDATE_APLICARE = ""
                .KODEBOOKING = txtKODEBOOKING.Text.ToString.Trim.ToUpper
                .KDBOOKING = txtANTRIANPOLI.Text
                .NAIKRANAP = ""
                .PEMBIAYAAN = ""
                .PENANGGUNGJAWAB = ""
                .TUJUANKUNJUNGAN = IIf(txtSKD.Text = "", "Normal", "Konsul Dokter")
                .FLAGPROCEDURE = ""
                .KDPENUNJANG = ""
                .ASESMENPELAYANAN = IIf(txtSKD.Text = "", "", "Tujuan Kontrol")
            End With

            '***** Kunjungan *****
            Dim dsKunjungan = oPendaftaran.GetStructureHeader_Kunjungan
            With dsKunjungan
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = Now
                .KDKUNJUNGAN = ""
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .ALAMAT = ""
                .KDPENJAMIN = dsCustomer.KDPENJAMIN
                .KDKESATUAN = dsCustomer.KDKESATUAN
                .KDPANGKAT = dsCustomer.KDPANGKAT
                .KDGOLONGAN = dsCustomer.KDGOLONGAN
                .KDPENDIDIKAN = dsCustomer.KDPENDIDIKAN
                .KDPEKERJAAN = dsCustomer.KDPEKERJAAN
                .KDPERUSAHAAN = dsCustomer.KDPERUSAHAAN
                .KDSTATUSKAWIN = dsCustomer.KDSTATUSKAWIN
                .NAMAKELUARGA = ""
                .KDSTATUSKELUARGA = dsCustomer.KDSTATUSKELUARGA
                .KDUSER = sFingerDaftar_UserDaftar
                .TERSEDIA = 0
                .KDUPDATE_APLICARE = ""
            End With

            '***** Penanggung Jawab *****
            Dim dsPenanggunjawab = oPendaftaran.GetStructureHeader_PenanggungJawab
            With dsPenanggunjawab
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENDAFTARAN = ds.KDPENDAFTARAN
                .NAMA = ""
                .HUBUNGAN = ""
                .ALAMAT = ""
                .NOMORTELEPON = ""
            End With

            Dim dsIdentitas = oPendaftaran.GetStructureHeader_Identitas

            If dsCustomer IsNot Nothing Then
                '***** Identitas *****
                With dsIdentitas
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .DATE = Now
                    .CATEGORY = 0
                    .KDKUNJUNGAN = dsKunjungan.KDKUNJUNGAN
                    .KDPENDAFTARAN = ds.KDPENDAFTARAN
                    .PENJAMIN = oPendaftaran.Daftar_L1_Default
                    .KDCUSTOMER = txtKDCUSTOMER.Text
                    .NAMAPASIEN = txtNAMAPASIEN.Text
                    .ALAMAT = dsCustomer.ALAMAT & " KELURAHAN : " & IIf(dsCustomer.M_KELURAHAN.MEMO = "DEFAULT", "-", dsCustomer.M_KELURAHAN.MEMO) & " KECAMATAN : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.MEMO & " Kode Pos : " & dsCustomer.M_KELURAHAN.KODEPOS & " KABUPATEN/KOTA : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO & " PROPINSI : " & dsCustomer.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO & " NO TELEPON : " & dsCustomer.PHONE
                    .DOKTER = grdKDDOCTOR.Text
                    .TUJUAN = grdKDDEPARTMENT.Text
                    .KDDOKTER = grdKDDOCTOR.EditValue
                    .KDTUJUAN = grdKDDEPARTMENT.EditValue
                    .TANGGALLAHIR = dsCustomer.TANGGALLAHIR
                    .JENISKELAMIN = IIf(dsCustomer.KDJENISKELAMIN = 0, "P", "L")
                    .NIK = txtNIK.Text.ToString.Trim.ToUpper
                    .TEMPATLAHIR = dsCustomer.TEMPATLAHIR
                    .AGAMA = dsCustomer.M_AGAMA.MEMO
                    .PANGKAT = dsCustomer.M_PANGKAT.MEMO
                    .NRP = dsCustomer.NRP
                    .KESATUAN = dsCustomer.M_KESATUAN.MEMO
                    .NOMORTELEPON = txtNOTELEPON.Text
                    .PENDIDIKAN = dsCustomer.KDPENDIDIKAN
                    .SUKU = dsCustomer.M_SUKU.MEMO
                    .USIA = oPendaftaran.GetUmurPasien(Now, dsCustomer.TANGGALLAHIR)
                    .NOMORSEP = sNOMORSEP_TERBIT
                    .KELASPELAYANAN = "KELASRAWAT_0000000004"
                    .KARTUBPJS = txtKARTUBPJS.Text
                    .STATUS_KAWIN = dsCustomer.KDSTATUSKAWIN
                    .HUBUNGAN = ""
                    .HUBUNGAN_NAMA = ""
                    .HUBUNGAN_PENDIDIKAN = ""
                    .HUBUNGAN_PEKERJAAN = ""
                    .NOMORASURANSILAIN = ""
                    .KDGOLONGANDARAH = dsCustomer.KDGOLONGANDARAH
                    .KDDIAGNOSA = txtKDDIAGNOSA.Text
                    .KDPENJAMIN = ""
                    .KDPERUSAHAAN = ""
                    Dim oDiagnosa As New Reference.clsDiagnosa
                    Dim dsDiagnosa = oDiagnosa.GetData(txtKDDIAGNOSA.Text)
                    If dsDiagnosa IsNot Nothing Then
                        .DIAGNOSA = dsDiagnosa.MEMO
                    Else
                        .DIAGNOSA = ""
                    End If

                    .KDUSER = sFingerDaftar_UserDaftar
                    .HAKKELAS = ""
                    .URL_SIGNATURE = ""
                    .NAMA_TANDATANGAN = ""
                    .FASKES = grdKDPPK.Text
                End With
            Else
                dsIdentitas = Nothing
            End If

            Dim sKDPENDAFTARAN As String = ""

            'Chekin

            frmErmList.fn_TaskID(txtKODEBOOKING.Text, 3)

            sKDPENDAFTARAN = oPendaftaran.InsertData(ds, dsKunjungan, Nothing, dsIdentitas)

            If sKDPENDAFTARAN = "" Then
                fn_Save = False
            Else
                fn_Save = True
                CetakRegister(sKDPENDAFTARAN)
            End If

            If fn_Save = True Then
                Try
                    oCustomer.UpdateNomorTelepon(dsCustomer.KDCUSTOMER, txtNOTELEPON.Text, txtNIK.Text.Trim)

                    Dim oGrouperRawatJalan As New Grouper.clsR_Identitas_Grouper
                    Dim dsDataGrouper = oGrouperRawatJalan.GetStructureHeader
                    Dim dsCariNosep = oGrouperRawatJalan.GetDataByNoRec(ds.KDPENDAFTARAN)

                    With dsDataGrouper
                        If dsCariNosep Is Nothing Then
                            .kodegrouper = 0
                            .datecreated = ds.DATECREATED
                        Else
                            .kodegrouper = dsCariNosep.kodegrouper
                            .datecreated = dsCariNosep.datecreated
                        End If
                        .dateupdated = ds.DATEUPDATED
                        .jeniskelompokpasien = ds.M_DAFTAR_L1.MEMO

                        If dsCariNosep Is Nothing Then
                            .statusenabled = "1"
                        Else
                            .statusenabled = dsCariNosep.statusenabled
                        End If

                        .norec = ds.KDPENDAFTARAN
                        .nostruklastfk = ds.KDPENDAFTARAN_AWAL
                        .jnsPelayanan = IIf(ds.CATEGORY = 0, "R.Jalan", "R.Inap")
                        .kelasRawat = "KELAS 3"
                        .noRm = ds.KDCUSTOMER
                        .nama = ds.M_CUSTOMER.NAME_DISPLAY
                        .noKartu = ds.KARTUBPJS
                        .asalrujukan = IIf(ds.ASALRUJUKAN = 0, 1, 2)
                        .noSep = ds.NOMORSEP
                        .noRujukan = ds.NOMORRUJUKAN
                        .Faskes = ds.M_PPK.KODEFASKES & " " & ds.M_PPK.MEMO
                        .dpjpkodevclaim = ds.M_DOCTOR.VCLAIM_KDDPJP
                        .dpjp = ds.M_DOCTOR.NAME_DISPLAY
                        .polikodevclaim = ds.M_DEPARTMENT.VCLAIM_KODEPOLI
                        .poli = ds.M_DEPARTMENT.NAME_DISPLAY
                        .catatan = ds.CATATAN
                        .infromasiprb = ds.INFORMASIPRB
                        .peserta = ds.M_DAFTAR_L2.MEMO
                        .cob = ds.M_COB.MEMO
                        .nomortelepon = ds.NOMORTELEPON
                        .noregistrasi = ds.KDPENDAFTARAN
                        .tglPlgSep = ds.DATE
                        .tglSep = ds.DATE
                        .tgl_lahir = ds.M_CUSTOMER.TANGGALLAHIR
                        .gender = IIf(ds.M_CUSTOMER.KDJENISKELAMIN = 0, "P", "L")
                        .diagnosaawal = ds.M_DIAGNOSA.MEMO
                        If dsCariNosep Is Nothing Then
                            .status = ""
                        Else
                            .status = dsCariNosep.status
                        End If
                        .statuspasien = IIf(ds.DATE.ToString("ddMMyyyy") = ds.M_CUSTOMER.DATECREATED.ToString("ddMMyyy"), IIf(ds.M_CUSTOMER.KDCUSTOMER_LAMA = "", "BARU", "LAMA"), "LAMA")
                        .alamat = ds.M_CUSTOMER.ALAMAT

                        If dsCariNosep Is Nothing Then
                            .tglpulang = ds.DATE
                        Else
                            .tglpulang = dsCariNosep.tglpulang
                        End If
                        .jsonpost = ""
                        .kduser = sFingerDaftar_UserDaftar
                        .ruangan = IIf(ds.CATEGORY = 0, "", ds.M_DEPARTMENT.NAME_DISPLAY)
                        .pangkat = ds.M_CUSTOMER.M_PANGKAT.MEMO
                        .kesatuan = ds.M_CUSTOMER.M_KESATUAN.MEMO
                        .pendidikan = ds.M_CUSTOMER.KDPENDIDIKAN
                        .statusmenikah = ds.M_CUSTOMER.KDSTATUSKAWIN
                        .propinsi = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.M_PROPINSI.MEMO
                        .kabupaten = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.M_KABUPATEN.MEMO
                        .kecamatan = ds.M_CUSTOMER.M_KELURAHAN.M_KECAMATAN.MEMO
                        .kelurahan = ds.M_CUSTOMER.M_KELURAHAN.MEMO
                    End With

                    If dsCariNosep Is Nothing Then
                        If oGrouperRawatJalan.InsertData(dsDataGrouper) = False Then
                            MsgBox("Simpan Data Grouper Gagal", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Else
                        If oGrouperRawatJalan.UpdateData(dsDataGrouper) = False Then
                            MsgBox("Simpan Data Grouper Gagal", MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    End If
                Catch oErr As Exception
                    MsgBox("Simpan Data Grouper" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDPENDAFTARAN As String)
        Try
            sCetakSEP = False

            Dim ds = oPendaftaran.GetData(KDPENDAFTARAN)

            If ds IsNot Nothing Then
                sUmur = oPendaftaran.GetUmurPasien(ds.DATE, ds.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraAntrianPendaftaran_88_Kecil
                rpt.BindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.PrintDialog()

                If sCetakSEP = True Then
                    oPendaftaran.UpdateCetak(KDPENDAFTARAN)
                End If
            End If

        Catch oErr As Exception
            MsgBox("Cetak Register" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_GetFingerPrint(ByVal Kartu As String, ByVal TglPel As String) As Boolean
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.GetFingerPrint(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Kartu, TglPel)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_GetFingerPrint = True
                        'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))

                        'MsgBox(CodeResponse & " - " & DataDecrypt("status").ToString(), MsgBoxStyle.Exclamation, Me.Text)
                    Else
                        fn_GetFingerPrint = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_GetFingerPrint = False
                    MsgBox("Kosong Koneksi", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_GetFingerPrint = False
                MsgBox("Cari Finger Print Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_GetFingerPrint = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_jsonRequestInsertSEPV2() As String
        Try
            Dim jsonRequest As String = String.Empty
            Dim oFaskes As New Reference.clsPPK
            Dim oPoli As New Reference.clsDepartment
            Dim oDoctor As New Reference.clsDoctor
            Dim oDepartment As New Reference.clsDepartment

            jsonRequest = " { "
            jsonRequest &= """request"" :  { "
            jsonRequest &= """t_sep"": { "
            jsonRequest &= """noKartu"": """ & txtKARTUBPJS.Text.Trim.ToUpper & ""","
            jsonRequest &= """tglSep"": """ & Now.ToString("yyyy-MM-dd") & """, "
            jsonRequest &= """ppkPelayanan"": """ & sPPKPELAYANAN & """, "
            jsonRequest &= """jnsPelayanan"": """ & "2" & """, "
            jsonRequest &= """klsRawat"": { "
            jsonRequest &= """klsRawatHak"": """ & "3" & """, "
            jsonRequest &= """klsRawatNaik"": """ & "" & """, "
            jsonRequest &= """pembiayaan"": """ & "" & """, "
            jsonRequest &= """penanggungJawab"": """ & "" & """ "
            jsonRequest &= "}, "
            jsonRequest &= """noMR"": """ & txtKDCUSTOMER.Text.ToString & """, "
            jsonRequest &= """rujukan"": { "
            jsonRequest &= """asalRujukan"": """ & sasalrujukan & """, "
            jsonRequest &= """tglRujukan"": """ & sTanggalRujukan & """, "
            jsonRequest &= """noRujukan"": """ & txtNOMORREFERNSI.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """ppkRujukan"": """ & oFaskes.GetData(grdKDPPK.EditValue).KODEFASKES & """ "
            jsonRequest &= "}, "
            jsonRequest &= """catatan"": """ & sCatatanBPJS & """, "
            jsonRequest &= """diagAwal"": """ & txtKDDIAGNOSA.Text & """, "
            jsonRequest &= """poli"": { "
            jsonRequest &= """tujuan"": """ & oDepartment.GetData(grdKDDEPARTMENT.EditValue).VCLAIM_KODEPOLI & """, "
            jsonRequest &= """eksekutif"": """ & 0 & """ "
            jsonRequest &= "}, "
            jsonRequest &= """cob"": { "
            jsonRequest &= """cob"": """ & 0 & """ "
            jsonRequest &= "}, "
            jsonRequest &= """katarak"": { "
            jsonRequest &= """katarak"": """ & IIf(chkKatarak.Checked = False, 0, 1) & """ "
            jsonRequest &= "}, "
            jsonRequest &= """jaminan"": { "
            jsonRequest &= """lakaLantas"": """ & 0 & """, "
            jsonRequest &= """penjamin"": { "
            jsonRequest &= """tglKejadian"": """ & "" & """, "
            jsonRequest &= """keterangan"": """ & "" & """, "
            jsonRequest &= """suplesi"": { "
            jsonRequest &= """suplesi"": """ & 0 & """, "
            jsonRequest &= """noSepSuplesi"": """ & "" & """, "
            jsonRequest &= """lokasiLaka"": { "
            jsonRequest &= """kdPropinsi"": """ & "" & """, "
            jsonRequest &= """kdKabupaten"": """ & "" & """, "
            jsonRequest &= """kdKecamatan"": """ & "" & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "}, "
            jsonRequest &= """tujuanKunj"": """ & IIf(txtSKD.Text = "", "0", "2") & """, "
            jsonRequest &= """flagProcedure"": """ & "" & """, "
            jsonRequest &= """kdPenunjang"": """ & "" & """, "
            jsonRequest &= """assesmentPel"": """ & IIf(txtSKD.Text = "", "", "5") & """, "
            jsonRequest &= """skdp"": { "
            jsonRequest &= """noSurat"": """ & txtSKD.Text & """, "
            jsonRequest &= """kodeDPJP"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """ "
            jsonRequest &= "}, "
            jsonRequest &= """dpjpLayan"": """ & oDoctor.GetData(grdKDDOCTOR.EditValue).VCLAIM_KDDPJP & """, "
            jsonRequest &= """noTelp"": """ & txtNOTELEPON.Text.ToString.Trim.ToUpper & """, "
            jsonRequest &= """user"": """ & sFingerDaftar_UserDaftar & """ "
            jsonRequest &= "} "
            jsonRequest &= "} "
            jsonRequest &= "} "
            fn_jsonRequestInsertSEPV2 = jsonRequest
        Catch oErr As Exception
            fn_jsonRequestInsertSEPV2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CreateSEPv2(ByVal Request As String, ByVal uTime As Integer) As String
        Dim setKoneksi As String = String.Empty

        Try
            If sVclaim_ConsId <> "" Then
                Dim DATETEIM As DateTime = Now

                Dim TanggalEstimasi As DateTime = Now

                Dim dsSetKoneksi = oSetKoneksi.InsertSEPv2(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, Request)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CreateSEPv2 = allData("response")
                    Else
                        fn_CreateSEPv2 = ""
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        Exit Function
                    End If
                Else
                    fn_CreateSEPv2 = ""
                    MsgBox("Insert SEP Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_CreateSEPv2 = ""
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_CreateSEPv2 = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & setKoneksi & "-" & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadNomorRujukan(ByVal sNOMORRUJUKAN As String, ByVal asalrujukan As String)
        Try
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariRujukan(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, sNOMORRUJUKAN, IIf(asalrujukan = "1", 0, 1))

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)
                        Dim CodeResponse As String = String.Empty
                        Dim messageResponse As String = String.Empty

                        CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                        messageResponse = allData("metaData")("message").ToString

                        If CodeResponse = "200" Then
                            Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime))

                            Dim Rujukan_date As DateTime = DataDecrypt("rujukan")("tglKunjungan").ToString()
                            sTanggalRujukan = Rujukan_date.ToString("yyyy-MM-dd")
                            sTanggalRujukan_Date = CDate(DataDecrypt("rujukan")("tglKunjungan").ToString())

                            Dim hari As Integer = DateDiff(DateInterval.Day, Rujukan_date, Today())
                            If hari > 90 Then
                                MsgBox("Surat Rujukan Nomor : " & txtNOMORREFERNSI.Text & " masa berlaku Habis, Maksimal 3(tiga) bulan dari tanggal rujukan Silahkan ke Faskes Perujuk untuk Perbaharui Rujukan", MsgBoxStyle.OkOnly, "Perhatian !!!")
                                fn_Reset()
                            Else
                                sCatatanBPJS = DataDecrypt("rujukan")("keluhan").ToString()

                                InsertDiagnosa(DataDecrypt("rujukan")("diagnosa")("kode").ToString(), DataDecrypt("rujukan")("diagnosa")("kode").ToString() & " - " & DataDecrypt("rujukan")("diagnosa")("nama").ToString())

                                If DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString() <> "" Then
                                    InsertJenisPeserta(DataDecrypt("rujukan")("peserta")("jenisPeserta")("kode").ToString(), DataDecrypt("rujukan")("peserta")("jenisPeserta")("keterangan").ToString())
                                End If
                                If DataDecrypt("rujukan")("provPerujuk")("nama").ToString() <> "" Then
                                    InsertPPK(DataDecrypt("rujukan")("provPerujuk")("kode").ToString(), DataDecrypt("rujukan")("provPerujuk")("nama").ToString())
                                End If
                            End If
                        Else
                            MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                        End If
                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                    End Try
                Else
                    MsgBox("Pencarian Rujukan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function InsertDiagnosa(ByVal sKDDIAGNOSA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oDiagnosa As New Reference.clsDiagnosa

            InsertDiagnosa = True

            If oDiagnosa.IsExist(sKDDIAGNOSA) Then
                txtKDDIAGNOSA.Text = sKDDIAGNOSA
            Else
                Dim ds = oDiagnosa.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDIAGNOSA = sKDDIAGNOSA
                    .MEMO = sMEMO.ToString.Trim.ToUpper
                    .ISDEFAULT = IIf(oDiagnosa.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertDiagnosa = oDiagnosa.InsertData(ds)

                'fn_LoadDiganosa()
                txtKDDIAGNOSA.Text = sKDDIAGNOSA
            End If

        Catch ex As Exception
            InsertDiagnosa = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertPPK(ByVal sKODEFASKES As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oPPK As New Reference.clsPPK

            InsertPPK = True

            If oPPK.IsExistKodeFaskes(sKODEFASKES) Then
                grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
            Else
                Dim ds = oPPK.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDPPK = ""
                    .JENISFASKES = 0
                    .KODEFASKES = sKODEFASKES
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oPPK.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                    .JENISFASKES = sasalrujukan
                End With

                InsertPPK = oPPK.InsertData(ds)

                fn_LoadFaskes()
                grdKDPPK.EditValue = oPPK.GetDataKodeFaskes(sKODEFASKES).KDPPK
            End If
        Catch ex As Exception
            InsertPPK = False
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function InsertJenisPeserta(ByVal sKDJENISPESERTA As String, ByVal sMEMO As String) As Boolean
        Try
            Dim oJenisPeserta As New Reference.clsDaftar_L2

            InsertJenisPeserta = True

            If oJenisPeserta.IsExist(sKDJENISPESERTA) Then
                grdKDDAFTAR_L2.EditValue = sKDJENISPESERTA
            Else
                Dim ds = oJenisPeserta.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDDAFTAR_L2 = sKDJENISPESERTA
                    .MEMO = sMEMO
                    .ISDEFAULT = IIf(oJenisPeserta.CheckDefault(0, False) = False, True, False)
                    .ISACTIVE = True
                End With

                InsertJenisPeserta = oJenisPeserta.InsertData(ds)

                fn_LoadDaftar2()
                grdKDDAFTAR_L2.EditValue = sKDJENISPESERTA
            End If

        Catch ex As Exception
            InsertJenisPeserta = False
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Look Up"
    Private Sub fn_LoadPOLI()
        Dim oPOLI As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oPOLI.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Poli" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDOKTER()
        Dim oDOKTER As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOKTER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFaskes()
        Dim oPPK As New Reference.clsPPK
        Try
            grdKDPPK.Properties.DataSource = oPPK.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDPPK.Properties.ValueMember = "KDPPK"
            grdKDPPK.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDaftar2()
        Dim oDAFTAR_L2 As New Reference.clsDaftar_L2
        Try
            grdKDDAFTAR_L2.Properties.DataSource = oDAFTAR_L2.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L2.Properties.ValueMember = "KDDAFTAR_L2"
            grdKDDAFTAR_L2.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        If txtKARTUBPJS.Text = "" Then
            MsgBox("Nomor Kartu BPJS Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim frmPencarianNomorRujukanMultiRecord As New frmPencarianNomorRujukanMultiRecord
            Try
                frmPencarianNomorRujukanMultiRecord.fn_cariKartu(txtKARTUBPJS.Text)
                frmPencarianNomorRujukanMultiRecord.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If sFind1_NomorRujukanAsalRujukan = "" Then
                    MsgBox("Nomor Kunjungan Belum di Pilih", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    sasalrujukan = sFind1_NomorRujukanAsalRujukan

                    txtNOMORREFERNSI.Text = sFind1_NomorRujukan

                    fn_LoadNomorRujukan(txtNOMORREFERNSI.Text, sasalrujukan)

                End If
            End Try
        End If
    End Sub
    Private Sub FocusWindow()
        If Not grdKDDEPARTMENT.Text.Contains("ANAK") Then
            Dim p = Process.GetProcessesByName("After").FirstOrDefault()
            If p IsNot Nothing Then
                If p.MainWindowHandle <> IntPtr.Zero Then
                    SetForegroundWindow(p.MainWindowHandle) ' Panggil API SetForegroundWindow (declare seperti sebelumnya)

                    Threading.Thread.Sleep(500) ' tunggu notepad siap

                    SendKeys.SendWait("^a")     ' Ctrl + A (Select All)
                    Threading.Thread.Sleep(200) ' Tunggu sedikit agar sempat select
                    SendKeys.SendWait("{DEL}")  ' Delete

                    SendKeys.SendWait(txtKARTUBPJS.Text)
                Else
                    ' Jika MainWindowHandle 0, mungkin proses belum punya window - kamu bisa gunakan AppActivate dengan Id
                    AppActivate(p.Id)
                End If
            Else
                fn_CariFingerData()
            End If
        End If
    End Sub
#End Region
End Class