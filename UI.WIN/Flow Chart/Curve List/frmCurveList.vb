Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient

Public Class frmCurveList
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private isLoad As Boolean = False
    Private oCurveList As New Flowchart.clsCurveList
    Private sKoneksi As String = String.Empty
    Private sNoid As String = String.Empty
    Private skdkunjungan As String = String.Empty
    Private sKode As String = String.Empty
    Private sIsOtority As Boolean = False

#End Region
#Region "Function"
    Public Sub LoadMeKodeKunjungan(ByVal kdkunjungan As String)
        Dim dsIdentitas = oCurveList.GetDataIdentitas(kdkunjungan)
        If dsIdentitas IsNot Nothing Then
            txtNAMA.Text = dsIdentitas.NAMAPASIEN
            txtKDCUSTOMER.Text = dsIdentitas.KDCUSTOMER
            txtTANGGALLAHIR.Text = dsIdentitas.TANGGALLAHIR.ToString("dd-MM-yyyy")
            skdkunjungan = kdkunjungan
        End If
    End Sub
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal NoId As String, Optional Byval kode As String = "")
        oFormMode = FormMode
        sNoid = NoId
        sKode = Kode
        LoadMeKodeKunjungan(NoId)
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim oSetKoneksi As New Setting.clsSetKoneksi
        Dim dsSetKoneksi = oSetKoneksi.GetData()
        If dsSetKoneksi IsNot Nothing Then
            sKoneksi = dsSetKoneksi.KONEKSI
        End If

        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Curve List"

            btnSaveNew.Caption = Caption.FormSaveNew
            btnSaveClose.Caption = Caption.FormSaveClose
            btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = sNoid
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

        txtTINGGIBADAN.Properties.ReadOnly = Status
        txtBERATBADAN.Properties.ReadOnly = Status
        txtDIET.Properties.ReadOnly = Status
        txtALERGI.Properties.ReadOnly = Status

        grv_1.OptionsBehavior.ReadOnly = Status
        grv_2.OptionsBehavior.ReadOnly = Status

        Dim oSetUser As New Setting.clsUser
        Dim dsUser = oSetUser.GetData(sUserID)
        If dsUser IsNot Nothing Then
            If dsUser.ISOTORTY = True Then
                sIsOtority = True
            Else
                sIsOtority = False
            End If
        End If
    End Sub
    Private Sub fn_EmptyMe()
        txtTINGGIBADAN.ResetText()
        txtBERATBADAN.ResetText()
        txtDIET.ResetText()
        txtALERGI.ResetText()
        deDATE.DateTime = Now
        txtKDKUNJUNGAN.Text = skdkunjungan
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = oCurveList.GetData(sKode)

            With ds
                txtKDKUNJUNGAN.Text = .KDKUNJUNGAN
                txtTINGGIBADAN.Text = .TINGGIBADAN
                txtBERATBADAN.Text = .BERATBADAN
                txtDIET.Text = .DIET
                txtALERGI.Text = .ALERGI
                deDATE.DateTime = .DATE

                BindingSource1.DataSource = oCurveList.GetDataDetail_1(ds.KDCURVELIST)
                grd_1.DataSource = BindingSource1

                BindingSource2.DataSource = oCurveList.GetDataDetail_2(ds.KDCURVELIST)
                grd_2.DataSource = BindingSource2
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True
            If txtKDKUNJUNGAN.Text = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            grv_1.UpdateCurrentRow()

            If grv_1.RowCount < 2 Then
                MsgBox("Dibutuhkan Curve List", MsgBoxStyle.Exclamation, Me.Text)
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
            Dim ds = oCurveList.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oCurveList.GetData(txtKDKUNJUNGAN.Text).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDCURVELIST = sKode
                .KDKUNJUNGAN = txtKDKUNJUNGAN.Text
                Try
                    .KDUSER = oCurveList.GetData(txtKDKUNJUNGAN.Text).KDUSER
                Catch oErr As Exception
                    .KDUSER = sUserID
                End Try
                .BERATBADAN = txtBERATBADAN.Text.ToString.Trim
                .TINGGIBADAN = txtTINGGIBADAN.Text.ToString.Trim
                .DIET = txtDIET.Text.ToString.Trim
                .ALERGI = txtALERGI.Text.ToString.Trim
                .DESCRIPTION = ""
                Try
                    .ISACTIVE = oCurveList.GetData(txtKDKUNJUNGAN.Text).ISACTIVE
                Catch oErr As Exception
                    .ISACTIVE = False
                End Try
            End With

            ' ***** DETIL 1 *****
            Dim arrDetail_1 = oCurveList.GetStructureDetailList_1
            For i As Integer = 0 To grv_1.RowCount - 2
                Dim dsDetail = oCurveList.GetStructureDetail_1
                With dsDetail
                    .SEQ = i
                    .KDCURVELIST = ds.KDCURVELIST
                    .KATEGORI = grv_1.GetRowCellValue(i, colKATEGORI)
                    .JAM = grv_1.GetRowCellValue(i, colJAM_1)
                    .JAM_INT = CDate(grv_1.GetRowCellValue(i, colJAM_1)).ToString("HH") + CDate(grv_1.GetRowCellValue(i, colJAM_1)).ToString("mm") / 60
                    .NILAI = grv_1.GetRowCellValue(i, colANGKA)
                    .REMARKS = IIf(String.IsNullOrEmpty(grv_1.GetRowCellValue(i, colREMARKS)), "-", grv_1.GetRowCellValue(i, colREMARKS))
                End With
                arrDetail_1.Add(dsDetail)
            Next

            ' ***** DETIL 2 *****
            Dim arrDetail_2 = oCurveList.GetStructureDetailList_2
            For i As Integer = 0 To grv_2.RowCount - 2
                Dim dsDetail = oCurveList.GetStructureDetail_2
                With dsDetail
                    .SEQ = i
                    .KDCURVELIST = ds.KDCURVELIST
                    .KATEGORI = grv_2.GetRowCellValue(i, colKategori_2)
                    .JAM = grv_2.GetRowCellValue(i, colJAM_2)
                    .JAM_INT = CDate(grv_2.GetRowCellValue(i, colJAM_2)).ToString("HH") + CDate(grv_2.GetRowCellValue(i, colJAM_2)).ToString("mm") / 60
                    .NILAI = grv_2.GetRowCellValue(i, colNILAI)
                    .REMARKS = IIf(String.IsNullOrEmpty(grv_2.GetRowCellValue(i, colCATATAN)), "-", grv_2.GetRowCellValue(i, colCATATAN))
                End With
                arrDetail_2.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    fn_Save = oCurveList.InsertData(ds, arrDetail_1, arrDetail_2)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oCurveList.UpdateData(ds, arrDetail_1, arrDetail_2)
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_1.DeleteSelectedRows()
    End Sub
    Private Sub ToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToolStripMenuItem1.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grv_2.DeleteSelectedRows()
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
        End Select
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
#End Region
#Region "Grid Method"
    Private Sub grv_1_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_1.CellValueChanged
        If e.Column.Name = colKATEGORI.Name Then
            Try
                If grv_1.GetFocusedRowCellValue(colKATEGORI) IsNot Nothing Then
                    grv_1.SetFocusedRowCellValue(colJAM_1, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub grv_2_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grv_2.CellValueChanged
        If e.Column.Name = colKategori_2.Name Then
            Try
                If grv_2.GetFocusedRowCellValue(colKategori_2) IsNot Nothing Then
                    grv_2.SetFocusedRowCellValue(colJAM_2, Now)
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
#End Region
#Region "Lookup / Event"

#End Region
End Class