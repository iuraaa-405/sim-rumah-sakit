Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports Newtonsoft.Json.Linq

Public Class frmPendaftaranList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oPendaftaran As New Admission.clsPendaftaran

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Pendaftaran.TITLE

        deDATEFrom.DateTime = Now
        deDATETo.DateTime = Now

        fn_LoadSecurity()

        Try
            grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
        Catch ex As Exception

        End Try
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                      On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "PENDAFTARAN" _
                      And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picAdd.Enabled = ds.ISADD
                picDelete.Enabled = ds.ISDELETE
                picBatal.Enabled = ds.ISDELETE
                picUpdate.Enabled = ds.ISUPDATE
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_LoadData()
                    fn_LoadLanguage()

                    Try
                        grv.RestoreLayoutFromRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text)
                    Catch ex As Exception

                    End Try
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picAdd.Enabled = False
                picDelete.Enabled = False
                picBatal.Enabled = False
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
            Me.Text = Pendaftaran.TITLE

            fn_LoadLanguageMaster()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("TANGGAL").Caption = Pendaftaran.TANGGAL
            grv.Columns("TUJUAN").Caption = Pendaftaran.KDDEPARTMENT
            grv.Columns("DOKTER").Caption = Pendaftaran.KDDOCTOR
            grv.Columns("KDCUSTOMER").Caption = "Rekam Medis"
            grv.Columns("PASIEN").Caption = Pendaftaran.KDCUSTOMER
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("STATUSDAFTAR").Caption = Pendaftaran.STATUSDAFTAR

            grv.Columns("DAFTAR_L1").Caption = sDaftar_L1
            grv.Columns("DAFTAR_L2").Caption = sDaftar_L2
            grv.Columns("DAFTAR_L3").Caption = sDaftar_L3
            grv.Columns("DAFTAR_L4").Caption = sDaftar_L4
            grv.Columns("DAFTAR_L5").Caption = sDaftar_L5
            grv.Columns("DAFTAR_L6").Caption = sDaftar_L6

        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oPendaftaran.GetData
        '             Where x.DATE.ToString("yyyyMMdd") >= deDATEFrom.DateTime.ToString("yyyyMMdd") And x.DATE.ToString("yyyyMMdd") <= deDATETo.DateTime.ToString("yyyyMMdd")
        '             Select x.KDPENDAFTARAN, TANGGAL = x.DATE, DAFTAR_L1 = x.M_DAFTAR_L1.MEMO, DAFTAR_L2 = x.M_DAFTAR_L2.MEMO, DAFTAR_L3 = x.M_DAFTAR_L3.MEMO, DAFTAR_L4 = x.M_DAFTAR_L4.MEMO, DAFTAR_L5 = x.M_DAFTAR_L5.MEMO, DAFTAR_L6 = x.M_DAFTAR_L6.MEMO, x.KDCUSTOMER, PASIEN = x.M_CUSTOMER.NAME_DISPLAY, x.KDUSER, STATUSDAFTAR = IIf(x.STATUSDAFTAR = 0, "DAFTAR", "BATAL")
        '    grd.DataSource = ds.ToList

        '    fn_LoadFormatData()
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing

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
            SQL &= "A.KDPENDAFTARAN "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",DAFTAR_L1 = E.MEMO "
            SQL &= ",DAFTAR_L2 = F.MEMO "
            SQL &= ",DAFTAR_L3 = G.MEMO "
            SQL &= ",DAFTAR_L4 = H.MEMO "
            SQL &= ",DAFTAR_L5 = I.MEMO "
            SQL &= ",DAFTAR_L6 = J.MEMO "
            SQL &= ",A.KDCUSTOMER "
            SQL &= ",PASIEN = D.NAME_DISPLAY "
            SQL &= ",TUJUAN = B.NAME_DISPLAY  "
            SQL &= ",DOKTER = C.NAME_DISPLAY "
            SQL &= ",NoSEP = A.NOMORSEP "
            SQL &= ",A.KDUSER "
            SQL &= ",STATUSDAFTAR = (SELECT CASE A.STATUSDAFTAR WHEN 0 THEN 'DAFTAR' WHEN 1 THEN 'BATAL' ELSE '' END) "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_H A "
            SQL &= "INNER JOIN M_DEPARTMENT B "
            SQL &= "ON A.KDDEPARTMENT = B.KDDEPARTMENT "
            SQL &= "INNER JOIN M_DOCTOR C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_CUSTOMER D "
            SQL &= "ON A.KDCUSTOMER = D.KDCUSTOMER "
            SQL &= "INNER JOIN M_DAFTAR_L1 E "
            SQL &= "ON A.KDDAFTAR_L1 = E.KDDAFTAR_L1 "
            SQL &= "INNER JOIN M_DAFTAR_L2 F "
            SQL &= "ON A.KDDAFTAR_L2 = F.KDDAFTAR_L2 "
            SQL &= "INNER JOIN M_DAFTAR_L3 G "
            SQL &= "ON A.KDDAFTAR_L3 = G.KDDAFTAR_L3 "
            SQL &= "INNER JOIN M_DAFTAR_L4 H "
            SQL &= "ON A.KDDAFTAR_L4 = H.KDDAFTAR_L4 "
            SQL &= "INNER JOIN M_DAFTAR_L5 I "
            SQL &= "ON A.KDDAFTAR_L5 = I.KDDAFTAR_L5 "
            SQL &= "INNER JOIN M_DAFTAR_L6 J "
            SQL &= "ON A.KDDAFTAR_L6 = J.KDDAFTAR_L6 "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.DATE DESC "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            grd.MainView = grv
            grd.DataSource = ds.Tables("ALL")
            grd.ForceInitialize()

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

    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDPENDAFTARAN As String) As Boolean
        Try
            Dim dsKunjungan = oPendaftaran.GetDataKunjunganByPendaftaran(sKDPENDAFTARAN)
            Dim sNoIdKunjungan As String

            If dsKunjungan IsNot Nothing Then
                sNoIdKunjungan = dsKunjungan.KDKUNJUNGAN
            Else
                sNoIdKunjungan = ""
            End If

            Dim dsSKD = oPendaftaran.GetDataKunjunganBySKD(sKDPENDAFTARAN)
            Dim sNoIdSKD As String

            If dsSKD IsNot Nothing Then
                sNoIdSKD = dsSKD.KDSKD
            Else
                sNoIdSKD = ""
            End If

            oPendaftaran.DeleteData(sKDPENDAFTARAN, sNoIdKunjungan, sNoIdSKD, sKDPENDAFTARAN)

            Dim oDelete As New Setting.clsDelete

            If oDelete.InsertData("PENDAFTARAN", sUserID, sKDPENDAFTARAN & " " & sNoIdKunjungan & " " & sNoIdSKD & " " & grv.GetFocusedRowCellValue("NoSEP"), "") = False Then
                fn_DeleteData = False
                Exit Function
            End If

            fn_DeleteData = True

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariSEP() As String
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_CariSEP = "KOSONG"
                Exit Function
            End If

            Dim oSetKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetKoneksi = oSetKoneksi.CariSEP("VCLAIM", grv.GetFocusedRowCellValue("NoSEP"))

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                    fn_CariSEP = "ADA"
                Else
                    fn_CariSEP = "KOSONG"
                    'MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If

            End If

            fn_CariSEP = "KOSONG"

        Catch oErr As Exception
            fn_CariSEP = "KOSONG"
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_DeleteSEP() As Boolean
        Try
            If grv.GetFocusedRowCellValue("NoSEP") = String.Empty Then
                fn_DeleteSEP = True
                Exit Function
            End If

            fn_DeleteSEP = True

            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim jsonRequest As String = String.Empty

            jsonRequest = "{" & """request"": {" & """t_sep"": {" & """noSep"": """ & grv.GetFocusedRowCellValue("NoSEP") & """," & """user"": """ & sUserID & """" & "}}}"

            Dim dsSetKoneksi = oSetKoneksi.HapusSEP("VCLAIM", jsonRequest)

            If dsSetKoneksi <> "" Then
                Dim allData = JObject.Parse(dsSetKoneksi)
                Dim CodeResponse As String = String.Empty
                Dim messageResponse As String = String.Empty

                CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                messageResponse = allData("metaData")("message").ToString

                If CodeResponse = "200" Then
                Else
                    fn_DeleteSEP = False
                    MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                End If

            End If

        Catch oErr As Exception
            fn_DeleteSEP = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If grv.GetRowCellValue(e.RowHandle, "STATUSDAFTAR") = "BATAL" Then
            e.Appearance.BackColor = Color.LightYellow
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
            Case Keys.B
                If e.Alt = True And picBatal.Enabled = True Then
                    picBatal_Click()
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
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        Dim frmPENDAFTARAN As New frmPendaftaran
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPENDAFTARAN.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmPENDAFTARAN As New frmPENDAFTARAN
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmPENDAFTARAN.ShowDialog(Me)

            If sStatusSave <> "NEW" Then
                fn_LoadSecurity()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPENDAFTARAN Is Nothing Then frmPENDAFTARAN.Dispose()
            frmPENDAFTARAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If
        Dim frmPENDAFTARAN As New frmPENDAFTARAN
        Try
            frmPENDAFTARAN.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDPENDAFTARAN"))
            frmPENDAFTARAN.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmPENDAFTARAN Is Nothing Then frmPENDAFTARAN.Dispose()
            frmPENDAFTARAN = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDPENDAFTARAN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDPENDAFTARAN") Is Nothing Then
            Exit Sub
        End If

        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_DeleteSEP() = True Then
            If fn_CariSEP() = "KOSONG" Then
                If fn_DeleteData(grv.GetFocusedRowCellValue("KDPENDAFTARAN")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                End If
                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadSecurity()
            End If
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub
            Dim ds = oPendaftaran.GetData(grv.GetFocusedRowCellValue("KDPENDAFTARAN"))

            If ds.NOMORSEP <> "" Then
                Dim rpt As New xtraSEP
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                Dim rpt As New xtraUmum
                rpt.bindingSource.DataSource = ds
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picBatal_Click() Handles picBatal.Click
        Try
            If grv.GetFocusedRowCellValue("KDPENDAFTARAN") = String.Empty Then Exit Sub

            If oPendaftaran.UpdateDataBatal(grv.GetFocusedRowCellValue("KDPENDAFTARAN")) = True Then
                MsgBox("Status Daftar diperbaharui", MsgBoxStyle.Information, Me.Text)
            End If

            fn_LoadSecurity()

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub

    Private Sub picDelete_Click(sender As Object, e As EventArgs) Handles picDelete.Click

    End Sub
#End Region
End Class