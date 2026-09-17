Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmSuratKeteranganKematian
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oSuratKematian As New Digital.clsDigital_SURATKETKEMATIAN
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sCATEGORY  As Integer
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtKDREG.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtKDREG.Text = dsPendaftaran.KDKUNJUNGAN
            deDATE.DateTime = dsPendaftaran.DATE

            grdDOCTOR.Text = dsPendaftaran.DOKTER
            txtPANGKATDOKTER.Text = ""
            txtNRP.Text = ""
            txtKESATUAN.Text = ""

            grdDepartment.Text = dsPendaftaran.TUJUAN

            txtPASIEN.Text = dsPendaftaran.NAMAPASIEN
            txtUMUR.Text = dsPendaftaran.USIA
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtKELUARGA.Text = dsPendaftaran.HUBUNGAN_NAMA
            txtPANGKATPASIEN.Text = dsPendaftaran.PANGKAT
            txtNRPPASIEN.Text = dsPendaftaran.NRP
            txtKESATUANPASIEN.Text = dsPendaftaran.KESATUAN
            txtALAMAT.Text = dsPendaftaran.ALAMAT
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            sCATEGORY = dsPendaftaran.CATEGORY
        Else
            txtNOMORRM.ResetText()
            txtKDREG.ResetText()
            deDATE.ResetText()
            grdDOCTOR.ResetText()
            txtPANGKATDOKTER.ResetText()
            txtNRP.ResetText()
            txtKESATUAN.ResetText()
            grdDepartment.ResetText()
            txtPASIEN.ResetText()
            txtUMUR.ResetText()
            txtJK.ResetText()
            txtKELUARGA.ResetText()
            txtPANGKATPASIEN.ResetText()
            txtNRPPASIEN.ResetText()
            txtKESATUANPASIEN.ResetText()
            txtALAMAT.ResetText()
        End If


    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = SuratKeteranganKematian.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtPASIEN.Text.Trim.ToUpper
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
        deDATE.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        cboHARI.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        deDATEMENINGGAL.Properties.ReadOnly = Status
        grdDepartment.Properties.ReadOnly = Status

        txtNOSURAT.Properties.ReadOnly = Status
        txtPANGKATDOKTER.Properties.ReadOnly = Status
        txtNRP.Properties.ReadOnly = Status
        txtKESATUAN.Properties.ReadOnly = Status

        txtKELUARGA.Properties.ReadOnly = Status

        deDATE_NOLPMANUAL.Properties.ReadOnly = Status

        If sCATEGORY = 0 Then
            lKETERANGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        Else
            lKETERANGAN.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        End If

        'cboKETERANGAN.Properties.ReadOnly = Status
    End Sub

    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        txtPANGKATDOKTER.ResetText()
        txtKESATUAN.Text = "Rumah Sakit Dustira Kesdam III/Siliwangi"
        txtNRP.ResetText()
        cboHARI.SelectedIndex = CInt(DateTime.Now.DayOfWeek) - 1
        txtJAM.Text = Now.ToString("HH:mm")
        deDATEMENINGGAL.DateTime = Now
        txtNOSURAT.ResetText()
        deDATE_NOLPMANUAL.DateTime = Now
        cboKETERANGAN.SelectedIndex = 0
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKematian.GetData(txtKDREG.Text)
            With ds
                txtNOMORRM.Text = .R_IDENTITAS_PASIEN.KDCUSTOMER
                txtKDREG.Text = .KDKUNJUNGAN
                deDATE.DateTime = .DATE
                txtNOSURAT.Text = .NOMOR

                grdDOCTOR.Text = .DOKTER_NAMEDISPLAY
                txtPANGKATDOKTER.Text = .DOKTER_PANGKAT
                txtNRP.Text = .DOKTER_NRP
                txtKESATUAN.Text = .DOKTER_KESATUAN

                cboHARI.Text = .HARI
                txtJAM.Text = .JAM
                deDATEMENINGGAL.DateTime = .TGL_MENINGGAL
                grdDepartment.Text = .R_IDENTITAS_PASIEN.TUJUAN

                txtPASIEN.Text = .R_IDENTITAS_PASIEN.NAMAPASIEN
                txtUMUR.Text = .R_IDENTITAS_PASIEN.USIA
                txtJK.Text = .R_IDENTITAS_PASIEN.JENISKELAMIN
                txtKELUARGA.Text = .KET
                txtPANGKATPASIEN.Text = .R_IDENTITAS_PASIEN.PANGKAT
                txtNRPPASIEN.Text = .R_IDENTITAS_PASIEN.NRP
                txtKESATUANPASIEN.Text = .R_IDENTITAS_PASIEN.KESATUAN
                txtALAMAT.Text = .R_IDENTITAS_PASIEN.ALAMAT

                deDATE_NOLPMANUAL.DateTime = .NOLPMANUAL
                cboKETERANGAN.Text = .KETERANGAN
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
   
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSuratKematian.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSuratKematian.GetData(txtKDREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDKUNJUNGAN = txtKDREG.Text

                .NOMOR = txtNOSURAT.Text.ToUpper.Trim

                .DATE = deDATE.DateTime
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                .HARI = cboHARI.Text
                .JAM = txtJAM.Text
                .TGL_MENINGGAL = deDATEMENINGGAL.DateTime
                .KET = txtKELUARGA.Text.ToUpper.Trim

                .DOKTER_KODE = sKODEDOKTER
                '.DOKTER_NAMEDISPLAY = sNAMADOKTER
                .DOKTER_KESATUAN = txtKESATUAN.Text
                .DOKTER_NRP = txtNRP.Text
                .DOKTER_PANGKAT = txtPANGKATDOKTER.Text

                Try
                    .CETAK = oSuratKematian.GetData(txtKDREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE

                .NOLPMANUAL = deDATE_NOLPMANUAL.DateTime
                .KETERANGAN = cboKETERANGAN.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSuratKematian.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuratKematian.UpdateData(ds)
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        'If fn_Validate() = False Then Exit Sub
        'If MsgBox("Save " & txtPASIEN.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        'If fn_Save() = False Then
        '    MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        'Else
        '    MsgBox("Save " & txtPASIEN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
        '    sStatusSave = "NEW"
        '    Me.Close()
        'End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        'If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtPASIEN.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtPASIEN.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
#End Region
End Class