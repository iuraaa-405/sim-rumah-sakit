Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSuKet_HD
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oSuKet_HD As New EMedrek.clsDigital_SuketHD
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String
    Private sRM As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal RM As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
        sRM = RM
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "SURAT KETERANGAN HEMODIALISA"
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
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
        txtDIAGNOSA.Properties.ReadOnly = Status
        grdDOKTER.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtNOMOR.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        grdDOKTER.EditValue = sKDDOCTOR
        'fn_LoadDiagnosa(txtNoRegister.Text)
        grdDOKTER.Text = sKDDOCTOR
        txtNAMA.Text = "Saat ini benar-benar sedang memerlukan pelayanan pengobatan dengan hemodialisis di Rumkit"
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oSuKet_HD.GetData(sNoId)

            With ds
                'txtNOMOR.Text = .KDSKHD
                deDATE.DateTime = .DATE
                txtDIAGNOSA.Text = .DIAGNOSA
                grdDOKTER.Text = .DOCTOR_KODE
                txtNAMA.Text = .NAMA
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoid = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                deDATE.Focus()
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            MsgBox("Validate Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            Dim oCustomer As New Reference.clsCustomer

            ' ***** HEADER *****
            Dim ds = oSuKet_HD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSuKet_HD.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDSKHD = sNoid
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                .NAMA = txtNAMA.Text
                .NOBPJS = oCustomer.GetData(sRM).KARTUBPJS
                .DIAGNOSA = txtDIAGNOSA.Text
                .DOCTOR_KODE = grdDOKTER.EditValue
                .DOCTOR_NAMEDISPLAY = grdDOKTER.Text
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oSuKet_HD.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oSuKet_HD.UpdateData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            'Case Keys.F12
            '    btnClose_Click()
            'Case Keys.F3
            '    If btnSaveClose.Enabled = True Then
            '        btnSaveClose_Click()
            '    End If
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & sNoid.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox("Save " & sNoid.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            'Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDiagnosa(ByVal Parameter As String)
        'Try
        '    Dim oDiagnosaMaster As New Reference.clsDiagnosa
        '    Dim dsDiagnosa = oDiagnosaMaster.GetDataByKDpendaftaran(Parameter)

        '    If dsDiagnosa IsNot Nothing Then
        '        txtDIAGNOSA.Text = dsDiagnosa.REMARKS
        '    End If

        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub fn_LoadKDDOCTOR()
        Try
            Dim oDPJP As New Reference.clsDoctor
            grdDOKTER.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOKTER.Properties.ValueMember = "KDDOCTOR"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class