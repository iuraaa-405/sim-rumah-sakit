Imports System.Data.SqlClient
Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports QRCoder

Public Class frmGeriatriEQ5D
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_ASKEPGERIATRI_EQ5D As New Digital.clsDigital_EQ5D
    Private down As Boolean = False
    Private sKDPENDAFTARAN As string
    Private sNoId As String
    Private sKDUSER As String
    Private sKDUSERSIGNATURE As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal NoId As String)
        oFormMode = FormMode

        sNoId = NoId

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

            Try
                Dim gen As New QRCodeGenerator
                Dim data = gen.CreateQrCode(sTandaTanganPasien, QRCodeGenerator.ECCLevel.Q)
                Dim code As New QRCode(data)
                PictureBox2.Image = code.GetGraphic(6)
            Catch ex As Exception
                PictureBox2.Visible = False
            End Try

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
        fn_LoadPerawat()
        fn_LoadDataHistory(txtNoPasien.Text)
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
        TextEdit1.Properties.ReadOnly = Status
        TextEdit2.Properties.ReadOnly = Status
        TextEdit3.Properties.ReadOnly = Status
        TextEdit4.Properties.ReadOnly = Status
        TextEdit5.Properties.ReadOnly = Status
        TextEdit6.Properties.ReadOnly = Status
        TextEdit7.Properties.ReadOnly = Status
        TextEdit8.Properties.ReadOnly = Status
        TextEdit9.Properties.ReadOnly = Status
        TextEdit10.Properties.ReadOnly = Status
        TextEdit11.Properties.ReadOnly = Status
        TextEdit12.Properties.ReadOnly = Status
        TextEdit13.Properties.ReadOnly = Status
        TextEdit14.Properties.ReadOnly = Status
        TextEdit15.Properties.ReadOnly = Status
        TextEdit16.Properties.ReadOnly = Status
        'PictureBox1.Properties.ReadOnly = Status
        'PictureBox2.Properties.ReadOnly = Status
        grdPerawat.Properties.ReadOnly = Status



    End Sub
    Private Sub fn_EmptyMe()
        deDATE.DateTime = Now
        
        CheckEdit1.Checked = False
        CheckEdit2.Checked = False
        CheckEdit3.Checked = False
        CheckEdit4.Checked = False
        CheckEdit5.Checked = False
        CheckEdit6.Checked = False
        CheckEdit7.Checked = False
        CheckEdit8.Checked = False
        CheckEdit9.Checked = False
        CheckEdit10.Checked = False
        CheckEdit11.Checked = False
        CheckEdit12.Checked = False
        CheckEdit13.Checked = False
        CheckEdit14.Checked = False
        CheckEdit15.Checked = False
        TextEdit1.ResetText()
        TextEdit2.ResetText()
        TextEdit3.ResetText()
        TextEdit4.ResetText()
        TextEdit5.ResetText()
        TextEdit6.ResetText()
        TextEdit7.ResetText()
        TextEdit8.ResetText()
        TextEdit9.ResetText()
        TextEdit10.ResetText()
        TextEdit11.ResetText()
        TextEdit12.ResetText()
        TextEdit13.ResetText()
        TextEdit14.ResetText()
        TextEdit15.ResetText()
        TextEdit16.ResetText()
        'PictureBox1.ResetText()
        'PictureBox2.ResetText()
        'grdPerawat.ResetText()


    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoid)
            With ds
                deDATE.DateTime = .DATE
                
                CheckEdit1.Checked = .KONDISIKESEHATAN1
                CheckEdit2.Checked = .KONDISIKESEHATAN2
                CheckEdit3.Checked = .KONDISIKESEHATAN3
                CheckEdit4.Checked = .KONDISIKESEHATAN4
                CheckEdit5.Checked = .KONDISIKESEHATAN5
                CheckEdit6.Checked = .KONDISIKESEHATAN6
                CheckEdit7.Checked = .KONDISIKESEHATAN7
                CheckEdit8.Checked = .KONDISIKESEHATAN8
                CheckEdit9.Checked = .KONDISIKESEHATAN9
                CheckEdit10.Checked = .KONDISIKESEHATAN10
                CheckEdit11.Checked = .KONDISIKESEHATAN11
                CheckEdit12.Checked = .KONDISIKESEHATAN12
                CheckEdit13.Checked = .KONDISIKESEHATAN13
                CheckEdit14.Checked = .KONDISIKESEHATAN14
                CheckEdit15.Checked = .KONDISIKESEHATAN15
                TextEdit1.Text = .SKOR1
                TextEdit2.Text = .SKOR2
                TextEdit3.Text = .SKOR3
                TextEdit4.Text = .SKOR4
                TextEdit5.Text = .SKOR5
                TextEdit6.Text = .SKOR6
                TextEdit7.Text = .SKOR7
                TextEdit8.Text = .SKOR8
                TextEdit9.Text = .SKOR9
                TextEdit10.Text = .SKOR10
                TextEdit11.Text = .SKOR11
                TextEdit12.Text = .SKOR12
                TextEdit13.Text = .SKOR13
                TextEdit14.Text = .SKOR14
                TextEdit15.Text = .SKOR15
                TextEdit16.Text = .SKOR16
                grdPerawat.Text = .PERAWAT

                Try
                    PictureBox1.Image = ByteArrayToImage(.GAMBAR1.ToArray())
                Catch oErr As Exception
                End Try

                Try
                    PictureBox2.Image = ByteArrayToImage(.GAMBAR2.ToArray())
                Catch oErr As Exception
                End Try

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
                .KDINDEKS = sNoId
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .KDCUSTOMER = txtNoPasien.Text
                Try
                    .DATECREATED = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .KONDISIKESEHATAN1 = CheckEdit1.Checked
                .KONDISIKESEHATAN2 = CheckEdit2.Checked
                .KONDISIKESEHATAN3 = CheckEdit3.Checked
                .KONDISIKESEHATAN4 = CheckEdit4.Checked
                .KONDISIKESEHATAN5 = CheckEdit5.Checked
                .KONDISIKESEHATAN6 = CheckEdit6.Checked
                .KONDISIKESEHATAN7 = CheckEdit7.Checked
                .KONDISIKESEHATAN8 = CheckEdit8.Checked
                .KONDISIKESEHATAN9 = CheckEdit9.Checked
                .KONDISIKESEHATAN10 = CheckEdit10.Checked
                .KONDISIKESEHATAN11 = CheckEdit11.Checked
                .KONDISIKESEHATAN12 = CheckEdit12.Checked
                .KONDISIKESEHATAN13 = CheckEdit13.Checked
                .KONDISIKESEHATAN14 = CheckEdit14.Checked
                .KONDISIKESEHATAN15 = CheckEdit15.Checked
                .SKOR1 = TextEdit1.Text
                .SKOR2 = TextEdit2.Text
                .SKOR3 = TextEdit3.Text
                .SKOR4 = TextEdit4.Text
                .SKOR5 = TextEdit5.Text
                .SKOR6 = TextEdit6.Text
                .SKOR7 = TextEdit7.Text
                .SKOR8 = TextEdit8.Text
                .SKOR9 = TextEdit9.Text
                .SKOR10 = TextEdit10.Text
                .SKOR11 = TextEdit11.Text
                .SKOR12 = TextEdit12.Text
                .SKOR13 = TextEdit13.Text
                .SKOR14 = TextEdit14.Text
                .SKOR15 = TextEdit15.Text
                .SKOR16 = TextEdit16.Text
                '.GAMBAR1 = PictureBox1.Text
                '.GAMBAR2 = PictureBox2.Text
                .PERAWAT = grdPerawat.Text

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox1.Image.Save(ms, PictureBox1.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .GAMBAR1 = data
                Catch oErr As Exception
                    Try
                        .GAMBAR1 = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId).GAMBAR1
                    Catch ex As Exception

                    End Try
                End Try

                Try
                    Dim ms As New IO.MemoryStream()
                    PictureBox2.Image.Save(ms, PictureBox2.Image.RawFormat)

                    Dim data As Byte() = ms.GetBuffer()

                    .GAMBAR2 = data
                Catch oErr As Exception
                    Try
                        .GAMBAR2 = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(sNoId).GAMBAR2
                    Catch ex As Exception

                    End Try
                End Try


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
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_ASKEPGERIATRI_EQ5D.UpdateData(ds)
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

    Private Sub PictureBox1_Click(sender As Object, e As MouseEventArgs) Handles PictureBox1.Click
        Dim GridColor As Color = Color.Red
        Dim GridPen As New Pen(GridColor)
        GridPen.Width = 2

        Dim X As Integer = e.X - 10
        Dim Y As Integer = e.Y - 10

        Using g As Graphics = Graphics.FromImage(PictureBox1.Image)
            'g.DrawLine(GridPen, _Previous.Value, e.Location)
            g.DrawEllipse(GridPen, X, Y, 20, 20)
        End Using
        PictureBox1.Invalidate()
    End Sub

    Private Sub ResetGambarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetGambarToolStripMenuItem.Click
        PictureBox1.Image = CType(My.Resources.ResourceManager.GetObject("eq5d"), Image)
    End Sub

   Private Sub fn_LoadPerawat()
        Try
            Dim sKoneksiOld As String = String.Empty
            Dim oSetKoneksi As New Setting.clsSetKoneksi
            Dim dsSetKoneksi = oSetKoneksi.GetData()
            If dsSetKoneksi IsNot Nothing Then
                sKoneksiOld = dsSetKoneksi.GENERATE_ECLAIM
            End If

            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = sKoneksiOld
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "* "
            SQL &= "FROM "
            SQL &= "M_STAFF A "
            SQL &= "WHERE "
            SQL &= "A.ISACTIVE = 1 "
            SQL &= "AND KELOMPOKIPK = 'NAKES'"
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "PERAWAT")

            grdPerawat.Properties.DataSource = ds.Tables("PERAWAT")
            grdPerawat.Properties.ValueMember = "NAME_DISPLAY"
            grdPerawat.Properties.DisplayMember = "NAME_DISPLAY"


            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

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
            SQL &= "A.KDINDEKS "
            SQL &= ",A.KDKUNJUNGAN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE  "
            SQL &= ",A.KDUSER "
            SQL &= "FROM  "
            SQL &= "S_DIGITAL_ASKEPGERIATRI_EQ5D A "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN B  "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN  "
            SQL &= "WHERE b.KDCUSTOMER = '" & KDCUSTOMER & "'  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_GERIATRIEQ5D")

            grd_Riwayat_EQ5D.MainView = grv_Riwayat_EQ5D
            grd_Riwayat_EQ5D.DataSource = ds.Tables("R_GERIATRIEQ5D")
            grd_Riwayat_EQ5D.ForceInitialize()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Preview Data Dokter: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub CopyEQ5DToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CopyEQ5DToolStripMenuItem.Click
        If grv_Riwayat_EQ5D.GetFocusedRowCellValue("KDINDEKS") Is Nothing Then
            Exit Sub
        End If
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_ASKEPGERIATRI_EQ5D.GetData(grv_Riwayat_EQ5D.GetFocusedRowCellValue("KDINDEKS"))
            With ds
                deDATE.DateTime = .DATE

                CheckEdit1.Checked = .KONDISIKESEHATAN1
                CheckEdit2.Checked = .KONDISIKESEHATAN2
                CheckEdit3.Checked = .KONDISIKESEHATAN3
                CheckEdit4.Checked = .KONDISIKESEHATAN4
                CheckEdit5.Checked = .KONDISIKESEHATAN5
                CheckEdit6.Checked = .KONDISIKESEHATAN6
                CheckEdit7.Checked = .KONDISIKESEHATAN7
                CheckEdit8.Checked = .KONDISIKESEHATAN8
                CheckEdit9.Checked = .KONDISIKESEHATAN9
                CheckEdit10.Checked = .KONDISIKESEHATAN10
                CheckEdit11.Checked = .KONDISIKESEHATAN11
                CheckEdit12.Checked = .KONDISIKESEHATAN12
                CheckEdit13.Checked = .KONDISIKESEHATAN13
                CheckEdit14.Checked = .KONDISIKESEHATAN14
                CheckEdit15.Checked = .KONDISIKESEHATAN15
                TextEdit1.Text = .SKOR1
                TextEdit2.Text = .SKOR2
                TextEdit3.Text = .SKOR3
                TextEdit4.Text = .SKOR4
                TextEdit5.Text = .SKOR5
                TextEdit6.Text = .SKOR6
                TextEdit7.Text = .SKOR7
                TextEdit8.Text = .SKOR8
                TextEdit9.Text = .SKOR9
                TextEdit10.Text = .SKOR10
                TextEdit11.Text = .SKOR11
                TextEdit12.Text = .SKOR12
                TextEdit13.Text = .SKOR13
                TextEdit14.Text = .SKOR14
                TextEdit15.Text = .SKOR15
                TextEdit16.Text = .SKOR16
                grdPerawat.Text = .PERAWAT

                Try
                    PictureBox1.Image = ByteArrayToImage(.GAMBAR1.ToArray())
                Catch oErr As Exception
                End Try

                Try
                    PictureBox2.Image = ByteArrayToImage(.GAMBAR2.ToArray())
                Catch oErr As Exception
                End Try

                sKDUSER = .KDUSER
                sKDUSERSIGNATURE = .KDUSER_SIGNATURE


            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmGeriatriEQ5D_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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