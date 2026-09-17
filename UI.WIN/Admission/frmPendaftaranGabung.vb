Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPendaftaranGabung
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sSEQ As String
    Private isLoad As Boolean = False
    Private oPendaftaranGabung As New Admission.clsPendaftaranGabung
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, ByVal SEQ As String)
        oFormMode = FormMode
        sNoId = NoId
        sSEQ = SEQ
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Pendaftaran Gabung"

            'lMEMO.Text = PendaftaranGabung.MEMO & " *"
            'chkISACTIVE.Text = PendaftaranGabung.ISACTIVE
            'chkISDEFAULT.Text = PendaftaranGabung.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtMEMO.Text.Trim.ToUpper
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

        txtKDPENDAFTARAN.Properties.ReadOnly = True
        txtNOMORSEP.Properties.ReadOnly = True
        txtSEQ.Properties.ReadOnly = True
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oPendaftaranGabung.GetData(sNoId, sSEQ)

            With ds
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                txtSEQ.Text = .SEQ
                txtNOMORSEP.Text = .NOMORSEP
                txtMEMO.Text = .MEMO
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDPENDAFTARAN.Text = String.Empty Then
                txtKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                txtKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            'If txtNOMORSEP.Text = String.Empty Then
            '    txtNOMORSEP.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtNOMORSEP.ErrorText = Statement.ErrorRequired

            '    txtNOMORSEP.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtSEQ.Text = String.Empty Then
                txtSEQ.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSEQ.ErrorText = Statement.ErrorRequired

                txtSEQ.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKDPENDAFTARAN.Text = txtSEQ.Text Then
                MsgBox("No Register Tidak Boleh Sama dengan No register gabung", MsgBoxStyle.Exclamation, Me.Text)

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
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPendaftaranGabung.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPendaftaranGabung.GetData(sNoId, sSEQ).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text
                .SEQ = txtSEQ.Text
                .NOMORSEP = txtNOMORSEP.Text
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPendaftaranGabung.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPendaftaranGabung.UpdateData(ds)
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
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(txtCARI.Text.ToString.Trim, cboCARI.SelectedIndex)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub txtCARIUTAMA_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARIUTAMA.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If txtCARIUTAMA.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN2(txtCARIUTAMA.Text.ToString.Trim, cboTypeGabung.SelectedIndex)
            txtCARIUTAMA.ResetText()
        End If
    End Sub
    Private Sub grdSEQ_EditValueChanged(sender As Object, e As EventArgs) Handles grdSEQ.EditValueChanged
        If isLoad = True Then
            If grdSEQ.Text <> "" Then
                txtSEQ.Text = grdSEQ.EditValue
            End If
        End If
    End Sub
    Private Sub grdNoRegisterGabung_EditValueChanged(sender As Object, e As EventArgs) Handles grdNoRegisterGabung.EditValueChanged
        If isLoad = True Then

            If grdNoRegisterGabung.Text <> "" Then
                txtKDPENDAFTARAN.Text = grdNoRegisterGabung.EditValue
                Dim oPendaftaran As New Admission.clsPendaftaran
                Dim dsPendaftaran = oPendaftaran.GetData(txtKDPENDAFTARAN.Text)
                If dsPendaftaran IsNot Nothing Then
                    txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
                End If
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDKUNJUNGAN(ByVal sParameter As String, ByVal sCari As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",NORM = A.KDCUSTOMER "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            'SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            'SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER LIKE '%" & sParameter & "%' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE D.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            End If

            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdSEQ.Properties.DataSource = ds.Tables("ALL")
            grdSEQ.Properties.ValueMember = "KDPENDAFTARAN"
            grdSEQ.Properties.DisplayMember = "NAME_DISPLAY"

            grdSEQ.ShowPopup()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDKUNJUNGAN2(ByVal sParameter As String, ByVal sCari As Integer)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",A.NOMORSEP "
            SQL &= ",NORM = A.KDCUSTOMER "
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            'SQL &= ",PULANG = ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN AND CARAPULANG <> 5), ISNULL((SELECT CONVERT(BIT, 1) FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN_AWAL = KDPENDAFTARAN AND CARAPULANG <> 5), CONVERT(BIT, 0)))  "
            'SQL &= ",RANAP = (SELECT CASE WHEN KDPENDAFTARAN_AWAL <> '' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER LIKE '%" & sParameter & "%' "
            ElseIf sCari = 1 Then
                SQL &= "WHERE D.NAME_DISPLAY LIKE '%" & sParameter & "%' "
            Else
                SQL &= "WHERE A.KDPENDAFTARAN LIKE '%" & sParameter & "%' "
            End If

            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grdNoRegisterGabung.Properties.DataSource = ds.Tables("ALL")
            grdNoRegisterGabung.Properties.ValueMember = "KDPENDAFTARAN"
            grdNoRegisterGabung.Properties.DisplayMember = "NAME_DISPLAY"

            grdNoRegisterGabung.ShowPopup()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
End Class