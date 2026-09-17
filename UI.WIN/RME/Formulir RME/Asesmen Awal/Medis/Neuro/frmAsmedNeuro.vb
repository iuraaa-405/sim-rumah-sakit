Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmAsmedNeuro
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASMEDNEURO As New EMedrek.clsDigital_RJ_ASMEDNEURO
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String
    Private sIsOtority As Boolean = False
    Private sNoid As String
    Private sKodeIdentitas As Integer
    Private sKDDOCTOR As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDDOCTOR As String, ByVal KodeIdentitas As Integer, ByVal NoId As String)
        oFormMode = FormMode

        sKodeIdentitas = KodeIdentitas
        sNoid = NoId
        sKDDOCTOR = KDDOCTOR
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "ASESMEN AWAL MEDIS PASIEN NEUROLOGI"
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

        txtKELUHANUTAMA.Properties.ReadOnly = Status
        txtRIWAYATPENYAKITSEKARANG.Properties.ReadOnly = Status
        txtRIWAYATPENYAKITDAHULU.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIKUMUM.Properties.ReadOnly = Status
        txtPEMERIKSAANFISIKNEUROLOGI.Properties.ReadOnly = Status

        'Dim oSetUser As New Setting.clsUser
        'Dim dsUser = oSetUser.GetData(sUserID)
        'If dsUser IsNot Nothing Then
        '    If dsUser.ISOTORTY = True Then
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        '        sIsOtority = True
        '    Else
        '        lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        '        sIsOtority = False
        '    End If
        'End If
    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        'txtTanggal.Text = DateTime.Now.ToString("dd-MM-yyyy")
        txtKELUHANUTAMA.ResetText()
        txtRIWAYATPENYAKITSEKARANG.ResetText()
        txtRIWAYATPENYAKITDAHULU.ResetText()
        txtPEMERIKSAANFISIKUMUM.ResetText()
        txtPEMERIKSAANFISIKNEUROLOGI.ResetText()
        txtPEMERIKSAANPENUNJANG.ResetText()
        txtDIFERENSIALDIAGNOSE.ResetText()
        txtDIAGNOSEKERJA.ResetText()
        txtPENGOBATANDANTINDAKAN.ResetText()
        txtREKONSILIASIOBAT.ResetText()
        txtDISCHARGEPLANNING.ResetText()

        grdDOCTOR.Text = sKDDOCTOR
        deDATE.DateTime = Now

    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASMEDNEURO.GetData(sNoid)

            With ds
                'txtTanggal.Text = .DATE
                txtKELUHANUTAMA.Text = .KELUHANUTAMA
                txtRIWAYATPENYAKITSEKARANG.Text = .RIWAYATPENYAKITSEKARANG
                txtRIWAYATPENYAKITDAHULU.Text = .RIWAYATPENYAKITDAHULU
                txtPEMERIKSAANFISIKUMUM.Text = .PEMERIKSAANFISIKUMUM
                txtPEMERIKSAANFISIKNEUROLOGI.Text = .PEMERIKSAANFISIKNEUROLOGI
                txtPEMERIKSAANPENUNJANG.Text = .PEMERIKSAANPENUNJANG
                txtDIFERENSIALDIAGNOSE.Text = .DIFERENSIALDIAGNOSE
                txtDIAGNOSEKERJA.Text = .DIAGNOSEKERJA
                txtPENGOBATANDANTINDAKAN.Text = .PENGOBATANDANTINDAKAN
                txtREKONSILIASIOBAT.Text = .REKONSILIASIOBAT
                txtDISCHARGEPLANNING.Text = .DISCHARGEPLANNING
                grdDOCTOR.Text = .KDCUSTOMER
                deDATE.DateTime = .DATE
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
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASMEDNEURO.GetStructureHeader
            With ds
                .KDIDENTITAS = sKodeIdentitas
                .KDKUNJUNGAN = sNoid
                .KDCUSTOMER = grdDOCTOR.EditValue
                Try
                    .DATECREATED = oS_DIGITAL_ASMEDNEURO.GetData(sNoid).DATECREATED
                    '.DATE = oS_DIGITAL_ASMEDNEURO.GetData(sNoId).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    '.DATE = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .KELUHANUTAMA = txtKELUHANUTAMA.Text
                .RIWAYATPENYAKITSEKARANG = txtRIWAYATPENYAKITSEKARANG.Text
                .RIWAYATPENYAKITDAHULU = txtRIWAYATPENYAKITDAHULU.Text
                .PEMERIKSAANFISIKUMUM = txtPEMERIKSAANFISIKUMUM.Text
                .PEMERIKSAANFISIKNEUROLOGI = txtPEMERIKSAANFISIKNEUROLOGI.Text
                .PEMERIKSAANPENUNJANG = txtPEMERIKSAANPENUNJANG.Text
                .DIFERENSIALDIAGNOSE = txtDIFERENSIALDIAGNOSE.Text
                .DIAGNOSEKERJA = txtDIAGNOSEKERJA.Text
                .PENGOBATANDANTINDAKAN = txtPENGOBATANDANTINDAKAN.Text
                .REKONSILIASIOBAT = txtREKONSILIASIOBAT.Text
                .DISCHARGEPLANNING = txtDISCHARGEPLANNING.Text

                Try
                    .CETAK = oS_DIGITAL_ASMEDNEURO.GetData(sNoid).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                'Try
                '    If sIsOtority = True Then
                '        .KDUSER = oS_DIGITAL_ASMEDNEURO.GetData(sNoId).KDUSER
                '        .KDUSER_SIGNATURE = oS_DIGITAL_ASMEDNEURO.GetData(sNoId).KDUSER_SIGNATURE
                '    Else
                '        .KDUSER = sUserID
                '        .KDUSER_SIGNATURE = sUserSIGNATURE
                '    End If
                'Catch ex As Exception
                '    .KDUSER = sUserID
                '    .KDUSER_SIGNATURE = sUserSIGNATURE
                'End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = ""
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASMEDNEURO.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASMEDNEURO.UpdateData(ds)
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
            Case Keys.PageUp
                fn_ScrollPage(True)
            Case Keys.PageDown
                fn_ScrollPage(False)
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
    Private Sub fn_LoadKDDOCTOR()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub frmAsmedNeuro_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel1.AutoScrollPosition
	    Dim scrollchange As Integer = 50

	    If isUp Then
		    'up
		    myView.X = -myView.X
		    myView.y = -scrollchange - myView.Y
	    Else
		    'down
		    myView.X = -myView.X
		    myView.y = scrollchange - myView.Y
	    End If

	    Me.Panel1.AutoScrollPosition = myView
    End Sub
#End Region
End Class