Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmPendaftaran_KunjunganPoli
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
     Private sKoneksi As String = String.Empty
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String)
        oFormMode = FormMode
        sNoId = NoId

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = Pendaftaran_KunjunganPoli.TITLE

            lKDKUNJUNGAN_POLI.Text = Pendaftaran_KunjunganPoli.KDKUNJUNGAN_POLI
            lDATE_MASUK.Text = Pendaftaran_KunjunganPoli.DATE_MASUK
            lKDDEPARTMENT.Text = "Tujuan"
            lDOCTOR.Text = "Dokter"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDEPARTMENT()
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

        deDATE_MASUK.Properties.ReadOnly = Status
        grdKDDEPARTMENT.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE_MASUK.DateTime = Now
        grdKDDEPARTMENT.ResetText()
        grdDOCTOR.ResetText()
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = oPendaftaran_KunjunganPoli.GetData(sNoId)

        '    With ds
        '        txtCODE.Text = sNoId
        '        cboCARI.SelectedIndex = 2
        '        fn_LoadKDPENDAFTARAN(.KDPENDAFTARAN)
        '        grdKDPENDAFTARAN.Text = .KDPENDAFTARAN
        '        deDATE_MASUK.DateTime = .DATE_MASUK
        '        deDATE_KELUAR.DateTime = .DATE_KELUAR
        '        grdKDDEPARTMENT.Text = .KDDEPARTMENT
        '        grdPENJAMIN.Text = .KDPENJAMIN
        '        grdDOCTOR.Text = .KDDOCTOR
        '        chkISJAGA.Checked = .ISJAGA
        '    End With
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            'If txtCATATAN.Text = String.Empty Then
            '    txtCATATAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
            '    txtCATATAN.ErrorText = Statement.ErrorRequired

            '    txtCATATAN.Focus()
            '    fn_Validate = False
            '    Exit Function
            'End If
            If txtPENJAMIN.Text = String.Empty Then
                txtPENJAMIN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtPENJAMIN.ErrorText = Statement.ErrorRequired

                txtPENJAMIN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdDOCTOR.Text = String.Empty Then
                grdDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdDOCTOR.ErrorText = Statement.ErrorRequired

                grdDOCTOR.Focus()
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
        'Try
        '    ' ***** HEADER *****
        '    Dim ds = oPendaftaran_KunjunganPoli.GetStructureHeader
        '    With ds
        '        Try
        '            .DATECREATED = oPendaftaran_KunjunganPoli.GetData(sNoId).DATECREATED
        '        Catch oErr As Exception
        '            .DATECREATED = Now
        '        End Try
        '        .DATEUPDATED = Now
        '        .KDKUNJUNGAN_POLI = sNoId
        '        .KDPENDAFTARAN = grdKDPENDAFTARAN.EditValue
        '        .KDDEPARTMENT = grdKDDEPARTMENT.EditValue
        '        .DATE_MASUK = deDATE_MASUK.DateTime
        '        .DATE_KELUAR = deDATE_KELUAR.DateTime
        '        Try
        '            .ISCHEKED = oPendaftaran_KunjunganPoli.GetData(sNoId).ISCHEKED
        '        Catch ex As Exception
        '            .ISCHEKED = False
        '        End Try
        '        Try
        '            .MEMO = oPendaftaran_KunjunganPoli.GetData(sNoId).MEMO
        '        Catch ex As Exception
        '            .MEMO = "POLI SELANJUTNYA"
        '        End Try
        '        .KDUSER = sUserID
        '        .KDPENJAMIN = grdPENJAMIN.EditValue
        '        .KDDOCTOR = grdDOCTOR.EditValue
        '        .ISJAGA = chkISJAGA.Checked
        '        Try
        '            .ISRESUME = oPendaftaran_KunjunganPoli.GetData(sNoId).ISRESUME
        '        Catch ex As Exception
        '            .ISRESUME = False
        '        End Try
        '    End With

        '    If oFormMode = FORM_MODE.FORM_MODE_ADD Then
        '        Try
        '            txtCODE.Text = oPendaftaran_KunjunganPoli.InsertData(ds, oPendaftaran_KunjunganPoli.GetDataMemoDepartment(grdKDDEPARTMENT.EditValue).MEMO & oPendaftaran_KunjunganPoli.GetDataMemoDoctor(grdDOCTOR.EditValue).MEMO & IIf(chkISJAGA.Checked = False, "P", "S" & If(grdPENJAMIN.Text = "BPJS KESEHATAN", "B", "A")))
        '            If txtCODE.Text = "" Then
        '                fn_Save = False
        '            Else
        '                fn_Save = True
        '                CetakRegister(txtCODE.Text)
        '            End If

        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
        '        Try
        '            fn_Save = oPendaftaran_KunjunganPoli.UpdateData(ds)
        '            CetakRegister(txtCODE.Text)
        '        Catch oErr As Exception
        '            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '        End Try
        '    End If
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        '    fn_Save = False
        'End Try
    End Function
    Private Sub CetakRegister(ByVal KDPendaftaran_KunjunganPoli As String)
        'Dim rpt As New xtraPendaftaran_KunjunganPoli

        'Dim ds = oPendaftaran_KunjunganPoli.GetData(KDPendaftaran_KunjunganPoli)
        'rpt.bindingSource.DataSource = ds
        'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
        'printTool.PrintDialog()
    End Sub
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
    Private Sub fn_LoadKDDEPARTMENT()
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
            SQL &= "M_DEPARTMENT A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DEPARTMENT")

            grdDOCTOR.Properties.DataSource = ds.Tables("DEPARTMENT")
            grdDOCTOR.Properties.ValueMember = "KDDEPARTMENT"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
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

            grdDOCTOR.Properties.DataSource = ds.Tables("DOKTER")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class