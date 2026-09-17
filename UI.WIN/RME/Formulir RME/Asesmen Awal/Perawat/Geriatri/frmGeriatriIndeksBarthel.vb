Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmGeriatriIndeksBarthel
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL As New Digital.clsDigital_IndeksBarthel
    Private down As Boolean = False
    Private sKDPENDAFTARAN As string
    Private sKDKUNJUNGAN As string
    Private sTOTALSKOR As Integer = 0
    Private sNoId As String
    Private sKDUSER As String
    Private sKDUSERSIGNATURE As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, ByVal NoId As String)
        oFormMode = FormMode

        sNoId = NoId

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtJenisKelamin.Text = dsPendaftaran.JENISKELAMIN
            sKDPENDAFTARAN = dsPendaftaran.KDPENDAFTARAN
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN

            deDate.DateTime = Now

            sKDUSER = sUserID
            sKDUSERSIGNATURE = sUserSIGNATURE
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtJenisKelamin.ResetText()
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
        fn_LoadPerawat()
        fn_LoadRiwayatDataIndeksBarthel(txtNoPasien.Text)

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

        deDate.Properties.ReadOnly = Status
        chk1_0.Properties.ReadOnly = Status
        chk1_1.Properties.ReadOnly = Status
        chk1_2.Properties.ReadOnly = Status
        chk2_0.Properties.ReadOnly = Status
        chk2_1.Properties.ReadOnly = Status
        chk2_2.Properties.ReadOnly = Status
        chk3_0.Properties.ReadOnly = Status
        chk3_1.Properties.ReadOnly = Status
        chk4_0.Properties.ReadOnly = Status
        chk4_1.Properties.ReadOnly = Status
        chk4_2.Properties.ReadOnly = Status
        chk5_0.Properties.ReadOnly = Status
        chk5_1.Properties.ReadOnly = Status
        chk5_2.Properties.ReadOnly = Status
        chk6_0.Properties.ReadOnly = Status
        chk6_1.Properties.ReadOnly = Status
        chk6_2.Properties.ReadOnly = Status
        chk6_3.Properties.ReadOnly = Status
        chk7_0.Properties.ReadOnly = Status
        chk7_1.Properties.ReadOnly = Status
        chk7_2.Properties.ReadOnly = Status
        chk7_3.Properties.ReadOnly = Status
        chk8_0.Properties.ReadOnly = Status
        chk8_1.Properties.ReadOnly = Status
        chk8_2.Properties.ReadOnly = Status
        chk9_0.Properties.ReadOnly = Status
        chk9_1.Properties.ReadOnly = Status
        chk9_2.Properties.ReadOnly = Status
        chk10_0.Properties.ReadOnly = Status
        chk10_1.Properties.ReadOnly = Status
        'txtTotalSkor.Properties.ReadOnly = Status
        grdPerawat.Properties.ReadOnly = Status
        chkEdukasi.Properties.ReadOnly = Status
        chkLaporDPJP.Properties.ReadOnly = Status
        chkKonsul.Properties.ReadOnly = Status

        chkKetSkor20.Properties.ReadOnly = Status
        chkKetSkor12_19.Properties.ReadOnly = Status
        chkKetSkor9_11.Properties.ReadOnly = Status
        chkKetSkor5_8.Properties.ReadOnly = Status
        chkKetSkor0_4.Properties.ReadOnly = Status



    End Sub
    Private Sub fn_EmptyMe()

        deDate.DateTime = Now
        chk1_0.Checked = False
        chk1_1.Checked = False
        chk1_2.Checked = False
        chk2_0.Checked = False
        chk2_1.Checked = False
        chk2_2.Checked = False
        chk3_0.Checked = False
        chk3_1.Checked = False
        chk4_0.Checked = False
        chk4_1.Checked = False
        chk4_2.Checked = False
        chk5_0.Checked = False
        chk5_1.Checked = False
        chk5_2.Checked = False
        chk6_0.Checked = False
        chk6_1.Checked = False
        chk6_2.Checked = False
        chk6_3.Checked = False
        chk7_0.Checked = False
        chk7_1.Checked = False
        chk7_2.Checked = False
        chk7_3.Checked = False
        chk8_0.Checked = False
        chk8_1.Checked = False
        chk8_2.Checked = False
        chk9_0.Checked = False
        chk9_1.Checked = False
        chk9_2.Checked = False
        chk10_0.Checked = False
        chk10_1.Checked = False
        txtTotalSkor.Text = 0
        grdPerawat.ResetText()
        chkEdukasi.Checked = False
        chkLaporDPJP.Checked = False
        chkKonsul.Checked = False

        sTOTALSKOR = 0

        chkKetSkor20.Checked = False
        chkKetSkor12_19.Checked = False
        chkKetSkor9_11.Checked = False
        chkKetSkor5_8.Checked = False
        chkKetSkor0_4.Checked = False

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.GetData(sNoid)

            With ds
                
                deDate.DateTime = .DATE
                chk1_0.Checked = .S_DIGITAL_1
                chk1_1.Checked = .S_DIGITAL_2
                chk1_2.Checked = .S_DIGITAL_3
                chk2_0.Checked = .S_DIGITAL_4
                chk2_1.Checked = .S_DIGITAL_5
                chk2_2.Checked = .S_DIGITAL_6
                chk3_0.Checked = .S_DIGITAL_7
                chk3_1.Checked = .S_DIGITAL_8
                chk4_0.Checked = .S_DIGITAL_9
                chk4_1.Checked = .S_DIGITAL_10
                chk4_2.Checked = .S_DIGITAL_11
                chk5_0.Checked = .S_DIGITAL_12
                chk5_1.Checked = .S_DIGITAL_13
                chk5_2.Checked = .S_DIGITAL_14
                chk6_0.Checked = .S_DIGITAL_15
                chk6_1.Checked = .S_DIGITAL_16
                chk6_2.Checked = .S_DIGITAL_17
                chk6_3.Checked = .S_DIGITAL_18
                chk7_0.Checked = .S_DIGITAL_19
                chk7_1.Checked = .S_DIGITAL_20
                chk7_2.Checked = .S_DIGITAL_21
                chk7_3.Checked = .S_DIGITAL_22
                chk8_0.Checked = .S_DIGITAL_23
                chk8_1.Checked = .S_DIGITAL_24
                chk8_2.Checked = .S_DIGITAL_25
                chk9_0.Checked = .S_DIGITAL_26
                chk9_1.Checked = .S_DIGITAL_27
                chk9_2.Checked = .S_DIGITAL_28
                chk10_0.Checked = .S_DIGITAL_29
                chk10_1.Checked = .S_DIGITAL_30
                txtTotalSkor.Text = .TOTAL_SKOR
                grdPerawat.EditValue = .KDPERAWAT
                chkEdukasi.Checked = .ISEDUKASI
                chkLaporDPJP.Checked = .ISLAPORDPJP
                chkKonsul.Checked = .ISKONSUL

                chkKetSkor20.Checked = .S_KET_20
                chkKetSkor12_19.Checked = .S_KET_12_19
                chkKetSkor9_11.Checked = .S_KET_9_11
                chkKetSkor5_8.Checked = .S_KET_5_8
                chkKetSkor0_4.Checked = .S_KET_0_4

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

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
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.GetStructureHeader
            With ds
                .KDINDEKS = sNoId
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDate.DateTime
                .S_DIGITAL_1 = chk1_0.Checked
                .S_DIGITAL_2 = chk1_1.Checked
                .S_DIGITAL_3 = chk1_2.Checked
                .S_DIGITAL_4 = chk2_0.Checked
                .S_DIGITAL_5 = chk2_1.Checked
                .S_DIGITAL_6 = chk2_2.Checked
                .S_DIGITAL_7 = chk3_0.Checked
                .S_DIGITAL_8 = chk3_1.Checked
                .S_DIGITAL_9 = chk4_0.Checked
                .S_DIGITAL_10 = chk4_1.Checked
                .S_DIGITAL_11 = chk4_2.Checked
                .S_DIGITAL_12 = chk5_0.Checked
                .S_DIGITAL_13 = chk5_1.Checked
                .S_DIGITAL_14 = chk5_2.Checked
                .S_DIGITAL_15 = chk6_0.Checked
                .S_DIGITAL_16 = chk6_1.Checked
                .S_DIGITAL_17 = chk6_2.Checked
                .S_DIGITAL_18 = chk6_3.Checked
                .S_DIGITAL_19 = chk7_0.Checked
                .S_DIGITAL_20 = chk7_1.Checked
                .S_DIGITAL_21 = chk7_2.Checked
                .S_DIGITAL_22 = chk7_3.Checked
                .S_DIGITAL_23 = chk8_0.Checked
                .S_DIGITAL_24 = chk8_1.Checked
                .S_DIGITAL_25 = chk8_2.Checked
                .S_DIGITAL_26 = chk9_0.Checked
                .S_DIGITAL_27 = chk9_1.Checked
                .S_DIGITAL_28 = chk9_2.Checked
                .S_DIGITAL_29 = chk10_0.Checked
                .S_DIGITAL_30 = chk10_1.Checked
                .TOTAL_SKOR = txtTotalSkor.Text
                .KDPERAWAT = grdPerawat.EditValue
                .ISEDUKASI = chkEdukasi.Checked
                .ISLAPORDPJP = chkLaporDPJP.Checked
                .ISKONSUL = chkKonsul.Checked

                .S_KET_20 = chkKetSkor20.Checked
                .S_KET_12_19 = chkKetSkor12_19.Checked
                .S_KET_9_11 = chkKetSkor9_11.Checked
                .S_KET_5_8 = chkKetSkor5_8.Checked
                .S_KET_0_4 = chkKetSkor0_4.Checked

                Try
                    .CETAK = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER
                .KDUSER_SIGNATURE = sKDUSERSIGNATURE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.UpdateData(ds)
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
    Private Sub fn_LoadPerawat()
        Try
            Dim sKoneksiOld As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksiOld
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdPerawat.Properties.DataSource = ds.Tables("STAFF")
            grdPerawat.Properties.ValueMember = "KDSTAFF"
            grdPerawat.Properties.DisplayMember = "NAME_DISPLAY"


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private sub fn_LoadRiwayatDataIndeksBarthel(ByVal parameter As String)
       Try
            'vgrdDetail.Rows.Clear()
            vgrdDetail.DataSource = Nothing

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "S_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL A "
            SQL &= "WHERE A.KDCUSTOMER = '" & parameter  & "' "
            SQL &= "ORDER BY A.DATE ASC "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "INDEKSBARTHEL")

            vgrdDetail.DataSource = ds.Tables("INDEKSBARTHEL")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End sub

    Dim skor1 As Integer = 0
    Private Sub chk1_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_0.CheckedChanged
        If chk1_0.Checked Then
            chk1_1.Checked = False
            chk1_2.Checked = False
            skor1 = 0
        Else
            skor1 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk1_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_1.CheckedChanged
        If chk1_1.Checked Then
            chk1_0.Checked = False
            chk1_2.Checked = False
            skor1 = 1
        Else
            skor1 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk1_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_2.CheckedChanged
        If chk1_2.Checked Then
            chk1_1.Checked = False
            chk1_0.Checked = False
            skor1 = 2
        Else
            skor1 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor2 As Integer = 0
    Private Sub chk2_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_0.CheckedChanged
        If chk2_0.Checked Then
            chk2_1.Checked = False
            chk2_2.Checked = False
            skor2 = 0
        Else
            skor2 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk2_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_1.CheckedChanged
        If chk2_1.Checked Then
            chk2_0.Checked = False
            chk2_2.Checked = False
            skor2 = 1
        Else
            skor2 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk2_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk2_2.CheckedChanged
        If chk2_2.Checked Then
            chk2_1.Checked = False
            chk2_0.Checked = False
            skor2 = 2
        Else
            skor2 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor3 As Integer = 0
    Private Sub chk3_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk3_0.CheckedChanged
        If chk3_0.Checked Then
            chk3_1.Checked = False
            skor3 = 0
        Else
            skor3 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk3_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk3_1.CheckedChanged
        If chk3_1.Checked Then
            chk3_0.Checked = False
            skor3 = 1
        Else
            skor3 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor4 As Integer = 0
    Private Sub chk4_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk4_0.CheckedChanged
        If chk4_0.Checked Then
            chk4_1.Checked = False
            chk4_2.Checked = False
            skor4 = 0
        Else
            skor4 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk4_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk4_1.CheckedChanged
        If chk4_1.Checked Then
            chk4_0.Checked = False
            chk4_2.Checked = False
            skor4 = 1
        Else
            skor4 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk4_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk4_2.CheckedChanged
        If chk4_2.Checked Then
            chk4_1.Checked = False
            chk4_0.Checked = False
            skor4 = 2
        Else
            skor4 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor5 As Integer = 0
    Private Sub chk5_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk5_0.CheckedChanged
        If chk5_0.Checked Then
            chk5_1.Checked = False
            chk5_2.Checked = False
            skor5 = 0
        Else
            skor5 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk5_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk5_1.CheckedChanged
        If chk5_1.Checked Then
            chk5_0.Checked = False
            chk5_2.Checked = False
            skor5 = 1
        Else
            skor5 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk5_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk5_2.CheckedChanged
        If chk5_2.Checked Then
            chk5_1.Checked = False
            chk5_0.Checked = False
            skor5 = 2
        Else
            skor5 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor6 As Integer = 0
    Private Sub chk6_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_0.CheckedChanged
        If chk6_0.Checked Then
            chk6_1.Checked = False
            chk6_2.Checked = False
            chk6_3.Checked = False
            skor6 = 0
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk6_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_1.CheckedChanged
        If chk6_1.Checked Then
            chk6_0.Checked = False
            chk6_2.Checked = False
            chk6_3.Checked = False
            skor6 = 1
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk6_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_2.CheckedChanged
        If chk6_2.Checked Then
            chk6_1.Checked = False
            chk6_0.Checked = False
            chk6_3.Checked = False
            skor6 = 2
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk6_3_CheckedChanged(sender As Object, e As EventArgs) Handles chk6_3.CheckedChanged
        If chk6_3.Checked Then
            chk6_1.Checked = False
            chk6_0.Checked = False
            chk6_2.Checked = False
            skor6 = 3
        Else
            skor6 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor7 As Integer = 0
    Private Sub chk7_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_0.CheckedChanged
        If chk7_0.Checked Then
            chk7_1.Checked = False
            chk7_2.Checked = False
            chk7_3.Checked = False
            skor7 = 0
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk7_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_1.CheckedChanged
        If chk7_1.Checked Then
            chk7_0.Checked = False
            chk7_2.Checked = False
            chk7_3.Checked = False
            skor7 = 1
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk7_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_2.CheckedChanged
        If chk7_2.Checked Then
            chk7_1.Checked = False
            chk7_0.Checked = False
            chk7_3.Checked = False
            skor7 = 2
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk7_3_CheckedChanged(sender As Object, e As EventArgs) Handles chk7_3.CheckedChanged
        If chk7_3.Checked Then
            chk7_1.Checked = False
            chk7_0.Checked = False
            chk7_2.Checked = False
            skor7 = 3
        Else
            skor7 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor8 As Integer = 0
    Private Sub chk8_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk8_0.CheckedChanged
        If chk8_0.Checked Then
            chk8_1.Checked = False
            chk8_2.Checked = False
            skor8 = 0
        Else
            skor8 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk8_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk8_1.CheckedChanged
        If chk8_1.Checked Then
            chk8_0.Checked = False
            chk8_2.Checked = False
            skor8 = 1
        Else
            skor8 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk8_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk8_2.CheckedChanged
        If chk8_2.Checked Then
            chk8_1.Checked = False
            chk8_0.Checked = False
            skor8 = 2
        Else
            skor8 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor9 As Integer = 0
    Private Sub chk9_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk9_0.CheckedChanged
        If chk9_0.Checked Then
            chk9_1.Checked = False
            chk9_2.Checked = False
            skor9 = 0
        Else
            skor9 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk9_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk9_1.CheckedChanged
        If chk9_1.Checked Then
            chk9_0.Checked = False
            chk9_2.Checked = False
            skor9 = 1
        Else
            skor9 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk9_2_CheckedChanged(sender As Object, e As EventArgs) Handles chk9_2.CheckedChanged
        If chk9_2.Checked Then
            chk9_1.Checked = False
            chk9_0.Checked = False
            skor9 = 2
        Else
            skor9 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Dim skor10 As Integer = 0
    Private Sub chk10_0_CheckedChanged(sender As Object, e As EventArgs) Handles chk10_0.CheckedChanged
        If chk10_0.Checked Then
            chk10_1.Checked = False
            skor10 = 0
        Else
            skor10 = 0
        End If
        fn_JumlahSkor()
    End Sub
    Private Sub chk10_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk10_1.CheckedChanged
        If chk10_1.Checked Then
            chk10_0.Checked = False
            skor10 = 1
        Else
            skor10 = 0
        End If
        fn_JumlahSkor()
    End Sub

    Private sub fn_JumlahSkor()
        Dim iTotal = skor1+skor2+skor3+skor4+skor5+skor6+skor7+skor8+skor9+skor10

        txtTotalSkor.Text = iTotal

        'Keterangan
        If iTotal = 20 Then
            chkKetSkor20.Checked = True
            chkKetSkor12_19.Checked = False
            chkKetSkor9_11.Checked = False
            chkKetSkor5_8.Checked = False
            chkKetSkor0_4.Checked = False
        ElseIf iTotal >= 12 And iTotal <= 19 Then
            chkKetSkor20.Checked = False
            chkKetSkor12_19.Checked = True
            chkKetSkor9_11.Checked = False
            chkKetSkor5_8.Checked = False
            chkKetSkor0_4.Checked = False
        ElseIf iTotal >= 9 And iTotal <= 11 Then
            chkKetSkor20.Checked = False
            chkKetSkor12_19.Checked = False
            chkKetSkor9_11.Checked = True
            chkKetSkor5_8.Checked = False
            chkKetSkor0_4.Checked = False
        ElseIf iTotal >= 5 And iTotal <= 8
            chkKetSkor20.Checked = False
            chkKetSkor12_19.Checked = False
            chkKetSkor9_11.Checked = False
            chkKetSkor5_8.Checked = True
            chkKetSkor0_4.Checked = False
        ElseIf iTotal >= 0 And iTotal <=4 Then
            chkKetSkor20.Checked = False
            chkKetSkor12_19.Checked = False
            chkKetSkor9_11.Checked = False
            chkKetSkor5_8.Checked = False
            chkKetSkor0_4.Checked = True
        End If


    End sub

    Private Sub CopyInstrumenStatusFungsionalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyInstrumenStatusFungsionalToolStripMenuItem.Click
        If vgrdDetail.GetCellValue(rowKDINDEKS, vgrdDetail.FocusedRecord).ToString() Is Nothing Then
            Exit Sub
        End If
        Dim sKDINDEKS As String = vgrdDetail.GetCellValue(rowKDINDEKS, vgrdDetail.FocusedRecord).ToString()

        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_INDEKSBARTHEL.GetData(sKDINDEKS)

            With ds
                
                deDate.DateTime = .DATE
                chk1_0.Checked = .S_DIGITAL_1
                chk1_1.Checked = .S_DIGITAL_2
                chk1_2.Checked = .S_DIGITAL_3
                chk2_0.Checked = .S_DIGITAL_4
                chk2_1.Checked = .S_DIGITAL_5
                chk2_2.Checked = .S_DIGITAL_6
                chk3_0.Checked = .S_DIGITAL_7
                chk3_1.Checked = .S_DIGITAL_8
                chk4_0.Checked = .S_DIGITAL_9
                chk4_1.Checked = .S_DIGITAL_10
                chk4_2.Checked = .S_DIGITAL_11
                chk5_0.Checked = .S_DIGITAL_12
                chk5_1.Checked = .S_DIGITAL_13
                chk5_2.Checked = .S_DIGITAL_14
                chk6_0.Checked = .S_DIGITAL_15
                chk6_1.Checked = .S_DIGITAL_16
                chk6_2.Checked = .S_DIGITAL_17
                chk6_3.Checked = .S_DIGITAL_18
                chk7_0.Checked = .S_DIGITAL_19
                chk7_1.Checked = .S_DIGITAL_20
                chk7_2.Checked = .S_DIGITAL_21
                chk7_3.Checked = .S_DIGITAL_22
                chk8_0.Checked = .S_DIGITAL_23
                chk8_1.Checked = .S_DIGITAL_24
                chk8_2.Checked = .S_DIGITAL_25
                chk9_0.Checked = .S_DIGITAL_26
                chk9_1.Checked = .S_DIGITAL_27
                chk9_2.Checked = .S_DIGITAL_28
                chk10_0.Checked = .S_DIGITAL_29
                chk10_1.Checked = .S_DIGITAL_30
                txtTotalSkor.Text = .TOTAL_SKOR
                grdPerawat.EditValue = .KDPERAWAT
                chkEdukasi.Checked = .ISEDUKASI
                chkLaporDPJP.Checked = .ISLAPORDPJP
                chkKonsul.Checked = .ISKONSUL

                chkKetSkor20.Checked = .S_KET_20
                chkKetSkor12_19.Checked = .S_KET_12_19
                chkKetSkor9_11.Checked = .S_KET_9_11
                chkKetSkor5_8.Checked = .S_KET_5_8
                chkKetSkor0_4.Checked = .S_KET_0_4

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE

            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmGeriatriIndeksBarthel_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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