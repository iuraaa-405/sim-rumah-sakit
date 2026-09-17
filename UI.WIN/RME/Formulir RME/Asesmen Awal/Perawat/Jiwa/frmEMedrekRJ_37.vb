Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmEMedrekRJ_37
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_37 As New Digital.clsDigital_RJ_37
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER As String
    Private sKoneksi As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

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
            'deDATE.EditValue = Now
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER
            grdDOCTOR.EditValue = dsPendaftaran.KDDOKTER
            grdDEPARTMENT.Text = dsPendaftaran.TUJUAN
            txt4.Text = dsPendaftaran.PENDIDIKAN
            txt5.Text = dsPendaftaran.ALAMAT
            TextEdit21.Text = dsPendaftaran.JENISKELAMIN
            TextEdit19.Text = dsPendaftaran.AGAMA
            TextEdit18.Text = dsPendaftaran.STATUS_KAWIN
            TextEdit23.Text = dsPendaftaran.HUBUNGAN_PEKERJAAN

        Else
            txtNAMA.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtKELAS.ResetText()
            txtNOMORRM.ResetText()
            txtNOREG.ResetText()
            txtWAKTU.ResetText()
            txtTanggal.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_37.TITLE
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
        MemoEdit1.Properties.ReadOnly = Status
        MemoEdit9.Properties.ReadOnly = Status
        MemoEdit8.Properties.ReadOnly = Status
        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit28.Properties.ReadOnly = Status
        CheckEdit27.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit29.Properties.ReadOnly = Status
        CheckEdit26.Properties.ReadOnly = Status
        CheckEdit30.Properties.ReadOnly = Status
        CheckEdit36.Properties.ReadOnly = Status
        CheckEdit35.Properties.ReadOnly = Status
        CheckEdit34.Properties.ReadOnly = Status
        CheckEdit33.Properties.ReadOnly = Status
        CheckEdit32.Properties.ReadOnly = Status
        CheckEdit31.Properties.ReadOnly = Status
        CheckEdit44.Properties.ReadOnly = Status
        CheckEdit37.Properties.ReadOnly = Status
        CheckEdit38.Properties.ReadOnly = Status
        CheckEdit39.Properties.ReadOnly = Status
        CheckEdit40.Properties.ReadOnly = Status
        CheckEdit41.Properties.ReadOnly = Status
        CheckEdit43.Properties.ReadOnly = Status
        CheckEdit51.Properties.ReadOnly = Status
        CheckEdit45.Properties.ReadOnly = Status
        CheckEdit46.Properties.ReadOnly = Status
        CheckEdit47.Properties.ReadOnly = Status
        CheckEdit48.Properties.ReadOnly = Status
        CheckEdit49.Properties.ReadOnly = Status
        CheckEdit50.Properties.ReadOnly = Status
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        MemoEdit10.Properties.ReadOnly = Status
        MemoEdit7.Properties.ReadOnly = Status
        MemoEdit4.Properties.ReadOnly = Status
        MemoEdit2.Properties.ReadOnly = Status
        MemoEdit3.Properties.ReadOnly = Status
        TextEdit20.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"
        MemoEdit1.ResetText()
        MemoEdit9.ResetText()
        MemoEdit8.ResetText()
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        MemoEdit10.ResetText()
        MemoEdit7.ResetText()
        MemoEdit4.ResetText()
        MemoEdit2.ResetText()
        MemoEdit3.ResetText()
        TextEdit20.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_37.GetData(txtNOREG.Text)

            With ds
                grdDOCTOR.EditValue = sKODEDOKTER
                deDATE.DateTime = .DATE
                MemoEdit1.Text = .SDIGITALRJ37_1
                MemoEdit9.Text = .SDIGITALRJ37_2
                MemoEdit8.Text = .SDIGITALRJ37_3
                CheckEdit1.Checked = .SDIGITALRJ37_4
                CheckEdit2.Checked = .SDIGITALRJ37_5
                CheckEdit4.Checked = .SDIGITALRJ37_6
                CheckEdit3.Checked = .SDIGITALRJ37_7
                CheckEdit6.Checked = .SDIGITALRJ37_8
                CheckEdit5.Checked = .SDIGITALRJ37_9
                CheckEdit7.Checked = .SDIGITALRJ37_10
                CheckEdit10.Checked = .SDIGITALRJ37_11
                CheckEdit9.Checked = .SDIGITALRJ37_12
                CheckEdit8.Checked = .SDIGITALRJ37_13
                CheckEdit12.Checked = .SDIGITALRJ37_14
                CheckEdit11.Checked = .SDIGITALRJ37_15
                CheckEdit15.Checked = .SDIGITALRJ37_16
                CheckEdit14.Checked = .SDIGITALRJ37_17
                CheckEdit13.Checked = .SDIGITALRJ37_18
                CheckEdit28.Checked = .SDIGITALRJ37_19
                CheckEdit27.Checked = .SDIGITALRJ37_20
                CheckEdit20.Checked = .SDIGITALRJ37_21
                CheckEdit19.Checked = .SDIGITALRJ37_22
                CheckEdit22.Checked = .SDIGITALRJ37_23
                CheckEdit21.Checked = .SDIGITALRJ37_24
                CheckEdit24.Checked = .SDIGITALRJ37_25
                CheckEdit23.Checked = .SDIGITALRJ37_26
                CheckEdit18.Checked = .SDIGITALRJ37_27
                CheckEdit17.Checked = .SDIGITALRJ37_28
                CheckEdit16.Checked = .SDIGITALRJ37_29
                CheckEdit29.Checked = .SDIGITALRJ37_30
                CheckEdit26.Checked = .SDIGITALRJ37_31
                CheckEdit30.Checked = .SDIGITALRJ37_32
                CheckEdit36.Checked = .SDIGITALRJ37_33
                CheckEdit35.Checked = .SDIGITALRJ37_34
                CheckEdit34.Checked = .SDIGITALRJ37_35
                CheckEdit33.Checked = .SDIGITALRJ37_36
                CheckEdit32.Checked = .SDIGITALRJ37_37
                CheckEdit31.Checked = .SDIGITALRJ37_38
                CheckEdit44.Checked = .SDIGITALRJ37_39
                CheckEdit37.Checked = .SDIGITALRJ37_40
                CheckEdit38.Checked = .SDIGITALRJ37_41
                CheckEdit39.Checked = .SDIGITALRJ37_42
                CheckEdit40.Checked = .SDIGITALRJ37_43
                CheckEdit41.Checked = .SDIGITALRJ37_44
                CheckEdit43.Checked = .SDIGITALRJ37_45
                CheckEdit51.Checked = .SDIGITALRJ37_46
                CheckEdit45.Checked = .SDIGITALRJ37_47
                CheckEdit46.Checked = .SDIGITALRJ37_48
                CheckEdit47.Checked = .SDIGITALRJ37_49
                CheckEdit48.Checked = .SDIGITALRJ37_50
                CheckEdit49.Checked = .SDIGITALRJ37_51
                CheckEdit50.Checked = .SDIGITALRJ37_52
                TextEdit1.Text = .SDIGITALRJ37_53
                TextEdit2.Text = .SDIGITALRJ37_54
                TextEdit3.Text = .SDIGITALRJ37_55
                TextEdit4.Text = .SDIGITALRJ37_56
                TextEdit5.Text = .SDIGITALRJ37_57
                MemoEdit10.Text = .SDIGITALRJ37_58
                MemoEdit7.Text = .SDIGITALRJ37_59
                MemoEdit4.Text = .SDIGITALRJ37_60
                MemoEdit2.Text = .SDIGITALRJ37_61
                MemoEdit3.Text = .SDIGITALRJ37_62
                CheckEdit25.Checked = .SDIGITALRJ37_63
                TextEdit20.Text = .SDIGITALRJ37_64

                BindingSource1.DataSource = oS_DIGITAL_RJ_37.GetDataDetail(txtNOREG.Text)
                grdDetail.DataSource = BindingSource1
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
            If txtNOREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNOREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If grdDEPARTMENT.Text = String.Empty Then
            '    MsgBox("Dibutuhkan Poliklinik", MsgBoxStyle.Exclamation, Me.Text)
            '    grdDEPARTMENT.Focus()
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
            Dim ds = oS_DIGITAL_RJ_37.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNOREG.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_37.GetData(txtNOREG.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .SDIGITALRJ37_1 = MemoEdit1.Text
                .SDIGITALRJ37_2 = MemoEdit9.Text
                .SDIGITALRJ37_3 = MemoEdit8.Text
                .SDIGITALRJ37_4 = CheckEdit1.Checked
                .SDIGITALRJ37_5 = CheckEdit2.Checked
                .SDIGITALRJ37_6 = CheckEdit4.Checked
                .SDIGITALRJ37_7 = CheckEdit3.Checked
                .SDIGITALRJ37_8 = CheckEdit6.Checked
                .SDIGITALRJ37_9 = CheckEdit5.Checked
                .SDIGITALRJ37_10 = CheckEdit7.Checked
                .SDIGITALRJ37_11 = CheckEdit10.Checked
                .SDIGITALRJ37_12 = CheckEdit9.Checked
                .SDIGITALRJ37_13 = CheckEdit8.Checked
                .SDIGITALRJ37_14 = CheckEdit12.Checked
                .SDIGITALRJ37_15 = CheckEdit11.Checked
                .SDIGITALRJ37_16 = CheckEdit15.Checked
                .SDIGITALRJ37_17 = CheckEdit14.Checked
                .SDIGITALRJ37_18 = CheckEdit13.Checked
                .SDIGITALRJ37_19 = CheckEdit28.Checked
                .SDIGITALRJ37_20 = CheckEdit27.Checked
                .SDIGITALRJ37_21 = CheckEdit20.Checked
                .SDIGITALRJ37_22 = CheckEdit19.Checked
                .SDIGITALRJ37_23 = CheckEdit22.Checked
                .SDIGITALRJ37_24 = CheckEdit21.Checked
                .SDIGITALRJ37_25 = CheckEdit24.Checked
                .SDIGITALRJ37_26 = CheckEdit23.Checked
                .SDIGITALRJ37_27 = CheckEdit18.Checked
                .SDIGITALRJ37_28 = CheckEdit17.Checked
                .SDIGITALRJ37_29 = CheckEdit16.Checked
                .SDIGITALRJ37_30 = CheckEdit29.Checked
                .SDIGITALRJ37_31 = CheckEdit26.Checked
                .SDIGITALRJ37_32 = CheckEdit30.Checked
                .SDIGITALRJ37_33 = CheckEdit36.Checked
                .SDIGITALRJ37_34 = CheckEdit35.Checked
                .SDIGITALRJ37_35 = CheckEdit34.Checked
                .SDIGITALRJ37_36 = CheckEdit33.Checked
                .SDIGITALRJ37_37 = CheckEdit32.Checked
                .SDIGITALRJ37_38 = CheckEdit31.Checked
                .SDIGITALRJ37_39 = CheckEdit44.Checked
                .SDIGITALRJ37_40 = CheckEdit37.Checked
                .SDIGITALRJ37_41 = CheckEdit38.Checked
                .SDIGITALRJ37_42 = CheckEdit39.Checked
                .SDIGITALRJ37_43 = CheckEdit40.Checked
                .SDIGITALRJ37_44 = CheckEdit41.Checked
                .SDIGITALRJ37_45 = CheckEdit43.Checked
                .SDIGITALRJ37_46 = CheckEdit51.Checked
                .SDIGITALRJ37_47 = CheckEdit45.Checked
                .SDIGITALRJ37_48 = CheckEdit46.Checked
                .SDIGITALRJ37_49 = CheckEdit47.Checked
                .SDIGITALRJ37_50 = CheckEdit48.Checked
                .SDIGITALRJ37_51 = CheckEdit49.Checked
                .SDIGITALRJ37_52 = CheckEdit50.Checked
                .SDIGITALRJ37_53 = TextEdit1.Text
                .SDIGITALRJ37_54 = TextEdit2.Text
                .SDIGITALRJ37_55 = TextEdit3.Text
                .SDIGITALRJ37_56 = TextEdit4.Text
                .SDIGITALRJ37_57 = TextEdit5.Text
                .SDIGITALRJ37_58 = MemoEdit10.Text
                .SDIGITALRJ37_59 = MemoEdit7.Text
                .SDIGITALRJ37_60 = MemoEdit4.Text
                .SDIGITALRJ37_61 = MemoEdit2.Text
                .SDIGITALRJ37_62 = MemoEdit3.Text
                .SDIGITALRJ37_63 = CheckEdit25.Checked
                .SDIGITALRJ37_64 = TextEdit20.Text

                .DOKTER_KODE = sKODEDOKTER
                .DOKTER_NAMEDISPLAY = sNAMADOKTER

                Try
                    .CETAK = oS_DIGITAL_RJ_37.GetData(txtNOREG.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                Try
                    Dim oSetUser As New Setting.clsUser
                    Dim dsUser = oSetUser.GetData(sUserID)
                    If dsUser.ISOTORTY = True Then
                        .KDUSER = oS_DIGITAL_RJ_37.GetData(txtNOREG.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RJ_37.GetData(txtNOREG.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try

            End With


            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_RJ_37.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RJ_37.GetStructureDetail
                With dsDetail
                    .SEQ = i
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .DATEUPDATED = Now
                    .DATECREATED = Now
                    .DATE = CDate(grvDetail.GetRowCellValue(i, colTANGGAL))
                    .SDIGITALRJ25D_1 = grvDetail.GetRowCellValue(i, colMASALAH)
                    .SDIGITALRJ25D_2 = grvDetail.GetRowCellValue(i, colTERAPI)
                    .SDIGITALRJ25D_3 = String.Empty
                End With
                arrDetail.Add(dsDetail)
            Next


            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_37.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_37.UpdateData(ds, arrDetail)
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
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNOREG.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
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
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub btnTambah_Click(sender As Object, e As EventArgs) Handles btnTambah.Click
        Dim frmPopUpPemeriksaanPsikologi As New frmPopUpPemeriksaanPsikologi
        frmPopUpPemeriksaanPsikologi.fn_LoadMe(Now, "", "", "")
        frmPopUpPemeriksaanPsikologi.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetail.Focus()
            grvDetail.AddNewRow()
            grvDetail.SetFocusedRowCellValue(colTANGGAL, sDATEFrom)
            grvDetail.SetFocusedRowCellValue(colMASALAH, sFind1)
            grvDetail.SetFocusedRowCellValue(colTERAPI, sFind2)

            grvDetail.UpdateCurrentRow()
        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty

        grdDetail.Focus()
    End Sub

#End Region
#Region "Handlle"
    Private Sub chPenampilanUmum_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click, CheckEdit2.Click
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
    End Sub
    Private Sub chSikapTerhadapPemeriksa_Click(sender As Object, e As EventArgs) Handles CheckEdit4.Click, CheckEdit3.Click
        CheckEdit4.Checked = False
        CheckEdit3.Checked = False
    End Sub
    Private Sub chAfek_Click(sender As Object, e As EventArgs) Handles CheckEdit6.Click, CheckEdit5.Click, CheckEdit7.Click
        CheckEdit6.Checked = False
        CheckEdit5.Checked = False
        CheckEdit7.Checked = False
    End Sub
    Private Sub chRomanMuka_Click(sender As Object, e As EventArgs) Handles CheckEdit10.Click, CheckEdit9.Click, CheckEdit8.Click
        CheckEdit10.Checked = False
        CheckEdit9.Checked = False
        CheckEdit8.Checked = False
    End Sub
    Private Sub chProsesPikir_Click(sender As Object, e As EventArgs) Handles CheckEdit12.Click, CheckEdit11.Click
        CheckEdit10.Checked = False
        CheckEdit9.Checked = False
    End Sub
    Private Sub chGangguanPersepsi_Click(sender As Object, e As EventArgs) Handles CheckEdit15.Click, CheckEdit14.Click, CheckEdit13.Click
        CheckEdit15.Checked = False
        CheckEdit14.Checked = False
        CheckEdit13.Checked = False
    End Sub
    Private Sub chKognitifMemori_Click(sender As Object, e As EventArgs) Handles CheckEdit28.Click, CheckEdit27.Click
        CheckEdit28.Checked = False
        CheckEdit27.Checked = False
    End Sub
    Private Sub chKognitifkonsentrasi_Click(sender As Object, e As EventArgs) Handles CheckEdit20.Click, CheckEdit19.Click
        CheckEdit20.Checked = False
        CheckEdit19.Checked = False
    End Sub
    Private Sub chKognitifOrientasi_Click(sender As Object, e As EventArgs) Handles CheckEdit22.Click, CheckEdit21.Click
        CheckEdit22.Checked = False
        CheckEdit21.Checked = False
    End Sub
    Private Sub chKognitifKemampuanVerbal_Click(sender As Object, e As EventArgs) Handles CheckEdit24.Click, CheckEdit23.Click
        CheckEdit24.Checked = False
        CheckEdit23.Checked = False
    End Sub
    Private Sub chEmosi_Click(sender As Object, e As EventArgs) Handles CheckEdit18.Click, CheckEdit23.Click
        CheckEdit24.Checked = False
        CheckEdit23.Checked = False
    End Sub
    Private Sub chPerilaku_Click(sender As Object, e As EventArgs) Handles CheckEdit25.Click, CheckEdit19.Click, CheckEdit29.Click, CheckEdit26.Click
        CheckEdit25.Checked = False
        CheckEdit19.Checked = False
        CheckEdit29.Checked = False
        CheckEdit26.Checked = False
    End Sub

    Private Sub frmEMedrekRJ_37_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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