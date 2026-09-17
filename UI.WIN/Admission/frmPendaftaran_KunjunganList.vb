Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaran_KunjunganList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran_Kunjungan As New Admission.clsPendaftaran_Kunjungan
    Private sKodeRegister As String = String.Empty

#Region "Function"
    Public Sub fn_LoadRegister(ByVal Parameter As String)
        sKodeRegister = Parameter
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Pendaftaran_Kunjungan.TITLE

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()

        'Try
        '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        'Catch ex As Exception

        'End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "PENDAFTARAN_KUNJUNGAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                If sKodeRegister <> "" Then
                    deDATEFrom.Properties.ReadOnly = True
                    deDATETo.Properties.ReadOnly = True
                End If

                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
                    'Try
                    '    grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    'Catch ex As Exception

                    'End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picUpdate.Enabled = False
                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = Pendaftaran_Kunjungan.TITLE

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDKUNJUNGAN").Caption = Pendaftaran_Kunjungan.KDKUNJUNGAN
            grv.Columns("TANGGAL").Caption = Pendaftaran_Kunjungan.TANGGAL
            grv.Columns("TUJUAN").Caption = Pendaftaran_Kunjungan.KDDEPARTMENT
            grv.Columns("DOKTER").Caption = Pendaftaran_Kunjungan.KDDOCTOR
            grv.Columns("KDCUSTOMER").Caption = "Rekam Medis"
            grv.Columns("PASIEN").Caption = Pendaftaran_Kunjungan.KDCUSTOMER
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("STATUSDAFTAR").Caption = Pendaftaran_Kunjungan.STATUSDAFTAR

        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            If sKodeRegister = "" Then
                Dim ds = From x In oPendaftaran_Kunjungan.GetDataByDate(deDATEFrom.DateTime, deDATETo.DateTime)
                         Select x.KDKUNJUNGAN, TANGGAL = x.DATE, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.M_DEPARTMENT.NAME_DISPLAY, x.KDUSER

                grd.DataSource = ds.ToList

                fn_LoadFormatData()
            Else
                Dim ds = From x In oPendaftaran_Kunjungan.GetDataListByKodePendaftaran(sKodeRegister)
                         Select x.KDKUNJUNGAN, TANGGAL = x.DATE, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.M_DEPARTMENT.NAME_DISPLAY, x.KDUSER

                grd.DataSource = ds.ToList

                fn_LoadFormatData()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDPendaftaran_Kunjungan As String) As Boolean
        Try
            Dim dsKD = oPendaftaran_Kunjungan.GetData(sKDPendaftaran_Kunjungan)
            If dsKD IsNot Nothing Then
                Dim dsLast = oPendaftaran_Kunjungan.GetDataLast(dsKD.KDPENDAFTARAN)

                If dsLast IsNot Nothing Then
                    oPendaftaran_Kunjungan.DeleteData(sKDPendaftaran_Kunjungan, dsLast.KDKUNJUNGAN)

                    fn_DeleteData = True

                Else
                    oPendaftaran_Kunjungan.DeleteData(sKDPendaftaran_Kunjungan, "")

                    fn_DeleteData = True

                End If

            End If
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.A
                If e.Alt = True And picAdd.Enabled = True Then
                    picAdd_Click()
                End If
            Case Keys.E
                If e.Alt = True And picUpdate.Enabled = True Then
                    picUpdate_Click()
                End If
            Case Keys.D
                If e.Alt = True And picDelete.Enabled = True Then
                    picDelete_Click()
                End If
            Case Keys.P
                If e.Alt = True And picPrint.Enabled = True Then
                    picPrint_Click()
                End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKodeRegister, grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKodeRegister)
            frmPendaftaran_Kunjungan.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_Kunjungan Is Nothing Then frmPendaftaran_Kunjungan.Dispose()
            frmPendaftaran_Kunjungan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDKUNJUNGAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmPendaftaran_Kunjungan As New frmPendaftaran_Kunjungan
        Try
            frmPendaftaran_Kunjungan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKodeRegister, grv.GetFocusedRowCellValue("KDKUNJUNGAN"))
            frmPendaftaran_Kunjungan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPendaftaran_Kunjungan Is Nothing Then frmPendaftaran_Kunjungan.Dispose()
            frmPendaftaran_Kunjungan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDKUNJUNGAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDKUNJUNGAN") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDKUNJUNGAN")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDKUNJUNGAN") = String.Empty Then Exit Sub

            Dim ds = oPendaftaran_Kunjungan.GetData(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

            Dim listTranskasi As New List(Of R_KARTU_STATUS)

            Dim dsRekap As New DataAccess.R_KARTU_STATUS

            With ds
                dsRekap.KDCUSTOMER = ds.S_PENDAFTARAN_H.KDCUSTOMER
                dsRekap.NAMAUNIT = ds.M_DEPARTMENT.NAME_DISPLAY
                dsRekap.TANGGAL_MASUK = ds.DATE
                dsRekap.PASIEN = ds.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                dsRekap.PASIEN = ds.M_PANGKAT.MEMO
                dsRekap.KDCUSTOMER = ds.S_PENDAFTARAN_H.M_CUSTOMER.NRP

                '[Pangkat] [nvarchar](50) Not NULL,
                '[NRP] [nvarchar](50) Not NULL,
                '[NAMAKELUARGA] [nvarchar](max) Not NULL,
                '[TEMPAT] [nvarchar](50) Not NULL,
                '[TANGGAL_LAHIR] [smalldatetime] Not NULL,
                '[UMUR] [nvarchar](50) Not NULL,
                '[KELAMIN] [nvarchar](50) Not NULL,
                '[DARAH] [nvarchar](50) Not NULL,
                '[STATUS] [nvarchar](50) Not NULL,
                '[Agama] [nvarchar](50) Not NULL,
                '[Pekerjaan] [nvarchar](50) Not NULL,
                '[Pendidikan] [nvarchar](50) Not NULL,
                '[ALAMAT] [nvarchar](max) Not NULL,
                '[Kesatuan] [nvarchar](50) Not NULL,
                '[TELEPON] [nvarchar](50) Not NULL,
                '[KELOMPOKPASIEN] [nvarchar](50) Not NULL,
                '[PEMEGANGASURANSI] [nvarchar](50) Not NULL,
                '[NOASURANSI] [nvarchar](50) Not NULL,
                '[Rujukan] [nvarchar](50) Not NULL,
                '[ALAMAT_PERUBAHAN] [nvarchar](max) Not NULL,

                listTranskasi.Add(dsRekap)
            End With

            Dim rpt As New xtraKartuStatus
            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
#End Region
End Class