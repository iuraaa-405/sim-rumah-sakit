Imports System.Linq
Imports DataAccess
Imports System.Data.SqlClient

Public Class frmEMedrekRI_13
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_13 As New Digital.clsS_DIGITAL_RI_13
    Private down As Boolean = False
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

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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
        fn_NOIDUSER()
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

        deDATE.Properties.ReadOnly = Status
        txtJAM.Properties.ReadOnly = Status
        grdDOCTOR2.Properties.ReadOnly = Status
        txtANAMNESIS_01.Properties.ReadOnly = Status
        txtANAMNESIS_02.Properties.ReadOnly = Status
        txtANAMNESIS_03.Properties.ReadOnly = Status
        txtANAMNESIS_04.Properties.ReadOnly = Status
        txtANAMNESIS_05.Properties.ReadOnly = Status
        txtANAMNESIS_06.Properties.ReadOnly = Status
        txtANAMNESIS_07.Properties.ReadOnly = Status
        txtANAMNESIS_08.Properties.ReadOnly = Status
        txtANAMNESIS_09.Properties.ReadOnly = Status
        txtANAMNESIS_10.Properties.ReadOnly = Status
        txtANAMNESIS_11.Properties.ReadOnly = Status
        txtANAMNESIS_12.Properties.ReadOnly = Status
        txtANAMNESIS_13.Properties.ReadOnly = Status
        txtANAMNESIS_14.Properties.ReadOnly = Status
        txtANAMNESIS_15.Properties.ReadOnly = Status
        txtANAMNESIS_16.Properties.ReadOnly = Status
        txtANAMNESIS_17.Properties.ReadOnly = Status
        txtANAMNESIS_18.Properties.ReadOnly = Status
        txtANAMNESIS_19.Properties.ReadOnly = Status
        txtANAMNESIS_20.Properties.ReadOnly = Status
        txtANAMNESIS_21.Properties.ReadOnly = Status
        txtANAMNESIS_22.Properties.ReadOnly = Status
        txtANAMNESIS_23.Properties.ReadOnly = Status
        txtANAMNESIS_24.Properties.ReadOnly = Status
        txtANAMNESIS_25.Properties.ReadOnly = Status
        txtANAMNESIS_26.Properties.ReadOnly = Status
        txtANAMNESIS_27.Properties.ReadOnly = Status
        txtANAMNESIS_28.Properties.ReadOnly = Status
        txtANAMNESIS_29.Properties.ReadOnly = Status
        txtANAMNESIS_30.Properties.ReadOnly = Status
        txtANAMNESIS_31.Properties.ReadOnly = Status
        txtANAMNESIS_32.Properties.ReadOnly = Status
        txtANAMNESIS_33.Properties.ReadOnly = Status
        txtANAMNESIS_34.Properties.ReadOnly = Status
        txtANAMNESIS_35.Properties.ReadOnly = Status
        txtANAMNESIS_36.Properties.ReadOnly = Status
        txtANAMNESIS_37.Properties.ReadOnly = Status
        txtANAMNESIS_38.Properties.ReadOnly = Status



    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        deDATE.DateTime = Now
        txtJAM.Text = Now.ToString("HH:mm")
        grdDOCTOR2.ResetText()
        txtANAMNESIS_01.ResetText()
        txtANAMNESIS_02.ResetText()
        txtANAMNESIS_03.ResetText()
        txtANAMNESIS_04.ResetText()
        txtANAMNESIS_05.ResetText()
        txtANAMNESIS_06.ResetText()
        txtANAMNESIS_07.ResetText()
        txtANAMNESIS_08.ResetText()
        txtANAMNESIS_09.ResetText()
        txtANAMNESIS_10.ResetText()
        txtANAMNESIS_11.ResetText()
        txtANAMNESIS_12.ResetText()
        txtANAMNESIS_13.ResetText()
        txtANAMNESIS_14.ResetText()
        txtANAMNESIS_15.ResetText()
        txtANAMNESIS_16.ResetText()
        txtANAMNESIS_17.ResetText()
        txtANAMNESIS_18.ResetText()
        txtANAMNESIS_19.ResetText()
        txtANAMNESIS_20.ResetText()
        txtANAMNESIS_21.ResetText()
        txtANAMNESIS_22.ResetText()
        txtANAMNESIS_23.ResetText()
        txtANAMNESIS_24.ResetText()
        txtANAMNESIS_25.ResetText()
        txtANAMNESIS_26.ResetText()
        txtANAMNESIS_27.ResetText()
        txtANAMNESIS_28.ResetText()
        txtANAMNESIS_29.ResetText()
        txtANAMNESIS_30.ResetText()
        txtANAMNESIS_31.ResetText()
        txtANAMNESIS_32.ResetText()
        txtANAMNESIS_33.ResetText()
        txtANAMNESIS_34.ResetText()
        txtANAMNESIS_35.ResetText()
        txtANAMNESIS_36.ResetText()
        txtANAMNESIS_37.ResetText()
        txtANAMNESIS_38.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text)

            With ds
                deDATE.DateTime = .DATE
                txtJAM.Text = .JAM
                grdDOCTOR2.Text = .DOCTOR_KODE
                txtANAMNESIS_01.Text = .ANAMNESIS_01
                txtANAMNESIS_02.Text = .ANAMNESIS_02
                txtANAMNESIS_03.Text = .ANAMNESIS_03
                txtANAMNESIS_04.Text = .ANAMNESIS_04
                txtANAMNESIS_05.Text = .ANAMNESIS_05
                txtANAMNESIS_06.Text = .ANAMNESIS_06
                txtANAMNESIS_07.Text = .ANAMNESIS_07
                txtANAMNESIS_08.Text = .ANAMNESIS_08
                txtANAMNESIS_09.Text = .ANAMNESIS_09
                txtANAMNESIS_10.Text = .ANAMNESIS_10
                txtANAMNESIS_11.Text = .ANAMNESIS_11
                txtANAMNESIS_12.Text = .ANAMNESIS_12
                txtANAMNESIS_13.Text = .ANAMNESIS_13
                txtANAMNESIS_14.Text = .ANAMNESIS_14
                txtANAMNESIS_15.Text = .ANAMNESIS_15
                txtANAMNESIS_16.Text = .ANAMNESIS_16
                txtANAMNESIS_17.Text = .ANAMNESIS_17
                txtANAMNESIS_18.Text = .ANAMNESIS_18
                txtANAMNESIS_19.Text = .ANAMNESIS_19
                txtANAMNESIS_20.Text = .ANAMNESIS_20
                txtANAMNESIS_21.Text = .ANAMNESIS_21
                txtANAMNESIS_22.Text = .ANAMNESIS_22
                txtANAMNESIS_23.Text = .ANAMNESIS_23
                txtANAMNESIS_24.Text = .ANAMNESIS_24
                txtANAMNESIS_25.Text = .ANAMNESIS_25
                txtANAMNESIS_26.Text = .ANAMNESIS_26
                txtANAMNESIS_27.Text = .ANAMNESIS_27
                txtANAMNESIS_28.Text = .ANAMNESIS_28
                txtANAMNESIS_29.Text = .ANAMNESIS_29
                txtANAMNESIS_30.Text = .ANAMNESIS_30
                txtANAMNESIS_31.Text = .ANAMNESIS_31
                txtANAMNESIS_32.Text = .ANAMNESIS_32
                txtANAMNESIS_33.Text = .ANAMNESIS_33
                txtANAMNESIS_34.Text = .ANAMNESIS_34
                txtANAMNESIS_35.Text = .ANAMNESIS_35
                txtANAMNESIS_36.Text = .ANAMNESIS_36
                txtANAMNESIS_37.Text = .ANAMNESIS_37
                txtANAMNESIS_38.Text = .ANAMNESIS_38

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
            Dim ds = oS_DIGITAL_RI_13.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime
                .JAM = txtJAM.Text
                .DOCTOR_KODE = grdDOCTOR2.EditValue
                .DOCTOR_NAME_DISPLAY = grdDOCTOR2.Text
                .ANAMNESIS_01 = txtANAMNESIS_01.Text
                .ANAMNESIS_02 = txtANAMNESIS_02.Text
                .ANAMNESIS_03 = txtANAMNESIS_03.Text
                .ANAMNESIS_04 = txtANAMNESIS_04.Text
                .ANAMNESIS_05 = txtANAMNESIS_05.Text
                .ANAMNESIS_06 = txtANAMNESIS_06.Text
                .ANAMNESIS_07 = txtANAMNESIS_07.Text
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
                .ANAMNESIS_21 = txtANAMNESIS_21.Text
                .ANAMNESIS_22 = txtANAMNESIS_22.Text
                .ANAMNESIS_23 = txtANAMNESIS_23.Text
                .ANAMNESIS_24 = txtANAMNESIS_24.Text
                .ANAMNESIS_25 = txtANAMNESIS_25.Text
                .ANAMNESIS_26 = txtANAMNESIS_26.Text
                .ANAMNESIS_27 = txtANAMNESIS_27.Text
                .ANAMNESIS_28 = txtANAMNESIS_28.Text
                .ANAMNESIS_29 = txtANAMNESIS_29.Text
                .ANAMNESIS_30 = txtANAMNESIS_30.Text
                .ANAMNESIS_31 = txtANAMNESIS_31.Text
                .ANAMNESIS_32 = txtANAMNESIS_32.Text
                .ANAMNESIS_33 = txtANAMNESIS_33.Text
                .ANAMNESIS_34 = txtANAMNESIS_34.Text
                .ANAMNESIS_35 = txtANAMNESIS_35.Text
                .ANAMNESIS_36 = txtANAMNESIS_36.Text
                .ANAMNESIS_37 = txtANAMNESIS_37.Text
                .ANAMNESIS_38 = txtANAMNESIS_38.Text

                Try
                    .CETAK = oS_DIGITAL_RI_13.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
                
                .HARI = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_13.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_13.UpdateData(ds)
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
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
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
    Private Sub fn_Doctor()
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

                grdDOCTOR2.Properties.DataSource = ds.Tables("DOKTER")
                grdDOCTOR2.Properties.ValueMember = "KDDOCTOR"
                grdDOCTOR2.Properties.DisplayMember = "NAME_DISPLAY"
            Catch oErr As Exception
                MsgBox("Load Doctor Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
    End Sub

    Private Sub frmEMedrekRI_13_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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