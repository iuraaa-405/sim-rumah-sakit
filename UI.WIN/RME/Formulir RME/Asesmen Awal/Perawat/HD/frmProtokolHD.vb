Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmProtokolHD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_PROTOKOLHD As New EMedrek.clsDigital_ProtokolHD
    Private down As Boolean = False
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "PROTOKOL HEMODIALISA"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Doctor()
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

        'txtTanggal.Properties.ReadOnly = Status
        'txtJam.Properties.ReadOnly = Status
        txtANAMNESA.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        txtTATALAKSANA.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        'txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
        'txtJam.Text = DateTime.Now.ToString("HH:mm")
        txtANAMNESA.ResetText()
        txtPEMERIKSAANFISIK.ResetText()
        txtDIAGNOSA.ResetText()
        txtTATALAKSANA.ResetText()

        If sNoid <> "" Then
            fn_LoadResumeRawatJalanAwal(sNoid)
        End If

        'Dim oKunjungan As New Identitas.clsIdentitasPasien
        'Dim dsKunjungan = oKunjungan.GetData(SNOID)
        'If dsKunjungan IsNot Nothing Then
        '    fn_LoadDataSKD(dsKunjungan.KDPENDAFTARAN)
        'End If

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_PROTOKOLHD.GetData(sNoid)

            With ds
                txtANAMNESA.Text = .ANAMNESA
                txtPEMERIKSAANFISIK.Text = .PEMERIKSAANFISIK
                txtDIAGNOSA.Text = .DIAGNOSA
                txtTATALAKSANA.Text = .TATALAKSANA
                grdDOKTERPJ.EditValue = .KDDOKTERPJ
                grdDOKTERPELAKSANA.EditValue = .KDDOKTERPELAKSANA
                grdDOKTERSUPERVISOR.EditValue = .KDDOKTERSUPERVISOR
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtANAMNESA.Focus()
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
            Dim ds = oS_DIGITAL_PROTOKOLHD.GetStructureHeader
            With ds
                .KDKUNJUNGAN = sNoid
                .KDIDENTITAS = sKodeIdentitas
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .ANAMNESA = txtANAMNESA.Text
                .PEMERIKSAANFISIK = txtPEMERIKSAANFISIK.Text
                .DIAGNOSA = txtDIAGNOSA.Text
                .TATALAKSANA = txtTATALAKSANA.Text
                .KDDOKTERPJ = grdDOKTERPJ.EditValue
                .NAMADOKTERPJ = grdDOKTERPJ.Text
                .KDDOKTERPELAKSANA = grdDOKTERPELAKSANA.EditValue
                .NAMADOKTERPELAKSANA = grdDOKTERPELAKSANA.Text
                .KDDOKTERSUPERVISOR = grdDOKTERSUPERVISOR.EditValue
                .NAMADOKTERSUPERVISOR = grdDOKTERSUPERVISOR.Text
                .NAMADOKTERPJ = txtDOKTERPJ.Text
                .NAMADOKTERPELAKSANA = txtDOKTERPELAKSANA.Text
                .NAMADOKTERSUPERVISOR = txtSUPERVISOR.Text

                Try
                    .CETAK = oS_DIGITAL_PROTOKOLHD.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_PROTOKOLHD.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_PROTOKOLHD.UpdateData(ds)
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
            'Case Keys.F12
            '    btnClose_Click()
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadResumeRawatJalanAwal(ByVal Parameter As String)
        'Dim oDiagnosaMaster As New Diagnosa.clsMasterDiagnosa
        'Dim dsDiagnosa = oDiagnosaMaster.GetData(Parameter)
        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01

        'If dsDiagnosa IsNot Nothing Then
        '    txtDIAGNOSA.Text = dsDiagnosa.REMARKS
        '    txtTATALAKSANA.Text = dsDiagnosa.REMARKS_
        'End If

        Dim dsIGD = oS_DIGITAL_IGD_01.GetDataByKodeIGD(Parameter)
        If dsIGD IsNot Nothing Then
            Dim Riwayat As String = dsIGD.RIWAYAT
            Dim Riwayat_Alergi As String = dsIGD.RIWAYAT_ALERGI
            Dim Riwayat_PenyakitDahulu As String = dsIGD.RIWAYAT_PENYAKITDAHULU
            Dim TingkatKesadaran As String = dsIGD.TINGKATKESADARAN
            Dim KeadaanUmum As String = dsIGD.KEADAANUMUM
            Dim BeratBadan As String = dsIGD.BERATBADAN & " Kg"
            Dim TinggiBadan As String = dsIGD.TINGGIBADAN & " Cm"
            Dim GCS As String = dsIGD.GCS & " E : " & dsIGD.E & " M : " & dsIGD.M & " V : " & dsIGD.V
            Dim Tensi As String = dsIGD.BP & " MmHg"
            Dim Nadi As String = dsIGD.HR & " x/menit"
            Dim Respirasi As String = dsIGD.RR & " x/menit"
            Dim Suhu As String = dsIGD.T & " oC"
            Dim SpO2 As String = dsIGD.SP02 & " %"
            Dim SkalaNyeri As String = String.Empty
            Dim Survey As String = String.Empty

            Dim Anamesa As String = String.Empty
            Anamesa = Riwayat
            If Riwayat_Alergi <> "" Or Riwayat_Alergi <> "-" Then
                Anamesa = Anamesa & ", " & " Alergi " & Riwayat_Alergi
            End If
            If Riwayat_PenyakitDahulu <> "" Or Riwayat_PenyakitDahulu <> "-" Then
                Anamesa = Anamesa & ", " & " Riwayat Terdahulu " & Riwayat_PenyakitDahulu
            End If

            Anamesa = Anamesa & ", " & " Tingkat Kesadaran " & TingkatKesadaran & ", " & " Keadaan Umum " & KeadaanUmum & ", " &
                      " GCS : " & GCS & ", " &
                      " BB : " & BeratBadan & ", " &
                      " TB : " & BeratBadan & ", " &
                      " BP : " & Tensi & ", " &
                      " HR : " & Nadi & ", " &
                      " RR : " & Respirasi

            If dsIGD.SKALANEYRI_00 = True Then
                Anamesa = Anamesa & ", VAS : 0"
            ElseIf dsIGD.SKALANEYRI_01 = True Then
                Anamesa = Anamesa & ", VAS : 1"
            ElseIf dsIGD.SKALANEYRI_02 = True Then
                Anamesa = Anamesa & ", VAS : 2"
            ElseIf dsIGD.SKALANEYRI_03 = True Then
                Anamesa = Anamesa & ", VAS : 3"
            ElseIf dsIGD.SKALANEYRI_04 = True Then
                Anamesa = Anamesa & ", VAS : 4"
            ElseIf dsIGD.SKALANEYRI_05 = True Then
                Anamesa = Anamesa & ", VAS : 5"
            ElseIf dsIGD.SKALANEYRI_06 = True Then
                Anamesa = Anamesa & ", VAS : 6"
            ElseIf dsIGD.SKALANEYRI_07 = True Then
                Anamesa = Anamesa & ", VAS : 7"
            ElseIf dsIGD.SKALANEYRI_08 = True Then
                Anamesa = Anamesa & ", VAS : 8"
            ElseIf dsIGD.SKALANEYRI_09 = True Then
                Anamesa = Anamesa & ", VAS : 9"
            ElseIf dsIGD.SKALANEYRI_10 = True Then
                Anamesa = Anamesa & ", VAS : 10"
            End If

            If dsIGD.SURVEY_KEPALA_2 <> "" Then
                Anamesa = Anamesa & ", Kepala : " & dsIGD.SURVEY_KEPALA_2
            ElseIf dsIGD.SURVEY_MATA_2 <> "" Then
                Anamesa = Anamesa & ", Mata : " & dsIGD.SURVEY_MATA_2
            ElseIf dsIGD.SURVEY_LEHER_2 <> "" Then
                Anamesa = Anamesa & ", Leher : " & dsIGD.SURVEY_LEHER_2
            ElseIf dsIGD.SURVEY_DADA_2 <> "" Then
                Anamesa = Anamesa & ", Dada : " & dsIGD.SURVEY_DADA_2
            ElseIf dsIGD.SURVEY_PERUT_2 <> "" Then
                Anamesa = Anamesa & ", Perut : " & dsIGD.SURVEY_PERUT_2
            ElseIf dsIGD.SURVEY_ALATGERAK_2 <> "" Then
                Anamesa = Anamesa & ", Alat Gerak : " & dsIGD.SURVEY_ALATGERAK_2
            End If

            txtANAMNESA.Text = Anamesa
            'txtTERAPI.Text = fn_LoadPengobatan()
        Else
            'Asesmen Penyakit Dalam
            Dim oAsesmen31 As New EMedrek.clsDigital_RJ_31
            Dim dsAsesmen_31 = oAsesmen31.GetData(sNoid)
            If dsAsesmen_31 IsNot Nothing Then
                txtANAMNESA.Text = dsAsesmen_31.SDIGITALRJ31_1 & " " & dsAsesmen_31.SDIGITALRJ31_2 & " " & dsAsesmen_31.SDIGITALRJ31_3
                'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsAsesmen_31.SDIGITALRJ31_29 & " " & dsAsesmen_31.SDIGITALRJ31_32
            End If

            'Asesmen Bedah
            Dim oAsesmen34 As New EMedrek.clsDigital_RJ_34
            Dim dsAsesmen_34 = oAsesmen34.GetData(sNoid)
            If dsAsesmen_34 IsNot Nothing Then
                txtANAMNESA.Text = dsAsesmen_34.SDIGITALRJ34_1 & " " & dsAsesmen_34.SDIGITALRJ34_2 & " " & dsAsesmen_34.SDIGITALRJ34_3
                'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsAsesmen_34.SDIGITALRJ34_31 & " " & dsAsesmen_34.SDIGITALRJ34_28
            End If

            'Dim oCPPT As New Digital.clsCPPT
            'Dim dsCPPT = oCPPT.GetDataKDREGProfesi(Parameter, "Dokter")

            'If dsCPPT IsNot Nothing Then
            '    If dsCPPT IsNot Nothing Then
            '        txtANAMNESA.Text = dsCPPT.SOAP_S
            '        txtPEMERIKSAANFISIK.Text = dsCPPT.SOAP_O
            '        'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsCPPT.SOAP_P
            '    End If
            'End If
        End If
    End Sub
    Private Sub fn_Doctor()
        Try
            Dim oDPJP As New Reference.clsDoctor

            Dim dsList = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True)

            grdDOKTERPJ.Properties.DataSource = dsList.ToList()
            grdDOKTERPJ.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERPJ.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERPELAKSANA.Properties.DataSource = dsList.ToList()
            grdDOKTERPELAKSANA.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERPELAKSANA.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERSUPERVISOR.Properties.DataSource = dsList.ToList()
            grdDOKTERSUPERVISOR.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERSUPERVISOR.Properties.DisplayMember = "NAME_DISPLAY"

            txtDOKTERPJ.Properties.DataSource = dsList.ToList()
            txtDOKTERPJ.Properties.ValueMember = "KDDOCTOR"
            txtDOKTERPJ.Properties.DisplayMember = "NAME_DISPLAY"

            txtDOKTERPELAKSANA.Properties.DataSource = dsList.ToList()
            txtDOKTERPELAKSANA.Properties.ValueMember = "KDDOCTOR"
            txtDOKTERPELAKSANA.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub mnuReload_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReload.ItemClick
        If sNoid <> "" Then
            fn_LoadResumeRawatJalanAwal(sNoid)
        End If
    End Sub
#End Region
End Class