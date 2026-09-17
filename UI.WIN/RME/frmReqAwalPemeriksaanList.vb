Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmReqAwalPemeriksaanList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oGrouperDataCppt As New Grouper.clsR_CPPT
    Private sNoRM As String = String.Empty
    Private sKDIDENTITAS As Integer = 0

#Region "Function"
    Public Sub fn_LoadData(ByVal RM As String, ByVal KDIDENTITAS As Integer)
        sNoRM = RM
        sKDIDENTITAS = KDIDENTITAS
    End Sub
    Private Sub Me_Load() Handles Me.Load
        Me.Text = "CPPT - List"

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
                      Where x.MODUL = "MEDREK_RJ" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()
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
            Me.Text = "CPPT"

            grv.Columns("KDCPPT").Caption = "NOMOR"
            grv.Columns("MEMO").Caption = "PROFESI"
            'grv.Columns("ISDEFAULT").Caption = Item_L1.ISDEFAULT

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oGrouperDataCppt.GetDataListNakes(sNoRM)
                     Where x.ISDELETE = False
                     Select TANGGAL = x.DATE, x.KDCPPT, x.R_CPPT_M_PROFESI.MEMO, x.KDUSER

            grd.DataSource = ds.ToList

            fn_LoadFormatData()
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

        'grv.Columns("KDCPPT").Visible = False
        'grv.Columns("ISDEFAULT").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        'grv.Columns("KDCPPT").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDITEM_L1 As String) As Boolean
        Try
            oGrouperDataCppt.DeleteData(sKDITEM_L1, sUserID)
            fn_DeleteData = True
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
            'Case Keys.P
            '    If e.Alt = True And picPrint.Enabled = True Then
            '        picPrint_Click()
            '    End If
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
        Try
            frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_VIEW, sKDIDENTITAS, grv.GetFocusedRowCellValue("KDCPPT"))
            frmReqAwalPemeriksaan.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        If sKDIDENTITAS = 0 Then
            MsgBox("Identitas Kosong", MsgBoxStyle.Exclamation, Me.Text)
        Else
            Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
            Try
                frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_ADD, sKDIDENTITAS)
                frmReqAwalPemeriksaan.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
                frmReqAwalPemeriksaan = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("User Tidak Sesuai, tidak bisa edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmReqAwalPemeriksaan As New frmReqAwalPemeriksaan
        Try
            frmReqAwalPemeriksaan.LoadMe(FORM_MODE.FORM_MODE_EDIT, sKDIDENTITAS, grv.GetFocusedRowCellValue("KDCPPT"))
            frmReqAwalPemeriksaan.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmReqAwalPemeriksaan Is Nothing Then frmReqAwalPemeriksaan.Dispose()
            frmReqAwalPemeriksaan = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("NAME_DISPLAY"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim sKDCPPT As String = grv.GetFocusedRowCellValue("KDCPPT")
        If grv.GetFocusedRowCellValue("KDUSER") <> sUserID Then
            MsgBox("User Tidak Sesuai, tidak bisa edit", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDCPPT")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oOrder As New Digital.clsR_Order
        Dim dsNomorRefrenceLab = oOrder.GetDataNoReference(sKDCPPT, "ORDER LABORATORIUM")
        If dsNomorRefrenceLab IsNot Nothing Then
            oOrder.UpdateStatus(dsNomorRefrenceLab.KDORDER, "DELETE")
        End If

        Dim dsNomorRefrenceRad = oOrder.GetDataNoReference(sKDCPPT, "ORDER RADIOLOGI")
        If dsNomorRefrenceRad IsNot Nothing Then
            oOrder.UpdateStatus(dsNomorRefrenceRad.KDORDER, "DELETE")
        End If

        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    'Private Sub picPrint_Click() Handles picPrint.Click
    '    Try
    '        Dim xtraItem_L1 As New xtraItem_L1

    '        Dim sKDITEM_L1 As New List(Of String)
    '        For i As Integer = 0 To grv.RowCount - 1
    '            sKDITEM_L1.Add(grv.GetRowCellValue(i, "KDCPPT"))
    '        Next

    '        Dim ds = oItem_L1.GetData.Where(Function(x) sKDITEM_L1.Contains(x.KDITEM_L1)).ToList

    '        xtraItem_L1.bindingSource.DataSource = ds
    '        Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraItem_L1)
    '        printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
    '    Catch oErr As Exception
    '        MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
    '    End Try
    'End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub PemberiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_HandOver_Pemberi As New EMedrek.clsCppt_HandOver_Pemberi

        Dim ds = oCppt_HandOver_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTHandOverPemberi As New frmCPPTHandOverPemberi
            Try
                frmCPPTHandOverPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTHandOverPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPemberi Is Nothing Then frmCPPTHandOverPemberi.Dispose()
                frmCPPTHandOverPemberi = Nothing

            End Try
        Else
            Dim frmCPPTHandOverPemberi As New frmCPPTHandOverPemberi
            Try
                frmCPPTHandOverPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTHandOverPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPemberi Is Nothing Then frmCPPTHandOverPemberi.Dispose()
                frmCPPTHandOverPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("HANDOVERPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Hand Over", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_HandOver_Penerima As New EMedrek.clsCppt_HandOver_Penerima

        Dim ds = oCppt_HandOver_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTHandOverPenerima As New frmCPPTHandOverPenerima
            Try
                frmCPPTHandOverPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTHandOverPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPenerima Is Nothing Then frmCPPTHandOverPenerima.Dispose()
                frmCPPTHandOverPenerima = Nothing

            End Try
        Else
            Dim frmCPPTHandOverPenerima As New frmCPPTHandOverPenerima
            Try
                frmCPPTHandOverPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTHandOverPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTHandOverPenerima Is Nothing Then frmCPPTHandOverPenerima.Dispose()
                frmCPPTHandOverPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub PemberiToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem1.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_NilaiKritis_Pemberi As New EMedrek.clsCppt_NilaiKritis_Pemberi

        Dim ds = oCppt_NilaiKritis_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTNilaiKritisPemberi As New frmCPPTNilaiKritisPemberi
            Try
                frmCPPTNilaiKritisPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTNilaiKritisPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPemberi Is Nothing Then frmCPPTNilaiKritisPemberi.Dispose()
                frmCPPTNilaiKritisPemberi = Nothing

            End Try
        Else
            Dim frmCPPTNilaiKritisPemberi As New frmCPPTNilaiKritisPemberi
            Try
                frmCPPTNilaiKritisPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTNilaiKritisPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPemberi Is Nothing Then frmCPPTNilaiKritisPemberi.Dispose()
                frmCPPTNilaiKritisPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem1.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("NILAIKRITISPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Nilai Kritis", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_NilaiKritis_Penerima As New EMedrek.clsCppt_NilaiKritis_Penerima

        Dim ds = oCppt_NilaiKritis_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTNilaiKritisPenerima As New frmCPPTNilaiKritisPenerima
            Try
                frmCPPTNilaiKritisPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTNilaiKritisPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPenerima Is Nothing Then frmCPPTNilaiKritisPenerima.Dispose()
                frmCPPTNilaiKritisPenerima = Nothing

            End Try
        Else
            Dim frmCPPTNilaiKritisPenerima As New frmCPPTNilaiKritisPenerima
            Try
                frmCPPTNilaiKritisPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTNilaiKritisPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTNilaiKritisPenerima Is Nothing Then frmCPPTNilaiKritisPenerima.Dispose()
                frmCPPTNilaiKritisPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub PemberiToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PemberiToolStripMenuItem2.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_SBAR_Pemberi As New EMedrek.clsCppt_SBAR_Pemberi

        Dim ds = oCppt_SBAR_Pemberi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTSBARPemberi As New frmCPPTSBARPemberi
            Try
                frmCPPTSBARPemberi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTSBARPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPemberi Is Nothing Then frmCPPTSBARPemberi.Dispose()
                frmCPPTSBARPemberi = Nothing

            End Try
        Else
            Dim frmCPPTSBARPemberi As New frmCPPTSBARPemberi
            Try
                frmCPPTSBARPemberi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTSBARPemberi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPemberi Is Nothing Then frmCPPTSBARPemberi.Dispose()
                frmCPPTSBARPemberi = Nothing

            End Try
        End If
    End Sub
    Private Sub PenerimaToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PenerimaToolStripMenuItem2.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        If grv.GetFocusedRowCellValue("SBARPEMBERI") = "" Then
            MsgBox("Belum ada pemberi Nilai Kritis", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim oCppt_SBAR_Penerima As New EMedrek.clsCppt_SBAR_Penerima

        Dim ds = oCppt_SBAR_Penerima.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTSBARPenerima As New frmCPPTSBARPenerima
            Try
                frmCPPTSBARPenerima.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTSBARPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPenerima Is Nothing Then frmCPPTSBARPenerima.Dispose()
                frmCPPTSBARPenerima = Nothing

            End Try
        Else
            Dim frmCPPTSBARPenerima As New frmCPPTSBARPenerima
            Try
                frmCPPTSBARPenerima.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTSBARPenerima.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTSBARPenerima Is Nothing Then frmCPPTSBARPenerima.Dispose()
                frmCPPTSBARPenerima = Nothing

            End Try
        End If
    End Sub
    Private Sub VerifikasiDPJPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles VerifikasiDPJPToolStripMenuItem.Click
        If grv.GetFocusedRowCellValue("KDCPPT") Is Nothing Then
            Exit Sub
        End If

        Dim oCppt_Verifikasi As New EMedrek.clsCppt_Verifikasi

        Dim ds = oCppt_Verifikasi.GetData(grv.GetFocusedRowCellValue("KDCPPT"))
        If ds Is Nothing Then
            Dim frmCPPTVerifikasi As New frmCPPTVerifikasi
            Try
                frmCPPTVerifikasi.LoadMe(FORM_MODE.FORM_MODE_ADD, grv.GetFocusedRowCellValue("KDCPPT"))
                frmCPPTVerifikasi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTVerifikasi Is Nothing Then frmCPPTVerifikasi.Dispose()
                frmCPPTVerifikasi = Nothing

            End Try
        Else
            Dim frmCPPTVerifikasi As New frmCPPTVerifikasi
            Try
                frmCPPTVerifikasi.LoadMe(FORM_MODE.FORM_MODE_EDIT, ds.KDCPPT)
                frmCPPTVerifikasi.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCPPTVerifikasi Is Nothing Then frmCPPTVerifikasi.Dispose()
                frmCPPTVerifikasi = Nothing

            End Try
        End If
    End Sub
#End Region
End Class