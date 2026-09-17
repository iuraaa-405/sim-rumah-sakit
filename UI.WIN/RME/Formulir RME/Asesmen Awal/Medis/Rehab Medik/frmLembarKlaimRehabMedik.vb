Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports System.Xml
Public Class frmLembarKlaimRehabMedik
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oS_DIGITAL_RJ_CLAIMREHABMEDIK As New Digital.clsDigital_RJ_CLAIMREHABMEDIK
    Private down As Boolean = False
    Private sKODEDOKTER As String
    Private sNAMADOKTER  As String

#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String)
        oFormMode = FormMode

        Dim oPendaftaran As New Identitas.clsIdentitasPasien
        Dim dsPendaftaran = oPendaftaran.GetData(KDREG)

        txtNoRegister.Text = KDREG

        If dsPendaftaran IsNot Nothing Then
            txtNamaPasien.Text = dsPendaftaran.NAMAPASIEN.Trim
            txtNIK.Text = dsPendaftaran.NIK.Trim
            txtTmpTglLahir.Text = dsPendaftaran.TEMPATLAHIR.Trim & ", " & dsPendaftaran.TANGGALLAHIR
            txtPangkatGol.Text = dsPendaftaran.PANGKAT.Trim
            txtNRPNIP.Text = dsPendaftaran.NRP.Trim
            txtKesatuan.Text = dsPendaftaran.KESATUAN.Trim
            txtJK.Text = dsPendaftaran.JENISKELAMIN.Trim
            txtUmur.Text = dsPendaftaran.USIA.Trim

            txtNoRegister.Text = dsPendaftaran.KDKUNJUNGAN
            txtNoRM.Text = dsPendaftaran.KDCUSTOMER
            txtNoTelepon.Text = dsPendaftaran.NOMORTELEPON
            txtTanggalMasuk.Text = dsPendaftaran.DATE.ToString("dd-MM-yyyy")
            txtJam.Text = dsPendaftaran.DATE.ToString("HH:mm:ss")
            txtAlamat.Text = dsPendaftaran.ALAMAT
            sKODEDOKTER = dsPendaftaran.KDDOKTER
            sNAMADOKTER = dsPendaftaran.DOKTER

        Else
            txtNamaPasien.ResetText()
            txtNIK.ResetText()
            txtTmpTglLahir.ResetText()
            txtPangkatGol.ResetText()
            txtNRPNIP.ResetText()
            txtKesatuan.ResetText()
            txtJK.ResetText()
            txtUmur.ResetText()
            txtNoRegister.ResetText()
            txtNoRM.ResetText()
            txtNoTelepon.ResetText()
            txtTanggalMasuk.ResetText()
            txtJam.ResetText()
            txtAlamat.ResetText()
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = EMedrekRJ_02.TITLE
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

        chkSuami.Properties.ReadOnly = Status
        chkIstri.Properties.ReadOnly = Status
        chkAnak.Properties.ReadOnly = Status
        deTANGGALPELAYANAN.Properties.ReadOnly = Status
        txtAnamnesis.Properties.ReadOnly = Status
        txtPemeriksaanDanUjiFungsi.Properties.ReadOnly = Status
        txtDiagnosisMedisICD10.Properties.ReadOnly = Status
        txtDiagnosisFungsiICD10.Properties.ReadOnly = Status
        txtPemeriksaanPenunjang.Properties.ReadOnly = Status
        txtTataLaksanaKFR.Properties.ReadOnly = Status
        txtAnjuran.Properties.ReadOnly = Status
        txtEvaluasi.Properties.ReadOnly = Status


    End Sub
    Private Sub fn_EmptyMe()
        'txtCODE.Text = "<--- AUTO --->"

        chkSuami.Checked = False
        chkIstri.Checked = False
        chkAnak.Checked = False
        deTANGGALPELAYANAN.ResetText()
        txtAnamnesis.ResetText()
        txtPemeriksaanDanUjiFungsi.ResetText()
        txtDiagnosisMedisICD10.ResetText()
        txtDiagnosisFungsiICD10.ResetText()
        txtPemeriksaanPenunjang.ResetText()
        txtTataLaksanaKFR.ResetText()
        txtAnjuran.ResetText()
        txtEvaluasi.ResetText()

        fn_LoadAsessmenAwalIRM(txtNoRegister.Text)
        btnReload_Click()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetData(txtNoRegister.Text)

            With ds
                chkSuami.Checked = .ISSUAMI
                chkIstri.Checked = .ISISTRI
                chkAnak.Checked = .ISANAK
                deTANGGALPELAYANAN.DateTime = .TANGGALPELAYANAN
                txtAnamnesis.Text = .ANAMNESIS
                txtPemeriksaanDanUjiFungsi.Text = .PEMERIKSAANDANUJIFUNGSI
                txtDiagnosisMedisICD10.Text = .DIAGNOSISMEDIS
                txtDiagnosisFungsiICD10.Text = .DIAGNOSISFUNGSI
                txtPemeriksaanPenunjang.Text = .PEMERIKSAANPENUNJANG
                txtTataLaksanaKFR.Text = .TATALAKSANAKFR
                txtAnjuran.Text = .ANJURAN
                txtEvaluasi.Text = .EVALUASI
                grdDOKTER.EditValue = .KDDOKTER
                
            End With
        Catch oErr As Exception
            MsgBox("Load List Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadAsessmenAwalIRM(ByVal Parameter As String)
        Dim oAsessmenAwal As New Digital.clsDigital_RJ_ASSREHABMEDIK
        Dim dsAsessmenAwal = oAsessmenAwal.GetData(Parameter)

        If dsAsessmenAwal IsNot Nothing Then
            txtAnamnesis.Text = dsAsessmenAwal.SDIGITAL3
            txtPemeriksaanDanUjiFungsi.Text = dsAsessmenAwal.SDIGITAL54
            txtDiagnosisMedisICD10.Text = dsAsessmenAwal.SDIGITAL70
            txtDiagnosisFungsiICD10.Text = dsAsessmenAwal.SDIGITAL71
            txtTataLaksanaKFR.Text = dsAsessmenAwal.SDIGITAL73
            txtEvaluasi.Text = dsAsessmenAwal.SDIGITAL74
        Else
            MsgBox("Assemen Awal Pasien Rehabilitasi Medik belum di input", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub

    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtNoRegister.Text = String.Empty Then
                MsgBox("Dibutuhkan Register", MsgBoxStyle.Exclamation, Me.Text)
                txtNoRegister.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdDOKTER.Text = String.Empty Then
                MsgBox("Dibutuhkan Dokter", MsgBoxStyle.Exclamation, Me.Text)
                grdDOKTER.Focus()
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
            Dim ds = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetStructureHeader
            With ds
                .KDKUNJUNGAN = txtNoRegister.Text
                Try
                    .DATECREATED = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetData(txtNoRegister.Text).DATECREATED
                    .DATE = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetData(txtNoRegister.Text).DATE
                Catch ex As Exception
                    .DATECREATED = Now
                    .DATE = now
                End Try
                .DATEUPDATED = Now

                .ISSUAMI = chkSuami.Checked
                .ISISTRI = chkIstri.Checked
                .ISANAK = chkAnak.Checked
                .TANGGALPELAYANAN = deTANGGALPELAYANAN.DateTime
                .ANAMNESIS = txtAnamnesis.Text
                .PEMERIKSAANDANUJIFUNGSI = txtPemeriksaanDanUjiFungsi.Text
                .DIAGNOSISMEDIS = txtDiagnosisMedisICD10.Text
                .DIAGNOSISFUNGSI = txtDiagnosisFungsiICD10.Text
                .PEMERIKSAANPENUNJANG = txtPemeriksaanPenunjang.Text
                .TATALAKSANAKFR = txtTataLaksanaKFR.Text
                .ANJURAN = txtAnjuran.Text
                .EVALUASI = txtEvaluasi.Text
                .KDDOKTER = grdDOKTER.EditValue
                Try
                    .CETAK = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetData(txtNoRegister.Text).CETAK
                Catch ex As Exception
                    .CETAK = 0
                End Try

                .KDUSER = sUserID
                .KDUSER_SIGNATURE = sUserSIGNATURE
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oS_DIGITAL_RJ_CLAIMREHABMEDIK.InsertData(ds)
                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oS_DIGITAL_RJ_CLAIMREHABMEDIK.UpdateData(ds)
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
            Case Keys.F5
                If btnReload.Enabled = True Then
                   btnReload_Click()
                End If
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
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Dim dsKunjungan = oS_DIGITAL_RJ_CLAIMREHABMEDIK.GetDataByKunjungan(txtNoRegister.Text)

        Dim listPenunjang As New List(Of String)
        Dim listTindakanPengobatan As New List(Of String)

        If dsKunjungan IsNot Nothing Then
            Dim oOrderTindakan As New Inventory.clsOrderTindakan
            Dim oOrderLab As New Inventory.clsOrderLab
            Dim oOrderRad As New Inventory.clsOrderRad
            Dim oKonsul As New Digital.clsKonsul
            Dim oKonsulJawab As New Digital.clsJawabKonsul

            Dim dsTindakan = From x In oOrderTindakan.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                             Join y In oOrderTindakan.GetDataDetail()
                             On x.KDORDERTINDAKAN Equals y.KDORDERTINDAKAN
                             Select TANGGAL = x.DATE, ITEM = "Tindakan : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsLab = From x In oOrderLab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderLab.GetDataDetail()
                        On x.KDORDERLAB Equals y.KDORDERLAB
                        Select TANGGAL = x.DATE, ITEM = "Lab : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsRad = From x In oOrderRad.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                        Join y In oOrderRad.GetDataDetail()
                        On x.KDORDERRAD Equals y.KDORDERRAD
                        Select TANGGAL = x.DATE, ITEM = "Rad : " & y.TARIFKT & " " & y.REMARKS & ", "

            Dim dsKonsul = From x In oKonsul.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                           Select TANGGAL = x.DATE, ITEM = "Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsJawabKonsul = From x In oKonsulJawab.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)
                                Select TANGGAL = x.DATE, ITEM = "Jawab Konsul : " & "Dari " & x.DOKTER_DARI & " Kepada " & x.DOKTER_KEPADA & " Isi " & x.MEMO & ", "

            Dim dsUnionPenunjang = dsLab.Union(dsRad)

            For Each xloop In dsUnionPenunjang.OrderBy(Function(x) x.TANGGAL)
                listPenunjang.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim dsUnionTindakanKonsul = dsTindakan.Union(dsKonsul).Union(dsJawabKonsul)

            For Each xloop In dsUnionTindakanKonsul.OrderBy(Function(x) x.TANGGAL)
                listTindakanPengobatan.Add("*) " & xloop.TANGGAL.ToString("dd-MM-yyy HH:mm:ss") & " " & xloop.ITEM)
            Next

            Dim oResep As New Inventory.clsOrderResep

            Dim dsResep = oResep.GetDataPendaftaranList(dsKunjungan.KDPENDAFTARAN)

            For Each xloop In dsResep
                For Each yloop In oResep.GetDataDetail(xloop.KDORDERRESEP)
                    listTindakanPengobatan.Add(yloop.TARIFKT & " " & yloop.SIGNA)
                Next
            Next

            txtPemeriksaanPenunjang.Text = String.Join(", ", listPenunjang.ToArray)
            'MemoEdit4.Text = String.Join(", ", listTindakanPengobatan.ToArray)
        End If
    End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox("Save Lembar Klaim Rajal Rehab Medik " & txtNamaPasien.Text.Trim.ToUpper & vbCrLf & "Dengan User : " & sUserID & " ?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
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
    Private Sub fn_Doctor()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = "Data Source=172.165.115.150;Initial Catalog=DUSTIRA_FARMASI;Persist Security Info=True;User ID=sa;Password=dust1r@@"
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
            da.Fill(ds, "DOKTER")

            grdDOKTER.Properties.DataSource = ds.Tables("DOKTER")
            grdDOKTER.Properties.ValueMember = "KDSTAFF"
            grdDOKTER.Properties.DisplayMember = "NAME_DISPLAY"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub frmLembarKlaimRehabMedik_MouseWheel(sender As Object, e As MouseEventArgs) Handles MyBase.MouseWheel
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