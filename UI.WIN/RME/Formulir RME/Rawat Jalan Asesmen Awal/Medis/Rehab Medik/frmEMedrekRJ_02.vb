Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_02
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_02 As New Digital.clsDigital_RJ_02
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
            txtNIK.Text = dsPendaftaran.NIK
            txtTmpTglLahir.Text = dsPendaftaran.TEMPATLAHIR & ", " & dsPendaftaran.TANGGALLAHIR
            txtAgama.Text = dspendaftaran.AGAMA
            txtPangkatGol.Text = dsPendaftaran.PANGKAT
            txtNRPNIP.Text = dsPendaftaran.NRP
            txtKesatuan.Text = dsPendaftaran.KESATUAN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA

            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtPendidikan.Text = dsPendaftaran.PENDIDIKAN
            txtSukuBangsa.Text = dspendaftaran.SUKU
            txtTanggalMasuk.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy")
            txtJam.Text = dsPendaftaran.DATE.ToString("HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT
            txtTujuan.Text = dsPendaftaran.TUJUAN
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER

        Else
            txtNamaPasien.ResetText()
            txtNIK.ResetText()
            txtTmpTglLahir.ResetText()
            txtAgama.ResetText()
            txtPangkatGol.ResetText()
            txtNRPNIP.ResetText()
            txtKesatuan.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtNoRM.ResetText()
            txtNoTelepon.ResetText()
            txtPendidikan.ResetText()
            txtSukuBangsa.ResetText()
            txtTanggalMasuk.ResetText()
            txtJam.ResetText()
            txtAlamat.ResetText()
            txtTujuan.ResetText()
            txtDR.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        GroupControl1.Text = "Data Pasien - ASESMEN AWAL REHABILITASI MEDIK"
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
        fn_NOIDUSER()

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

        chkRujukanYa.Properties.ReadOnly = Status
        chkRS.Properties.ReadOnly = Status
        txtRS.ReadOnly = Status
        chkPuskesmas.Properties.ReadOnly = Status
        txtPuskesmas.ReadOnly = Status
        chkDR.Properties.ReadOnly = Status
        txtDR.ReadOnly = Status
        chkLainnya.Properties.ReadOnly = Status
        txtLainnya.ReadOnly = Status
        txtDXRujukan.Properties.ReadOnly = Status
        chkTIdak.Properties.ReadOnly = Status
        chkDatangSendiri.Properties.ReadOnly = Status
        chkDiantar.Properties.ReadOnly = Status
        chkAUTO.Properties.ReadOnly = Status
        chkALLO.Properties.ReadOnly = Status
        txtAnamnesis.Properties.ReadOnly = Status
        txtPemeriksaanDanUjiFungsi.Properties.ReadOnly = Status
        txtDiagnosisMedisICD10.Properties.ReadOnly = Status
        txtDiagnosisFungsiICD10.Properties.ReadOnly = Status
        txtPemeriksaanPenunjang.Properties.ReadOnly = Status
        txtTataLaksanaKFR.Properties.ReadOnly = Status
        txtAnjuran.Properties.ReadOnly = Status
        txtEvaluasi.Properties.ReadOnly = Status
        'txtNoPasien.Properties.ReadOnly = False
        'txtNamaPasien.Properties.ReadOnly = False
        'txtUmur.Properties.ReadOnly = False
        'txtNoRegister.Properties.ReadOnly = False
        'txtTujuan.Properties.ReadOnly = False
        'txtTanggalDaftar.Properties.ReadOnly = False
        'txtDokter.Properties.ReadOnly = False


    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        chkRujukanYa.Checked = False
        chkRS.Checked = False
        txtRS.ResetText()
        chkPuskesmas.Checked = False
        txtPuskesmas.ResetText()
        chkDR.Checked = False
        txtDR.ResetText()
        chkLainnya.Checked = False
        txtLainnya.ResetText()
        txtDXRujukan.ResetText()
        chkTIdak.Checked = False
        chkDatangSendiri.Checked = False
        chkDiantar.Checked = False
        chkAUTO.Checked = False
        chkALLO.Checked = False
        txtAnamnesis.ResetText()
        txtPemeriksaanDanUjiFungsi.ResetText()
        txtDiagnosisMedisICD10.ResetText()
        txtDiagnosisFungsiICD10.ResetText()
        txtPemeriksaanPenunjang.ResetText()
        txtTataLaksanaKFR.ResetText()
        txtAnjuran.ResetText()
        txtEvaluasi.ResetText()

        txtAnamnesis.ResetText()
        txtPemeriksaanDanUjiFungsi.ResetText()
        txtDiagnosisMedisICD10.ResetText()
        txtDiagnosisFungsiICD10.ResetText()
        txtPemeriksaanPenunjang.ResetText()
        txtTataLaksanaKFR.ResetText()
        txtAnjuran.ResetText()
        txtEvaluasi.ResetText()
        'txtNoPasien.ResetText()
        'txtNamaPasien.ResetText()
        'txtUmur.ResetText()
        'txtNoRegister.ResetText()
        'txtTujuan.ResetText()
        'txtTanggalDaftar.ResetText()
        'txtDokter.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_02.GetData(txtNoRegister.Text)

            With ds
                chkRujukanYa.Checked = .RUJUKAN
                chkRS.Checked = .DARIRSBIT
                txtRS.Text = .DARIRS
                chkPuskesmas.Checked = .DARIPUSKESMASBIT
                txtPuskesmas.Text = .DARIPUSKESMAS
                chkDR.Checked = .DARIDRBIT
                txtDR.Text = .DARIDR
                chkLainnya.Checked = .DARILAINNYABIT
                txtLainnya.Text = .DARILAINNYA
                txtDXRujukan.Text = .DXRUJUKAN
                chkTIdak.Checked = .DXRUJUKANTIDAK
                chkDatangSendiri.Checked = .DXRUJUKANDATANGSENDIRI
                chkDiantar.Checked = .DXRUJUKANDIANTAR
                chkAUTO.Checked = .AUTO
                chkALLO.Checked = .ALLO
                txtAnamnesis.Text = .ANAMNESIS
                txtPemeriksaanDanUjiFungsi.Text = .PEMERIKSAANFISIK_DAN_UJIFUNGSI
                txtDiagnosisMedisICD10.Text = .DIAGNOSISMEDIS_ICD10
                txtDiagnosisFungsiICD10.Text = .DIAGNOSISFUNGSI_ICD10
                txtPemeriksaanPenunjang.Text = .PEMERIKSAANPENUNJANG
                txtTataLaksanaKFR.Text = .TATALAKSANA_KFR_ICD9CM
                txtAnjuran.Text = .AJNURAN
                txtEvaluasi.Text = .EVALUASI
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
            'If grdNOIDUSER.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Petugas Triage", MsgBoxStyle.Exclamation, Me.Text)
            '    grdNOIDUSER.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_02.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_02.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .RUJUKAN = chkRujukanYa.Checked
                .DARIRSBIT = chkRS.Checked
                .DARIRS = txtRS.Text
                .DARIPUSKESMASBIT = chkPuskesmas.Checked
                .DARIPUSKESMAS = txtPuskesmas.Text
                .DARIDRBIT = chkDR.Checked
                .DARIDR = txtDR.Text
                .DARILAINNYABIT = chkLainnya.Checked
                .DARILAINNYA = txtLainnya.Text
                .DXRUJUKAN = txtDXRujukan.Text
                .DXRUJUKANTIDAK = chkTIdak.Checked
                .DXRUJUKANDATANGSENDIRI = chkDatangSendiri.Checked
                .DXRUJUKANDIANTAR = chkDiantar.Checked
                .AUTO = chkAUTO.Checked
                .ALLO = chkALLO.Checked
                .ANAMNESIS = txtAnamnesis.Text
                .PEMERIKSAANFISIK_DAN_UJIFUNGSI = txtPemeriksaanDanUjiFungsi.Text
                .DIAGNOSISMEDIS_ICD10 = txtDiagnosisMedisICD10.Text
                .DIAGNOSISFUNGSI_ICD10 = txtDiagnosisFungsiICD10.Text
                .PEMERIKSAANPENUNJANG = txtPemeriksaanPenunjang.Text
                .TATALAKSANA_KFR_ICD9CM = txtTataLaksanaKFR.Text
                .AJNURAN = txtAnjuran.Text
                .EVALUASI = txtEvaluasi.Text
                
                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = sNAMADOKTER
                Try
                    .CETAK = oS_DIGITAL_RJ_02.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_02.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_02.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_02.GetDataByKunjungan(txtNoRegister.Text)

        Dim listPenunjang As New List(Of String)
        Dim listTindakanPengobatan As New List(Of String)

        If dsKunjungan IsNot Nothing Then
            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim oOrderLab As New Inventory.clsOrderLab
            Dim oOrderRad As New Inventory.clsOrderRad
            Dim oKonsul As New Digital.clsKonsul
            Dim oKonsulJawab As New Digital.clsJawabKonsul

            Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                             Join y In oOrderTindakan.GetDataDetail()
                             On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
                             Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderLab.GetDataDetail()
                        On x.KDORDERLAB Equals y.KDORDERLAB
                        Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderRad.GetDataDetail()
                        On x.KDORDERRAD Equals y.KDORDERRAD
                        Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                           Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                                Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsUnionPenunjang = dsLab.Union(dsRad)

            For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
                listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

            For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
                listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim oResep As New Inventory.clsOrderResep

            Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

            For Each xloop In dsResep
                For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
                    listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Next
            Next

            txtPemeriksaanPenunjang.Text = String.Join(", ", listPenunjang.ToArray)
            'MemoEdit4.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
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
    Private Sub fn_NOIDUSER()
        'Dim oUser As New Setting.clsUser
        'Try
        '    grdNOIDUSER.Properties.DataSource = oUser.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdNOIDUSER.Properties.ValueMember = "NOIDUSER"
        '    grdNOIDUSER.Properties.DisplayMember = "NOIDUSER"
        'Catch oErr As Exception
        '    MsgBox("Load Sub Spesialis Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub txtALLO_CheckedChanged(sender As Object, e As EventArgs) Handles chkALLO.CheckedChanged

    End Sub



#End Region
End Class