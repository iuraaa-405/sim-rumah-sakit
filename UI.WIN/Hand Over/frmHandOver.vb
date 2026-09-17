Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmHandOver
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private sKDKUNJUNGAN As String = String.Empty
    Private oS_DIGITAL_HANDOVER As New Digital.clsS_DIGITAL_HANDOVER
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        txtNoRegister.Text = KDKUNJUNGAN

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA.ToString.Trim.ToUpper
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtTujuan.Text = dsPendaftaran.TUJUAN
        Else
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtNoPasien.ResetText()
            txtNoRegister.ResetText()
            txtTanggalDaftar.ResetText()
            txtTujuan.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_LoadDoctorDPJP()
        fn_LoadDiagnosa()
        fn_LoadKDSTAFF()

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

        cboKATEGORI.Properties.ReadOnly = Status
        grdDIAGNOSA.Properties.ReadOnly = Status
        grdDPJP.Properties.ReadOnly = Status
        txtMASALAHKEPERAWATAN.Properties.ReadOnly = Status
        txtRIWAYATALERGI.Properties.ReadOnly = Status
        txtRIWAYATREAKSI.Properties.ReadOnly = Status
        cboKESADARAN.Properties.ReadOnly = Status
        txtGCS.Properties.ReadOnly = Status
        cboE.Properties.ReadOnly = Status
        cboM.Properties.ReadOnly = Status
        cboV.Properties.ReadOnly = Status
        txtTD.Properties.ReadOnly = Status
        txtHR.Properties.ReadOnly = Status
        txtSUHU.Properties.ReadOnly = Status
        txtRR.Properties.ReadOnly = Status
        txtSPO2.Properties.ReadOnly = Status
        txtSKALANYERI.Properties.ReadOnly = Status
        txtOKSIGEN.Properties.ReadOnly = Status
        txtINFUS.Properties.ReadOnly = Status
        txtTRANSFUSI.Properties.ReadOnly = Status
        chkBAKNORMAL.Properties.ReadOnly = Status
        chkKATETER.Properties.ReadOnly = Status
        txtURINE.Properties.ReadOnly = Status
        chkBABNORMAL.Properties.ReadOnly = Status
        chkCOLOSTOMY.Properties.ReadOnly = Status
        chkORAL.Properties.ReadOnly = Status
        chkNGT.Properties.ReadOnly = Status
        chkMOBILISASI1.Properties.ReadOnly = Status
        chkMOBILISASI2.Properties.ReadOnly = Status
        chkMOBILISASI3.Properties.ReadOnly = Status
        txtSCORE.Properties.ReadOnly = Status
        txtKONSULTASI.Properties.ReadOnly = Status
        txtTHERAPY.Properties.ReadOnly = Status
        txtRENCANA1.Properties.ReadOnly = Status
        txtRENCANA2.Properties.ReadOnly = Status
        txtPEMBERIOPERAN.Properties.ReadOnly = Status
        txtPENERIMAOPERAN.Properties.ReadOnly = Status
        deDATEFrom.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                sIsOtority = True
            Else
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        'deDATE.DateTime = Now

        cboKATEGORI.SelectedIndex=0
        'grdDIAGNOSA.ResetText()
        'grdDPJP.ResetText()
        txtMASALAHKEPERAWATAN.ResetText()
        txtRIWAYATALERGI.ResetText()
        txtRIWAYATREAKSI.ResetText()
        cboKESADARAN.ResetText()
        txtGCS.Text = 0
        cboE.SelectedIndex=0
        cboM.SelectedIndex=0
        cboV.SelectedIndex=0
        txtTD.ResetText()
        txtHR.ResetText()
        txtSUHU.ResetText()
        txtRR.ResetText()
        txtSPO2.ResetText()
        txtSKALANYERI.ResetText()
        txtOKSIGEN.ResetText()
        txtTRANSFUSI.ResetText()
        txtINFUS.ResetText()
        chkBAKNORMAL.Checked=False
        chkKATETER.Checked=False
        txtURINE.ResetText()
        chkBABNORMAL.Checked=False
        chkCOLOSTOMY.Checked=False
        chkORAL.Checked=False
        chkNGT.Checked=False
        chkMOBILISASI1.Checked=False
        chkMOBILISASI2.Checked=False
        chkMOBILISASI3.Checked=False
        txtSCORE.ResetText()
        txtKONSULTASI.ResetText()
        txtTHERAPY.ResetText()
        txtRENCANA1.ResetText()
        txtRENCANA2.ResetText()
        'txtPEMBERIOPERAN.ResetText()
        'txtPENERIMAOPERAN.ResetText()
        deDATEFrom.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_HANDOVER.GetData(sNoId)

            With ds
                deDATEFrom.DateTime = .DATE
                cboKATEGORI.Text = .KATEGORI
                grdDIAGNOSA.EditValue = .KDDIAGNOSA
                grdDPJP.EditValue = .KDDOCTOR
                txtMASALAHKEPERAWATAN.Text = .MASALAHKEPERAWATAN
                txtRIWAYATALERGI.Text = .RIWAYATALERGI
                txtRIWAYATREAKSI.Text = .txtRIWAYATREAKSI
                cboKESADARAN.Text = .KESADARAN
                txtGCS.Text = .GCS
                cboE.Text = .E
                cboM.Text = .M
                cboV.Text = .V
                txtTD.Text = .TD
                txtHR.Text = .HR
                txtSUHU.Text = .SUHU
                txtRR.Text = .RR
                txtSPO2.Text = .SPO2
                txtSKALANYERI.Text = .SKALANYERI
                txtOKSIGEN.Text = .OKSIGEN
                txtTRANSFUSI.Text = .TRANSFUSI
                txtINFUS.Text = .INFUS
                chkBAKNORMAL.Checked = .BAKNORMAL
                chkKATETER.Checked = .KATETER
                txtURINE.Text = .URINE
                chkBABNORMAL.Checked = .BABNORMAL
                chkCOLOSTOMY.Checked = .COLOSTOMY
                chkORAL.Checked = .ORAL
                chkNGT.Checked = .NGT
                chkMOBILISASI1.Checked = .MOBILISASI1
                chkMOBILISASI2.Checked = .MOBILISASI2
                chkMOBILISASI3.Checked = .MOBILISASI3
                txtSCORE.Text = .SCORE
                txtKONSULTASI.Text = .KONSULTASI
                txtTHERAPY.Text = .THERAPY
                txtRENCANA1.Text = .RENCANA1
                txtRENCANA2.Text = .RENCANA2
                txtPEMBERIOPERAN.EditValue = .KDPEMBERIOPERAN
                txtPENERIMAOPERAN.EditValue = .KDPENERIMAOPERAN

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
            Dim ds = oS_DIGITAL_HANDOVER.GetStructureHeader
            With ds
                .KDHO = sNoId
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATEFrom.DateTime
                .KATEGORI = cboKATEGORI.Text
                .KDDIAGNOSA = grdDIAGNOSA.EditValue
                .DIAGNOSA = grdDIAGNOSA.Text
                .KDDOCTOR = grdDPJP.EditValue
                .DOKTER = grdDPJP.Text
                .MASALAHKEPERAWATAN = txtMASALAHKEPERAWATAN.Text
                .RIWAYATALERGI = txtRIWAYATALERGI.Text
                .txtRIWAYATREAKSI = txtRIWAYATREAKSI.Text
                .KESADARAN = cboKESADARAN.SelectedText
                .GCS = txtGCS.Text
                .E = cboE.Text
                .M = cboM.Text
                .V = cboV.Text
                .TD = txtTD.Text
                .HR = txtHR.Text
                .SUHU = txtSUHU.Text
                .RR = txtRR.Text
                .SPO2 = txtSPO2.Text
                .SKALANYERI = txtSKALANYERI.Text
                .OKSIGEN = txtOKSIGEN.Text
                .TRANSFUSI = txtTRANSFUSI.Text
                .INFUS = txtINFUS.Text
                .BAKNORMAL = chkBAKNORMAL.Checked
                .KATETER = chkKATETER.Checked
                .URINE = txtURINE.Text
                .BABNORMAL = chkBABNORMAL.Checked
                .COLOSTOMY = chkCOLOSTOMY.Checked
                .ORAL = chkORAL.Checked
                .NGT = chkNGT.Checked
                .MOBILISASI1 = chkMOBILISASI1.Checked
                .MOBILISASI2 = chkMOBILISASI2.Checked
                .MOBILISASI3 = chkMOBILISASI3.Checked
                .SCORE = txtSCORE.Text
                .KONSULTASI = txtKONSULTASI.Text
                .THERAPY = txtTHERAPY.Text
                .RENCANA1 = txtRENCANA1.Text
                .RENCANA2 = txtRENCANA2.Text
                .KDPEMBERIOPERAN = txtPEMBERIOPERAN.EditValue
                .PEMBERIOPERAN = txtPEMBERIOPERAN.Text
                .KDPENERIMAOPERAN = txtPENERIMAOPERAN.EditValue
                .PENERIMAOPERAN = txtPENERIMAOPERAN.Text

                Try
                    .CETAK = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_HANDOVER.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
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
                    fn_Save = oS_DIGITAL_HANDOVER.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_HANDOVER.UpdateData(ds)
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
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub fn_LoadDiagnosa()
        Try
            Dim sKoneksi As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksi = dsSetKoneksi.KONEKSI
            End If

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
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            grdDIAGNOSA.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdDIAGNOSA.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox("Load No Hubungan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadDoctorDPJP()
        Try
            Dim sKoneksi As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksi = dsSetKoneksi.KONEKSI
            End If

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
            'SQL &= "AND A.KDDEPARTMENT = '" & sKDDEPARTMENT_POLI & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTERDPJP")

            grdDPJP.Properties.DataSource = ds.Tables("DOKTERDPJP")
            grdDPJP.Properties.ValueMember = "KDDOCTOR"
            grdDPJP.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub cboE_EditValueChanged(sender As Object, e As EventArgs) Handles cboE.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub cboM_EditValueChanged(sender As Object, e As EventArgs) Handles cboM.EditValueChanged
        fn_GCSValue()
    End Sub

    Private Sub cboV_EditValueChanged(sender As Object, e As EventArgs) Handles cboV.EditValueChanged
        fn_GCSValue()
    End Sub
    Private Sub fn_GCSValue()
        Try
            Dim val As Integer
            val = CInt(cboE.EditValue) + CInt(cboM.EditValue) + CInt(cboV.EditValue)
            txtGCS.Text = val.ToString()
        Catch ex As Exception
            MsgBox("Load Sub Spesialis Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, "Hand Over Shift")
        End Try

    End Sub

    Private Sub chkBAKNORMAL_CheckedChanged(sender As Object, e As EventArgs) Handles chkBAKNORMAL.CheckedChanged
        If chkBAKNORMAL.Checked=False Then
            chkKATETER.Checked = True
        Else
            chkKATETER.Checked = False
        End If
    End Sub


    Private Sub fn_LoadKDSTAFF()
        Try
            Dim sKoneksi As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksi = dsSetKoneksi.GENERATE_ECLAIM
            End If

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
            SQL &= "A.KDSTAFF "
            SQL &= ",A.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            txtPEMBERIOPERAN.Properties.DataSource = ds.Tables("STAFF")
            txtPEMBERIOPERAN.Properties.ValueMember = "KDSTAFF"
            txtPEMBERIOPERAN.Properties.DisplayMember = "NAME_DISPLAY"

            txtPENERIMAOPERAN.Properties.DataSource = ds.Tables("STAFF")
            txtPENERIMAOPERAN.Properties.ValueMember = "KDSTAFF"
            txtPENERIMAOPERAN.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmHandOver_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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