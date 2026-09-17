Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmLaporanPersalinan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oLaporanPersalinan As New EMedrek.clsLaporanPersalinan
    Private isLoad As Boolean = False
    Private sNoRekamMedis As String
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        txtKDLAPORANPERSALINAN.Text = NoId
        txtKDIDENTITAS.Text = KDIDENTITAS
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Laporan Persalinan"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDPJP()

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

        Dim oKunjungan As New Admission.clsPendaftaran_Kunjungan
        Dim dsKunjungan = oKunjungan.GetDatabyIdentitas(txtKDIDENTITAS.Text)

        If dsKunjungan IsNot Nothing Then
            sNoRekamMedis = dsKunjungan.KDCUSTOMER
            txtRUANGAN.Text = dsKunjungan.KDDEPARTMENT_NAMA
            grdCPPT_KDDOCTOR.Text = dsKunjungan.KDDOCTOR
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)

    End Sub
    Private Sub fn_EmptyMe()
        txtKDLAPORANPERSALINAN.Text = "<--- AUTO --->"
        txtRUANGAN.ResetText()
        grdCPPT_KDDOCTOR.ResetText()
        txtCATATANLAPORAN.ResetText()
        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oLaporanPersalinan.GetData(txtKDLAPORANPERSALINAN.Text)
            With ds
                txtCATATANLAPORAN.Text = .CATATANLAPORAN
            End With
        Catch oErr As Exception
            MsgBox("Load List Data Laporan Operasi: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDIDENTITAS.Text = "" Then
                If txtKDIDENTITAS.Text = 0 Then
                    txtKDIDENTITAS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                    txtKDIDENTITAS.ErrorText = Statement.ErrorRequired

                    MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If
            If txtCATATANLAPORAN.Text = "" Then
                txtCATATANLAPORAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtCATATANLAPORAN.ErrorText = Statement.ErrorRequired

                MsgBox("Catatan Laporan Masih Kosong Kosong", MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End If
        Catch oErr As Exception
            fn_Validate = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oLaporanPersalinan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oLaporanPersalinan.GetData(txtKDLAPORANPERSALINAN.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDIDENTITAS = txtKDIDENTITAS.Text
                .KDLAPROANPERSALINAN = txtKDLAPORANPERSALINAN.Text
                .KDCUSTOMER = sNoRekamMedis
                .CATATANLAPORAN = txtCATATANLAPORAN.Text
                .KDUSER = sUserID
                Try
                    .ISDELETE = oLaporanPersalinan.GetData(txtKDLAPORANPERSALINAN.Text).ISDELETE
                Catch oErr As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oLaporanPersalinan.GetData(txtKDLAPORANPERSALINAN.Text).DATEDELETE
                Catch oErr As Exception
                    .DATEDELETE = Now
                End Try
                Try
                    .USERDELETE = oLaporanPersalinan.GetData(txtKDLAPORANPERSALINAN.Text).USERDELETE
                Catch oErr As Exception
                    .USERDELETE = ""
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDLAPORANPERSALINAN.Text = oLaporanPersalinan.InsertData(ds)

                    If txtKDLAPORANPERSALINAN.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oLaporanPersalinan.UpdateData(ds)
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
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdCPPT_KDDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdCPPT_KDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdCPPT_KDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class