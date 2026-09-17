Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient

Public Class frmSET_BOOKING_JADWALOPERASI
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private sKDKUNJUNGAN As String = String.Empty
    Private oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = SET_BOOKING_JADWALOPERASII.TITLE

            lKDBOOKINGJADWALOPERASI.Text = "Kode"
            lKDPENDAFTARAN.Text = SET_BOOKING_JADWALOPERASII.KDPENDAFTARAN
            lTANGGALOPERASI.Text = SET_BOOKING_JADWALOPERASII.TANGGALOPERASI
            lJENISTINDAKAN.Text = SET_BOOKING_JADWALOPERASII.JENISTINDAKAN
            lJENISOPERASI.Text = "Jenis Operasi"
            chkISAPPROVAL.Text = SET_BOOKING_JADWALOPERASII.ISAPROVAL
            lREMARKS.Text = SET_BOOKING_JADWALOPERASII.REMARKS

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
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

        deTANGGALOPERASI.Properties.ReadOnly = Status
        txtKDPENDAFTARAN.Properties.ReadOnly = True
        txtJENISTINDAKAN.Properties.ReadOnly = Status
        txtJENISOPERASI.Properties.ReadOnly = Status
        chkISAPPROVAL.Properties.ReadOnly = Status
        txtREMARKS.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        deTANGGALOPERASI.DateTime = Now
        txtKDPENDAFTARAN.Text = sKDKUNJUNGAN
        txtJENISTINDAKAN.ResetText()
        txtJENISOPERASI.ResetText()
        txtREMARKS.ResetText()
        chkISAPPROVAL.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSET_BOOKING_JADWALOPERASI.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                deTANGGALOPERASI.DateTime = .TANGGALOPERASI
                txtKDPENDAFTARAN.Text = .KDKUNJUNGAN
                txtJENISTINDAKAN.Text = .JENISTINDAKAN
                txtJENISOPERASI.Text = .JENISOPERASI
                txtREMARKS.Text = .REMARKS
                chkISAPPROVAL.Checked = .ISAPROVAL
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtJENISTINDAKAN.Text = String.Empty Then
                txtJENISTINDAKAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJENISTINDAKAN.ErrorText = Statement.ErrorRequired

                txtJENISTINDAKAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtJENISOPERASI.Text = String.Empty Then
                txtJENISOPERASI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtJENISOPERASI.ErrorText = Statement.ErrorRequired

                txtJENISOPERASI.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtREMARKS.Text = String.Empty Then
                txtREMARKS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtREMARKS.ErrorText = Statement.ErrorRequired

                txtREMARKS.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    'Private Function fn_SaveBookoingOperasi(ByVal KDBOOKING As String, ByVal DATECREATED As DateTime) As Boolean
    '    Try
    '        fn_SaveBookoingOperasi = True

    '        Dim Koneksi As String = oSET_BOOKING_JADWALOPERASI.GetKoneksiApi()
    '        Dim oPendaftaran As New Admission.clsPendaftaran
    '        Dim oDepartment As New Reference.clsDepartment
    '        Dim dsPendaftaran = oPendaftaran.GetData(txtKDPENDAFTARAN.Text)

    '        If dsPendaftaran IsNot Nothing Then
    '            If Koneksi <> String.Empty Then
    '                Dim oConn As New SqlConnection
    '                Dim oComm As New SqlCommand
    '                Dim da As SqlDataAdapter
    '                Dim ds As New DataSet
    '                Dim SQL As String

    '                Dim sConn As String = Koneksi

    '                oConn = New SqlConnection(sConn)

    '                SQL = "SELECT * FROM JADWALOPERASI WHERE KDBOOKINGJADWALOPERASI = '" & KDBOOKING & "'  "

    '                oComm.Connection = oConn
    '                oComm.CommandText = SQL
    '                oComm.CommandTimeout = 120
    '                oComm.CommandType = CommandType.Text

    '                da = New SqlDataAdapter(oComm)
    '                da.Fill(ds, "SELECT_BOOKING")

    '                If ds.Tables("SELECT_BOOKING").Rows.Count > 0 Then
    '                    SQL = "DELETE FROM JADWALOPERASI WHERE KDBOOKINGJADWALOPERASI = '" & KDBOOKING & "'  "
    '                    oComm.Connection = oConn
    '                    oComm.CommandText = SQL
    '                    oComm.CommandTimeout = 120
    '                    oComm.CommandType = CommandType.Text
    '                    da = New SqlDataAdapter(oComm)
    '                    da.Fill(ds, "DELETE_BOOKING")
    '                End If

    '                SQL = "INSERT INTO "
    '                SQL &= "JADWALOPERASI "
    '                SQL &= "("
    '                SQL &= "KDBOOKINGJADWALOPERASI "
    '                SQL &= ",KDPENDAFTARAN "
    '                SQL &= ",TANGGALOPERASI "
    '                SQL &= ",JENISTINDAKAN "
    '                SQL &= ",JENISOPERASI "
    '                SQL &= ",ISAPROVAL "
    '                SQL &= ",ISCHEKED "
    '                SQL &= ",KDUSER "
    '                SQL &= ",REMARKS "
    '                SQL &= ",NOBPJS "
    '                SQL &= ",KODEPOLI "
    '                SQL &= ",NAME_DISPLAY "
    '                SQL &= ",NAMA_PASIEN "
    '                SQL &= ") "
    '                SQL &= "VALUES "
    '                SQL &= "("
    '                SQL &= "'" & KDBOOKING & "' "
    '                SQL &= ",'" & txtKDPENDAFTARAN.Text & "' "
    '                SQL &= ",'" & deTANGGALOPERASI.DateTime.ToString("yyyy-MM-dd HH:mm:ss") & "' "
    '                SQL &= ",'" & txtJENISTINDAKAN.Text & "' "
    '                SQL &= ",'" & txtJENISOPERASI.Text & "' "
    '                SQL &= ",0 "
    '                SQL &= ",0 "
    '                SQL &= ",'" & sUserID & "' "
    '                SQL &= ",'" & txtREMARKS.Text & "'"
    '                SQL &= ",'" & dsPendaftaran.KARTUBPJS & "' "
    '                SQL &= ",'" & oDepartment.GetData(dsPendaftaran.KDDEPARTMENT).VCLAIM_KODEPOLI & "' "
    '                SQL &= ",'" & oDepartment.GetData(dsPendaftaran.KDDEPARTMENT).NAME_DISPLAY & "' "
    '                SQL &= ",'" & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & "' "
    '                SQL &= ")"

    '                oComm.Connection = oConn
    '                oComm.CommandText = SQL
    '                oComm.CommandTimeout = 120
    '                oComm.CommandType = CommandType.Text

    '                da = New SqlDataAdapter(oComm)
    '                da.Fill(ds, "INSERT_DATABASE_API")

    '                If oConn.State = ConnectionState.Open Then
    '                    oConn.Close()
    '                End If
    '            Else
    '                fn_SaveBookoingOperasi = False
    '                MsgBox("Koneksi tidak ada untuk database API", MsgBoxStyle.Exclamation, Me.Text)
    '            End If
    '        Else
    '            fn_SaveBookoingOperasi = False
    '            MsgBox("Pendaftaran tidak ditemukan untuk save database API", MsgBoxStyle.Exclamation, Me.Text)
    '        End If

    '    Catch oErr As Exception
    '        fn_SaveBookoingOperasi = False
    '        MsgBox("Save Database API" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Function
    Private Function fn_Save() As Boolean
        Try
            Dim DATECREATED As DateTime = Now

            ' ***** HEADER *****
            Dim ds = oSET_BOOKING_JADWALOPERASI.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSET_BOOKING_JADWALOPERASI.GetData(sNoId).DATECREATED
                    DATECREATED = .DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
                Dim dsKunjungan = oKunjungan.GetData(sKDKUNJUNGAN)
                If dsKunjungan IsNot Nothing Then
                    .KDBOOKINGJADWALOPERASI = sNoId
                    .KDKUNJUNGAN = txtKDPENDAFTARAN.Text
                    .TANGGALOPERASI = deTANGGALOPERASI.DateTime
                    .JENISTINDAKAN = txtJENISTINDAKAN.Text
                    .JENISOPERASI = txtJENISOPERASI.Text
                    .DIAGNOSA_KODE = dsKunjungan.S_PENDAFTARAN_H.KDDIAGNOSA
                    .DIAGNOSA_MEMO = dsKunjungan.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO
                    .DOCTOR_KODE = dsKunjungan.S_PENDAFTARAN_H.KDDOCTOR
                    .DOCTOR_NAME_DISPLAY = dsKunjungan.S_PENDAFTARAN_H.M_DOCTOR.NAME_DISPLAY
                    .ISAPROVAL = chkISAPPROVAL.Checked
                    .ISCHEKED = False
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = ""
                    .LAMAOPERASI = 0
                    .ALATKHUSUS = ""
                    .REMARKS = txtREMARKS.Text
                    .DIAGNOSAMEDIS = ""
                End If
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    Dim oCounter As New Setting.clsCounter
                    Dim sLASTNUMBER As Integer = 0
                    Dim sMODUL = "JOPX"
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deTANGGALOPERASI.DateTime)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, deTANGGALOPERASI.DateTime)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, deTANGGALOPERASI.DateTime)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    txtCODE.Text = oSET_BOOKING_JADWALOPERASI.AutoNumber(sMODUL, sLASTNUMBER + 1, deTANGGALOPERASI.DateTime)

                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(deTANGGALOPERASI.DateTime), Year(deTANGGALOPERASI.DateTime))

                    fn_Save = oSET_BOOKING_JADWALOPERASI.InsertData(ds, txtCODE.Text)

                    'If fn_SaveBookoingOperasi(txtCODE.Text, DATECREATED) = True Then
                    '    fn_Save = oSET_BOOKING_JADWALOPERASI.InsertData(ds, txtCODE.Text)
                    'Else
                    '    fn_Save =False 
                    'End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSET_BOOKING_JADWALOPERASI.UpdateData(ds)

                    'If fn_SaveBookoingOperasi(txtCODE.Text, DATECREATED) = True Then
                    '    fn_Save = oSET_BOOKING_JADWALOPERASI.UpdateData(ds)
                    'Else
                    '    fn_Save = False
                    'End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
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
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class