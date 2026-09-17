Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmGerdQ
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private sNoId As String
    Private isLoad As Boolean = False
    Private oGerdQ As New Digital.clsGerdQ
    Private sAutoClose As Boolean = False
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As Integer, ByVal KDREG As String, ByVal KDCUSTOMER As String, ByVal NAMAPASIEN As String, ByVal KDDOCTOR As String, ByVal NoId As String, ByVal AutoClose As Boolean)
        oFormMode = FormMode
        sNoId = NoId
        txtKDKUNJUNGAN.Text = KDREG
        txtRM.Text = KDCUSTOMER
        txtNAMAPASIEN.Text = NAMAPASIEN
        grdKDDOCTOR.Text = KDDOCTOR
        sAutoClose = AutoClose
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
        isLoad = True
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "FORMULIR GERD - Q"

            'btnSaveNew.Caption = Caption.FormSaveNew
            'btnSaveClose.Caption = Caption.FormSaveClose
            'btnClose.Caption = Caption.FormClose
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'sCode = txtCODE.Text.Trim.ToUpper
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDoctor()

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

        deDATE.Properties.ReadOnly = Status
        txtKDKUNJUNGAN.Properties.ReadOnly = True
        txtMEMO.Properties.ReadOnly = Status
        txtHASIL.Properties.ReadOnly = Status
        grdKDDOCTOR.Properties.ReadOnly = Status

        grvDetail.OptionsBehavior.ReadOnly = Status
    End Sub
    Private Sub fn_EmptyMe()
        txtCODE.Text = "<--- AUTO --->"
        deDATE.DateTime = Now
        txtMEMO.ResetText()
        txtHASIL.Text = "0"

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami perasaan terbakar di bagian belakang tulang dada Anda (heartburn)?")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 0)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 3)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami naiknya isi lambung ke arah tenggorokan/mulut Anda (regurgitasi)?")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 0)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 3)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami nyeri ulu hati?")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 3)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 0)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda mengalami mual?")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 3)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 0)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberpa sering Anda mengalami kesulitan tidur malam oleh karena rasa terbakar di dada (heartburn) dan/atau naiknya isi perut ?")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 0)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 3)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()

        grvDetail.Focus()
        grvDetail.AddNewRow()
        grvDetail.SetFocusedRowCellValue(colPERTANYAAN, "Seberapa sering Anda meminum obat tambahan untuk rasa terbakar di dada (heartburn) dan/atau naiknya isi perut (regurgitasi), selain yang diberikan oleh dokter Anda ? (seperti obat maag yang dijual bebas)")
        grvDetail.SetFocusedRowCellValue(colCHEK_01_INT, 0)
        grvDetail.SetFocusedRowCellValue(colCHEK_02_INT, 1)
        grvDetail.SetFocusedRowCellValue(colCHEK_03_INT, 2)
        grvDetail.SetFocusedRowCellValue(colCHEK_04_INT, 3)
        grvDetail.SetFocusedRowCellValue(colREMARKS, "")
        grvDetail.UpdateCurrentRow()
    End Sub
    Private Sub fn_LoadData()
        Try
            ' ***** HEADER *****
            Dim ds = oGerdQ.GetData(sNoId)

            With ds
                txtCODE.Text = .KDGERDQ
                deDATE.DateTime = .DATE
                txtKDKUNJUNGAN.Text = .KDPENDAFTARAN
                txtRM.Text = .KDCUSTOMER
                txtMEMO.Text = .MEMO
                txtHASIL.Text = .HASIL
                grdKDDOCTOR.Text = .KDDOCTOR

                bindingSource.DataSource = oGerdQ.GetDataDetail(sNoId).OrderBy(Function(x) x.SEQ).ToList()
                grdDetail.DataSource = bindingSource
            End With
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

            If txtRM.Text = String.Empty Then
                MsgBox("Nomor Rekam Medis Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            If txtKDKUNJUNGAN.Text = String.Empty Then
                MsgBox("Kode Kunjungan Kosong", MsgBoxStyle.Exclamation, Me.Text)

                fn_Validate = False
                Exit Function
            End If

            If grdKDDOCTOR.Text = String.Empty Then
                MsgBox("Nama Dokter Kosong", MsgBoxStyle.Exclamation, Me.Text)

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
            Dim ds = oGerdQ.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oGerdQ.GetData(sNoId).DATECREATED
                Catch oErr As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .DATE = deDATE.DateTime
                .KDGERDQ = sNoId
                .KDPENDAFTARAN = txtKDKUNJUNGAN.Text
                .KDCUSTOMER = txtRM.Text
                .HASIL = txtHASIL.Text
                .MEMO = txtMEMO.Text.Trim.ToUpper
                .KDUSER = sUserID
                .KDDOCTOR = grdKDDOCTOR.EditValue
                .DOKTER = grdKDDOCTOR.Text
                Try
                    .ISDELETE = oGerdQ.GetData(sNoId).ISDELETE
                Catch oErr As Exception
                    .ISDELETE = False
                End Try
                Try
                    .DATEDELETE = oGerdQ.GetData(sNoId).DATEDELETE
                Catch oErr As Exception
                    .DATEDELETE = Now
                End Try
                Try
                    .USERDELETE = oGerdQ.GetData(sNoId).USERDELETE
                Catch oErr As Exception
                    .USERDELETE = ""
                End Try
            End With

            ' ***** DETIL *****
            Dim arrDetail = oGerdQ.GetStructureDetailList
            For i As Integer = 0 To grvDetail.RowCount - 2
                Dim dsDetail = oGerdQ.GetStructureDetail
                With dsDetail
                    .KDGERDQ = ds.KDGERDQ
                    .SEQ = i
                    .PERTANYAAN = grvDetail.GetRowCellValue(i, colPERTANYAAN)
                    .CHEK_01_INT = grvDetail.GetRowCellValue(i, colCHEK_01_INT)
                    .CHEK_01 = grvDetail.GetRowCellValue(i, colCHEK_01)
                    .CHEK_02_INT = grvDetail.GetRowCellValue(i, colCHEK_02_INT)
                    .CHEK_02 = grvDetail.GetRowCellValue(i, colCHEK_02)
                    .CHEK_03_INT = grvDetail.GetRowCellValue(i, colCHEK_03_INT)
                    .CHEK_03 = grvDetail.GetRowCellValue(i, colCHEK_03)
                    .CHEK_04_INT = grvDetail.GetRowCellValue(i, colCHEK_04_INT)
                    .CHEK_04 = grvDetail.GetRowCellValue(i, colCHEK_04)
                    .REMARKS = grvDetail.GetRowCellValue(i, colREMARKS)
                End With

                arrDetail.Add(dsDetail)
            Next

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Try
                    sNoId = oGerdQ.InsertData(ds, arrDetail)
                    If sNoId = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                Try
                    fn_Save = oGerdQ.UpdateData(ds, arrDetail)
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
    Private Sub OnValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grvDetail.FocusedRowChanged
        If isLoad Then
            Calculate()
        End If
    End Sub
    Private Sub Calculate()
        Dim sSubTotal = 0
        Dim sSubTotal_ = 0

        For i As Integer = 0 To grvDetail.RowCount - 2
            If grvDetail.GetRowCellValue(i, colCHEK_01) = True Then
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colCHEK_01_INT))
            End If
            If grvDetail.GetRowCellValue(i, colCHEK_02) = True Then
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colCHEK_02_INT))
            End If
            If grvDetail.GetRowCellValue(i, colCHEK_03) = True Then
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colCHEK_03_INT))
            End If
            If grvDetail.GetRowCellValue(i, colCHEK_04) = True Then
                sSubTotal += CDec(grvDetail.GetRowCellValue(i, colCHEK_04_INT))
            End If
        Next

        'For i As Integer = 0 To grvDetail.RowCount - 2
        '    If grvDetail.GetRowCellValue(i, colCHEK_01) = False Then
        '        sSubTotal_ += CDec(grvDetail.GetRowCellValue(i, colCHEK_01_INT))
        '    End If
        '    If grvDetail.GetRowCellValue(i, colCHEK_02) = False Then
        '        sSubTotal_ += CDec(grvDetail.GetRowCellValue(i, colCHEK_02_INT))
        '    End If
        '    If grvDetail.GetRowCellValue(i, colCHEK_03) = False Then
        '        sSubTotal_ += CDec(grvDetail.GetRowCellValue(i, colCHEK_03_INT))
        '    End If
        '    If grvDetail.GetRowCellValue(i, colCHEK_04) = False Then
        '        sSubTotal_ += CDec(grvDetail.GetRowCellValue(i, colCHEK_04_INT))
        '    End If
        'Next

        txtHASIL.Text = sSubTotal
    End Sub
    Private Sub grvDetail_CellValueChanged(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles grvDetail.CellValueChanged
        If e.Column.Name = colCHEK_01.Name Then
            Try
                If grvDetail.GetFocusedRowCellValue(colCHEK_01) IsNot Nothing Then
                    'Dim cek As Integer = 0

                    'For i As Integer = 0 To grvDetail.RowCount - 2
                    '    If grvDetail.GetRowCellValue(i, colKDITEM) = grvDetail.GetFocusedRowCellValue(colKDITEM) Then
                    '        cek = cek + 1
                    '    End If
                    'Next

                    'If cek <> 0 Then
                    '    MsgBox("Item sudah ada di list!", MsgBoxStyle.Critical, Me.Text)
                    '    grvDetail.CancelUpdateCurrentRow()
                    'End If
                End If
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If oFormMode = FORM_MODE.FORM_MODE_VIEW Then Exit Sub
        grvDetail.DeleteSelectedRows()
    End Sub
#End Region
#Region "Command Button"
    'Private Sub frmGerdQ_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    Select Case e.KeyCode
    '        Case Keys.F12
    '            btnClose_Click()
    '        Case Keys.F2
    '            If btnSaveNew.Enabled = True Then
    '                btnSaveNew_Click()
    '            End If
    '        Case Keys.F3
    '            If btnSaveClose.Enabled = True Then
    '                btnSaveClose_Click()
    '            End If
    '    End Select
    'End Sub
    'Private Sub btnSaveNew_Click() Handles btnSaveNew.ItemClick
    '    If fn_Validate() = False Then Exit Sub
    '    If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
    '    If fn_Save() = False Then
    '        MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
    '    Else
    '        MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
    '        Me.Close()
    '    End If
    'End Sub
    Private Sub btnSaveClose_Click() Handles btnSaveClose.ItemClick
        If fn_Validate() = False Then Exit Sub
        If MsgBox(Statement.SaveQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_Save() = False Then
            MsgBox(Statement.SaveFail, MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sAutoClose = True Then
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
                Me.Close()
            Else
                oFormMode = FORM_MODE.FORM_MODE_EDIT
                MsgBox(Statement.SaveSuccess, MsgBoxStyle.Information, Me.Text)
            End If
        End If
    End Sub
    Private Sub btnClose_Click() Handles btnClose.ItemClick
        Me.Close()
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub fn_LoadDoctor()
        Dim oDPJP As New Reference.clsDoctor
        Try
            grdKDDOCTOR.Properties.DataSource = oDPJP.GetData.Where(Function(x) x.ISACTIVE = True).ToList()
            grdKDDOCTOR.Properties.ValueMember = "KDDOCTOR"
            grdKDDOCTOR.Properties.DisplayMember = "NAME_DISPLAY"

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class