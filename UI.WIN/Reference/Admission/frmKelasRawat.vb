Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Imports Newtonsoft.Json.Linq

Public Class frmKelasRawat
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oKelasRawat As New Reference.clsKelasRawat
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
            Me.Text = KelasRawat.TITLE

            lKODEVCLAIM.Text = KelasRawat.KODE_VCLAIM & " *"
            lKODEAPLICARE.Text = KelasRawat.KODE_APLICARE & " *"
            lMEMO.Text = KelasRawat.MEMO & " *"
            chkISACTIVE.Text = KelasRawat.ISACTIVE
            chkISDEFAULT.Text = KelasRawat.ISDEFAULT
            lKODEPENDAFTARAN.Text = KelasRawat.KODEPENDAFTARAN

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

        txtKODEVCLAIM.Properties.ReadOnly = True
        txtAPLICARE.Properties.ReadOnly = True
        txtMEMO.Properties.ReadOnly = Status
        chkISACTIVE.Properties.ReadOnly = Status
        chkISDEFAULT.Properties.ReadOnly = Status
        cboKODEPENDAFTARAN.Properties.ReadOnly = Status
        cboKELOMPOK.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtMEMO.ResetText()
        chkISACTIVE.Checked = True
        chkISDEFAULT.Checked = False
        cboKODEPENDAFTARAN.SelectedIndex = 0
        cboKELOMPOK.SelectedIndex = 0
        txtKODEVCLAIM.Text = "-"
        txtAPLICARE.Text = "-"
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oKelasRawat.GetData(sNoId)

            With ds
                txtKODEVCLAIM.Text = .KODE_VCLAIM
                txtAPLICARE.Text = .KODE_APLICARE
                txtMEMO.Text = .MEMO
                chkISACTIVE.Checked = .ISACTIVE
                chkISDEFAULT.Checked = .ISDEFAULT
                cboKODEPENDAFTARAN.SelectedIndex = .KODEPENDAFTARAN
                cboKELOMPOK.Text = .KELOMPOKKELAS
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtMEMO.Text = String.Empty Then
                txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtMEMO.ErrorText = Statement.ErrorRequired

                txtMEMO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKODEVCLAIM.Text = String.Empty Then
                txtKODEVCLAIM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKODEVCLAIM.ErrorText = Statement.ErrorRequired

                txtKODEVCLAIM.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtAPLICARE.Text = String.Empty Then
                txtAPLICARE.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtAPLICARE.ErrorText = Statement.ErrorRequired

                txtAPLICARE.Focus()
                fn_Validate = False
                Exit Function
            End If
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                If oKelasRawat.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtMEMO.ErrorText = Statement.ErrorRegistered

                    txtMEMO.Focus()
                    fn_Validate = False
                    Exit Function
                End If
            Else
                If txtMEMO.Text.Trim.ToUpper <> oKelasRawat.GetData(sNoId).MEMO Then
                    If oKelasRawat.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
                        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                        txtMEMO.ErrorText = Statement.ErrorRegistered

                        txtMEMO.Focus()
                        fn_Validate = False
                        Exit Function
                    End If
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oKelasRawat.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oKelasRawat.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDKELASRAWAT = sNoId
                .KODE_VCLAIM = txtKODEVCLAIM.Text.Trim.ToUpper
                .KODE_APLICARE = txtAPLICARE.Text.Trim.ToUpper
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .ISACTIVE = chkISACTIVE.Checked
                .ISDEFAULT = chkISDEFAULT.Checked
                .KODEPENDAFTARAN = cboKODEPENDAFTARAN.SelectedIndex
                .KELOMPOKKELAS = cboKELOMPOK.Text
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oKelasRawat.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oKelasRawat.UpdateData(ds)
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
    Private Sub btnCARI_Click(sender As Object, e As EventArgs) Handles btnCARI.Click
        Dim oSetKoneksi As New Brigging.clsSetKoneksi
        If chkAplicare.Checked = False Then
            Try
                Dim uTime As Integer = 0

                If sVclaim_ConsId <> "" Then
                    uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                    Dim dsSetKoneksi = oSetKoneksi.GetDataVClaimReferensiKelasRawat(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime)

                    If dsSetKoneksi <> "" Then
                        Try
                            Dim allData = JObject.Parse(dsSetKoneksi)

                            Dim table As DataTable

                            table = New DataTable("M_TABEL")
                            table.Columns.Add("kode")
                            table.Columns.Add("nama")

                            Dim dsData = oSetKoneksi.Decrypt(allData("response"), sVclaim_ConsId & sVclaim_SecreatKey & uTime)
                            Dim ds = JObject.Parse(dsData)

                            For Each item In ds("list")
                                table.Rows.Add(New String() {item("kode"), item("nama")})
                            Next

                            grdCARI.Properties.DataSource = table
                            grdCARI.Properties.ValueMember = "kode"
                            grdCARI.Properties.DisplayMember = "nama"

                            grdCARI.ShowPopup()

                        Catch oErr As Exception
                            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                        End Try
                    End If
                Else
                    MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
                End If

            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Try
                Dim dsSetKoneksi = oSetKoneksi.GetDataAplicareReferensiKelasRawat(sAplicare_Url, sAplicare_ConsId, sAplicare_SecreatKey, sAplicare_UserKey)

                If dsSetKoneksi <> "" Then
                    Try
                        Dim allData = JObject.Parse(dsSetKoneksi)

                        Dim table As DataTable

                        table = New DataTable("M_KELAS")
                        table.Columns.Add("kode")
                        table.Columns.Add("nama")

                        For Each item In allData("response")("list")
                            table.Rows.Add(New String() {item("kodekelas"), item("kodekelas")})
                        Next

                        grdCARI.Properties.DataSource = table

                        grdCARI.Properties.ValueMember = "kode"
                        grdCARI.Properties.DisplayMember = "nama"

                        grdCARI.ShowPopup()

                    Catch oErr As Exception
                        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message & vbCrLf & vbCrLf & "Result : " & vbCrLf & dsSetKoneksi, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        End If
    End Sub
    Private Sub grdCARI_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdCARI.KeyPress
        If Asc(e.KeyChar) = 13 Then
            If chkAplicare.Checked = False Then
                txtKODEVCLAIM.Text = grvCARI.GetFocusedRowCellValue("kode")
            Else
                txtAPLICARE.Text = grvCARI.GetFocusedRowCellValue("kode")
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class