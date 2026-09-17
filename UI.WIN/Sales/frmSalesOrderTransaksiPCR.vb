Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmSalesOrderTransaksiPCR
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oSalesOrderPCR As New Sales.clsSalesOrderTransaksi
    Private sKDSOTRANSAKSI As String = String.Empty
    Private sKDITEM As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDSOTRANSAKSI As String, ByVal KDITEM As String)
        oFormMode = FormMode
        sKDSOTRANSAKSI = KDSOTRANSAKSI
        sKDITEM = KDITEM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Antigen dan PCR"
            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCATATAN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDITEM()
        fn_LoadKDDOCTOR_LAB()
        fn_LoadKDDOCTOR_PENGIRIM()
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

        grdKDITEM.Properties.ReadOnly = True
        txtNOSAMPLE.Properties.ReadOnly = Status
        deDATE_PENGAMBILSWAB.Properties.ReadOnly = Status
        deDATE_TERIMASAMPLE.Properties.ReadOnly = Status
        deDATE_PEMERIKSAAN.Properties.ReadOnly = Status
        deDATE_KELUARHASIL.Properties.ReadOnly = Status
        grdKDDOCTOR_LAB.Properties.ReadOnly = Status
        grdKDDOCTOR_PENGIRIM.Properties.ReadOnly = Status
        cboJENISSPESIMEN.Properties.ReadOnly = Status
        cboMETODEPEMERIKSAAN.Properties.ReadOnly = Status
        cboHASIL.Properties.ReadOnly = Status
        txtHASIL_01.Properties.ReadOnly = Status
        txtHASIL_02.Properties.ReadOnly = Status
        txtHASIL_03.Properties.ReadOnly = Status
        txtHASIL_04.Properties.ReadOnly = Status
        cboNILAIRUJUKANCTVALUE.Properties.ReadOnly = Status
        txtNILAIRUJUKANVALUE_1.Properties.ReadOnly = Status
        txtNILAIRUJUKANVALUE_2.Properties.ReadOnly = Status
        txtNILAIRUJUKANVALUE_3.Properties.ReadOnly = Status
        txtNILAIRUJUKANVALUE_4.Properties.ReadOnly = Status
        txtNILAIRUJUKANVALUE_5.Properties.ReadOnly = Status
        txtCATATAN.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grdKDITEM.Text = sKDITEM
        txtNOSAMPLE.ResetText()
        deDATE_PENGAMBILSWAB.DateTime = Now
        deDATE_TERIMASAMPLE.DateTime = Now
        deDATE_PEMERIKSAAN.DateTime = Now
        deDATE_KELUARHASIL.DateTime = Now
        grdKDDOCTOR_LAB.Text = "087"
        grdKDDOCTOR_PENGIRIM.ResetText()
        grdKDDEPARTMENT.ResetText()
        txtCATATAN.ResetText()

        Dim ds = oSalesOrderPCR.GetData(sKDSOTRANSAKSI)
        If ds IsNot Nothing Then
            grdKDDOCTOR_PENGIRIM.Text = ds.KDDOCTOR
            grdKDDEPARTMENT.Text = ds.S_PENDAFTARAN_KUNJUNGAN.KDDEPARTMENT
        End If
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oSalesOrderPCR.GetDataPCR(sKDSOTRANSAKSI, sKDITEM)

            With ds
                grdKDITEM.Text = .KDITEM
                txtNOSAMPLE.Text = .NOSAMPLE
                deDATE_PENGAMBILSWAB.DateTime = .DATE_PENGAMBILSWAB
                deDATE_TERIMASAMPLE.DateTime = .DATE_TERIMASAMPLE
                deDATE_PEMERIKSAAN.DateTime = .DATE_PEMERIKSAAN
                deDATE_KELUARHASIL.DateTime = .DATE_KELUARHASIL
                grdKDDEPARTMENT.Text = .KDDEPARTMENT
                grdKDDOCTOR_LAB.Text = .KDDOCTOR_LAB
                grdKDDOCTOR_PENGIRIM.Text = .KDDOCTOR_PENGIRIM
                cboJENISSPESIMEN.Text = .JENISSPESIMEN
                cboMETODEPEMERIKSAAN.Text = .METODEPEMERIKSAAN
                cboHASIL.Text = .HASIL
                txtHASIL_01.Text = .HASIL_TEXT1
                txtHASIL_02.Text = .HASIL_TEXT2
                txtHASIL_03.Text = .HASIL_TEXT3
                txtHASIL_04.Text = .HASIL_TEXT4
                cboNILAIRUJUKANCTVALUE.Text = .NILAIRUJUKANCTVALUE
                txtNILAIRUJUKANVALUE_1.Text = .NILAIRUJUKANCTVALUE_TEXT1
                txtNILAIRUJUKANVALUE_2.Text = .NILAIRUJUKANCTVALUE_TEXT2
                txtNILAIRUJUKANVALUE_3.Text = .NILAIRUJUKANCTVALUE_TEXT3
                txtNILAIRUJUKANVALUE_4.Text = .NILAIRUJUKANCTVALUE_TEXT4
                txtNILAIRUJUKANVALUE_5.Text = .NILAIRUJUKANCTVALUE_TEXT5
                txtCATATAN.Text = .CATATAN
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDITEM.Text = String.Empty Then
                grdKDITEM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDITEM.ErrorText = Statement.ErrorRequired

                grdKDITEM.Focus()
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
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oSalesOrderPCR.GetStructureHeaderPCR
            With ds
                Try
                    .DATECREATED = oSalesOrderPCR.GetDataPCR(sKDSOTRANSAKSI, sKDITEM).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = sKDSOTRANSAKSI
                .KDITEM = grdKDITEM.EditValue
                .NOSAMPLE = txtNOSAMPLE.Text
                .DATE_PENGAMBILSWAB = deDATE_PENGAMBILSWAB.DateTime
                .DATE_TERIMASAMPLE = deDATE_TERIMASAMPLE.DateTime
                .DATE_PEMERIKSAAN = deDATE_PEMERIKSAAN.DateTime
                .DATE_KELUARHASIL = deDATE_KELUARHASIL.DateTime
                .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
                .KDDOCTOR_LAB = grdKDDOCTOR_LAB.EditValue
                .KDDOCTOR_PENGIRIM = grdKDDOCTOR_PENGIRIM.EditValue
                .JENISSPESIMEN = cboJENISSPESIMEN.Text
                .METODEPEMERIKSAAN = cboMETODEPEMERIKSAAN.Text
                .HASIL = cboHASIL.Text
                .HASIL_TEXT1 = txtHASIL_01.Text
                .HASIL_TEXT2 = txtHASIL_02.Text
                .HASIL_TEXT3 = txtHASIL_03.Text
                .HASIL_TEXT4 = txtHASIL_04.Text
                .NILAIRUJUKANCTVALUE = cboNILAIRUJUKANCTVALUE.Text
                .NILAIRUJUKANCTVALUE_TEXT1 = txtNILAIRUJUKANVALUE_1.Text
                .NILAIRUJUKANCTVALUE_TEXT2 = txtNILAIRUJUKANVALUE_2.Text
                .NILAIRUJUKANCTVALUE_TEXT3 = txtNILAIRUJUKANVALUE_3.Text
                .NILAIRUJUKANCTVALUE_TEXT4 = txtNILAIRUJUKANVALUE_4.Text
                .NILAIRUJUKANCTVALUE_TEXT5 = txtNILAIRUJUKANVALUE_5.Text
                .CATATAN = txtCATATAN.Text
                Try
                    .ISAPPROVAL = oSalesOrderPCR.GetDataPCR(sKDSOTRANSAKSI, sKDITEM).ISAPPROVAL
                Catch ex As Exception
                    .ISAPPROVAL = False
                End Try
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSalesOrderPCR.InsertDataPCR(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSalesOrderPCR.UpdateDataPCR(ds)
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
    Private Sub fn_LoadKDITEM()
        Dim oITEM As New Reference.clsItem
        Try
            grdKDITEM.Properties.DataSource = oITEM.GetData.Where(Function(x) x.NMITEM1 = "SWAB").ToList()
            grdKDITEM.Properties.ValueMember = "KDITEM"
            grdKDITEM.Properties.DisplayMember = "NMITEM2"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDEPARTMENT()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR_LAB()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR_LAB.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_LAB.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_LAB.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR_PENGIRIM()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR_PENGIRIM.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_PENGIRIM.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_PENGIRIM.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class