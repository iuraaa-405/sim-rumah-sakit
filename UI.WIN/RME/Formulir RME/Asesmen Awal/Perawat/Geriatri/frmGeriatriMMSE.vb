Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmGeriatriMMSE
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_MMSE As New Digital.clsDigital_MMSE
    Private down As Boolean = False
    Private sKDPENDAFTARAN As string
    Private sKDKUNJUNGAN As string
    Private sNoId As String
    Private sKDUSER As String
    Private sKDUSERSIGNATURE As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal NoId As String)
        oFormMode = FormMode

        sNoId = NoId

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtJenisKelamin.Text = dsPendaftaran.JENISKELAMIN
            sKDPENDAFTARAN = dsPendaftaran.KDPENDAFTARAN

            deDATE.DateTime = Now

            sKDUSER = sUserID
            sKDUSERSIGNATURE = sUserSIGNATURE
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtJenisKelamin.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        fn_LoadDataHistory(txtNoPasien.Text)
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

        txtNILAI1.Properties.ReadOnly = Status
        txtNILAI2.Properties.ReadOnly = Status
        txtNILAI3.Properties.ReadOnly = Status
        txtNILAI4.Properties.ReadOnly = Status
        txtNILAI5.Properties.ReadOnly = Status
        txtNILAI6.Properties.ReadOnly = Status
        txtNILAI7.Properties.ReadOnly = Status
        txtNILAI8.Properties.ReadOnly = Status
        txtNILAI9.Properties.ReadOnly = Status
        txtNILAI10.Properties.ReadOnly = Status
        txtNILAI11.Properties.ReadOnly = Status
        'picGAMBAR1.Properties.ReadOnly = Status
        'txtTOTALSKOR.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        txtINSTRUKSI.Properties.ReadOnly = Status
        'picKALIMAT.Properties.ReadOnly = Status
        'picGAMBAR2.Properties.ReadOnly = Status
        chk1_YA.Properties.ReadOnly = Status
        chk1_TIDAK_1.Properties.ReadOnly = Status
        txtSKOR1.Properties.ReadOnly = Status
        chk2_YA_1.Properties.ReadOnly = Status
        chk2_TIDAK.Properties.ReadOnly = Status
        txtSKOR2.Properties.ReadOnly = Status
        chk3_YA_1.Properties.ReadOnly = Status
        chk3_TIDAK.Properties.ReadOnly = Status
        txtSKOR3.Properties.ReadOnly = Status
        chk4_YA_1.Properties.ReadOnly = Status
        chk4_TIDAK.Properties.ReadOnly = Status
        txtSKOR4.Properties.ReadOnly = Status
        chk5_YA.Properties.ReadOnly = Status
        chk5_TIDAK_1.Properties.ReadOnly = Status
        txtSKOR5.Properties.ReadOnly = Status
        chk6_YA_1.Properties.ReadOnly = Status
        chk6_TIDAK.Properties.ReadOnly = Status
        txtSKOR6.Properties.ReadOnly = Status
        chk7_YA.Properties.ReadOnly = Status
        chk7_TIDAK_1.Properties.ReadOnly = Status
        txtSKOR7.Properties.ReadOnly = Status
        chk8_YA_1.Properties.ReadOnly = Status
        chk8_TIDAK.Properties.ReadOnly = Status
        txtSKOR8.Properties.ReadOnly = Status
        chk9_YA_1.Properties.ReadOnly = Status
        chk9_TIDAK.Properties.ReadOnly = Status
        txtSKOR9.Properties.ReadOnly = Status
        chk10_YA_1.Properties.ReadOnly = Status
        chk10_TIDAK.Properties.ReadOnly = Status
        txtSKOR10.Properties.ReadOnly = Status
        chk11_YA.Properties.ReadOnly = Status
        chk11_TIDAK_1.Properties.ReadOnly = Status
        txtSKOR11.Properties.ReadOnly = Status
        chk12_YA_1.Properties.ReadOnly = Status
        chk12_TIDAK.Properties.ReadOnly = Status
        txtSKOR12.Properties.ReadOnly = Status
        chk13_YA.Properties.ReadOnly = Status
        chk13_TIDAK_1.Properties.ReadOnly = Status
        txtSKOR13.Properties.ReadOnly = Status
        chk14_YA_1.Properties.ReadOnly = Status
        chk14_TIDAK.Properties.ReadOnly = Status
        txtSKOR14.Properties.ReadOnly = Status
        chk15_YA_1.Properties.ReadOnly = Status
        chk15_TIDAK.Properties.ReadOnly = Status
        txtSKOR15.Properties.ReadOnly = Status
        txtTOTALSKOR2.Properties.ReadOnly = Status

        chkSkor_0_10.Properties.ReadOnly = Status
        chkSkor_11_20.Properties.ReadOnly = Status
        chkSkor_21_30.Properties.ReadOnly = Status

        chkSkor_0_5.Properties.ReadOnly = Status
        chkSkor_5_9.Properties.ReadOnly = Status
        chkSkor_10.Properties.ReadOnly = Status


    End Sub
    Private Sub fn_EmptyMe()

        deDATE.DateTime = Now
        
        txtNILAI1.ResetText()
        txtNILAI2.ResetText()
        txtNILAI3.ResetText()
        txtNILAI4.ResetText()
        txtNILAI5.ResetText()
        txtNILAI6.ResetText()
        txtNILAI7.ResetText()
        txtNILAI8.ResetText()
        txtNILAI9.ResetText()
        txtNILAI10.ResetText()
        txtNILAI11.ResetText()
        'picGAMBAR1.ResetText()
        txtTOTALSKOR.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        txtINSTRUKSI.ResetText()
        'picKALIMAT
        'picGAMBAR2
        chk1_YA.Checked = False
        chk1_TIDAK_1.Checked = False
        txtSKOR1.ResetText()
        chk2_YA_1.Checked = False
        chk2_TIDAK.Checked = False
        txtSKOR2.ResetText()
        chk3_YA_1.Checked = False
        chk3_TIDAK.Checked = False
        txtSKOR3.ResetText()
        chk4_YA_1.Checked = False
        chk4_TIDAK.Checked = False
        txtSKOR4.ResetText()
        chk5_YA.Checked = False
        chk5_TIDAK_1.Checked = False
        txtSKOR5.ResetText()
        chk6_YA_1.Checked = False
        chk6_TIDAK.Checked = False
        txtSKOR6.ResetText()
        chk7_YA.Checked = False
        chk7_TIDAK_1.Checked = False
        txtSKOR7.ResetText()
        chk8_YA_1.Checked = False
        chk8_TIDAK.Checked = False
        txtSKOR8.ResetText()
        chk9_YA_1.Checked = False
        chk9_TIDAK.Checked = False
        txtSKOR9.ResetText()
        chk10_YA_1.Checked = False
        chk10_TIDAK.Checked = False
        txtSKOR10.ResetText()
        chk11_YA.Checked = False
        chk11_TIDAK_1.Checked = False
        txtSKOR11.ResetText()
        chk12_YA_1.Checked = False
        chk12_TIDAK.Checked = False
        txtSKOR12.ResetText()
        chk13_YA.Checked = False
        chk13_TIDAK_1.Checked = False
        txtSKOR13.ResetText()
        chk14_YA_1.Checked = False
        chk14_TIDAK.Checked = False
        txtSKOR14.ResetText()
        chk15_YA_1.Checked = False
        chk15_TIDAK.Checked = False
        txtSKOR15.ResetText()
        txtTOTALSKOR2.ResetText()


        chkSkor_0_10.Checked = False
        chkSkor_11_20.Checked = False
        chkSkor_21_30.Checked = False

        chkSkor_0_5.Checked = False
        chkSkor_5_9.Checked = False
        chkSkor_10.Checked = False

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoid)
            With ds
                deDATE.DateTime = .DATE
                
                txtNILAI1.Text = .txtNILAI1
                txtNILAI2.Text = .txtNILAI2
                txtNILAI3.Text = .txtNILAI3
                txtNILAI4.Text = .txtNILAI4
                txtNILAI5.Text = .txtNILAI5
                txtNILAI6.Text = .txtNILAI6
                txtNILAI7.Text = .txtNILAI7
                txtNILAI8.Text = .txtNILAI8
                txtNILAI9.Text = .txtNILAI9
                txtNILAI10.Text = .txtNILAI10
                txtNILAI11.Text = .txtNILAI11

                Try
                    picGAMBAR1.Image = ByteArrayToImage(.picGAMBAR1.ToArray())
                Catch oErr As Exception
                End Try

                txtTOTALSKOR.Text = .txtTOTALSKOR
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                txtINSTRUKSI.Text = .txtINSTRUKSI
                
                Try
                    picKALIMAT.Image = ByteArrayToImage(.picKALIMAT.ToArray())
                Catch oErr As Exception
                End Try
                
                Try
                    picGAMBAR2.Image = ByteArrayToImage(.picGAMBAR2.ToArray())
                Catch oErr As Exception
                End Try

                chk1_YA.Checked = .chk1_YA
                chk1_TIDAK_1.Checked = .chk1_TIDAK_1
                txtSKOR1.Text = .txtSKOR1
                chk2_YA_1.Checked = .chk2_YA_1
                chk2_TIDAK.Checked = .chk2_TIDAK
                txtSKOR2.Text = .txtSKOR2
                chk3_YA_1.Checked = .chk3_YA_1
                chk3_TIDAK.Checked = .chk3_TIDAK
                txtSKOR3.Text = .txtSKOR3
                chk4_YA_1.Checked = .chk4_YA_1
                chk4_TIDAK.Checked = .chk4_TIDAK
                txtSKOR4.Text = .txtSKOR4
                chk5_YA.Checked = .chk5_YA
                chk5_TIDAK_1.Checked = .chk5_TIDAK_1
                txtSKOR5.Text = .txtSKOR5
                chk6_YA_1.Checked = .chk6_YA_1
                chk6_TIDAK.Checked = .chk6_TIDAK
                txtSKOR6.Text = .txtSKOR6
                chk7_YA.Checked = .chk7_YA
                chk7_TIDAK_1.Checked = .chk7_TIDAK_1
                txtSKOR7.Text = .txtSKOR7
                chk8_YA_1.Checked = .chk8_YA_1
                chk8_TIDAK.Checked = .chk8_TIDAK
                txtSKOR8.Text = .txtSKOR8
                chk9_YA_1.Checked = .chk9_YA_1
                chk9_TIDAK.Checked = .chk9_TIDAK
                txtSKOR9.Text = .txtSKOR9
                chk10_YA_1.Checked = .chk10_YA_1
                chk10_TIDAK.Checked = .chk10_TIDAK
                txtSKOR10.Text = .txtSKOR10
                chk11_YA.Checked = .chk11_YA
                chk11_TIDAK_1.Checked = .chk11_TIDAK_1
                txtSKOR11.Text = .txtSKOR11
                chk12_YA_1.Checked = .chk12_YA_1
                chk12_TIDAK.Checked = .chk12_TIDAK
                txtSKOR12.Text = .txtSKOR12
                chk13_YA.Checked = .chk13_YA
                chk13_TIDAK_1.Checked = .chk13_TIDAK_1
                txtSKOR13.Text = .txtSKOR13
                chk14_YA_1.Checked = .chk14_YA_1
                chk14_TIDAK.Checked = .chk14_TIDAK
                txtSKOR14.Text = .txtSKOR14
                chk15_YA_1.Checked = .chk15_YA_1
                chk15_TIDAK.Checked = .chk15_TIDAK
                txtSKOR15.Text = .txtSKOR15
                txtTOTALSKOR2.Text = .txtTOTALSKOR2

                chkSkor_0_10.Checked = .S_KET_0_10
                chkSkor_11_20.Checked = .S_KET_11_20
                chkSkor_21_30.Checked = .S_KET_21_30

                chkSkor_0_5.Checked = .S_KET_0_5
                chkSkor_5_9.Checked = .S_KET_5_9
                chkSkor_10.Checked = .S_KET_10

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
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
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetStructureHeader
            With ds
                .KDINDEKS = sNoId
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .txtNILAI1 = txtNILAI1.Text
                .txtNILAI2 = txtNILAI2.Text
                .txtNILAI3 = txtNILAI3.Text
                .txtNILAI4 = txtNILAI4.Text
                .txtNILAI5 = txtNILAI5.Text
                .txtNILAI6 = txtNILAI6.Text
                .txtNILAI7 = txtNILAI7.Text
                .txtNILAI8 = txtNILAI8.Text
                .txtNILAI9 = txtNILAI9.Text
                .txtNILAI10 = txtNILAI10.Text
                .txtNILAI11 = txtNILAI11.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR1.Image.Save(ms, picGAMBAR1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .picGAMBAR1 = data
                Catch oErr As Exception
                    Try
                        .picGAMBAR1 = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoId).picGAMBAR1
                    Catch ex As Exception

                    End Try
                End Try

                .txtTOTALSKOR = txtTOTALSKOR.Text
                .CheckEdit1 = CheckEdit1.Checked
                .CheckEdit2 = CheckEdit2.Checked
                .CheckEdit3 = CheckEdit3.Checked
                .CheckEdit4 = CheckEdit4.Checked
                .txtINSTRUKSI = txtINSTRUKSI.Text
                

                Try
                    Dim ms As New IO.MemoryStream()
                    picKALIMAT.Image.Save(ms, picKALIMAT.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .picKALIMAT = data
                Catch oErr As Exception
                    Try
                        .picKALIMAT = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoId).picKALIMAT
                    Catch ex As Exception

                    End Try
                End Try

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .picGAMBAR2 = data
                Catch oErr As Exception
                    Try
                        .picGAMBAR2 = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoId).picGAMBAR2
                    Catch ex As Exception

                    End Try
                End Try

                .chk1_YA = chk1_YA.Checked
                .chk1_TIDAK_1 = chk1_TIDAK_1.Checked
                .txtSKOR1 = txtSKOR1.Text
                .chk2_YA_1 = chk2_YA_1.Checked
                .chk2_TIDAK = chk2_TIDAK.Checked
                .txtSKOR2 = txtSKOR2.Text
                .chk3_YA_1 = chk3_YA_1.Checked
                .chk3_TIDAK = chk3_TIDAK.Checked
                .txtSKOR3 = txtSKOR3.Text
                .chk4_YA_1 = chk4_YA_1.Checked
                .chk4_TIDAK = chk4_TIDAK.Checked
                .txtSKOR4 = txtSKOR4.Text
                .chk5_YA = chk5_YA.Checked
                .chk5_TIDAK_1 = chk5_TIDAK_1.Checked
                .txtSKOR5 = txtSKOR5.Text
                .chk6_YA_1 = chk6_YA_1.Checked
                .chk6_TIDAK = chk6_TIDAK.Checked
                .txtSKOR6 = txtSKOR6.Text
                .chk7_YA = chk7_YA.Checked
                .chk7_TIDAK_1 = chk7_TIDAK_1.Checked
                .txtSKOR7 = txtSKOR7.Text
                .chk8_YA_1 = chk8_YA_1.Checked
                .chk8_TIDAK = chk8_TIDAK.Checked
                .txtSKOR8 = txtSKOR8.Text
                .chk9_YA_1 = chk9_YA_1.Checked
                .chk9_TIDAK = chk9_TIDAK.Checked
                .txtSKOR9 = txtSKOR9.Text
                .chk10_YA_1 = chk10_YA_1.Checked
                .chk10_TIDAK = chk10_TIDAK.Checked
                .txtSKOR10 = txtSKOR10.Text
                .chk11_YA = chk11_YA.Checked
                .chk11_TIDAK_1 = chk11_TIDAK_1.Checked
                .txtSKOR11 = txtSKOR11.Text
                .chk12_YA_1 = chk12_YA_1.Checked
                .chk12_TIDAK = chk12_TIDAK.Checked
                .txtSKOR12 = txtSKOR12.Text
                .chk13_YA = chk13_YA.Checked
                .chk13_TIDAK_1 = chk13_TIDAK_1.Checked
                .txtSKOR13 = txtSKOR13.Text
                .chk14_YA_1 = chk14_YA_1.Checked
                .chk14_TIDAK = chk14_TIDAK.Checked
                .txtSKOR14 = txtSKOR14.Text
                .chk15_YA_1 = chk15_YA_1.Checked
                .chk15_TIDAK = chk15_TIDAK.Checked
                .txtSKOR15 = txtSKOR15.Text
                .txtTOTALSKOR2 = txtTOTALSKOR2.Text

                .S_KET_0_10 = chkSkor_0_10.Checked
                .S_KET_11_20 = chkSkor_11_20.Checked
                .S_KET_21_30 = chkSkor_21_30.Checked

                .S_KET_0_5 = chkSkor_0_5.Checked
                .S_KET_5_9 = chkSkor_5_9.Checked
                .S_KET_10 = chkSkor_10.Checked

                Try
                    .CETAK = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER
                .KDUSER_SIGNATURE = sKDUSERSIGNATURE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_MMSE.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_MMSE.UpdateData(ds)
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
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
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
    Private Sub picGAMBAR1_Click(sender As Object, e As EventArgs) Handles picGAMBAR1.Click
        If fileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            picGAMBAR1.Load(fileDialog.FileName)
            picGAMBAR1.Update()
        End If
    End Sub

    Private Sub picKALIMAT_Click(sender As Object, e As EventArgs) Handles picKALIMAT.Click
        If fileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            picKALIMAT.Load(fileDialog.FileName)
            picKALIMAT.Update()
        End If
    End Sub

    Private Sub picGAMBAR2_Click(sender As Object, e As EventArgs) Handles picGAMBAR2.Click
        If fileDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then
            picGAMBAR2.Load(fileDialog.FileName)
            picGAMBAR2.Update()
        End If
    End Sub


    Dim nilai1 As Integer
    Private Sub txtNILAI1_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI1.EditValueChanging
        If txtNILAI1.Text IsNot String.Empty Then
            nilai1 =  CInt(txtNILAI1.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai2 As Integer
    Private Sub txtNILAI2_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI2.EditValueChanging
        If txtNILAI2.Text IsNot String.Empty Then
            nilai2 =  CInt(txtNILAI2.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai3 As Integer
    Private Sub txtNILAI3_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI3.EditValueChanging
        If txtNILAI3.Text IsNot String.Empty Then
            nilai3 =  CInt(txtNILAI3.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai4 As Integer
    Private Sub txtNILAI4_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI4.EditValueChanging
        If txtNILAI4.Text IsNot String.Empty Then
            nilai4 =  CInt(txtNILAI4.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai5 As Integer
    Private Sub txtNILAI5_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI5.EditValueChanging
        If txtNILAI5.Text IsNot String.Empty Then
            nilai5 =  CInt(txtNILAI5.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai6 As Integer
    Private Sub txtNILAI6_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI6.EditValueChanging
        If txtNILAI6.Text IsNot String.Empty Then
            nilai6 =  CInt(txtNILAI6.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai7 As Integer
    Private Sub txtNILAI7_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI7.EditValueChanging
        If txtNILAI7.Text IsNot String.Empty Then
            nilai7 =  CInt(txtNILAI7.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai8 As Integer
    Private Sub txtNILAI8_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI8.EditValueChanging
        If txtNILAI8.Text IsNot String.Empty Then
            nilai8 =  CInt(txtNILAI8.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai9 As Integer
    Private Sub txtNILAI9_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI9.EditValueChanging
        If txtNILAI9.Text IsNot String.Empty Then
            nilai9 =  CInt(txtNILAI9.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai10 As Integer
    Private Sub txtNILAI10_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI10.EditValueChanging
        If txtNILAI10.Text IsNot String.Empty Then
            nilai10 =  CInt(txtNILAI10.Text)
            fn_totalskor1()
        End If
    End Sub

    Dim nilai11 As Integer
    Private Sub txtNILAI11_EditValueChanging(sender As Object, e As DevExpress.XtraEditors.Controls.ChangingEventArgs) Handles txtNILAI11.EditValueChanging
        If txtNILAI11.Text IsNot String.Empty Then
            nilai11 =  CInt(txtNILAI11.Text)
            fn_totalskor1()
        End If
    End Sub

    Private sub fn_totalskor1()
        Dim iTotal = nilai1 + nilai2 + nilai3 + nilai4 + nilai5 + nilai6 + nilai7 + nilai8 + nilai9 + nilai10 + nilai11
        txtTOTALSKOR.Text = iTotal

        If iTotal >= 0 And iTotal <= 10  Then
	        chkSkor_0_10.Checked = True
            chkSkor_11_20.Checked = False
            chkSkor_21_30.Checked = False
        ElseIf iTotal >= 11 And iTotal <= 20 Then
            chkSkor_0_10.Checked = False
	        chkSkor_11_20.Checked = True
            chkSkor_21_30.Checked = False
        ElseIf iTotal >= 21 And iTotal <= 30 Then
            chkSkor_0_10.Checked = False
            chkSkor_11_20.Checked = False
	        chkSkor_21_30.Checked = True
        End If

    End sub


    Dim skor1 As Integer
    Private Sub chk1_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_YA.CheckedChanged
        If chk1_YA.Checked Then
            chk1_TIDAK_1.Checked = False
            skor1 = 0
        Else
            skor1 = 0
        End If
        fn_JumlahSkor()
    End Sub
    
    Private Sub chk1_TIDAK_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_TIDAK_1.CheckedChanged
        If chk1_TIDAK_1.Checked Then
            chk1_YA.Checked = False
            skor1 = 1
        Else
            skor1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor2 As Integer
    Private Sub chk2_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_YA_1.CheckedChanged
        If chk2_YA_1.Checked Then
            chk2_TIDAK.Checked = False
            skor2 = 1
        Else
            skor2 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk2_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_TIDAK.CheckedChanged
        If chk2_TIDAK.Checked Then
            chk2_YA_1.Checked = False
            skor2 = 0
        Else
            skor2 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor3 As Integer
    Private Sub chk3_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk3_YA_1.CheckedChanged
        If chk3_YA_1.Checked Then
            chk3_TIDAK.Checked = False
            skor3 = 1
        Else
            skor3 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk3_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk3_TIDAK.CheckedChanged
        If chk3_TIDAK.Checked Then
            chk3_YA_1.Checked = False
            skor3 = 0
        Else
            skor3 = 0
        End If
        fn_JumlahSkor()
    End Sub


    Dim skor4 As Integer
    Private Sub chk4_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk4_YA_1.CheckedChanged
        If chk4_YA_1.Checked Then
            chk4_TIDAK.Checked = False
            skor4 = 1
        Else
            skor4 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk4_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk4_TIDAK.CheckedChanged
        If chk4_TIDAK.Checked Then
            chk4_YA_1.Checked = False
            skor4 = 0
        Else
            skor4 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor5 As Integer
    Private Sub chk5_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chk5_YA.CheckedChanged
        If chk5_YA.Checked Then
            chk5_TIDAK_1.Checked = False
            skor5 = 0
        Else
            skor5 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk5_TIDAK_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk5_TIDAK_1.CheckedChanged
        If chk5_TIDAK_1.Checked Then
            chk5_YA.Checked = False
            skor5 = 1
        Else
            skor5 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor6 As Integer
    Private Sub chk6_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_YA_1.CheckedChanged
        If chk6_YA_1.Checked Then
            chk6_TIDAK.Checked = False
            skor6 = 1
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk6_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_TIDAK.CheckedChanged
        If chk6_TIDAK.Checked Then
            chk6_YA_1.Checked = False
            skor6 = 0
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor7 As Integer
    Private Sub chk7_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_YA.CheckedChanged
        If chk7_YA.Checked Then
            chk7_TIDAK_1.Checked = False
            skor7 = 0
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk7_TIDAK_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_TIDAK_1.CheckedChanged
        If chk7_TIDAK_1.Checked Then
            chk7_YA.Checked = False
            skor7 = 1
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor8 As Integer
    Private Sub chk8_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk8_YA_1.CheckedChanged
        If chk8_YA_1.Checked Then
            chk8_TIDAK.Checked = False
            skor8 = 1
        Else
            skor8 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk8_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk8_TIDAK.CheckedChanged
        If chk8_TIDAK.Checked Then
            chk8_YA_1.Checked = False
            skor8 = 0
        Else
            skor8 = 0
        End If
        fn_JumlahSkor()
    End Sub


    Dim skor9 As Integer
    Private Sub chk9_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk9_YA_1.CheckedChanged
        If chk9_YA_1.Checked Then
            chk9_TIDAK.Checked = False
            skor9 = 1
        Else
            skor9 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk9_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk9_TIDAK.CheckedChanged
        If chk9_TIDAK.Checked Then
            chk9_YA_1.Checked = False
            skor9 = 0
        Else
            skor9 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor10 As Integer
    Private Sub chk10_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk10_YA_1.CheckedChanged
        If chk10_YA_1.Checked Then
            chk10_TIDAK.Checked = False
            skor10 = 1
        Else
            skor10 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk10_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk10_TIDAK.CheckedChanged
        If chk10_TIDAK.Checked Then
            chk10_YA_1.Checked = False
            skor10 = 0
        Else
            skor10 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor11 As Integer
    Private Sub chk11_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chk11_YA.CheckedChanged
        If chk11_YA.Checked Then
            chk11_TIDAK_1.Checked = False
            skor11 = 0
        Else
            skor11 = 1
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk11_TIDAK_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk11_TIDAK_1.CheckedChanged
        If chk11_TIDAK_1.Checked Then
            chk11_YA.Checked = False
            skor11 = 1
        Else
            skor11 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor12 As Integer
    Private Sub chk12_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk12_YA_1.CheckedChanged
        If chk12_YA_1.Checked Then
            chk12_TIDAK.Checked = False
            skor12 = 1
        Else
            skor12 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk12_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk12_TIDAK.CheckedChanged
        If chk12_TIDAK.Checked Then
            chk12_YA_1.Checked = False
            skor12 = 0
        Else
            skor12 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor13 As Integer
    Private Sub chk13_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chk13_YA.CheckedChanged
        If chk13_YA.Checked Then
            chk13_TIDAK_1.Checked = False
            skor13 = 0
        Else
            skor13 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk13_TIDAK_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk13_TIDAK_1.CheckedChanged
        If chk13_TIDAK_1.Checked Then
            chk13_YA.Checked = False
            skor13 = 1
        Else
            skor13 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor14 As Integer
    Private Sub chk14_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk14_YA_1.CheckedChanged
        If chk14_YA_1.Checked Then
            chk14_TIDAK.Checked = False
            skor14 = 1
        Else
            skor14 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk14_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk14_TIDAK.CheckedChanged
        If chk14_TIDAK.Checked Then
            chk14_YA_1.Checked = False
            skor14 = 0
        Else
            skor14 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor15 As Integer
    Private Sub chk15_YA_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk15_YA_1.CheckedChanged
        If chk15_YA_1.Checked Then
            chk15_TIDAK.Checked = False
            skor15 = 1
        Else
            skor15 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private Sub chk15_TIDAK_CheckedChanged(sender As Object, e As EventArgs) Handles chk15_TIDAK.CheckedChanged, chkSkor_5_9.CheckedChanged, chkSkor_10.CheckedChanged, chkSkor_0_5.CheckedChanged
        If chk15_TIDAK.Checked Then
            chk15_YA_1.Checked = False
            skor15 = 0
        Else
            skor15 = 0
        End If
        fn_JumlahSkor()
    End Sub


    Private Sub fn_JumlahSkor()
        txtSKOR1.Text = skor1
        txtSKOR2.Text = skor2
        txtSKOR3.Text = skor3
        txtSKOR4.Text = skor4
        txtSKOR5.Text = skor5
        txtSKOR6.Text = skor6
        txtSKOR7.Text = skor7
        txtSKOR8.Text = skor8
        txtSKOR9.Text = skor9
        txtSKOR10.Text = skor10
        txtSKOR11.Text = skor11
        txtSKOR12.Text = skor12
        txtSKOR13.Text = skor13
        txtSKOR14.Text = skor14
        txtSKOR15.Text = skor15

        Dim iTotal = skor1 + skor2 + skor3 + skor4 + skor5 + skor6 + skor7 + skor8 + skor9 + skor10 + skor11 + skor12 + skor13 + skor14 + skor15

        txtTOTALSKOR2.Text = iTotal

        If iTotal >= 0 And iTotal <= 5 Then
            chkSkor_0_5.Checked = True
            chkSkor_5_9.Checked = False
            chkSkor_10.Checked = False
        ElseIf iTotal > 5 And iTotal <= 9 Then
            chkSkor_0_5.Checked = False
            chkSkor_5_9.Checked = True
            chkSkor_10.Checked = False
        ElseIf iTotal = 10 Then
            chkSkor_0_5.Checked = False
            chkSkor_5_9.Checked = False
            chkSkor_10.Checked = True
        End If


    End Sub

    Private Sub fn_LoadDataHistory(ByVal KDCUSTOMER As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDINDEKS "
            SQL &= ",A.KDKUNJUNGAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE  "
            SQL &= ",A.KDUSER "
            SQL &= "FROM  "
            SQL &= "S_DIGITAL_ASKEPGERIATRI_MMSE A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B  "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN  "
            SQL &= "WHERE b.KDCUSTOMER = '" & KDCUSTOMER & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_GERIATRIMMSE")

            grd_Riwayat_MMSE.MainView = grv_Riwayat_MMSE
            grd_Riwayat_MMSE.DataSource = ds.Tables("R_GERIATRIMMSE")
            grd_Riwayat_MMSE.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CopyMMSEToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyMMSEToolStripMenuItem.Click
        If grv_Riwayat_MMSE.GetFocusedRowCellValue("KDINDEKS") Is Nothing Then
            Exit Sub
        End If

        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_MMSE.GetData(grv_Riwayat_MMSE.GetFocusedRowCellValue("KDINDEKS"))
            With ds
                deDATE.DateTime = .DATE

                txtNILAI1.Text = .txtNILAI1
                txtNILAI2.Text = .txtNILAI2
                txtNILAI3.Text = .txtNILAI3
                txtNILAI4.Text = .txtNILAI4
                txtNILAI5.Text = .txtNILAI5
                txtNILAI6.Text = .txtNILAI6
                txtNILAI7.Text = .txtNILAI7
                txtNILAI8.Text = .txtNILAI8
                txtNILAI9.Text = .txtNILAI9
                txtNILAI10.Text = .txtNILAI10
                txtNILAI11.Text = .txtNILAI11

                Try
                    picGAMBAR1.Image = ByteArrayToImage(.picGAMBAR1.ToArray())
                Catch oErr As Exception
                End Try

                txtTOTALSKOR.Text = .txtTOTALSKOR
                CheckEdit1.Checked = .CheckEdit1
                CheckEdit2.Checked = .CheckEdit2
                CheckEdit3.Checked = .CheckEdit3
                CheckEdit4.Checked = .CheckEdit4
                txtINSTRUKSI.Text = .txtINSTRUKSI

                Try
                    picKALIMAT.Image = ByteArrayToImage(.picKALIMAT.ToArray())
                Catch oErr As Exception
                End Try

                Try
                    picGAMBAR2.Image = ByteArrayToImage(.picGAMBAR2.ToArray())
                Catch oErr As Exception
                End Try

                chk1_YA.Checked = .chk1_YA
                chk1_TIDAK_1.Checked = .chk1_TIDAK_1
                txtSKOR1.Text = .txtSKOR1
                chk2_YA_1.Checked = .chk2_YA_1
                chk2_TIDAK.Checked = .chk2_TIDAK
                txtSKOR2.Text = .txtSKOR2
                chk3_YA_1.Checked = .chk3_YA_1
                chk3_TIDAK.Checked = .chk3_TIDAK
                txtSKOR3.Text = .txtSKOR3
                chk4_YA_1.Checked = .chk4_YA_1
                chk4_TIDAK.Checked = .chk4_TIDAK
                txtSKOR4.Text = .txtSKOR4
                chk5_YA.Checked = .chk5_YA
                chk5_TIDAK_1.Checked = .chk5_TIDAK_1
                txtSKOR5.Text = .txtSKOR5
                chk6_YA_1.Checked = .chk6_YA_1
                chk6_TIDAK.Checked = .chk6_TIDAK
                txtSKOR6.Text = .txtSKOR6
                chk7_YA.Checked = .chk7_YA
                chk7_TIDAK_1.Checked = .chk7_TIDAK_1
                txtSKOR7.Text = .txtSKOR7
                chk8_YA_1.Checked = .chk8_YA_1
                chk8_TIDAK.Checked = .chk8_TIDAK
                txtSKOR8.Text = .txtSKOR8
                chk9_YA_1.Checked = .chk9_YA_1
                chk9_TIDAK.Checked = .chk9_TIDAK
                txtSKOR9.Text = .txtSKOR9
                chk10_YA_1.Checked = .chk10_YA_1
                chk10_TIDAK.Checked = .chk10_TIDAK
                txtSKOR10.Text = .txtSKOR10
                chk11_YA.Checked = .chk11_YA
                chk11_TIDAK_1.Checked = .chk11_TIDAK_1
                txtSKOR11.Text = .txtSKOR11
                chk12_YA_1.Checked = .chk12_YA_1
                chk12_TIDAK.Checked = .chk12_TIDAK
                txtSKOR12.Text = .txtSKOR12
                chk13_YA.Checked = .chk13_YA
                chk13_TIDAK_1.Checked = .chk13_TIDAK_1
                txtSKOR13.Text = .txtSKOR13
                chk14_YA_1.Checked = .chk14_YA_1
                chk14_TIDAK.Checked = .chk14_TIDAK
                txtSKOR14.Text = .txtSKOR14
                chk15_YA_1.Checked = .chk15_YA_1
                chk15_TIDAK.Checked = .chk15_TIDAK
                txtSKOR15.Text = .txtSKOR15
                txtTOTALSKOR2.Text = .txtTOTALSKOR2

                chkSkor_0_10.Checked = .S_KET_0_10
                chkSkor_11_20.Checked = .S_KET_11_20
                chkSkor_21_30.Checked = .S_KET_21_30

                chkSkor_0_5.Checked = .S_KET_0_5
                chkSkor_5_9.Checked = .S_KET_5_9
                chkSkor_10.Checked = .S_KET_10

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmGeriatriMMSE_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel3.AutoScrollPosition = myView
    End Sub
#End Region
End Class