Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmTelaahResep
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oTelaahResep As New Sales.clsSalesOrderTransaksi
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
            Me.Text = "Telaah Resep"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
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

        CheckEdit1.Properties.ReadOnly = Status
        CheckEdit2.Properties.ReadOnly = Status
        CheckEdit3.Properties.ReadOnly = Status
        CheckEdit4.Properties.ReadOnly = Status
        CheckEdit5.Properties.ReadOnly = Status
        CheckEdit6.Properties.ReadOnly = Status
        CheckEdit7.Properties.ReadOnly = Status
        CheckEdit8.Properties.ReadOnly = Status
        CheckEdit9.Properties.ReadOnly = Status
        CheckEdit10.Properties.ReadOnly = Status
        CheckEdit11.Properties.ReadOnly = Status
        CheckEdit12.Properties.ReadOnly = Status
        CheckEdit13.Properties.ReadOnly = Status
        CheckEdit14.Properties.ReadOnly = Status
        CheckEdit15.Properties.ReadOnly = Status
        CheckEdit16.Properties.ReadOnly = Status
        CheckEdit17.Properties.ReadOnly = Status
        CheckEdit18.Properties.ReadOnly = Status
        CheckEdit19.Properties.ReadOnly = Status
        CheckEdit20.Properties.ReadOnly = Status
        CheckEdit21.Properties.ReadOnly = Status
        CheckEdit22.Properties.ReadOnly = Status
        CheckEdit23.Properties.ReadOnly = Status
        CheckEdit24.Properties.ReadOnly = Status

        CheckEditObat1_1.Properties.ReadOnly = Status
        CheckEditObat1_2.Properties.ReadOnly = Status
        CheckEditObat1_3.Properties.ReadOnly = Status
        CheckEditObat1_4.Properties.ReadOnly = Status
        CheckEditObat1_5.Properties.ReadOnly = Status

        CheckEditObat2_1.Properties.ReadOnly = Status
        CheckEditObat2_2.Properties.ReadOnly = Status
        CheckEditObat2_3.Properties.ReadOnly = Status
        CheckEditObat2_4.Properties.ReadOnly = Status
        CheckEditObat2_5.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        CheckEdit1.Checked = True
        CheckEdit2.Checked = False
        CheckEdit3.Checked = True
        CheckEdit4.Checked = False
        CheckEdit5.Checked = True
        CheckEdit6.Checked = False
        CheckEdit7.Checked = True
        CheckEdit8.Checked = False
        CheckEdit9.Checked = True
        CheckEdit10.Checked = False
        CheckEdit11.Checked = True
        CheckEdit12.Checked = False
        CheckEdit13.Checked = True
        CheckEdit14.Checked = False
        CheckEdit15.Checked = True
        CheckEdit16.Checked = False
        CheckEdit17.Checked = True
        CheckEdit18.Checked = False
        CheckEdit19.Checked = True
        CheckEdit20.Checked = False
        CheckEdit21.Checked = True
        CheckEdit22.Checked = False
        CheckEdit23.Checked = True
        CheckEdit24.Checked = False

        CheckEditObat1_1.Checked = True
        CheckEditObat1_2.Checked = True
        CheckEditObat1_3.Checked = True
        CheckEditObat1_4.Checked = True
        CheckEditObat1_5.Checked = True

        CheckEditObat2_1.Checked = False
        CheckEditObat2_2.Checked = False
        CheckEditObat2_3.Checked = False
        CheckEditObat2_4.Checked = False
        CheckEditObat2_5.Checked = False
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oTelaahResep.GetDataTelaahResep(sNoId)

            With ds
                CheckEdit1.Checked = .TELAAH_01
                CheckEdit2.Checked = .TELAAH_02
                CheckEdit3.Checked = .TELAAH_03
                CheckEdit4.Checked = .TELAAH_04
                CheckEdit5.Checked = .TELAAH_05
                CheckEdit6.Checked = .TELAAH_06
                CheckEdit7.Checked = .TELAAH_07
                CheckEdit8.Checked = .TELAAH_08
                CheckEdit9.Checked = .TELAAH_09
                CheckEdit10.Checked = .TELAAH_10
                CheckEdit11.Checked = .TELAAH_11
                CheckEdit12.Checked = .TELAAH_12
                CheckEdit13.Checked = .TELAAH_13
                CheckEdit14.Checked = .TELAAH_14
                CheckEdit15.Checked = .TELAAH_15
                CheckEdit16.Checked = .TELAAH_16
                CheckEdit17.Checked = .TELAAH_17
                CheckEdit18.Checked = .TELAAH_18
                CheckEdit19.Checked = .TELAAH_19
                CheckEdit20.Checked = .TELAAH_20
                CheckEdit21.Checked = .TELAAH_21
                CheckEdit22.Checked = .TELAAH_22
                CheckEdit23.Checked = .TELAAH_23
                CheckEdit24.Checked = .TELAAH_24
            End With

            Dim dsObat1 = oTelaahResep.GetDataTelaahObat1(sNoId)

            With dsObat1
                CheckEditObat1_1.Checked = .TELAAH_01
                CheckEditObat1_2.Checked = .TELAAH_02
                CheckEditObat1_3.Checked = .TELAAH_03
                CheckEditObat1_4.Checked = .TELAAH_04
                CheckEditObat1_5.Checked = .TELAAH_05
            End With

            Dim dsObat2 = oTelaahResep.GetDataTelaahObat2(sNoId)

            With dsObat2
                CheckEditObat2_1.Checked = .TELAAH_01
                CheckEditObat2_2.Checked = .TELAAH_02
                CheckEditObat2_3.Checked = .TELAAH_03
                CheckEditObat2_4.Checked = .TELAAH_04
                CheckEditObat2_5.Checked = .TELAAH_05
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            'If txtMEMO.Text = String.Empty Then
            '    txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtMEMO.ErrorText = Statement.ErrorRequired

            '    txtMEMO.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    If oTelaahResep.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '        txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '        txtMEMO.ErrorText = Statement.ErrorRegistered

            '        txtMEMO.Focus()
            '        fn_Validate = False
            '        Exit Function
            '    End If
            'Else
            '    If txtMEMO.Text.Trim.ToUpper <> oTelaahResep.GetData(sNoId).MEMO Then
            '        If oTelaahResep.IsExist(txtMEMO.Text.ToUpper.Trim) = True Then
            '            txtMEMO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '            txtMEMO.ErrorText = Statement.ErrorRegistered

            '            txtMEMO.Focus()
            '            fn_Validate = False
            '            Exit Function
            '        End If
            '    End If
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oTelaahResep.GetStructureHeaderTelaahResep
            With ds
                Try
                    .DATECREATED = oTelaahResep.GetDataTelaahResep(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = sNoId
                .TELAAH_01 = CheckEdit1.Checked
                .TELAAH_02 = CheckEdit2.Checked
                .TELAAH_03 = CheckEdit3.Checked
                .TELAAH_04 = CheckEdit4.Checked
                .TELAAH_05 = CheckEdit5.Checked
                .TELAAH_06 = CheckEdit6.Checked
                .TELAAH_07 = CheckEdit7.Checked
                .TELAAH_08 = CheckEdit8.Checked
                .TELAAH_09 = CheckEdit9.Checked
                .TELAAH_10 = CheckEdit10.Checked
                .TELAAH_11 = CheckEdit11.Checked
                .TELAAH_12 = CheckEdit12.Checked
                .TELAAH_13 = CheckEdit13.Checked
                .TELAAH_14 = CheckEdit14.Checked
                .TELAAH_15 = CheckEdit15.Checked
                .TELAAH_16 = CheckEdit16.Checked
                .TELAAH_17 = CheckEdit17.Checked
                .TELAAH_18 = CheckEdit18.Checked
                .TELAAH_19 = CheckEdit19.Checked
                .TELAAH_20 = CheckEdit20.Checked
                .TELAAH_21 = CheckEdit21.Checked
                .TELAAH_22 = CheckEdit22.Checked
                .TELAAH_23 = CheckEdit23.Checked
                .TELAAH_24 = CheckEdit24.Checked
                Try
                    Dim sREMARKS As String = oTelaahResep.GetDataTelaahResep(sNoId).REMARKS
                    Dim sKDUSER_SIMPAN As String = oTelaahResep.GetDataTelaahResep(sNoId).KDUSER

                    .REMARKS = sREMARKS & " " & sKDUSER_SIMPAN
                Catch oErr As Exception
                    .REMARKS = ""
                End Try

                Try
                    .KDUSER = oTelaahResep.GetDataTelaahObat2(sNoId).KDUSER & ", " & sUserID
                Catch oErr As Exception
                    .KDUSER = sUserID
                End Try
            End With

            Dim dsObat1 = oTelaahResep.GetStructureHeaderTelaahObat1

            With dsObat1
                Try
                    .DATECREATED = oTelaahResep.GetDataTelaahObat1(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = sNoId
                .TELAAH_01 = CheckEditObat1_1.Checked
                .TELAAH_02 = CheckEditObat1_2.Checked
                .TELAAH_03 = CheckEditObat1_3.Checked
                .TELAAH_04 = CheckEditObat1_4.Checked
                .TELAAH_05 = CheckEditObat1_5.Checked
                Try
                    Dim sREMARKS As String = oTelaahResep.GetDataTelaahObat1(sNoId).REMARKS
                    Dim sKDUSER_SIMPAN As String = oTelaahResep.GetDataTelaahObat1(sNoId).KDUSER

                    .REMARKS = sREMARKS & " " & sKDUSER_SIMPAN
                Catch oErr As Exception
                    .REMARKS = ""
                End Try
                Try
                    .KDUSER = oTelaahResep.GetDataTelaahObat2(sNoId).KDUSER & ", " & sUserID
                Catch oErr As Exception
                    .KDUSER = sUserID
                End Try
            End With

            Dim dsObat2 = oTelaahResep.GetStructureHeaderTelaahObat2

            With dsObat2
                Try
                    .DATECREATED = oTelaahResep.GetDataTelaahObat2(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDSOTRANSAKSI = sNoId
                .TELAAH_01 = CheckEditObat2_1.Checked
                .TELAAH_02 = CheckEditObat2_2.Checked
                .TELAAH_03 = CheckEditObat2_3.Checked
                .TELAAH_04 = CheckEditObat2_4.Checked
                .TELAAH_05 = CheckEditObat2_5.Checked
                Try
                    Dim sREMARKS As String = oTelaahResep.GetDataTelaahObat2(sNoId).REMARKS
                    Dim sKDUSER_SIMPAN As String = oTelaahResep.GetDataTelaahObat2(sNoId).KDUSER

                    .REMARKS = sREMARKS & " " & sKDUSER_SIMPAN
                Catch oErr As Exception
                    .REMARKS = ""
                End Try
                Try
                    .KDUSER = oTelaahResep.GetDataTelaahObat2(sNoId).KDUSER & ", " & sUserID
                Catch oErr As Exception
                    .KDUSER = sUserID
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oTelaahResep.InsertDataTelaahResep(ds, dsObat1, dsObat2)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oTelaahResep.UpdateDataTelaahResep(ds, dsObat1, dsObat2)
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
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
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
            'MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub CheckEdit1_Click(sender As Object, e As EventArgs) Handles CheckEdit1.Click
        CheckEdit2.Checked = False
    End Sub
    Private Sub CheckEdit2_Click(sender As Object, e As EventArgs) Handles CheckEdit2.Click
        CheckEdit1.Checked = False
    End Sub
    Private Sub CheckEdit3_Click(sender As Object, e As EventArgs) Handles CheckEdit3.Click
        CheckEdit4.Checked = False
    End Sub
    Private Sub CheckEdit4_Click(sender As Object, e As EventArgs) Handles CheckEdit4.Click
        CheckEdit3.Checked = False
    End Sub
    Private Sub CheckEdit5_Click(sender As Object, e As EventArgs) Handles CheckEdit5.Click
        CheckEdit6.Checked = False
    End Sub
    Private Sub CheckEdit6_Click(sender As Object, e As EventArgs) Handles CheckEdit6.Click
        CheckEdit5.Checked = False
    End Sub
    Private Sub CheckEdit7_Click(sender As Object, e As EventArgs) Handles CheckEdit7.Click
        CheckEdit8.Checked = False
    End Sub
    Private Sub CheckEdit8_Click(sender As Object, e As EventArgs) Handles CheckEdit8.Click
        CheckEdit7.Checked = False
    End Sub
    Private Sub CheckEdit9_Click(sender As Object, e As EventArgs) Handles CheckEdit9.Click
        CheckEdit10.Checked = False
    End Sub
    Private Sub CheckEdit10_Click(sender As Object, e As EventArgs) Handles CheckEdit10.Click
        CheckEdit9.Checked = False
    End Sub
    Private Sub CheckEdit11_Click(sender As Object, e As EventArgs) Handles CheckEdit11.Click
        CheckEdit12.Checked = False
    End Sub
    Private Sub CheckEdit12_Click(sender As Object, e As EventArgs) Handles CheckEdit12.Click
        CheckEdit11.Checked = False
    End Sub
    Private Sub CheckEdit13_Click(sender As Object, e As EventArgs) Handles CheckEdit13.Click
        CheckEdit14.Checked = False
    End Sub
    Private Sub CheckEdit14_Click(sender As Object, e As EventArgs) Handles CheckEdit14.Click
        CheckEdit13.Checked = False
    End Sub
    Private Sub CheckEdit15_Click(sender As Object, e As EventArgs) Handles CheckEdit15.Click
        CheckEdit16.Checked = False
    End Sub
    Private Sub CheckEdit16_Click(sender As Object, e As EventArgs) Handles CheckEdit16.Click
        CheckEdit15.Checked = False
    End Sub
    Private Sub CheckEdit17_Click(sender As Object, e As EventArgs) Handles CheckEdit17.Click
        CheckEdit18.Checked = False
    End Sub
    Private Sub CheckEdit18_Click(sender As Object, e As EventArgs) Handles CheckEdit18.Click
        CheckEdit17.Checked = False
    End Sub
    Private Sub CheckEdit19_Click(sender As Object, e As EventArgs) Handles CheckEdit19.Click
        CheckEdit20.Checked = False
    End Sub
    Private Sub CheckEdit20_Click(sender As Object, e As EventArgs) Handles CheckEdit20.Click
        CheckEdit19.Checked = False
    End Sub
    Private Sub CheckEdit21_Click(sender As Object, e As EventArgs) Handles CheckEdit21.Click
        CheckEdit22.Checked = False
    End Sub
    Private Sub CheckEdit22_Click(sender As Object, e As EventArgs) Handles CheckEdit22.Click
        CheckEdit21.Checked = False
    End Sub
    Private Sub CheckEdit23_Click(sender As Object, e As EventArgs) Handles CheckEdit23.Click
        CheckEdit24.Checked = False
    End Sub
    Private Sub CheckEdit24_Click(sender As Object, e As EventArgs) Handles CheckEdit24.Click
        CheckEdit23.Checked = False
    End Sub
#End Region
End Class