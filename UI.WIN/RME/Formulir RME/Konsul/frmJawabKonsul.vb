Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmJawabKonsul
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oJawabKonsul As New Digital.clsJawabKonsul
    Private sKDKUNJUNGAN As String = String.Empty
    Private sKDKONSULTASI As String = String.Empty
    Private sKoneksi As String = String.Empty
    Private sDOCTOR_1 As String = String.Empty
    Private sDOCTOR_2 As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String, ByVal KDKONSULTASI As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDKUNJUNGAN = KDKUNJUNGAN
        sKDKONSULTASI = KDKONSULTASI
        Dim dsKunjungan = oJawabKonsul.GetDataKonsultasi(KDKONSULTASI)
        If dsKunjungan IsNot Nothing Then
            sDOCTOR_1 = dsKunjungan.KDDOKTER_KEPADA
            sDOCTOR_2 = dsKunjungan.KDDOKTER_DARI
            txtKONSULTASI.Text = dsKunjungan.MEMO
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        deDATE.DateTime = Now

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = JawabKonsul.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = sNoId
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

        grdKDOCTOR_FROM.Properties.ReadOnly = Status
        grdKDOCTOR_TO.Properties.ReadOnly = Status
        txtMEMO.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        grdKDOCTOR_FROM.ResetText()
        grdKDOCTOR_TO.ResetText()
        txtMEMO.ResetText()
        grdKDOCTOR_FROM.Text = sDOCTOR_1
        grdKDOCTOR_TO.Text = sDOCTOR_2
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oJawabKonsul.GetData(sNoId)

            With ds
                grdKDOCTOR_FROM.Text = .KDDOKTER_DARI
                grdKDOCTOR_TO.Text = .KDDOKTER_KEPADA
                txtMEMO.Text = .MEMO
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If grdKDOCTOR_FROM.Text = String.Empty Then
                grdKDOCTOR_FROM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDOCTOR_FROM.ErrorText = Statement.ErrorRequired

                grdKDOCTOR_FROM.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDOCTOR_TO.Text = String.Empty Then
                grdKDOCTOR_TO.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDOCTOR_TO.ErrorText = Statement.ErrorRequired

                grdKDOCTOR_TO.Focus()
                fn_Validate = False
                Exit Function
            End If
            If sKDKONSULTASI = String.Empty Then
                MsgBox("Kode Konsulatasi Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If
            If sKDKUNJUNGAN = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

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
            Dim ds = oJawabKonsul.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oJawabKonsul.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDJAWABKONSUL = sNoId
                .KDKUNJUNGAN = sKDKUNJUNGAN
                .KDKONSULTASI = sKDKONSULTASI
                .KDDOKTER_DARI = grdKDOCTOR_FROM.EditValue
                .DOKTER_DARI = grdKDOCTOR_FROM.Text
                .KDDOKTER_KEPADA = grdKDOCTOR_TO.EditValue
                .DOKTER_KEPADA = grdKDOCTOR_TO.Text
                .MEMO = txtMEMO.Text.ToString.Trim.ToUpper
                Try
                    .ISCHEKED = oJawabKonsul.GetData(sNoId).ISCHEKED
                Catch ex As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oJawabKonsul.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oJawabKonsul.UpdateData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            'Dim oResumeRawatJalan As New Digital.clsResumeRawatJalan

            'oResumeRawatJalan.UpdateResumeDataKunjunganRawatJalanNew(sKDKUNJUNGAN, 1)
            'oResumeRawatJalan.UpdateResumeDataKunjunganRawatJalanOld(sKDKUNJUNGAN, 1)
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
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
        End Select
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
    Private Sub fn_LoadKDDOCTOR()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksi
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_DOCTOR A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DOKTER")

            grdKDOCTOR_FROM.Properties.DataSource = ds.Tables("DOKTER")
            grdKDOCTOR_FROM.Properties.ValueMember = "KDDOCTOR"
            grdKDOCTOR_FROM.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDOCTOR_TO.Properties.DataSource = ds.Tables("DOKTER")
            grdKDOCTOR_TO.Properties.ValueMember = "KDDOCTOR"
            grdKDOCTOR_TO.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class