Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekAWAL_MEDIS_NEONATUS_RJ
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEP_PEDIATRIK_03 As New EMedrek.clsDigital_RJ_Neonatus
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
        Me.Text = "ASESMEN AWAL MEDIS NEONATUS"
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

        MemoEdit2.Properties.ReadOnly = False
        MemoEdit3.Properties.ReadOnly = False
        MemoEdit5.Properties.ReadOnly = False
        DateEdit1.Properties.ReadOnly = False
        DateEdit2.Properties.ReadOnly = False
        txtPENILAIAN_20.Properties.ReadOnly = False
        txtPENILAIAN_20.Properties.ReadOnly = False
        TextEdit2.Properties.ReadOnly = False
        TextEdit3.Properties.ReadOnly = False
        TextEdit4.Properties.ReadOnly = False
        TextEdit5.Properties.ReadOnly = False
        TextEdit10.Properties.ReadOnly = False
        TextEdit9.Properties.ReadOnly = False
        TextEdit6.Properties.ReadOnly = False
        TextEdit8.Properties.ReadOnly = False
        TextEdit7.Properties.ReadOnly = False
        TextEdit10.Properties.ReadOnly = False
        TextEdit22.Properties.ReadOnly = False
        TextEdit23.Properties.ReadOnly = False
        TextEdit24.Properties.ReadOnly = False
        TextEdit27.Properties.ReadOnly = False
        TextEdit26.Properties.ReadOnly = False
        TextEdit25.Properties.ReadOnly = False
        MemoEdit4.Properties.ReadOnly = False
        TextEdit13.Properties.ReadOnly = False
        TextEdit12.Properties.ReadOnly = False
        TextEdit14.Properties.ReadOnly = False
        TextEdit15.Properties.ReadOnly = False
        TextEdit16.Properties.ReadOnly = False
        TextEdit17.Properties.ReadOnly = False
        TextEdit18.Properties.ReadOnly = False
        TextEdit19.Properties.ReadOnly = False
        TextEdit20.Properties.ReadOnly = False
        TextEdit21.Properties.ReadOnly = False
        MemoEdit6.Properties.ReadOnly = False
        TextEdit28.Properties.ReadOnly = False
        MemoEdit7.Properties.ReadOnly = False
        MemoEdit8.Properties.ReadOnly = False

    End Sub
    Private Sub fn_EmptyMe()

        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit5.ResetText()
        DateEdit1.DateTime = Now
        DateEdit2.DateTime = Now
        CheckBox9.Checked = False
        CheckBox10.Checked = False
        CheckBox12.Checked = False
        CheckBox11.Checked = False
        CheckBox13.Checked = False
        CheckBox15.Checked = False
        CheckBox14.Checked = False
        CheckBox18.Checked = False
        CheckBox16.Checked = False
        CheckBox17.Checked = False
        CheckBox19.Checked = False
        CheckBox20.Checked = False
        CheckBox22.Checked = False
        CheckBox21.Checked = False
        txtPENILAIAN_20.ResetText()
        txtPENILAIAN_20.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit10.ResetText()
        TextEdit9.ResetText()
        TextEdit6.ResetText()
        TextEdit8.ResetText()
        TextEdit7.ResetText()
        TextEdit10.ResetText()
        CheckBox23.Checked = False
        CheckBox24.Checked = False
        TextEdit22.ResetText()
        TextEdit23.ResetText()
        TextEdit24.ResetText()
        TextEdit27.ResetText()
        TextEdit26.ResetText()
        TextEdit25.ResetText()
        CheckBox1.Checked = False
        CheckBox2.Checked = False
        CheckBox3.Checked = False
        CheckBox6.Checked = False
        CheckBox5.Checked = False
        CheckBox4.Checked = False
        CheckBox7.Checked = False
        CheckBox8.Checked = False
        MemoEdit4.ResetText()
        TextEdit13.ResetText()
        TextEdit12.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        TextEdit17.ResetText()
        TextEdit18.ResetText()
        TextEdit19.ResetText()
        TextEdit20.ResetText()
        TextEdit21.ResetText()
        MemoEdit6.ResetText()
        CheckBox25.Checked = False
        CheckBox26.Checked = False
        CheckBox28.Checked = False
        CheckBox27.Checked = False
        CheckBox29.Checked = False
        CheckBox30.Checked = False
        CheckBox32.Checked = False
        CheckBox31.Checked = False
        CheckBox34.Checked = False
        CheckBox33.Checked = False
        CheckBox36.Checked = False
        CheckBox35.Checked = False
        TextEdit28.ResetText()
        MemoEdit7.ResetText()
        MemoEdit8.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(sNoid)

            With ds
                MemoEdit2.Text = .KELUHANUTAMA
                MemoEdit3.Text = .RIWAYATKEHAMILAN
                MemoEdit5.Text = .RIWAYATPERSALINAN
                DateEdit1.EditValue = .PEMERIKSAAANFISIK
                DateEdit2.EditValue = .WAKTUPEMERIKSAAANFISIK
                CheckBox9.Checked = .WARNABIASA
                CheckBox10.Checked = .WARNASIANOSIS
                CheckBox12.Checked = .WARNAPUCAT
                CheckBox11.Checked = .WARNAIKTERUS
                CheckBox13.Checked = .WARNAPLETORA
                CheckBox15.Checked = .PERNAPASAN_FREKUENSI
                CheckBox14.Checked = .PERNAPASAN_TIPE
                CheckBox18.Checked = .KEADAANUMUM_ALERT
                CheckBox16.Checked = .KEADAANUMUM_SEDASI
                CheckBox17.Checked = .KEADAANUMUM_LETARGIS
                CheckBox19.Checked = .KEPALA_SEMETRISASIMETRIS
                CheckBox20.Checked = .KEPALA_HEMATOMASEFAL
                CheckBox22.Checked = .KEPALA_KAPUTSUKSEDANEUM
                CheckBox21.Checked = .KEPALA_LAINLAIN
                txtPENILAIAN_20.Text = .FONTANEL_1
                txtPENILAIAN_20.Text = .FONTANEL_2
                TextEdit2.Text = .KELAINAN
                TextEdit3.Text = .SUTURA
                TextEdit4.Text = .RAMBUT
                TextEdit5.Text = .MATA
                TextEdit10.Text = .TELINGA
                TextEdit9.Text = .HIDUNG
                TextEdit6.Text = .MULUT
                TextEdit8.Text = .LIDAH
                TextEdit7.Text = .LEHER
                TextEdit10.Text = .KULIT
                CheckBox23.Checked = .GENITALIA_L
                CheckBox24.Checked = .GENITALIA_P
                TextEdit22.Text = .GENITALIA_TESTIS
                TextEdit23.Text = .GENITALIA_DENSENSUS
                TextEdit24.Text = .GENITALIA_TESTISKULOUM
                TextEdit27.Text = .GENITALIA_LABIAMAYOR
                TextEdit26.Text = .GENITALIA_LABIAMINOR
                TextEdit25.Text = .GENITALIA_KELAINAN
                CheckBox1.Checked = .NEOROLOGI_REFREXMORO
                CheckBox2.Checked = .NEOROLOGI_REFREXHISAP
                CheckBox3.Checked = .NEOROLOGI_REFREXPEGANG
                CheckBox6.Checked = .NEOROLOGI_REFREXRODING
                CheckBox5.Checked = .NEOROLOGI_REFREXBABINSKY
                CheckBox4.Checked = .NEOROLOGI_REFREXLAINLAIN
                CheckBox7.Checked = .INSPEKSI_BATUK
                CheckBox8.Checked = .INSPEKSI_PERGERAKAN
                MemoEdit4.Text = .PERKUSI
                TextEdit13.Text = .AUSKULTASI_SUARAPERNAPASAN
                TextEdit12.Text = .AUSKULTASI_SUARATAMBAHAN
                TextEdit14.Text = .ABDOMEN_HATI
                TextEdit15.Text = .ABDOMEN_LIMPA
                TextEdit16.Text = .ABDOMEN_UMBILIKUS
                TextEdit17.Text = .PEMBESARANKELENJAR
                TextEdit18.Text = .ANUS
                TextEdit19.Text = .EKSTREMITASATAS
                TextEdit20.Text = .EKSTREMITASBAWAH
                TextEdit21.Text = .TULANGTULANG
                MemoEdit6.Text = .DIAGNOSAIS
                CheckBox25.Checked = .EKSPRESIWAJAH_1
                CheckBox26.Checked = .EKSPRESIWAJAH_2
                CheckBox28.Checked = .TANGISAN_1
                CheckBox27.Checked = .TANGISAN_2
                CheckBox29.Checked = .POLANAFAS_1
                CheckBox30.Checked = .POLANAFAS_2
                CheckBox32.Checked = .TANGAN_1
                CheckBox31.Checked = .TANGAN_2
                CheckBox34.Checked = .KAKI_1
                CheckBox33.Checked = .KAKI_2
                CheckBox36.Checked = .KESADARAN_1
                CheckBox35.Checked = .KESADARAN_2
                TextEdit28.Text = .NILAISKOR
                MemoEdit7.Text = .TATALAKSANA
                MemoEdit8.Text = .DISCHARFEPLANNING

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
                DateEdit1.Focus()
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

    Private Function cekVal(ByVal val As String) As Integer
        If (val = "") Then
            Return 0
        Else
            Return CInt(val)
        End If
    End Function

    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                Try
                    .DATECREATED = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(sNoid).DATECREATED
                    .DATE = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(sNoid).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    .DATE = DateEdit1.EditValue
                End Try
                .DATEUPDATED = Now
                .KELUHANUTAMA = MemoEdit2.Text
                .RIWAYATKEHAMILAN = MemoEdit3.Text
                .RIWAYATPERSALINAN = MemoEdit5.Text
                .PEMERIKSAAANFISIK = DateEdit1.EditValue
                .WAKTUPEMERIKSAAANFISIK = DateEdit2.EditValue
                .WARNABIASA = CheckBox9.Checked
                .WARNASIANOSIS = CheckBox10.Checked
                .WARNAPUCAT = CheckBox12.Checked
                .WARNAIKTERUS = CheckBox11.Checked
                .WARNAPLETORA = CheckBox13.Checked
                .PERNAPASAN_FREKUENSI = CheckBox15.Checked
                .PERNAPASAN_TIPE = CheckBox14.Checked
                .KEADAANUMUM_ALERT = CheckBox18.Checked
                .KEADAANUMUM_SEDASI = CheckBox16.Checked
                .KEADAANUMUM_LETARGIS = CheckBox17.Checked
                .KEPALA_SEMETRISASIMETRIS = CheckBox19.Checked
                .KEPALA_HEMATOMASEFAL = CheckBox20.Checked
                .KEPALA_KAPUTSUKSEDANEUM = CheckBox22.Checked
                .KEPALA_LAINLAIN = CheckBox21.Checked
                .FONTANEL_1 = txtPENILAIAN_20.Text
                .FONTANEL_2 = txtPENILAIAN_20.Text
                .KELAINAN = TextEdit2.Text
                .SUTURA = TextEdit3.Text
                .RAMBUT = TextEdit4.Text
                .MATA = TextEdit5.Text
                .TELINGA = TextEdit10.Text
                .HIDUNG = TextEdit9.Text
                .MULUT = TextEdit6.Text
                .LIDAH = TextEdit8.Text
                .LEHER = TextEdit7.Text
                .KULIT = TextEdit10.Text
                .GENITALIA_L = CheckBox23.Checked
                .GENITALIA_P = CheckBox24.Checked
                .GENITALIA_TESTIS = TextEdit22.Text
                .GENITALIA_DENSENSUS = TextEdit23.Text
                .GENITALIA_TESTISKULOUM = TextEdit24.Text
                .GENITALIA_LABIAMAYOR = TextEdit27.Text
                .GENITALIA_LABIAMINOR = TextEdit26.Text
                .GENITALIA_KELAINAN = TextEdit25.Text
                .NEOROLOGI_REFREXMORO = CheckBox1.Checked
                .NEOROLOGI_REFREXHISAP = CheckBox2.Checked
                .NEOROLOGI_REFREXPEGANG = CheckBox3.Checked
                .NEOROLOGI_REFREXRODING = CheckBox6.Checked
                .NEOROLOGI_REFREXBABINSKY = CheckBox5.Checked
                .NEOROLOGI_REFREXLAINLAIN = CheckBox4.Checked
                .INSPEKSI_BATUK = CheckBox7.Checked
                .INSPEKSI_PERGERAKAN = CheckBox8.Checked
                .PERKUSI = MemoEdit4.Text
                .AUSKULTASI_SUARAPERNAPASAN = TextEdit13.Text
                .AUSKULTASI_SUARATAMBAHAN = TextEdit12.Text
                .ABDOMEN_HATI = TextEdit14.Text
                .ABDOMEN_LIMPA = TextEdit15.Text
                .ABDOMEN_UMBILIKUS = TextEdit16.Text
                .PEMBESARANKELENJAR = TextEdit17.Text
                .ANUS = TextEdit18.Text
                .EKSTREMITASATAS = TextEdit19.Text
                .EKSTREMITASBAWAH = TextEdit20.Text
                .TULANGTULANG = TextEdit21.Text
                .DIAGNOSAIS = MemoEdit6.Text
                .EKSPRESIWAJAH_1 = CheckBox25.Checked
                .EKSPRESIWAJAH_2 = CheckBox26.Checked
                .TANGISAN_1 = CheckBox28.Checked
                .TANGISAN_2 = CheckBox27.Checked
                .POLANAFAS_1 = CheckBox29.Checked
                .POLANAFAS_2 = CheckBox30.Checked
                .TANGAN_1 = CheckBox32.Checked
                .TANGAN_2 = CheckBox31.Checked
                .KAKI_1 = CheckBox34.Checked
                .KAKI_2 = CheckBox33.Checked
                .KESADARAN_1 = CheckBox36.Checked
                .KESADARAN_2 = CheckBox35.Checked
                .NILAISKOR = TextEdit28.Text
                .TATALAKSANA = MemoEdit7.Text
                .DISCHARFEPLANNING = MemoEdit8.Text
                Try
                    .CETAK = oS_DIGITAL_ASKEP_PEDIATRIK_03.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEP_PEDIATRIK_03.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEP_PEDIATRIK_03.UpdateData(ds)
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
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
        End Select
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
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

    Private Sub CheckBox12_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox12.CheckedChanged

    End Sub

    Private Sub frmEMedrekAWAL_MEDIS_NEONATUS_RJ_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel1.AutoScrollPosition
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

	    Me.Panel1.AutoScrollPosition = myView
    End Sub

#End Region
End Class