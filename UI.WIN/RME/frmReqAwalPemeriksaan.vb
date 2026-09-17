Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmReqAwalPemeriksaan
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sKDIDENTITAS As Integer
    Private isLoad As Boolean = False
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private sKelas As String = String.Empty
    Private sKDKUNJUNGAN As String = String.Empty
    Private sKDPENDAFTARAN As String = String.Empty
    Private sKDDEPARTMENT As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private oRME As New RME.clsRME
    Private sCategory As Integer = 0
    Private listTindakanLab_LoadData As New List(Of String)
    Private listTindakanRad_LoadData As New List(Of String)
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDIDENTITAS As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
        sKDIDENTITAS = KDIDENTITAS
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Pemeriksaan Awal"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = txtKDCPPT.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadKDPROFESI()
        fn_LoadKDUOM()
        fn_LoadKDITEMSearch()

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

        Dim ds = oGrouperDataCppt.GetDataIdentitas(sKDIDENTITAS)
        If ds IsNot Nothing Then
            sCategory = ds.CATEGORY
            txtNAMAPASIEN.Text = ds.NAMAPASIEN
            deDATETANGGALLAHIR.DateTime = ds.TANGGALLAHIR
            txtKDCUSTOMER.Text = ds.KDCUSTOMER
            txtOBJEKTIF_UMUR.Text = oRME.GetUmurPasien(ds.DATE, ds.TANGGALLAHIR)
            txtOBJEKTIF_JENISKELAMIN.Text = ds.JENISKELAMIN

            sKDKUNJUNGAN = ds.KDKUNJUNGAN
            sKDDEPARTMENT = ds.KDDEPARTMENT
            sKDDOCTOR = ds.KDDOCTOR

            Dim oSalesOrderTransaksi As New Sales.clsSalesOrderTransaksi
            Dim dsKelas = oSalesOrderTransaksi.GetDataKunjunganByKD(ds.KDKUNJUNGAN)
            If dsKelas IsNot Nothing Then
                sKelas = dsKelas.S_PENDAFTARAN_H.KDKELASRAWAT
                sKDPENDAFTARAN = dsKelas.KDPENDAFTARAN
                txtNOMORANTRIAN.Text = dsKelas.S_PENDAFTARAN_H.KDBOOKING
            End If

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim dsReload = oGrouperDataCppt.GetDataByRmTerakhirTTV(txtKDCUSTOMER.Text)

                If dsReload IsNot Nothing Then
                    With dsReload
                        'chkALERGI_YA.Checked = .SUBJEKTIF_ALERGI_YA
                        'chkALERGI_TIDAK.Checked = .SUBJEKTIF_ALERGI_TIDAK
                        'txtALERGI_TEXT.Text = .SUBJEKTIF_ALERGI_YA_TEXT
                        Try
                            txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, 0, .OBJEKTIF_BERATBADAN)
                            txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, 0, .OBJEKTIF_TINGGIBADAN)
                        Catch ex As Exception
                            txtOBJEKTIF_BERATBADAN.Text = 0
                            txtOBJEKTIF_TINGGIBADAN.Text = 0
                        End Try
                        'txtCPPT_Sistole.Text = .OBJEKTIF_SISTOLE
                        'txtCPPT_Diastole.Text = .OBJEKTIF_DIASTOLE
                        txtKESADARAN.Text = .OBJEKTIF_KESADARAN
                    End With
                End If

                Dim dsAlergi = oGrouperDataCppt.GetDataByRmTerakhirTTVAlergi(txtKDCUSTOMER.Text)
                If dsAlergi IsNot Nothing Then
                    chkALERGI_YA.Checked = dsAlergi.SUBJEKTIF_ALERGI_YA
                    chkALERGI_TIDAK.Checked = dsAlergi.SUBJEKTIF_ALERGI_TIDAK
                    txtALERGI_TEXT.Text = dsAlergi.SUBJEKTIF_ALERGI_YA_TEXT

                    txtALERGI_TEXT.BackColor = Color.Red
                Else
                    txtALERGI_TEXT.BackColor = Color.White
                End If
            End If
        End If
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        btnSaveNew.Enabled = Not Status
        btnSaveClose.Enabled = Not Status
        btnPending.Enabled = Not Status

        deDATE.Properties.ReadOnly = Status
        chkALERGI_YA.Properties.ReadOnly = Status
        chkALERGI_TIDAK.Properties.ReadOnly = Status
        txtALERGI_TEXT.Properties.ReadOnly = Status
        txtNAMAPASIEN.Properties.ReadOnly = True
        deDATETANGGALLAHIR.Properties.ReadOnly = True
        txtKDCUSTOMER.Properties.ReadOnly = True
        txtSUBJEKTIF.Properties.ReadOnly = Status
        txtOBJEKTIF_JENISKELAMIN.Properties.ReadOnly = True
        txtOBJEKTIF_UMUR.Properties.ReadOnly = True
        txtOBJEKTIF_BERATBADAN.Properties.ReadOnly = Status
        txtOBJEKTIF_TINGGIBADAN.Properties.ReadOnly = Status
        txtCPPT_Diastole.Properties.ReadOnly = Status
        txtCPPT_Sistole.Properties.ReadOnly = Status
        txtOBJEKTIF_NADI.Properties.ReadOnly = Status
        txtOBJEKTIF_RESPIRASI.Properties.ReadOnly = Status
        txtOBJEKTIF_SATURASIOKSIGEN.Properties.ReadOnly = Status
        txtOBJEKTIF_SUHU.Properties.ReadOnly = Status
        txtDESKRIPSI.Properties.ReadOnly = Status
        txtAssesment.Properties.ReadOnly = Status
        txtPlanning.Properties.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtAssesment.ResetText()
        txtPlanning.ResetText()
        'txtKDAWALPEMERIKSAAN.Text = "<--- AUTO --->"
        txtKDCPPT.Text = sNoId
        deDATE.DateTime = Now
        chkALERGI_YA.Checked = False
        chkALERGI_TIDAK.Checked = False
        txtALERGI_TEXT.ResetText()
        txtNAMAPASIEN.ResetText()
        deDATETANGGALLAHIR.ResetText()
        txtKDCUSTOMER.ResetText()
        txtSUBJEKTIF.ResetText()
        txtOBJEKTIF_JENISKELAMIN.ResetText()
        txtOBJEKTIF_UMUR.ResetText()
        txtOBJEKTIF_BERATBADAN.ResetText()
        txtOBJEKTIF_TINGGIBADAN.ResetText()
        txtCPPT_Diastole.Text = 0
        txtCPPT_Sistole.Text = 0
        txtOBJEKTIF_NADI.ResetText()
        txtOBJEKTIF_RESPIRASI.ResetText()
        txtOBJEKTIF_SATURASIOKSIGEN.ResetText()
        txtOBJEKTIF_SUHU.ResetText()
        txtDESKRIPSI.ResetText()


        grvCPPT_Tindakan.AddNewRow()
        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, "10138")
        grvCPPT_Tindakan.UpdateCurrentRow()
    End Sub
    'Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
    '    Dim years As Long
    '    Dim months As Long
    '    Dim days As Long
    '    Dim yearWord As String
    '    Dim monthWord As String
    '    Dim dayWord As String

    '    ' menghitung tahun
    '    years = DateDiff("yyyy", tgllahir, dateNow)
    '    If Month(tgllahir) > Month(dateNow) Then
    '        years = years - 1
    '    ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
    '        years = years - 1
    '    ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
    '        'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
    '    End If
    '    ' menghitung bulan
    '    tgllahir = DateAdd("yyyy", years, tgllahir)
    '    months = DateDiff("m", tgllahir, dateNow)
    '    If tgllahir.Day > dateNow.Day Then
    '        months = months - 1
    '    ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
    '        months = months - 1
    '    End If
    '    tgllahir = DateAdd("m", months, tgllahir)
    '    ' menghitung hari
    '    days = DateDiff("d", tgllahir, dateNow)

    '    yearWord = IIf(years = 0, "", years & " Tahun ")
    '    monthWord = IIf(months = 0, "", months & " Bulan ")
    '    dayWord = IIf(days = 0, "", days & " Hari ")
    '    'calculateAge = yearWord & monthWord & dayWord
    '    'calculateAge = Trim(calculateAge)

    '    'GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
    '    GetUmurPasien = yearWord
    'End Function
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oGrouperDataCppt.GetData(sNoId)

            With ds
                grdCPPT_Profesi.Text = .KDPROFESI
                txtKDCPPT.Text = .KDCPPT
                deDATE.DateTime = .DATE
                chkALERGI_YA.Checked = .SUBJEKTIF_ALERGI_YA
                chkALERGI_TIDAK.Checked = .SUBJEKTIF_ALERGI_TIDAK
                txtALERGI_TEXT.Text = .SUBJEKTIF_ALERGI_YA_TEXT
                'txtNAMAPASIEN.Text = .NAMAPASIEN
                'deDATETANGGALLAHIR.DateTime = .TANGGALLAHIR
                'txtKDCUSTOMER.Text = .KDCUSTOMER
                txtSUBJEKTIF.Text = .SUBJEKTIF_KELUHANUTAMA
                'txtOBJEKTIF_JENISKELAMIN.Text = .OBJEKTIF_JENISKELAMIN
                'txtOBJEKTIF_UMUR.Text = .OBJEKTIF_UMUR
                Try
                    txtOBJEKTIF_BERATBADAN.Text = IIf(CDec(.OBJEKTIF_BERATBADAN) = 0, "", .OBJEKTIF_BERATBADAN)
                    txtOBJEKTIF_TINGGIBADAN.Text = IIf(CDec(.OBJEKTIF_TINGGIBADAN) = 0, "", .OBJEKTIF_TINGGIBADAN)

                Catch ex As Exception
                    txtOBJEKTIF_BERATBADAN.Text = 0
                    txtOBJEKTIF_TINGGIBADAN.Text = 0
                End Try
                txtCPPT_Sistole.Text = .OBJEKTIF_SISTOLE
                txtCPPT_Diastole.Text = .OBJEKTIF_DIASTOLE
                txtOBJEKTIF_NADI.Text = .OBJEKTIF_HR
                txtOBJEKTIF_RESPIRASI.Text = .OBJEKTIF_RR
                txtOBJEKTIF_SATURASIOKSIGEN.Text = .OBJEKTIF_SPO2
                txtOBJEKTIF_SUHU.Text = .OBJEKTIF_SUHU
                txtDESKRIPSI.Text = .OBJEKTIF_PEMERIKSAAN
                'txtANTRIANDOKTER.Text = .ANTRIANDOKTER
                'cboProfesi.Text = .PROPESI
                txtAssesment.Text = .ASSEMENT_TEXT
                txtPlanning.Text = .PLANNING_TEXT

                txtIMT_TAMBAH2.Text = .OBJEKTIF_GCS
                txtBBIDEAL_TAMBAH3.Text = .OBJEKTIF_TAMPAKSAKIT
                txtBBTURUN_TAMBAH4.Text = .OBJEKTIF_VISUALANALOGSCORE

                bindingSource.DataSource = oGrouperDataCppt.GetDataDetailTindakan(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdCPPT_Tindakan.DataSource = bindingSource

                Dim oItem As New Reference.clsItem

                For Each xloop In oGrouperDataCppt.GetDataDetailTindakan(ds.KDCPPT)
                    Dim dsItem = oItem.GetData(xloop.KDITEM)

                    If dsItem IsNot Nothing Then
                        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                            listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                            listTindakanLab_LoadData.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.memO = "RADIOLOGI"
                            listTindakanRad_LoadData.Add(dsItem.NMITEM2)
                        End If
                    End If
                Next
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            Try
                If txtOBJEKTIF_BERATBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_BERATBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Berat Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            Try
                If txtOBJEKTIF_TINGGIBADAN.Text <> "" Then
                    Dim TES = CDec(txtOBJEKTIF_TINGGIBADAN.Text)
                End If
            Catch ex As Exception
                MsgBox("Tinggi Badan Harus Angka" & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                fn_Validate = False
                Exit Function
            End Try

            If txtSUBJEKTIF.Text = String.Empty Then
                txtSUBJEKTIF.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtSUBJEKTIF.ErrorText = Statement.ErrorRequired

                txtSUBJEKTIF.Focus()
                fn_Validate = False
                Exit Function
            End If

            If grdCPPT_Profesi.Text = String.Empty Then
                grdCPPT_Profesi.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdCPPT_Profesi.ErrorText = Statement.ErrorRequired

                grdCPPT_Profesi.Focus()
                fn_Validate = False
                Exit Function
            End If

            If sKDPENDAFTARAN = String.Empty Then
                MsgBox("Nomor Pendaftaran Kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtKDCPPT.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDCPPT.ErrorText = Statement.ErrorRequired

                txtKDCPPT.Focus()
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
            Dim ds = oGrouperDataCppt.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCPPT = txtKDCPPT.Text
                .KDIDENTITAS = sKDIDENTITAS
                .KDPROFESI = grdCPPT_Profesi.EditValue
                .SUBJEKTIF_KELUHANUTAMA = txtSUBJEKTIF.Text
                .SUBJEKTIF_ALERGI_TIDAK = chkALERGI_TIDAK.Checked
                .SUBJEKTIF_ALERGI_YA = chkALERGI_YA.Checked
                .SUBJEKTIF_ALERGI_YA_TEXT = txtALERGI_TEXT.Text

                Dim listSubjektif As New List(Of String)

                listSubjektif.Add("Keluhan Utama " & .SUBJEKTIF_KELUHANUTAMA)

                Dim Alergi As String = String.Empty

                If .SUBJEKTIF_ALERGI_TIDAK = True Then
                    Alergi = "Alergi Obat: Tidak"
                End If
                If .SUBJEKTIF_ALERGI_YA = True Then
                    Alergi = "Alergi Obat: Ya"
                End If

                'listSubjektif.Add((Alergi & " " & .SUBJEKTIF_ALERGI_YA_TEXT).Trim)

                .SUBJEKTIF_TEXT = String.Join(vbCrLf, listSubjektif.ToArray)

                .OBJEKTIF_KESADARAN = txtKESADARAN.Text
                .OBJEKTIF_GCS = txtIMT_TAMBAH2.Text
                .OBJEKTIF_TAMPAKSAKIT = txtBBIDEAL_TAMBAH3.Text
                .OBJEKTIF_VISUALANALOGSCORE = txtBBTURUN_TAMBAH4.Text
                .OBJEKTIF_BERATBADAN = txtOBJEKTIF_BERATBADAN.Text
                .OBJEKTIF_TINGGIBADAN = txtOBJEKTIF_TINGGIBADAN.Text
                .OBJEKTIF_SPO2 = txtOBJEKTIF_SATURASIOKSIGEN.Text
                .OBJEKTIF_SISTOLE = txtCPPT_Sistole.Text
                .OBJEKTIF_DIASTOLE = txtCPPT_Diastole.Text
                .OBJEKTIF_HR = txtOBJEKTIF_NADI.Text
                .OBJEKTIF_RR = txtOBJEKTIF_RESPIRASI.Text
                .OBJEKTIF_SUHU = txtOBJEKTIF_SUHU.Text
                .OBJEKTIF_PEMERIKSAAN = txtDESKRIPSI.Text
                .OBJEKTIF_ALAMATGAMBARPEMERIKSAAN = ""

                Dim listOBjektif As New List(Of String)

                listOBjektif.Add("Umur : " & txtOBJEKTIF_UMUR.Text)

                If .OBJEKTIF_KESADARAN <> "" Then
                    listOBjektif.Add("Kesadaran : " & .OBJEKTIF_KESADARAN)
                End If
                If .OBJEKTIF_GCS <> "" Then
                    listOBjektif.Add("IMT : " & .OBJEKTIF_GCS)
                End If
                If .OBJEKTIF_TAMPAKSAKIT <> "" Then
                    listOBjektif.Add("BB Ideal : " & .OBJEKTIF_TAMPAKSAKIT)
                End If
                If .OBJEKTIF_VISUALANALOGSCORE <> "" Then
                    listOBjektif.Add("BB Yang diturunkan : " & .OBJEKTIF_VISUALANALOGSCORE)
                End If
                If .OBJEKTIF_BERATBADAN <> "" Then
                    listOBjektif.Add("Berat Badan : " & .OBJEKTIF_BERATBADAN.Replace(",", ".") & " kg")
                End If
                If .OBJEKTIF_TINGGIBADAN <> "" Then
                    listOBjektif.Add("Tinggi Badan : " & .OBJEKTIF_TINGGIBADAN.Replace(",", ".") & " cm")
                End If
                If .OBJEKTIF_SPO2 <> "" Then
                    listOBjektif.Add("SpO2 : " & .OBJEKTIF_SPO2 & " %")
                End If
                listOBjektif.Add("Tekanan Darah : " & .OBJEKTIF_SISTOLE & "/" & .OBJEKTIF_DIASTOLE & " mmHg")
                If .OBJEKTIF_HR <> "" Then
                    listOBjektif.Add("HR : " & .OBJEKTIF_HR & " x/mnt")
                End If
                If .OBJEKTIF_RR <> "" Then
                    listOBjektif.Add("RR : " & .OBJEKTIF_RR & " x/mnt")
                End If
                If .OBJEKTIF_SUHU <> "" Then
                    listOBjektif.Add("Suhu : " & .OBJEKTIF_SUHU & " oC")
                End If
                If .OBJEKTIF_PEMERIKSAAN <> "" Then
                    listOBjektif.Add("Pemeriksaan : " & .OBJEKTIF_PEMERIKSAAN)
                End If


                .OBJEKTIF_TEXT = String.Join(vbCrLf, listOBjektif.ToArray)
                .ASSEMENT_INDIKASI = ""
                .ASSEMENT_TEXT = txtAssesment.Text
                .PLANNING_ISTINDAKLANJUT_PULANG = False
                .PLANNING_ISTINDAKLANJUT_RAWAT = False
                .PLANNING_ISTINDAKLANJUT_KONSUL = False
                .PLANNING_ISTINDAKLANJUT_RUJUK = False
                .PLANNING_ISTINDAKLANJUT_KONSUL_TEXT = "DAFTAR_L4_0000000001"
                .PLANNING_ISTINDAKLANJUT_RUJUK_TEXT = ""
                .PLANNING_ALASAN = ""

                Dim listPlanning As New List(Of String)
                'Dim oItem As New Reference.clsItem

                'For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                '    Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                '    If dsItem IsNot Nothing Then
                '        If dsItem.KDITEM_L1 = "ITEM_L1_0000000002" Then
                '            listPlanning.Add(dsItem.NMITEM2 & " " & IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)))
                '        End If
                '    End If
                'Next

                .PLANNING_TEXT = txtPlanning.Text & IIf(listPlanning.Count > 0, vbCrLf & "" & String.Join(vbCrLf, listPlanning.ToArray), "")
                .CATATAN = ""
                .KDUSER = sUserID
                Try
                    .ISDELETE = oGrouperDataCppt.GetData(txtKDCPPT.Text).ISDELETE
                Catch ex As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oGrouperDataCppt.GetData(txtKDCPPT.Text).DATEDELETE
                Catch ex As Exception
                    .DATEDELETE = ds.DATECREATED
                End Try
                Try
                    .USERDELETE = oGrouperDataCppt.GetData(txtKDCPPT.Text).USERDELETE
                Catch ex As Exception
                    .USERDELETE = ""
                End Try
                Try
                    .GOALOFTREATMENT = oGrouperDataCppt.GetData(txtKDCPPT.Text).GOALOFTREATMENT
                Catch ex As Exception
                    .GOALOFTREATMENT = ""
                End Try
                Try
                    .TINDAKANREHAB = oGrouperDataCppt.GetData(txtKDCPPT.Text).TINDAKANREHAB
                Catch ex As Exception
                    .TINDAKANREHAB = ""
                End Try
                Try
                    .EDUKASI = oGrouperDataCppt.GetData(txtKDCPPT.Text).EDUKASI
                Catch ex As Exception
                    .EDUKASI = ""
                End Try
                Try
                    .FREKUENSIKUNJUNGAN = oGrouperDataCppt.GetData(txtKDCPPT.Text).FREKUENSIKUNJUNGAN
                Catch ex As Exception
                    .FREKUENSIKUNJUNGAN = ""
                End Try
            End With

            Dim arrDetailTindakan = oGrouperDataCppt.GetStructureDetailTindakanList
            For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                Dim dsDetail = oGrouperDataCppt.GetStructureDetailTindakan
                With dsDetail
                    .DATECREATED = ds.DATECREATED
                    .DATEUPDATED = ds.DATEUPDATED
                    .KDCPPT = ds.KDCPPT
                    .SEQ = i
                    .KDITEM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN)
                    .KDUOM = grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDUOMTINDAKAN)
                    .JUMLAH = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_JUMLAHTINDAKAN))
                    .HARGA = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_HARGATINDAKAN))
                    .TOTAL = CDec(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_TOTALTINDAKAN))
                    .MEMO = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN)), "", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_MEMOTINDAKAN))
                    .KDUSER = sUserID
                    .ISBACA = IIf(String.IsNullOrEmpty(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA)), "0", grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_ISBACA))
                    .ISPERAWAT = True
                End With
                arrDetailTindakan.Add(dsDetail)
            Next

            Dim arrDetailDiagnosa = oGrouperDataCppt.GetStructureDetailDiagnosaList
            Dim arrDetail = oGrouperDataCppt.GetStructureDetailNonRacikanList
            Dim arrDetailObatRacikan = oGrouperDataCppt.GetStructureDetailRacikanList

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    txtKDCPPT.Text = oGrouperDataCppt.InsertData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, Nothing)
                    If txtKDCPPT.Text = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oGrouperDataCppt.UpdateData(ds, arrDetailDiagnosa, arrDetailTindakan, arrDetail, arrDetailObatRacikan, Nothing)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If

            If fn_Save = True Then
                oGrouperDataCppt.UpdateDaftarL6(sKDPENDAFTARAN, "DAFTAR_L6_0000000002")

                Dim oItem As New Reference.clsItem
                Dim oOrder As New Digital.clsR_Order
                Dim listTindakanLab As New List(Of String)
                Dim listTindakanRad As New List(Of String)

                For i As Integer = 0 To grvCPPT_Tindakan.RowCount - 2
                    Dim dsItem = oItem.GetData(grvCPPT_Tindakan.GetRowCellValue(i, colCPPT_KDITEMTINDAKAN))
                    If dsItem IsNot Nothing Then
                        If dsItem.M_ITEM_L3.MEMO = "LABORATORIUM" Then
                            listTindakanLab.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.MEMO = "BANK DARAH" Then
                            listTindakanLab.Add(dsItem.NMITEM2)
                        ElseIf dsItem.M_ITEM_L3.MEMO = "RADIOLOGI" Then
                            listTindakanRad.Add(dsItem.NMITEM2)
                        End If
                    End If
                Next

                If listTindakanLab.Count > 0 Then
                    Dim UpddateLab As Boolean = False
                    Dim TambahHasil1 = String.Join(", ", listTindakanLab.ToArray)
                    Dim TambahHasil2 = String.Join(", ", listTindakanLab_LoadData.ToArray)

                    For Each yloop In listTindakanLab_LoadData
                        If Not TambahHasil1.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                    If dsNomorRefrenceLab Is Nothing Then
                        'Add

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                            .KDORDER = ""
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "TAMBAH"
                            .MEMO = "ORDER LABORATORIUM"
                        End With

                        oOrder.InsertData(dsOrder)
                    Else
                        'Update

                        If UpddateLab = True Then
                            Dim dsOrder = oOrder.GetStructureHeader
                            With dsOrder
                                .DATECREATED = ds.DATECREATED
                                .DATEUPDATED = ds.DATEUPDATED
                                .TANGGALORDER = ds.DATE
                                .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                                .KDORDER = dsNomorRefrenceLab.KDORDER
                                .KDIDENTITAS = ds.KDIDENTITAS
                                .NOMORREFERENCE = ds.KDCPPT
                                .USERORDER = ds.KDUSER
                                .STATUS = "UPDATE"
                                .MEMO = "ORDER LABORATORIUM"
                            End With

                            oOrder.UpdateData(dsOrder)
                        Else
                            If dsNomorRefrenceLab.STATUS = "DELETE" Then
                                Dim dsOrder = oOrder.GetStructureHeader
                                With dsOrder
                                    .DATECREATED = ds.DATECREATED
                                    .DATEUPDATED = ds.DATEUPDATED
                                    .TANGGALORDER = ds.DATE
                                    .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                                    .KDORDER = dsNomorRefrenceLab.KDORDER
                                    .KDIDENTITAS = ds.KDIDENTITAS
                                    .NOMORREFERENCE = ds.KDCPPT
                                    .USERORDER = ds.KDUSER
                                    .STATUS = "TAMBAH"
                                    .MEMO = "ORDER LABORATORIUM"
                                End With

                                oOrder.UpdateData(dsOrder)
                            End If
                        End If
                    End If
                Else
                    Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER LABORATORIUM")
                    If dsNomorRefrenceLab IsNot Nothing Then
                        'delete
                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "LABRJ", "LABRI")
                            .KDORDER = dsNomorRefrenceLab.KDORDER
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "DELETE"
                            .MEMO = "ORDER LABORATORIUM"
                        End With

                        oOrder.UpdateData(dsOrder)
                    End If
                End If

                If listTindakanRad.Count > 0 Then
                    Dim UpddateLab As Boolean = False
                    Dim TambahHasil1 = String.Join(", ", listTindakanRad.ToArray)
                    Dim TambahHasil2 = String.Join(", ", listTindakanRad_LoadData.ToArray)

                    For Each yloop In listTindakanRad_LoadData
                        If Not TambahHasil1.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next

                    For Each yloop In listTindakanRad
                        If Not TambahHasil2.Contains(yloop) Then
                            UpddateLab = True
                        End If
                    Next


                    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                    If dsNomorRefrenceRad Is Nothing Then
                        'Add

                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                            .KDORDER = ""
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "TAMBAH"
                            .MEMO = "ORDER RADIOLOGI"
                        End With

                        oOrder.InsertData(dsOrder)
                    Else
                        'Update

                        If UpddateLab = True Then
                            Dim dsOrder = oOrder.GetStructureHeader
                            With dsOrder
                                .DATECREATED = ds.DATECREATED
                                .DATEUPDATED = ds.DATEUPDATED
                                .TANGGALORDER = ds.DATE
                                .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                                .KDORDER = dsNomorRefrenceRad.KDORDER
                                .KDIDENTITAS = ds.KDIDENTITAS
                                .NOMORREFERENCE = ds.KDCPPT
                                .USERORDER = ds.KDUSER
                                .STATUS = "UPDATE"
                                .MEMO = "ORDER RADIOLOGI"
                            End With

                            oOrder.UpdateData(dsOrder)
                        Else
                            If dsNomorRefrenceRad.STATUS = "DELETE" Then
                                Dim dsOrder = oOrder.GetStructureHeader
                                With dsOrder
                                    .DATECREATED = ds.DATECREATED
                                    .DATEUPDATED = ds.DATEUPDATED
                                    .TANGGALORDER = ds.DATE
                                    .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                                    .KDORDER = dsNomorRefrenceRad.KDORDER
                                    .KDIDENTITAS = ds.KDIDENTITAS
                                    .NOMORREFERENCE = ds.KDCPPT
                                    .USERORDER = ds.KDUSER
                                    .STATUS = "TAMBAH"
                                    .MEMO = "ORDER RADIOLOGI"
                                End With

                                oOrder.UpdateData(dsOrder)
                            End If
                        End If
                    End If
                Else
                    Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(ds.KDCPPT, "ORDER RADIOLOGI")
                    If dsNomorRefrenceRad IsNot Nothing Then
                        'delete
                        Dim dsOrder = oOrder.GetStructureHeader
                        With dsOrder
                            .DATECREATED = ds.DATECREATED
                            .DATEUPDATED = ds.DATEUPDATED
                            .TANGGALORDER = ds.DATE
                            .JENISORDER = IIf(sCategory = 0, "RADRJ", "RADRI")
                            .KDORDER = dsNomorRefrenceRad.KDORDER
                            .KDIDENTITAS = ds.KDIDENTITAS
                            .NOMORREFERENCE = ds.KDCPPT
                            .USERORDER = ds.KDUSER
                            .STATUS = "DELETE"
                            .MEMO = "ORDER RADIOLOGI"
                        End With

                        oOrder.UpdateData(dsOrder)
                    End If
                End If
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        grvCPPT_Tindakan.DeleteSelectedRows()
    End Sub
