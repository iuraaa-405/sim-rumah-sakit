Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmEMedrekRujuan_IGD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oEMedrekRujuan_IGD As New Digital.clsDigital_SRIGD
    Private sNoid As String = ""
    Private sKoneksi As String = String.Empty
    Private sDOCTOR As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private oPendaftaran As New Identitas.clsIdentitasPasien

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoid = NoId

        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNIK.Text = dsPendaftaran.NIK
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtNOBPJS.Text = dsPendaftaran.KARTUBPJS
            txtNoRegister.Text = dsPendaftaran.KDPENDAFTARAN
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtKUNJUNGAN.Text = dsPendaftaran.KDKUNJUNGAN
            txtDokter.Text = dsPendaftaran.DOKTER
            sKDKUNJUNGAN = dsPendaftaran.KDKUNJUNGAN

        Else
            txtNoPasien.ResetText()
            txtNIK.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtNOBPJS.ResetText()
            txtNoRegister.ResetText()
            txtTanggalDaftar.ResetText()
            txtKUNJUNGAN.ResetText()
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
        'fn_DIAGNOSA()

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
        deDATE.Properties.ReadOnly = Status
        'txtNOMOR.Properties.ReadOnly = Status
        grdDOKTERAHLI.Properties.ReadOnly = Status
        cboHUBUNGANKELUARGA.Properties.ReadOnly = Status
        txtKETERANGANMEDIS.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        txtTerapi.Properties.ReadOnly = Status
        txtKETERANGAN_LAIN.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        grdDOCTOR.Text = sDOCTOR
        deDATE.DateTime = Now
        txtNOMOR.Text = "<----AUTO---->"
        grdDOKTERAHLI.ResetText()
        cboHUBUNGANKELUARGA.ResetText()
        'txtKETERANGANMEDIS.ResetText()
        'txtDIAGNOSA.ResetText()
        'txtTerapi.ResetText()
        txtKETERANGAN_LAIN.ResetText()

        Dim oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01

        Dim dsIGD1 = oS_DIGITAL_IGD_01.GetData(txtKUNJUNGAN.Text)

        If dsIGD1 IsNot Nothing Then
            txtKETERANGANMEDIS.Text = dsIGD1.RIWAYAT
            txtDIAGNOSA.Text = dsIGD1.KDDIAGNOSA
        Else
            txtKETERANGANMEDIS.ResetText()
            txtDIAGNOSA.ResetText()
        End If

        txtTerapi.Text = fn_LoadPengobatan()

    End Sub
    Private Function fn_LoadPengobatan() As String
        Try
            Dim oIdentitas As New Identitas.clsIdentitasPasien
            Dim oResep As New Inventory.clsOrderResep
            Dim listObat As New List(Of String)

            For Each yloop In oResep.GetDataDetilList(sKDKUNJUNGAN)
                If yloop.KDITEM = "" Then
                    listObat.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Else
                    If fn_KodeItemAlkes(yloop.KDITEM) = False Then
                        listObat.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                    End If
                End If
            Next

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

            SQL = "SELECT TARIFKT = B.TARIFKT, ITEM = CONCAT('Tindakan : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERTINDAKAN_H A "
            SQL &= "INNER JOIN I_ORDERTINDAKAN_D B ON A.KDORDERTINDAKAN = B.KDORDERTINDAKAN "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDKUNJUNGAN = '" & sKDKUNJUNGAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TARIFKT = '', ITEM = CONCAT('Lab : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERLAB_H A  "
            SQL &= "INNER JOIN I_ORDERLAB_D B ON A.KDORDERLAB = B.KDORDERLAB "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDKUNJUNGAN = '" & sKDKUNJUNGAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TARIFKT = '', ITEM = CONCAT('Rad : ' , B.TARIFKT, ', Catatan : ' , B.REMARKS) FROM I_ORDERRAD_H A  "
            SQL &= "INNER JOIN I_ORDERRAD_D B ON A.KDORDERRAD = B.KDORDERRAD "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDKUNJUNGAN = '" & sKDKUNJUNGAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TARIFKT = '', ITEM = CONCAT('Konsul Dari : ' , A.DOKTER_DARI, ', Kepada : ' , A.DOKTER_KEPADA , ', Isi : ' , A.MEMO) FROM S_DIGITAL_KONSULTASI A  "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDKUNJUNGAN = '" & sKDKUNJUNGAN & "' "

            SQL &= "UNION  "

            SQL &= "SELECT TARIFKT = '', ITEM = CONCAT('Jawab Konsul Dari : ' , A.DOKTER_DARI, ', Kepada : ' , A.DOKTER_KEPADA , ', Isi : ' , A.MEMO) FROM S_DIGITAL_JAWABKONSUL A  "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C ON C.KDKUNJUNGAN = A.KDKUNJUNGAN "
            SQL &= "WHERE C.KDKUNJUNGAN = '" & sKDKUNJUNGAN & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ASMEDIGD")

            For iLoop As Integer = 0 To ds.Tables("ASMEDIGD").Rows.Count - 1
                With ds.Tables("ASMEDIGD")

                    If .Rows(iLoop)("TARIFKT") <> "ADMINISTRASI RAWAT JALAN" Then
                        If .Rows(iLoop)("TARIFKT") <> "PEMERIKSAAN DR. POLIKLINIK" Then
                            listObat.Add(.Rows(iLoop)("ITEM"))
                        End If
                    End If
                End With
            Next

            fn_LoadPengobatan = String.Join(vbCrLf, listObat.ToArray)
        Catch ex As Exception
            fn_LoadPengobatan = ""
            'MsgBox("Reload Hasil Obat" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_KodeItemAlkes(ByVal KDITEM As String) As Boolean
        Try
            fn_KodeItemAlkes = False

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
            SQL &= "A.DESCRIPTION "
            SQL &= "FROM "
            SQL &= "DUSTIRA_FARMASI.dbo.M_ITEM_L6 A "
            SQL &= "INNER JOIN DUSTIRA_FARMASI.dbo.M_ITEM B "
            SQL &= "ON A.KDITEM_L6 = B.KDITEM_L6 "
            SQL &= "WHERE "
            SQL &= "B.KDITEM = '" & KDITEM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDATARADIOLOGI")


            For iLoop As Integer = 0 To ds.Tables("GETDATARADIOLOGI").Rows.Count - 1
                With ds.Tables("GETDATARADIOLOGI")
                    If .Rows(iLoop)("DESCRIPTION") = "ALKES" Then
                        fn_KodeItemAlkes = True
                    Else
                        fn_KodeItemAlkes = False
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            fn_KodeItemAlkes = False
            MsgBox("Get Kode Alkes" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oEMedrekRujuan_IGD.GetData(sNoid)

            With ds
                grdDOCTOR.Text = .KDDOCTOR
                deDATE.DateTime = .DATE
                txtNOMOR.Text = .NOMOR
                grdDOKTERAHLI.Text = .KDDOCTORAHLI
                cboHUBUNGANKELUARGA.Text = .HUBUNGAN
                txtKETERANGANMEDIS.Text = .KETERANGAN_MEDIS
                txtDIAGNOSA.Text = .DIAGNOSA
                txtTerapi.Text = .PENGOBATAN_TINDAKAN
                txtKETERANGAN_LAIN.Text = .KETERANGAN_LAIN

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

            Dim ds = oEMedrekRujuan_IGD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oEMedrekRujuan_IGD.GetData(sKDKUNJUNGAN).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDSR_IGD = sNoid
                .NOMOR = txtNOMOR.Text
                .DOKTER_AHLI = grdDOKTERAHLI.Text
                .KDDOCTORAHLI = grdDOKTERAHLI.EditValue
                .KDKUNJUNGAN = txtKUNJUNGAN.Text
                .HUBUNGAN = cboHUBUNGANKELUARGA.Text
                .KETERANGAN_MEDIS = txtKETERANGANMEDIS.Text
                .DIAGNOSA = txtDIAGNOSA.Text
                .PENGOBATAN_TINDAKAN = txtTerapi.Text
                .KETERANGAN_LAIN = txtKETERANGAN_LAIN.Text
                .KDDOCTOR = grdDOCTOR.EditValue
                .DOCTOR = grdDOCTOR.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oEMedrekRujuan_IGD.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oEMedrekRujuan_IGD.UpdateData(ds)
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

            grdDOKTERAHLI.Properties.DataSource = ds.Tables("DOCTOR")
            grdDOKTERAHLI.Properties.ValueMember = "KDDOCTOR"
            grdDOKTERAHLI.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_DIAGNOSA()
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
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "DATABASE_NEW.dbo.M_DIAGNOSA A "
            'SQL &= "WHERE "
            'SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            'grdDIAGNOSA.Properties.DataSource = ds.Tables("DIAGNOSA")
            'grdDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            'grdDIAGNOSA.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class
