Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmKartuAnastesi_B
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_OK_KARTU_ANESTESI_B As New EMedrek.clsS_DIGITAL_OK_KARTU_ANESTESI_B
    Private sNoRekamMedis As String
    Private sNoId As String
    Private sNAMADOKTER As String
    Private sKODEDOKTER As String
    Private sPangkat As String
    Private sKDKUNJUNGAN As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId

        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
        Dim dsPendaftaran = oKunjungan.GetDatabyKodeKunjungan(KDKUNJUNGAN)

        If dsPendaftaran IsNot Nothing Then
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtNama.Text = dsPendaftaran.NAMAPASIEN
            txtUMUR.Text = dsPendaftaran.UMUR
            txtNOREG.Text = dsPendaftaran.KDKUNJUNGAN
            sNAMADOKTER = dsPendaftaran.KDDOCTOR_NAMA
            sKODEDOKTER = dsPendaftaran.KDDOCTOR
            sNoRekamMedis = dsPendaftaran.KDCUSTOMER
            sPangkat = dsPendaftaran.PANGKAT
            txtPANGKATNRP.Text = dsPendaftaran.PANGKAT
            txtKESATUAN.Text = dsPendaftaran.KESATUAN
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Kartu Anastesi B"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
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

        chkSEDASI.Properties.ReadOnly = Status
        chkRINGAN.Properties.ReadOnly = Status
        chkSEDANG.Properties.ReadOnly = Status
        chkDalam.Properties.ReadOnly = Status
        chkANESTESI.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        txtJAM2.Properties.ReadOnly = Status
        txtDiagnosis.Properties.ReadOnly = Status
        txtTindakan.Properties.ReadOnly = Status
        txtKamar.Properties.ReadOnly = Status
        txtBAGIAN.Properties.ReadOnly = Status
        txtOperator.Properties.ReadOnly = Status
        txtAsistenOperator.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        txtAsistenAnestesi.Properties.ReadOnly = Status
        txtKeadaanUmum.Properties.ReadOnly = Status
        txtKesadaran.Properties.ReadOnly = Status
        txtASA.Properties.ReadOnly = Status
        txtTekananDarah.Properties.ReadOnly = Status
        txtNADI.Properties.ReadOnly = Status
        txtRespirasi.Properties.ReadOnly = Status
        txtSuhu.Properties.ReadOnly = Status
        txtSaturasi.Properties.ReadOnly = Status
        txtBB.Properties.ReadOnly = Status
        txtTB.Properties.ReadOnly = Status
        txtGolonganDarah.Properties.ReadOnly = Status
        txtHB.Properties.ReadOnly = Status
        txtTrombosit.Properties.ReadOnly = Status
        txtPemeriksaanLain.Properties.ReadOnly = Status
        txtCairanObat_01.Properties.ReadOnly = Status
        txtCairanObat_02.Properties.ReadOnly = Status
        txtCairanObat_03.Properties.ReadOnly = Status
        txtCairanObat_04.Properties.ReadOnly = Status
        txtCairanObat_05.Properties.ReadOnly = Status
        txtCairanObat_06.Properties.ReadOnly = Status
        txtCairanObat_07.Properties.ReadOnly = Status
        txtCairanObat_08.Properties.ReadOnly = Status
        txtKomplikasi.Properties.ReadOnly = Status
        txtTindakan.Properties.ReadOnly = Status
        txtKeadaanUmum2.Properties.ReadOnly = Status
        txtKesadaran2.Properties.ReadOnly = Status
        txtKomplikasi2.Properties.ReadOnly = Status
        txtIntruksi.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        chkSEDASI.Checked = False
        chkRINGAN.Checked = False
        chkSEDANG.Checked = False
        chkDalam.Checked = False
        chkANESTESI.Checked = False
        deDATE.DateTime = Now
        txtJAM.Text = Now.ToString("HH:mm")
        txtJAM2.Text = Now.ToString("HH:mm")
        txtDiagnosis.ResetText()
        txtTindakan.ResetText()
        txtKamar.ResetText()
        txtBAGIAN.ResetText()
        txtOperator.ResetText()
        txtAsistenOperator.ResetText()
        grdDOCTOR.ResetText()
        txtAsistenAnestesi.ResetText()
        txtKeadaanUmum.ResetText()
        txtKesadaran.ResetText()
        txtASA.ResetText()
        txtTekananDarah.ResetText()
        txtNADI.ResetText()
        txtRespirasi.ResetText()
        txtSuhu.ResetText()
        txtSaturasi.ResetText()
        txtBB.ResetText()
        txtTB.ResetText()
        txtGolonganDarah.ResetText()
        txtHB.ResetText()
        txtTrombosit.ResetText()
        txtPemeriksaanLain.ResetText()
        txtCairanObat_01.ResetText()
        txtCairanObat_02.ResetText()
        txtCairanObat_03.ResetText()
        txtCairanObat_04.ResetText()
        txtCairanObat_05.ResetText()
        txtCairanObat_06.ResetText()
        txtCairanObat_07.ResetText()
        txtCairanObat_08.ResetText()
        txtKomplikasi.ResetText()
        txtTindakan.ResetText()
        txtKeadaanUmum2.ResetText()
        txtKesadaran2.ResetText()
        txtKomplikasi2.ResetText()
        txtIntruksi.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(sNoId)
            With ds
                chkSEDASI.Checked = .SEDASI
                chkRINGAN.Checked = .RINGAN
                chkSEDANG.Checked = .SEDANG
                chkDalam.Checked = .DALAM
                chkANESTESI.Checked = .ANESTESILOKAL
                deDATE.DateTime = .DATE
                txtJAM.Text = .JAM1
                txtJAM2.Text = .JAM2
                txtDiagnosis.Text = .DIAGNOSIS
                txtTindakan.Text = .TINDAKAN
                txtKamar.Text = .KAMAR
                txtBAGIAN.Text = .RUANGAN
                txtOperator.Text = .OPERATOR
                txtAsistenOperator.Text = .ASISTENOPERATOR
                grdDOCTOR.Text = .KDDOCTOR
                txtAsistenAnestesi.Text = .ASISTENANESTESI
                txtKeadaanUmum.Text = .KEADAANUMUM
                txtKesadaran.Text = .KESADARAN
                txtASA.Text = .ASA
                txtTekananDarah.Text = .TEKANANDARAH
                txtNADI.Text = .NADI
                txtRespirasi.Text = .RESPIRASI
                txtSuhu.Text = .SUHU
                txtSaturasi.Text = .SATURASI
                txtBB.Text = .BB
                txtTB.Text = .TB
                txtGolonganDarah.Text = .GOLDAR
                txtHB.Text = .HB
                txtTrombosit.Text = .TROMBOSIT
                txtPemeriksaanLain.Text = .PEMERIKSAANLAIN
                txtCairanObat_01.Text = .CAIRAN_OBAT1
                txtCairanObat_02.Text = .CAIRAN_OBAT2
                txtCairanObat_03.Text = .CAIRAN_OBAT3
                txtCairanObat_04.Text = .CAIRAN_OBAT4
                txtCairanObat_05.Text = .CAIRAN_OBAT5
                txtCairanObat_06.Text = .CAIRAN_OBAT6
                txtCairanObat_07.Text = .CAIRAN_OBAT7
                txtCairanObat_08.Text = .CAIRAN_OBAT8
                txtKomplikasi.Text = .KOMPLIKASI
                txtTindakan.Text = .TINDAKAN_KOMPLIKASI
                txtKeadaanUmum2.Text = .KEADAANUMUM_ST
                txtKesadaran2.Text = .KESADARAN_ST
                txtKomplikasi2.Text = .KOMPLIKASI_ST
                txtIntruksi.Text = .INTRUKSI_ST

                Try
                    Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(sNoId).img1

                    PictureBox1.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try

                Try
                    Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(sNoId).img2

                    PictureBox2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                    MsgBox("Load List Data Gambar tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End Try

            End With
        Catch oErr As Exception
            MsgBox("Load List Data Kartu Anastesi B: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Nomor Kunjungan", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter Anestesi", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetStructureHeader
            With ds
                .KODE = sNoId
                .KDPENDAFTARAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = sKDKUNJUNGAN
                .KDCUSTOMER = txtNOMORRM.Text
                .SEDASI = chkSEDASI.Checked
                .RINGAN = chkRINGAN.Checked
                .SEDANG = chkSEDANG.Checked
                .DALAM = chkDalam.Checked
                .ANESTESILOKAL = chkANESTESI.Checked
                .DATE = deDATE.DateTime
                .JAM1 = txtJAM.Text
                .JAM2 = txtJAM2.Text
                .DIAGNOSIS = txtDiagnosis.Text
                .TINDAKAN = txtTindakan.Text
                .KAMAR = txtKamar.Text
                .RUANGAN = txtBAGIAN.Text
                .OPERATOR = txtOperator.Text
                .ASISTENOPERATOR = txtAsistenOperator.Text
                .KDDOCTOR = grdDOCTOR.EditValue
                .ASISTENANESTESI = txtAsistenAnestesi.Text
                .KEADAANUMUM = txtKeadaanUmum.Text
                .KESADARAN = txtKesadaran.Text
                .ASA = txtASA.Text
                .TEKANANDARAH = txtTekananDarah.Text
                .NADI = txtNADI.Text
                .RESPIRASI = txtRespirasi.Text
                .SUHU = txtSuhu.Text
                .SATURASI = txtSaturasi.Text
                .BB = txtBB.Text
                .TB = txtTB.Text
                .GOLDAR = txtGolonganDarah.Text
                .HB = txtHB.Text
                .TROMBOSIT = txtTrombosit.Text
                .PEMERIKSAANLAIN = txtPemeriksaanLain.Text
                .CAIRAN_OBAT1 = txtCairanObat_01.Text
                .CAIRAN_OBAT2 = txtCairanObat_02.Text
                .CAIRAN_OBAT3 = txtCairanObat_03.Text
                .CAIRAN_OBAT4 = txtCairanObat_04.Text
                .CAIRAN_OBAT5 = txtCairanObat_05.Text
                .CAIRAN_OBAT6 = txtCairanObat_06.Text
                .CAIRAN_OBAT7 = txtCairanObat_07.Text
                .CAIRAN_OBAT8 = txtCairanObat_08.Text
                .KOMPLIKASI = txtKomplikasi.Text
                .TINDAKAN_KOMPLIKASI = txtTindakanKomplikasi.Text
                .KEADAANUMUM_ST = txtKeadaanUmum2.Text
                .KESADARAN_ST = txtKesadaran2.Text
                .KOMPLIKASI_ST = txtKomplikasi2.Text
                .INTRUKSI_ST = txtIntruksi.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .img1 = data
                Catch oErr As Exception
                    Try
                        .img1 = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).img1
                    Catch ex As Exception
                    End Try
                End Try

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat)
                    Dim data As Byte() = ms.GetBuffer()
                    .img2 = data
                Catch oErr As Exception
                    Try
                        .img2 = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).img2
                    Catch ex As Exception
                    End Try
                End Try

                Try
                    .CETAK = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_OK_KARTU_ANESTESI_B.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_OK_KARTU_ANESTESI_B.UpdateData(ds)
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
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_Doctor()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub

    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            PictureBox1.Image = CType(My.Resources.ResourceManager.GetObject("KartuAnastesiB_1_monitoring"), Image)
        ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Try
                ' ***** HEADER *****
                Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text)

                With ds
                    Try
                        Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).img1

                        PictureBox1.Image = ByteArrayToImage(img.ToArray())
                    Catch oErr As Exception
                        MsgBox("Load List Data Gambar Pria tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End With
            Catch oErr As Exception
                MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As MouseEventArgs) Handles PictureBox1.Click
        If cboPilih.SelectedText = Nothing Then
            MsgBox(Statement.ErrorStatement & vbCrLf & "Silahkan pilih keterangan simbol", MsgBoxStyle.Exclamation, Me.Text)
            cboPilih.Focus()
        Else
            Dim g As Graphics = Graphics.FromImage(PictureBox1.Image)
            Dim xpoint As Single = e.X - 8
            Dim ypoint As Single = e.Y - 8
            g.DrawString(cboPilih.Text.Substring(0, 1), New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
            g.Dispose()
            PictureBox1.Invalidate()
        End If
    End Sub

    Private Sub ResetGambarToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            PictureBox2.Image = CType(My.Resources.ResourceManager.GetObject("KartuAnastesiB_2_monitoring"), Image)
        ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            Try
                ' ***** HEADER *****
                Dim ds = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text)

                With ds
                    Try
                        Dim img = oS_DIGITAL_OK_KARTU_ANESTESI_B.GetData(txtNOREG.Text).img2

                        PictureBox2.Image = ByteArrayToImage(img.ToArray())
                    Catch oErr As Exception
                        MsgBox("Load List Data Gambar Pria tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End With
            Catch oErr As Exception
                MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation)
            End Try
        End If
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As MouseEventArgs) Handles PictureBox2.Click
        If cboPilih2.SelectedText = Nothing Then
            MsgBox(Statement.ErrorStatement & vbCrLf & "Silahkan pilih keterangan simbol", MsgBoxStyle.Exclamation, Me.Text)
            cboPilih2.Focus()
        Else
            Dim g As Graphics = Graphics.FromImage(PictureBox2.Image)
            Dim xpoint As Single = e.X - 8
            Dim ypoint As Single = e.Y - 8
            g.DrawString(cboPilih2.Text.Substring(0, 1), New Font("Arial", 14, FontStyle.Regular), Brushes.DarkBlue, New PointF(xpoint, ypoint))
            g.Dispose()
            PictureBox2.Invalidate()
        End If
    End Sub

    Private Sub frmKartuAnastesi_B_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
            myView.Y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y
        End If

        Me.Panel1.AutoScrollPosition = myView
    End Sub





#End Region
End Class