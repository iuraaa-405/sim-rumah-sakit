Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient


Public Class frmSET_BOOKING_JADWALOPERASIList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSET_BOOKING_JADWALOPERASI As New WebService.clsSET_BOOKING_JADWALOPERASI

#Region "Function"
    Public Sub fn_LoadRegister(ByVal Register As String)
        txtKDPENDAFTARAN.Text = Register
    End Sub
    Private Sub Me_Load() Handles Me.Load
        Me.Text = SET_BOOKING_JADWALOPERASII.TITLE

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
                      Where x.MODUL = "SET_BOOKING_JADWALOPERASI" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picTerlaksana.Enabled = ds.ISVIEW
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
                picTerlaksana.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguage() Implements ILanguage.fn_LoadLanguage
        Try
            Me.Text = SET_BOOKING_JADWALOPERASII.TITLE

            grv.Columns("KDPENDAFTARAN").Caption = SET_BOOKING_JADWALOPERASII.KDPENDAFTARAN
            grv.Columns("TANGGALOPERASI").Caption = SET_BOOKING_JADWALOPERASII.TANGGALOPERASI
            grv.Columns("JENISTINDAKAN").Caption = SET_BOOKING_JADWALOPERASII.JENISTINDAKAN
            grv.Columns("ISAPROVAL").Caption = SET_BOOKING_JADWALOPERASII.ISAPROVAL
            grv.Columns("REMARKS").Caption = SET_BOOKING_JADWALOPERASII.REMARKS
            grv.Columns("KDUSER").Caption = User.KDUSER

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oSET_BOOKING_JADWALOPERASI.GetData
                     Select x.KDBOOKINGJADWALOPERASI, x.TANGGALOPERASI, x.KDKUNJUNGAN, x.R_IDENTITAS_PASIEN.KDPENDAFTARAN, POLI = x.R_IDENTITAS_PASIEN.TUJUAN, DOKTER = x.R_IDENTITAS_PASIEN.DOKTER, x.JENISTINDAKAN, ISAPROVAL = IIf(x.ISAPROVAL = False, "Belum", "Sudah"), x.REMARKS, x.KDUSER

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

        grv.Columns("KDBOOKINGJADWALOPERASI").Visible = False
        'grv.Columns("ISAPROVAL").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        grv.Columns("KDBOOKINGJADWALOPERASI").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal SET_BOOKING_JADWALOPERASISET_BOOKING_JADWALOPERASI As String) As Boolean
        Try
            oSET_BOOKING_JADWALOPERASI.DeleteData(SET_BOOKING_JADWALOPERASISET_BOOKING_JADWALOPERASI)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If grv.GetRowCellValue(e.RowHandle, "ISAPROVAL") = "Belum" Then
            'e.Appearance.BackColor = Color.Red
        Else
            e.Appearance.BackColor = Color.LightGreen
        End If
    End Sub
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
            Case Keys.T
                If e.Alt = True And picTerlaksana.Enabled = True Then
                    picTerlaksana_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI") Is Nothing Then
            Exit Sub
        End If

        Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
        Try
            frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"), grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI"))
            frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picTerlaksana_Click() Handles picTerlaksana.Click
        If grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI") Is Nothing Then
            Exit Sub
        End If

        If fn_UpdateOperasi(grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI"), IIf(grv.GetFocusedRowCellValue("ISAPROVAL") = "Belum", 1, 0)) = True Then
            oSET_BOOKING_JADWALOPERASI.UpdateIscheked(grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI"))
        Else
            MsgBox("Update Gagal", MsgBoxStyle.Exclamation, Me.Text)
        End If
        fn_LoadData()
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        If txtKDPENDAFTARAN.Text = "" Then
            Dim oBooking As New WebService.clsSET_BOOKING_JADWALOPERASI
            Dim dsBooking = oBooking.GetDataByKD(grv.GetFocusedRowCellValue("KDKUNJUNGAN"))

            If dsBooking IsNot Nothing Then
                If grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI") Is Nothing Then
                    Exit Sub
                End If
                Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
                Try
                    frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_EDIT, dsBooking.KDKUNJUNGAN, dsBooking.KDBOOKINGJADWALOPERASI)
                    frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
                    fn_LoadSecurity()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
                    frmSET_BOOKING_JADWALOPERASI = Nothing

                    Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
                    If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                    If sStatusSave = "NEW" Then
                        sStatusSave = "NONE"
                        picAdd_Click()
                    End If
                End Try
            Else
                If txtKDPENDAFTARAN.Text = "" Then
                    Exit Sub
                End If

                Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
                Try
                    frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text)
                    frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
                    fn_LoadSecurity()
                Catch oErr As Exception
                    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                Finally
                    If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
                    frmSET_BOOKING_JADWALOPERASI = Nothing

                    Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
                    If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                    If sStatusSave = "NEW" Then
                        sStatusSave = "NONE"
                        picAdd_Click()
                    End If
                End Try
            End If
        Else
            Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
            Try
                frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_ADD, txtKDPENDAFTARAN.Text)
                frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
                frmSET_BOOKING_JADWALOPERASI = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        End If
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI") Is Nothing Then
            Exit Sub
        End If
        Dim frmSET_BOOKING_JADWALOPERASI As New frmSET_BOOKING_JADWALOPERASI
        Try
            frmSET_BOOKING_JADWALOPERASI.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDKUNJUNGAN"), grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI"))
            frmSET_BOOKING_JADWALOPERASI.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSET_BOOKING_JADWALOPERASI Is Nothing Then frmSET_BOOKING_JADWALOPERASI.Dispose()
            frmSET_BOOKING_JADWALOPERASI = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteOperasi(grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI")) = True Then
            If fn_DeleteData(grv.GetFocusedRowCellValue("KDBOOKINGJADWALOPERASI")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            End If
            MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
            fn_LoadSecurity()
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        'Try
        '    Dim xtraSET_BOOKING_JADWALOPERASI As New xtraSET_BOOKING_JADWALOPERASI

        '    Dim SET_BOOKING_JADWALOPERASISET_BOOKING_JADWALOPERASI As New List(Of String)
        '    For i As Integer = 0 To grv.RowCount - 1
        '        SET_BOOKING_JADWALOPERASISET_BOOKING_JADWALOPERASI.Add(grv.GetRowCellValue(i, "KDBOOKINGJADWALOPERASI"))
        '    Next

        '    Dim ds = oSET_BOOKING_JADWALOPERASI.GetData.Where(Function(x) SET_BOOKING_JADWALOPERASISET_BOOKING_JADWALOPERASI.Contains(x.KDSET_BOOKING_JADWALOPERASI)).ToList

        '    xtraSET_BOOKING_JADWALOPERASI.bindingSource.DataSource = ds
        '    Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(xtraSET_BOOKING_JADWALOPERASI)
        '    printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Function fn_UpdateOperasi(ByVal KDBOOKING As String, ByVal ISAPROVAL As Integer) As Boolean
        Try
            fn_UpdateOperasi = True

            Dim Koneksi As String = oSET_BOOKING_JADWALOPERASI.GetKoneksiApi()

            If Koneksi <> String.Empty Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Koneksi

                oConn = New SqlConnection(sConn)

                SQL = "UPDATE JADWALOPERASI SET ISAPROVAL = " & ISAPROVAL & " WHERE KDBOOKINGJADWALOPERASI = '" & KDBOOKING & "'  "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "UPDATE_BOOKING")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Else
                fn_UpdateOperasi = False
                MsgBox("Koneksi tidak ada untuk database API", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_UpdateOperasi = False
            MsgBox("Save Database API" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteOperasi(ByVal KDBOOKING As String) As Boolean
        Try
            fn_DeleteOperasi = True

            Dim Koneksi As String = oSET_BOOKING_JADWALOPERASI.GetKoneksiApi()

            If Koneksi <> String.Empty Then
                Dim oConn As New SqlConnection
                Dim oComm As New SqlCommand
                Dim da As SqlDataAdapter
                Dim ds As New DataSet
                Dim SQL As String

                Dim sConn As String = Koneksi

                oConn = New SqlConnection(sConn)

                SQL = "DELETE FROM JADWALOPERASI WHERE KDBOOKINGJADWALOPERASI = '" & KDBOOKING & "'  "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "DELETE_BOOKING")

                If oConn.State = ConnectionState.Open Then
                    oConn.Close()
                End If
            Else
                fn_DeleteOperasi = False
                MsgBox("Koneksi tidak ada untuk database API", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_DeleteOperasi = False
            MsgBox("Save Database API" & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
#End Region
End Class