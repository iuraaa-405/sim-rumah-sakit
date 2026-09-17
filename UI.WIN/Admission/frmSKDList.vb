Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmSKDList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSKD As New Admission.clsSKD

#Region "Function"
    Private Sub Me_Load() Handles Me.Load
        Me.Text = SKD.TITLE

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
                      Where x.MODUL = "SKD" _
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
            Me.Text = SKD.TITLE

            'grv.Columns("KDSKD").Caption = SKD.KDSKD
            'grv.Columns("KDPENDAFTARAN").Caption = SKD.KDPENDAFTARAN
            'grv.Columns("TUJUAN").Caption = SKD.KDDEPARTMENT
            'grv.Columns("DOKTER").Caption = SKD.KDDOCTOR
            'grv.Columns("KDCUSTOMER").Caption = Customer.KDCUSTOMER
            'grv.Columns("NAME_DISPLAY").Caption = Customer.NAME_DISPLAY
            'grv.Columns("DATEKONTROL").Caption = SKD.TANGGAL_KONTROL

            grv.Columns("KDSKD").Caption = SKD.KDSKD
            grv.Columns("KDPENDAFTARAN").Caption = Pendaftaran.KDPENDAFTARAN
            grv.Columns("TANGGAL").Caption = Pendaftaran.TANGGAL
            grv.Columns("TUJUAN").Caption = Pendaftaran.KDDEPARTMENT
            grv.Columns("DOKTER").Caption = Pendaftaran.KDDOCTOR
            grv.Columns("KDCUSTOMER").Caption = "Rekam Medis"
            grv.Columns("PASIEN").Caption = Customer.NAME_DISPLAY
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("STATUSDAFTAR").Caption = Pendaftaran.STATUSDAFTAR
            grv.Columns("JENISPASIEN").Caption = "Jenis Pasien"
            grv.Columns("DATEKONTROL").Caption = SKD.TANGGAL_KONTROL
            grv.Columns("DAFTAR_L1").Caption = sDaftar_L1
            grv.Columns("DAFTAR_L2").Caption = sDaftar_L2
            grv.Columns("DAFTAR_L3").Caption = sDaftar_L3
            grv.Columns("DAFTAR_L4").Caption = sDaftar_L4
            grv.Columns("DAFTAR_L5").Caption = sDaftar_L5
            grv.Columns("DAFTAR_L6").Caption = sDaftar_L6

            ColumnChooserToolStripMenuItem.Text = Caption.ColumnChooser
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oSKD.GetData
        '             Select x.KDSKD, x.S_PENDAFTARAN_H.KDCUSTOMER, x.DATEKONTROL, x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, x.KDPENDAFTARAN, TUJUAN = x.M_DEPARTMENT.NAME_DISPLAY, DOKTER = x.M_DOCTOR.NAME_DISPLAY

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
            'SQL &= "No = ROW_NUMBER() OVER (ORDER BY A.KDPENDAFTARAN) "
            SQL &= "K.KDSKD "
            SQL &= ",K.DATEKONTROL "
            SQL &= ",A.KDPENDAFTARAN "
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
            SQL &= ",JENISPASIEN = (SELECT CASE WHEN CONVERT(VARCHAR(8), A.DATE, 112) = CONVERT(VARCHAR(8), D.DATECREATED, 112) THEN 'PASIEN BARU' ELSE 'PASIEN LAMA' END) "
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
            SQL &= "INNER JOIN S_PENDAFTARAN_SKD K "
            SQL &= "ON A.KDPENDAFTARAN = K.KDPENDAFTARAN "
            If chkKontrol.Checked = False Then
                SQL &= "WHERE CONVERT(VARCHAR(8), A.DATE, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), A.DATE, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' AND K.ALASAN = 'KONTROL' "
            Else
                SQL &= "WHERE CONVERT(VARCHAR(8), K.DATEKONTROL, 112) >= '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' AND CONVERT(VARCHAR(8), K.DATEKONTROL, 112) <= '" & deDATETo.DateTime.ToString("yyyyMMdd") & "' AND K.ALASAN = 'KONTROL' "
            End If
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

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
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

        'grv.Columns("KDSKD").Visible = False
        'grv.Columns("ISDEFAULT").Visible = False
        'grv.Columns("ISACTIVE").Visible = False

        'grv.Columns("KDSKD").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub ColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles ColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSKD As String) As Boolean
        Try
            oSKD.DeleteData(sKDSKD)
            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_CariNomorRencanaKontrol(ByVal SuratKontrol As String) As Boolean
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.CariNomorRencanaKontrol(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, SuratKontrol)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_CariNomorRencanaKontrol = True
                    Else
                        fn_CariNomorRencanaKontrol = False
                    End If
                Else
                    fn_CariNomorRencanaKontrol = False
                End If
            Else
                fn_CariNomorRencanaKontrol = False
            End If
        Catch oErr As Exception
            fn_CariNomorRencanaKontrol = False
        End Try
    End Function
    Private Function fn_HapusRencanaKontrol() As Boolean
        Try
            Dim oSetKoneksi As New Brigging.clsSetKoneksi
            Dim uTime As Integer = 0

            If sVclaim_ConsId <> "" Then
                Dim jsonRequest As String = String.Empty

                jsonRequest = "{" & """request"": {" & """t_suratkontrol"": {" & """noSuratKontrol"": """ & grv.GetFocusedRowCellValue("KDSKD") & """," & """user"": """ & sUserID & """" & "}}}"

                uTime = (DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
                Dim dsSetKoneksi = oSetKoneksi.HapusRencanaKontrol(sVclaim_Url, sVclaim_ConsId, sVclaim_SecreatKey, sVclaim_UserKey, uTime, jsonRequest)

                If dsSetKoneksi <> "" Then
                    Dim allData = JObject.Parse(dsSetKoneksi)

                    Dim CodeResponse As String = String.Empty
                    Dim messageResponse As String = String.Empty

                    CodeResponse = IIf(IsDBNull(allData.Item("metaData").Item("code")) = True, "", allData.Item("metaData").Item("code"))
                    messageResponse = allData("metaData")("message").ToString

                    If CodeResponse = "200" Then
                        fn_HapusRencanaKontrol = True
                    Else
                        fn_HapusRencanaKontrol = False
                        MsgBox(CodeResponse & " - " & messageResponse, MsgBoxStyle.Exclamation, Me.Text)
                    End If
                Else
                    fn_HapusRencanaKontrol = False
                    MsgBox("Delete Rencana Kontrol Data Gagal/Kosong", MsgBoxStyle.Exclamation, Me.Text)
                End If
            Else
                fn_HapusRencanaKontrol = False
                MsgBox("Koneksi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            fn_HapusRencanaKontrol = False
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
        If grv.GetFocusedRowCellValue("KDSKD") Is Nothing Then
            Exit Sub
        End If

        Dim frmSKD As New frmSKD
        Try
            frmSKD.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDSKD"))
            frmSKD.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmSKD As New frmSKD
        Try
            frmSKD.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmSKD.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSKD Is Nothing Then frmSKD.Dispose()
            frmSKD = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSKD") Is Nothing Then
            Exit Sub
        End If
        Dim frmSKD As New frmSKD
        Try
            frmSKD.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSKD"))
            frmSKD.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSKD Is Nothing Then frmSKD.Dispose()
            frmSKD = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("MEMO"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSKD") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        If fn_CariNomorRencanaKontrol(grv.GetFocusedRowCellValue("KDSKD")) = False Then
            If fn_DeleteData(grv.GetFocusedRowCellValue("KDSKD")) = False Then
                MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
            Else
                MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                fn_LoadSecurity()
            End If
        Else
            If fn_HapusRencanaKontrol() = True Then
                If fn_DeleteData(grv.GetFocusedRowCellValue("KDSKD")) = False Then
                    MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
                    fn_LoadSecurity()
                End If
            End If
        End If
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            If grv.GetFocusedRowCellValue("KDSKD") Is Nothing Then
                Exit Sub
            End If

            If grv.GetFocusedRowCellValue("KDSKD") = String.Empty Then Exit Sub

            Dim rpt As New xtraRencanaKontrol

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oSKD.GetDataOfline(grv.GetFocusedRowCellValue("KDSKD"))

            rpt.bindingSource.DataSource = ds
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