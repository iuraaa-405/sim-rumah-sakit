Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_33
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_33 As New Digital.clsDigital_RJ_33
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sKODETUJUAN As String
    Private sTUJUAN As String
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNOREG.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNAMA.Text = dsPendaftaran.NAMAPASIEN
            txtJK.Text = dsPendaftaran.JENISKELAMIN
            txtUmur.Text = dsPendaftaran.USIA
            txtKELAS.Text = dsPendaftaran.KELASPELAYANAN
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtNOREG.Text = dsPendaftaran.KDKUNJUNGAN
            txtTanggal.Text = dsPendaftaran.DATE
            txtWAKTU.Text = Now
            grdDOCTOR.Text = dsPendaftaran.DOKTER
            grdDEPARTMENT.Text= dsPendaftaran.TUJUAN
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            sTUJUAN = dsPendaftaran.TUJUAN
            sKODETUJUAN = dsPendaftaran.KDTUJUAN
        Else
            txtNAMA.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtKELAS.ResetText()
            txtNOMORRM.ResetText()
            txtNOREG.ResetText()
            txtWAKTU.ResetText()
            txtTanggal.ResetText()
            grdDOCTOR.ResetText()
            grdDEPARTMENT.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        me.Text = EMedrekRJ_33.TITLE
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNOREG.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_NOIDUSER()
        fn_DOCTOR()
        fn_DEPARTMENT()

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
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        MemoEdit5.Properties.ReadOnly = Status
        MemoEdit6.Properties.ReadOnly = Status

        txtSTATUSDERMATOLOGISKEPALA.Properties.ReadOnly=Status
        txtSTATUSDERMATOLOGISLEHER.Properties.ReadOnly=Status
        txtSTATUSDERMATOLOGISDADA.Properties.ReadOnly=Status
        txtSTATUSDERMATOLOGISTANGAN.Properties.ReadOnly=Status
        txtSTATUSDERMATOLOGISKAKI.Properties.ReadOnly=Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                sIsOtority = True
            Else
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sIsOtority = False
            End If
        End If
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
        MemoEdit1.ResetText()
        MemoEdit7.ResetText()
        MemoEdit4.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        MemoEdit5.ResetText()
        MemoEdit6.ResetText()

        txtSTATUSDERMATOLOGISKEPALA.ResetText()
        txtSTATUSDERMATOLOGISLEHER.ResetText()
        txtSTATUSDERMATOLOGISDADA.ResetText()
        txtSTATUSDERMATOLOGISTANGAN.ResetText()
        txtSTATUSDERMATOLOGISKAKI.ResetText()

        deDATE.DateTime = Now

        fn_LoadAsessmenRawatJalan(txtNOREG.Text)
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text)

            With ds
                grdDOCTOR.Text = .DOKTER_NAMEDISPLAY
                grdDEPARTMENT.Text = sTUJUAN
                txt1.Text = .SDIGITALRJ33_1
                txt2.Text = .SDIGITALRJ33_2
                txt3.Text = .SDIGITALRJ33_3
                txt4.Text = .SDIGITALRJ33_4
                txt5.Text = .SDIGITALRJ33_5
                txt6.Text = .SDIGITALRJ33_6
                txt7.Text = .SDIGITALRJ33_7
                txt8.Text = .SDIGITALRJ33_8
                txt9.Text = .SDIGITALRJ33_9
                TextEdit4.Text = .SDIGITALRJ33_10
                TextEdit5.Text = .SDIGITALRJ33_11
                TextEdit6.Text = .SDIGITALRJ33_12
                TextEdit7.Text = .SDIGITALRJ33_13
                MemoEdit1.Text = .SDIGITALRJ33_14

                Try
                    Dim img = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).SDIGITALRJ33_15

                    picGAMBAR2.Image = ByteArrayToImage(img.ToArray())
                Catch oErr As Exception
                End Try

                MemoEdit7.Text = .SDIGITALRJ33_16
                MemoEdit4.Text = .SDIGITALRJ33_17
                MemoEdit2.Text = .SDIGITALRJ33_18
                MemoEdit3.Text = .SDIGITALRJ33_19
                MemoEdit5.Text = .SDIGITALRJ33_20
                MemoEdit6.Text = .SDIGITALRJ33_21

                txtSTATUSDERMATOLOGISKEPALA.Text = .SDIGITALRJ33_22
                txtSTATUSDERMATOLOGISLEHER.Text = .SDIGITALRJ33_23
                txtSTATUSDERMATOLOGISDADA.Text = .SDIGITALRJ33_24
                txtSTATUSDERMATOLOGISTANGAN.Text = .SDIGITALRJ33_25
                txtSTATUSDERMATOLOGISKAKI.Text = .SDIGITALRJ33_26

                deDATE.DateTime = .DATE
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenRawatJalan(ByVal Parameter As String)
        Dim oAsessmenRawatJalan As New Digital.clsDigital_RJ_08
        Dim dsAsessmenRawatJalan = oAsessmenRawatJalan.GetData(Parameter)

        Dim oAsessmenRawatJalan2 As New Digital.clsS_DIGITAL_ASKEP_RAWATJALAN
        Dim dsAsessmenRawatJalan2 = oAsessmenRawatJalan2.GetDatabyKDKUNJUNGAN(Parameter)

        If dsAsessmenRawatJalan IsNot Nothing Then
            'txtSOAP_O.Text = "Tensi : " & dsAsessmenRawatJalan.TANDA_VITAL_01 & " mmHg" & vbCrLf & vbCrLf & "Nadi : " & dsAsessmenRawatJalan.TANDA_VITAL_02 & " x/m" & vbCrLf & vbCrLf & "Respirasi : " & dsAsessmenRawatJalan.TANDA_VITAL_04 & " x/m" & vbCrLf & vbCrLf & "Suhu : " & dsAsessmenRawatJalan.TANDA_VITAL_03 & " °C" & vbCrLf & vbCrLf & "Berat Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_05 & " Kg" & vbCrLf & vbCrLf & "Tinggi Badan : " & dsAsessmenRawatJalan.TANDA_VITAL_06 & " Cm"
            txt1.Text = dsAsessmenRawatJalan.KELUHAN_UTAMA
            txt7.Text = dsAsessmenRawatJalan.TANDA_VITAL_05
            txt8.Text = dsAsessmenRawatJalan.TANDA_VITAL_06
            TextEdit4.Text = dsAsessmenRawatJalan.TANDA_VITAL_02
            TextEdit5.Text = dsAsessmenRawatJalan.TANDA_VITAL_01
            TextEdit6.Text = dsAsessmenRawatJalan.TANDA_VITAL_03
            TextEdit7.Text = dsAsessmenRawatJalan.TANDA_VITAL_04
            txt5.Text = dsAsessmenRawatJalan.KESADARAN_UMUM
            txt4.Text = dsAsessmenRawatJalan.ALERGI_02_02_TEXT
        ElseIf dsAsessmenRawatJalan2 IsNot Nothing Then
            txt1.Text = dsAsessmenRawatJalan2.KELUHAN_UTAMA
            txt7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_BB
            txt8.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TB
            TextEdit4.Text = dsAsessmenRawatJalan2.TANDA_VITAL_NADI
            TextEdit5.Text = dsAsessmenRawatJalan2.TANDA_VITAL_TD
            TextEdit6.Text = dsAsessmenRawatJalan2.TANDA_VITAL_SUHU
            TextEdit7.Text = dsAsessmenRawatJalan2.TANDA_VITAL_R
            txt5.Text = dsAsessmenRawatJalan2.KESADARAN_UMUM
            txt4.Text = dsAsessmenRawatJalan2.KEADAAN
        Else
            Exit Sub
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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
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
            Dim ds = oS_DIGITAL_RJ_33.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .KDDEPARTMENT = cint(sKODETUJUAN)
                .SDIGITALRJ33_1 = txt1.Text
                .SDIGITALRJ33_2 = txt2.Text
                .SDIGITALRJ33_3 = txt3.Text
                .SDIGITALRJ33_4 = txt4.Text
                .SDIGITALRJ33_5 = txt5.Text
                .SDIGITALRJ33_6 = txt6.Text
                .SDIGITALRJ33_7 = txt7.Text
                .SDIGITALRJ33_8 = txt8.Text
                .SDIGITALRJ33_9 = txt9.Text
                .SDIGITALRJ33_10 = TextEdit4.Text
                .SDIGITALRJ33_11 = TextEdit5.Text
                .SDIGITALRJ33_12 = TextEdit6.Text
                .SDIGITALRJ33_13 = TextEdit7.Text
                .SDIGITALRJ33_14 = MemoEdit1.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    picGAMBAR2.Image.Save(ms, picGAMBAR2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .SDIGITALRJ33_15 = data
                Catch oErr As Exception
                    Try
                        .SDIGITALRJ33_15 = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).SDIGITALRJ33_15
                    Catch ex As Exception

                    End Try
                End Try

                .SDIGITALRJ33_16 = MemoEdit7.Text
                .SDIGITALRJ33_17 = MemoEdit4.Text
                .SDIGITALRJ33_18 = MemoEdit2.Text
                .SDIGITALRJ33_19 = MemoEdit3.Text
                .SDIGITALRJ33_20 = MemoEdit5.Text
                .SDIGITALRJ33_21 = MemoEdit6.Text

                .SDIGITALRJ33_22 = txtSTATUSDERMATOLOGISKEPALA.Text
                .SDIGITALRJ33_23 = txtSTATUSDERMATOLOGISLEHER.Text
                .SDIGITALRJ33_24 = txtSTATUSDERMATOLOGISDADA.Text
                .SDIGITALRJ33_25 = txtSTATUSDERMATOLOGISTANGAN.Text
                .SDIGITALRJ33_26 = txtSTATUSDERMATOLOGISKAKI.Text

                .DOKTER_KODE = grdDOCTOR.EditValue
                .DOKTER_NAMEDISPLAY = grdDOCTOR.Text
                Try
                    .CETAK = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_33.GetData(txtNOREG.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_33.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_33.UpdateData(ds)
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
                    btnReload_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub

    Private Sub btnDiagnosa_Click() Handles btnDiagnosa.ItemClick
        Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        Dim dsMasterDiagnosa = oMasterDiagnosa.GetData(txtNOREG.Text)
        If dsMasterDiagnosa IsNot Nothing Then
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_EDIT, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        Else
            Dim frmMasterDiagnosa As New frmMasterDiagnosa
            frmMasterDiagnosa.LoadMe(FORM_MODE.FORM_MODE_ADD, txtNOREG.Text)
            frmMasterDiagnosa.ShowDialog(Me)
        End If

        If sCode = "Berhasil" Then
            Dim listDiagnosa As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail(txtNOREG.Text)
                listDiagnosa.Add(xloop.KATEGORI & " : " & xloop.REMARKS)
            Next

            Dim listProsedur As New List(Of String)
            For Each xloop In oMasterDiagnosa.GetDataDetail_(txtNOREG.Text)
                listProsedur.Add(xloop.SEQ + 1 & ". " & xloop.REMARKS)
            Next
            MemoEdit4.Text = String.Join(vbCrLf, listProsedur.ToArray)
            MemoEdit2.Text = String.Join(vbCrLf, listDiagnosa.ToArray)
        End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_33.GetDataByKunjungan(txtNOREG.Text)

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

            MemoEdit7.Text = String.Join(", ", listPenunjang.ToArray)
            MemoEdit3.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
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
    Private Sub fn_DOCTOR()
        'Dim oDOCTOR As New Master.clsDoctor
        'Try
        '    grdDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True And x.ADDRESS_COUNTRY <> "").ToList()
        '    grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
        '    grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Dokter DPJP Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub fn_DEPARTMENT()
        'Dim oDEPARTMENT As New Master.clsDepartment
        'Try
        '    grdDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
        '    grdDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
        '    grdDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        'Catch oErr As Exception
        '    MsgBox("Load Department Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub

    Private Sub btnEDITIMAGE_Click(sender As Object, e As EventArgs) Handles btnEDITIMAGE.Click
        Dim frmPopUp_Image As New frmPopUp_image1
        frmPopUp_Image.fn_LoadMe(picGAMBAR2.Image)
        frmPopUp_Image.ShowDialog()

        If sPicture IsNot Nothing Then
            picGAMBAR2.Image = sPicture
        End If

        picGAMBAR2.Focus()
        sFind10 = String.Empty
        sPicture = Nothing
    End Sub

    Private Sub frmEMedrekRJ_33_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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