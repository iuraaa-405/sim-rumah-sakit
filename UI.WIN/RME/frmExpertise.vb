Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmExpertise
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sSeq As Integer
    Private sKDDOCTOR As String
    Private sKDITEM As String
    Private isLoad As Boolean = False
    Private oExpertise As New Grouper.clsExpertise
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, ByVal Seq As Integer, ByVal KDDOCTOR As String, ByVal KDITEM As String)
        oFormMode = FormMode
        sNoId = NoId
        sSeq = Seq
        sKDDOCTOR = KDDOCTOR
        sKDITEM = KDITEM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Expertise"

            'lMEMO.Text = Expertise.MEMO & " *"
            'chkISACTIVE.Text = Expertise.ISACTIVE
            'chkISDEFAULT.Text = Expertise.ISDEFAULT

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtHASIL.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_Dokter()

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
        txtHASIL.Properties.ReadOnly = Status
        txtKESAN.Properties.ReadOnly = Status
        txtNOMORFOTO.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status
        grdKDDOCTOR_PENANGGUNGJAWAB.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        txtNOMORFOTO.ResetText()
        txtHASIL.ResetText()
        txtKESAN.ResetText()

        Dim oDoctor As New Reference.clsDoctor
        Dim dsDoctor = oDoctor.GetDataByStatus("RADIOLOGI")
        If dsDoctor IsNot Nothing Then
            grdKDDOCTOR_PENANGGUNGJAWAB.Text = dsDoctor.KDDOCTOR
            grdKDDOCTOR.Text = dsDoctor.KDDOCTOR
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oExpertise.GetData(sNoId, sSeq)

            With ds
                deDATE.DateTime = .DATE
                grdKDDOCTOR_PENANGGUNGJAWAB.Text = .KDDOCTOR_PENANGGUNGJAWAB
                grdKDDOCTOR.Text = .KDDOCTOR
                txtNOMORFOTO.Text = .NOMORFOTO
                txtHASIL.Text = .HASIL
                txtKESAN.Text = .KESAN
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtHASIL.Text = String.Empty Then
                txtHASIL.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtHASIL.ErrorText = Statement.ErrorRequired

                txtHASIL.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKESAN.Text = String.Empty Then
                txtKESAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKESAN.ErrorText = Statement.ErrorRequired

                txtKESAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtNOMORFOTO.Text = String.Empty Then
                txtNOMORFOTO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNOMORFOTO.ErrorText = Statement.ErrorRequired

                txtNOMORFOTO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR_PENANGGUNGJAWAB.Text = String.Empty Then
                grdKDDOCTOR_PENANGGUNGJAWAB.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR_PENANGGUNGJAWAB.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR_PENANGGUNGJAWAB.Focus()
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oExpertise.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oExpertise.GetData(sNoId, sSeq).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDITEM = sKDITEM
                .KDSOTRANSAKSI = sNoId
                .SEQ = sSeq
                .KDDOCTOR_PENANGGUNGJAWAB = grdKDDOCTOR_PENANGGUNGJAWAB.EditValue
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .NOMORFOTO = txtNOMORFOTO.Text
                .HASIL = txtHASIL.Text
                .KESAN = txtKESAN.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oExpertise.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oExpertise.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If sFind2 <> "" Then
                Dim oDigitalOrder As New Digital.clsR_Order
                oDigitalOrder.UpdateStatus(sFind2, "HASIL")
            End If

            If fn_Save = True Then
                fn_Cetak(ds.KDSOTRANSAKSI, ds.SEQ)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_Cetak(ByVal KDSO As String, ByVal sSEQ As Integer)
        Try
            Dim oExpertise As New Grouper.clsExpertise
            Dim oRME As New RME.clsRME

            Dim ds = oExpertise.GetData(KDSO, sSEQ)

            If ds IsNot Nothing Then
                sUSIA = oRME.GetUmurPasien(ds.DATE, ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)

                Dim rpt As New xtraExpertise

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                MsgBox("Expertise Belum di Input", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Cetak Expertise" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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
    Private Sub fn_Dokter()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_PENANGGUNGJAWAB.Properties.DisplayMember = "NAME_DISPLAY"

            Dim ds = oDoctor.GetDataByName("USEP, DR, SP.RAD")
            If ds IsNot Nothing Then
                grdKDDOCTOR_PENANGGUNGJAWAB.Text = ds.KDDOCTOR
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class