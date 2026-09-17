Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient
Imports DevExpress.XtraGrid
Imports System.IO

Public Class frmEMedrekRI_36
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RI_36 As New Digital.clsS_DIGITAL_RI_36
    Private down As Boolean = False
    Private sKdkunjungan As String
    Private sKdpendaftaran As String
    Private sKdcustomer As String
    Private sIsOtority As Boolean = False
    Private oMerge As New Setting.clsMergeiTextSharp
    Private dataList As New List(Of Byte())

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNoPasien.Text = dsPendaftaran.KDCUSTOMER
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN
            txtUmur.Text = dsPendaftaran.USIA
            txtRuangan.Text = dsPendaftaran.TUJUAN
            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN

            sKdkunjungan = dsPendaftaran.KDKUNJUNGAN
            sKdpendaftaran = dsPendaftaran.KDPENDAFTARAN
            sKdcustomer = dsPendaftaran.KDCUSTOMER

            fn_loadHistory(dsPendaftaran.KDCUSTOMER)

            'txtTujuan.Text = dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY
            'txtDokter.Text = dsPendaftaran.M_DOCTOR.NAME_DISPLAY
        Else
            txtNoPasien.ResetText()
            txtNamaPasien.ResetText()
            txtUmur.ResetText()
            txtRuangan.ResetText()
            txtNoRegister.ResetText()

            ' txtTujuan.ResetText()
            ' txtDokter.ResetText()
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
        fn_NOIDUSER()

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

        SDIGITAL_1.Properties.ReadOnly = Status
        SDIGITAL_2.Properties.ReadOnly = Status
        SDIGITAL_3.Properties.ReadOnly = Status
        SDIGITAL_4.Properties.ReadOnly = Status
        SDIGITAL_5.Properties.ReadOnly = Status
        SDIGITAL_6.Properties.ReadOnly = Status
        SDIGITAL_7.Properties.ReadOnly = Status
        SDIGITAL_8.Properties.ReadOnly = Status
        SDIGITAL_9.Properties.ReadOnly = Status
        SDIGITAL_10.Properties.ReadOnly = Status
        SDIGITAL_11.Properties.ReadOnly = Status
        SDIGITAL_12.Properties.ReadOnly = Status
        SDIGITAL_13.Properties.ReadOnly = Status
        SDIGITAL_14.Properties.ReadOnly = Status
        SDIGITAL_15.Properties.ReadOnly = Status
        SDIGITAL_16.Properties.ReadOnly = Status
        SDIGITAL_17.Properties.ReadOnly = Status
        SDIGITAL_18.Properties.ReadOnly = Status
        SDIGITAL_19.Properties.ReadOnly = Status
        SDIGITAL_20.Properties.ReadOnly = Status
        SDIGITAL_21.Properties.ReadOnly = Status
        SDIGITAL_22.Properties.ReadOnly = Status
        SDIGITAL_23.Properties.ReadOnly = Status
        SDIGITAL_24.Properties.ReadOnly = Status
        SDIGITAL_25.Properties.ReadOnly = Status
        SDIGITAL_26.Properties.ReadOnly = Status
        SDIGITAL_27.Properties.ReadOnly = Status
        SDIGITAL_28.Properties.ReadOnly = Status
        SDIGITAL_29.Properties.ReadOnly = Status
        SDIGITAL_30.Properties.ReadOnly = Status
        SDIGITAL_31.Properties.ReadOnly = Status
        SDIGITAL_32.Properties.ReadOnly = Status
        SDIGITAL_33.Properties.ReadOnly = Status
        SDIGITAL_34.Properties.ReadOnly = Status
        SDIGITAL_35.Properties.ReadOnly = Status
        SDIGITAL_36.Properties.ReadOnly = Status
        SDIGITAL_37.Properties.ReadOnly = Status
        SDIGITAL_38.Properties.ReadOnly = Status
        SDIGITAL_39.Properties.ReadOnly = Status
        SDIGITAL_40.Properties.ReadOnly = Status
        SDIGITAL_41.Properties.ReadOnly = Status
        SDIGITAL_42.Properties.ReadOnly = Status
        SDIGITAL_43.Properties.ReadOnly = Status
        SDIGITAL_44.Properties.ReadOnly = Status
        SDIGITAL_45.Properties.ReadOnly = Status
        SDIGITAL_46.Properties.ReadOnly = Status
        SDIGITAL_47.Properties.ReadOnly = Status
        SDIGITAL_48.Properties.ReadOnly = Status
        SDIGITAL_49.Properties.ReadOnly = Status
        SDIGITAL_50.Properties.ReadOnly = Status
        SDIGITAL_51.Properties.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                sIsOtority = True
            Else
                lTanggal.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        SDIGITAL_1.Checked = False
        SDIGITAL_2.Checked = False
        SDIGITAL_3.Checked = False
        SDIGITAL_4.Checked = False
        SDIGITAL_5.Checked = False
        SDIGITAL_6.Checked = False
        SDIGITAL_7.Checked = False
        SDIGITAL_8.Checked = False
        SDIGITAL_9.Checked = False
        SDIGITAL_10.Checked = False
        SDIGITAL_11.Checked = False
        SDIGITAL_12.Checked = False
        SDIGITAL_13.Checked = False
        SDIGITAL_14.Checked = False
        SDIGITAL_15.Checked = False
        SDIGITAL_16.Checked = False
        SDIGITAL_17.Checked = False
        SDIGITAL_18.Checked = False
        SDIGITAL_19.Checked = False
        SDIGITAL_20.ResetText()
        SDIGITAL_21.Checked = False
        SDIGITAL_22.ResetText()
        SDIGITAL_23.Checked = False
        SDIGITAL_24.Checked = False
        SDIGITAL_25.Checked = False
        SDIGITAL_26.Checked = False
        SDIGITAL_27.Checked = False
        SDIGITAL_28.ResetText()
        SDIGITAL_29.Checked = False
        SDIGITAL_30.Checked = False
        SDIGITAL_31.Checked = False
        SDIGITAL_32.Checked = False
        SDIGITAL_33.Checked = False
        SDIGITAL_34.Checked = False
        SDIGITAL_35.Checked = False
        SDIGITAL_36.Checked = False
        SDIGITAL_37.Checked = False
        SDIGITAL_38.Checked = False
        SDIGITAL_39.Checked = False
        SDIGITAL_40.Checked = False
        SDIGITAL_41.Checked = False
        SDIGITAL_42.Checked = False
        SDIGITAL_43.Checked = False
        SDIGITAL_44.Checked = False
        SDIGITAL_45.Checked = False
        SDIGITAL_46.Checked = False
        SDIGITAL_47.ResetText()
        SDIGITAL_48.ResetText()
        SDIGITAL_49.ResetText()
        SDIGITAL_50.Checked = False
        SDIGITAL_51.Checked = False

        deDATE.DateTime = Now
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RI_36.GetData(txtNoRegister.Text)

            With ds

                SDIGITAL_1.Checked = .SDIGITALRI36_1
                SDIGITAL_2.Checked = .SDIGITALRI36_2
                SDIGITAL_3.Checked = .SDIGITALRI36_3
                SDIGITAL_4.Checked = .SDIGITALRI36_4
                SDIGITAL_5.Checked = .SDIGITALRI36_5
                SDIGITAL_6.Checked = .SDIGITALRI36_6
                SDIGITAL_7.Checked = .SDIGITALRI36_7
                SDIGITAL_8.Checked = .SDIGITALRI36_8
                SDIGITAL_9.Checked = .SDIGITALRI36_9
                SDIGITAL_10.Checked = .SDIGITALRI36_10
                SDIGITAL_11.Checked = .SDIGITALRI36_11
                SDIGITAL_12.Checked = .SDIGITALRI36_12
                SDIGITAL_13.Checked = .SDIGITALRI36_13
                SDIGITAL_14.Checked = .SDIGITALRI36_14
                SDIGITAL_15.Checked = .SDIGITALRI36_15
                SDIGITAL_16.Checked = .SDIGITALRI36_16
                SDIGITAL_17.Checked = .SDIGITALRI36_17
                SDIGITAL_18.Checked = .SDIGITALRI36_18
                SDIGITAL_19.Checked = .SDIGITALRI36_19
                SDIGITAL_20.Text = .SDIGITALRI36_20
                SDIGITAL_21.Checked = .SDIGITALRI36_21
                SDIGITAL_22.Text = .SDIGITALRI36_22
                SDIGITAL_23.Checked = .SDIGITALRI36_23
                SDIGITAL_24.Checked = .SDIGITALRI36_24
                SDIGITAL_25.Checked = .SDIGITALRI36_25
                SDIGITAL_26.Checked = .SDIGITALRI36_26
                SDIGITAL_27.Checked = .SDIGITALRI36_27
                SDIGITAL_28.Text = .SDIGITALRI36_28
                SDIGITAL_29.Checked = .SDIGITALRI36_29
                SDIGITAL_30.Checked = .SDIGITALRI36_30
                SDIGITAL_31.Checked = .SDIGITALRI36_31
                SDIGITAL_32.Checked = .SDIGITALRI36_32
                SDIGITAL_33.Checked = .SDIGITALRI36_33
                SDIGITAL_34.Checked = .SDIGITALRI36_34
                SDIGITAL_35.Checked = .SDIGITALRI36_35
                SDIGITAL_36.Checked = .SDIGITALRI36_36
                SDIGITAL_37.Checked = .SDIGITALRI36_37
                SDIGITAL_38.Checked = .SDIGITALRI36_38
                SDIGITAL_39.Checked = .SDIGITALRI36_39
                SDIGITAL_40.Checked = .SDIGITALRI36_40
                SDIGITAL_41.Checked = .SDIGITALRI36_41
                SDIGITAL_42.Checked = .SDIGITALRI36_42
                SDIGITAL_43.Checked = .SDIGITALRI36_43
                SDIGITAL_44.Checked = .SDIGITALRI36_44
                SDIGITAL_45.Checked = .SDIGITALRI36_45
                SDIGITAL_46.Checked = .SDIGITALRI36_46
                SDIGITAL_47.Text = .SDIGITALRI36_47
                SDIGITAL_48.Text = .SDIGITALRI36_48
                SDIGITAL_49.Text = .SDIGITALRI36_49
                SDIGITAL_50.Checked = .SDIGITALRI36_50
                SDIGITAL_51.Checked = .SDIGITALRI36_51

                deDATE.DateTime = .DATE

                BindingSource1.DataSource = oS_DIGITAL_RI_36.GetDataDetail(.KDKUNJUNGAN)
                GridControl1.DataSource = BindingSource1

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
            Dim ds = oS_DIGITAL_RI_36.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                .KDPENDAFTARAN = sKdpendaftaran
                .KDCUSTOMER = sKdcustomer
                Try
                    .DATECREATED = oS_DIGITAL_RI_36.GetData(txtNoRegister.Text).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .DATE = deDATE.DateTime

                .DOKTER_KODE = 0
                .DOKTER_NAMEDISPLAY = 0
                .SDIGITALRI36_1 = SDIGITAL_1.Checked
                .SDIGITALRI36_2 = SDIGITAL_2.Checked
                .SDIGITALRI36_3 = SDIGITAL_3.Checked
                .SDIGITALRI36_4 = SDIGITAL_4.Checked
                .SDIGITALRI36_5 = SDIGITAL_5.Checked
                .SDIGITALRI36_6 = SDIGITAL_6.Checked
                .SDIGITALRI36_7 = SDIGITAL_7.Checked
                .SDIGITALRI36_8 = SDIGITAL_8.Checked
                .SDIGITALRI36_9 = SDIGITAL_9.Checked
                .SDIGITALRI36_10 = SDIGITAL_10.Checked
                .SDIGITALRI36_11 = SDIGITAL_11.Checked
                .SDIGITALRI36_12 = SDIGITAL_12.Checked
                .SDIGITALRI36_13 = SDIGITAL_13.Checked
                .SDIGITALRI36_14 = SDIGITAL_14.Checked
                .SDIGITALRI36_15 = SDIGITAL_15.Checked
                .SDIGITALRI36_16 = SDIGITAL_16.Checked
                .SDIGITALRI36_17 = SDIGITAL_17.Checked
                .SDIGITALRI36_18 = SDIGITAL_18.Checked
                .SDIGITALRI36_19 = SDIGITAL_19.Checked
                .SDIGITALRI36_20 = SDIGITAL_20.Text
                .SDIGITALRI36_21 = SDIGITAL_21.Checked
                .SDIGITALRI36_22 = SDIGITAL_22.Text
                .SDIGITALRI36_23 = SDIGITAL_23.Checked
                .SDIGITALRI36_24 = SDIGITAL_24.Checked
                .SDIGITALRI36_25 = SDIGITAL_25.Checked
                .SDIGITALRI36_26 = SDIGITAL_26.Checked
                .SDIGITALRI36_27 = SDIGITAL_27.Checked
                .SDIGITALRI36_28 = SDIGITAL_28.Text
                .SDIGITALRI36_29 = SDIGITAL_29.Checked
                .SDIGITALRI36_30 = SDIGITAL_30.Checked
                .SDIGITALRI36_31 = SDIGITAL_31.Checked
                .SDIGITALRI36_32 = SDIGITAL_32.Checked
                .SDIGITALRI36_33 = SDIGITAL_33.Checked
                .SDIGITALRI36_34 = SDIGITAL_34.Checked
                .SDIGITALRI36_35 = SDIGITAL_35.Checked
                .SDIGITALRI36_36 = SDIGITAL_36.Checked
                .SDIGITALRI36_37 = SDIGITAL_37.Checked
                .SDIGITALRI36_38 = SDIGITAL_38.Checked
                .SDIGITALRI36_39 = SDIGITAL_39.Checked
                .SDIGITALRI36_40 = SDIGITAL_40.Checked
                .SDIGITALRI36_41 = SDIGITAL_41.Checked
                .SDIGITALRI36_42 = SDIGITAL_42.Checked
                .SDIGITALRI36_43 = SDIGITAL_43.Checked
                .SDIGITALRI36_44 = SDIGITAL_44.Checked
                .SDIGITALRI36_45 = SDIGITAL_45.Checked
                .SDIGITALRI36_46 = SDIGITAL_46.Checked
                .SDIGITALRI36_47 = SDIGITAL_47.Text
                .SDIGITALRI36_48 = SDIGITAL_48.Text
                .SDIGITALRI36_49 = SDIGITAL_49.Text
                .SDIGITALRI36_50 = SDIGITAL_50.Checked
                .SDIGITALRI36_51 = SDIGITAL_51.Checked

                Try
                    .CETAK = oS_DIGITAL_RI_36.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try


                Try
                    If sIsOtority = True Then
                        .KDUSER = oS_DIGITAL_RI_36.GetData(txtNoRegister.Text).KDUSER
                        .KDUSER_SIGNATURE = oS_DIGITAL_RI_36.GetData(txtNoRegister.Text).KDUSER_SIGNATURE
                    Else
                        .KDUSER = sUserID
                        .KDUSER_SIGNATURE = sUserSIGNATURE
                    End If
                Catch ex As Exception
                    .KDUSER = sUserID
                    .KDUSER_SIGNATURE = sUserSIGNATURE
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oS_DIGITAL_RI_36.GetStructureDetailList
            For i As Integer = 0 To grvDetil.RowCount - 2
                Dim dsDetail = oS_DIGITAL_RI_36.GetStructureDetail
                With dsDetail
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    Try
                        .DATECREATED = oS_DIGITAL_RI_36.GetData(ds.KDKUNJUNGAN).DATECREATED
                        .DATE = oS_DIGITAL_RI_36.GetData(ds.KDKUNJUNGAN).DATE
                    Catch ex As Exception
                        .DATECREATED = Now
                        .DATE = Now
                    End Try
                    .DATEUPDATED = Now
                    .SDIGITALRI36D_1 = gridVal(grvDetil, i, No)
                    .SDIGITALRI36D_2 = gridVal(grvDetil, i, PenjelasanMateri)
                    .SDIGITALRI36D_3 = gridVal(grvDetil, i, TglWaktuRencana)
                    .SDIGITALRI36D_4 = gridVal(grvDetil, i, DurasiMetode)
                    .SDIGITALRI36D_5 = gridVal(grvDetil, i, TglWaktuPelaksanaan)
                    .SDIGITALRI36D_6 = gridVal(grvDetil, i, DurasiMetodePelaksanaan)
                    .SDIGITALRI36D_7 = gridVal(grvDetil, i, ParafNamaYgDiEdukasiPelaksana)
                    .SDIGITALRI36D_8 = gridVal(grvDetil, i, ParafEdukatorPelaksana)
                    .SDIGITALRI36D_9 = gridVal(grvDetil, i, TglWaktuRedukasi)
                    .SDIGITALRI36D_10 = gridVal(grvDetil, i, PenjelasanMateriRedukasi)
                    .SDIGITALRI36D_11 = gridVal(grvDetil, i, ParafNamaYgDiEdukasiRedukasi)
                    .SDIGITALRI36D_12 = gridVal(grvDetil, i, ParafNamaEdukatorRedukasi)
                    .SEQ = i
                    .NAMA_1 = fn_NOIDUSERX(gridVal(grvDetil, i, ParafEdukatorPelaksana))
                    .NAMA_2 = fn_NOIDUSERX(gridVal(grvDetil, i, ParafNamaEdukatorRedukasi))
                    .DESCRIPTION = sKdkunjungan
                    .KODE_LEAFLET = gridVal(grvDetil, i, KodeLeaflet)
                    .METODE = gridVal(grvDetil, i, Metode)
                    .HASIL_VERIFIKASI = gridVal(grvDetil, i, HasilVerifikasi)
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RI_36.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RI_36.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub

        Dim result As DialogResult = MessageBox.Show("Apakah data edukasi ini akan dihapus?", "Alert", MessageBoxButtons.OKCancel)
        If result = DialogResult.OK Then
            grvDetil.DeleteSelectedRows()
        End If

    End Sub
    Private Function gridVal(ByVal grid As DevExpress.XtraGrid.Views.Grid.GridView, ByVal i As Integer, ByVal col As Columns.GridColumn) As String
        Dim str As String
        If grid.GetRowCellValue(i, col) Is Nothing Then
            str = ""
        Else
            str = grid.GetRowCellValue(i, col)
        End If

        Return str
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
            SQL &= "AND KELOMPOKIPK = 'NAKES' "
            SQL &= "ORDER BY NAME_DISPLAY ASC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "STAFF")

            grdUSER.DataSource = ds.Tables("STAFF")
            grdUSER.ValueMember = "KDSTAFF"
            grdUSER.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_NOIDUSERX(ByVal KDSATFF As String) As String
        Try
            fn_NOIDUSERX = ""

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
            SQL &= "A.KDSTAFF = '" & KDSATFF & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "NAMESTAFF")

            For iLoop As Integer = 0 To ds.Tables("NAMESTAFF").Rows.Count - 1
                With ds.Tables("NAMESTAFF")
                    fn_NOIDUSERX = .Rows(iLoop)("NAME_DISPLAY")
                End With
            Next

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            fn_NOIDUSERX = ""
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetil.CellValueChanged
        If e.Column.Name = No.Name Then
            Try
                If grvDetil.GetFocusedRowCellValue(No) IsNot Nothing Then
                    grvDetil.SetFocusedRowCellValue(TglWaktuRencana, Now)
                    grvDetil.SetFocusedRowCellValue(TglWaktuPelaksanaan, Now)
                    grvDetil.SetFocusedRowCellValue(TglWaktuRedukasi, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_loadHistory(ByVal sKDCUSTOMER As String)
        Try
            If sKDCUSTOMER = String.Empty Then Exit Sub

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

            'SQL &= "PenjelasanMateri = B.SDIGITALRI36D_2 "
            'SQL &= ",TglWaktuRencana = B.SDIGITALRI36D_3 "
            'SQL &= ",DurasiMetode = B.SDIGITALRI36D_4 "
            'SQL &= ",TglWaktuPelaksanaan = B.SDIGITALRI36D_5 "
            'SQL &= ",DurasiMetodePelaksanaan = B.SDIGITALRI36D_6 "
            'SQL &= ",ParafNamaYgDiEdukasiPelaksana = B.SDIGITALRI36D_7 "
            'SQL &= ",ParafEdukatorPelaksana = B.NAMA_1 "
            'SQL &= ",TglWaktuRedukasi = B.SDIGITALRI36D_9 "
            'SQL &= ",PenjelasanMateriRedukasi = B.SDIGITALRI36D_10 "
            'SQL &= ",ParafNamaYgDiEdukasiRedukasi = B.SDIGITALRI36D_11 "
            'SQL &= ",ParafNamaEdukatorRedukasi = B.NAMA_2 "

            'REVISI
            SQL &= "TglDanJamEdukasi = B.SDIGITALRI36D_5 "
            SQL &= ",MateriEdukasiBerdasarkanKebutuhan = B.SDIGITALRI36D_2 "
            SQL &= ",KodeLeaflet = B.KODE_LEAFLET "
            SQL &= ",LamaEdukasi = B.SDIGITALRI36D_6 "
            SQL &= ",B.METODE "
            SQL &= ",HasilVerifikasi = B.HASIL_VERIFIKASI "
            SQL &= ",TglReEdukasiReDemonstrasi = B.SDIGITALRI36D_9 "
            SQL &= ",PemberiEdukasi = B.NAMA_1 "
            SQL &= ",ParafNamaYgDiEdukasiPelaksana = B.SDIGITALRI36D_7 "
            SQL &= ",ParafNamaYgDiEdukasiRedukasi = B.SDIGITALRI36D_11 "


            SQL &= "FROM "
            SQL &= "S_DIGITAL_RI_36 A "
            SQL &= "INNER JOIN S_DIGITAL_RI_36_DETIL B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN R_IDENTITAS_PASIEN C "
            SQL &= "ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
            SQL &= "WHERE C.KDCUSTOMER = '" & sKDCUSTOMER & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_DIGITAL_RI_36")

            grdHistory.MainView = grvHistory
            grdHistory.DataSource = ds.Tables("S_DIGITAL_RI_36")
            grdHistory.ForceInitialize()

            fn_LoadFormatDataFormulirEdukasi()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadFormatDataFormulirEdukasi()
        For iLoop As Integer = 0 To grvHistory.Columns.Count - 1
            If grvHistory.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grvHistory.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grvHistory.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grvHistory.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grvHistory.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grvHistory.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grvHistory.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next
        grvHistory.BestFitColumns()
    End Sub
    Private Sub btnTAMBAH_Click(sender As Object, e As EventArgs) Handles btnTAMBAH.Click
        Dim frmPopUpEMedrekRI_36 As New frmPopUpEMedrekRI_36
        frmPopUpEMedrekRI_36.fn_LoadMe(FORM_MODE.FORM_MODE_ADD, "", "", "", Now, "", Now, "", "", "", Now, "", "", "", False, "", "", "", "")
        frmPopUpEMedrekRI_36.ShowDialog()

        If sFind1 <> String.Empty Then
            grvDetil.Focus()
            grvDetil.AddNewRow()
            grvDetil.SetFocusedRowCellValue(PenjelasanMateri, sFind1)
            grvDetil.SetFocusedRowCellValue(TglWaktuRencana, sFind2)
            grvDetil.SetFocusedRowCellValue(DurasiMetode, sFind3)
            grvDetil.SetFocusedRowCellValue(TglWaktuPelaksanaan, sFind4)
            grvDetil.SetFocusedRowCellValue(DurasiMetodePelaksanaan, sFind5)
            grvDetil.SetFocusedRowCellValue(ParafNamaYgDiEdukasiPelaksana, sFind6)
            grvDetil.SetFocusedRowCellValue(ParafEdukatorPelaksana, sFind7)
            grvDetil.SetFocusedRowCellValue(TglWaktuRedukasi, sFind8)
            grvDetil.SetFocusedRowCellValue(PenjelasanMateriRedukasi, sFind9)
            grvDetil.SetFocusedRowCellValue(ParafNamaYgDiEdukasiRedukasi, sFind10)
            grvDetil.SetFocusedRowCellValue(ParafNamaEdukatorRedukasi, sFind11)
            grvDetil.SetFocusedRowCellValue(KodeLeaflet, sFind12)
            grvDetil.SetFocusedRowCellValue(Metode, sFind13)
            grvDetil.SetFocusedRowCellValue(HasilVerifikasi, sFind14)
            grvDetil.UpdateCurrentRow()

        End If

        sFind1 = String.Empty
        sFind2 = String.Empty
        sFind3 = String.Empty
        sFind4 = String.Empty
        sFind5 = String.Empty
        sFind6 = String.Empty
        sFind7 = String.Empty
        sFind8 = String.Empty
        sFind9 = String.Empty
        sFind10 = String.Empty
        sFind11 = String.Empty
        sFind12 = String.Empty
        sFind13 = String.Empty
        sFind14 = String.Empty

        grvDetil.Focus()
    End Sub


    Private Sub frmEMedrekRI_36_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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
            myView.Y = -scrollchange - myView.Y
        Else
            'down
            myView.X = -myView.X
            myView.Y = scrollchange - myView.Y
        End If

        Me.Panel3.AutoScrollPosition = myView
    End Sub

    Private Sub LihatLeafletToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LihatLeafletToolStripMenuItem.Click
        Try
            If String.IsNullOrEmpty(grvDetil.GetFocusedRowCellValue("KODE_LEAFLET").ToString()) Then
                Exit Sub
            Else
                Dim AlamatLeaflet As String = "\\172.165.115.200\δleafletδ\"
                Dim filename As String = FileMatches(AlamatLeaflet,"*.pdf",grvDetil.GetFocusedRowCellValue("KODE_LEAFLET").ToString())
                
                Dim FormPopUpPdf As New FormPopUpPdf
                Try
                    FormPopUpPdf.LoadMe(filename)
                    FormPopUpPdf.ShowDialog(Me)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not FormPopUpPdf Is Nothing Then FormPopUpPdf.Dispose()
                    FormPopUpPdf = Nothing
                End Try

            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Public Function FileMatches(folderPath As String, filePattern As String, phrase As String) As String
        For Each fileName As String In Directory.GetFiles(folderPath, filePattern)
            If fileName.Contains(phrase) Then
                Return fileName
            End If
        Next

        Return ""
    End Function


#End Region
End Class