Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmLembarUjiFungsi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_UJIFUNGSIRM As New Digital.clsDigital_RJ_UJIFUNGSIRM
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtTmpTglLahir.Text = dsPendaftaran.TEMPATLAHIR.Trim & ", " & dsPendaftaran.TANGGALLAHIR
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
            txtJam.Text = DateTime.Now.ToString("HH:mm")
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
        Else
            txtRehabilitasi.Text = "Medik"
            txtCoding.ResetText()
            txtNamaPasien.ResetText()
            txtTmpTglLahir.ResetText()
            txtKesatuan.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtNoRM.ResetText()
            txtTanggal.ResetText()
            txtJam.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_LembarUjiFungsi.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select

    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        txtRehabilitasi.Properties.ReadOnly = Status
        txtCoding.Properties.ReadOnly = Status
        txtTanggal.Properties.ReadOnly = Status
        txtJam.Properties.ReadOnly = Status
        txtDKFUNGSIONAL.Properties.ReadOnly = Status
        txtDKMEDIS.Properties.ReadOnly = Status
        txtINSTRUMENUJIFUNGSI.Properties.ReadOnly = Status
        txtHASILYGDIDAPAT.Properties.ReadOnly = Status
        txtKESIMPULAN.Properties.ReadOnly = Status
        txtREKOMENDASI.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        txtRehabilitasi.Text = "Medik"
        txtCoding.ResetText()
        txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
        txtJam.Text = DateTime.Now.ToString("HH:mm")
        txtDKFUNGSIONAL.ResetText()
        txtDKMEDIS.ResetText()
        txtINSTRUMENUJIFUNGSI.ResetText()
        txtHASILYGDIDAPAT.ResetText()
        txtKESIMPULAN.ResetText()
        txtREKOMENDASI.ResetText()
        txtCATATAN.ResetText()

        fn_LoadAsessmenAwalIRM(txtNoRegister.Text)

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_UJIFUNGSIRM.GetData(txtNoRegister.Text)

            With ds
                txtRehabilitasi.Text = .REHABILITASI
                txtCoding.Text = .CODING
                txtTanggal.Text = .TANGGAL
                txtJam.Text = .JAM
                txtDKFUNGSIONAL.Text = .DKFUNGSIONAL
                txtDKMEDIS.Text = .DKMEDIS
                txtINSTRUMENUJIFUNGSI.Text = .INSTRUMENUJIFUNGSI
                txtHASILYGDIDAPAT.Text = .HASILYGDIDAPAT
                txtKESIMPULAN.Text = .KESIMPULAN
                txtREKOMENDASI.Text = .REKOMENDASI
                txtCATATAN.Text = .CATATAN
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenAwalIRM(ByVal Parameter As String)
        Dim oAsessmenAwal As New Digital.clsDigital_RJ_ASSREHABMEDIK
        Dim dsAsessmenAwal = oAsessmenAwal.GetData(Parameter)

        If dsAsessmenAwal IsNot Nothing Then
            txtHASILYGDIDAPAT.Text = dsAsessmenAwal.SDIGITAL54
            txtKESIMPULAN.Text = dsAsessmenAwal.SDIGITAL71
            txtINSTRUMENUJIFUNGSI.Text = dsAsessmenAwal.SDIGITAL73
        Else
            MsgBox("Assemen Awal Pasien Rehabilitasi Medik belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_UJIFUNGSIRM.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_UJIFUNGSIRM.GetData(txtNoRegister.Text).DATECREATED
                    .DATE = oS_DIGITAL_RJ_UJIFUNGSIRM.GetData(txtNoRegister.Text).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    .DATE = Now
                End Try
                .DATEUPDATED = Now

                .REHABILITASI = txtRehabilitasi.Text.Trim
                .CODING = txtCoding.Text.Trim
                .TANGGAL = txtTanggal.Text.Trim
                .JAM = txtJam.Text.Trim
                .DKFUNGSIONAL = txtDKFUNGSIONAL.Text.Trim
                .DKMEDIS = txtDKMEDIS.Text.Trim
                .INSTRUMENUJIFUNGSI = txtINSTRUMENUJIFUNGSI.Text.Trim
                .HASILYGDIDAPAT = txtHASILYGDIDAPAT.Text.Trim
                .KESIMPULAN = txtKESIMPULAN.Text.Trim
                .REKOMENDASI = txtREKOMENDASI.Text.Trim
                .CATATAN = txtCATATAN.Text.Trim

                Try
                    .CETAK = oS_DIGITAL_RJ_UJIFUNGSIRM.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_UJIFUNGSIRM.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_UJIFUNGSIRM.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save Lembar Uji Fungsi " & txtNamaPasien.Text.Trim.ToUpper & vbCrLf & "Dengan User : " & sUserID & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
'
#End Region
End Class