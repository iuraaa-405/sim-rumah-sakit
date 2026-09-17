Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDeposit
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDeposit As New Finance.clsDeposit
    Private sPopUP As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Deposit.TITLE

            lNAMAPASIEN.Text = Customer.TITLE
            lKDDEPARTMENT.Text = Department.TITLE
            lKDDOCTOR.Text = Doctor.TITLE

            lKDPENDAFTARAN.Text = Deposit.KDPENDAFTARAN
            lKDDEPOSIT.Text = Deposit.KDDEPOSIT
            lDATE.Text = Deposit.TANGGAL

            tab3.Text = Deposit.TAB_MEMO

            lTOTAL.Text = Deposit.TOTAL

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDDeposit.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
        fn_LoadKDDEPARTMENT()

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
        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status

        txtTOTAL.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtKDDeposit.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtMEMO.ResetText()
        txtTOTAL.ResetText()
        grdKDPENDAFTARAN.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDeposit.GetData(sNoId)

            With ds
                txtKDDEPOSIT.Text = .KDDEPOSIT

                fn_LoadKDKUNJUNGAN(.KDPENDAFTARAN, 3)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN

                Dim oKunjungan As New Admission.clsPendaftaran
                Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
                If dsKunjungan IsNot Nothing Then
                    txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                    grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                    grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
                Else
                    txtNAMAPASIEN.ResetText()
                    grdKDDEPARTMENT_H.ResetText()
                    grdKDDOCTOR_H.ResetText()
                End If

                deDATE.DateTime = .DATE
                txtMEMO.Text = .MEMO

                txtTOTAL.Text = .TOTAL
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
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
            Dim ds = oDeposit.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDeposit.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .KDDEPOSIT = sNoId
                .DATE = deDATE.DateTime
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .TOTAL = CDec(txtTOTAL.Text)
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDeposit.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDeposit.UpdateData(ds)
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
    Private Sub frmDeposit_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
            sPopUP = True
            If txtCARI.Text = String.Empty Then Exit Sub
            fn_LoadKDKUNJUNGAN(IIf(cboCARI.SelectedIndex = 0, txtCARI.Text.ToString.Trim.PadLeft(9, "0"), txtCARI.Text.ToString.Trim), cboCARI.SelectedIndex)
            txtCARI.ResetText()
        End If
    End Sub
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
            SQL &= ",D.NAME_DISPLAY "
            SQL &= ",A.DATE "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DPJP = C.NAME_DISPLAY "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            If sCari = 0 Then
                SQL &= "WHERE A.KDCUSTOMER = '" & sParameter & "' "
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

            grdKDPENDAFTARAN.Properties.DataSource = ds.Tables("ALL")
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If sPopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDKUNJUNGAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oKunjungan As New Admission.clsPendaftaran
            Dim dsKunjungan = oKunjungan.GetData(grdKDPENDAFTARAN.EditValue)
            If dsKunjungan IsNot Nothing Then
                txtNAMAPASIEN.Text = dsKunjungan.M_CUSTOMER.NAME_DISPLAY & " / " & dsKunjungan.KDCUSTOMER
                grdKDDEPARTMENT_H.Text = dsKunjungan.KDDEPARTMENT
                grdKDDOCTOR_H.Text = dsKunjungan.KDDOCTOR
            Else
                txtNAMAPASIEN.ResetText()
                grdKDDEPARTMENT_H.ResetText()
                grdKDDOCTOR_H.ResetText()
            End If

        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            Dim ds = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDOCTOR_H.Properties.DataSource = ds
            grdKDDOCTOR_H.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_H.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            Dim ds = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()

            grdKDDEPARTMENT_H.Properties.DataSource = ds
            grdKDDEPARTMENT_H.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT_H.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class