#End Region
#Region "Grid Method"
    Private Sub grvCPPT_Tindakan_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvCPPT_Tindakan.CellValueChanged
        Dim oItem As New Reference.clsItem

        If e.Column.Name = colCPPT_KDITEMTINDAKAN.Name Then
            If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing Then
                Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN))

                If ds IsNot Nothing Then
                    Try
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.M_UOM.KDKELASRAWAT = sKelas).KDUOM)
                    Catch ex As Exception
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
                    End Try
                End If
            End If
        ElseIf e.Column.Name = colCPPT_KDUOMTINDAKAN.Name Then
            Try
                If grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN) IsNot Nothing And grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN) IsNot Nothing Then
                    Dim ds = oItem.GetDataDetail_UOM(grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN), grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDUOMTINDAKAN))

                    If ds IsNot Nothing Then
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_JUMLAHTINDAKAN, 1)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_HARGATINDAKAN, ds.PRICESALESSTANDARD)
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_TOTALTINDAKAN, ds.PRICESALESSTANDARD)
                    Else
                        MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

                        Dim sItem = grvCPPT_Tindakan.GetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN)
                        grvCPPT_Tindakan.CancelUpdateCurrentRow()

                        grvCPPT_Tindakan.AddNewRow()
                        grvCPPT_Tindakan.SetFocusedRowCellValue(colCPPT_KDITEMTINDAKAN, sItem)
                    End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Command Button"
    Private Sub frmReqAwalPemeriksaan_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                btnClose_Click()
            'Case Keys.F2
            '    If btnSaveNew.Enabled = True Then
            '        btnSaveNew_Click()
            '    End If
            Case Keys.F3
                If btnSaveClose.Enabled = True Then
                    btnSaveClose_Click()
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
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
    Private Sub chkALERGI_YA_CheckedChanged(sender As Object, e As EventArgs) Handles chkALERGI_YA.CheckedChanged
        If isLoad = True Then
            'If chkALERGI_YA.Checked = False Then
            '    chkALERGI_TIDAK.Checked = True
            'End If
        End If
    End Sub
    Private Sub btnHitung_Click(sender As Object, e As EventArgs) Handles btnHitung.Click
        Try
            Dim berat As Double = CDec(txtOBJEKTIF_BERATBADAN.Text)
            Dim tinggi As Double = CDec(txtOBJEKTIF_TINGGIBADAN.Text) / 100
            Dim imt As Double = berat / (tinggi * tinggi)

            txtIMT_TAMBAH2.Text = FormatNumber(imt, 2)

            txtBBIDEAL_TAMBAH3.Text = FormatNumber((berat / imt) * 18.5, 2) & " - " & FormatNumber((berat / imt) * 25, 2)

        Catch oErr As Exception
            MsgBox("Hitung : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadKDPROFESI()
        Dim oPROFESI As New Reference.clsProfesi
        Try
            grdCPPT_Profesi.Properties.DataSource = oPROFESI.GetData.Where(Function(x) x.ISACTIVE = True And Not x.MEMO.Contains("DOKTER")).ToList()
            grdCPPT_Profesi.Properties.ValueMember = "KDPROFESI"
            grdCPPT_Profesi.Properties.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDUOM()
        Dim oUOM As New Reference.clsUOM
        Try
            Dim dsList = oUOM.GetData()

            grdCPPT_KDUOMTINDAKAN.DataSource = dsList.ToList()
            grdCPPT_KDUOMTINDAKAN.ValueMember = "KDUOM"
            grdCPPT_KDUOMTINDAKAN.DisplayMember = "MEMO"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadKDITEMSearch()
        Try
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
            SQL &= "A.KDITEM "
            'SQL &= ",NMITEM2 = (SELECT CASE D.MEMO WHEN 'NON KELAS' THEN A.NMITEM2 ELSE A.NMITEM2 + ' ' + D.MEMO END) "
            SQL &= ",A.NMITEM2 "
            SQL &= ",KELOMPOK = B.MEMO "
            'SQL &= ",PRICE = C.PRICESALESSTANDARD "
            SQL &= "FROM "
            SQL &= "M_ITEM A "
            SQL &= "INNER JOIN M_ITEM_L2 B "
            SQL &= "ON A.KDITEM_L2 = B.KDITEM_L2 "
            'SQL &= "INNER JOIN M_ITEM_UOM C "
            'SQL &= "ON A.KDITEM = C.KDITEM "
            'SQL &= "INNER JOIN M_UOM D "
            'SQL &= "ON C.KDUOM = D.KDUOM "
            SQL &= "WHERE A.ISSTOK = 0 "
            SQL &= "AND A.ISACTIVE = 1 "
            SQL &= "AND B.MEMO NOT IN ('LABORATORIUM', 'BANK DARAH', 'RADIOLOGI', 'PENUNJANG')  "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ITEM_ALL")

            grdCPPT_KDITEMTINDAKAN.DataSource = ds.Tables("ITEM_ALL")
            grdCPPT_KDITEMTINDAKAN.ValueMember = "KDITEM"
            grdCPPT_KDITEMTINDAKAN.DisplayMember = "NMITEM2"

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_LoadTindakLanjutAlasanCek(ByVal KDREG As String, ByVal ALASAN As String) As String
        fn_LoadTindakLanjutAlasanCek = ""

        Try
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
            SQL &= "FROM S_PENDAFTARAN_SKD WHERE KDPENDAFTARAN = '" & KDREG & "' AND ALASAN = '" & ALASAN & "'"

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "S_PENDAFTARAN_SKD")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            For iLoop As Integer = 0 To ds.Tables("S_PENDAFTARAN_SKD").Rows.Count - 1
                With ds.Tables("S_PENDAFTARAN_SKD")
                    fn_LoadTindakLanjutAlasanCek = .Rows(iLoop)("KDSKD").ToString()
                    sRemarks_Ruangan = .Rows(iLoop)("REQUEST").ToString()
                    sRemarks_RencanaPembedahan = .Rows(iLoop)("RESPONSE").ToString()
                    sRemarks_IntruksiDokter = .Rows(iLoop)("DESCRIPTION").ToString()
                End With
            Next

        Catch ex As Exception
            MsgBox("Tindak Lanjut : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub btnSKD_Click(sender As Object, e As EventArgs) Handles btnSKD.Click
        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "KONTROL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, sKDDEPARTMENT, sKDDOCTOR, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONTROL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub btnEksternal_Click(sender As Object, e As EventArgs) Handles btnEksternal.Click
        Dim oRujukan As New Admission.clsRujukan

        Dim ds = oRujukan.GetDataByNoRegister(sKDPENDAFTARAN)

        If ds IsNot Nothing Then
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 0)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDRUJUKAN)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        Else
            Dim frmRujukan As New frmRujukan
            Try
                frmRujukan.LoadMeRegister(sKDPENDAFTARAN, 0)
                frmRujukan.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmRujukan.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmRujukan Is Nothing Then frmRujukan.Dispose()
                frmRujukan = Nothing
            End Try
        End If
    End Sub
    Private Sub btnInternal_Click(sender As Object, e As EventArgs) Handles btnInternal.Click
        Dim Kode As String = fn_LoadTindakLanjutAlasanCek(sKDPENDAFTARAN, "KONSUL INTERNAL")
        If Kode <> "" Then
            Dim frmSKD As New frmSKD
            Try
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, Kode)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSKD Is Nothing Then frmSKD.Dispose()
                frmSKD = Nothing
            End Try
        Else
            Dim frmSKD As New frmSKD
            Try
                frmSKD.fn_LoadNoPendaftaranPolidanDokter(0, sKDDEPARTMENT, sKDDOCTOR, txtKDCUSTOMER.Text, sKDPENDAFTARAN, "KONSUL INTERNAL")
                frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
                frmSKD.ShowDialog(Me)
            Catch ex As Exception
                MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub btnPenjadwalanOperasi_Click(sender As Object, e As EventArgs) Handles btnPenjadwalanOperasi.Click
        Dim oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI

        Dim ds = oSET_BOOKING_JADWALOPERASI.GetDataByKD(sKDKUNJUNGAN)

        If ds Is Nothing Then
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDKUNJUNGAN)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox("Jadwal Operasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        Else
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDKUNJUNGAN, ds.KDBOOKINGJADWALOPERASI)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
            Catch oErr As Exception
                MsgBox("Jadwal Operasi" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub btnSelesaiPengobatan_Click(sender As Object, e As EventArgs) Handles btnSelesaiPengobatan.Click
        If MsgBox("Apakah Selesai Pengobatan?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataTindakLanjut(sKDKUNJUNGAN, "SELESAI PENGOBATAN")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "SELESAI PENGOBATAN", 0)
        Else
            fn_SaveSKD(True, "", "SELESAI PENGOBATAN", 0)
        End If
    End Sub
    Private Sub btnKembaliKeDokter_Click(sender As Object, e As EventArgs) Handles btnKembaliKeDokter.Click
        If MsgBox("Apakah Akan dilakukan Penunjang hari Ini?", MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim oSKD As New Admission.clsSKD
        Dim ds = oSKD.GetDataTindakLanjut(sKDKUNJUNGAN, "PENUNJANG HARI INI")

        If ds IsNot Nothing Then
            fn_SaveSKD(False, ds.KDSKD, "PENUNJANG HARI INI", 0)
        Else
            fn_SaveSKD(True, "", "PENUNJANG HARI INI", 0)
        End If

    End Sub
    Private Function fn_SaveSKD(ByVal isAdd As Boolean, ByVal NOIID As String, ByVal ALASAN As String, ByVal sISCATEGORY As Integer) As Boolean
        Try
            ' ***** HEADER *****
            Dim oSKD As New Admission.clsSKD

            Dim dsDaftar = oSKD.GetDataPendaftaranByKoderegistrasi(sKDPENDAFTARAN)

            Dim ds = oSKD.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oSKD.GetData(NOIID).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try

                .DATEUPDATED = Now
                .KDSKD = NOIID
                .KDPENDAFTARAN = sKDPENDAFTARAN
                .DATE = deDATE.DateTime
                .ISCATEGORY = sISCATEGORY
                .KDDEPARTMENT = sKDDEPARTMENT
                .KDDOCTOR = sKDDOCTOR
                .NOMORRUJUKAN = dsDaftar.NOMORRUJUKAN
                .DESCRIPTION = ""
                Try
                    .ISCHEKED = oSKD.GetData(NOIID).ISCHEKED
                Catch oErr As Exception
                    .ISCHEKED = False
                End Try
                .KDUSER = sUserID
                .DATEKONTROL = deDATE.DateTime
                .ALASAN = ALASAN
                .TINDAKLANJUT = ALASAN
                .TANGGALPERIKSA_TEXT = deDATE.DateTime.ToString("ddMMyyyy")
                .KDJADWALDOKTER = ""
                .SEQ = 0
                .REQUEST = ""
                .RESPONSE = ""
                .NOMORSEP = dsDaftar.NOMORSEP
                .ISONLINE = False
                .ISSKD = IIf(sISCATEGORY = 0, False, True)
            End With

            If isAdd = True Then
                Dim KDSKD As String = oSKD.InsertData(ds, NOIID)

                If KDSKD <> "" Then
                    fn_SaveSKD = True
                Else
                    fn_SaveSKD = False
                End If
            Else
                fn_SaveSKD = oSKD.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox("Save SKD" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveSKD = False
        End Try
    End Function
    Private Sub btnOrderLaboratorium_Click(sender As Object, e As EventArgs) Handles btnOrderLaboratorium.Click
        Dim frmOrderRanapLab As New frmOrderRanapLab

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            'For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
            '    listdiagnosa.Add(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
            'Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTITAS, String.Join(", ", listdiagnosa.ToArray), txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
            frmOrderRanapLab.ShowDialog(Me)

            LoadPenunjang(sKDPENDAFTARAN)

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
            frmOrderRanapLab = Nothing
        End Try
    End Sub
    Private Sub btnOrderRadiologi_Click(sender As Object, e As EventArgs) Handles btnOrderRadiologi.Click
        Dim frmOrderRanapRad As New frmOrderRanapRad

        Try
            If txtOBJEKTIF_BERATBADAN.Text = "" Then
                MsgBox("Dibutuhkan Berat Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            If txtOBJEKTIF_TINGGIBADAN.Text = "" Then
                MsgBox("Dibutuhkan Tinggi Badan", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            Dim listdiagnosa As New List(Of String)

            'For i As Integer = 0 To grvDiagnosaPenyerta.RowCount - 2
            '    listdiagnosa.Add(grvDiagnosaPenyerta.GetRowCellValue(i, colKETERANGAN))
            'Next

            'If listdiagnosa.Count <= 0 Then
            '    MsgBox("Dibutuhkan Diagnosa", MsgBoxStyle.Exclamation, Me.Text)
            '    Exit Sub
            'End If

            frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTITAS, String.Join(", ", listdiagnosa.ToArray), txtOBJEKTIF_BERATBADAN.Text, txtOBJEKTIF_TINGGIBADAN.Text)
            frmOrderRanapRad.ShowDialog(Me)

            LoadPenunjang(sKDPENDAFTARAN)

        Catch ex As Exception
            MsgBox("Load Form Detail : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
            frmOrderRanapRad = Nothing
        End Try
    End Sub
    Private Sub EditOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Keterangan") <> "TAMBAH" Then
            MsgBox("Status" & vbCrLf & grvPenunjang.GetFocusedRowCellValue("Keterangan"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            Dim frmOrderRanapLab As New frmOrderRanapLab
            Try
                frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapLab.ShowDialog(Me)
                LoadPenunjang(sKDPENDAFTARAN)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                frmOrderRanapLab = Nothing
            End Try
        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            Dim frmOrderRanapRad As New frmOrderRanapRad
            Try

                frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapRad.ShowDialog(Me)
                LoadPenunjang(sKDPENDAFTARAN)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                frmOrderRanapRad = Nothing
            End Try
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

    End Sub
    Private Sub ViewOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ViewOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            Dim frmOrderRanapLab As New frmOrderRanapLab
            Try
                frmOrderRanapLab.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapLab.ShowDialog(Me)
                LoadPenunjang(sKDPENDAFTARAN)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapLab Is Nothing Then frmOrderRanapLab.Dispose()
                frmOrderRanapLab = Nothing
            End Try
        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            Dim frmOrderRanapRad As New frmOrderRanapRad
            Try
                frmOrderRanapRad.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTITAS, "", "", "", grvPenunjang.GetFocusedRowCellValue("KODE"))
                frmOrderRanapRad.ShowDialog(Me)
                LoadPenunjang(sKDPENDAFTARAN)
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmOrderRanapRad Is Nothing Then frmOrderRanapRad.Dispose()
                frmOrderRanapRad = Nothing
            End Try
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If

    End Sub
    Private Sub BatalOrderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BatalOrderToolStripMenuItem.Click
        If grvPenunjang.GetFocusedRowCellValue("KODE") Is Nothing Then
            Exit Sub
        End If
        If grvPenunjang.GetFocusedRowCellValue("Keterangan") <> "TAMBAH" Then
            MsgBox("Status" & vbCrLf & grvPenunjang.GetFocusedRowCellValue("Keterangan"), MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER LABORATORIUM" Then
            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            Dim oOrder As New Order.clsOrderRanapLab

            If oOrder.DeleteData(grvPenunjang.GetFocusedRowCellValue("KODE")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            LoadPenunjang(sKDPENDAFTARAN)

        ElseIf grvPenunjang.GetFocusedRowCellValue("Penunjang") = "ORDER RADIOLOGI"
            If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
            Dim oOrder As New Order.clsOrderRanapRad

            If oOrder.DeleteData(grvPenunjang.GetFocusedRowCellValue("KODE")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            LoadPenunjang(sKDPENDAFTARAN)
        Else
            MsgBox("Order Tidak di Temukan", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub LoadPenunjang(ByVal kdreg As String)
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String
            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)

            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPLAB C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER LABORATORIUM' "

            SQL &= "UNION "

            SQL &= "SELECT "
            SQL &= "KODE = B.KDORDER  "
            SQL &= ",TanggalOrder = B.TANGGALORDER "
            SQL &= ",Penunjang = B.MEMO "
            SQL &= ",JenisPemeriksaan = C.NAMATINDAKAN "
            SQL &= ",Dokter = B.USERORDER "
            SQL &= ",Keterangan = B.STATUS "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_ORDER B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_ORDER_RANAPRAD C "
            SQL &= "ON B.KDORDER = C.KDORDER "
            SQL &= "WHERE "
            SQL &= "A.KDPENDAFTARAN = '" & kdreg & "' "
            SQL &= "AND B.MEMO = 'ORDER RADIOLOGI' "

            'SQL &= "ORDER BY B.TANGGALORDER "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ORDERPENUNJANG")

            grdPenunjang.DataSource = ds.Tables("ORDERPENUNJANG")
            grdPenunjang.ForceInitialize()


            Dim listPlanningTindakanPenunjang As New List(Of String)

            For iLoop As Integer = 0 To ds.Tables("ORDERPENUNJANG").Rows.Count - 1
                With ds.Tables("ORDERPENUNJANG")
                    listPlanningTindakanPenunjang.Add("(" & CDate(.Rows(iLoop)("TanggalOrder").ToString()).ToString("dd-MM-yyyy") & ") " & .Rows(iLoop)("JenisPemeriksaan").ToString())
                End With
            Next

            'sPenunjnag = String.Join(vbCrLf, listPlanningTindakanPenunjang.ToArray)

            For iLoop As Integer = 0 To grvPenunjang.Columns.Count - 1
                If grvPenunjang.Columns(iLoop).ColumnType.Name = "Decimal" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                    grvPenunjang.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
                ElseIf grvPenunjang.Columns(iLoop).ColumnType.Name = "DateTime" Then
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                    grvPenunjang.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm}"
                End If
            Next

            grvPenunjang.Columns("KODE").VisibleIndex = -1

            grvPenunjang.BestFitColumns()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Penunjang: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class