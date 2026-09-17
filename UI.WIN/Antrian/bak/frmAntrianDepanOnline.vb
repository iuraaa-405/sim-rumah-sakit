Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmAntrianDepanOnline
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oAntrianChekin As New AntrianRS.clsAntrian_Checkin
    Private sNoid As String = String.Empty
    'Private sKategori As Integer = 0
    'Private sParameterCari As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoid = NoId
        'sKategori = Kategori
        'sParameterCari = ParameterCari
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        txtSEARCH.Focus()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Antrean"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtNAMAPASIEN.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDepartment()
        fn_LoadDoctor()

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
        'btnSaveNew.Enabled = Not Status
        'btnSaveClose.Enabled = Not Status

        'grdKDDOCTOR.Properties.ReadOnly = Status
        'grdKDDEPARTMENT.Properties.ReadOnly = Status
        'txtMEMO.Properties.ReadOnly = Status

        'grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtSEARCH.ResetText()
        txtNAMAPASIEN.ResetText()
        txtNOMORHP.ResetText()
        txtNOMORNIK.ResetText()
        txtNOMORRM.ResetText()
        txtNOMORKARTU.ResetText()
        grdKDDEPARTMENT.ResetText()
        grdKDDOCTOR.ResetText()
        txtSEARCH.Focus()

        oFormMode = FORM_MODE.FORM_MODE_ADD

    End Sub
    Private Sub fn_LoadData()
        'Try
        '    ' ***** HEADER *****
        '    Dim ds = oAntrian.GetData(sNoId)

        '    With ds
        '        txtKDAntrian.Text = .KDAntrian
        '        grdKDDOCTOR.Text = .KDDOCTOR
        '        grdKDDEPARTMENT.Text = .KDDEPARTMENT
        '        txtMEMO.Text = .DESCRIPTION

        '        BindingSource.DataSource = oAntrian.GetDataDetail.Where(Function(x) x.KDAntrian = sNoId).OrderBy(Function(x) x.SEQ).ToList()
        '        grdDetail.DataSource = BindingSource
        '    End With
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtNAMAPASIEN.Text = String.Empty Then
                txtNAMAPASIEN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtNAMAPASIEN.ErrorText = Statement.ErrorRequired

                txtNAMAPASIEN.Focus()
                fn_Validate = False

                fn_EmptyMe()

                MsgBox("SILAHKAN MASUKKAN KARTU BEROBAT ANDA", MsgBoxStyle.Exclamation, Me.Text)

                Exit Function
            End If

            If grdKDDEPARTMENT.Text = String.Empty Then
                grdKDDEPARTMENT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDEPARTMENT.ErrorText = Statement.ErrorRequired

                grdKDDEPARTMENT.Focus()
                fn_Validate = False

                fn_EmptyMe()

                MsgBox("POLI BELUM DI PILIH", MsgBoxStyle.Exclamation, Me.Text)

                Exit Function
            End If

            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False

                fn_EmptyMe()

                MsgBox("DOKTER BELUM DI PILIH", MsgBoxStyle.Exclamation, Me.Text)

                Exit Function
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oAntrianChekin.GetStructureHeader
            With ds
                .DATECREATED = Now
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KODEBOOKING As String = oAntrianChekin.InsertData(ds)
                If KODEBOOKING <> "" Then
                    fn_Save = True
                    CetakAntrian(KODEBOOKING)
                Else
                    fn_Save = False
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmAntrian_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        'Select Case e.KeyCode
        '    Case Keys.F12
        '        btnClose_Click()
        '    Case Keys.F2
        '        If btnSaveNew.Enabled = True Then
        '            btnSaveNew_Click()
        '        End If
        '    Case Keys.F3
        '        If btnSaveClose.Enabled = True Then
        '            btnSaveClose_Click()
        '        End If
        'End Select
    End Sub
    Private Sub CetakAntrian(ByVal KODEBOOKING As String)
        Try
            If KODEBOOKING = String.Empty Then Exit Sub

            Dim oAntrian As New AntrianRS.clsAntrian
            Dim rpt As New xtraAntrian

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oAntrian.GetData(KODEBOOKING)
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            printTool.Print()

            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        sStatusSave = "NEW"
    '        Me.Close()
    '    End If
    'End Sub
    'Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        Me.Close()
    '    End If
    'End Sub
    'Private Sub btnClose_Click() Handles btnClose.ItemClick
    '    Me.Close()
    'End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "" And x.ISRUANGRAWAT = False).ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "KDDEPARTMENT"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KDDPJP <> "").ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub txtSEARCH_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSEARCH.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim oSKD As New Admission.clsSKD
            Dim dsSKD = oSKD.GetDataByDate(txtSEARCH.Text)
            If dsSKD IsNot Nothing Then
                txtNAMAPASIEN.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                txtNOMORHP.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.PHONE
                txtNOMORNIK.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                txtNOMORRM.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KDCUSTOMER
                txtNOMORKARTU.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KARTUBPJS
                grdKDDEPARTMENT.Text = dsSKD.KDDEPARTMENT
                grdKDDOCTOR.Text = dsSKD.KDDOCTOR
                txtNOREFERENSI.Text = dsSKD.NOMORRUJUKAN
            Else
                txtNAMAPASIEN.ResetText()
                txtNOMORHP.ResetText()
                txtNOMORNIK.ResetText()
                txtNOMORRM.ResetText()
                txtNOMORKARTU.ResetText()
                grdKDDEPARTMENT.ResetText()
                grdKDDOCTOR.ResetText()
                txtNOREFERENSI.ResetText()
                MsgBox("Data Pasien Tidak ditemukan, Silahkan kebagian Rekam Medis untuk mendaftarkan sebagai pasien baru atau Pilih Antrian Ofline", MsgBoxStyle.Information, Me.Text)
            End If

            'If rbJENISKUNJUNGAN.SelectedIndex = 2 Then
            '    Dim oSKD As New Admission.clsSKD
            '    Dim dsSKD = oSKD.GetDataByDate(txtSEARCH.Text)
            '    If dsSKD IsNot Nothing Then
            '        txtNAMAPASIEN.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
            '        txtNOMORHP.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.PHONE
            '        txtNOMORNIK.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KTP
            '        txtNOMORRM.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KDCUSTOMER
            '        txtNOMORKARTU.Text = dsSKD.S_PENDAFTARAN_H.M_CUSTOMER.KARTUBPJS
            '        grdKDDEPARTMENT.Text = dsSKD.KDDEPARTMENT
            '        grdKDDOCTOR.Text = dsSKD.KDDOCTOR
            '        txtNOREFERENSI.Text = dsSKD.NOMORRUJUKAN
            '    Else
            '        txtNAMAPASIEN.ResetText()
            '        txtNOMORHP.ResetText()
            '        txtNOMORNIK.ResetText()
            '        txtNOMORRM.ResetText()
            '        txtNOMORKARTU.ResetText()
            '        grdKDDEPARTMENT.ResetText()
            '        grdKDDOCTOR.ResetText()
            '        txtNOREFERENSI.ResetText()
            '        MsgBox("Data Pasien Tidak ditemukan, Silahkan kebagian Rekam Medis untuk mendaftarkan sebagai pasien baru", MsgBoxStyle.Information, Me.Text)

            '    End If
            'Else
            '    Dim oCustomer As New Reference.clsCustomer
            '    Dim dsCustomer = oCustomer.GetData(txtSEARCH.Text)
            '    If dsCustomer IsNot Nothing Then
            '        txtNAMAPASIEN.Text = dsCustomer.NAME_DISPLAY
            '        txtNOMORHP.Text = dsCustomer.PHONE
            '        txtNOMORNIK.Text = dsCustomer.KTP
            '        txtNOMORRM.Text = dsCustomer.KDCUSTOMER
            '        txtNOMORKARTU.Text = dsCustomer.KARTUBPJS
            '        lPASIENLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            '        grdKDDEPARTMENT.ShowPopup()
            '    Else
            '        txtNAMAPASIEN.ResetText()
            '        txtNOMORHP.ResetText()
            '        txtNOMORNIK.ResetText()
            '        txtNOMORRM.ResetText()
            '        txtNOMORKARTU.ResetText()
            '        txtNOREFERENSI.ResetText()
            '        MsgBox("Data Pasien Tidak ditemukan, Silahkan kebagian Rekam Medis untuk mendaftarkan sebagai pasien baru", MsgBoxStyle.Information, Me.Text)
            '        lPASIENLAMA.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            '    End If
            'End If

            txtSEARCH.ResetText()

        End If
    End Sub
    Private Sub RadioGroup1_SelectedIndexChanged(sender As Object, e As EventArgs)
        txtSEARCH.Focus()
    End Sub
