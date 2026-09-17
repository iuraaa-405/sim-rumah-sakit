Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class frmEMedrekRI_11
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_11 As New Digital.clsDigital_RI_11
    Private sNoid As String = ""
    Private sKoneksi As String = String.Empty
    Private sDOCTOR As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDKUNJUNGAN As String)
        oFormMode = FormMode
        sNoid = KDKUNJUNGAN

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDKUNJUNGAN)

        txtNoRegister.Text = KDKUNJUNGAN

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.ToString.Trim.ToUpper
            txtUmur.Text = dsPendaftaran.USIA
            txtTanggalDaftar.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy HH:mm:ss")
            txtTujuan.Text = dsPendaftaran.TUJUAN
            txtDokter.Text = dsPendaftaran.DOKTER
            sKDPENDAFTARAN = dsPendaftaran.KDPENDAFTARAN
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtTanggalDaftar.ResetText()
            txtNoRegister.ResetText()
            txtTujuan.ResetText()
            txtDokter.ResetText()
        End If

        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If
        Dim oSetUser As New Setting.clsUser
        Dim dsSetUser = oSetUser.GetData(sUserID)
        If dsSetUser IsNot Nothing Then
            sDOCTOR = dsSetUser.KDDOCTOR
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
        fn_Doctor()

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

        'txtNOMOR.Properties.ReadOnly = Status
        deDATE.Properties.ReadOnly = Status
        grdDOCTOR.Properties.ReadOnly = Status
        txtNAMADOKTERRUJUK.Properties.ReadOnly = Status
        txtBAGIAN.Properties.ReadOnly = Status
        txtRSRUJUK.Properties.ReadOnly = Status
        txtPEMERIKSAAN_01.Properties.ReadOnly = Status
        txtPEMERIKSAAN_02.Properties.ReadOnly = Status
        txtPEMERIKSAAN_03.Properties.ReadOnly = Status
        txtPEMERIKSAAN_04.Properties.ReadOnly = Status
        txtPEMERIKSAAN_05.Properties.ReadOnly = Status
        txtPEMERIKSAAN_06.Properties.ReadOnly = Status
        txtPEMERIKSAAN_07.Properties.ReadOnly = Status
        txtPEMERIKSAAN_08.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtNOMOR.ResetText()
        deDATE.DateTime = Now
        grdDOCTOR.ResetText()
        txtNAMADOKTERRUJUK.ResetText()
        txtNORUJUKAN.ResetText()
        txtBAGIAN.ResetText()
        txtRSRUJUK.ResetText()
        txtPEMERIKSAAN_01.ResetText()
        txtPEMERIKSAAN_02.ResetText()
        txtPEMERIKSAAN_03.ResetText()
        txtPEMERIKSAAN_04.ResetText()
        txtPEMERIKSAAN_05.ResetText()
        txtPEMERIKSAAN_06.ResetText()
        txtPEMERIKSAAN_07.ResetText()
        txtPEMERIKSAAN_08.ResetText()
        grdDOCTOR.Text = sDOCTOR

        Dim oS_DIGITAL_IGD_01 As New Digital.clsDigital_IGD_01

        Dim dsIGD1 = oS_DIGITAL_IGD_01.GetData(txtNoRegister.Text)

        If dsIGD1 IsNot Nothing Then
            txtPEMERIKSAAN_01.Text = dsIGD1.RIWAYAT
            txtPEMERIKSAAN_06.Text = dsIGD1.KDDIAGNOSA
            txtPEMERIKSAAN_02.Text = "Kepala : " & dsIGD1.SURVEY_KEPALA_2 & vbCrLf & "Mata : " & dsIGD1.SURVEY_MATA_2 & vbCrLf & 
                                     "Leher : " & dsIGD1.SURVEY_LEHER_2 & vbCrLf & "Dada : " & dsIGD1.SURVEY_DADA_2 & vbCrLf & 
                                     "Leher : " & dsIGD1.SURVEY_LEHER_2 & vbCrLf & "Perut : " & dsIGD1.SURVEY_PERUT_2 & vbCrLf & 
                                     "Alat Gerak : " & dsIGD1.SURVEY_ALATGERAK_2
        Else
            txtPEMERIKSAAN_01.Text = ""
            txtPEMERIKSAAN_06.Text = ""
            txtPEMERIKSAAN_02.Text = ""
        End If

        txtPEMERIKSAAN_07.Text = fn_LoadPengobatan()

        
    End Sub
    Private Function fn_LoadPengobatan() As String
        Try
            Dim oIdentitas As New Identitas.clsIdentitasPasien
            Dim oResep As New Inventory.clsOrderResep
            Dim listObat As New List(Of String)

            For Each yloop In oResep.GetDataDetilList(txtNoRegister.Text)
                If yloop.KDITEM = "" Then
                    listObat.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Else
                    If fn_KodeItemAlkes(yloop.KDITEM) = False Then
                        listObat.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                    End If
                End If
            Next

            fn_LoadPengobatan = String.Join(vbCrLf, listObat.ToArray)
        Catch ex As Exception
            fn_LoadPengobatan = ""
            'MsgBox("Reload Hasil Obat" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_KodeItemAlkes(ByVal KDITEM As String) As Boolean
        Try
            fn_KodeItemAlkes = False

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
            SQL &= "A.DESCRIPTION "
            SQL &= "FROM "
            SQL &= "DUSTIRA_FARMASI.dbo.M_ITEM_L6 A "
            SQL &= "INNER JOIN DUSTIRA_FARMASI.dbo.M_ITEM B "
            SQL &= "ON A.KDITEM_L6 = B.KDITEM_L6 "
            SQL &= "WHERE "
            SQL &= "B.KDITEM = '" & KDITEM & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "GETDATARADIOLOGI")


            For iLoop As Integer = 0 To ds.Tables("GETDATARADIOLOGI").Rows.Count - 1
                With ds.Tables("GETDATARADIOLOGI")
                    If .Rows(iLoop)("DESCRIPTION").ToString.Contains("OBAT") Then
                        fn_KodeItemAlkes = False
                    Else
                        fn_KodeItemAlkes = True
                    End If
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch ex As Exception
            fn_KodeItemAlkes = False
            MsgBox("Get Kode Alkes" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_11.GetData(sNoid)

            With ds
                'txtNOMOR.Text = ""
                deDATE.DateTime = .DATE
                grdDOCTOR.Text = .DOCTOR_KODE
                txtNAMADOKTERRUJUK.Text = .NAMA_DOKTER_RUJUKAN
                txtBAGIAN.Text = .BAGIAN
                txtRSRUJUK.Text = .NAMA_RS_RUJUKAN
                txtPEMERIKSAAN_01.Text = .PEMERIKSAAN_01
                txtPEMERIKSAAN_02.Text = .PEMERIKSAAN_02
                txtPEMERIKSAAN_03.Text = .PEMERIKSAAN_03
                txtPEMERIKSAAN_04.Text = .PEMERIKSAAN_04
                txtPEMERIKSAAN_05.Text = .PEMERIKSAAN_05
                txtPEMERIKSAAN_06.Text = .PEMERIKSAAN_06
                txtPEMERIKSAAN_07.Text = .PEMERIKSAAN_07
                txtPEMERIKSAAN_08.Text = .PEMERIKSAAN_08
                txtNORUJUKAN.Text = .NOMOR
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
            Dim ds = oS_DIGITAL_RI_11.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RI_11.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .NOMOR = txtNORUJUKAN.Text
                .DATE = deDATE.DateTime
                .DOCTOR_KODE = grdDOCTOR.EditValue
                .DOCTOR_NAME_DISPLAY = grdDOCTOR.Text
                .NAMA_DOKTER_RUJUKAN = txtNAMADOKTERRUJUK.Text
                .BAGIAN = txtBAGIAN.Text
                .NAMA_RS_RUJUKAN = txtRSRUJUK.Text
                .PEMERIKSAAN_01 = txtPEMERIKSAAN_01.Text
                .PEMERIKSAAN_02 = txtPEMERIKSAAN_02.Text
                .PEMERIKSAAN_03 = txtPEMERIKSAAN_03.Text
                .PEMERIKSAAN_04 = txtPEMERIKSAAN_04.Text
                .PEMERIKSAAN_05 = txtPEMERIKSAAN_05.Text
                .PEMERIKSAAN_06 = txtPEMERIKSAAN_06.Text
                .PEMERIKSAAN_07 = txtPEMERIKSAAN_07.Text
                .PEMERIKSAAN_08 = txtPEMERIKSAAN_08.Text

                Try
                    .CETAK = oS_DIGITAL_RI_11.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try
                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_11.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_11.UpdateData(ds)
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
    Private Sub fn_NOIDUSER()
    End Sub
    Private Sub fn_Doctor()
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
            da.Fill(ds, "DOCTOR")

            grdDOCTOR.Properties.DataSource = ds.Tables("DOCTOR")
            grdDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmEMedrekRI_11_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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

    Private Sub btnBrowseHasilLab_Click(sender As Object, e As EventArgs) Handles btnBrowseHasilLab.Click
        Dim frmBrowseOrder As New frmBrowseOrder
        'sKUNJUNGANORDER = String.Empty
        frmBrowseOrder.fn_LoadKategori(sKDPENDAFTARAN, 0)
        frmBrowseOrder.ShowDialog(Me)

        fn_LoadHasilLab2(sKDITEMORDER)
        sKDITEMORDER.Clear()
    End Sub
    Private Sub fn_LoadHasilLab2(ByVal sKDORDER As List(Of String))
        Try
            Dim listLab As New List(Of String)
            listLab.Clear()
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\EMEDREK\", "Database", "").ToString())
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            For Each iLoop In sKDORDER.Distinct
                ds = New DataSet
                If iLoop.Contains("PD") Or iLoop.Contains("CD") Or iLoop.Contains("FD") Then

                    SQL = "SELECT "
                    SQL &= "A.KDTARIF "
                    SQL &= ",A.HASIL "
                    SQL &= ",A.TGLTERIMA "

                    SQL &= "FROM "
                    SQL &= "DUSTIRA_FARMASI.dbo.S_HASILLABGAB_H_PA A "
                    SQL &= "WHERE "
                    SQL &= "A.NOLAB = '" & iLoop & "'  "
                    SQL &= "ORDER BY A.SEQ ASC "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "GETDATATRANSAKSI_ITEM")

                    Dim listItemLab As New List(Of String)
                    listItemLab.Clear()
                    Dim tgl As DateTime = Now
                    Dim tgl_tex As String = String.Empty

                    For xLoop As Integer = 0 To ds.Tables("GETDATATRANSAKSI_ITEM").Rows.Count - 1
                        With ds.Tables("GETDATATRANSAKSI_ITEM")
                            listItemLab.Add(IIf(.Rows(xLoop)("HASIL") = String.Empty, "", .Rows(xLoop)("KDTARIF") & ": " & .Rows(xLoop)("HASIL")))

                            Try
                                tgl = .Rows(xLoop)("TGLTERIMA")
                                tgl_tex = tgl.ToString("dd-MM-yyyy")
                            Catch ex As Exception
                                tgl_tex = ""
                            End Try

                        End With
                    Next

                    listLab.Add("(-) " & tgl_tex & " : " & String.Join(", ", listItemLab.ToArray))

                Else

                    SQL = "SELECT "
                    SQL &= "A.PEMERIKSAAN "
                    SQL &= ",A.HASIL "
                    SQL &= ",A.SATUAN "
                    SQL &= ",A.TGLTERIMA "

                    SQL &= "FROM "
                    SQL &= "DUSTIRA_FARMASI.dbo.S_HASILLABGAB_H A "
                    SQL &= "WHERE "
                    SQL &= "A.NOLAB = '" & iLoop & "'  "
                    SQL &= "ORDER BY A.URUT ASC "

                    oComm.Connection = oConn
                    oComm.CommandText = SQL
                    oComm.CommandTimeout = 120
                    oComm.CommandType = CommandType.Text

                    da = New SqlDataAdapter(oComm)
                    da.Fill(ds, "GETDATATRANSAKSI_ITEM")

                    Dim listItemLab As New List(Of String)
                    listItemLab.Clear()
                    Dim tgl As DateTime = Now
                    Dim tgl_tex As String = String.Empty

                    For xLoop As Integer = 0 To ds.Tables("GETDATATRANSAKSI_ITEM").Rows.Count - 1
                        With ds.Tables("GETDATATRANSAKSI_ITEM")
                            listItemLab.Add(IIf(.Rows(xLoop)("HASIL") = String.Empty, .Rows(xLoop)("PEMERIKSAAN"), .Rows(xLoop)("PEMERIKSAAN") & ": " & .Rows(xLoop)("HASIL") & " " & .Rows(xLoop)("SATUAN")))

                            Try
                                tgl = .Rows(xLoop)("TGLTERIMA")
                                tgl_tex = tgl.ToString("dd-MM-yyyy")
                            Catch ex As Exception
                                tgl_tex = ""
                            End Try

                        End With
                    Next

                    listLab.Add("(-) " & tgl_tex & " : " & String.Join(", ", listItemLab.ToArray))
                End If
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If listLab.Count > 0 Then
                txtPEMERIKSAAN_03.Text = IIf(txtPEMERIKSAAN_03.Text <> "", txtPEMERIKSAAN_03.Text & vbCrLf, txtPEMERIKSAAN_03.Text.Trim) & "Laboratorium : " & String.Join(vbCrLf, listLab.ToArray)
                'txtHASILLAB.Text = "Laboratorium : " & String.Join(vbCrLf, listLab.ToArray)
            End If



        Catch ex As Exception
            MsgBox("Reload Hasil Laboratorium" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnBrowseHasilRad_Click(sender As Object, e As EventArgs) Handles btnBrowseHasilRad.Click
        Dim frmBrowseOrder As New frmBrowseOrder
        'sKUNJUNGANORDER = String.Empty
        frmBrowseOrder.fn_LoadKategori(sKDPENDAFTARAN, 1)
        frmBrowseOrder.ShowDialog(Me)

        fn_LoadHasilRadiologi2(sKDITEMORDER)
        sKDITEMORDER.Clear()
    End Sub
    Private Sub fn_LoadHasilRadiologi2(ByVal sParameter As List(Of String))
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

            Dim listRad As New List(Of String)

            For Each xLoop In sParameter.Distinct
                ds = New DataSet

                SQL = "SELECT "
                SQL &= "A.* "
                SQL &= ",NAMATARIF = ISNULL((SELECT RTRIM(TARIFKT) FROM DUSTIRA_FARMASI.dbo.M_TARIFRS WHERE A.KDITEMTARIF = KDITEMTARIF), '-') "
                SQL &= "FROM "
                SQL &= "DUSTIRA_FARMASI.dbo.S_RADIOLOGI_H A "
                SQL &= "WHERE "
                SQL &= "A.KDRADIOLOGI = '" & xLoop & "' "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "GETDATARADIOLOGI")


                Dim Kesimpulan As String = String.Empty

                For iLoop As Integer = 0 To ds.Tables("GETDATARADIOLOGI").Rows.Count - 1
                    With ds.Tables("GETDATARADIOLOGI")
                        'Kesimpulan = .Rows(iLoop)("NAMATARIF")
                        'Kesimpulan = .Rows(iLoop)("NAMATARIF").IndexOf("Kesimpulan:")

                        Dim rtf As String = .Rows(iLoop)("DESCRIPTION_RTF")
                        Dim rBox As New RichTextBox
                        rBox.Rtf = rtf
                        Dim txt As String = ""

                        For i = 0 To rBox.Lines.Length - 1
                            If txt = "" Then
                                txt = rBox.Lines(i)
                            Else
                                txt = txt & " \r\n " & rBox.Lines(i)
                            End If
                        Next

                        Dim pattern As String = "(kesimpulan|Kesimpulan|KESIMPULAN|kesan|Kesan|KESAN)( |)(:|)(.*)"

                        Dim rgx As Regex = New Regex(pattern)
                        Dim match = Regex.Matches(txt, pattern)
                        If (match.Count > 0) Then
                            txt = match(0).Groups(4).Value.Replace(" \r\n ", vbNewLine).Trim()
                        Else
                            txt = txt.Replace(" \r\n ", vbNewLine).Trim()
                        End If


                        listRad.Add(.Rows(iLoop)("NAMATARIF") & ": " & txt)
                    End With
                Next

            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            If listRad.Count > 0 Then
                txtPEMERIKSAAN_04.Text = IIf(txtPEMERIKSAAN_04.Text <> "", txtPEMERIKSAAN_04.Text & vbCrLf, txtPEMERIKSAAN_04.Text.Trim) & "Radiologi : " & String.Join(vbCrLf, listRad.ToArray)
            End If

        Catch ex As Exception
            MsgBox("Reload Hasil Radiologi" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class