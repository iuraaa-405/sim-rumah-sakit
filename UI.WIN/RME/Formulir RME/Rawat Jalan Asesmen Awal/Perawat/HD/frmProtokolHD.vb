Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmProtokolHD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_PROTOKOLHD As New Digital.clsDigital_PROTOKOLHD
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sKDKUNJUNGAN As String = String.Empty

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
            txtAlamat.Text = dsPendaftaran.KESATUAN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
            txtJam.Text = DateTime.Now.ToString("HH:mm")
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN
        Else
            txtNamaPasien.ResetText()
            txtTmpTglLahir.ResetText()
            txtAlamat.ResetText()
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

        txtTanggal.Properties.ReadOnly = Status
        txtJam.Properties.ReadOnly = Status
        txtANAMNESA.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIK.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        txtTATALAKSANA.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
        txtJam.Text = DateTime.Now.ToString("HH:mm")
        txtANAMNESA.ResetText()
        txtPEMERIKSAANFISIK.ResetText()
        txtDIAGNOSA.ResetText()
        txtTATALAKSANA.ResetText()

        If sKDKUNJUNGAN <> "" Then
            fn_LoadResumeRawatJalanAwal(sKDKUNJUNGAN)
        End If

        'Dim oKunjungan As New Identitas.clsIdentitasPasien
        'Dim dsKunjungan = oKunjungan.GetData(sKDKUNJUNGAN)
        'If dsKunjungan IsNot Nothing Then
        '    fn_LoadDataSKD(dsKunjungan.KDPENDAFTARAN)
        'End If

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_PROTOKOLHD.GetData(txtNoRegister.Text)

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
            Dim ds = oS_DIGITAL_PROTOKOLHD.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_PROTOKOLHD.GetData(txtNoRegister.Text).DATECREATED
                    .DATE = oS_DIGITAL_PROTOKOLHD.GetData(txtNoRegister.Text).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    .DATE = Now
                End Try
                .DATEUPDATED = Now

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
                    .CETAK = oS_DIGITAL_PROTOKOLHD.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
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
    Private Sub fn_LoadResumeRawatJalanAwal(ByVal Parameter As String)
        Dim oDiagnosaMaster As New Diagnosa.clsMasterDiagnosa
        Dim dsDiagnosa = oDiagnosaMaster.GetData(Parameter)
        Dim oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01

        If dsDiagnosa IsNot Nothing Then
            txtDIAGNOSA.Text = dsDiagnosa.REMARKS
            txtTATALAKSANA.Text = dsDiagnosa.REMARKS_
        End If

        Dim dsIGD = oS_DIGITAL_IGD_01.GetData(sKDKUNJUNGAN)
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
            Dim oAsesmen31 As New Digital.clsDigital_RJ_31
            Dim dsAsesmen_31 = oAsesmen31.GetData(sKDKUNJUNGAN)
            If dsAsesmen_31 IsNot Nothing Then
                txtANAMNESA.Text = dsAsesmen_31.SDIGITALRJ31_1 & " " & dsAsesmen_31.SDIGITALRJ31_2 & " " & dsAsesmen_31.SDIGITALRJ31_3
                'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsAsesmen_31.SDIGITALRJ31_29 & " " & dsAsesmen_31.SDIGITALRJ31_32
            End If

            'Asesmen Bedah
            Dim oAsesmen34 As New Digital.clsDigital_RJ_34
            Dim dsAsesmen_34 = oAsesmen34.GetData(sKDKUNJUNGAN)
            If dsAsesmen_34 IsNot Nothing Then
                txtANAMNESA.Text = dsAsesmen_34.SDIGITALRJ34_1 & " " & dsAsesmen_34.SDIGITALRJ34_2 & " " & dsAsesmen_34.SDIGITALRJ34_3
                'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsAsesmen_34.SDIGITALRJ34_31 & " " & dsAsesmen_34.SDIGITALRJ34_28
            End If

            Dim oCPPT As New Digital.clsCPPT
            Dim dsCPPT = oCPPT.GetDataKDREGProfesi(Parameter, "Dokter")

            If dsCPPT IsNot Nothing Then
                If dsCPPT IsNot Nothing Then
                    txtANAMNESA.Text = dsCPPT.SOAP_S
                    txtPEMERIKSAANFISIK.Text = dsCPPT.SOAP_O
                    'txtTERAPI.Text = txtTERAPI.Text & vbCrLf & dsCPPT.SOAP_P
                End If
            End If
        End If
    End Sub

    

        Private Sub fn_Doctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.150;Initial Catalog=DUSTIRA_FARMASI;Persist Security Info=True;User ID=sa;Password=dust1r@@"
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOKTERPJ.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTERPJ.Properties.ValueMember = "KDSTAFF"
            grdDOKTERPJ.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERPELAKSANA.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTERPELAKSANA.Properties.ValueMember = "KDSTAFF"
            grdDOKTERPELAKSANA.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOKTERSUPERVISOR.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTERSUPERVISOR.Properties.ValueMember = "KDSTAFF"
            grdDOKTERSUPERVISOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub mnuReload_ItemClick(sender As Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles mnuReload.ItemClick
        If sKDKUNJUNGAN <> "" Then
            fn_LoadResumeRawatJalanAwal(sKDKUNJUNGAN)
        End If
    End Sub
#End Region
End Class