Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmDischargePlanning
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oDigital_DischargePlanning As New EMedrek.clsDigital_DischargePlanning
    Private sNoId As String
    Private isLoad As Boolean = False
    Private sKDDOCTOR As String = String.Empty
    Private sRM As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private sTUJUAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, ByVal TUJUAN As String, ByVal KDDOCTOR As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sKDDOCTOR = KDDOCTOR
        sRM = KDCUSTOMER
        sKDKUNJUNGAN = KDKUNJUNGAN
        sTUJUAN = TUJUAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "CATATAN MEDIS AWAL RAWAT INAP DAN DISCHARGE PLANNING"

            btnSaveClose.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()

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
        'btnSaveNew.Enabled = False
        'btnSaveClose.Enabled = Not Status

        'btnSimpanLaporanOperasi.Enabled = Not Status

        'TableLayoutPanel9.Anchor = AnchorStyles.Top
        'TableLayoutPanel9.Anchor = AnchorStyles.Left
        'TableLayoutPanel9.Anchor = AnchorStyles.Right

        'GroupControl9.Anchor = AnchorStyles.Top
        'GroupControl9.Anchor = AnchorStyles.Left
        'GroupControl9.Anchor = AnchorStyles.Right
    End Sub
    Private Sub fn_EmptyMe()
        txtKLINIK.ResetText()
        deDATEAsesmen.DateTime = Now
        txtANAMNESIS_01.ResetText()
        txtANAMNESIS_02.ResetText()
        txtANAMNESIS_03.ResetText()
        'txtANAMNESIS_04.ResetText()
        cboKeadaanUmumAsesmen.ResetText()
        cboKesadaranAsesmen.ResetText()
        'txtANAMNESIS_07.ResetText()
        txtANAMNESIS_08.ResetText()
        txtANAMNESIS_09.ResetText()
        txtANAMNESIS_10.ResetText()
        txtANAMNESIS_11.ResetText()
        txtANAMNESIS_12.ResetText()
        txtANAMNESIS_13.ResetText()
        txtANAMNESIS_14.ResetText()
        txtSPO2_ASESMENMEDIS.ResetText()
        txtANAMNESIS_15.ResetText()
        txtANAMNESIS_16.ResetText()
        txtANAMNESIS_17.ResetText()
        txtANAMNESIS_18.ResetText()
        txtANAMNESIS_19.ResetText()
        txtANAMNESIS_20.ResetText()
        txtANAMNESIS_29.ResetText()
        txtANAMNESIS_30.ResetText()
        txtANAMNESIS_31.ResetText()
        txtANAMNESIS_32.ResetText()
        txtANAMNESIS_33.ResetText()
        txtANAMNESIS_34.ResetText()
        'txtANAMNESIS_35.ResetText()
        txtANAMNESIS_36.ResetText()
        txtANAMNESIS_37.ResetText()
        txtANAMNESIS_38.ResetText()
        cboRencanaPerawatan.ResetText()
        grdDPJP.Text = sKDDOCTOR

        chkANAMNESIS_15_1_CHEK.Checked = False
        chkANAMNESIS_16_1_CHEK.Checked = False
        chkANAMNESIS_17_1_CHEK.Checked = False
        chkANAMNESIS_18_1_CHEK.Checked = False
        chkANAMNESIS_19_1_CHEK.Checked = False
        chkANAMNESIS_20_1_CHEK.Checked = False
        chkANAMNESIS_15_2_CHEK.Checked = False
        chkANAMNESIS_16_2_CHEK.Checked = False
        chkANAMNESIS_17_2_CHEK.Checked = False
        chkANAMNESIS_18_2_CHEK.Checked = False
        chkANAMNESIS_19_2_CHEK.Checked = False
        chkANAMNESIS_20_2_CHEK.Checked = False

        'LoadAsesmenIGDDischargePlanning(sKDREGRAWATJALAN)
    End Sub
    Private Sub fn_LoadData()
        Try
            Try
                ' ***** HEADER *****
                Dim ds = oDigital_DischargePlanning.GetData(sNoId)

                With ds
                    txtKLINIK.Text = .ANAMNESIS_04
                    deDATEAsesmen.DateTime = .DATE
                    grdDPJP.Text = .DOCTOR_KODE
                    txtANAMNESIS_01.Text = .ANAMNESIS_01
                    txtANAMNESIS_02.Text = .ANAMNESIS_02
                    txtANAMNESIS_03.Text = .ANAMNESIS_03
                    'txtANAMNESIS_04.Text = .ANAMNESIS_04
                    cboKeadaanUmumAsesmen.Text = .ANAMNESIS_05
                    cboKesadaranAsesmen.Text = .ANAMNESIS_06
                    'txtANAMNESIS_07.Text = .ANAMNESIS_07
                    txtANAMNESIS_08.Text = .ANAMNESIS_08
                    txtANAMNESIS_09.Text = .ANAMNESIS_09
                    txtANAMNESIS_10.Text = .ANAMNESIS_10
                    txtANAMNESIS_11.Text = .ANAMNESIS_11
                    txtANAMNESIS_12.Text = .ANAMNESIS_12
                    txtANAMNESIS_13.Text = .ANAMNESIS_13
                    txtANAMNESIS_14.Text = .ANAMNESIS_14
                    txtSPO2_ASESMENMEDIS.Text = .ANAMNESIS_22
                    txtANAMNESIS_15.Text = .ANAMNESIS_15
                    txtANAMNESIS_16.Text = .ANAMNESIS_16
                    txtANAMNESIS_17.Text = .ANAMNESIS_17
                    txtANAMNESIS_18.Text = .ANAMNESIS_18
                    txtANAMNESIS_19.Text = .ANAMNESIS_19
                    txtANAMNESIS_20.Text = .ANAMNESIS_20
                    txtANAMNESIS_29.Text = .ANAMNESIS_29
                    txtANAMNESIS_30.Text = .ANAMNESIS_30
                    txtANAMNESIS_31.Text = .ANAMNESIS_31
                    txtANAMNESIS_32.Text = .ANAMNESIS_32
                    txtANAMNESIS_33.Text = .ANAMNESIS_33
                    txtANAMNESIS_34.Text = .ANAMNESIS_34
                    'txtANAMNESIS_35.Text = .ANAMNESIS_35
                    txtANAMNESIS_36.Text = .ANAMNESIS_36
                    txtANAMNESIS_37.Text = .ANAMNESIS_37
                    txtANAMNESIS_38.Text = .ANAMNESIS_38
                    cboRencanaPerawatan.Text = .HARI

                    chkANAMNESIS_15_1_CHEK.Checked = .ANAMNESIS_15_1_CHEK
                    chkANAMNESIS_16_1_CHEK.Checked = .ANAMNESIS_16_1_CHEK
                    chkANAMNESIS_17_1_CHEK.Checked = .ANAMNESIS_17_1_CHEK
                    chkANAMNESIS_18_1_CHEK.Checked = .ANAMNESIS_18_1_CHEK
                    chkANAMNESIS_19_1_CHEK.Checked = .ANAMNESIS_19_1_CHEK
                    chkANAMNESIS_20_1_CHEK.Checked = .ANAMNESIS_20_1_CHEK
                    chkANAMNESIS_15_2_CHEK.Checked = .ANAMNESIS_15_2_CHEK
                    chkANAMNESIS_16_2_CHEK.Checked = .ANAMNESIS_16_2_CHEK
                    chkANAMNESIS_17_2_CHEK.Checked = .ANAMNESIS_17_2_CHEK
                    chkANAMNESIS_18_2_CHEK.Checked = .ANAMNESIS_18_2_CHEK
                    chkANAMNESIS_19_2_CHEK.Checked = .ANAMNESIS_19_2_CHEK
                    chkANAMNESIS_20_2_CHEK.Checked = .ANAMNESIS_20_2_CHEK
                End With

                BindingSource.DataSource = oDigital_DischargePlanning.GetDataDetailDiagnosa(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaAsesmen.DataSource = BindingSource

            Catch oErr As Exception
                MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoId = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If cboRencanaPerawatan.Text = String.Empty Then
                MsgBox("Dibutuhkan Rencana Perawatan", MsgBoxStyle.Exclamation, Me.Text)
                cboRencanaPerawatan.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDigital_DischargePlanning.GetStructureHeader
            With ds
                .KDREG = sNoId
                .KDCUSTOMER = sRM
                Try
                    .DATECREATED = oDigital_DischargePlanning.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATEAsesmen.DateTime
                .JAM = deDATEAsesmen.DateTime.ToString("HH:mm")
                .DOCTOR_KODE = grdDPJP.EditValue
                .DOCTOR_NAME_DISPLAY = grdDPJP.Text
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = txtKLINIK.Text
                .ANAMNESIS_05 = cboKeadaanUmumAsesmen.Text
                .ANAMNESIS_06 = cboKesadaranAsesmen.Text
                .ANAMNESIS_07 = ""
                .ANAMNESIS_08 = txtANAMNESIS_08.Text
                .ANAMNESIS_09 = txtANAMNESIS_09.Text
                .ANAMNESIS_10 = txtANAMNESIS_10.Text
                .ANAMNESIS_11 = txtANAMNESIS_11.Text
                .ANAMNESIS_12 = txtANAMNESIS_12.Text
                .ANAMNESIS_13 = txtANAMNESIS_13.Text
                .ANAMNESIS_14 = txtANAMNESIS_14.Text
                .ANAMNESIS_15 = txtANAMNESIS_15.Text
                .ANAMNESIS_16 = txtANAMNESIS_16.Text
                .ANAMNESIS_17 = txtANAMNESIS_17.Text
                .ANAMNESIS_18 = txtANAMNESIS_18.Text
                .ANAMNESIS_19 = txtANAMNESIS_19.Text
                .ANAMNESIS_20 = txtANAMNESIS_20.Text
                Try
                    Dim getdata = oDigital_DischargePlanning.GetData(sNoId)
                    .ANAMNESIS_21 = getdata.ANAMNESIS_15 & ", " & getdata.ANAMNESIS_16 & ", " & getdata.ANAMNESIS_17 & ", " & getdata.ANAMNESIS_18 & ", " & getdata.ANAMNESIS_19 & ", " & getdata.ANAMNESIS_20
                Catch ex As Exception
                    .ANAMNESIS_21 = ""
                End Try
                .ANAMNESIS_22 = txtSPO2_ASESMENMEDIS.Text
                .ANAMNESIS_23 = sKDKUNJUNGAN
                .ANAMNESIS_24 = sTUJUAN
                .ANAMNESIS_25 = ""
                .ANAMNESIS_26 = ""
                .ANAMNESIS_27 = ""
                .ANAMNESIS_28 = ""
                .ANAMNESIS_29 = txtANAMNESIS_29.Text
                .ANAMNESIS_30 = txtANAMNESIS_30.Text
                .ANAMNESIS_31 = txtANAMNESIS_31.Text
                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text

                Try
                    .ANAMNESIS_35 = oDigital_DischargePlanning.GetData(sNoId).ANAMNESIS_35
                Catch ex As Exception
                    .ANAMNESIS_35 = ""
                End Try

                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text
                Try
                    .CETAK = oDigital_DischargePlanning.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
                .HARI = cboRencanaPerawatan.Text
                .ANAMNESIS_15_1_CHEK = chkANAMNESIS_15_1_CHEK.Checked
                .ANAMNESIS_16_1_CHEK = chkANAMNESIS_16_1_CHEK.Checked
                .ANAMNESIS_17_1_CHEK = chkANAMNESIS_17_1_CHEK.Checked
                .ANAMNESIS_18_1_CHEK = chkANAMNESIS_18_1_CHEK.Checked
                .ANAMNESIS_19_1_CHEK = chkANAMNESIS_19_1_CHEK.Checked
                .ANAMNESIS_20_1_CHEK = chkANAMNESIS_20_1_CHEK.Checked
                .ANAMNESIS_15_2_CHEK = chkANAMNESIS_15_2_CHEK.Checked
                .ANAMNESIS_16_2_CHEK = chkANAMNESIS_16_2_CHEK.Checked
                .ANAMNESIS_17_2_CHEK = chkANAMNESIS_17_2_CHEK.Checked
                .ANAMNESIS_18_2_CHEK = chkANAMNESIS_18_2_CHEK.Checked
                .ANAMNESIS_19_2_CHEK = chkANAMNESIS_19_2_CHEK.Checked
                .ANAMNESIS_20_2_CHEK = chkANAMNESIS_20_2_CHEK.Checked
            End With

            'DIAGNOSA
            Dim listdiagnosa As New List(Of String)

            Dim arrDetailDiagnosa = oDigital_DischargePlanning.GetStructureDetaiDiagnosalList
            For i As Integer = 0 To grvDiagnosaAsesmen.RowCount - 2
                Dim dsDetail = oDigital_DischargePlanning.GetStructureDetailDiagnosa
                With dsDetail
                    .SEQ = i
                    .KDREG = ds.KDREG
                    .KATEGORI = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colKATEGORIASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colKATEGORIASESMEN))
                    .NAMADIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colNAMADIAGNOSAASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colNAMADIAGNOSAASESMEN))
                    .KDDIAGNOSA = IIf(String.IsNullOrEmpty(grvDiagnosaAsesmen.GetRowCellValue(i, colKDDIAGNOSAASESMEN)), "", grvDiagnosaAsesmen.GetRowCellValue(i, colKDDIAGNOSAASESMEN))
                End With
                arrDetailDiagnosa.Add(dsDetail)
            Next

            For Each xloop In arrDetailDiagnosa
                listdiagnosa.Add(xloop.KATEGORI & " " & xloop.KDDIAGNOSA & " " & xloop.NAMADIAGNOSA)
            Next

            Dim dsSave = oDigital_DischargePlanning.GetData(sNoId)

            If dsSave Is Nothing Then
                Try
                    ds.ANAMNESIS_35 = String.Join(vbCrLf, listdiagnosa.ToArray)
                    fn_Save = oDigital_DischargePlanning.InsertData(ds, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    ds.ANAMNESIS_35 = String.Join(vbCrLf, listdiagnosa.ToArray)
                    fn_Save = oDigital_DischargePlanning.UpdateData(ds, arrDetailDiagnosa)
                Catch ex As Exception
                    MsgBox("Simpan Data Discharge Planning: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data Discharge Planning: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F2
        '        If btnSaveNew.Enabled = True Then
        '            btnSaveNew_Click()
        '        End If
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    'Private Sub btnClose_Click() Handles btnClose.ItemClick
    '    Me.Close()
    'End Sub
    Private Sub chkKeadaanUmumAsesmenRingan_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenRingan.Click
        chkKeadaanUmumAsesmenSedang.Checked = False
        chkKeadaanUmumAsesmenBerat.Checked = False

        cboKeadaanUmumAsesmen.Text = "Ringan"
    End Sub
    Private Sub chkKeadaanUmumAsesmenSedang_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenSedang.Click
        chkKeadaanUmumAsesmenRingan.Checked = False
        chkKeadaanUmumAsesmenBerat.Checked = False

        cboKeadaanUmumAsesmen.Text = "Sedang"
    End Sub
    Private Sub chkKeadaanUmumAsesmenBerat_Click(sender As Object, e As EventArgs) Handles chkKeadaanUmumAsesmenBerat.Click
        chkKeadaanUmumAsesmenRingan.Checked = False
        chkKeadaanUmumAsesmenSedang.Checked = False

        cboKeadaanUmumAsesmen.Text = "Berat"
    End Sub
    Private Sub chkKesadaranAsesmenComposMentis_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenComposMentis.Click
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenSopor.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Compos Mentis"
    End Sub
    Private Sub chkKesadaranAsesmenSomnolen_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenSomnolen.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSopor.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Somnolen"
    End Sub
    Private Sub chkKesadaranAsesmenSopor_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenSopor.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenKoma.Checked = False

        cboKesadaranAsesmen.Text = "Sopor"
    End Sub
    Private Sub chkKesadaranAsesmenKoma_Click(sender As Object, e As EventArgs) Handles chkKesadaranAsesmenKoma.Click
        chkKesadaranAsesmenComposMentis.Checked = False
        chkKesadaranAsesmenSomnolen.Checked = False
        chkKesadaranAsesmenSopor.Checked = False

        cboKesadaranAsesmen.Text = "Koma"
    End Sub
    Private Sub LoadAsesmenIGDDischargePlanning()
        Dim oS_DIGITAL_IGD_01 As New Transaksi.clsDigital_IGD_01

        Try
            Dim dsTerakhir = oS_DIGITAL_IGD_01.GetDataByRMTerakhir(sRM)

            If dsTerakhir IsNot Nothing Then
                txtANAMNESIS_01.Text = dsTerakhir.RIWAYAT_PENYAKITDAHULU
                txtANAMNESIS_01.Text = dsTerakhir.SURVEY_LEHER_1
                txtANAMNESIS_03.Text = dsTerakhir.RIWAYAT
                txtANAMNESIS_02.Text = dsTerakhir.RIWAYAT_PENYAKITDAHULU
                cboKeadaanUmumAsesmen.Text = dsTerakhir.KEADAANUMUM
                cboKesadaranAsesmen.Text = dsTerakhir.TINGKATKESADARAN
                txtANAMNESIS_08.Text = dsTerakhir.BERATBADAN
                txtANAMNESIS_09.Text = dsTerakhir.TINGGIBADAN
                txtANAMNESIS_11.Text = dsTerakhir.HR
                txtANAMNESIS_12.Text = dsTerakhir.BP
                txtANAMNESIS_13.Text = dsTerakhir.T
                txtANAMNESIS_14.Text = dsTerakhir.RR
                txtSPO2_ASESMENMEDIS.Text = dsTerakhir.SP02
                Dim list As New List(Of String)

                For Each xloop In oS_DIGITAL_IGD_01.GetDataDetailPenunjang(dsTerakhir.KDPENDAFTARAN)
                    list.Add(xloop.PENANGANAN)
                Next

                txtANAMNESIS_33.Text = String.Join(", ", list.ToArray)

                chkANAMNESIS_15_1_CHEK.Checked = dsTerakhir.NORMAL_KEPALA
                chkANAMNESIS_15_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_KEPALA

                chkANAMNESIS_16_1_CHEK.Checked = dsTerakhir.NORMAL_MATA
                chkANAMNESIS_16_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_MATA

                chkANAMNESIS_17_1_CHEK.Checked = dsTerakhir.NORMAL_LEHER
                chkANAMNESIS_17_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_LEHER

                chkANAMNESIS_18_1_CHEK.Checked = dsTerakhir.NORMAL_DADA
                chkANAMNESIS_18_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_DADA

                chkANAMNESIS_19_1_CHEK.Checked = dsTerakhir.NORMAL_PERUT
                chkANAMNESIS_19_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_PERUT

                chkANAMNESIS_20_1_CHEK.Checked = dsTerakhir.NORMAL_ALATGERAK
                chkANAMNESIS_20_2_CHEK.Checked = dsTerakhir.TIDAK_NORMAL_ALATGERAK

                txtANAMNESIS_15.Text = dsTerakhir.SURVEY_KEPALA_2
                txtANAMNESIS_16.Text = dsTerakhir.SURVEY_MATA_2
                txtANAMNESIS_17.Text = dsTerakhir.SURVEY_LEHER_2
                txtANAMNESIS_18.Text = dsTerakhir.SURVEY_DADA_2
                txtANAMNESIS_19.Text = dsTerakhir.SURVEY_PERUT_2
                txtANAMNESIS_20.Text = dsTerakhir.SURVEY_ALATGERAK_2

                BindingSource.DataSource = oS_DIGITAL_IGD_01.GetDataDetail(dsTerakhir.KDPENDAFTARAN).OrderBy(Function(x) x.SEQ).ToList()
                grdDiagnosaAsesmen.DataSource = BindingSource
            End If
        Catch oErr As Exception
            MsgBox("Form Browse Load Assemen Medis Awal IGD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    'Private Sub fn_CopyCPPTAkhirdiAsesmenAwalTTVPerawat(ByVal Parameter As String)
    '    Try
    '        ' ***** HEADER *****
    '        Dim dsLainnya = oCPPT.GetDataLainnya(Parameter)

    '        If dsLainnya IsNot Nothing Then
    '            txtANAMNESIS_08.Text = dsLainnya.OBJEKTIF_BERATBADAN
    '            txtANAMNESIS_09.Text = dsLainnya.OBJEKTIF_TINGGIBADAN

    '            'txtANAMNESIS_11.Text = dsLainnya.OBJEKTIF_TEKANANDARAH
    '            'txtANAMNESIS_12.Text = dsLainnya.OBJEKTIF_NADI

    '            txtANAMNESIS_11.Text = dsLainnya.OBJEKTIF_NADI
    '            txtANAMNESIS_12.Text = dsLainnya.OBJEKTIF_TEKANANDARAH

    '            txtANAMNESIS_13.Text = dsLainnya.OBJEKTIF_SUHU
    '            txtANAMNESIS_14.Text = dsLainnya.OBJEKTIF_RESPIRASI
    '            txtSPO2_ASESMENMEDIS.Text = dsLainnya.OBJEKTIF_SATURASIOKSIGEN
    '        End If

    '    Catch oErr As Exception
    '        MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdDPJP.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnAsesmenIGd_Click(sender As Object, e As EventArgs) Handles btnAsesmenIGd.Click
        LoadAsesmenIGDDischargePlanning()
    End Sub
#End Region
End Class