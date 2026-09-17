Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmGeneralICU
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private sIdentitas As String
    Private oGeneralICU As New EMedrek.clsGeneralICU
    Private isLoad As Boolean = False
#End Region
#Region "Function"
    Public Sub fn_loadDataIdentitas(ByVal Parameter As String)
        sIdentitas = Parameter
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoId = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "General Intensive Care Unit"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Dispose()
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.Collect()
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

        txtTIMRS.Properties.ReadOnly = Status
        deTGLMASUKICU.Properties.ReadOnly = Status
        deTGLOBSERVASI.Properties.ReadOnly = Status
        txtHARIKE.Properties.ReadOnly = Status
        txtKAMAR.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtKIRIMANDARI.Properties.ReadOnly = Status
        txtDIAGNOSA.Properties.ReadOnly = Status
        txtAPACHE.Properties.ReadOnly = Status
        txtSOFASCORE.Properties.ReadOnly = Status

        grvTandaVital.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        'txtKDADJUSTMENT.Text = "<--- AUTO --->"
        txtTIMRS.ResetText()
        deTGLMASUKICU.DateTime = Now
        deTGLOBSERVASI.DateTime = Now
        txtHARIKE.ResetText()
        txtKAMAR.ResetText()
        txtBERATBADAN.ResetText()
        txtTINGGIBADAN.ResetText()
        txtKIRIMANDARI.ResetText()
        txtDIAGNOSA.ResetText()
        txtAPACHE.ResetText()
        txtSOFASCORE.ResetText()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oGeneralICU.GetData(sNoId)

            With ds
                'txtKDADJUSTMENT.Text = .KDADJUSTMENT
                txtTIMRS.Text = .TIMRS
                deTGLMASUKICU.DateTime = .TGLMASUKICU
                deTGLOBSERVASI.DateTime = .TGLOBSERVASI
                txtHARIKE.Text = .HARIKE
                txtKAMAR.Text = .KAMAR
                txtBERATBADAN.Text = .BERATBADAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtKIRIMANDARI.Text = .KIRIMANDARI
                txtDIAGNOSA.Text = .DIAGNOSA
                txtAPACHE.Text = .APACHESCORE
                txtSOFASCORE.Text = .SOFASCORE

                BindingSourceTandaVital.DataSource = oGeneralICU.GetDataDetailTandaVital(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTandaVital.DataSource = BindingSourceTandaVital

                BindingSourceTandaVital2.DataSource = oGeneralICU.GetDataDetailTandaVital2(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTandaVital2.DataSource = BindingSourceTandaVital2

                BindingSourceHemodiamik.DataSource = oGeneralICU.GetDataDetailHemodiamik(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdHemodiamik.DataSource = BindingSourceHemodiamik

                BindingSourceTandaVital3.DataSource = oGeneralICU.GetDataDetailTandaVital3(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTandaVital3.DataSource = BindingSourceTandaVital3

                BindingSourceTandaVital4.DataSource = oGeneralICU.GetDataDetailTandaVital4(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdTandaVital4.DataSource = BindingSourceTandaVital4
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If sIdentitas = String.Empty Then
                txtTIMRS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTIMRS.ErrorText = Statement.ErrorRequired
                MsgBox("Identitas masih kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtTIMRS.Focus()
                fn_Validate = False
                Exit Function
            End If

            If txtTIMRS.Text = String.Empty Then
                txtTIMRS.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTIMRS.ErrorText = Statement.ErrorRequired
                MsgBox("TIMRS masih kosong", MsgBoxStyle.Exclamation, Me.Text)
                txtTIMRS.Focus()
                fn_Validate = False
                Exit Function
            End If

            grvTandaVital.UpdateCurrentRow()

            If grvTandaVital.RowCount < 2 Then
                MsgBox("Tanda Vital masih kosong", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oGeneralICU.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oGeneralICU.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KDIDENTITAS = sIdentitas
                .KDGENERALICU = sNoId
                .TIMRS = txtTIMRS.Text
                .TGLMASUKICU = deTGLMASUKICU.DateTime
                .TGLOBSERVASI = deTGLOBSERVASI.DateTime
                .HARIKE = txtHARIKE.Text
                .KAMAR = txtKAMAR.Text
                .BERATBADAN = txtBERATBADAN.Text
                .TINGGIBADAN = txtTINGGIBADAN.Text
                .KIRIMANDARI = txtKIRIMANDARI.Text
                .DIAGNOSA = txtDIAGNOSA.Text
                .APACHESCORE = txtAPACHE.Text
                .SOFASCORE = txtSOFASCORE.Text
                .KDUSER = sUserID
            End With

            ' ***** DETIL 1 *****
            Dim arrDetailTandaVital = oGeneralICU.GetStructureDetailTandaVitalList
            For i As Integer = 0 To grvTandaVital.RowCount - 2
                Dim dsDetail = oGeneralICU.GetStructureDetailTandaVital
                With dsDetail
                    .SEQ = i
                    .KDGENERALICU = ds.KDGENERALICU
                    .TANGGAL = CDate(grvTandaVital.GetRowCellValue(i, colTANGGAL))
                    .JAM = CDate(grvTandaVital.GetRowCellValue(i, colTANGGAL)).ToString("HH:mm")
                    .HR = CInt(grvTandaVital.GetRowCellValue(i, colHR))
                    .T = CInt(grvTandaVital.GetRowCellValue(i, colT))
                    .RR = CInt(grvTandaVital.GetRowCellValue(i, colRR))
                    .NIBP = CInt(grvTandaVital.GetRowCellValue(i, colNIBP))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvTandaVital.GetRowCellValue(i, colREMARKS)), "", grvTandaVital.GetRowCellValue(i, colREMARKS))
                End With
                arrDetailTandaVital.Add(dsDetail)
            Next

            ' ***** DETIL 2 *****
            Dim arrDetailTandaVital2 = oGeneralICU.GetStructureDetaiTandaVital2List
            For i As Integer = 0 To grvTandaVital2.RowCount - 2
                Dim dsDetail = oGeneralICU.GetStructureDetailTandaVital2
                With dsDetail
                    .SEQ = i
                    .KDGENERALICU = ds.KDGENERALICU
                    .NOMOR01 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR01)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR01))
                    .NOMOR02 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR02)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR02))
                    .NOMOR03 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR03)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR03))
                    .NOMOR04 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR04)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR04))
                    .NOMOR05 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR05)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR05))
                    .NOMOR06 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR06)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR06))
                    .NOMOR07 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR07)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR07))
                    .NOMOR08 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR08)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR08))
                    .NOMOR09 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR09)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR09))
                    .NOMOR10 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR10)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR10))
                    .NOMOR11 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR11)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR11))
                    .NOMOR12 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR12)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR12))
                    .NOMOR13 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR13)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR13))
                    .NOMOR14 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR14)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR14))
                    .NOMOR15 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR15)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR15))
                    .NOMOR16 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR16)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR16))
                    .NOMOR17 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR17)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR17))
                    .NOMOR18 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR18)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR18))
                    .NOMOR19 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR19)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR19))
                    .NOMOR20 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR20)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR20))
                    .NOMOR21 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR21)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR21))
                    .NOMOR22 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR22)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR22))
                    .NOMOR23 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR23)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR23))
                    .NOMOR24 = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colNOMOR24)), "", grvTandaVital2.GetRowCellValue(i, colNOMOR24))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvTandaVital2.GetRowCellValue(i, colREMARKS2)), "", grvTandaVital2.GetRowCellValue(i, colREMARKS2))
                End With
                arrDetailTandaVital2.Add(dsDetail)
            Next

            ' ***** DETIL 3 *****
            Dim arrDetailHemodiamik = oGeneralICU.GetStructureDetailHemodiamikList
            For i As Integer = 0 To grvHemodiamik.RowCount - 2
                Dim dsDetail = oGeneralICU.GetStructureDetailHemodiamik
                With dsDetail
                    .SEQ = i
                    .KDGENERALICU = ds.KDGENERALICU
                    .TANGGAL = CDate(grvHemodiamik.GetRowCellValue(i, colTANGGAL2))
                    .JAM = CDate(grvHemodiamik.GetRowCellValue(i, colTANGGAL2)).ToString("HH:mm")
                    .IBP = CInt(grvHemodiamik.GetRowCellValue(i, colIBP))
                    .CO = CInt(grvHemodiamik.GetRowCellValue(i, colCO))
                    .CVP = CInt(grvHemodiamik.GetRowCellValue(i, colCVP))
                    .PAP = CInt(grvHemodiamik.GetRowCellValue(i, colPAP))
                    .REMARKS = IIf(String.IsNullOrEmpty(grvHemodiamik.GetRowCellValue(i, colREMARKS3)), "", grvHemodiamik.GetRowCellValue(i, colREMARKS3))
                End With
                arrDetailHemodiamik.Add(dsDetail)
            Next


            ' ***** DETIL 4 *****
            Dim arrDetailTandaVital3 = oGeneralICU.GetStructureDetaiTandaVital3List
            For i As Integer = 0 To grvTandaVital3.RowCount - 2
                Dim dsDetail = oGeneralICU.GetStructureDetailTandaVital3
                With dsDetail
                    .SEQ = i
                    .KDGENERALICU = ds.KDGENERALICU
                    .NOMOR01 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR01_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR01_))
                    .NOMOR02 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR02_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR02_))
                    .NOMOR03 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR03_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR03_))
                    .NOMOR04 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR04_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR04_))
                    .NOMOR05 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR05_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR05_))
                    .NOMOR06 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR06_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR06_))
                    .NOMOR07 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR07_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR07_))
                    .NOMOR08 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR08_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR08_))
                    .NOMOR09 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR09_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR09_))
                    .NOMOR10 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR10_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR10_))
                    .NOMOR11 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR11_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR11_))
                    .NOMOR12 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR12_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR12_))
                    .NOMOR13 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR13_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR13_))
                    .NOMOR14 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR14_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR14_))
                    .NOMOR15 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR15_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR15_))
                    .NOMOR16 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR16_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR16_))
                    .NOMOR17 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR17_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR17_))
                    .NOMOR18 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR18_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR18_))
                    .NOMOR19 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR19_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR19_))
                    .NOMOR20 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR20_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR20_))
                    .NOMOR21 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR21_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR21_))
                    .NOMOR22 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR22_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR22_))
                    .NOMOR23 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR23_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR23_))
                    .NOMOR24 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colNOMOR24_)), "", grvTandaVital3.GetRowCellValue(i, colNOMOR24_))
                    .REMARKS1 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colREMARKS1_)), "", grvTandaVital3.GetRowCellValue(i, colREMARKS1_))
                    .REMARKS2 = IIf(String.IsNullOrEmpty(grvTandaVital3.GetRowCellValue(i, colREMARKS2_)), "", grvTandaVital3.GetRowCellValue(i, colREMARKS2_))
                    .REMARKS3 = ""
                End With
                arrDetailTandaVital3.Add(dsDetail)
            Next

            ' ***** DETIL 5 *****
            Dim arrDetailTandaVital4 = oGeneralICU.GetStructureDetaiTandaVital4List
            For i As Integer = 0 To grvTandaVital4.RowCount - 2
                Dim dsDetail = oGeneralICU.GetStructureDetailTandaVital4
                With dsDetail
                    .SEQ = i
                    .KDGENERALICU = ds.KDGENERALICU
                    .NOMOR01 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR01X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR01X))
                    .NOMOR02 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR02X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR02X))
                    .NOMOR03 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR03X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR03X))
                    .NOMOR04 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR04X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR04X))
                    .NOMOR05 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR05X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR05X))
                    .NOMOR06 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR06X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR06X))
                    .NOMOR07 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR07X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR07X))
                    .NOMOR08 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR08X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR08X))
                    .NOMOR09 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR09X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR09X))
                    .NOMOR10 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR10X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR10X))
                    .NOMOR11 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR11X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR11X))
                    .NOMOR12 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR12X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR12X))
                    .NOMOR13 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR13X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR13X))
                    .NOMOR14 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR14X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR14X))
                    .NOMOR15 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR15X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR15X))
                    .NOMOR16 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR16X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR16X))
                    .NOMOR17 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR17X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR17X))
                    .NOMOR18 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR18X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR18X))
                    .NOMOR19 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR19X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR19X))
                    .NOMOR20 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR20X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR20X))
                    .NOMOR21 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR21X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR21X))
                    .NOMOR22 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR22X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR22X))
                    .NOMOR23 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR23X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR23X))
                    .NOMOR24 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colNOMOR24X)), "", grvTandaVital4.GetRowCellValue(i, colNOMOR24X))
                    .REMARKS1 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colREMARKS1X)), "", grvTandaVital4.GetRowCellValue(i, colREMARKS1X))
                    .REMARKS2 = IIf(String.IsNullOrEmpty(grvTandaVital4.GetRowCellValue(i, colREMARKS2X)), "", grvTandaVital4.GetRowCellValue(i, colREMARKS2X))
                    .REMARKS3 = ""
                End With
                arrDetailTandaVital4.Add(dsDetail)
            Next


            'If oFormMode = FORM_MODE.FORM_MODE_ADD Then
            '    Try
            '        fn_Save = oGeneralICU.InsertData(ds, arrDetailTandaVital, arrDetailTandaVital2, arrDetailHemodiamik, arrDetailTandaVital3, arrDetailTandaVital4)
            '    Catch oErr As Exception
            '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
            '    Try
            '        fn_Save = oGeneralICU.UpdateData(ds, arrDetailTandaVital, arrDetailTandaVital2, arrDetailHemodiamik, arrDetailTandaVital3, arrDetailTandaVital4)
            '    Catch oErr As Exception
            '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            '    End Try
            'End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Grid Method"
    'Private Sub grvTandaVital_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvTandaVital.CellValueChanged
    'If e.Column.Name = colKDITEM.Name Then
    '    Dim oItem As New Reference.clsItem
    '    Try
    '        If grvDetail.GetFocusedRowCellValue(colKDITEM) IsNot Nothing Then
    '            Dim ds = oItem.GetDataDetail_UOM(grvDetail.GetFocusedRowCellValue(colKDITEM))

    '            If ds IsNot Nothing Then
    '                grvDetail.SetFocusedRowCellValue(colKDUOM, ds.FirstOrDefault(Function(x) x.RATE = 1).KDUOM)
    '            Else
    '                MsgBox(Statement.ErrorUOM, MsgBoxStyle.Exclamation, Me.Text)

    '                grvDetail.CancelUpdateCurrentRow()
    '            End If
    '        End If
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End If
    'End Sub
    'Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
    '    If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
    '    grvDetail.DeleteSelectedRows()
    'End Sub
