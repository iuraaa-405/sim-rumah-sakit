Imports System.Linq
Imports System.Data
Imports System.Data.SqlClient
Imports DataAccess
Imports DataAccess.My.Resources
Imports DevExpress.XtraGrid
Imports System.IO

Public Class frmPopUpEMedrekRI_36
    Private oPenjelasanMateri As String = ""
    Private oWaktuRencana As DateTime = Now
    Private oDurasiMetode As String = ""
    Private oWaktuPelaksanaan As DateTime = Now
    Private oMetodePelaksanaan As String = ""
    Private oEdukasiPelaksana As String = ""
    Private oEdukatorPelaksana As String = ""
    Private oWaktuRedukasi As DateTime = Now
    Private oMateriRedukasi As String = ""
    Private oEdukasiRedukasi As String = ""
    Private oEdukatorRedukasi As String = ""
    Private oKodeLeaflet As String = ""
    Private oMetode As String = ""
    Private oHasilVerifikasi As String = ""
    Private sKDPENDAFTARANS As String = ""
    Private sKDKUNJUNGANS As String = ""
    Private sDisplays As Boolean = True
    Private sSeqs As String = ""
    Private oEdukasi_Template As New Digital.clsEdukasi_Template
    Private oLeaflet As New Master.clsKodeLeaflet
    Private oS_DIGITAL_RI_36 As New Digital.clsS_DIGITAL_RI_36
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW


    Public Sub fn_LoadMe(ByVal FormMode As Integer, ByVal sKDPENDAFTARAN As String, ByVal sKDKUNJUNGAN As String, ByVal sPenjelasanMateri As String, ByVal sWaktuRencana As DateTime, ByVal sDurasiMetode As String, ByVal sWaktuPelaksanaan As DateTime, ByVal sMetodePelaksanaan As String, ByVal sEdukasiPelaksana As String, ByVal sEdukatorPelaksana As String, ByVal sWaktuRedukasi As DateTime, ByVal sMateriRedukasi As String, ByVal sEdukasiRedukasi As String, ByVal sEdukatorRedukasi As String, ByVal sDisplay As Boolean, ByVal sSeq As String, ByVal sKodeLeaflet As String, ByVal sMetode As String, ByVal sHasilVerifikasi As String)
        oFormMode = FormMode
        sDisplays = sDisplay
        oPenjelasanMateri = sPenjelasanMateri
        oWaktuRencana = sWaktuRencana
        oDurasiMetode = sDurasiMetode
        oWaktuPelaksanaan = sWaktuPelaksanaan
        oMetodePelaksanaan = sMetodePelaksanaan
        oEdukasiPelaksana = sEdukasiPelaksana
        oEdukatorPelaksana = sEdukatorPelaksana
        oWaktuRedukasi = sWaktuRedukasi
        oMateriRedukasi = sMateriRedukasi
        oEdukasiRedukasi = sEdukasiRedukasi
        oEdukatorRedukasi = sEdukatorRedukasi
        sSeqs = sSeq
        sKDPENDAFTARANS = sKDPENDAFTARAN
        sKDKUNJUNGANS = sKDKUNJUNGAN
        oKodeLeaflet = sKodeLeaflet
        oMetode = sMetode
        oHasilVerifikasi = sHasilVerifikasi
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_Load()
        fn_LoadDataFormulir()
        fn_LoadDataLeaflet()
        fn_NOIDUSER()
    End Sub
    Private Sub cmdSelect_Click()
        If txtPenjelasanMateri.Text = String.Empty Then

            MsgBox("Dibutuhkan Penjelasan Materi ", MsgBoxStyle.Exclamation, Me.Text)
            txtPenjelasanMateri.Focus()
            Exit Sub
        End If


        sFind1 = txtPenjelasanMateri.Text
        sFind2 = deTglWaktuRencana.DateTime
        sFind3 = txtDurasiMetode.Text
        sFind4 = deTglWaktuPelaksanaan.DateTime
        sFind5 = txtDurasiMetodePelaksanaan.Text
        sFind6 = txtParafNamaYgDiEdukasiPelaksana.Text
        sFind7 = grdParafEdukatorPelaksana.EditValue
        sFind8 = deTglWaktuRedukasi.DateTime
        sFind9 = txtPenjelasanMateriRedukasi.Text
        sFind10 = txtParafNamaYgDiEdukasiRedukasi.Text
        sFind11 = grdParafNamaEdukatorRedukasi.EditValue
        sFind12 = grdKODELEAFLET.EditValue
        sFind13 = cboMetode.Text
        sFind14 = cboHasilVerifikasi.Text

        Me.Close()
    End Sub

    Private Function fn_Save() As Boolean
        Dim ds = oS_DIGITAL_RI_36.GetDataDetail(sKDKUNJUNGANS).OrderByDescending(Function(x) x.SEQ).FirstOrDefault

        Try
            Dim dsDetail = oS_DIGITAL_RI_36.GetStructureDetail
            With dsDetail
                .KDKUNJUNGAN = sKDKUNJUNGANS
                .DATECREATED = Now
                .DATE = Now
                .DATEUPDATED = Now
                .SDIGITALRI36D_1 = String.Empty
                .SDIGITALRI36D_2 = txtPenjelasanMateri.Text
                .SDIGITALRI36D_3 = deTglWaktuRencana.DateTime
                .SDIGITALRI36D_4 = txtDurasiMetode.Text
                .SDIGITALRI36D_5 = deTglWaktuPelaksanaan.DateTime
                .SDIGITALRI36D_6 = txtDurasiMetodePelaksanaan.Text
                .SDIGITALRI36D_7 = txtParafNamaYgDiEdukasiPelaksana.Text
                .SDIGITALRI36D_8 = grdParafEdukatorPelaksana.EditValue
                .SDIGITALRI36D_9 = deTglWaktuRedukasi.DateTime
                .SDIGITALRI36D_10 = txtPenjelasanMateriRedukasi.Text
                .SDIGITALRI36D_11 = txtParafNamaYgDiEdukasiRedukasi.Text
                .SDIGITALRI36D_12 = grdParafNamaEdukatorRedukasi.EditValue
                .NAMA_1 = fn_NOIDUSERX(grdParafEdukatorPelaksana.EditValue)
                .NAMA_2 = fn_NOIDUSERX(grdParafNamaEdukatorRedukasi.EditValue)
                .DESCRIPTION = sKDKUNJUNGANS
                .KODE_LEAFLET = grdKODELEAFLET.EditValue
                .METODE = cboMetode.Text
                .HASIL_VERIFIKASI = cboHasilVerifikasi.Text
            End With
            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    If ds IsNot Nothing Then
                        dsDetail.SEQ = ds.SEQ + 1
                    Else
                        dsDetail.SEQ = 1
                    End If

                    fn_Save = oS_DIGITAL_RI_36.InsertDataDetail(dsDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    dsDetail.SEQ = sSeqs
                    fn_Save = oS_DIGITAL_RI_36.UpdateDataDetail(dsDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
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

            grdParafEdukatorPelaksana.Properties.DataSource = ds.Tables("STAFF")
            grdParafEdukatorPelaksana.Properties.ValueMember = "KDSTAFF"
            grdParafEdukatorPelaksana.Properties.DisplayMember = "NAME_DISPLAY"

            grdParafNamaEdukatorRedukasi.Properties.DataSource = ds.Tables("STAFF")
            grdParafNamaEdukatorRedukasi.Properties.ValueMember = "KDSTAFF"
            grdParafNamaEdukatorRedukasi.Properties.DisplayMember = "NAME_DISPLAY"

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
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
        'sCode = txtDescription.Text.Trim.ToUpper
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_LoadDataFormulir()
        Try
            Dim ds = oEdukasi_Template.GetData()

            grdTemplate.Properties.DataSource = ds.ToList()
            grdTemplate.Properties.ValueMember = "KDEDUKASI"
            grdTemplate.Properties.DisplayMember = "JUDUL"
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDataLeaflet()
        Try
            Dim ds = oLeaflet.GetData()

            grdKODELEAFLET.Properties.DataSource = ds.ToList()
            grdKODELEAFLET.Properties.ValueMember = "KODE"
            grdKODELEAFLET.Properties.DisplayMember = "JUDUL"
        Catch ex As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub grdTemplate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles grdTemplate.KeyPress
        If Asc(e.KeyChar) = 13 Then
            Dim dsEDUKASI = oEdukasi_Template.GetData(grdTemplate.EditValue)
            If dsEDUKASI IsNot Nothing Then
                txtPenjelasanMateri.Text = dsEDUKASI.ISI
            End If
        End If
    End Sub
    Private Sub fn_Load()
        txtPenjelasanMateri.Text = oPenjelasanMateri
        deTglWaktuRencana.DateTime = oWaktuRencana
        txtDurasiMetode.Text = oDurasiMetode
        deTglWaktuPelaksanaan.DateTime = oWaktuPelaksanaan
        txtDurasiMetodePelaksanaan.Text = oMetodePelaksanaan
        grdParafNamaEdukatorRedukasi.EditValue = oEdukatorRedukasi
        txtParafNamaYgDiEdukasiPelaksana.Text = oEdukasiPelaksana
        deTglWaktuRedukasi.DateTime = oWaktuRedukasi
        txtPenjelasanMateriRedukasi.Text = oMateriRedukasi
        txtParafNamaYgDiEdukasiRedukasi.Text = oEdukasiRedukasi
        grdParafEdukatorPelaksana.EditValue = oEdukatorPelaksana
        grdKODELEAFLET.Text = oKodeLeaflet
        cboMetode.Text = oMetode
        cboHasilVerifikasi.Text = oHasilVerifikasi
    End Sub
#Region "Command Button"
    Private Sub frmItem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.Escape
                btnClose_Click()
        End Select
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick

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

        Me.Close()
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If sDisplays = False Then
            cmdSelect_Click()
        Else
            If MsgBox("Save ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            If fn_Save() = False Then
                MsgBox("Save gagal! Tolong kontak admin anda!", MsgBoxStyle.Exclamation, Me.Text)
                Me.Close()
            Else
                MsgBox("Save success!", MsgBoxStyle.Information, Me.Text)
                Me.Close()
            End If
        End If
    End Sub

    Private Sub btnLihatLeaflet_Click(sender As Object, e As EventArgs) Handles btnLihatLeaflet.Click
        Try
            If String.IsNullOrEmpty(grdKODELEAFLET.EditValue) Then
                Exit Sub
            Else
                Dim AlamatLeaflet As String = "\\172.165.115.200\δleafletδ\"
                Dim filename As String = FileMatches(AlamatLeaflet,"*.pdf",grdKODELEAFLET.EditValue)
                
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