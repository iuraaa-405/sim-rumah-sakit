Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsesmenAwalMedisTHT
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASESMENAWALMEDISTHT As New EMedrek.clsDigital_RJ_ASMEDTHT
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoId = NoId
        sKDDOCTOR = KDDOCTOR
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS PASIEN THT"
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
        'btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status

        deDATE_AWAL.Properties.ReadOnly = Status
        deDATE_AWAL_TIME.Properties.ReadOnly = Status
        deDATE_AKHIR.Properties.ReadOnly = Status
        deDATE_AKHIR_TIME.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        TextEdit17.Properties.ReadOnly = Status
        TextEdit18.Properties.ReadOnly = Status
        TextEdit19.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
        TextEdit21.Properties.ReadOnly = Status
        TextEdit22.Properties.ReadOnly = Status
        TextEdit23.Properties.ReadOnly = Status
        TextEdit24.Properties.ReadOnly = Status
        TextEdit25.Properties.ReadOnly = Status
        TextEdit26.Properties.ReadOnly = Status
        TextEdit27.Properties.ReadOnly = Status
        TextEdit28.Properties.ReadOnly = Status
        TextEdit29.Properties.ReadOnly = Status
        TextEdit30.Properties.ReadOnly = Status
        TextEdit31.Properties.ReadOnly = Status
        TextEdit32.Properties.ReadOnly = Status
        TextEdit33.Properties.ReadOnly = Status
        TextEdit34.Properties.ReadOnly = Status
        TextEdit35.Properties.ReadOnly = Status
        TextEdit36.Properties.ReadOnly = Status
        TextEdit37.Properties.ReadOnly = Status
        TextEdit38.Properties.ReadOnly = Status
        TextEdit39.Properties.ReadOnly = Status
        TextEdit40.Properties.ReadOnly = Status
        TextEdit41.Properties.ReadOnly = Status
        TextEdit42.Properties.ReadOnly = Status
        TextEdit43.Properties.ReadOnly = Status
        TextEdit44.Properties.ReadOnly = Status
        TextEdit45.Properties.ReadOnly = Status
        TextEdit46.Properties.ReadOnly = Status
        TextEdit47.Properties.ReadOnly = Status
        TextEdit48.Properties.ReadOnly = Status
        TextEdit49.Properties.ReadOnly = Status
        TextEdit50.Properties.ReadOnly = Status
        TextEdit51.Properties.ReadOnly = Status
        TextEdit52.Properties.ReadOnly = Status
        TextEdit53.Properties.ReadOnly = Status
        TextEdit54.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        chkTAMBAH1.Properties.ReadOnly = Status
        chkTAMBAH2.Properties.ReadOnly = Status
        chkTAMBAH3.Properties.ReadOnly = Status
        chkTAMBAH4.Properties.ReadOnly = Status
        chkTAMBAH5.Properties.ReadOnly = Status
        chkTAMBAH6.Properties.ReadOnly = Status
        chkTAMBAH7.Properties.ReadOnly = Status
        chkTAMBAH8.Properties.ReadOnly = Status
        chkTAMBAH9.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE_AWAL.DateTime = Now
        deDATE_AWAL_TIME.DateTime = Now
        deDATE_AKHIR.DateTime = Now
        deDATE_AKHIR_TIME.DateTime = Now
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
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
        TextEdit20.ResetText()
        TextEdit21.ResetText()
        TextEdit22.ResetText()
        TextEdit23.ResetText()
        TextEdit24.ResetText()
        TextEdit25.ResetText()
        TextEdit26.ResetText()
        TextEdit27.ResetText()
        TextEdit28.ResetText()
        TextEdit29.ResetText()
        TextEdit30.ResetText()
        TextEdit31.ResetText()
        TextEdit32.ResetText()
        TextEdit33.ResetText()
        TextEdit34.ResetText()
        TextEdit35.ResetText()
        TextEdit36.ResetText()
        TextEdit37.ResetText()
        TextEdit38.ResetText()
        TextEdit39.ResetText()
        TextEdit40.ResetText()
        TextEdit41.ResetText()
        TextEdit42.ResetText()
        TextEdit43.ResetText()
        TextEdit44.ResetText()
        TextEdit45.ResetText()
        TextEdit46.ResetText()
        TextEdit47.ResetText()
        TextEdit48.ResetText()
        TextEdit49.ResetText()
        TextEdit50.ResetText()
        TextEdit51.ResetText()
        TextEdit52.ResetText()
        TextEdit53.ResetText()
        TextEdit54.ResetText()
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        MemoEdit1.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit4.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()

        fn_LoadAsessmenRawatJalan(sNoid)

        grdKDDOCTOR.Text =sKDDOCTOR 
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****

            Dim ds = oS_DIGITAL_ASESMENAWALMEDISTHT.GetData(sNoid)

            With ds
                deDATE_AWAL.DateTime = .DATE_AWAL
                deDATE_AWAL_TIME.DateTime = .DATE_AWAL_TIME
                deDATE_AKHIR.DateTime = .DATE_AKHIR
                deDATE_AKHIR_TIME.DateTime = .DATE_AKHIR_TIME
                grdKDDOCTOR.EditValue = .DOKTER_KODE
                TextEdit1.Text = .TETXTEDIT1
                TextEdit2.Text = .TETXTEDIT2
                TextEdit3.Text = .TETXTEDIT3
                TextEdit4.Text = .TETXTEDIT4
                TextEdit5.Text = .TETXTEDIT5
                TextEdit6.Text = .TETXTEDIT6
                TextEdit7.Text = .TETXTEDIT7
                TextEdit8.Text = .TETXTEDIT8
                TextEdit9.Text = .TETXTEDIT9
                TextEdit10.Text = .TETXTEDIT10
                TextEdit11.Text = .TETXTEDIT11
                TextEdit12.Text = .TETXTEDIT12
                TextEdit13.Text = .TETXTEDIT13
                TextEdit14.Text = .TETXTEDIT14
                TextEdit15.Text = .TETXTEDIT15
                TextEdit16.Text = .TETXTEDIT16
                TextEdit17.Text = .TETXTEDIT17
                TextEdit18.Text = .TETXTEDIT18
                TextEdit19.Text = .TETXTEDIT19
                TextEdit20.Text = .TETXTEDIT20
                TextEdit21.Text = .TETXTEDIT21
                TextEdit22.Text = .TETXTEDIT22
                TextEdit23.Text = .TETXTEDIT23
                TextEdit24.Text = .TETXTEDIT24
                TextEdit25.Text = .TETXTEDIT25
                TextEdit26.Text = .TETXTEDIT26
                TextEdit27.Text = .TETXTEDIT27
                TextEdit28.Text = .TETXTEDIT28
                TextEdit29.Text = .TETXTEDIT29
                TextEdit30.Text = .TETXTEDIT30
                TextEdit31.Text = .TETXTEDIT31
                TextEdit32.Text = .TETXTEDIT32
                TextEdit33.Text = .TETXTEDIT33
                TextEdit34.Text = .TETXTEDIT34
                TextEdit35.Text = .TETXTEDIT35
                TextEdit36.Text = .TETXTEDIT36
                TextEdit37.Text = .TETXTEDIT37
                TextEdit38.Text = .TETXTEDIT38
                TextEdit39.Text = .TETXTEDIT39
                TextEdit40.Text = .TETXTEDIT40
                TextEdit41.Text = .TETXTEDIT41
                TextEdit42.Text = .TETXTEDIT42
                TextEdit43.Text = .TETXTEDIT43
                TextEdit44.Text = .TETXTEDIT44
                TextEdit45.Text = .TETXTEDIT45
                TextEdit46.Text = .TETXTEDIT46
                TextEdit47.Text = .TETXTEDIT47
                TextEdit48.Text = .TETXTEDIT48
                TextEdit49.Text = .TETXTEDIT49
                TextEdit50.Text = .TETXTEDIT50
                TextEdit51.Text = .TETXTEDIT51
                TextEdit52.Text = .TETXTEDIT52
                TextEdit53.Text = .TETXTEDIT53
                TextEdit54.Text = .TETXTEDIT54
                CheckEdit1.Checked = .CHECKEDIT1
                CheckEdit2.Checked = .CHECKEDIT2
                CheckEdit3.Checked = .CHECKEDIT3
                CheckEdit4.Checked = .CHECKEDIT4
                CheckEdit5.Checked = .CHECKEDIT5
                CheckEdit6.Checked = .CHECKEDIT6
                MemoEdit1.Text = .MEMOEDIT1
                MemoEdit2.Text = .MEMOEDIT2
                MemoEdit3.Text = .MEMOEDIT3
                MemoEdit4.Text = .MEMOEDIT4
                MemoEdit5.Text = .MEMOEDIT5
                MemoEdit6.Text = .MEMOEDIT6
                MemoEdit7.Text = .MEMOEDIT7
                MemoEdit8.Text = .MEMOEDIT8
                chkTAMBAH1.Checked = .TAMBAH_1
                chkTAMBAH2.Checked = .TAMBAH_2
                chkTAMBAH3.Checked = .TAMBAH_3
                chkTAMBAH4.Checked = .TAMBAH_4
                chkTAMBAH5.Checked = .TAMBAH_5
                chkTAMBAH6.Checked = .TAMBAH_6
                chkTAMBAH7.Checked = .TAMBAH_7
                chkTAMBAH8.Checked = .TAMBAH_8
                chkTAMBAH9.Checked = .TAMBAH_9
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New EMedrek.clsDigital_RJ_08
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        Dim oAsessmenRawatJalan2 As New EMedrek.clsS_DIGITAL_ASKEP_RAWATJALAN
        Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetData(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            MemoEdit1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
            TextEdit4.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            TextEdit5.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
            TextEdit7.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
            TextEdit8.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
            TextEdit9.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
            TextEdit10.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
            TextEdit2.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
            TextEdit1.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
            TextEdit26.Text = "Tidak Ada"
            TextEdit25.Text = "Tidak Ada"
            TextEdit24.Text = "Tidak Ada"
            TextEdit23.Text = "Tidak Ada"
            TextEdit27.Text = "Tidak Ada"

        ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
            MemoEdit1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
            TextEdit4.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
            TextEdit5.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
            TextEdit7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
            TextEdit8.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
            TextEdit9.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
            TextEdit10.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
            TextEdit2.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
            TextEdit1.Text = dsAsessmenRawatJalan2.KEADAAN
            TextEdit26.Text = "Tidak Ada"
            TextEdit25.Text = "Tidak Ada"
            TextEdit24.Text = "Tidak Ada"
            TextEdit23.Text = "Tidak Ada"
            TextEdit27.Text = "Tidak Ada"
        Else
            Exit Sub
        End If
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Keterangan", MsgBoxStyle.Exclamation, Me.Text)
                deDATE_AWAL.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter DPJP", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
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

            Dim ds = oS_DIGITAL_ASESMENAWALMEDISTHT.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oS_DIGITAL_ASESMENAWALMEDISTHT.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = deDATE_AWAL.DateTime
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                .DATE_AWAL = deDATE_AWAL.DateTime
                .DATE_AWAL_TIME = deDATE_AWAL.DateTime
                .DATE_AKHIR = deDATE_AKHIR.DateTime
                .DATE_AKHIR_TIME = deDATE_AKHIR.DateTime
                .KDDEPARTMENT = 0
                .TETXTEDIT1 = TextEdit1.Text
                .TETXTEDIT2 = TextEdit2.Text
                .TETXTEDIT3 = TextEdit3.Text
                .TETXTEDIT4 = TextEdit4.Text
                .TETXTEDIT5 = TextEdit5.Text
                .TETXTEDIT6 = TextEdit6.Text
                .TETXTEDIT7 = TextEdit7.Text
                .TETXTEDIT8 = TextEdit8.Text
                .TETXTEDIT9 = TextEdit9.Text
                .TETXTEDIT10 = TextEdit10.Text
                .TETXTEDIT11 = TextEdit11.Text
                .TETXTEDIT12 = TextEdit12.Text
                .TETXTEDIT13 = TextEdit13.Text
                .TETXTEDIT14 = TextEdit14.Text
                .TETXTEDIT15 = TextEdit15.Text
                .TETXTEDIT16 = TextEdit16.Text
                .TETXTEDIT17 = TextEdit17.Text
                .TETXTEDIT18 = TextEdit18.Text
                .TETXTEDIT19 = TextEdit19.Text
                .TETXTEDIT20 = TextEdit20.Text
                .TETXTEDIT21 = TextEdit21.Text
                .TETXTEDIT22 = TextEdit22.Text
                .TETXTEDIT23 = TextEdit23.Text
                .TETXTEDIT24 = TextEdit24.Text
                .TETXTEDIT25 = TextEdit25.Text
                .TETXTEDIT26 = TextEdit26.Text
                .TETXTEDIT27 = TextEdit27.Text
                .TETXTEDIT28 = TextEdit28.Text
                .TETXTEDIT29 = TextEdit29.Text
                .TETXTEDIT30 = TextEdit30.Text
                .TETXTEDIT31 = TextEdit31.Text
                .TETXTEDIT32 = TextEdit32.Text
                .TETXTEDIT33 = TextEdit33.Text
                .TETXTEDIT34 = TextEdit34.Text
                .TETXTEDIT35 = TextEdit35.Text
                .TETXTEDIT36 = TextEdit36.Text
                .TETXTEDIT37 = TextEdit37.Text
                .TETXTEDIT38 = TextEdit38.Text
                .TETXTEDIT39 = TextEdit39.Text
                .TETXTEDIT40 = TextEdit40.Text
                .TETXTEDIT41 = TextEdit41.Text
                .TETXTEDIT42 = TextEdit42.Text
                .TETXTEDIT43 = TextEdit43.Text
                .TETXTEDIT44 = TextEdit44.Text
                .TETXTEDIT45 = TextEdit45.Text
                .TETXTEDIT46 = TextEdit46.Text
                .TETXTEDIT47 = TextEdit47.Text
                .TETXTEDIT48 = TextEdit48.Text
                .TETXTEDIT49 = TextEdit49.Text
                .TETXTEDIT50 = TextEdit50.Text
                .TETXTEDIT51 = TextEdit51.Text
                .TETXTEDIT52 = TextEdit52.Text
                .TETXTEDIT53 = TextEdit53.Text
                .TETXTEDIT54 = TextEdit54.Text
                .CHECKEDIT1 = CheckEdit1.Checked
                .CHECKEDIT2 = CheckEdit2.Checked
                .CHECKEDIT3 = CheckEdit3.Checked
                .CHECKEDIT4 = CheckEdit4.Checked
                .CHECKEDIT5 = CheckEdit5.Checked
                .CHECKEDIT6 = CheckEdit6.Checked
                .MEMOEDIT1 = MemoEdit1.Text
                .MEMOEDIT2 = MemoEdit2.Text
                .MEMOEDIT3 = MemoEdit3.Text
                .MEMOEDIT4 = MemoEdit4.Text
                .MEMOEDIT5 = MemoEdit5.Text
                .MEMOEDIT6 = MemoEdit6.Text
                .MEMOEDIT7 = MemoEdit7.Text
                .MEMOEDIT8 = MemoEdit8.Text

                .DOKTER_KODE = grdKDDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdKDDOCTOR.Text
                Try
                    .CETAK = oS_DIGITAL_ASESMENAWALMEDISTHT.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                'Try
                '    Dim oSetUser As New Setting.clsUser
                '    Dim dsUser = oSetUser.GetData(sUserID)
                '    If dsUser.ISOTORTY = True Then
                '        .KDUSER = oS_DIGITAL_ASESMENAWALMEDISTHT.GetData(sNoid).KDUSER
                '        .KDUSER_SIGNATURE = oS_DIGITAL_ASESMENAWALMEDISTHT.GetData(sNoid).KDUSER_SIGNATURE
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
                .TAMBAH_1 = chkTAMBAH1.Checked
                .TAMBAH_2 = chkTAMBAH2.Checked
                .TAMBAH_3 = chkTAMBAH3.Checked
                .TAMBAH_4 = chkTAMBAH4.Checked
                .TAMBAH_5 = chkTAMBAH5.Checked
                .TAMBAH_6 = chkTAMBAH6.Checked
                .TAMBAH_7 = chkTAMBAH7.Checked
                .TAMBAH_8 = chkTAMBAH8.Checked
                .TAMBAH_9 = chkTAMBAH9.Checked
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASESMENAWALMEDISTHT.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASESMENAWALMEDISTHT.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
            Case Keys.F6
                If btnDiagnosa.Enabled = True Then
                    btnDiagnosa_Click()
                End If
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
        '    MemoEdit5.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        'End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        'Dim dsKunjungan = oS_DIGITAL_ASESMENAWALMEDISTHT.GetDataByKunjungan(sNoid)

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

        '    'MemoEdit1.Text = String.Join(", ", listPenunjang.ToArray)
        '    MemoEdit6.Text = String.Join(", ", listTindakanPengobatan.ToArray)
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
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox("Load Dokter DPJP Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class