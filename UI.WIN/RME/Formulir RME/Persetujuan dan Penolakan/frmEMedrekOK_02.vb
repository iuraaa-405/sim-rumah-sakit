Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports QRCoder

Public Class frmEMedrekOK_02
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_OK_02 As New Digital.clsS_DIGITAL_OK_02
    Private down As Boolean = False
    Private sNoid As String = String.Empty
    Private sKoneksi As String = String.Empty
    Private oUser As New Setting.clsUser
    Private sKDKUNJUNGAN As String = String.Empty
    Private sDPJP As String = String.Empty
    Private sIsOtority As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional Noid As String = "")
        oFormMode = FormMode
        sNoid = Noid

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)
        sKDKUNJUNGAN = KDKUNJUNGAN

        If dsPendaftaran IsNot Nothing Then
            txtNAMA.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtNOMORRM.Text = dsPendaftaran.KDCUSTOMER
            txtTUJUAN.Text = dsPendaftaran.TUJUAN
            sDPJP = dsPendaftaran.KDDOKTER
        Else
            txtNAMA.ResetText()
            txtUmur.ResetText()
            txtNOMORRM.ResetText()
            grdDOCTOR.ResetText()
            txtTUJUAN.ResetText()
        End If
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = sKDKUNJUNGAN.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_DOCTOR()
        fn_ITEMOPERASILIST()

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
        txtJUDULFORMULIR.Properties.ReadOnly = Status
        txtJUDUL1.Properties.ReadOnly = Status
        txtJUDUL2.Properties.ReadOnly = Status
        txtNO_1.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        grdKDDOCTOR_2.Properties.ReadOnly = Status
        txtPERSETUJUAN.ReadOnly = Status
        grvDetail_Item.OptionsBehavior.ReadOnly = Status
        grdKDITEMOPERASI.Properties.ReadOnly = Status
        txtMEMO_1.Properties.ReadOnly = Status
        txtMEMO_2.Properties.ReadOnly = Status
        txtMEMO_3.Properties.ReadOnly = Status

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
        grdKDITEMOPERASI.ResetText()
        grdKDDOCTOR_2.ResetText()
        txtPERSETUJUAN.ResetText()
        txtTANDATANGAN_PENERIMAINFORMASI.ResetText()
        txtKDTANDATANGAN_YANGMENYATAKAN.ResetText()
        txtSAKSI1.ResetText()
        txtSAKSI2.ResetText()
        txtJUDULFORMULIR.ResetText()
        txtJUDUL1.ResetText()
        txtJUDUL2.ResetText()
        txtNO_1.Text = txtNAMA.Text
        Dim oSetUser As New Setting.clsUser
        Dim dsSetUser = oSetUser.GetData(sUserID)
        If dsSetUser IsNot Nothing Then
            grdDOCTOR.Text = dsSetUser.KDDOCTOR
        Else
            grdDOCTOR.Text = sDPJP
        End If

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_OK_02.GetData(sNoid)

            With ds
                grdDOCTOR.EditValue = .DOCTOR_KODE
                grdKDDOCTOR_2.EditValue = .DOCTOR2_KODE
                txtNO_1.Text = .SDIGITALOK02_1
                txtTANDATANGAN_PENERIMAINFORMASI.Text = .SDIGITALOK02_4
                txtSEQ1.Text = .SDIGITALOK02_5
                txtKDTANDATANGAN_YANGMENYATAKAN.Text = .SDIGITALOK02_6
                txtSEQ2.Text = .SDIGITALOK02_7
                txtSAKSI1.Text = .SDIGITALOK02_8
                txtSEQ3.Text = .SDIGITALOK02_9
                txtSAKSI2.Text = .SDIGITALOK02_11
                txtSEQ4.Text = .SDIGITALOK02_12
                txtJUDULFORMULIR.Text = .SDIGITALOK02_3
                txtPERSETUJUAN.Text = .SDIGITALOK02_20
                lblOperator.Text = .SDIGITALOK02_10
                grdKDITEMOPERASI.Text = .SDIGITALOK02_14
                txtJUDUL1.Text = .SDIGITALOK02_21
                txtJUDUL2.Text = .SDIGITALOK02_13
                txtMEMO_1.Text = .SDIGITALOK02_15
                txtMEMO_2.Text = .SDIGITALOK02_16
                txtMEMO_3.Text = .SDIGITALOK02_17
                Try
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(txtTANDATANGAN_PENERIMAINFORMASI.Text, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    picPENERIMAINFORMASI.Image = code.GetGraphic(6)
                Catch ex As Exception
                    picPENERIMAINFORMASI.Image = Nothing
                End Try

                Try
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(txtKDTANDATANGAN_YANGMENYATAKAN.Text, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    picYANGMENYATAKAN.Image = code.GetGraphic(6)
                Catch ex As Exception
                    picYANGMENYATAKAN.Image = Nothing
                End Try

                Try
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(txtSAKSI1.Text, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    picSAKSI1.Image = code.GetGraphic(6)
                Catch ex As Exception
                    picSAKSI1.Image = Nothing
                End Try

                Try
                    Dim gen As New QRCodeGenerator
                    Dim data = gen.CreateQrCode(txtSAKSI2.Text, QRCodeGenerator.ECCLevel.Q)
                    Dim code As New QRCode(data)
                    picSAKSI2.Image = code.GetGraphic(6)
                Catch ex As Exception
                    picSAKSI2.Image = Nothing
                End Try

                deDATE.DateTime = .DATE

                bindingSource_ITEM.DataSource = oS_DIGITAL_OK_02.GetDataDetail(.KDPERSETUJUAN).OrderBy(Function(x) x.NO).ToList()
                grdDetail_ITEM.DataSource = bindingSource_ITEM

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sKDKUNJUNGAN = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If

            If txtNO_1.Text = String.Empty Then
                MsgBox("Dibutuhkan Penerima Informasi", MsgBoxStyle.Exclamation, Me.Text)
                txtNO_1.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grvDetail_Item.RowCount < 2 Then
                MsgBox("Dibutuhkan Informasi", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan DPJP", MsgBoxStyle.Exclamation, Me.Text)
                grdDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "OPERASI" Then
                If grdKDDOCTOR_2.Text = String.Empty Then
                    MsgBox("Dibutuhkan Operator", MsgBoxStyle.Exclamation, Me.Text)
                    grdKDDOCTOR_2.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            ElseIf grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "ANESTESI" Then
                If grdKDDOCTOR_2.Text = String.Empty Then
                    MsgBox("Dibutuhkan Operator", MsgBoxStyle.Exclamation, Me.Text)
                    grdKDDOCTOR_2.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            End If

            If txtJUDULFORMULIR.Text = String.Empty Then
                MsgBox("Dibutuhkan Nama Tindakan", MsgBoxStyle.Exclamation, Me.Text)
                txtJUDULFORMULIR.Focus()
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
            Dim ds = oS_DIGITAL_OK_02.GetStructureHeader
            With ds
                .KDPERSETUJUAN = sNoid
                .KDKUNJUNGAN = sKDKUNJUNGAN
                Try
                    .DATECREATED = oS_DIGITAL_OK_02.GetData(sKDKUNJUNGAN).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .DOCTOR_KODE = grdDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdDOCTOR.Text
                .DOCTOR2_KODE = grdKDDOCTOR_2.EditValue
                .DOCTOR2_NAME_DISPLAY = grdKDDOCTOR_2.Text
                .DEPARTMENT_KODE = ""
                .DEPARTMENT_NAME_DISPLAY = ""
                .SDIGITALOK02_1 = txtNO_1.Text
                .SDIGITALOK02_2 = ""
                .SDIGITALOK02_3 = txtJUDULFORMULIR.Text
                .SDIGITALOK02_4 = txtTANDATANGAN_PENERIMAINFORMASI.Text
                .SDIGITALOK02_5 = txtSEQ1.Text
                .SDIGITALOK02_6 = txtKDTANDATANGAN_YANGMENYATAKAN.Text
                .SDIGITALOK02_7 = txtSEQ2.Text
                .SDIGITALOK02_8 = txtSAKSI1.Text
                .SDIGITALOK02_9 = txtSEQ3.Text
                .SDIGITALOK02_10 = lblOperator.Text
                .SDIGITALOK02_11 = txtSAKSI2.Text
                .SDIGITALOK02_12 = txtSEQ4.Text
                .SDIGITALOK02_13 = txtJUDUL2.Text
                .SDIGITALOK02_14 = grdKDITEMOPERASI.EditValue
                .SDIGITALOK02_15 = txtMEMO_1.Text
                .SDIGITALOK02_16 = txtMEMO_2.Text
                .SDIGITALOK02_17 = txtMEMO_3.Text
                .SDIGITALOK02_18 = False
                .SDIGITALOK02_19 = False
                .SDIGITALOK02_20 = txtPERSETUJUAN.Text.ToString
                .SDIGITALOK02_21 = txtJUDUL1.Text
                .SDIGITALOK02_22 = ""
                .SDIGITALOK02_23 = ""
                .SDIGITALOK02_24 = ""
                .SDIGITALOK02_25 = False
                .SDIGITALOK02_26 = False
                .SDIGITALOK02_27 = ""
                .SDIGITALOK02_28 = ""
                .SDIGITALOK02_29 = grdKDDOCTOR_2.Text
                .SDIGITALOK02_30 = ""
                .SDIGITALOK02_31 = ""
                Try
                    .CETAK = oS_DIGITAL_OK_02.GetData(sKDKUNJUNGAN).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_OK_02.GetData(sKDKUNJUNGAN).KDUSER
                        .KDUSER_SIGANTURE = oS_DIGITAL_OK_02.GetData(sKDKUNJUNGAN).KDUSER_SIGANTURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGANTURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGANTURE = sUserSIGNATURE
                End Try
                .SDIGITALOK02_TEXT = ""
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_OK_02.GetStructureDetailList
            For i As Integer = 0 To grvDetail_Item.RowCount - 2
                Dim dsDetail = oS_DIGITAL_OK_02.GetStructureDetail
                With dsDetail
                    .KDPERSETUJUAN = ds.KDPERSETUJUAN
                    .KDITEMOPERASI = grdKDITEMOPERASI.EditValue
                    .NO = grvDetail_Item.GetRowCellValue(i, colNO)
                    .JENISINFORMASI = grvDetail_Item.GetRowCellValue(i, colJENISINFORMASI)
                    .ISIINFORMASI = grvDetail_Item.GetRowCellValue(i, colISIINFORMASI)
                    .BERITANDA = grvDetail_Item.GetRowCellValue(i, colBERITANDA)
                    .REMARKS = ""
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_OK_02.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_OK_02.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail_Item.DeleteSelectedRows()
    End Sub
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
        If MsgBox("Save " & sKDKUNJUNGAN.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & sKDKUNJUNGAN.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_ITEMOPERASI(ByVal KDITEMOPERASI As String)
        grvDetail_Item.OptionsSelection.MultiSelect = True
        grvDetail_Item.SelectAll()
        grvDetail_Item.DeleteSelectedRows()
        grvDetail_Item.OptionsSelection.MultiSelect = False

        Dim oItemOperasi As New Master.clsItemOperasi
        Try
            Dim ds = (From x In oItemOperasi.GetDataDetail()
                      Where x.KDITEMOPERASI = KDITEMOPERASI And x.M_ITEM_OPERASI_H.ISACTIVE = True
                      Select x.NO, x.JENISINFORMASI, x.ISIINFORMASI)

            For Each iLoop In ds
                grvDetail_Item.Focus()
                grvDetail_Item.AddNewRow()
                grvDetail_Item.SetFocusedRowCellValue(colNO, iLoop.NO)
                grvDetail_Item.SetFocusedRowCellValue(colJENISINFORMASI, iLoop.JENISINFORMASI)
                grvDetail_Item.SetFocusedRowCellValue(colISIINFORMASI, iLoop.ISIINFORMASI)
                grvDetail_Item.SetFocusedRowCellValue(colBERITANDA, True)
                grvDetail_Item.UpdateCurrentRow()
            Next

        Catch oErr As Exception
            MsgBox("Load Sub Item Operasi : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_ITEMOPERASILIST()
        Dim oItemOperasi As New Master.clsItemOperasi
        Try
            grdKDITEMOPERASI.Properties.DataSource = oItemOperasi.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDITEMOPERASI.Properties.ValueMember = "KDITEMOPERASI"
            grdKDITEMOPERASI.Properties.DisplayMember = "DESCRIPTION"
        Catch oErr As Exception
            MsgBox("Load Sub Item Operasi List : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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

            grdKDDOCTOR_2.Properties.DataSource = ds.Tables("DOKTER")
            grdKDDOCTOR_2.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_2.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDOCTOR_2_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDDOCTOR_2.EditValueChanged
        fn_LoadSignatureOperator()
    End Sub
    Private Sub grdDOCTOR_EditValueChanged(sender As Object, e As EventArgs) Handles grdDOCTOR.EditValueChanged
        fn_LoadSignatureDokterPemberiInformasi()
    End Sub
#End Region
#Region "Function Load Tanda Tangan"
    Private Sub fn_LoadSignatureDokterPemberiInformasi()
        picDPJP.Image = Nothing
        If grdDOCTOR.Text <> "" Then
            Dim dsUser = oUser.GetDataByDokter(grdDOCTOR.EditValue)
            If dsUser IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(oUser.GetDataSignatuerUser(dsUser.KDUSER), QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picDPJP.Image = code.GetGraphic(6)
            End If
        End If
    End Sub
    Private Sub fn_LoadSignatureOperator()
        picOperator.Image = Nothing
        If grdKDDOCTOR_2.Text <> "" Then
            Dim dsUser = oUser.GetDataByDokter(grdKDDOCTOR_2.EditValue)
            If dsUser IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(oUser.GetDataSignatuerUser(dsUser.KDUSER), QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picOperator.Image = code.GetGraphic(6)
            End If
        End If
    End Sub
    Private Sub fn_LoadSEQ(ByVal sParameter As String, ByVal sSEQ As Integer)
        If sSEQ = 1 Then
            picPENERIMAINFORMASI.Image = Nothing
            Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
            Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, sSEQ)
            If dsSignature IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim sURL As String = oSignature.GetDataSignatuerPersetujuan(sParameter, sSEQ)
                Dim data = gen.CreateQrCode(sURL, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picPENERIMAINFORMASI.Image = code.GetGraphic(6)
                txtTANDATANGAN_PENERIMAINFORMASI.Text = sURL
                txtNO_1.Text = dsSignature.NAMA
            End If
        ElseIf sSEQ = 2 Then
            picYANGMENYATAKAN.Image = Nothing
            Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
            Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, sSEQ)
            If dsSignature IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim sURL As String = oSignature.GetDataSignatuerPersetujuan(sParameter, sSEQ)
                Dim data = gen.CreateQrCode(sURL, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picYANGMENYATAKAN.Image = code.GetGraphic(6)
                txtKDTANDATANGAN_YANGMENYATAKAN.Text = sURL
            End If
        ElseIf sSEQ = 3 Then
            picSAKSI1.Image = Nothing
            Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
            Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, sSEQ)
            If dsSignature IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim sURL As String = oSignature.GetDataSignatuerPersetujuan(sParameter, sSEQ)
                Dim data = gen.CreateQrCode(sURL, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picSAKSI1.Image = code.GetGraphic(6)
                txtSAKSI1.Text = sURL
            End If
        ElseIf sSEQ = 4 Then
            picSAKSI2.Image = Nothing
            Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
            Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, sSEQ)
            If dsSignature IsNot Nothing Then
                Dim gen As New QRCodeGenerator
                Dim sURL As String = oSignature.GetDataSignatuerPersetujuan(sParameter, sSEQ)
                Dim data = gen.CreateQrCode(sURL, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                picSAKSI2.Image = code.GetGraphic(6)
                txtSAKSI2.Text = sURL

            End If
        End If
    End Sub
    Private Sub btnPenerimInformasi_Click(sender As Object, e As EventArgs) Handles btnSEQ1.Click
        Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
        Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, txtSEQ1.Text)
        If dsSignature Is Nothing Then
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDKUNJUNGAN, txtSEQ1.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ1.Text)
            End Try
        Else
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDKUNJUNGAN, txtSEQ1.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ1.Text)
            End Try
        End If
    End Sub
    Private Sub btnYANGMENYATAKAN_Click(sender As Object, e As EventArgs) Handles btnYANGMENYATAKAN.Click
        Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
        Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, txtSEQ2.Text)
        If dsSignature Is Nothing Then
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDKUNJUNGAN, txtSEQ2.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ2.Text)
            End Try
        Else
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDKUNJUNGAN, txtSEQ2.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ2.Text)
            End Try
        End If
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
        Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, txtSEQ3.Text)
        If dsSignature Is Nothing Then
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDKUNJUNGAN, txtSEQ3.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ3.Text)
            End Try
        Else
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDKUNJUNGAN, txtSEQ3.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ3.Text)
            End Try
        End If
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Dim oSignature As New Digital.clsS_DIGITAL_OK_SIGNATURE
        Dim dsSignature = oSignature.GetData(sKDKUNJUNGAN, txtSEQ4.Text)
        If dsSignature Is Nothing Then
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDKUNJUNGAN, txtSEQ4.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ4.Text)
            End Try
        Else
            Dim frmSignaturePersetujuandanPenolakan As New frmSignaturePersetujuandanPenolakan
            Try
                frmSignaturePersetujuandanPenolakan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDKUNJUNGAN, txtSEQ4.Text)
                frmSignaturePersetujuandanPenolakan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSignaturePersetujuandanPenolakan Is Nothing Then frmSignaturePersetujuandanPenolakan.Dispose()
                frmSignaturePersetujuandanPenolakan = Nothing
                fn_LoadSEQ(sKDKUNJUNGAN, txtSEQ4.Text)
            End Try
        End If
    End Sub
    Private Sub cboPersetujuan_SelectedIndexChanged(sender As Object, e As EventArgs)
        'If isLoad = True Then
        '    If grdKDITEMOPERASI.Text = "" Then
        '        MsgBox("Template Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
        '        cboPersetujuan.ResetText()
        '        Exit Sub
        '    End If

        '    fn_ITEMOPERASI(grdKDITEMOPERASI.EditValue)

        '    If cboPersetujuan.SelectedIndex = 0 Then
        '        txtJUDUL2.Text = "PERSETUJUAN TINDAKAN OPERASI"
        '        lblOperator.Text = "* Operator"
        '        txtJUDUL.Text = "INFORMASI DAN PERSETUJUAN TINDAKAN OPERASI " & grdKDITEMOPERASI.Text

        '    ElseIf cboPersetujuan.SelectedIndex = 1 Then
        '        txtJUDUL2.Text = "PENOLAKAN TINDAKAN OPERASI"
        '        lblOperator.Text = "* Operator"
        '        txtJUDUL.Text = "PERNYATAAN PENOLAKAN TINDAKAN OPERASI " & grdKDITEMOPERASI.Text

        '    ElseIf cboPersetujuan.SelectedIndex = 2 Then
        '        txtJUDUL2.Text = "PERSETUJUAN TINDAKAN ANESTESI"
        '        lblOperator.Text = "* dr. Anestesi"
        '        txtJUDUL.Text = "INFORMASI DAN PERSETUJUAN TINDAKAN ANESTESI " & grdKDITEMOPERASI.Text

        '    ElseIf cboPersetujuan.SelectedIndex = 3 Then
        '        txtJUDUL2.Text = "PENOLAKAN TINDAKAN ANESTESI"
        '        lblOperator.Text = "* dr. Anestesi"
        '        txtJUDUL.Text = "PERNYATAAN PENOLAKAN TINDAKAN ANESTESI " & grdKDITEMOPERASI.Text

        '    ElseIf cboPersetujuan.SelectedIndex = 4 Then
        '        txtJUDUL2.Text = "PERSETUJUAN PEMBERIAN DARAH DAN PRODUK DARAH"
        '    ElseIf cboPersetujuan.SelectedIndex = 5 Then
        '        txtJUDUL2.Text = "PENOLAKAN TINDAKAN MEDIS"
        '    End If
        'End If
    End Sub
    Private Sub grdKDITEMOPERASI_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDITEMOPERASI.EditValueChanged
        If isLoad = True Then
            If grdKDITEMOPERASI.Text = "" Then
                MsgBox("Template Belum dipilih", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If

            grdKDDOCTOR_2.Visible = True

            fn_ITEMOPERASI(grdKDITEMOPERASI.EditValue)

            If grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "OPERASI" Then
                txtJUDULFORMULIR.Text = grdKDITEMOPERASI.Text
                txtJUDUL1.Text = "PEMBERIAN INFORMASI"
                txtJUDUL2.Text = IIf(grdKDITEMOPERASI.Text.Contains("PENOLAKAN"), "PENOLAKAN TINDAKAN OPERASI", "PERSETUJUAN TINDAKAN OPERASI")
                lblOperator.Text = "*Operator"
                txtPERSETUJUAN.Text = "Yang bertandatangan di bawah ini, saya , nama _________________________, umur ______ " & "      " & " tahun " & " laki-laki / perempuan*, alamat " & vbCrLf _
                                      & "_______________________________________________________________ " & vbCrLf &
                                      "dengan ini menyatakan persetujuan untuk dilakukannya tindakan " _
                                       & "____________________________________" & vbCrLf &
                                       "terhadap saya / ___________________ saya*" _
                                       & " " & vbCrLf &
                                       "bernama___________________, umur ______tahun, laki-laki / perempuan*, " _
                                       & " " & vbCrLf &
                                       "alamat " _
                                       & "____________________________________ " & vbCrLf &
                                       "Saya memahami perlunya dan manfaat tindakan tersebut sebagaimana telah dijelaskan seperti di atas kepada saya, termasuk risiko dan komplikasi yang mungkin timbul. " _
                                       & " " & vbCrLf &
                                       "Saya juga menyadari bahwa oleh karena ilmu kedokteran bukanlah ilmu pasti, maka keberhasilan tindakan kedokteran bukanlah keniscayaan, melainkan sangat bergantung kepada izin Tuhan Yang Maha Esa."
            ElseIf grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "ANESTESI" Then
                txtJUDULFORMULIR.Text = grdKDITEMOPERASI.Text
                txtJUDUL1.Text = "PEMBERIAN INFORMASI"
                txtJUDUL2.Text = IIf(grdKDITEMOPERASI.Text.Contains("PENOLAKAN"), "PENOLAKAN TINDAKAN ANESTESI", "PERSETUJUAN TINDAKAN ANESTESI")
                lblOperator.Text = "*dr. Anestesi"
                txtPERSETUJUAN.Text = "Yang bertandatangan di bawah ini, saya , nama _________________________, umur ______ " & "      " & " tahun " & " laki-laki / perempuan*, alamat " & vbCrLf _
                                          & "_______________________________________________________________ " & vbCrLf &
                                          "dengan ini menyatakan persetujuan untuk dilakukannya tindakan " _
                                           & "____________________________________" & vbCrLf &
                                           "terhadap saya / ___________________ saya*" _
                                           & " " & vbCrLf &
                                           "bernama___________________, umur ______tahun, laki-laki / perempuan*, " _
                                           & " " & vbCrLf &
                                           "alamat " _
                                           & "____________________________________ " & vbCrLf &
                                           "Saya memahami perlunya dan manfaat tindakan tersebut sebagaimana telah dijelaskan seperti di atas kepada saya, termasuk risiko dan komplikasi yang mungkin timbul. " _
                                           & " " & vbCrLf &
                                           "Saya juga menyadari bahwa oleh karena ilmu kedokteran bukanlah ilmu pasti, maka keberhasilan tindakan kedokteran bukanlah keniscayaan, melainkan sangat bergantung kepada izin Tuhan Yang Maha Esa."
            ElseIf grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "PEMBERIAN DARAH DAN PRODUK DARAH" Then
                txtJUDULFORMULIR.Text = grdKDITEMOPERASI.Text
                txtJUDUL1.Text = "PEMBERIAN INFORMASI"
                txtJUDUL2.Text = IIf(grdKDITEMOPERASI.Text.Contains("PENOLAKAN"), "PENOLAKAN PEMBERIAN DARAH DAN PRODUK DARAH", "PERSETUJUAN PEMBERIAN DARAH DAN PRODUK DARAH")
                lblOperator.Text = ""
                grdKDDOCTOR_2.Visible = False
                txtPERSETUJUAN.Text = "Yang bertanda tangan di bawah ini, saya, nama" & vbCrLf _
                                          & "____________________________________,umur______tahun______, " & vbCrLf &
                                          "Laki-laki / perempuan , " _
                                           & "Alamat____________________________________" & vbCrLf &
                                           "Dengan ini menyatakan persetujuan untuk di lakukan Pemberian darah dan Produk Darah" _
                                           & " " & vbCrLf &
                                           "Terhadap___________________, " _
                                           & " " & vbCrLf &
                                           "Nama ___________________Umur_____tahun,  laki-laki / perempuan " _
                                           & "" & vbCrLf &
                                           "Alamat____________________________________" _
                                           & " " & vbCrLf &
                                           "Telah membaca / dibacakan formulir persetujuan pemberian  darah dan produk darah yang akan di lakukan terhadap diri saya sendiri / pihak yang saya wakili, sehingga saya memahami alasan memerlukan darah dan produk darah, memahami resiko yang mungkin terjadi saat atau sesudah pelaksanaan pemberian darah dan produk darah Saya memahami perlunya dan manfaat tindakan tersebut sebagaimana telah dijelaskan seperti di atas kepada saya , termasuk resiko dan komplikasi yang mungkin timbul. Saya juga menyadari bahwa oleh karena ilmu kedokteran bukanlah ilmu pasti, maka keberhasilan tindakan kedokteran bukanlah keniscayaan, melainkan sangat tergantung kepad izin Tuhan Yang Maha Esa.
                                            "
            ElseIf grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "PENUNDAAN PELAYANAN" Then
                txtJUDULFORMULIR.Text = grdKDITEMOPERASI.Text
                txtJUDUL1.Text = "PEMBERIAN INFORMASI"
                txtJUDUL2.Text = IIf(grdKDITEMOPERASI.Text.Contains("PENOLAKAN"), "PENOLAKAN PENUNDAAN PELAYANAN", "PERSETUJUAN PENUNDAAN PELAYANAN")
                lblOperator.Text = ""
                grdKDDOCTOR_2.Visible = False
                txtPERSETUJUAN.Text = "Yang bertandatangan di bawah ini, saya , nama ____________, umur ______ tahun, " & vbCrLf _
                                          & "laki-laki/ perempuan*, alamat " & vbCrLf &
                                          "____________________________________" _
                                           & "dengan ini menyatakan persetujuan / penolakan untuk dilakukannya tindakan" & vbCrLf &
                                           "____________________________________" _
                                           & " " & vbCrLf &
                                           "terhadap saya / ……………………………… saya* " _
                                           & " " & vbCrLf &
                                           "bernama ……………………………………..,  umur ……………… tahun, laki-laki / perempuan*,  " _
                                           & "" & vbCrLf &
                                           "Alamat____________________________________" _
                                           & " " & vbCrLf &
                                           "Saya memahami perlunya dan manfaat tindakan tersebut sebagaimana telah dijelaskan seperti di atas kepada saya, termasuk risiko dan komplikasi yang mungkin timbul. Saya juga menyadari bahwa oleh karena ilmu kedokteran bukanlah ilmu pasti, maka keberhasilan tindakan kedokteran bukanlah keniscayaan, melainkan sangat bergantung kepada izin Tuhan Yang Maha Esa.
                                            "
            ElseIf grvKDITEMOPERASI.GetFocusedRowCellValue("FORMATISI") = "TINDAKAN MEDIS" Then
                txtJUDULFORMULIR.Text = grdKDITEMOPERASI.Text
                txtJUDUL1.Text = "PEMBERIAN INFORMASI"
                txtJUDUL2.Text = IIf(grdKDITEMOPERASI.Text.Contains("PENOLAKAN"), "PENOLAKAN TINDAKAN MEDIS", "PERSETUJUAN TINDAKAN MEDIS")
                lblOperator.Text = ""
                grdKDDOCTOR_2.Visible = False
                txtPERSETUJUAN.Text = "Yang bertandatangan di bawah ini, saya , nama ____________, umur ______ tahun, " & vbCrLf _
                                          & "laki-laki/ perempuan*, alamat " & vbCrLf &
                                          "____________________________________" _
                                           & "dengan ini menyatakan persetujuan / penolakan untuk dilakukannya tindakan" & vbCrLf &
                                           "____________________________________" _
                                           & " " & vbCrLf &
                                           "terhadap saya / ……………………………… saya* " _
                                           & " " & vbCrLf &
                                           "bernama ……………………………………..,  umur ……………… tahun, laki-laki / perempuan*,  " _
                                           & "" & vbCrLf &
                                           "Alamat____________________________________" _
                                           & " " & vbCrLf &
                                           "Saya memahami perlunya dan manfaat tindakan tersebut sebagaimana telah dijelaskan seperti di atas kepada saya, termasuk risiko dan komplikasi yang mungkin timbul. Saya juga menyadari bahwa oleh karena ilmu kedokteran bukanlah ilmu pasti, maka keberhasilan tindakan kedokteran bukanlah keniscayaan, melainkan sangat bergantung kepada izin Tuhan Yang Maha Esa.
                                            "
            End If
        End If
    End Sub
#End Region
End Class