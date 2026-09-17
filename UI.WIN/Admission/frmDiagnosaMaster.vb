Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq

Public Class frmDiagnosaMaster
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDiagnosaMaster As New Admission.clsDiagnosaMaster
    Private PopUP As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal Register As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        Dim dsPendaftaran = oDiagnosaMaster.GetDataByKD(Register)
        If dsPendaftaran IsNot Nothing Then
            txtKDPENDAFTARAN.Text = dsPendaftaran.KDPENDAFTARAN
            txtNAMAPASIEN.Text = dsPendaftaran.M_CUSTOMER.NAME_DISPLAY
            txtNORM.Text = dsPendaftaran.KDCUSTOMER
            txtNOMORSEP.Text = dsPendaftaran.NOMORSEP
            fn_LoadKDDEPARTMENT()
            fn_LoadKDDOCTOR()
            grdKDDEPARTMENT.Text = dsPendaftaran.KDDEPARTMENT
            grdKDDOCTOR.Text = dsPendaftaran.KDDOCTOR
            deDATE_MASUK.DateTime = dsPendaftaran.DATE
            txtKDDIAGNOSAAWAL.Text = dsPendaftaran.M_DIAGNOSA.MEMO
            grdKDDAFTAR_L4.Text = dsPendaftaran.KDDAFTAR_L4
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = DiagnosaMaster.TITLE

            lNAMAPASIEN.Text = Pendaftaran.KDCUSTOMER_NAMAPASIEN
            lNORM.Text = Pendaftaran.KDCUSTOMER
            lKDDEPARTMENT.Text = Pendaftaran.KDDEPARTMENT
            lKDDOCTOR.Text = Pendaftaran.KDDOCTOR
            lNOMORSEP.Text = Pendaftaran.NOMORSEP
            lKDDIAGNOSAAWAL.Text = Pendaftaran.KDDIAGNOSA

            lKDDIAGNOSAMASTER.Text = DiagnosaMaster.KDKDDIAGNOSAMASTER
            lKDPENDAFTARAN.Text = DiagnosaMaster.KDPENDAFTARAN
            lNOMORSEP.Text = DiagnosaMaster.NOMORSEP
            lDATE_MASUK.Text = DiagnosaMaster.DATE_MASUK
            lKDDEPARTMENT.Text = DiagnosaMaster.KDDEPARTMENT
            lKDDIAGNOSA1.Text = DiagnosaMaster.KDDIAGNOSA1
            lKDDIAGNOSA2.Text = DiagnosaMaster.KDDIAGNOSA2
            lKDDIAGNOSA3.Text = DiagnosaMaster.KDDIAGNOSA3
            lKDDIAGNOSA4.Text = DiagnosaMaster.KDDIAGNOSA4
            lKDDIAGNOSA5.Text = DiagnosaMaster.KDDIAGNOSA5
            lKDDIAGNOSA6.Text = DiagnosaMaster.KDDIAGNOSA6
            lKDPROSEDURE1.Text = DiagnosaMaster.KDPROCEDURE1
            lKDPROSEDURE2.Text = DiagnosaMaster.KDPROCEDURE2
            lKDPROSEDURE3.Text = DiagnosaMaster.KDPROCEDURE3
            lKDPROSEDURE4.Text = DiagnosaMaster.KDPROCEDURE4
            lKDPROSEDURE5.Text = DiagnosaMaster.KDPROCEDURE5
            lKDPROSEDURE6.Text = DiagnosaMaster.KDPROCEDURE6
            lKDDOCTOR.Text = DiagnosaMaster.KDDOCTOR

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
        fn_LoadKDDIAGNOSA()
        fn_LoadKDPROCEDURE()
        fn_LoadKDDOCTOR()
        fn_LoadDaftar4()

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

        grdKDDIAGNOSA1.Properties.ReadOnly = Status
        grdKDDIAGNOSA2.Properties.ReadOnly = Status
        grdKDDIAGNOSA3.Properties.ReadOnly = Status
        grdKDDIAGNOSA4.Properties.ReadOnly = Status
        grdKDDIAGNOSA5.Properties.ReadOnly = Status
        grdKDDIAGNOSA6.Properties.ReadOnly = Status
        grdKDPROCEDURE1.Properties.ReadOnly = Status
        grdKDPROCEDURE2.Properties.ReadOnly = Status
        grdKDPROCEDURE3.Properties.ReadOnly = Status
        grdKDPROCEDURE4.Properties.ReadOnly = Status
        grdKDPROCEDURE5.Properties.ReadOnly = Status
        grdKDPROCEDURE6.Properties.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        grdKDDIAGNOSA1.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA2.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA3.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA4.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA5.Text = oDiagnosaMaster.DIAGNOSA_Default
        grdKDDIAGNOSA6.Text = oDiagnosaMaster.DIAGNOSA_Default

        grdKDPROCEDURE1.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE2.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE3.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE4.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE5.Text = oDiagnosaMaster.PROSEDUR_Default
        grdKDPROCEDURE6.Text = oDiagnosaMaster.PROSEDUR_Default

    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oDiagnosaMaster.GetData(sNoId)

            With ds
                txtCODE.Text = sNoId
                txtKDPENDAFTARAN.Text = .KDPENDAFTARAN
                grdKDDIAGNOSA1.Text = .KDDIAGNOSA1
                grdKDDIAGNOSA2.Text = .KDDIAGNOSA2
                grdKDDIAGNOSA3.Text = .KDDIAGNOSA3
                grdKDDIAGNOSA4.Text = .KDDIAGNOSA4
                grdKDDIAGNOSA5.Text = .KDDIAGNOSA5
                grdKDDIAGNOSA6.Text = .KDDIAGNOSA6
                grdKDPROCEDURE1.Text = .KDPROCEDURE1
                grdKDPROCEDURE2.Text = .KDPROCEDURE2
                grdKDPROCEDURE3.Text = .KDPROCEDURE3
                grdKDPROCEDURE4.Text = .KDPROCEDURE4
                grdKDPROCEDURE5.Text = .KDPROCEDURE5
                grdKDPROCEDURE6.Text = .KDPROCEDURE6

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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oDiagnosaMaster.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDiagnosaMaster.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDIAGNOSAMASTER = sNoId
                .KDPENDAFTARAN = txtKDPENDAFTARAN.Text.ToString.Trim.ToUpper
                .KDDIAGNOSA1 = grdKDDIAGNOSA1.EditValue
                .KDDIAGNOSA2 = grdKDDIAGNOSA2.EditValue
                .KDDIAGNOSA3 = grdKDDIAGNOSA3.EditValue
                .KDDIAGNOSA4 = grdKDDIAGNOSA4.EditValue
                .KDDIAGNOSA5 = grdKDDIAGNOSA5.EditValue
                .KDDIAGNOSA6 = grdKDDIAGNOSA6.EditValue
                .KDPROCEDURE1 = grdKDPROCEDURE1.EditValue
                .KDPROCEDURE2 = grdKDPROCEDURE2.EditValue
                .KDPROCEDURE3 = grdKDPROCEDURE3.EditValue
                .KDPROCEDURE4 = grdKDPROCEDURE4.EditValue
                .KDPROCEDURE5 = grdKDPROCEDURE5.EditValue
                .KDPROCEDURE6 = grdKDPROCEDURE6.EditValue
                .KDUSER = sUserID
                .DESCRIPTION = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtCODE.Text = oDiagnosaMaster.InsertData(ds)
                    If txtCODE.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                        oDiagnosaMaster.UpdateKDDAFTAR_L4(txtKDPENDAFTARAN.Text, grdKDDAFTAR_L4.EditValue)
                        CetakRegister(txtCODE.Text)
                    End If

                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDiagnosaMaster.UpdateData(ds)
                    oDiagnosaMaster.UpdateKDDAFTAR_L4(txtKDPENDAFTARAN.Text, grdKDDAFTAR_L4.EditValue)
                    CetakRegister(txtCODE.Text)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub CetakRegister(ByVal KDDiagnosaMaster As String)
        'Dim rpt As New xtraDiagnosaMaster

        'Dim ds = oDiagnosaMaster.GetData(KDDiagnosaMaster)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
    Private Function fn_CariSEP(ByVal NOMORSEP As String) As Boolean
        Try
            If NOMORSEP = String.Empty Then
                fn_CariSEP = False
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariSEP(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, NOMORSEP)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariSEP = True
                        'Dim DataDecrypt = JObject.Parse(oSetKoneksi.Decrypt(allData("response"), dsDataSetKoneksi.CONSID & dsDataSetKoneksi.SECREATKEY & uTime))
                    Else
                        fn_CariSEP = False
                    End If
                Else
                    fn_CariSEP = False
                End If
            Else
                fn_CariSEP = False
            End If
        Catch oErr As Exception
            fn_CariSEP = False
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
    Private Sub btnMaster_Click() Handles btnMaster.ItemClick
        sFind1 = ""

        frmBrowsePaymentMasterDiagnosa.ShowDialog(Me)

        Dim oData As New Reference.clsDiagnosaMasterPrice
        Dim dsData = oData.GetData(sFind1)
        If dsData IsNot Nothing Then
            grdKDDIAGNOSA1.Text = dsData.KDDIAGNOSA1
            grdKDDIAGNOSA2.Text = dsData.KDDIAGNOSA2
            grdKDDIAGNOSA3.Text = dsData.KDDIAGNOSA3
            grdKDDIAGNOSA4.Text = dsData.KDDIAGNOSA4
            grdKDDIAGNOSA5.Text = dsData.KDDIAGNOSA5
            grdKDDIAGNOSA6.Text = dsData.KDDIAGNOSA6
        End If
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
    Private Sub fn_LoadDaftar4()
        Dim oDAFTAR_L4 As New Reference.clsDaftar_L4
        Try
            grdKDDAFTAR_L4.Properties.DataSource = oDAFTAR_L4.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDAFTAR_L4.Properties.ValueMember = "KDDAFTAR_L4"
            grdKDDAFTAR_L4.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDEPARTMENT As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDEPARTMENT.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Dim oDOCTOR As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDOCTOR.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDIAGNOSA()
        Dim oDIAGNOSA As New Reference.clsDiagnosa

        Try
            Dim dsDiagnosa = oDIAGNOSA.GetData.Where(Function(x) x.ISACTIVE = True)

            grdKDDIAGNOSA1.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA1.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA1.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA2.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA2.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA2.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA3.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA3.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA3.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA4.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA4.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA4.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA5.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA5.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA5.Properties.DisplayMember = "MEMO"

            grdKDDIAGNOSA6.Properties.DataSource = dsDiagnosa.ToList()
            grdKDDIAGNOSA6.Properties.ValueMember = "KDDIAGNOSA"
            grdKDDIAGNOSA6.Properties.DisplayMember = "MEMO"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDPROCEDURE()
        Dim oPROCEDURE As New Reference.clsProsedur

        Try
            Dim dsDiagnosa = oPROCEDURE.GetData.Where(Function(x) x.ISACTIVE = True)

            grdKDPROCEDURE1.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE1.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE1.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE2.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE2.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE2.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE3.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE3.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE3.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE4.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE4.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE4.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE5.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE5.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE5.Properties.DisplayMember = "MEMO"

            grdKDPROCEDURE6.Properties.DataSource = dsDiagnosa.ToList()
            grdKDPROCEDURE6.Properties.ValueMember = "KDPROSEDUR"
            grdKDPROCEDURE6.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
End Class