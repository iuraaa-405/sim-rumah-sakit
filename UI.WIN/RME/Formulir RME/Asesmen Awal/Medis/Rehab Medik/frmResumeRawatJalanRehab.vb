Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmResumeRawatJalanRehab
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oRehabMedik As New EMedrek.clsFisioterafi_3
    Private sKDDOCTOR As String = String.Empty
    Private sDiagnosa As String = String.Empty
    Private sTandaTanganPasien As String = String.Empty
    Private sTanggal As DateTime = Now
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KodeKunjungan As String, ByVal Diagnosa As String, ByVal Kddokter As String, ByVal TandaTanganPasien As String)
        oFormMode = FormMode
        txtKDKUNJUNGAN.Text = KodeKunjungan
        sDiagnosa = Diagnosa
        sTandaTanganPasien = TandaTanganPasien

        Dim dsKunjungan = oRehabMedik.GetDatabykodeKunjungan(txtKDKUNJUNGAN.Text)
        If dsKunjungan IsNot Nothing Then
            txtKDCUSTOMER.Text = dsKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
            txtNAMA.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
            txtTANGGALLAHIR.Text = dsKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("dd-MM-yyyy")
            txtTANGGALDAFTAR.Text = dsKunjungan.DATE.ToString("dd-MM-yyyy")
            sKDDOCTOR = Kddokter
            txtTUJUAN.Text = dsKunjungan.M_DEPARTMENT.NAME_DISPLAY

            sTanggal = dsKunjungan.DATE
        End If
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "LAYANAN KEDOKTERAN FISIK DAN REHABILITASI"

            btnSaveClosee.Caption = "Simpan"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtMEMO.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDPJP()
        fn_LoadUser()

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
        'btnSaveClosee.Enabled = False
        'btnSaveClose.Enabled = Not Status

        'btnSimpanLaporanOperasi.Enabled = Not Status

        'TableLayoutPanel9.Anchor = AnchorStyles.Top
        'TableLayoutPanel9.Anchor = AnchorStyles.Left
        'TableLayoutPanel9.Anchor = AnchorStyles.Right

        'GroupControl9.Anchor = AnchorStyles.Top
        'GroupControl9.Anchor = AnchorStyles.Left
        'GroupControl9.Anchor = AnchorStyles.Right
    End Sub
    Private Sub fn_EmptyMe()
        grdKDDOCTOR.Text = sKDDOCTOR
        deDATE.DateTime = sTanggal
        txtDIAGNOSA.Text = sDiagnosa
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oRehabMedik.GetData(txtKDKUNJUNGAN.Text)
            With ds
                deDATE.DateTime = .DATE
                txtTUJUAN.Text = .TUJUAN
                grdKDDOCTOR.Text = .KDDOCTOR
                txtDIAGNOSA.Text = .DIAGNOSA
                txtPERMINTAANTERAPI.Text = .PERMINTAANTERAPI

                BindingSource.DataSource = oRehabMedik.GetDataDetail(txtKDKUNJUNGAN.Text).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = BindingSource
            End With
        Catch oErr As Exception
            MsgBox("Load Data: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtKDKUNJUNGAN.Text = String.Empty Then
                txtKDKUNJUNGAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtKDKUNJUNGAN.ErrorText = Statement.ErrorRequired

                txtKDKUNJUNGAN.Focus()
                fn_Validate = False
                Exit Function
            End If
            If grdKDDOCTOR.Text = String.Empty Then
                grdKDDOCTOR.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                grdKDDOCTOR.ErrorText = Statement.ErrorRequired

                grdKDDOCTOR.Focus()
                fn_Validate = False
                Exit Function
            End If
            If txtTUJUAN.Text = String.Empty Then
                txtTUJUAN.ErrorIconAlignment = ErrorIconAlignment.MiddleRight
                txtTUJUAN.ErrorText = Statement.ErrorRequired

                txtTUJUAN.Focus()
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

            Dim ds = oRehabMedik.GetStructureHeader
            With ds
                .DATE = deDATE.DateTime
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDCUSTOMER = txtKDCUSTOMER.Text
                .TUJUAN = txtTUJUAN.Text
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .DIAGNOSA = txtDIAGNOSA.Text
                .PERMINTAANTERAPI = txtPERMINTAANTERAPI.Text
                .KDUSER = sUserID
                .ALAMATSIMPANTTDPASIEN = sTandaTanganPasien
                .ISDELETE = False
                .USERDELETE = ""
            End With

            ' ***** DETIL *****
            Dim arrDetail = oRehabMedik.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oRehabMedik.GetStructureDetail
                With dsDetail
                    .SEQ = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colSEQ)), 0, grvDetail.GetRowCellValue(i, colSEQ))
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .PROGRAM = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colPROGRAM)), "", grvDetail.GetRowCellValue(i, colPROGRAM))
                    .TANGGAL = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTANGGAL)), Now, grvDetail.GetRowCellValue(i, colTANGGAL))
                    .ALAMATSIMPANTTDPASIEN = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colALAMATSIMPANTTDPASIEN)), "", grvDetail.GetRowCellValue(i, colALAMATSIMPANTTDPASIEN))
                    .KDDOCTOR = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colKDDOCTOR)), "", grvDetail.GetRowCellValue(i, colKDDOCTOR))
                    .TERAFIS = IIf(String.IsNullOrEmpty(grvDetail.GetRowCellValue(i, colTERAFIS)), "", grvDetail.GetRowCellValue(i, colTERAFIS))
                End With
                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oRehabMedik.InsertData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            Else
                Try
                    fn_Save = oRehabMedik.UpdateData(ds, arrDetail)
                Catch ex As Exception
                    MsgBox("Simpan Data: " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function

#End Region
#Region "Command Button"
    Private Sub btnSaveClosee_Click() Handles btnSaveClosee.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            oFormMode = FORM_MODE.FORM_MODE_EDIT
            MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
        End If
    End Sub
    Private Sub RepositoryItemButtonEdit1_Click(sender As Object, e As EventArgs) Handles RepositoryItemButtonEdit1.Click
        If sTandaTanganPasien <> "" Then
            grvDetail.SetFocusedRowCellValue(colALAMATSIMPANTTDPASIEN, sTandaTanganPasien)
            'grvDetail.SetFocusedRowCellValue(colTANGGAL, Now)
            grvDetail.SetFocusedRowCellValue(colKDDOCTOR, grdKDDOCTOR.EditValue)
            grvDetail.SetFocusedRowCellValue(colTERAFIS, sUserID)
        End If
    End Sub
#End Region
#Region "Grid Method"
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Dim oRehab As New EMedrek.clsFisioterafi_3

        Dim ds = oRehab.GetDataTerakhir(txtKDCUSTOMER.Text)

        If ds IsNot Nothing Then
            txtPERMINTAANTERAPI.Text = ds.PERMINTAANTERAPI

            For Each xloop In oRehab.GetDataDetail(ds.KDKUNJUNGAN)
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colSEQ, xloop.SEQ)
                grvDetail.SetFocusedRowCellValue(colPROGRAM, xloop.PROGRAM)
                grvDetail.SetFocusedRowCellValue(colTANGGAL, xloop.TANGGAL)
                grvDetail.SetFocusedRowCellValue(colALAMATSIMPANTTDPASIEN, xloop.ALAMATSIMPANTTDPASIEN)
                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, xloop.KDDOCTOR)
                grvDetail.SetFocusedRowCellValue(colTERAFIS, xloop.TERAFIS)
                grvDetail.UpdateCurrentRow()
            Next
        Else
            For i As Integer = 1 To 10
                grvDetail.Focus()
                grvDetail.AddNewRow()
                grvDetail.SetFocusedRowCellValue(colSEQ, i)
                grvDetail.SetFocusedRowCellValue(colPROGRAM, "")
                grvDetail.SetFocusedRowCellValue(colTANGGAL, Now)
                grvDetail.SetFocusedRowCellValue(colALAMATSIMPANTTDPASIEN, "")
                grvDetail.SetFocusedRowCellValue(colKDDOCTOR, "")
                grvDetail.SetFocusedRowCellValue(colTERAFIS, "")
                grvDetail.UpdateCurrentRow()
            Next
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colPROGRAM.Name Then
            If grvDetail.GetFocusedRowCellValue(colPROGRAM) IsNot Nothing Then
                'grvDetail.SetFocusedRowCellValue(colTANGGAL, Now)
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDPJP()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDOCTOR_DETIL.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR_DETIL.ValueMember = "KDDOCTOR"
            grdKDDOCTOR_DETIL.DisplayMember = "NAME_DISPLAY"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadUser()
        Dim oUSER As New Setting.clsUser
        Try
            grdKDUSER.DataSource = oUSER.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDUSER.ValueMember = "KDUSER"
            grdKDUSER.DisplayMember = "KDUSER"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form1_MouseWheel(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseWheel
        If e.Delta > 0 Then
            Trace.WriteLine("Scrolled up!")
            fn_ScrollPage(True)
        Else
            Trace.WriteLine("Scrolled down!")
            fn_ScrollPage(False)
        End If
    End Sub
    Private Sub fn_ScrollPage(ByVal isUp As Boolean)
        Dim myView As Point = Me.Panel2.AutoScrollPosition

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

        Me.Panel2.AutoScrollPosition = myView
    End Sub

#End Region
End Class