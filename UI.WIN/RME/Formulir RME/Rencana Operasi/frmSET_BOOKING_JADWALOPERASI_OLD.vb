Imports DataAccess
Imports System.Data.SqlClient
Imports UI.WIN.MAIN.My.Resources

Public Class frmSET_BOOKING_JADWALOPERASI_OLD
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

        Dim oIdentitas As New Identitas.clsIdentitasPasien
        Dim dsIdentitas = oIdentitas.GetData(txtKDREG.Text)
        If dsIdentitas IsNot Nothing Then
            Dim oDIAGNOSA As New Diagnosa.clsMasterDiagnosa
            Dim dsDiagnosaUtama = oDIAGNOSA.GetDataDiagnosaUtama(txtKDREG.Text)
            If dsDiagnosaUtama IsNot Nothing Then
                grdKDDIAGNOSA.Text = dsDiagnosaUtama.KDDIAGNOSA
                txtDIAGNOSAMEDIS.Text = dsDiagnosaUtama.REMARKS
            Else
                grdKDDIAGNOSA.Text = dsIdentitas.KDDIAGNOSA
            End If
        End If
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

                    SQL &= "WHERE KDBOOKINGJADWALOPERASI = '"& dsBookingOperasi.KDBOOKINGJADWALOPERASI &"' "

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
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
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

#End Region
End Class