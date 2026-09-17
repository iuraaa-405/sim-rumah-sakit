Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmSKD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oSKD As New Admission.clsSKD
    Private PopUP As Boolean = False

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
            Me.Text = SKD.TITLE

            lKDSKD.Text = SKD.KDSKD
            lKDPENDAFATRAN.Text = SKD.KDPENDAFTARAN & " *"
            lDATE.Text = SKD.KDPENDAFTARAN
            lDATE_KONTROL.Text = SKD.KDPENDAFTARAN
            lISCATEGORY.Text = SKD.ISCATEGORY
            lKDDEPARTMENT.Text = SKD.KDDEPARTMENT
            lKDDOCTOR.Text = SKD.KDDOCTOR
            lNOMORRUJUKAN.Text = SKD.NOMORRUJUKAN
            lDESCRIPTION.Text = SKD.DESCRIPTION
            lALASAN.Text = SKD.ALASAN
            lTINDAKLANJUT.Text = SKD.TINDAKLANJUT

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

        grdKDPENDAFTARAN.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        deDATEKONTROL.Properties.ReadOnly = Status
        rbCATEGORY.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        txtNOMORRUJKAN.Properties.ReadOnly = Status
        txtDESCRIPTION.Properties.ReadOnly = Status
        txtALASAN.Properties.ReadOnly = Status
        txtTINDAKLANJUT.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"

        grdKDPENDAFTARAN.ResetText()
        deDATE.DateTime = Now
        deDATEKONTROL.DateTime = Now
        grdKDDEPARTMENT.ResetText()
        grdKDDOCTOR.ResetText()
        txtDESCRIPTION.ResetText()
        txtALASAN.ResetText()
        txtTINDAKLANJUT.ResetText()

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSKD.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                cboCARI.SelectedIndex = 2
                fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
                grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
                rbCATEGORY.SelectedIndex = .ISCATEGORY
                txtDESCRIPTION.Text = .DESCRIPTION
                deDATE.DateTime = .DATE
                deDATEKONTROL.DateTime = .DATEKONTROL
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                fn_LoadKDDOCTOR()
                grdKDDOCTOR.Text = .KDDOCTOR
                txtNOMORRUJKAN.Text = .NOMORRUJUKAN
                txtALASAN.Text = .ALASAN
                txtTINDAKLANJUT.Text = .TINDAKLANJUT

            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtALASAN.Text = String.Empty Then
                txtALASAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtALASAN.ErrorText = Statement.ErrorRequired

                txtALASAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTINDAKLANJUT.Text = String.Empty Then
                txtTINDAKLANJUT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTINDAKLANJUT.ErrorText = Statement.ErrorRequired

                txtTINDAKLANJUT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDPENDAFTARAN.Text = String.Empty Then
                grdKDPENDAFTARAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDPENDAFTARAN.ErrorText = Statement.ErrorRequired

                grdKDPENDAFTARAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If deDATE.DateTime.ToString("yyyyMMdd") = deDATEKONTROL.DateTime.ToString("yyyyMMdd") Then
                deDATEKONTROL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                deDATEKONTROL.ErrorText = Statement.ErrorRequired

                deDATEKONTROL.Focus()
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
            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDSKD = sNoId
                .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
                .DATE = deDATE.DateTime
                .ISCATEGORY = rbCATEGORY.SelectedIndex
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORRUJUKAN = txtNOMORRUJKAN.Text.Trim.ToUpper
                .DESCRIPTION = txtDESCRIPTION.Text.Trim.ToUpper
                Try
                    .ISCHEKED = oSKD.GetData(sNoId).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATEKONTROL.DateTime
                .ALASAN = txtALASAN.Text.Trim.ToUpper
                .TINDAKLANJUT = txtTINDAKLANJUT.Text.Trim.ToUpper

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSKD.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSKD.UpdateData(ds)
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
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor

        If rbCATEGORY.SelectedIndex = 0 Then
            Try
                Dim dsDoctor = From x In oDOCTOR.GetDataDetail_DEPARMENT
                               Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DOCTOR.VCLAIM_KDDPJP <> "" And x.M_DEPARTMENT.ISRUANGRAWAT = False
                               Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

                grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                Dim dsDoctor = From x In oDOCTOR.GetDataDetail_DEPARMENT
                               Where x.KDDEPARTMENT = grdKDDEPARTMENT.EditValue And x.M_DEPARTMENT.ISRUANGRAWAT = True
                               Select x.KDDOCTOR, x.M_DOCTOR.NAME_DISPLAY

                grdKDDOCTOR.Properties.DataSource = dsDoctor.ToList()
                grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
                grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub txtCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            PopUP = True
            fn_LoadKDPENDAFTARAN(txtCARI.Text)
            txtCARI.ResetText()
        End If
    End Sub
    Private Sub fn_LoadKDPENDAFTARAN(ByVal Parameter As String)
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            Dim oPendaftaran As New Admission.clsPendaftaran

            Dim dsPendaftaran = (From x In oPendaftaran.GetDataBySKD(Parameter, cboCARI.SelectedIndex)
                                 Select x.KDPENDAFTARAN, x.CATEGORY, x.KDCUSTOMER, x.M_CUSTOMER.NAME_DISPLAY, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY, x.DATE).OrderByDescending(Function(x) x.DATE)

            grdKDPENDAFTARAN.Properties.DataSource = dsPendaftaran.Where(Function(x) x.CATEGORY = rbCATEGORY.SelectedIndex).ToList()
            grdKDPENDAFTARAN.Properties.ValueMember = "KDPENDAFTARAN"
            grdKDPENDAFTARAN.Properties.DisplayMember = "KDPENDAFTARAN"

            If PopUP = True Then
                grdKDPENDAFTARAN.ShowPopup()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDDEPARTMENT_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDDEPARTMENT.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If grdKDDEPARTMENT.Text = String.Empty Then Exit Sub
            fn_LoadKDDOCTOR()
            grdKDDOCTOR.ShowPopup()
        End If
    End Sub
    Private Sub grdKDPENDAFTARAN_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdKDPENDAFTARAN.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(grdKDPENDAFTARAN.EditValue)
            If dsPendaftaran IsNot Nothing Then
                deDATE.DateTime = dsPendaftaran.DATE
                rbCATEGORY.SelectedIndex = dsPendaftaran.CATEGORY
                txtNOMORRUJKAN.Text = dsPendaftaran.NOMORRUJUKAN
                grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
                'grdKDDOCTOR.ShowPopup()
            End If
        End If
    End Sub
#End Region
End Class