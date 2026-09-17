Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmFromulirTemplate
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
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
            Me.Text = "Template"

            btnSaveClosee.Caption = "Simpan"
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
        'btnSaveNew.Enabled = False
        'btnSaveClose.Enabled = Not Status

        'btnSimpanLaporanOperasi.Enabled = Not Status

        'TableLayoutPanel9.Anchor = AnchorStyles.Top
        'TableLayoutPanel9.Anchor = AnchorStyles.Left
        'TableLayoutPanel9.Anchor = AnchorStyles.Right

        'GroupControl9.Anchor = AnchorStyles.Top
        'GroupControl9.Anchor = AnchorStyles.Left
        'GroupControl9.Anchor = AnchorStyles.Right
    End Sub
    Private Sub fn_EmptyMe()

    End Sub
    Private Sub fn_LoadData()
        Try
            '' ***** HEADER *****
            'Dim ds = oDigital_LaporanTindakan.GetData(sNoId)
            'With ds

            'End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean

    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****

            'Dim ds = oDigital_LaporanTindakan.GetStructureHeader
            'With ds

            'End With

            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    Try
            '        sNoId = oDigital_LaporanTindakan.InsertData(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)
            '        If sNoId = "" Then
            '            fn_Save = False
            '        Else
            '            fn_Save = True
            '        End If
            '    Catch ex As Exception
            '        MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'Else
            '    Try
            '        fn_Save = oDigital_LaporanTindakan.UpdateData(ds, arrDetailDiagnosa, Nothing, arrDetailProsedur)
            '    Catch ex As Exception
            '        MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'End If
        Catch oErr As Exception
            MsgBox("Simpan Data Laporan Tindakan: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Command Button"
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class