Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmDiet
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oDiet As New Digital.clsDiet
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDateFrom.DateTime = Now
        deDateTo.DateTime = Now

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Diet"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDDIET.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        Dim oKelasBed As New Reference.clsKelasAplicareBed

        Dim dsCekBed = oKelasBed.GetDataAda()
        If dsCekBed IsNot Nothing Then
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            chkPemetaan.Checked = True
        Else
            lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            chkPemetaan.Checked = False
        End If

        fn_LoadKDDOCTOR()
        fn_LoadKDBENTUKMAKANAN()
        fn_LoadKDJENISDIET()

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
        btnReload.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        cboKATEGORI.Properties.ReadOnly = Status

        txtMEMO.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        txtKDDIET.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        cboKATEGORI.ResetText()
        txtMEMO.ResetText()

        grvDetail.OptionsSelection.MultiSelect = True
        grvDetail.SelectAll()
        grvDetail.DeleteSelectedRows()
        grvDetail.OptionsSelection.MultiSelect = False
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oDiet.GetData(sNoId)

            With ds
                txtKDDIET.Text = .KDDIET
                deDATE.DateTime = .DATE
                cboKATEGORI.Text = .KATEGORI
                txtMEMO.Text = .MEMO

                BindingSource.DataSource = oDiet.GetDataDetail.Where(Function(x) x.KDDiet = sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If cboKATEGORI.Text = String.Empty Then
                cboKATEGORI.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                cboKATEGORI.ErrorText = Statement.ErrorRequired

                cboKATEGORI.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvDetail.UpdateCurrentRow()

            If grvDetail.RowCount < 2 Then
                MsgBox(Statement.ErrorDetail, MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oDiet.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oDiet.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now

                .KDDIET = sNoId
                .DATE = deDATE.DateTime
                .KATEGORI = cboKATEGORI.Text
                .KDUSER = sUserID
                .MEMO = txtMEMO.Text
            End With

            Dim oDoctor As New Reference.clsDoctor
            ' ***** DETIL *****
            Dim arrDetail = oDiet.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oDiet.GetStructureDetail
                With dsDetail
                    .KDDIET = ds.KDDIET
                    .SEQ = i
                    .KDIDENTITAS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDIDENTITAS)), 0, grvDetail.GetRowCellValue(i, colKDIDENTITAS))
                    .NAMAPASIEN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colNAMAPASIEN)), "", grvDetail.GetRowCellValue(i, colNAMAPASIEN))
                    .KDCUSTOMER = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDCUSTOMER)), "", grvDetail.GetRowCellValue(i, colKDCUSTOMER))
                    .BED = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colBED)), "", grvDetail.GetRowCellValue(i, colBED))
                    .RUANGAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colRUANGAN)), "", grvDetail.GetRowCellValue(i, colRUANGAN))
                    .KDDOCTOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDDOCTOR)), "", grvDetail.GetRowCellValue(i, colKDDOCTOR))
                    Dim dsDoctor = oDoctor.GetData(.KDDOCTOR)
                    If dsDoctor IsNot Nothing Then
                        .KDDOCTOR_NAMEDISPLAY = dsDoctor.NAME_DISPLAY
                    Else
                        .KDDOCTOR_NAMEDISPLAY = ""
                    End If
                    .KDBENTUKMAKANAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDBENTUKMAKANAN)), "", grvDetail.GetRowCellValue(i, colKDBENTUKMAKANAN))
                    .KDJENISDIET = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDJENISDIET)), "", grvDetail.GetRowCellValue(i, colKDJENISDIET))
                    .ISCHEKED = False
                    .REMARKS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colREMARKS)), "", grvDetail.GetRowCellValue(i, colREMARKS))
                    .BATASMAKAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colBATASMAKAN)), "", grvDetail.GetRowCellValue(i, colBATASMAKAN))
                    .WAKTUMAKAN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colWAKTUMAKAN)), "", grvDetail.GetRowCellValue(i, colWAKTUMAKAN))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oDiet.InsertData(ds, arrDetail)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oDiet.UpdateData(ds, arrDetail)
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
#Region "Grid Method"
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged

    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmDiet_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            Case Keys.F2
                If btnSaveNew.Enabled = True Then
                    btnSaveNew_Click()
                End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
                End If
            Case Keys.F5
                If btnReload.Enabled = True Then
                    btnReload_Click()
                End If
        End Select
    End Sub
    Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            sStatusSave = "NEW"
            Me.Close()
        End If
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
    Private Sub chkPemetaan_EditValueChanged(sender As Object, e As EventArgs) Handles chkPemetaan.EditValueChanged
        If isLoad = True Then
            If chkPemetaan.Checked = False Then
                lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
            Else
                lDATEFROM.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
                lDATETO.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
            End If
        End If
    End Sub
    Private Sub btnReload_Click() Handles btnReload.ItemClick
        Try
            'If fn_Validate() = False Then Exit Sub

            If cboKATEGORI.Text = "" Then
                MsgBox("Waktu Makan Kosong", MsgBoxStyle.Information, Me.Text)
                Exit Sub
            End If

            grvDetail.OptionsSelection.MultiSelect = True
            grvDetail.SelectAll()
            grvDetail.DeleteSelectedRows()
            grvDetail.OptionsSelection.MultiSelect = False

            If chkPemetaan.Checked = True Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)
                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT "
                SQL &= "* "
                SQL &= "FROM ( "
                SQL &= "SELECT	"
                SQL &= "A.KODEBED "
                SQL &= ",A.BED "
                SQL &= ",A.KDUPDATE_APLICARE "
                SQL &= ",KDPENDAFTARAN = ISNULL((Z.KODEREGISTER), '') "
                SQL &= ",NoRM = ISNULL((Z.KDCUSTOMER), '') "
                SQL &= ",NoRegister = ISNULL((Z.KDKUNJUNGAN), '') "
                SQL &= ",Antrian = ISNULL((Z.KDBOOKING), '')  "
                SQL &= ",KodeBooking = ISNULL((Z.KODEBOOKING), '') "
                SQL &= ",Pasien = ISNULL((Z.PASIEN), '')  "
                SQL &= ",WaktuPeriksa = BBB.NAME_DISPLAY  "
                SQL &= ",Dokter = ISNULL((Z.DOKTER), '') "
                SQL &= ",SIP = ISNULL((Z.SIP), '') "
                SQL &= ",KDCUSTOMER = ISNULL((Z.KDCUSTOMER), '') "
                SQL &= ",USIA = ISNULL(CONVERT(nvarchar(50), DATEDIFF(YEAR,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE()))) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE())) - (DATEDIFF(YEAR,  ISNULL((Z.TANGGALLAHIR), GETDATE()), Z.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH,  ISNULL((Z.TANGGALLAHIR), GETDATE()), ISNULL((Z.DATE), GETDATE())),  ISNULL((Z.TANGGALLAHIR), GETDATE())), ISNULL((Z.DATE), GETDATE()))) + ' Hari' , '')	"
                SQL &= ",TANGGALLAHIR  = ISNULL((Z.TANGGALLAHIR), GETDATE())	"
                SQL &= ",KDDIAGNOSA = ISNULL((Z.KDDIAGNOSA), '') "
                SQL &= ",DIAGNOSA = '' "
                SQL &= ",DATE = ISNULL((Z.DATE), GETDATE())	"
                SQL &= ",WaktuSelisih = ISNULL((Z.GABUNGRUANGAN), '') "
                SQL &= ",PENJAMIN = ISNULL((Z.PENJAMIN), '') "
                SQL &= ",NOMORSEP = ISNULL((Z.NOMORSEP), '') "
                SQL &= ",KARTUBPJS = ISNULL((Z.KARTUBPJS), '') "
                SQL &= ",Cek = ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END ), CONVERT(BIT, 0))	"
                SQL &= ",Cek2 = ISNULL((Z.KDDAFTAR_L6), '') "
                SQL &= ",KDDOCTOR = ISNULL((Z.KDDOCTOR), '')	"
                SQL &= ",JK = ISNULL((Z.KDJENISKELAMIN), '') "
                SQL &= ",ISPERAWAT =  ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END ), CONVERT(BIT, 0)) "
                SQL &= ",Keterangan = A.STATUS	"
                SQL &= ",KDDAFTAR_L6 = ISNULL((CASE Z.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END), 'Z') "
                SQL &= ",KDKELASRAWAT = ISNULL((Z.KDKELASRAWAT), '')	"
                SQL &= ",DPJPKeDua = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 2), '') "
                SQL &= ",DPJPKeTiga = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 3), '') "
                SQL &= ",DPJPKeEmpat = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 4), '') "
                SQL &= ",DPJPKeLima = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE ISNULL((Z.KDPENDAFTARAN), '')  = AA.KDREG AND AA.SEQ = 5), '') "
                SQL &= ",KDPENDAFTARAN_AWAL = ISNULL((Z.KDPENDAFTARAN_AWAL), '')	"
                SQL &= ",CATEGORY = ISNULL((Z.CATEGORY), '')	"
                SQL &= ",CekSudahAdaCPPT = A.MEMO "
                SQL &= "FROM	"
                SQL &= "M_KELASAPLICARE_DEPARTMENT_BED A	"
                SQL &= "INNER JOIN M_DEPARTMENT BBB	"
                SQL &= "ON A.KDDEPARTMENT = BBB.KDDEPARTMENT	"
                SQL &= "LEFT JOIN (SELECT GABUNGRUANGAN = FF.ANTRIAN,KODEREGISTER = AA.KDPENDAFTARAN,AA.KDKUNJUNGAN, BB.KDPENDAFTARAN_AWAL, BB.CATEGORY,BB.KDKELASRAWAT,CC.KDJENISKELAMIN,AA.KDDOCTOR,BB.NOMORSEP, BB.KDDAFTAR_L6,BB.KARTUBPJS, BB.DATE,AA.KDPENDAFTARAN, BB.KDDIAGNOSA,PASIEN = CC.NAME_DISPLAY, BB.KDCUSTOMER, BB.KODEBOOKING, BB.KDBOOKING, PENJAMIN = EE.MEMO,DOKTER = DD.NAME_DISPLAY, DD.SIP, CC.TANGGALLAHIR FROM S_PENDAFTARAN_KUNJUNGAN AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN INNER JOIN M_CUSTOMER CC ON BB.KDCUSTOMER = CC.KDCUSTOMER INNER JOIN M_DOCTOR DD ON AA.KDDOCTOR = DD.KDDOCTOR INNER JOIN M_DAFTAR_L1 EE ON BB.KDDAFTAR_L1 = EE.KDDAFTAR_L1 INNER JOIN M_DEPARTMENT FF ON AA.KDDEPARTMENT = FF.KDDEPARTMENT) Z ON A.KDPENDAFTARAN = Z.KDKUNJUNGAN "
                SQL &= ") XX "
                SQL &= "ORDER BY XX.WaktuPeriksa, XX.BED ASC "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_LISTPASIEN")


                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                Dim oGrouperDataCppt As New Grouper.clsR_CPPT
                Dim oBentukMakanan As New Reference.clsBentukMakanan
                Dim oJenisDiet As New Reference.clsJenisDiet
                Dim oDepartment As New Reference.clsDepartment

                For iLoop As Integer = 0 To ds.Tables("S_LISTPASIEN").Rows.Count - 1
                    With ds.Tables("S_LISTPASIEN")
                        Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(.Rows(iLoop)("NoRegister"))
                        If dsIdentitas IsNot Nothing Then
                            Dim dsDepartment = oDepartment.GetData(dsIdentitas.KDDEPARTMENT)

                            If dsDepartment Is Nothing Then
                                grvDetail.SetFocusedRowCellValue(colRUANGAN, dsIdentitas.KDDEPARTMENT_NAMA)
                            Else
                                grvDetail.SetFocusedRowCellValue(colRUANGAN, dsDepartment.ANTRIAN)
                            End If

                            grvDetail.Focus()
                            grvDetail.AddNewRow()
                            grvDetail.SetFocusedRowCellValue(colKDIDENTITAS, dsIdentitas.KDIDENTITAS)
                            grvDetail.SetFocusedRowCellValue(colNAMAPASIEN, dsIdentitas.NAMAPASIEN)
                            grvDetail.SetFocusedRowCellValue(colKDCUSTOMER, dsIdentitas.KDCUSTOMER)
                            grvDetail.SetFocusedRowCellValue(colBED, .Rows(iLoop)("BED"))


                            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, dsIdentitas.KDDOCTOR)
                            grvDetail.SetFocusedRowCellValue(colKDBENTUKMAKANAN, oBentukMakanan.AmbilDefault)
                            grvDetail.SetFocusedRowCellValue(colKDJENISDIET, oJenisDiet.AmbilDefault)
                            grvDetail.SetFocusedRowCellValue(colWAKTUMAKAN, cboKATEGORI.Text)
                            grvDetail.SetFocusedRowCellValue(colBATASMAKAN, dsIdentitas.KELAS & " / " & dsIdentitas.KDDEPARTMENT_NAMA & " / " & .Rows(iLoop)("BED"))
                            grvDetail.UpdateCurrentRow()
                        End If
                    End With
                Next
            Else
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String
                Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\SW\", "Database", "").ToString())

                oConn = New SqlConnection(sConn)
                If oConn.State = ConnectionState.Closed Then
                    oConn.Open()
                End If

                SQL = "SELECT "
                SQL &= "* "
                SQL &= "FROM ( "
                SQL &= "SELECT "
                SQL &= "B.KDPENDAFTARAN "
                SQL &= ",KODEBED = '' "
                SQL &= ",BED = '' "
                SQL &= ",KDUPDATE_APLICARE = '' "
                SQL &= ",NoRM = B.KDCUSTOMER "
                SQL &= ",NoRegister = A.KDKUNJUNGAN "
                SQL &= ",Antrian = B.KDBOOKING "
                SQL &= ",KodeBooking = B.KODEBOOKING "
                SQL &= ",Pasien = D.NAME_DISPLAY "
                SQL &= ",WaktuPeriksa = G.NAME_DISPLAY  "
                SQL &= ",Dokter = C.NAME_DISPLAY "
                SQL &= ",SIP = C.SIP "
                SQL &= ",B.KDCUSTOMER "
                SQL &= ",USIA = CONVERT(nvarchar(50), DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE)) + ' Tahun, ' + CONVERT(nvarchar(50), DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE) - (DATEDIFF(YEAR, D.TANGGALLAHIR, B.DATE) * 12)) + ' Bulan, ' + CONVERT(nvarchar(50), DATEDIFF(DAY, DATEADD(MONTH, DATEDIFF(MONTH, D.TANGGALLAHIR, B.DATE), D.TANGGALLAHIR), B.DATE)) + ' Hari' "
                SQL &= ",D.TANGGALLAHIR "
                SQL &= ",B.KDDIAGNOSA "
                SQL &= ",DIAGNOSA = E.MEMO "
                SQL &= ",A.DATE "
                SQL &= ",WaktuSelisih = ISNULL((SELECT  DATEDIFF(MINUTE, B.DATE, DATE) FROM SET_WAKTUTUNGGU WHERE B.KODEBOOKING = KODEBOOKING AND ISPANGGIL = 4) ,'') "
                SQL &= ",PENJAMIN = F.MEMO "
                SQL &= ",B.NOMORSEP "
                SQL &= ",B.KARTUBPJS "
                SQL &= ",Cek = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000003' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                SQL &= ",Cek2 = B.KDDAFTAR_L6 "
                SQL &= ",C.KDDOCTOR "
                SQL &= ",JK = D.KDJENISKELAMIN "
                SQL &= ",ISPERAWAT =  CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000002' THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END "
                SQL &= ",Keterangan = ISNULL((SELECT STRING_AGG(AA.PLANNING_ALASAN,', ') FROM DATABASERME..R_CPPT AA INNER JOIN DATABASERME..A_IDENTITASPASIEN_LIST BB ON AA.KDIDENTITAS = BB.KDIDENTITAS WHERE BB.KDKUNJUNGAN = A.KDKUNJUNGAN AND AA.KDPROFESI = 'PROFESI_0000000001'), '') "
                SQL &= ",KDDAFTAR_L6 = CASE B.KDDAFTAR_L6 WHEN 'DAFTAR_L6_0000000001' THEN 'X' WHEN 'DAFTAR_L6_0000000002' THEN 'Y' ELSE 'Z' END "
                SQL &= ",B.KDKELASRAWAT "
                SQL &= ",DPJPKeDua = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 2), '') "
                SQL &= ",DPJPKeTiga = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 3), '') "
                SQL &= ",DPJPKeEmpat = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 4), '') "
                SQL &= ",DPJPKeLima = ISNULL((SELECT BB.NAME_DISPLAY FROM I_TRACKING_KDDOCTOR AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR_KEPADA = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDREG AND AA.SEQ = 5), '') "
                SQL &= ",B.KDPENDAFTARAN_AWAL "
                SQL &= ",B.CATEGORY "
                SQL &= ",CekSudahAdaCPPT = '' "
                SQL &= "FROM "
                SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
                SQL &= "INNER JOIN S_PENDAFTARAN_H B "
                SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
                SQL &= "INNER JOIN M_DOCTOR C "
                SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
                SQL &= "INNER JOIN M_CUSTOMER D "
                SQL &= "ON B.KDCUSTOMER = D.KDCUSTOMER "
                SQL &= "INNER JOIN M_DIAGNOSA E "
                SQL &= "ON B.KDDIAGNOSA = E.KDDIAGNOSA "
                SQL &= "INNER JOIN M_DAFTAR_L1 F "
                SQL &= "ON B.KDDAFTAR_L1 = F.KDDAFTAR_L1 "
                SQL &= "INNER JOIN M_DEPARTMENT G "
                SQL &= "ON A.KDDEPARTMENT = G.KDDEPARTMENT "

                'SEMUA
                SQL &= "WHERE B.STATUSDAFTAR <> '3' "
                SQL &= "AND B.CATEGORY = 1 "
                SQL &= "AND CONVERT(VARCHAR(8), B.DATE, 112) >= '" & deDATE.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), B.DATE, 112) <= '" & deDateTo.DateTime.ToString("yyyyMMdd") & "' "

                SQL &= "AND ISNULL((SELECT TOP 1 'Y' FROM DATABASERS..T_UPDATE_TANGGAL_PULANG WHERE KDPENDAFTARAN = B.KDPENDAFTARAN), '') = '' "

                SQL &= ") Z "
                'SQL &= "WHERE ISNULL((SELECT KDIDENTITAS FROM DATABASERME..A_IDENTITASPASIEN_LIST WHERE Z.NoRegister = KDKUNJUNGAN), 0) <> 0 "
                SQL &= "ORDER BY Z.KDDAFTAR_L6, Z.NoRegister ASC "


                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "S_LISTPASIEN")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If

                Dim oGrouperDataCppt As New Grouper.clsR_CPPT
                Dim oBentukMakanan As New Reference.clsBentukMakanan
                Dim oJenisDiet As New Reference.clsJenisDiet
                Dim oDepartment As New Reference.clsDepartment

                For iLoop As Integer = 0 To ds.Tables("S_LISTPASIEN").Rows.Count - 1
                    With ds.Tables("S_LISTPASIEN")
                        Dim dsIdentitas = oGrouperDataCppt.GetDataByKdKunjungan(.Rows(iLoop)("NoRegister"))
                        If dsIdentitas IsNot Nothing Then
                            Dim dsDepartment = oDepartment.GetData(dsIdentitas.KDDEPARTMENT)

                            If dsDepartment Is Nothing Then
                                grvDetail.SetFocusedRowCellValue(colRUANGAN, dsIdentitas.KDDEPARTMENT_NAMA)
                            Else
                                grvDetail.SetFocusedRowCellValue(colRUANGAN, dsDepartment.ANTRIAN)
                            End If

                            grvDetail.Focus()
                            grvDetail.AddNewRow()
                            grvDetail.SetFocusedRowCellValue(colKDIDENTITAS, dsIdentitas.KDIDENTITAS)
                            grvDetail.SetFocusedRowCellValue(colNAMAPASIEN, dsIdentitas.NAMAPASIEN)
                            grvDetail.SetFocusedRowCellValue(colKDCUSTOMER, dsIdentitas.KDCUSTOMER)
                            grvDetail.SetFocusedRowCellValue(colBED, .Rows(iLoop)("BED"))


                            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, dsIdentitas.KDDOCTOR)
                            grvDetail.SetFocusedRowCellValue(colKDBENTUKMAKANAN, oBentukMakanan.AmbilDefault)
                            grvDetail.SetFocusedRowCellValue(colKDJENISDIET, oJenisDiet.AmbilDefault)
                            grvDetail.SetFocusedRowCellValue(colWAKTUMAKAN, cboKATEGORI.Text)
                            grvDetail.SetFocusedRowCellValue(colBATASMAKAN, dsIdentitas.KELAS & " / " & dsIdentitas.KDDEPARTMENT_NAMA & " / " & .Rows(iLoop)("BED"))
                            grvDetail.UpdateCurrentRow()
                        End If
                    End With
                Next
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDDOCTOR()
        Dim oDoctor As New Reference.clsDoctor
        Try
            grdKDDOKTER.DataSource = oDoctor.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOKTER.ValueMember = "KDDOCTOR"
            grdKDDOKTER.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDBENTUKMAKANAN()
        Dim oBentukMakanan As New Reference.clsBentukMakanan
        Try
            grdKDBENTUKMAKANAN.DataSource = oBentukMakanan.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDBENTUKMAKANAN.ValueMember = "KDBENTUKMAKANAN"
            grdKDBENTUKMAKANAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDJENISDIET()
        Dim oJenisDiet As New Reference.clsJenisDiet
        Try
            grdKDJENISDIET.DataSource = oJenisDiet.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDJENISDIET.ValueMember = "KDJENISDIET"
            grdKDJENISDIET.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub CetakEtiketToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CetakEtiketToolStripMenuItem.Click
        Try
            If grvDetail.GetFocusedRowCellValue("KDDIET") Is Nothing Then
                Exit Sub
            End If

            Dim dsEtiket = oDiet.GetStructureHeaderEtiket
            Dim oDietMaster As New Reference.clsJenisDiet
            Dim oBentukMakananMaster As New Reference.clsBentukMakanan
            Dim oGrouperDataCppt As New Grouper.clsR_CPPT

            With dsEtiket
                .RUANGAN = grvDetail.GetFocusedRowCellValue("RUANGAN")
                .DESCRIPTION = grvDetail.GetFocusedRowCellValue("WAKTUMAKAN")
                .NAMA = grvDetail.GetFocusedRowCellValue("KDCUSTOMER") & " / " & grvDetail.GetFocusedRowCellValue("NAMAPASIEN")
                .NORM = grvDetail.GetFocusedRowCellValue("KDCUSTOMER")
                .EXPIREOBAT = oGrouperDataCppt.GetDataIdentitas(grvDetail.GetFocusedRowCellValue("KDIDENTITAS")).TANGGALLAHIR.ToString("dd-MM-yyyy")
                .NORESEP = grvDetail.GetFocusedRowCellValue("BATASMAKAN")
                .NAMAOBAT = oDietMaster.GetData(grvDetail.GetFocusedRowCellValue("KDJENISDIET")).MEMO
                .DOKTER = oBentukMakananMaster.GetData(grvDetail.GetFocusedRowCellValue("KDBENTUKMAKANAN")).MEMO
                .TANGGAL = Now
                .TANGGALLAHIR = Now

                Dim rpt As New xtraEtiketGizi
                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK
                rpt.BindingSource.DataSource = dsEtiket
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.Print()
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class