Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports QRCoder

Public Class frmSkriningGeriatri
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_EQ5D As New Digital.clsDigital_SkriningGeriatri
    Private down As Boolean = False
    Private sKDPENDAFTARAN As String
    Private sNoId As String
    Private sKDUSER As String
    Private sKDUSERSIGNATURE As String
     
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal NoId As String)
        oFormMode = FormMode

        sNoId = KDREG

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtJenisKelamin.Text = dsPendaftaran.JENISKELAMIN
            sKDPENDAFTARAN = dsPendaftaran.KDPENDAFTARAN

            deDATE.DateTime = Now

            sKDUSER = sUserID
            sKDUSERSIGNATURE = sUserSIGNATURE

        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtJenisKelamin.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        sCode = txtNoRegister.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
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

        deDATE.ReadOnly = Status

        ChkKondisiPasien1Ya.Properties.ReadOnly = Status
        ChkKondisiPasien1Tidak.Properties.ReadOnly = Status
        ChkKondisiPasien2Ya.Properties.ReadOnly = Status
        ChkKondisiPasien2Tidak.Properties.ReadOnly = Status
        ChkKondisiPasien3Ya.Properties.ReadOnly = Status
        ChkKondisiPasien3Tidak.Properties.ReadOnly = Status



    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now

        ChkKondisiPasien1Ya.Checked = False
        ChkKondisiPasien1Tidak.Checked = False
        ChkKondisiPasien2Ya.Checked = False
        ChkKondisiPasien2Tidak.Checked = False
        ChkKondisiPasien3Ya.Checked = False
        ChkKondisiPasien3Tidak.Checked = False



    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId)
            With ds
                deDATE.DateTime = .DATE

                ChkKondisiPasien1Ya.Checked = .S_DIGITAL_1
                ChkKondisiPasien1Tidak.Checked = .S_DIGITAL_2
                ChkKondisiPasien2Ya.Checked = .S_DIGITAL_3
                ChkKondisiPasien2Tidak.Checked = .S_DIGITAL_4
                ChkKondisiPasien3Ya.Checked = .S_DIGITAL_5
                ChkKondisiPasien3Tidak.Checked = .S_DIGITAL_6


                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE


            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function ByteArrayToImage(ByVal byteArrayIn() As Byte) As Image
        Using ms As New System.IO.MemoryStream(byteArrayIn)
            Dim returnImage = Image.FromStream(ms)
            Return returnImage
        End Using
    End Function
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
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
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .S_DIGITAL_1 = ChkKondisiPasien1Ya.Checked
                .S_DIGITAL_2 = ChkKondisiPasien1Tidak.Checked
                .S_DIGITAL_3 = ChkKondisiPasien2Ya.Checked
                .S_DIGITAL_4 = ChkKondisiPasien2Tidak.Checked
                .S_DIGITAL_5 = ChkKondisiPasien3Ya.Checked
                .S_DIGITAL_6 = ChkKondisiPasien3Tidak.Checked


                Try
                    .CETAK = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sKDUSER
                .KDUSER_SIGNATURE = sKDUSERSIGNATURE

            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_EQ5D.InsertData(ds)

                    If ds.S_DIGITAL_1 = True And ds.S_DIGITAL_3 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If

                    If ds.S_DIGITAL_1 = True And ds.S_DIGITAL_5 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If

                    If ds.S_DIGITAL_3 = True And ds.S_DIGITAL_5 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If

                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_EQ5D.UpdateData(ds)

                    fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "")

                    If ds.S_DIGITAL_1 = True And ds.S_DIGITAL_3 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If

                    If ds.S_DIGITAL_1 = True And ds.S_DIGITAL_5 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If

                    If ds.S_DIGITAL_3 = True And ds.S_DIGITAL_5 = True Then
                        fn_UpdateStatusPendaftaran(ds.KDPENDAFTARAN, "GERIATRI")
                    End If
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub fn_UpdateStatusPendaftaran(ByVal KDPENDAFTARAN As String, ByVal STATUS As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.210;Initial Catalog=DATABASE_NEW;Persist Security Info=True;User ID=sa;Password=dust1r@@"

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "UPDATE S_PENDAFTARAN_H SET STATUSLAIN = '"& STATUS &"' "
            SQL &= "WHERE KDPENDAFTARAN = '" & KDPENDAFTARAN & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "UPDATEPENDAFTARAN")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Gagal Update : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
            Case Keys.PageUp
	            fn_ScrollPage(True)
            Case Keys.PageDown
	            fn_ScrollPage(False)
        End Select
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & "?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox("Save " & txtNoRegister.Text.Trim.ToUpper & " success!", MsgBoxStyle.Information, Me.Text)
            Me.Close()
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub

#End Region
#Region "Lookup / Event"

    'Private _Previous As System.Nullable(Of Point) = Nothing
    'Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
    '    _Previous = e.Location
    '    PictureBox1_MouseMove(sender, e)
    'End Sub

    'Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
    '    If _Previous IsNot Nothing Then
    '        Dim GridColor As Color = Color.Red
    '        Dim GridPen As New Pen(GridColor)
    '        GridPen.Width = 2

    '        Dim X As Integer = e.X - 10
    '        Dim Y As Integer = e.Y - 10

    '        Using g As Graphics = Graphics.FromImage(PictureBox1.Image)
    '            'g.DrawLine(GridPen, _Previous.Value, e.Location)
    '            g.DrawEllipse(GridPen,X,Y,20,20)
    '        End Using
    '        PictureBox1.Invalidate()
    '        _Previous = e.Location
    '    End If
    'End Sub

    'Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
    '    _Previous = Nothing
    'End Sub

    Private Sub fn_LoadDataHistory(ByVal KDCUSTOMER As String)
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
            SQL &= "A.KDKUNJUNGAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE  "
            SQL &= ",A.KDUSER "
            SQL &= "FROM  "
            SQL &= "S_DIGITAL_ASKEPSKRININGGERIATRI A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B  "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN  "
            SQL &= "WHERE b.KDCUSTOMER = '" & KDCUSTOMER & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_GERIATRI")

            grd_Riwayat_Skinning.MainView = grv_Riwayat_Skinning
            grd_Riwayat_Skinning.DataSource = ds.Tables("R_GERIATRI")
            grd_Riwayat_Skinning.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CopySkrinningToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopySkrinningToolStripMenuItem.Click
        If grv_Riwayat_Skinning.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(grv_Riwayat_Skinning.GetFocusedRowCellValue("KDKUNJUNGAN"))
            With ds
                deDATE.DateTime = .DATE

                ChkKondisiPasien1Ya.Checked = .S_DIGITAL_1
                ChkKondisiPasien1Tidak.Checked = .S_DIGITAL_2
                ChkKondisiPasien2Ya.Checked = .S_DIGITAL_3
                ChkKondisiPasien2Tidak.Checked = .S_DIGITAL_4
                ChkKondisiPasien3Ya.Checked = .S_DIGITAL_5
                ChkKondisiPasien3Tidak.Checked = .S_DIGITAL_6


                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE


            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmSkriningGeriatri_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
	    If e.Delta > 0 Then
		    'up
		    fn_ScrollPage(True)
	    Else
		    'down
		    fn_ScrollPage(False)
	    End If
    End Sub

    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
	    Dim myView As Point = Me.Panel3.AutoScrollPosition
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

	    Me.Panel3.AutoScrollPosition = myView
    End Sub

#End Region
End Class