#End Region
#Region "ButtonNumber"
    Private Sub SimpleButton1_Click_1(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        txtSEARCH.Text = txtSEARCH.Text + "1"
    End Sub
    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        txtSEARCH.Text = txtSEARCH.Text + "2"
    End Sub
    Private Sub SimpleButton3_Click(sender As Object, e As EventArgs) Handles SimpleButton3.Click
        txtSEARCH.Text = txtSEARCH.Text + "3"
    End Sub
    Private Sub SimpleButton4_Click(sender As Object, e As EventArgs) Handles SimpleButton4.Click
        txtSEARCH.Text = txtSEARCH.Text + "4"
    End Sub
    Private Sub SimpleButton5_Click(sender As Object, e As EventArgs) Handles SimpleButton5.Click
        txtSEARCH.Text = txtSEARCH.Text + "5"
    End Sub
    Private Sub SimpleButton6_Click(sender As Object, e As EventArgs) Handles SimpleButton6.Click
        txtSEARCH.Text = txtSEARCH.Text + "6"
    End Sub
    Private Sub SimpleButton7_Click(sender As Object, e As EventArgs) Handles SimpleButton7.Click
        txtSEARCH.Text = txtSEARCH.Text + "7"
    End Sub
    Private Sub SimpleButton8_Click(sender As Object, e As EventArgs) Handles SimpleButton8.Click
        txtSEARCH.Text = txtSEARCH.Text + "8"
    End Sub
    Private Sub SimpleButton9_Click(sender As Object, e As EventArgs) Handles SimpleButton9.Click
        txtSEARCH.Text = txtSEARCH.Text + "9"
    End Sub
    Private Sub SimpleButton0_Click(sender As Object, e As EventArgs) Handles SimpleButton0.Click
        txtSEARCH.Text = txtSEARCH.Text + "0"
    End Sub
    Private Sub btnHapus_Click(sender As Object, e As EventArgs) Handles btnHapus.Click
        'If txtSEARCH.Text.Length > 0 Then
        '    Dim d As Integer = txtSEARCH.Text.Length
        '    txtSEARCH.Text = txtSEARCH.Text.Remove(d - 1, 1)
        'End If
        txtSEARCH.ResetText()
        txtSEARCH.Focus()
    End Sub
    Private Sub btnEnter_Click(sender As Object, e As EventArgs) Handles btnEnter.Click
        txtSEARCH.Focus()
        txtSEARCH.Select(txtSEARCH.Text.Length, 0)
        SendKeys.Send("{ENTER}")
    End Sub
    Private Sub rbJENISKUNJUNGAN_SelectedIndexChanged(sender As Object, e As EventArgs)
        fn_EmptyMe()
        txtSEARCH.Focus()
    End Sub
    Private Sub picQR_Click(sender As Object, e As EventArgs) Handles picQR.Click
        txtSEARCH.ResetText()
        txtSEARCH.Focus()
    End Sub
    Private Sub btnCETAKANTRIAN_Click(sender As Object, e As EventArgs) Handles btnCETAKANTRIAN.Click
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            'sStatusSave = "NEW"
            'Me.Close()
            fn_EmptyMe()
            txtSEARCH.Focus()
        End If
    End Sub

#End Region
End Class