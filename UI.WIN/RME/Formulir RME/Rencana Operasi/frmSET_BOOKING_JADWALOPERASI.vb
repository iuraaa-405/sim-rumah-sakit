Imports DataAccess
Imports System.Data.SqlClient
Imports UI.WIN.MAIN.My.Resources

Public Class frmSET_BOOKING_JADWALOPERASI
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSET_BOOKING_JADWALOPERASI As New Sales.clsSET_BOOKING_JADWALOPERASI
    Private sKDREG As String = String.Empty
    Private sKoneksi As String = String.Empty
    Private sKoneksi_ As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDREG = KDKUNJUNGAN
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
            sKoneksi_ = dsSetKoneksi.GENERATE_ECLAIM
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_DiagnosaICD10()
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
        cboJENISTINDAKAN.Properties.ReadOnly = Status
        txtJENISOPERASI.Properties.ReadOnly = Status
        grdKDDIAGNOSA.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtLAMAOPERASI.Properties.ReadOnly = Status
        txtALATKHUSUS.Properties.ReadOnly = Status
        txtREMARKS.Properties.ReadOnly = Status
        txtDIAGNOSAMEDIS.Properties.ReadOnly = Status

        cboKamar.Properties.ReadOnly = Status
        cboSESI.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtKDREG.ResetText()
        cboJENISTINDAKAN.SelectedIndex = 0
        txtJENISOPERASI.ResetText()
        grdKDDOCTOR.ResetText()
        txtLAMAOPERASI.ResetText()
        txtALATKHUSUS.ResetText()
        txtREMARKS.Text = "-"
        txtKDREG.Text = sKDREG
        txtDIAGNOSAMEDIS.ResetText()

        cboKamar.ResetText()
        cboSESI.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSET_BOOKING_JADWALOPERASI.GetData(sNoId)

            With ds
                txtCODE.Text = .KDBOOKINGJADWALOPERASI
                deDATE.DateTime = .TANGGALOPERASI
                txtKDREG.Text = .KDKUNJUNGAN
                cboJENISTINDAKAN.Text = .JENISTINDAKAN
                txtJENISOPERASI.Text = .JENISOPERASI
                grdKDDIAGNOSA.Text = .DIAGNOSA_KODE
                grdKDDOCTOR.Text = .DOCTOR_KODE
                txtLAMAOPERASI.Text = .LAMAOPERASI
                txtALATKHUSUS.Text = .ALATKHUSUS
                txtREMARKS.Text = .REMARKS
                txtDIAGNOSAMEDIS.Text = .DIAGNOSAMEDIS

                cboKamar.Text = .KAMAR
                cboSESI.Text = .SESI
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDREG.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtKDREG.Focus()
                fn_Validate = False
                Exit Function
            End If
            If cboJENISTINDAKAN.Text = String.Empty Then
                MsgBox("Dibutuhkan Jenis Tindakan", MsgBoxStyle.Exclamation, Me.Text)
                cboJENISTINDAKAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtJENISOPERASI.Text = String.Empty Then
                MsgBox("Dibutuhkan Jenis Operasi", MsgBoxStyle.Exclamation, Me.Text)
                txtJENISOPERASI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDIAGNOSA.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Dibutuhkan Operator", MsgBoxStyle.Exclamation, Me.Text)
                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOSAMEDIS.Text = String.Empty Then
                MsgBox("Dibutuhkan Diagnosa Medis", MsgBoxStyle.Exclamation, Me.Text)
                txtDIAGNOSAMEDIS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboKamar.Text = String.Empty Then
                MsgBox("Dibutuhkan Kamar", MsgBoxStyle.Exclamation, Me.Text)
                cboKamar.Focus()
                fn_Validate = False
                Exit Function
            End If

            If cboSESI.Text = String.Empty Then
                MsgBox("Dibutuhkan Sesi", MsgBoxStyle.Exclamation, Me.Text)
                cboSESI.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtALATKHUSUS.Text = String.Empty Then
                MsgBox("Dibutuhkan Alat Khusus", MsgBoxStyle.Exclamation, Me.Text)
                txtALATKHUSUS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtREMARKS.Text = String.Empty Then
                MsgBox("Dibutuhkan Keterangan", MsgBoxStyle.Exclamation, Me.Text)
                txtREMARKS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oSET_BOOKING_JADWALOPERASI.IsExistByPeriode(deDATE.DateTime, cboKamar.Text, cboSESI.Text) = True Then
                    MsgBox("Jadwal Sudah digunakan, Pilih Kamar dan Sesi Lain", MsgBoxStyle.Exclamation, Me.Text)
                    cboKamar.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                Dim dsBooking = oSET_BOOKING_JADWALOPERASI.GetData(sNoId)

                If dsBooking IsNot Nothing Then
                    If deDATE.DateTime.ToString("yyyyMMdd") <> dsBooking.TANGGALOPERASI.ToString("yyyyMMdd") And cboKamar.Text <> dsBooking.KAMAR And cboSESI.Text <> dsBooking.SESI Then
                        If oSET_BOOKING_JADWALOPERASI.IsExistByPeriode(deDATE.DateTime, cboKamar.Text, cboSESI.Text) = True Then
                            MsgBox("Jadwal Sudah digunakan, Pilih Kamar dan Sesi Lain", MsgBoxStyle.Exclamation, Me.Text)
                            cboKamar.Focus()
                            fn_Validate = False
                            Exit Function
                        End If
                    End If
                End If
            End If

            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    Dim ds = oSET_BOOKING_JADWALOPERASI.GetDataByKDREG(txtKDREG.Text)

            '    If ds IsNot Nothing Then
            '        MsgBox("Register sudah booking tidak dapat booking lagi", MsgBoxStyle.Exclamation, Me.Text)
            '        txtKDREG.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If

            'End If

        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Sub fn_Print(ByVal sKDBOOKING As String)
    '    Try
    '        If sKDBOOKING = String.Empty Then Exit Sub

    '        Dim rpt As New xtraSET_BOOKING_JADWALOPERASI
    '        Dim ds = oSET_BOOKING_JADWALOPERASI.GetData(sKDBOOKING)
    '        rpt.bindingSource.DataSource = ds

    '        If ds.S_PENDAFTARAN_H.ID = String.Empty Then
    '            sAlamatPasien_Asesmen = ds.S_PENDAFTARAN_H.ALAMATPASIENJALAN
    '        Else
    '            Dim oALL As New Master.clsDesa

    '            Dim dsID = oALL.GetData(ds.S_PENDAFTARAN_H.ID)

    '            If dsID IsNot Nothing Then

    '                sAlamatPasien_Asesmen = ds.S_PENDAFTARAN_H.ALAMATPASIENJALAN.ToString.Trim & " RT " & ds.S_PENDAFTARAN_H.RT.ToString.Trim & "/" & ds.S_PENDAFTARAN_H.RW.ToString.Trim & " " & dsID.NAMADESA.ToString.Trim & " " & dsID.M_KECAMATAN.NAMAKEC.ToString.Trim & " " & dsID.M_KOTA.NAMAKOTA.ToString.Trim

    '            End If

    '        End If

    '        If ds.S_PENDAFTARAN_H.KODE Is Nothing Then
    '            sKESATUAN = "***"
    '        Else
    '            Try
    '                Dim oSATUAN As New Master.clsSatuan
    '                sKESATUAN = oSATUAN.GetData(ds.S_PENDAFTARAN_H.KODE).NAMA.Trim.ToString.ToUpper
    '            Catch ex As Exception
    '                sKESATUAN = "***"
    '            End Try
    '        End If

    '        Try
    '            Dim oPangkat As New Master.clsJabatan
    '            sPangkat = oPangkat.GetData(ds.S_PENDAFTARAN_H.KODEJBT).NAMAJBT.Trim
    '        Catch ex As Exception
    '            sPangkat = "***"
    '        End Try

    '        sHARIOPERASI = Replace(Replace(Replace(Replace(Replace(Replace(Replace(ds.TANGGALOPERASI.ToString("dddd"), "Sunday", "Minggu"), "Monday", "Senin"), "Tuesday", "Selasa"), "Wednesday", "Rabu"), "Thursday", "Kamis"), "Friday", "Jumat"), "Saturday", "Sabtu") & " / " & ds.TANGGALOPERASI.ToString("dd-MM-yyyy")

    '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
    '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
    '    Catch oErr As Exception
    '        MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSET_BOOKING_JADWALOPERASI.GetStructureHeader
            With ds
                .KDBOOKINGJADWALOPERASI = sNoId
                Try
                    .DATECREATED = oSET_BOOKING_JADWALOPERASI.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .TANGGALOPERASI = deDATE.DateTime
                .KDKUNJUNGAN = txtKDREG.Text.ToString.Trim.ToUpper
                .JENISTINDAKAN = cboJENISTINDAKAN.Text.ToString.Trim.ToUpper
                .JENISOPERASI = txtJENISOPERASI.Text.ToString.Trim.ToUpper
                .DIAGNOSA_KODE = grdKDDIAGNOSA.EditValue
                .DIAGNOSA_MEMO = grdKDDIAGNOSA.Text
                .DOCTOR_KODE = grdKDDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdKDDOCTOR.Text
                .LAMAOPERASI = txtLAMAOPERASI.Text.ToString.Trim.ToUpper
                .ALATKHUSUS = txtALATKHUSUS.Text.ToString.Trim.ToUpper
                .REMARKS = txtREMARKS.Text.Trim.ToUpper
                .DIAGNOSAMEDIS = txtDIAGNOSAMEDIS.Text.Trim.ToUpper
                Try
                    .ISAPROVAL = oSET_BOOKING_JADWALOPERASI.GetData(sNoId).ISAPROVAL
                Catch ex As Exception
                    .ISAPROVAL = 0
                End Try
                Try
                    .ISCHEKED = oSET_BOOKING_JADWALOPERASI.GetData(sNoId).ISAPROVAL
                Catch ex As Exception
                    .ISCHEKED = True
                End Try

                .KAMAR = cboKamar.Text
                .SESI = cboSESI.Text

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim kdbooking As String = String.Empty
                    kdbooking = oSET_BOOKING_JADWALOPERASI.InsertData(ds)

                    If kdbooking = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        fn_Save_simrsLama(kdbooking)
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSET_BOOKING_JADWALOPERASI.UpdateData(ds)

                    fn_Save_simrsLama(ds.KDBOOKINGJADWALOPERASI)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_Save_simrsLama(ByVal KDBOOKINGJADWALOPERASI As String) As String
        Try
            Dim dsBookingOperasi = oSET_BOOKING_JADWALOPERASI.GetData(KDBOOKINGJADWALOPERASI)

            If dsBookingOperasi IsNot Nothing Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                Dim sConn As String = sKoneksi_
                oConn = New SqlConnection(sConn)

                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT *  "
                SQL &= "FROM SET_BOOKING_JADWALOPERASI "
                SQL &= "WHERE "
                SQL &= "KDBOOKINGJADWALOPERASI = '" & KDBOOKINGJADWALOPERASI & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "GETDATA_PENDAFTARAN")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                If ds.Tables("GETDATA_PENDAFTARAN").Rows.Count > 0 Then
                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "UPDATE "
                    SQL &= "SET_BOOKING_JADWALOPERASI "
                    SQL &= "SET "
                    SQL &= "[DATEUPDATED] = '" & dsBookingOperasi.DATEUPDATED.ToString("yyyy-MM-dd HH:mm:ss") & "'  "
                    SQL &= ",[KDREG] = '" & dsBookingOperasi.R_IDENTITAS_PASIEN.KDPENDAFTARAN & "' "
                    SQL &= ",[TANGGALOPERASI] = '" & dsBookingOperasi.TANGGALOPERASI.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                    SQL &= ",[JENISTINDAKAN] = '" & dsBookingOperasi.JENISTINDAKAN & "' "
                    SQL &= ",[JENISOPERASI] = '" & dsBookingOperasi.JENISOPERASI & "' "
                    SQL &= ",[KDDIAGNOSA] = '" & dsBookingOperasi.DIAGNOSA_KODE & "' "
                    SQL &= ",[KDDOCTOR] = " & CInt(dsBookingOperasi.DOCTOR_KODE) & " "
                    SQL &= ",[ISAPROVAL] = " & CInt(dsBookingOperasi.ISAPROVAL) & " "
                    SQL &= ",[ISCHEKED] = " & IIf(dsBookingOperasi.ISCHEKED = True, 1, 0) & " "
                    SQL &= ",[NOIDUSER] = '" & dsBookingOperasi.KDUSER & "' "
                    SQL &= ",[LAMAOPERASI] = '" & dsBookingOperasi.LAMAOPERASI & "' "
                    SQL &= ",[ALATKHUSUS] = '" & dsBookingOperasi.ALATKHUSUS & "' "
                    SQL &= ",[REMARKS] = '" & dsBookingOperasi.REMARKS & "' "
                    SQL &= ",[DIAGNOSAMEDIS] = '" & dsBookingOperasi.DIAGNOSAMEDIS & "' "
                    SQL &= ",[KAMAR] = '" & dsBookingOperasi.KAMAR & "' "
                    SQL &= ",[SESI] = '" & dsBookingOperasi.SESI & "' "

                    SQL &= "WHERE KDBOOKINGJADWALOPERASI = '" & dsBookingOperasi.KDBOOKINGJADWALOPERASI & "' "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "INSERTSET_BOOKING")

                    fn_Save_simrsLama = SQL

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If
                Else
                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "INSERT INTO "
                    SQL &= "SET_BOOKING_JADWALOPERASI "
                    SQL &= "( "
                    SQL &= "[KDBOOKINGJADWALOPERASI] "
                    SQL &= ",[DATECREATED] "
                    SQL &= ",[DATEUPDATED] "
                    SQL &= ",[KDREG] "
                    SQL &= ",[TANGGALOPERASI] "
                    SQL &= ",[JENISTINDAKAN] "
                    SQL &= ",[JENISOPERASI] "
                    SQL &= ",[KDDIAGNOSA] "
                    SQL &= ",[KDDOCTOR] "
                    SQL &= ",[ISAPROVAL] "
                    SQL &= ",[ISCHEKED] "
                    SQL &= ",[NOIDUSER] "
                    SQL &= ",[LAMAOPERASI] "
                    SQL &= ",[ALATKHUSUS] "
                    SQL &= ",[REMARKS] "
                    SQL &= ",[DIAGNOSAMEDIS] "
                    SQL &= ",[KAMAR] "
                    SQL &= ",[SESI] "
                    SQL &= ") "

                    SQL &= "VALUES "

                    SQL &= "( "
                    SQL &= "'" & dsBookingOperasi.KDBOOKINGJADWALOPERASI & "' "
                    SQL &= ",'" & dsBookingOperasi.DATECREATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                    SQL &= ",'" & dsBookingOperasi.DATEUPDATED.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                    SQL &= ",'" & dsBookingOperasi.R_IDENTITAS_PASIEN.KDPENDAFTARAN & "' "
                    SQL &= ",'" & dsBookingOperasi.TANGGALOPERASI.ToString("yyyy-MM-dd HH:mm:ss") & "' "
                    SQL &= ",'" & dsBookingOperasi.JENISTINDAKAN & "' "
                    SQL &= ",'" & dsBookingOperasi.JENISOPERASI & "' "
                    SQL &= ",'" & dsBookingOperasi.DIAGNOSA_KODE & "' "
                    SQL &= "," & CInt(dsBookingOperasi.DOCTOR_KODE) & " "
                    SQL &= "," & CInt(dsBookingOperasi.ISAPROVAL) & " "
                    SQL &= "," & IIf(dsBookingOperasi.ISCHEKED = True, 1, 0) & " "
                    SQL &= ",'" & dsBookingOperasi.KDUSER & "' "
                    SQL &= ",'" & dsBookingOperasi.LAMAOPERASI & "' "
                    SQL &= ",'" & dsBookingOperasi.ALATKHUSUS & "' "
                    SQL &= ",'" & dsBookingOperasi.REMARKS & "' "
                    SQL &= ",'" & dsBookingOperasi.DIAGNOSAMEDIS & "' "
                    SQL &= ",'" & dsBookingOperasi.KAMAR & "' "
                    SQL &= ",'" & dsBookingOperasi.SESI & "' "
                    SQL &= ") "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "INSERTSET_BOOKING")

                    fn_Save_simrsLama = SQL

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If
                End If
            Else
                fn_Save_simrsLama = "JADWAL TIDAK ADA"
            End If

        Catch oErr As Exception
            fn_Save_simrsLama = oErr.Message
            MsgBox("Gagal Simpan RI ke SIMRS Lama, Silahkan Edit Kembali" & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtCODE.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtCODE.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadPendaftaran(ByVal Parameter As String)
        'Dim oPendaftaran As New Sales.clsPendaftaran
        'Try
        '    Dim dsPendaftaran = From x In oPendaftaran.GetDataByNopasienKDREG(Parameter, cboKategoriCari.SelectedIndex)
        '                        Select x.KDREG, x.NOPASIEN, NAMAPASIEN = x.MASTER_PASIEN.namapasien.ToString.Trim.ToUpper, x.DATE, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY

        '    grdCari.Properties.DataSource = dsPendaftaran.ToList()
        '    grdCari.Properties.ValueMember = "KDREG"
        '    grdCari.Properties.DisplayMember = "KDREG"

        '    grdCari.ShowPopup()

        'Catch oErr As Exception
        '    MsgBox("Load Pendaftaran Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)

        'End Try
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs)
        'If Asc(e.KeyChar) = 13 Then
        '    Dim Parameter As String = String.Empty

        '    If txtCARI.Text = "" Then Exit Sub

        '    If cboKategoriCari.SelectedIndex = 0 Then
        '        Parameter = txtCARI.Text.ToString.PadLeft(8, "0")
        '    Else
        '        Parameter = txtCARI.Text
        '    End If

        '    fn_LoadPendaftaran(Parameter)

        '    txtCARI.ResetText()

        'End If
    End Sub
    Private Sub grdCari_KeyPress(sender As Object, e As KeyPressEventArgs)
        'If Asc(e.KeyChar) = 13 Then
        '    If grdCari.Text <> String.Empty Then
        '        txtKDREG.Text = grdCari.Text
        '    End If
        'End If
    End Sub
    Private Sub fn_DiagnosaICD10()
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
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            grdKDDIAGNOSA.Properties.DataSource = ds.Tables("DIAGNOSA")
            grdKDDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            da.Fill(ds, "DOCTOR")

            grdKDDOCTOR.Properties.DataSource = ds.Tables("DOCTOR")
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub deDATE_EditValueChanged(sender As Object, e As EventArgs) Handles deDATE.EditValueChanged
        txtHARI.Text = Replace(Replace(Replace(Replace(Replace(Replace(Replace(deDATE.DateTime.ToString("dddd"), "Sunday", "Minggu"), "Monday", "Senin"), "Tuesday", "Selasa"), "Wednesday", "Rabu"), "Thursday", "Kamis"), "Friday", "Jumat"), "Saturday", "Sabtu")
    End Sub

    Private Sub btnCari_Click(sender As Object, e As EventArgs) Handles btnCari.Click
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi_
            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "Tanggal = A.TANGGALOPERASI "
            SQL &= ",Kamar = A.KAMAR "
            SQL &= ",Sesi = A.SESI "
            SQL &= ",Poliklinik = D.NAME_DISPLAY "
            SQL &= ",NamaDokter = C.NAME_DISPLAY "
            SQL &= ",NomorRegister = A.KDREG "
            SQL &= ",DiagnosaMedis = A.DIAGNOSAMEDIS "

            SQL &= "FROM SET_BOOKING_JADWALOPERASI AS A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B ON A.KDREG = B.KDREG "
            SQL &= "INNER JOIN M_DOCTOR C ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT D ON B.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALOPERASI, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.TANGGALOPERASI, 112) <= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "BOOKING_JADWALOPERASI")

            bindingSource.DataSource = ds.Tables("BOOKING_JADWALOPERASI")

            grdJadwal.DataSource = bindingSource
            grdJadwal.ForceInitialize()


            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Jadwal: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_SetFormat()
        grvJadwal.Columns("Kamar").Group()
        grvJadwal.ExpandAllGroups()

        For iLoop As Integer = 0 To grvJadwal.Columns.Count - 1
            If grvJadwal.Columns(iLoop).ColumnType.Name = "Decimal" Then
                'grvBilling.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                'grvBilling.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                'grvBilling.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far

                'grvBilling.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, grv.Columns(iLoop).FieldName, grv.Columns(iLoop),
                '                     "{0:n0}")
                'grvBilling.Columns(iLoop).SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum
                'grvBilling.Columns(iLoop).SummaryItem.DisplayFormat = "{0:n2}"
            ElseIf grvJadwal.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvJadwal.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvJadwal.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"

            End If
        Next

        grvJadwal.BestFitColumns()
        grvJadwal.OptionsView.ColumnAutoWidth = False
    End Sub

    Private Sub cboSESI_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSESI.SelectedIndexChanged
        If cboSESI.SelectedIndex = 1 Or cboSESI.SelectedIndex = 2 Then
            If txtHARI.Text = "Jumat" Then
                txtLAMAOPERASI.Text = "1 Jam 30 Menit"
            Else
                txtLAMAOPERASI.Text = "2 Jam"
            End If
        Else
            txtLAMAOPERASI.Text = "2 Jam"
        End If
    End Sub

#End Region
End Class