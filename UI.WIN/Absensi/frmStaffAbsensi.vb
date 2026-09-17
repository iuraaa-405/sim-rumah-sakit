Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmStaffAbsensi
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oStaffAbsensi As New StaffAbsensi.clsStaffAbsensi
    Private sNoid As Integer = 0
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As String, Optional ByVal NoId As Integer = 0)
        oFormMode = FormMode
        sNoid = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Absensi.TITLE
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
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
        'btnSaveNew.Enabled = Not Status
        'btnSaveClose.Enabled = Not Status

        'grdKDDOCTOR.Properties.ReadOnly = Status
        'grdKDDEPARTMENT.Properties.ReadOnly = Status
        'txtMEMO.Properties.ReadOnly = Status

        'grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()

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

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try

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
        Select Case e.KeyCode
            Case Keys.F12
                Me.Close()
                'Case Keys.F2
                '    If btnSaveNew.Enabled = True Then
                '        btnSaveNew_Click()
                '    End If
                'Case Keys.F3
                '    If btnSaveClose.Enabled = True Then
                '        btnSaveClose_Click()
                '    End If
        End Select
    End Sub
    Private Sub picMasuk_Click(sender As Object, e As EventArgs) Handles picMasuk.Click
        sCategoryAbsensi = cboStatus.SelectedIndex
        Dim frmFingerController As New frmFingerController
        frmFingerController.ShowDialog()
    End Sub
    Private Sub picKeluar_Click(sender As Object, e As EventArgs)
        sCategoryAbsensi = cboStatus.SelectedIndex
        Dim frmFingerController As New frmFingerController
        frmFingerController.ShowDialog()
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim frmReportAbsensi As New frmReportAbsensi
        frmReportAbsensi.ShowDialog()
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class