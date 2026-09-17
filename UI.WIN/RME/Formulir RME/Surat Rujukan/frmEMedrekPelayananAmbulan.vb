Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekPelayananAmbulan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_AMBULAN As New Digital.clsS_DIGITAL_AMBULAN
    Private sNoid As String = ""
    Private sKoneksi As String = String.Empty
    Private sDOCTOR As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private oPendaftaran As New Identitas.clsIdentitasPasien

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String="")
        oFormMode = FormMode
        sNoid = NoId

        
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)


        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtNOBPJS.Text = dsPendaftaran.KARTUBPJS
            txtUmur.Text = dsPendaftaran.USIA
            txtNoRegister.Text = dsPendaftaran.KDPENDAFTARAN
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtDokter.Text = dsPendaftaran.DOKTER
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtNOBPJS.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtTanggalDaftar.ResetText()
            txtDokter.ResetText()
            grdDOCTOR.ResetText()
        End If

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
        Dim oSetUser As New Setting.clsUser
        Dim dsSetUser = oSetUser.GetData(sUserID)
        If dsSetUser IsNot Nothing Then
            sDOCTOR = dsSetUser.KDDOCTOR
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
        fn_Doctor()
        fn_LoadKDSTAFF()
        fn_LoadDriver()
        fn_LoadAmbulance()

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

        grdDOCTOR.Properties.ReadOnly = Status
        txtDiagnosa.Properties.ReadOnly = Status
        txtTerapi.Properties.ReadOnly = Status
        txtIndikasiRujukan.Properties.ReadOnly = Status
        grdPerawat.Properties.ReadOnly = Status
        txtPendamping.Properties.ReadOnly = Status
        grdDriver.Properties.ReadOnly = Status
        txtHariTanggal.Properties.ReadOnly = Status
        grdAmbulance.Properties.ReadOnly = Status


    End Sub
    Private Sub fn_EmptyMe()
        grdDOCTOR.Text = sDOCTOR
        txtDiagnosa.ResetText()
        txtTerapi.ResetText()
        txtIndikasiRujukan.ResetText()
        grdPerawat.ResetText()
        txtPendamping.ResetText()
        grdDriver.ResetText()
        txtHariTanggal.Text = "Selasa, " & oS_DIGITAL_AMBULAN.GetDataDateTimeServer().ToString("dd-MM-yyyy")
        grdAmbulance.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_AMBULAN.GetData(sNoid)

            With ds
                grdDOCTOR.EditValue = .KDDOKTER

                txtDiagnosa.Text = .DIAGNOSA
                txtTerapi.Text = .TERAPI
                txtIndikasiRujukan.Text = .INDIKASI
                grdPerawat.EditValue = .KDPERAWAT
                txtPendamping.Text = .PENDAMPING
                grdDriver.EditValue = .KDDRIVER
                txtHariTanggal.Text = .HARITANGGAL
                grdAmbulance.EditValue = .KODEAMBULAN


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
            Dim dsPendaftaran = oPendaftaran.GetData(sKDKUNJUNGAN)

            Dim ds = oS_DIGITAL_AMBULAN.GetStructureHeader
            With ds
                .KDASESMEN = sNoid
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KDPENDAFTARAN = txtNoRegister.Text
                .NOPASIEN = txtNoPasien.Text

                Try
                    .DATECREATED = oS_DIGITAL_AMBULAN.GetData(sKDKUNJUNGAN).DATECREATED
                Catch ex As Exception
                    .DATECREATED = oS_DIGITAL_AMBULAN.GetDataDateTimeServer()
                End Try
                .DATEUPDATED = oS_DIGITAL_AMBULAN.GetDataDateTimeServer()
                .DATE = oS_DIGITAL_AMBULAN.GetDataDateTimeServer()
                .DIAGNOSA = txtDiagnosa.Text
                .HARITANGGAL = txtHariTanggal.Text
                .INDIKASI = txtIndikasiRujukan.Text
                .KDDOKTER = grdDOCTOR.EditValue
                .NMDOKTER = grdDOCTOR.Text
                .KDDRIVER = grdDriver.EditValue
                .NMDRIVER = grdDriver.Text
                .KDPERAWAT = grdPerawat.EditValue
                .NMPERAWAT = grdPerawat.Text
                .PENDAMPING = txtPendamping.Text
                .TERAPI = txtTerapi.Text
                .KODEAMBULAN = grdAmbulance.EditValue

                Try
                    .CETAK = oS_DIGITAL_AMBULAN.GetData(sKDKUNJUNGAN).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE

                .ISDELETE = 0
                .DATEDELETE = oS_DIGITAL_AMBULAN.GetDataDateTimeServer()
                .USERDELETE = "-"
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_AMBULAN.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_AMBULAN.UpdateData(ds)
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
            da.Fill(ds, "DOCTOR")

            grdDOCTOR.Properties.DataSource = ds.Tables("DOCTOR")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdDOCTOR.SelectedText = txtDokter.Text

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            SQL &= "AND KELOMPOKIPK = 'NAKES' "

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

    Private Sub fn_LoadDriver()
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
            SQL &= "AND (KDSTAFF='STAFF_0000000112' "
            SQL &= "OR KDSTAFF='STAFF_0000000123' "
            SQL &= "OR KDSTAFF='STAFF_0000000126' "
            SQL &= "OR KDSTAFF='STAFF_0000000294' ) "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdDriver.Properties.DataSource = ds.Tables("STAFF")
            grdDriver.Properties.ValueMember = "KDSTAFF"
            grdDriver.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub fn_LoadAmbulance()
        Try
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
            SQL &= "*"
            SQL &= "FROM "
            SQL &= "M_AMBULAN A "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "AMBULANCE")

            grdAmbulance.Properties.DataSource = ds.Tables("AMBULANCE")
            grdAmbulance.Properties.ValueMember = "KODE"
            grdAmbulance.Properties.DisplayMember = "TIPE"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    

#End Region
End Class
