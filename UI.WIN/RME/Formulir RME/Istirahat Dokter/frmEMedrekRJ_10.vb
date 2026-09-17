Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmEMedrekRJ_10
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_10 As New Digital.clsIstirahatDokter
    Private sKoneksi As String = String.Empty
    Private sUrut As Integer = 0

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, Optional ByVal SEQ As Integer = 0)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG
        sUrut = SEQ
        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
            txtKESATUAN.Text = dsPendaftaran.KESATUAN
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()
            grdDOCTOR.ResetText()
            txtKESATUAN.ResetText()
        End If

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = IstirhataDokter.TITLE
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
        fn_LoadDokter()

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

        deDATEFrom.Properties.ReadOnly = Status
        deDATETo.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now
        TextEdit3.ResetText()
        Dim oSetUser As New Setting.clsUser
        Dim dsSetUser = oSetUser.GetData(sUserID)
        If dsSetUser IsNot Nothing Then
            grdDOCTOR.Text = dsSetUser.KDDOCTOR
        Else
            grdDOCTOR.ResetText()
        End If

        txtREMARKS.Text = "-"

        'txtNOMOR.Text = "No. " & oS_DIGITAL_RJ_10.GetDataUserCount(sUserID) + 1 & " / " & Now.ToString("MM") & " / " & Now.ToString("yyyy")
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_10.GetDataBySEQ(txtNoRegister.Text, sUrut)

            With ds
                deDATETo.DateTime = .SAMPAITANGGAL
                deDATEFrom.DateTime = .MULAITANGGAL
                TextEdit3.Text = .SELAMA
                grdDOCTOR.Text = .DOCTOR_KODE
                chk_1.Checked = .PILIH_1
                chk_2.Checked = .PILIH_2
                chk_3.Checked = .PILIH_3
                chk_4.Checked = .PILIH_4
                txtREMARKS.Text = .REMARKS

                chk_5.Checked = .PILIHAN_5
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
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
            Dim ds = oS_DIGITAL_RJ_10.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_10.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = Now
                .SAMPAITANGGAL = deDATETo.DateTime
                .MULAITANGGAL = deDATEFrom.DateTime
                .SELAMA = TextEdit3.Text
                .PILIH_1 = chk_1.Checked
                .PILIH_2 = chk_2.Checked
                .PILIH_3 = chk_3.Checked
                .PILIH_4 = chk_4.Checked
                .PILIHAN_5 = chk_5.Checked
                Try
                    .CETAK = oS_DIGITAL_RJ_10.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
                .DOCTOR_KODE = grdDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdDOCTOR.Text
                Try
                    .NOMOR = oS_DIGITAL_RJ_10.GetData(txtNoRegister.Text).NOMOR
                Catch ex As Exception
                    .NOMOR = ""
                End Try
                .REMARKS = txtREMARKS.Text

                If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                    Try
                        .SEQ = oS_DIGITAL_RJ_10.GetDataListByKunjungan(txtNoRegister.Text)
                    Catch ex As Exception
                        .SEQ = 0
                    End Try
                Else
                    .SEQ = sUrut
                End If


            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_10.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_10.UpdateData(ds)
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
        End Select
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
    Private Sub fn_LoadDokter()
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
            da.Fill(ds, "DOCTOR")

            grdDOCTOR.Properties.DataSource = ds.Tables("DOCTOR")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnHitung_Click(sender As Object, e As EventArgs) Handles btnHitung.Click
        If deDATETo.DateTime.ToString("yyyyMMdd") < deDATEFrom.DateTime.ToString("yyyyMMdd") Then
            MsgBox("Tanggal Sampai harus lebih besar dari Tanggal Dari!", MsgBoxStyle.OkOnly, Me.Text)
        Else
            TextEdit3.Text = DateDiff(DateInterval.Day, deDATEFrom.DateTime, deDATETo.DateTime) + 1 & " Hari"
        End If
    End Sub
#End Region
End Class