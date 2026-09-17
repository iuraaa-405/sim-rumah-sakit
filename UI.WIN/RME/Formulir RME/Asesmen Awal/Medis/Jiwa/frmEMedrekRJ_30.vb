Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_30
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_30 As New EMedrek.clsDigital_RJ_30
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
        Me.Text = "ASESMEN AWAL MEDIS PASIEN PSIKIATRIK"
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
        fn_DOCTOR()

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
        txt1.Properties.ReadOnly = Status
        txt2.Properties.ReadOnly = Status
        txt3.Properties.ReadOnly = Status
        txt4.Properties.ReadOnly = Status
        txt5.Properties.ReadOnly = Status
        txt6.Properties.ReadOnly = Status
        txt7.Properties.ReadOnly = Status
        txt8.Properties.ReadOnly = Status
        txt9.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        'CheckEdit1.Properties.ReadOnly = Status
        'CheckEdit2.Properties.ReadOnly = Status
        'CheckEdit4.Properties.ReadOnly = Status
        'CheckEdit3.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        'CheckEdit8.Properties.ReadOnly = Status
        'CheckEdit7.Properties.ReadOnly = Status
        'CheckEdit6.Properties.ReadOnly = Status
        'CheckEdit5.Properties.ReadOnly = Status
        'TextEdit26.Properties.ReadOnly = Status
        'TextEdit25.Properties.ReadOnly = Status
        'TextEdit24.Properties.ReadOnly = Status
        'TextEdit1.Properties.ReadOnly = Status
        'TextEdit2.Properties.ReadOnly = Status
        'TextEdit3.Properties.ReadOnly = Status
        'TextEdit8.Properties.ReadOnly = Status
        'TextEdit9.Properties.ReadOnly = Status
        'TextEdit10.Properties.ReadOnly = Status
        'TextEdit11.Properties.ReadOnly = Status
        'TextEdit12.Properties.ReadOnly = Status
        'TextEdit13.Properties.ReadOnly = Status
        'TextEdit14.Properties.ReadOnly = Status
        'TextEdit15.Properties.ReadOnly = Status
        'TextEdit16.Properties.ReadOnly = Status
        'TextEdit17.Properties.ReadOnly = Status
        'TextEdit18.Properties.ReadOnly = Status
        'TextEdit19.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status

        txtRIWAYATMEDIPSIKIATRILAIN.Properties.ReadOnly = Status
        txtPEMERIKSAANSTATUSPSIKIATRI.Properties.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        'lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        'lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '        sIsOtority = False
        '    End If
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        txt1.ResetText()
        txt2.ResetText()
        txt3.ResetText()
        txt4.ResetText()
        txt5.ResetText()
        txt6.ResetText()
        txt7.ResetText()
        txt8.ResetText()
        txt9.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit4.Checked = False
        CheckEdit3.Checked = False
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()
        CheckEdit8.Checked = False
        CheckEdit7.Checked = False
        CheckEdit6.Checked = False
        CheckEdit5.Checked = False
        TextEdit26.ResetText()
        TextEdit25.ResetText()
        TextEdit24.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()

        txtRIWAYATMEDIPSIKIATRILAIN.ResetText()
        txtPEMERIKSAANSTATUSPSIKIATRI.ResetText()

        deDATE.DateTime = Now
        grdDOCTOR.Text = sKDDOCTOR

        fn_LoadAsessmenRawatJalan(sNoid)

    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New EMedrek.clsDigital_RJ_24
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            txt1.Text = dsAsessmenRawatJalan.SDIGITALRJ24_04
            txt5.Text = dsAsessmenRawatJalan.SDIGITALRJ24_05
            txt7.Text = dsAsessmenRawatJalan.SDIGITALRJ24_12
            txt8.Text = dsAsessmenRawatJalan.SDIGITALRJ24_13
            TextEdit4.Text = dsAsessmenRawatJalan.SDIGITALRJ24_07
            TextEdit5.Text = dsAsessmenRawatJalan.SDIGITALRJ24_06
            TextEdit6.Text = dsAsessmenRawatJalan.SDIGITALRJ24_08
            TextEdit7.Text = dsAsessmenRawatJalan.SDIGITALRJ24_09
        Else
            'MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_30.GetData(sNoid)

            With ds
                txt1.Text = .SDIGITALRJ30_1
                txt2.Text = .SDIGITALRJ30_2
                txt3.Text = .SDIGITALRJ30_3
                txt4.Text = .SDIGITALRJ30_4
                txt5.Text = .SDIGITALRJ30_5
                txt6.Text = .SDIGITALRJ30_6
                txt7.Text = .SDIGITALRJ30_7
                txt8.Text = .SDIGITALRJ30_8
                txt9.Text = .SDIGITALRJ30_9
                TextEdit4.Text = .SDIGITALRJ30_10
                TextEdit5.Text = .SDIGITALRJ30_11
                TextEdit6.Text = .SDIGITALRJ30_12
                TextEdit7.Text = .SDIGITALRJ30_13
                CheckEdit1.Checked = .SDIGITALRJ30_14
                CheckEdit2.Checked = .SDIGITALRJ30_15
                CheckEdit4.Checked = .SDIGITALRJ30_16
                CheckEdit3.Checked = .SDIGITALRJ30_17
                MemoEdit7.Text = .SDIGITALRJ30_18
                MemoEdit8.Text = .SDIGITALRJ30_19
                CheckEdit8.Checked = .SDIGITALRJ30_20
                CheckEdit7.Checked = .SDIGITALRJ30_21
                CheckEdit6.Checked = .SDIGITALRJ30_22
                CheckEdit5.Checked = .SDIGITALRJ30_23
                TextEdit26.Text = .SDIGITALRJ30_24
                TextEdit25.Text = .SDIGITALRJ30_25
                TextEdit24.Text = .SDIGITALRJ30_26
                TextEdit1.Text = .SDIGITALRJ30_27
                TextEdit2.Text = .SDIGITALRJ30_28
                TextEdit3.Text = .SDIGITALRJ30_29
                TextEdit8.Text = .SDIGITALRJ30_30
                TextEdit9.Text = .SDIGITALRJ30_31
                TextEdit10.Text = .SDIGITALRJ30_32
                TextEdit11.Text = .SDIGITALRJ30_33
                TextEdit12.Text = .SDIGITALRJ30_34
                TextEdit13.Text = .SDIGITALRJ30_35
                TextEdit14.Text = .SDIGITALRJ30_36
                TextEdit15.Text = .SDIGITALRJ30_37
                TextEdit16.Text = .SDIGITALRJ30_38
                TextEdit17.Text = .SDIGITALRJ30_39
                TextEdit18.Text = .SDIGITALRJ30_40
                TextEdit19.Text = .SDIGITALRJ30_41
                MemoEdit1.Text = .SDIGITALRJ30_42
                MemoEdit2.Text = .SDIGITALRJ30_43
                MemoEdit3.Text = .SDIGITALRJ30_44
                MemoEdit4.Text = .SDIGITALRJ30_45
                MemoEdit5.Text = .SDIGITALRJ30_46
                MemoEdit6.Text = .SDIGITALRJ30_47

                txtRIWAYATMEDIPSIKIATRILAIN.Text = .RIWAYATMEDIPSIKIATRILAIN
                txtPEMERIKSAANSTATUSPSIKIATRI.Text = .PEMERIKSAANSTATUSPSIKIATRI

                deDATE.DateTime = .DATE
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
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.Focus()
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
            Dim ds = oS_DIGITAL_RJ_30.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                .KDCUSTOMER = ""
                .TUJUAN = ""
                Try
                    .DATECREATED = oS_DIGITAL_RJ_30.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .SDIGITALRJ30_1 = txt1.Text
                .SDIGITALRJ30_2 = txt2.Text
                .SDIGITALRJ30_3 = txt3.Text
                .SDIGITALRJ30_4 = txt4.Text
                .SDIGITALRJ30_5 = txt5.Text
                .SDIGITALRJ30_6 = txt6.Text
                .SDIGITALRJ30_7 = txt7.Text
                .SDIGITALRJ30_8 = txt8.Text
                .SDIGITALRJ30_9 = txt9.Text
                .SDIGITALRJ30_10 = TextEdit4.Text
                .SDIGITALRJ30_11 = TextEdit5.Text
                .SDIGITALRJ30_12 = TextEdit6.Text
                .SDIGITALRJ30_13 = TextEdit7.Text
                .SDIGITALRJ30_14 = CheckEdit1.Checked
                .SDIGITALRJ30_15 = CheckEdit2.Checked
                .SDIGITALRJ30_16 = CheckEdit4.Checked
                .SDIGITALRJ30_17 = CheckEdit3.Checked
                .SDIGITALRJ30_18 = MemoEdit7.Text
                .SDIGITALRJ30_19 = MemoEdit8.Text
                .SDIGITALRJ30_20 = CheckEdit8.Checked
                .SDIGITALRJ30_21 = CheckEdit7.Checked
                .SDIGITALRJ30_22 = CheckEdit6.Checked
                .SDIGITALRJ30_23 = CheckEdit5.Checked
                .SDIGITALRJ30_24 = TextEdit26.Text
                .SDIGITALRJ30_25 = TextEdit25.Text
                .SDIGITALRJ30_26 = TextEdit24.Text
                .SDIGITALRJ30_27 = TextEdit1.Text
                .SDIGITALRJ30_28 = TextEdit2.Text
                .SDIGITALRJ30_29 = TextEdit3.Text
                .SDIGITALRJ30_30 = TextEdit8.Text
                .SDIGITALRJ30_31 = TextEdit9.Text
                .SDIGITALRJ30_32 = TextEdit10.Text
                .SDIGITALRJ30_33 = TextEdit11.Text
                .SDIGITALRJ30_34 = TextEdit12.Text
                .SDIGITALRJ30_35 = TextEdit13.Text
                .SDIGITALRJ30_36 = TextEdit14.Text
                .SDIGITALRJ30_37 = TextEdit15.Text
                .SDIGITALRJ30_38 = TextEdit16.Text
                .SDIGITALRJ30_39 = TextEdit17.Text
                .SDIGITALRJ30_40 = TextEdit18.Text
                .SDIGITALRJ30_41 = TextEdit19.Text
                .SDIGITALRJ30_42 = MemoEdit1.Text
                .SDIGITALRJ30_43 = MemoEdit2.Text
                .SDIGITALRJ30_44 = MemoEdit3.Text
                .SDIGITALRJ30_45 = MemoEdit4.Text
                .SDIGITALRJ30_46 = MemoEdit5.Text
                .SDIGITALRJ30_47 = MemoEdit6.Text

                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                Try
                    .CETAK = oS_DIGITAL_RJ_30.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                'Try
                '    If sIsOtority = True Then
                '        .KDUSER = oS_DIGITAL_RJ_30.GetData(sNoId).KDUSER
                '        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_30.GetData(sNoId).KDUSER_SIGNATURE
                '    Else
                '        .KDUSER = sUserID
                '        .KDUSER_SIGNATURE = sUserSIGNATURE
                '    End If
                'Catch ex As Exception
                '    .KDUSER = sUserID
                '    .KDUSER_SIGNATURE = sUserSIGNATURE
                'End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""

                .RIWAYATMEDIPSIKIATRILAIN = txtRIWAYATMEDIPSIKIATRILAIN.Text
                .PEMERIKSAANSTATUSPSIKIATRI = txtPEMERIKSAANSTATUSPSIKIATRI.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_30.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_30.UpdateData(ds)
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
            'Case Keys.F5
            '    If btnReload.Enabled = True Then
            '        btnReload_Click()
            '    End If
            'Case Keys.F6
            '    If btnDiagnosa.Enabled = True Then
            '        btnDiagnosa_Click()
            '    End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        'Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        'Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(sNoId)
        'If dsMasterDiagnosa IsNot Nothing Then
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, sNoId)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'Else
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, sNoId)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'End If

        'If sCode = "Berhasil" Then
        '    Dim listDiagnosa As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail(sNoId)
        '        listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
        '    Next

        '    Dim listProsedur As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail_(sNoId)
        '        listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
        '    Next
        '    MemoEdit3.Text = String.Join(vbCrLf, listProsedur.ToArray)
        '    MemoEdit1.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        'End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIGITAL_RJ_30.GetDataByKunjungan(sNoId)

        'Dim listPenunjang As New List(Of String)
        'Dim listTindakanPengobatan As New List(Of String)

        'If dsKunjungan IsNot Nothing Then
        '    Dim oOrderTindakan As New Inventory.clsOrderTindakan
        '    Dim oOrderLab As New Inventory.clsOrderLab
        '    Dim oOrderRad As New Inventory.clsOrderRad
        '    Dim oKonsul As New Digital.clsKonsul
        '    Dim oKonsulJawab As New Digital.clsJawabKonsul

        '    Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                     Join y In oOrderTindakan.GetDataDetail()
        '                     On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
        '                     Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderLab.GetDataDetail()
        '                On x.KDORDERLAB Equals y.KDORDERLAB
        '                Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                Join y In oOrderRad.GetDataDetail()
        '                On x.KDORDERRAD Equals y.KDORDERRAD
        '                Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

        '    Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                   Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
        '                        Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

        '    Dim dsUnionPenunjang = dsLab.Union(dsRad)

        '    For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
        '        listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

        '    For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
        '        listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
        '    Next

        '    Dim oResep As New Inventory.clsOrderResep

        '    Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

        '    For Each xloop In dsResep
        '        For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
        '            listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
        '        Next
        '    Next

        '    MemoEdit2.Text = String.Join(", ", listPenunjang.ToArray)
        '    MemoEdit4.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        'End If
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
    Private Sub fn_DOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        TextEdit26.Text = "dalam batas normal"
        TextEdit25.Text = "dalam batas normal"
        TextEdit24.Text = "dalam batas normal"
        TextEdit2.Text = "dalam batas normal"
        TextEdit3.Text = "dalam batas normal"
        TextEdit1.Text = "dalam batas normal"
        TextEdit8.Text = "dalam batas normal"
        TextEdit9.Text = "dalam batas normal"
        TextEdit10.Text = "dalam batas normal"
        TextEdit11.Text = "dalam batas normal"
        TextEdit12.Text = "dalam batas normal"
        TextEdit13.Text = "dalam batas normal"
        TextEdit14.Text = "dalam batas normal"
        TextEdit15.Text = "dalam batas normal"
        TextEdit16.Text = "dalam batas normal"
        TextEdit17.Text = "dalam batas normal"
        TextEdit18.Text = "dalam batas normal"
        TextEdit19.Text = "dalam batas normal"
    End Sub

    Private Sub frmEMedrekRJ_30_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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