Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmPermintaanRuangan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oPermintaanRuangan As New Digital.clsPermintaanRuangan
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
            Me.Text = PermintaanRuangan.TITLE

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = sNoId
        sCPPTAKTIVE = ""
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDDOCTOR()
        fn_DIAGNOSA()
        fn_RUANGRAWAT()

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

        txtKEADAANUMUM.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        txtTTV.Properties.ReadOnly = Status
        txtKET.Properties.ReadOnly = True
    End Sub
    Private Sub fn_EmptyMe()
        txtKEADAANUMUM.Text = "cm"
        txtDIAGNOSA.ResetText()
        txtTTV.ResetText()
        txtKET.Text = sNAMEDISPLAY_POLI

        Dim oMasterDiagnosa As New Diagnosa.clsMasterDiagnosa
        Dim dsDiagnosa = oMasterDiagnosa.GetDataDiagnosaUtama(sNoId)
        If dsDiagnosa IsNot Nothing Then
            txtDIAGNOSA.EditValue = dsDiagnosa.KDDIAGNOSA
        End If

        'IGD
        Dim oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01
        Dim dsIGD = oS_DIGITAL_IGD_01.GetData(sNoId)
        If dsIGD IsNot Nothing Then
            txtKEADAANUMUM.Text = dsIGD.KEADAANUMUM
        End If



    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oPermintaanRuangan.GetData(sNoId)

            With ds
                txtKEADAANUMUM.Text = .KEADAANUMUM
                txtDIAGNOSA.EditValue = .DIAGNOSA.Trim
                txtTTV.Text = .TTV
                txtKET.Text = .KET
                grdDOCTOR.Text = .KDUSER_SIGNATURE
                'grdSEQ.Text = 
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If sNoId = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If
            If txtKEADAANUMUM.Text = String.Empty Then
                txtKEADAANUMUM.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKEADAANUMUM.ErrorText = Statement.ErrorRequired

                txtKEADAANUMUM.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtDIAGNOSA.Text = String.Empty Then
                txtDIAGNOSA.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtDIAGNOSA.ErrorText = Statement.ErrorRequired

                txtDIAGNOSA.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTTV.Text = String.Empty Then
                txtTTV.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTTV.ErrorText = Statement.ErrorRequired

                txtTTV.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtKET.Text = String.Empty Then
                txtKET.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKET.ErrorText = Statement.ErrorRequired

                txtKET.Focus()
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
            If grdKDRUANGRAWAT.Text = String.Empty Then
                grdKDRUANGRAWAT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDRUANGRAWAT.ErrorText = Statement.ErrorRequired

                grdKDRUANGRAWAT.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdSEQ.Text = String.Empty Then
                grdSEQ.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdSEQ.ErrorText = Statement.ErrorRequired

                grdSEQ.Focus()
                fn_Validate = False
                Exit Function
            End If

            If chkISTERISI.Checked = True Then
                grdSEQ.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdSEQ.ErrorText = Statement.ErrorRequired

                MsgBox("Tempat Tidur Sudah Terisi, Harap Memilih Tempat Tidur Lain.", MsgBoxStyle.Exclamation, Me.Text)

                grdSEQ.Focus()
                fn_Validate = False
                Exit Function
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsKunjungan = oPermintaanRuangan.GetDataByKunjungan(sNoId)
                If dsKunjungan IsNot Nothing Then
                    MsgBox("Sudah Input Resume Rawat Jalan " & dsKunjungan.KDKUNJUNGAN, MsgBoxStyle.Exclamation, Me.Text)
                    fn_Validate = False
                    Exit Function
                End If
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save() As Boolean
        Try
            ' ***** HEADER *****
            Dim ds = oPermintaanRuangan.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oPermintaanRuangan.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = Now
                .KDKUNJUNGAN = sNoId
                .KEADAANUMUM = txtKEADAANUMUM.Text.Trim.ToUpper
                .DIAGNOSA = txtDIAGNOSA.EditValue
                .TTV = txtTTV.Text.Trim.ToUpper
                .KET = txtKET.Text.Trim.ToUpper
                Try
                    .ISVERIFIKASI = oPermintaanRuangan.GetData(sNoId).ISVERIFIKASI
                Catch ex As Exception
                    .ISVERIFIKASI = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = grdDOCTOR.EditValue
                .KDRUANGRAWAT = grdKDRUANGRAWAT.EditValue
                .SEQ = grdSEQ.EditValue
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oPermintaanRuangan.InsertData(ds)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oPermintaanRuangan.UpdateData(ds)
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
    Private Sub fn_DIAGNOSA()
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
            SQL &= "M_DIAGNOSA A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "DIAGNOSA")

            txtDIAGNOSA.Properties.DataSource = ds.Tables("DIAGNOSA")
            txtDIAGNOSA.Properties.ValueMember = "KDDIAGNOSA"
            txtDIAGNOSA.Properties.DisplayMember = "MEMO"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_RUANGRAWAT()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            'SQL = "SELECT "
            'SQL &= "B.KDRUANGRAWAT "
            'SQL &= ",B.SEQ "
            'SQL &= ",RUANGAN = A.NAME_DISPLAY "
            'SQL &= ",TEMPATTIDUR = B.MEMO "
            'SQL &= ",B.KATEGORI "
            'SQL &= ",B.ISTERISI "
            'SQL &= "FROM "
            'SQL &= "DATABASE_NEW..M_RUANGRAWAT A "
            'SQL &= "INNER JOIN DATABASE_NEW..M_RUANGRAWAT_D B "
            'SQL &= "ON A.KDRUANGRAWAT = B.KDRUANGRAWAT "

            SQL = "SELECT "
            SQL &= "A.KDRUANGRAWAT "
            SQL &= ",A.NAME_DISPLAY "

            SQL &= "FROM DATABASE_NEW.dbo.M_RUANGRAWAT A "
            SQL &= "WHERE A.ISACTIVE = 1 "
            SQL &= "ORDER BY A.NAME_DISPLAY "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_RUANGRAWAT")

            grdKDRUANGRAWAT.Properties.DataSource = ds.Tables("M_RUANGRAWAT")
            grdKDRUANGRAWAT.Properties.ValueMember = "KDRUANGRAWAT"
            grdKDRUANGRAWAT.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdKDRUANGRAWAT_EditValueChanged(sender As Object, e As EventArgs) Handles grdKDRUANGRAWAT.EditValueChanged
        If isLoad = True Then
            If grdKDRUANGRAWAT.Text <> "" Then
                grdSEQ.ResetText()
                txtKET.ResetText()

                Try
                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String
                    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
                    oConn = New SqlConnection(sConn)

                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "SELECT "
                    SQL &= "A.KDRUANGRAWAT "
                    SQL &= ",B.SEQ "
                    SQL &= ",RUANGAN = A.NAME_DISPLAY "
                    SQL &= ",TEMPATTIDUR = B.MEMO "
                    SQL &= ",B.KATEGORI "
                    SQL &= ",B.ISTERISI "
                    SQL &= "FROM "
                    SQL &= "DATABASE_NEW..M_RUANGRAWAT A "
                    SQL &= "INNER JOIN DATABASE_NEW..M_RUANGRAWAT_D B "
                    SQL &= "ON A.KDRUANGRAWAT = B.KDRUANGRAWAT "
                    SQL &= "WHERE "
                    SQL &= "A.KDRUANGRAWAT = '" & grdKDRUANGRAWAT.EditValue & "' "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "M_RUANGRAWAT_NAME")

                    grdSEQ.Properties.DataSource = ds.Tables("M_RUANGRAWAT_NAME")
                    grdSEQ.Properties.ValueMember = "SEQ"
                    grdSEQ.Properties.DisplayMember = "TEMPATTIDUR"

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
    Private Sub grdSEQ_EditValueChanged(sender As Object, e As EventArgs) Handles grdSEQ.EditValueChanged
        If isLoad = True Then
            If grdSEQ.Text <> "" Then
                Try
                    Dim oConn As New SqlConnection
                    Dim oComm As New SqlCommand
                    Dim da As SqlDataAdapter
                    Dim ds As New DataSet
                    Dim SQL As String
                    Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
                    oConn = New SqlConnection(sConn)

                    If oConn.State = ConnectionState.Closed Then
                        oConn.Open()
                    End If

                    SQL = "SELECT "
                    SQL &= "B.KDRUANGRAWAT "
                    SQL &= ",B.SEQ "
                    SQL &= ",RUANGAN = A.NAME_DISPLAY "
                    SQL &= ",TEMPATTIDUR = B.MEMO "
                    SQL &= ",B.KATEGORI "
                    SQL &= ",B.ISTERISI "
                    SQL &= "FROM "
                    SQL &= "DATABASE_NEW..M_RUANGRAWAT A "
                    SQL &= "INNER JOIN DATABASE_NEW..M_RUANGRAWAT_D B "
                    SQL &= "ON A.KDRUANGRAWAT = B.KDRUANGRAWAT "
                    SQL &= "WHERE "
                    SQL &= "B.KDRUANGRAWAT = '" & grdKDRUANGRAWAT.EditValue & "' AND B.SEQ = " & grdSEQ.EditValue & " "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "M_RUANGRAWAT_NAME")

                    For iLoop As Integer = 0 To ds.Tables("M_RUANGRAWAT_NAME").Rows.Count - 1
                        With ds.Tables("M_RUANGRAWAT_NAME")
                            txtKET.Text = .Rows(iLoop)("RUANGAN") & " " & .Rows(iLoop)("TEMPATTIDUR")
                            chkISTERISI.Checked = .Rows(iLoop)("ISTERISI")
                        End With
                    Next

                    If oConn.State = ConnectionState.Open Then
                        oConn.Close()
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        End If
    End Sub
#End Region
End Class