#End Region
#Region "Command Button"
    Private Sub frmAdjustment_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        grvTandaVital.OptionsSelection.MultiSelect = True
        grvTandaVital.SelectAll()
        grvTandaVital.DeleteSelectedRows()
        grvTandaVital.OptionsSelection.MultiSelect = False

        grvTandaVital2.OptionsSelection.MultiSelect = True
        grvTandaVital2.SelectAll()
        grvTandaVital2.DeleteSelectedRows()
        grvTandaVital2.OptionsSelection.MultiSelect = False

        grvTandaVital3.OptionsSelection.MultiSelect = True
        grvTandaVital3.SelectAll()
        grvTandaVital3.DeleteSelectedRows()
        grvTandaVital3.OptionsSelection.MultiSelect = False

        grvTandaVital4.OptionsSelection.MultiSelect = True
        grvTandaVital4.SelectAll()
        grvTandaVital4.DeleteSelectedRows()
        grvTandaVital4.OptionsSelection.MultiSelect = False

        grvTandaVital5.OptionsSelection.MultiSelect = True
        grvTandaVital5.SelectAll()
        grvTandaVital5.DeleteSelectedRows()
        grvTandaVital5.OptionsSelection.MultiSelect = False

        grvTandaVital6.OptionsSelection.MultiSelect = True
        grvTandaVital6.SelectAll()
        grvTandaVital6.DeleteSelectedRows()
        grvTandaVital6.OptionsSelection.MultiSelect = False

        grvHemodiamik.OptionsSelection.MultiSelect = True
        grvHemodiamik.SelectAll()
        grvHemodiamik.DeleteSelectedRows()
        grvHemodiamik.OptionsSelection.MultiSelect = False

        Dim waktu As DateTime = DateTime.Today.AddHours(7) '00:00

        For i As Integer = 0 To 287 '24 jam / 5 menit
            grvTandaVital.Focus()
            grvTandaVital.AddNewRow()
            grvTandaVital.SetFocusedRowCellValue(colTANGGAL, waktu)
            grvTandaVital.SetFocusedRowCellValue(colHR, 0)
            grvTandaVital.SetFocusedRowCellValue(colT, 0)
            grvTandaVital.SetFocusedRowCellValue(colRR, 0)
            grvTandaVital.SetFocusedRowCellValue(colNIBP, 0)
            grvTandaVital.UpdateCurrentRow()

            waktu = waktu.AddMinutes(5)
        Next

        grvTandaVital2.Focus()
        grvTandaVital2.AddNewRow()
        grvTandaVital2.SetFocusedRowCellValue(colREMARKS2, "GAMBARAN EKG")
        grvTandaVital2.UpdateCurrentRow()

        grvTandaVital2.Focus()
        grvTandaVital2.AddNewRow()
        grvTandaVital2.SetFocusedRowCellValue(colREMARKS2, "VES")
        grvTandaVital2.UpdateCurrentRow()

        grvTandaVital2.Focus()
        grvTandaVital2.AddNewRow()
        grvTandaVital2.SetFocusedRowCellValue(colREMARKS2, "SATURASI O2")
        grvTandaVital2.UpdateCurrentRow()

        grvTandaVital2.Focus()
        grvTandaVital2.AddNewRow()
        grvTandaVital2.SetFocusedRowCellValue(colREMARKS2, "EtCO2")
        grvTandaVital2.UpdateCurrentRow()


        Dim waktu2 As DateTime = DateTime.Today.AddHours(7) '00:00

        For i As Integer = 0 To 287 '24 jam / 5 menit
            grvHemodiamik.Focus()
            grvHemodiamik.AddNewRow()
            grvHemodiamik.SetFocusedRowCellValue(colTANGGAL2, waktu2)
            grvHemodiamik.SetFocusedRowCellValue(colIBP, 0)
            grvHemodiamik.SetFocusedRowCellValue(colCO, 0)
            grvHemodiamik.SetFocusedRowCellValue(colCVP, 0)
            grvHemodiamik.SetFocusedRowCellValue(colPAP, 0)
            grvHemodiamik.UpdateCurrentRow()

            waktu2 = waktu2.AddMinutes(5)
        Next

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "GLASGOW COMA SCALE")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "E")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "M")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "V")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "Ukuran Pupil")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "Kanan")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "Kiri")
        grvTandaVital3.UpdateCurrentRow()


        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "SKALA NYERI")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "-CPOT, NRS")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "-CS, FS")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "NILAI")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "RESIKO JATUH")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital3.Focus()
        grvTandaVital3.AddNewRow()
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS1_, "BRADEN SCALE")
        grvTandaVital3.SetFocusedRowCellValue(colREMARKS2_, "")
        grvTandaVital3.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "TIPE")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "V")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "RR     : SETTING / AKTUAL")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "E")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "I      : E RATIO")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "N")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "TV     : SETTING / AKTUAL")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "T")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "MV     : SETTING / AKTUAL")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "I")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "P. CONTROL / P. SUPP")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "L")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "PEEP")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "A")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "F102")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "S")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "PEAK / PLATEU PRESSURE")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "I")
        grvTandaVital4.UpdateCurrentRow()

        grvTandaVital4.Focus()
        grvTandaVital4.AddNewRow()
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS1X, "ETT/TC : DIAMETER/KEDALAMAN")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS2X, "")
        grvTandaVital4.SetFocusedRowCellValue(colREMARKS3X, "")
        grvTandaVital4.UpdateCurrentRow()

        'grvTandaVital5.Focus()
        'grvTandaVital5.AddNewRow()
        'grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        'grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "T")
        'grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR01C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR02C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR03C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR04C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR05C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR06C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR07C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR08C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR09C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR10C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR11C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR12C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR13C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR14C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR15C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR16C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR17C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR18C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR19C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR20C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR21C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR22C, "CM")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR23C, "CA")
        'grvTandaVital5.SetFocusedRowCellValue(colNOMOR24C, "CM")
        'grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "R")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "I")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "II")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "N")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "III")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "F")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "IV")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "U")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "S")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "JUMLAH 1 JAM / KUMULATIF")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "C")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "I")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "M")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "ORAL")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "I")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "ENTERNAL")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "R")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "K")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "JUMLAH 1 JAM / KUMULATIF")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "N")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "N")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        '-------------------------------------------------------------------------------------------------
        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "M")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "S")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "U")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "K")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "P")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "R")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "E")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()


        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "N")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "T")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "A")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "L")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "JUMLAH 1 JAM / KUMULTAIF")
        grvTandaVital5.UpdateCurrentRow()

        grvTandaVital5.Focus()
        grvTandaVital5.AddNewRow()
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS2C, "")
        grvTandaVital5.SetFocusedRowCellValue(colREMARKS3C, "KUMULATIF")
        grvTandaVital5.UpdateCurrentRow()

        '------------------------------------------------------------------------
        grvTandaVital6.Focus()
        grvTandaVital6.AddNewRow()
        grvTandaVital6.SetFocusedRowCellValue(colREMARKS1C, "")
        grvTandaVital6.SetFocusedRowCellValue(colREMARKS2C, "URINE")
        grvTandaVital6.SetFocusedRowCellValue(colREMARKS3C, "")
        grvTandaVital6.UpdateCurrentRow()

    End Sub
#End Region
#Region "Lookup / Event"
    'Private Sub fn_LoadKDWAREHOUSE()
    '    Dim oWAREHOUSE As New Reference.clsWarehouse
    '    Try
    '        grdKDWAREHOUSE.Properties.DataSource = oWAREHOUSE.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
    '        grdKDWAREHOUSE.Properties.ValueMember = "KDWAREHOUSE"
    '        grdKDWAREHOUSE.Properties.DisplayMember = "NAME_DISPLAY"
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
#End Region
End Class