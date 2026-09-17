Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_35
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_35 As New EMedrek.clsDigital_RJ_35
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
        Me.Text = "ASESMEN AWAL MEDIS PASIEN GIGI"
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
        fn_NOIDUSER()
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
        TextEdit1.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
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
        TextEdit1.ResetText()
        TextEdit15.ResetText()
        TextEdit13.ResetText()
        MemoEdit7.ResetText()
        MemoEdit4.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()

        grdDOCTOR.Text = sKDDOCTOR

        deDATE.DateTime = Now

        fn_LoadAsessmenRawatJalan(sNoid)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_35.GetData(sNoid)

            With ds
                grdDOCTOR.Text = .DOKTER_KODE
                ' grdDEPARTMENT.EditValue = .KDDEPARTMENT
                CheckEdit74.Checked = .SDIGITALRJ35_1
                CheckEdit73.Checked = .SDIGITALRJ35_2
                txt1.Text = .SDIGITALRJ35_3
                txt1.Text = .SDIGITALRJ35_3
                txt2.Text = .SDIGITALRJ35_4
                txt3.Text = .SDIGITALRJ35_5
                txt4.Text = .SDIGITALRJ35_6
                txt5.Text = .SDIGITALRJ35_7
                txt6.Text = .SDIGITALRJ35_8
                txt7.Text = .SDIGITALRJ35_9
                txt8.Text = .SDIGITALRJ35_10
                txt9.Text = .SDIGITALRJ35_11
                TextEdit4.Text = .SDIGITALRJ35_12
                TextEdit5.Text = .SDIGITALRJ35_13
                TextEdit6.Text = .SDIGITALRJ35_14
                TextEdit7.Text = .SDIGITALRJ35_15
                CheckEdit1.Checked = .SDIGITALRJ35_16
                CheckEdit2.Checked = .SDIGITALRJ35_17
                CheckEdit3.Checked = .SDIGITALRJ35_18
                CheckEdit4.Checked = .SDIGITALRJ35_19
                TextEdit1.Text = .SDIGITALRJ35_20
                CheckEdit5.Checked = .SDIGITALRJ35_21
                CheckEdit6.Checked = .SDIGITALRJ35_22
                CheckEdit7.Checked = .SDIGITALRJ35_23
                CheckEdit8.Checked = .SDIGITALRJ35_24
                CheckEdit9.Checked = .SDIGITALRJ35_25
                CheckEdit11.Checked = .SDIGITALRJ35_26
                CheckEdit10.Checked = .SDIGITALRJ35_27
                CheckEdit13.Checked = .SDIGITALRJ35_28
                CheckEdit12.Checked = .SDIGITALRJ35_29
                CheckEdit15.Checked = .SDIGITALRJ35_30
                CheckEdit14.Checked = .SDIGITALRJ35_31
                CheckEdit17.Checked = .SDIGITALRJ35_32
                CheckEdit16.Checked = .SDIGITALRJ35_33
                TextEdit15.Text = .SDIGITALRJ35_34
                CheckEdit19.Checked = .SDIGITALRJ35_35
                CheckEdit18.Checked = .SDIGITALRJ35_36
                TextEdit13.Text = .SDIGITALRJ35_37
                'CheckEdit72.Checked = .SDIGITALRJ35_38
                'CheckEdit71.Checked = .SDIGITALRJ35_39
                'CheckEdit70.Checked = .SDIGITALRJ35_40
                'CheckEdit69.Checked = .SDIGITALRJ35_41
                'CheckEdit68.Checked = .SDIGITALRJ35_42
                'CheckEdit67.Checked = .SDIGITALRJ35_43
                'CheckEdit49.Checked = .SDIGITALRJ35_44
                'CheckEdit48.Checked = .SDIGITALRJ35_45
                'CheckEdit47.Checked = .SDIGITALRJ35_46
                'CheckEdit46.Checked = .SDIGITALRJ35_47
                'CheckEdit45.Checked = .SDIGITALRJ35_48
                'CheckEdit44.Checked = .SDIGITALRJ35_49
                'CheckEdit43.Checked = .SDIGITALRJ35_50
                'CheckEdit41.Checked = .SDIGITALRJ35_51
                'CheckEdit40.Checked = .SDIGITALRJ35_52
                'CheckEdit39.Checked = .SDIGITALRJ35_53
                'CheckEdit38.Checked = .SDIGITALRJ35_54
                'CheckEdit27.Checked = .SDIGITALRJ35_55
                'CheckEdit26.Checked = .SDIGITALRJ35_56
                'CheckEdit25.Checked = .SDIGITALRJ35_57
                'CheckEdit24.Checked = .SDIGITALRJ35_58
                'CheckEdit23.Checked = .SDIGITALRJ35_59
                'CheckEdit22.Checked = .SDIGITALRJ35_60
                'CheckEdit21.Checked = .SDIGITALRJ35_61
                'CheckEdit20.Checked = .SDIGITALRJ35_62
                'CheckEdit28.Checked = .SDIGITALRJ35_63
                'CheckEdit50.Checked = .SDIGITALRJ35_64
                'CheckEdit37.Checked = .SDIGITALRJ35_65
                'CheckEdit36.Checked = .SDIGITALRJ35_66
                'CheckEdit35.Checked = .SDIGITALRJ35_67
                'CheckEdit34.Checked = .SDIGITALRJ35_68
                'CheckEdit33.Checked = .SDIGITALRJ35_69
                'CheckEdit32.Checked = .SDIGITALRJ35_70
                'CheckEdit31.Checked = .SDIGITALRJ35_71
                'CheckEdit30.Checked = .SDIGITALRJ35_72
                'CheckEdit29.Checked = .SDIGITALRJ35_73
                'CheckEdit66.Checked = .SDIGITALRJ35_74
                'CheckEdit65.Checked = .SDIGITALRJ35_75
                'CheckEdit64.Checked = .SDIGITALRJ35_76
                'CheckEdit63.Checked = .SDIGITALRJ35_77
                'CheckEdit62.Checked = .SDIGITALRJ35_78
                'CheckEdit61.Checked = .SDIGITALRJ35_79
                'CheckEdit60.Checked = .SDIGITALRJ35_80
                'CheckEdit59.Checked = .SDIGITALRJ35_81
                'CheckEdit58.Checked = .SDIGITALRJ35_82
                'CheckEdit57.Checked = .SDIGITALRJ35_83
                'CheckEdit56.Checked = .SDIGITALRJ35_84
                'CheckEdit55.Checked = .SDIGITALRJ35_85
                'CheckEdit54.Checked = .SDIGITALRJ35_86
                'CheckEdit53.Checked = .SDIGITALRJ35_87
                'CheckEdit52.Checked = .SDIGITALRJ35_88
                'CheckEdit51.Checked = .SDIGITALRJ35_89
                MemoEdit7.Text = .SDIGITALRJ35_90
                MemoEdit4.Text = .SDIGITALRJ35_91
                MemoEdit2.Text = .SDIGITALRJ35_92
                MemoEdit3.Text = .SDIGITALRJ35_93
                MemoEdit5.Text = .SDIGITALRJ35_94
                MemoEdit6.Text = .SDIGITALRJ35_95
                Try
                    Dim img = oS_DIGITAL_RJ_35.GetData(sNoid).SDIGITALRJ35_96_TEXT

                    picGambar2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                txtSDIGITALRJ34_27_TEXT.Text = .SDIGITALRJ35_96
                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New EMedrek.clsDigital_RJ_22
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            'keluhan utama
            txt1.Text = dsAsessmenRawatJalan.SDIGITALRJ22_1
            'kesadaran 
            txt5.Text = dsAsessmenRawatJalan.SDIGITALRJ22_2
            'bb
            txt7.Text = dsAsessmenRawatJalan.SDIGITALRJ22_7
            'tb
            txt8.Text = dsAsessmenRawatJalan.SDIGITALRJ22_8
            'nadi
            TextEdit4.Text = dsAsessmenRawatJalan.SDIGITALRJ22_4
            'tensi
            TextEdit5.Text = dsAsessmenRawatJalan.SDIGITALRJ22_3
            'suhu
            TextEdit6.Text = dsAsessmenRawatJalan.SDIGITALRJ22_5
            'respirasi
            TextEdit7.Text = dsAsessmenRawatJalan.SDIGITALRJ22_6
        Else
            'MsgBox("Assemen Awal Keperawatan rawat jalan belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
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
            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
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
            Dim ds = oS_DIGITAL_RJ_35.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                .KDCUSTOMER = ""
                Try
                    .DATECREATED = oS_DIGITAL_RJ_35.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                .SDIGITALRJ35_1 = CheckEdit74.Checked
                .SDIGITALRJ35_2 = CheckEdit73.Checked
                .SDIGITALRJ35_3 = txt1.Text
                .SDIGITALRJ35_4 = txt2.Text
                .SDIGITALRJ35_5 = txt3.Text
                .SDIGITALRJ35_6 = txt4.Text
                .SDIGITALRJ35_7 = txt5.Text
                .SDIGITALRJ35_8 = txt6.Text
                .SDIGITALRJ35_9 = txt7.Text
                .SDIGITALRJ35_10 = txt8.Text
                .SDIGITALRJ35_11 = txt9.Text
                .SDIGITALRJ35_12 = TextEdit4.Text
                .SDIGITALRJ35_13 = TextEdit5.Text
                .SDIGITALRJ35_14 = TextEdit6.Text
                .SDIGITALRJ35_15 = TextEdit7.Text
                .SDIGITALRJ35_16 = CheckEdit1.Checked
                .SDIGITALRJ35_17 = CheckEdit2.Checked
                .SDIGITALRJ35_18 = CheckEdit3.Checked
                .SDIGITALRJ35_19 = CheckEdit4.Checked
                .SDIGITALRJ35_20 = TextEdit1.Text
                .SDIGITALRJ35_21 = CheckEdit5.Checked
                .SDIGITALRJ35_22 = CheckEdit6.Checked
                .SDIGITALRJ35_23 = CheckEdit7.Checked
                .SDIGITALRJ35_24 = CheckEdit8.Checked
                .SDIGITALRJ35_25 = CheckEdit9.Checked
                .SDIGITALRJ35_26 = CheckEdit11.Checked
                .SDIGITALRJ35_27 = CheckEdit10.Checked
                .SDIGITALRJ35_28 = CheckEdit13.Checked
                .SDIGITALRJ35_29 = CheckEdit12.Checked
                .SDIGITALRJ35_30 = CheckEdit15.Checked
                .SDIGITALRJ35_31 = CheckEdit14.Checked
                .SDIGITALRJ35_32 = CheckEdit17.Checked
                .SDIGITALRJ35_33 = CheckEdit16.Checked
                .SDIGITALRJ35_34 = TextEdit15.Text
                .SDIGITALRJ35_35 = CheckEdit19.Checked
                .SDIGITALRJ35_36 = CheckEdit18.Checked
                .SDIGITALRJ35_37 = TextEdit13.Text
                .SDIGITALRJ35_38 = False
                .SDIGITALRJ35_39 = False
                .SDIGITALRJ35_40 = False
                .SDIGITALRJ35_41 = False
                .SDIGITALRJ35_42 = False
                .SDIGITALRJ35_43 = False
                .SDIGITALRJ35_44 = False
                .SDIGITALRJ35_45 = False
                .SDIGITALRJ35_46 = False
                .SDIGITALRJ35_47 = False
                .SDIGITALRJ35_48 = False
                .SDIGITALRJ35_49 = False
                .SDIGITALRJ35_50 = False
                .SDIGITALRJ35_51 = False
                .SDIGITALRJ35_52 = False
                .SDIGITALRJ35_53 = False
                .SDIGITALRJ35_54 = False
                .SDIGITALRJ35_55 = False
                .SDIGITALRJ35_56 = False
                .SDIGITALRJ35_57 = False
                .SDIGITALRJ35_58 = False
                .SDIGITALRJ35_59 = False
                .SDIGITALRJ35_60 = False
                .SDIGITALRJ35_61 = False
                .SDIGITALRJ35_62 = False
                .SDIGITALRJ35_63 = False
                .SDIGITALRJ35_64 = False
                .SDIGITALRJ35_65 = False
                .SDIGITALRJ35_66 = False
                .SDIGITALRJ35_67 = False
                .SDIGITALRJ35_68 = False
                .SDIGITALRJ35_69 = False
                .SDIGITALRJ35_70 = False
                .SDIGITALRJ35_71 = False
                .SDIGITALRJ35_72 = False
                .SDIGITALRJ35_73 = False
                .SDIGITALRJ35_74 = False
                .SDIGITALRJ35_75 = False
                .SDIGITALRJ35_76 = False
                .SDIGITALRJ35_77 = False
                .SDIGITALRJ35_78 = False
                .SDIGITALRJ35_79 = False
                .SDIGITALRJ35_80 = False
                .SDIGITALRJ35_81 = False
                .SDIGITALRJ35_82 = False
                .SDIGITALRJ35_83 = False
                .SDIGITALRJ35_84 = False
                .SDIGITALRJ35_85 = False
                .SDIGITALRJ35_86 = False
                .SDIGITALRJ35_87 = False
                .SDIGITALRJ35_88 = False
                .SDIGITALRJ35_89 = False
                .SDIGITALRJ35_90 = MemoEdit7.Text
                .SDIGITALRJ35_91 = MemoEdit4.Text
                .SDIGITALRJ35_92 = MemoEdit2.Text
                .SDIGITALRJ35_93 = MemoEdit3.Text
                .SDIGITALRJ35_94 = MemoEdit5.Text
                .SDIGITALRJ35_95 = MemoEdit6.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGambar2.Image.Save(ms, picGambar2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .SDIGITALRJ35_96_TEXT = data
                Catch oErr As Exception
                    Try
                        .SDIGITALRJ35_96_TEXT = oS_DIGITAL_RJ_35.GetData(sNoid).SDIGITALRJ35_96_TEXT
                    Catch ex As Exception

                    End Try

                End Try
                .SDIGITALRJ35_96 = txtSDIGITALRJ34_27_TEXT.Text

                Try
                    .CETAK = oS_DIGITAL_RJ_35.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                'Try
                '    If sIsOtority = True Then
                '        .KDUSER = oS_DIGITAL_RJ_35.GetData(sNoid).KDUSER
                '        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_35.GetData(sNoid).KDUSER_SIGNATURE
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
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_35.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_35.UpdateData(ds)
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
        'Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(sNoid)
        'If dsMasterDiagnosa IsNot Nothing Then
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, sNoid)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'Else
        '    Dim frmMasterDiagnosa As New frmMasterDiagnosa
        '    frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, sNoid)
        '    frmMasterDiagnosa.ShowDialog(Me)
        'End If

        'If sCode = "Berhasil" Then
        '    Dim listDiagnosa As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail(sNoid)
        '        listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
        '    Next

        '    Dim listProsedur As New List(Of String)
        '    For Each xloop In oMasterDiagnosa.GetDataDetail_(sNoid)
        '        listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
        '    Next
        '    MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
        '    MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        'End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIGITAL_RJ_35.GetDataByKunjungan(sNoid)

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

        '    MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
        '    MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
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
    Private Sub fn_DOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
        'Try
        '    Dim oConn As New SqlConnection
        '    Dim oComm As New SqlCommand
        '    Dim da As SqlDataAdapter
        '    Dim ds As New DataSet
        '    Dim SQL As String
        '    Dim sConn As String = sKoneksi
        '    oConn = New SqlConnection(sConn)

        '    If oConn.State = ConnectionState.Closed Then
        '        oConn.Open()
        '    End If

        '    SQL = "SELECT "
        '    SQL &= "* "
        '    SQL &= "FROM "
        '    SQL &= "M_DOCTOR A "
        '    SQL &= "WHERE "
        '    SQL &= "A.ISACTIVE = 1 "

        '    oComm.Connection = oConn
        '    oComm.CommandText = SQL
        '    oComm.CommandTimeout = 120
        '    oComm.CommandType = CommandType.Text

        '    da = New SqlDataAdapter(oComm)
        '    da.Fill(ds, "DOKTER")

        '    grdDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
        '    grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '    grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        '    If oConn.State = ConnectionState.Open Then
        '        oConn.Close()
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_img10
        frmPopUp_Image.fn_LoadMe(picGambar2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGambar2.Image = sPicture
        End If

        picGambar2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub frmEMedrekRJ_35_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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