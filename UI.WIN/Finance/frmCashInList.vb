Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmCashInList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oCashIn As New Finance.clsCashIn

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = CashIn.TITLE

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
                      Where x.MODUL = "CASHIN" _
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
            Me.Text = CashIn.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDCASHIN").Caption = CashIn.KDCASHIN
            grv.Columns("TANGGAL").Caption = CashIn.TANGGAL
            grv.Columns("SUBTOTAL").Caption = CashIn.SUBTOTAL
            grv.Columns("COSTSHARE").Caption = CashIn.COSTSHARE
            grv.Columns("DEPOSIT").Caption = CashIn.DEPOSIT
            grv.Columns("ADMIN").Caption = CashIn.ADMIN
            grv.Columns("ROUND").Caption = CashIn.ROUND
            grv.Columns("GRANDTOTAL").Caption = CashIn.GRANDTOTAL
            grv.Columns("MEMO").Caption = CashIn.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
            grv.Columns("KDPAYMENTTYPE").Caption = CashIn.KDPAYMENTTYPE
            grv.Columns("KDPENDAFTARAN").Caption = CashIn.KDPENDAFTARAN
            grv.Columns("KDPENDAFTARAN_AWAL").Caption = Pendaftaran.KDPENDAFTARAN_AWAL
            grv.Columns("PASIEN").Caption = Caption.ReferenceCustomer
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("NOINVOICE").Caption = CashIn.DETAIL_NOINVOICE
            grv1.Columns("AMOUNTPAYMENT").Caption = CashIn.DETAIL_AMOUNTPAYMENT
            grv1.Columns("REMARKS").Caption = CashIn.DETAIL_REMARKS
            grv1.Columns("AMOUNTORIGINAL").Caption = CashIn.DETAIL_AMOUNTORIGINAL
            grv1.Columns("AMOUNTDUE").Caption = CashIn.DETAIL_AMOUNTDUE
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    Dim ds = From x In oCashIn.GetData
        '             Select x.KDCASHIN, x.KDPENDAFTARAN, x.S_PENDAFTARAN_H.KDPENDAFTARAN_AWAL, PASIEN = x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY, TANGGAL = x.DATE, KDPAYMENTTYPE = x.M_PAYMENTTYPE.MEMO, x.MEMO, Details = x.F_CASHIN_Ds, x.SUBTOTAL, x.COSTSHARE, x.DEPOSIT, x.ADMIN, x.ROUND, x.GRANDTOTAL, x.KDUSER
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
            SQL &= "Kategori = (SELECT CASE A.CATEGORY WHEN 0 THEN 'Kasir 1' WHEN '1' THEN 'Kasir 2' ELSE 'Apotek' END) "
            SQL &= ",A.KDCASHIN "
            SQL &= ",A.KDPENDAFTARAN "
            SQL &= ",B.KDPENDAFTARAN_AWAL "
            SQL &= ",Tujuan = E.NAME_DISPLAY "
            SQL &= ",Nomedrek = B.KDCUSTOMER "
            SQL &= ",PASIEN = C.NAME_DISPLAY "
            SQL &= ",TANGGAL = A.DATE "
            SQL &= ",KDPAYMENTTYPE = D.MEMO "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.COSTSHARE "
            SQL &= ",A.DEPOSIT "
            SQL &= ",A.ADMIN "
            SQL &= ",A.ROUND "
            SQL &= ",A.GRANDTOTAL "
            SQL &= ",A.MEMO "
            SQL &= ",A.KDUSER "
            SQL &= ",Setor = (SELECT CASE A.ISSETOR WHEN 0 THEN CONVERT(BIT, 0) ELSE CONVERT(BIT, 1) END) "
            SQL &= "FROM "
            SQL &= "F_CASHIN_H A "
            SQL &= "INNER JOIN S_PENDAFTARAN_H B "
            SQL &= "ON A.KDPENDAFTARAN = B.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_CUSTOMER C "
            SQL &= "ON B.KDCUSTOMER = C.KDCUSTOMER "
            SQL &= "INNER JOIN M_PAYMENTTYPE D "
            SQL &= "ON A.KDPAYMENTTYPE = D.KDPAYMENTTYPE "
            SQL &= "INNER JOIN M_DEPARTMENT E "
            SQL &= "ON B.KDDEPARTMENT = E.KDDEPARTMENT "
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
    Private Sub grv_MasterRowExpanded(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs) Handles grv.MasterRowExpanded
        grv1 = TryCast(grv.GetDetailView(e.RowHandle, e.RelationIndex), DevExpress.XtraGrid.Views.Grid.GridView)

        fn_LoadFormatDataDetail()
        fn_LoadLanguageDetail()
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
    Private Sub fn_LoadFormatDataDetail()
        For iLoop As Integer = 0 To grv1.Columns.Count - 1
            If grv1.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv1.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv1.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv1.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv1.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv1.Columns("DATECREATED").Visible = False
        grv1.Columns("DATEUPDATED").Visible = False
        grv1.Columns("SEQ").Visible = False
        grv1.Columns("KDCASHIN").Visible = False
        grv1.Columns("F_CASHIN_H").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDCASHIN").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("F_CASHIN_H").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDCASHIN As String, ByVal IdentitasPasien As String) As Boolean
        Try
            Dim dsTotal = oCashIn.GetData(sKDCASHIN)
            Dim Total As String = "0"

            If dsTotal IsNot Nothing Then
                Total = dsTotal.GRANDTOTAL
            End If


            Dim frmPesanDelete As New frmPesanDelete
            frmPesanDelete.ShowDialog(Me)

            If sPesanHapus <> "XXXXXBATALXXXXX" Then
                oCashIn.DeleteData(sKDCASHIN)

                Dim oDelete As New Setting.clsDelete

                If oDelete.InsertData("CASHIN", sUserID, IdentitasPasien & "-" & sPesanHapus, sKDCASHIN & " : " & Total) = False Then
                    'fn_DeleteData = False
                End If

                fn_DeleteData = True

            Else
                fn_DeleteData = False
            End If

        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        If CBool(grv.GetRowCellValue(e.RowHandle, "Setor")) = True Then
            e.Appearance.BackColor = Color.LightGreen
        Else
            'e.Appearance.BackColor = Color.Red
        End If
    End Sub
    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPrint.Click
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then Exit Sub
        mnuStripCetak.Show(picPrint.Location.X, picPrint.Location.Y + 125)
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
            Case Keys.R
                If e.Alt = True And picRefresh.Enabled = True Then
                    picRefresh_Click()
                End If
        End Select
    End Sub
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If

        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDCASHIN"))
            frmCashIn.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmCashIn As New frmCashIn
        Try
            frmCashIn.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmCashIn.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmCashIn Is Nothing Then frmCashIn.Dispose()
            frmCashIn = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDCASHIN"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If
        If oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN")).ISSETOR = True Then
            MsgBox("No Kwitansi sudah distor tidak dapat dirubah", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If

        Dim frmPesanDelete As New frmPesanDelete
        frmPesanDelete.ShowDialog(Me)

        If sPesanHapus <> "XXXXXBATALXXXXX" Then
            Dim oDelete As New Setting.clsDelete
            Dim dsTotal = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))
            Dim Total As String = "0"

            If dsTotal IsNot Nothing Then
                Total = dsTotal.GRANDTOTAL
            End If

            Dim IdentitasPasien As String = grv.GetFocusedRowCellValue("Nomedrek") & " " & grv.GetFocusedRowCellValue("PASIEN") & " " & grv.GetFocusedRowCellValue("KDPAYMENTTYPE") & grv.GetFocusedRowCellValue("Tujuan")

            If oDelete.InsertData("CASHINUPDATE", sUserID, IdentitasPasien & "-" & sPesanHapus, grv.GetFocusedRowCellValue("KDCASHIN") & " : " & Total) = False Then
                MsgBox("Gagal Insert Tabel Delete", MsgBoxStyle.Exclamation, Me.Text)
                Exit Sub
                'fn_DeleteData = False
            End If

            Dim frmCashIn As New frmCashIn
            Try
                frmCashIn.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDCASHIN"))
                frmCashIn.ShowDialog(Me)
                fn_LoadSecurity()
            Catch oErr As Exception
                MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            Finally
                If Not frmCashIn Is Nothing Then frmCashIn.Dispose()
                frmCashIn = Nothing

                Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDCASHIN"), sCode)
                If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

                If sStatusSave = "NEW" Then
                    sStatusSave = "NONE"
                    picAdd_Click()
                End If
            End Try
        Else
            'MsgBox("Alasan Masih Kosong", MsgBoxStyle.Exclamation, Me.Text)
        End If
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDCASHIN") Is Nothing Then
            Exit Sub
        End If
        If oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN")).ISSETOR = True Then
            MsgBox("No Kwitansi sudah distor tidak dapat dirubah", MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub

        Dim Identitas As String = grv.GetFocusedRowCellValue("Nomedrek") & " " & grv.GetFocusedRowCellValue("PASIEN") & " " & grv.GetFocusedRowCellValue("KDPAYMENTTYPE") & grv.GetFocusedRowCellValue("Tujuan")

        If fn_DeleteData(grv.GetFocusedRowCellValue("KDCASHIN"), Identitas) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub DetilKecilToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KDCASHIN") = String.Empty Then Exit Sub
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang

            Dim dsCashin = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))

            Dim dsDaftar = oPendaftaran.GetData(dsCashin.KDPENDAFTARAN)

            If dsDaftar.CATEGORY = 1 Then
                Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)
                If dsPulang Is Nothing Then
                    MsgBox("Pasien belum Pulang", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    sPrintGrandTotal = dsCashin.GRANDTOTAL
                End If
            Else
                sPrintGrandTotal = dsCashin.GRANDTOTAL
            End If

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

            SQL = " SELECT  "
            SQL &= " A.KDCASHIN "
            SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
            SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,ITEM = E.NMITEM2 "
            'If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
            '    SQL &= " ,ITEM = E.NMITEM2 "
            'Else
            '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
            'End If
            SQL &= " ,QTY = SUM(D.QTY) "
            SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
            SQL &= " FROM F_CASHIN_H A  "
            SQL &= " INNER JOIN F_CASHIN_D B "
            SQL &= " ON A.KDCASHIN = B.KDCASHIN "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
            SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
            SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON D.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L3 F "
            SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
            SQL &= " WHERE "
            SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
            SQL &= "GROUP BY "
            SQL &= " A.KDCASHIN "
            SQL &= " ,A.KDPENDAFTARAN "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,A.KDPENDAFTARAN "
            'SQL &= " ,B.NOINVOICE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,D.KDDOCTOR "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                    dsRekap.ROUND = .Rows(iLoop)("ROUND")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraCashIn_Kecil

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub KwitansiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KwitansiToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDCASHIN") = String.Empty Then Exit Sub

            Dim rpt As New xtraKwitansiRekap

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))

            If ds.COSTSHARE > 0 Then
                sPrintGrandTotal = ds.COSTSHARE
                sCostShare = True

            Else
                sPrintGrandTotal = ds.GRANDTOTAL
                sCostShare = False
            End If


            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
            Dim dsPulang = oPulang.GetDatabyKD(ds.KDPENDAFTARAN)
            Dim sPulang As String = String.Empty

            If dsPulang IsNot Nothing Then
                sPulang = ds.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy") & " sampai tanggal " & dsPulang.DATE.ToString("dd-MM-yyyy") & " di " & sCompany
            Else
                sPulang = ds.S_PENDAFTARAN_H.DATE.ToString("dd-MM-yyyy") & " sampai tanggal " & ds.DATE.ToString("dd-MM-yyyy") & " di " & sCompany
            End If

            sPrintCashierDescriprtion = "Untuk pembayaran biaya pelayanan kesehatan pasien " & IIf(ds.S_PENDAFTARAN_H.CATEGORY = 0, "Rawat Jalan tanggal " & ds.DATE.ToString("dd-MM-yyyy"), "Rawat Inap tanggal " & sPulang)
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub RincianKwitansiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RincianKwitansiToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDCASHIN") = String.Empty Then Exit Sub
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang

            Dim dsCashin = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))

            Dim dsDaftar = oPendaftaran.GetData(dsCashin.KDPENDAFTARAN)

            If dsDaftar.CATEGORY = 1 Then
                Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)
                If dsPulang Is Nothing Then
                    MsgBox("Pasien belum Pulang", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    If dsCashin.COSTSHARE > 0 Then
                        sPrintGrandTotal = dsCashin.COSTSHARE
                        sCostShare = True

                    Else
                        sPrintGrandTotal = dsCashin.GRANDTOTAL
                        sCostShare = False
                    End If
                End If
            Else
                If dsCashin.COSTSHARE > 0 Then
                    sPrintGrandTotal = dsCashin.COSTSHARE
                    sCostShare = True

                Else
                    sPrintGrandTotal = dsCashin.GRANDTOTAL
                    sCostShare = False
                End If
            End If

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

            Dim listTranskasi As New List(Of R_CASHIN)

            If dsDaftar.CATEGORY = 0 Then
                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
                SQL &= " ,C.KDSOTRANSAKSI "
                SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
                SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,ITEM = E.NMITEM2 "

                'If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                '    'SQL &= " ,ITEM = E.NMITEM2 "
                '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                'Else
                '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                'End If
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L3 F "
                SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
                SQL &= "GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,A.KDPENDAFTARAN "
                'SQL &= " ,B.NOINVOICE "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,F.MEMO "
                SQL &= " ,E.NMITEM2 "
                SQL &= " ,C.KDSOTRANSAKSI "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    'SQL &= " ,ITEM = E.NMITEM2 "
                    SQL &= " ,D.KDDOCTOR "
                Else
                    SQL &= " ,D.KDDOCTOR "
                End If

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")

                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                        dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                        dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                        dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                        dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                        dsRekap.DPJP = .Rows(iLoop)("DPJP")
                        dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                        dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                        dsRekap.KELAS = .Rows(iLoop)("KELAS")
                        dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                        dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                        dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next

            Else
                SQL = " SELECT  "
                SQL &= " A.KDCASHIN "
                SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
                SQL &= " ,C.KDSOTRANSAKSI "
                SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
                SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
                SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
                SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,ITEM_GROUP = F.MEMO "
                SQL &= " ,ITEM = E.NMITEM2 "
                'If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                '    'SQL &= " ,ITEM = E.NMITEM2 "
                '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                'Else
                '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
                'End If
                SQL &= " ,QTY = SUM(D.QTY) "
                SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                SQL &= " FROM F_CASHIN_H A  "
                SQL &= " INNER JOIN F_CASHIN_D B "
                SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                SQL &= " INNER JOIN M_ITEM E "
                SQL &= " ON D.KDITEM = E.KDITEM "
                SQL &= " INNER JOIN M_ITEM_L3 F "
                SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
                SQL &= " WHERE "
                SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
                'SQL &= " AND F.MEMO NOT IN('LABORATORIUM', 'RADIOLOGI', 'XFARMASI') "
                SQL &= " GROUP BY "
                SQL &= " A.KDCASHIN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,C.KDKUNJUNGAN "
                SQL &= " ,A.KDPENDAFTARAN "
                SQL &= " ,A.SUBTOTAL "
                SQL &= " ,A.COSTSHARE "
                SQL &= " ,A.DEPOSIT "
                SQL &= " ,A.ADMIN "
                SQL &= " ,A.ROUND "
                SQL &= " ,A.GRANDTOTAL "
                SQL &= " ,F.MEMO "
                SQL &= " ,E.NMITEM2 "
                SQL &= " ,A.DATE "
                SQL &= " ,C.KDSOTRANSAKSI "
                If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
                    'SQL &= " ,ITEM = E.NMITEM2 "
                    SQL &= " ,D.KDDOCTOR "
                Else
                    SQL &= " ,D.KDDOCTOR "
                End If

                'SQL &= " ORDER BY A.DATE "

                oComm.Connection = oConn
                oComm.CommandText = SQL
                oComm.CommandTimeout = 120
                oComm.CommandType = CommandType.Text

                da = New SqlDataAdapter(oComm)
                da.Fill(ds, "ALL")


                For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                    Dim dsRekap As New DataAccess.R_CASHIN

                    With ds.Tables("ALL")
                        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                        dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                        dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                        dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                        dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                        dsRekap.DPJP = .Rows(iLoop)("DPJP")
                        dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                        dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                        dsRekap.KELAS = .Rows(iLoop)("KELAS")
                        dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                        dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                        dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                        ' dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                        dsRekap.QTY = .Rows(iLoop)("QTY")
                        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                        listTranskasi.Add(dsRekap)
                    End With
                Next


                'SQL = " SELECT  "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                'SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                'SQL &= " ,C.KDSOTRANSAKSI "
                'SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                'SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                'SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                'SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                'SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                'SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                'SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                'SQL &= " ,TANGGAL_PULANG = '" & oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN).DATE & "' "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,ITEM_GROUP = F.MEMO "
                'SQL &= " ,ITEM = 'BIAYA LABORATORIUM' "
                'SQL &= " ,QTY = SUM(D.QTY) "
                'SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                'SQL &= " FROM F_CASHIN_H A  "
                'SQL &= " INNER JOIN F_CASHIN_D B "
                'SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                'SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                'SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN M_ITEM E "
                'SQL &= " ON D.KDITEM = E.KDITEM "
                'SQL &= " INNER JOIN M_ITEM_L3 F "
                'SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
                'SQL &= " WHERE "
                'SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
                'SQL &= " AND F.MEMO = 'LABORATORIUM' "
                'SQL &= "GROUP BY "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,F.MEMO "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,A.DATE "
                'SQL &= " ,C.KDSOTRANSAKSI "

                'oComm.Connection = oConn
                'oComm.CommandText = SQL
                'oComm.CommandTimeout = 120
                'oComm.CommandType = CommandType.Text

                'da = New SqlDataAdapter(oComm)
                'da.Fill(ds, "ALL2")


                'For iLoop As Integer = 0 To ds.Tables("ALL2").Rows.Count - 1
                '    Dim dsRekap As New DataAccess.R_CASHIN

                '    With ds.Tables("ALL2")
                '        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                '        dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                '        dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                '        dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                '        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                '        dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                '        dsRekap.DPJP = .Rows(iLoop)("DPJP")
                '        dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                '        dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                '        dsRekap.KELAS = .Rows(iLoop)("KELAS")
                '        dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                '        dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                '        dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                '        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                '        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                '        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                '        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                '        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                '        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.QTY = .Rows(iLoop)("QTY")
                '        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                '        listTranskasi.Add(dsRekap)
                '    End With
                'Next

                'SQL = " SELECT  "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                'SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                'SQL &= " ,C.KDSOTRANSAKSI "
                'SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                'SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                'SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                'SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                'SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                'SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                'SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                'SQL &= " ,TANGGAL_PULANG = '" & oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN).DATE & "' "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,ITEM_GROUP = F.MEMO "
                'SQL &= " ,ITEM = 'BIAYA RADIOLOGI' "
                'SQL &= " ,QTY = SUM(D.QTY) "
                'SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                'SQL &= " FROM F_CASHIN_H A  "
                'SQL &= " INNER JOIN F_CASHIN_D B "
                'SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                'SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                'SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN M_ITEM E "
                'SQL &= " ON D.KDITEM = E.KDITEM "
                'SQL &= " INNER JOIN M_ITEM_L3 F "
                'SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
                'SQL &= " WHERE "
                'SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
                'SQL &= " AND F.MEMO = 'RADIOLOGI' "
                'SQL &= "GROUP BY "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,F.MEMO "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,A.DATE"
                'SQL &= " ,C.KDSOTRANSAKSI "

                'oComm.Connection = oConn
                'oComm.CommandText = SQL
                'oComm.CommandTimeout = 120
                'oComm.CommandType = CommandType.Text

                'da = New SqlDataAdapter(oComm)
                'da.Fill(ds, "ALL3")

                'For iLoop As Integer = 0 To ds.Tables("ALL3").Rows.Count - 1
                '    Dim dsRekap As New DataAccess.R_CASHIN

                '    With ds.Tables("ALL3")
                '        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                '        dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                '        dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                '        dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                '        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                '        dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                '        dsRekap.DPJP = .Rows(iLoop)("DPJP")
                '        dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                '        dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                '        dsRekap.KELAS = .Rows(iLoop)("KELAS")
                '        dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                '        dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                '        dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                '        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                '        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                '        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                '        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                '        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                '        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.QTY = .Rows(iLoop)("QTY")
                '        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                '        listTranskasi.Add(dsRekap)
                '    End With
                'Next

                'SQL = " SELECT  "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,CATEGORY = '" & dsDaftar.CATEGORY & "' "
                'SQL &= " ,KDPENDAFTARAN = '" & dsDaftar.KDPENDAFTARAN & "' "
                'SQL &= " ,C.KDSOTRANSAKSI "
                'SQL &= " ,TUJUAN = '" & dsDaftar.M_DEPARTMENT.NAME_DISPLAY & "' "
                'SQL &= " ,DPJP = '" & dsDaftar.M_DOCTOR.NAME_DISPLAY & "' "
                'SQL &= " ,KDCUSTOMER = '" & dsDaftar.KDCUSTOMER & "' "
                'SQL &= " ,PASIEN = '" & dsDaftar.M_CUSTOMER.NAME_DISPLAY & "' "
                'SQL &= " ,ALAMAT = '" & dsDaftar.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
                'SQL &= " ,KELAS = '" & dsDaftar.M_KELASRAWAT.MEMO & "' "
                'SQL &= " ,TANGGAL_DATANG = '" & dsDaftar.DATE & "' "
                'SQL &= " ,TANGGAL_PULANG = '" & oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN).DATE & "' "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,ITEM_GROUP = F.MEMO "
                'SQL &= " ,ITEM = 'BIAYA FARMASI' "
                'SQL &= " ,QTY = SUM(D.QTY) "
                'SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
                'SQL &= " FROM F_CASHIN_H A  "
                'SQL &= " INNER JOIN F_CASHIN_D B "
                'SQL &= " ON A.KDCASHIN = B.KDCASHIN "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
                'SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
                'SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
                'SQL &= " INNER JOIN M_ITEM E "
                'SQL &= " ON D.KDITEM = E.KDITEM "
                'SQL &= " INNER JOIN M_ITEM_L3 F "
                'SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
                'SQL &= " WHERE "
                'SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
                'SQL &= " AND F.MEMO = 'XFARMASI' "
                'SQL &= "GROUP BY "
                'SQL &= " A.KDCASHIN "
                'SQL &= " ,F.MEMO "
                'SQL &= " ,A.SUBTOTAL "
                'SQL &= " ,A.COSTSHARE "
                'SQL &= " ,A.DEPOSIT "
                'SQL &= " ,A.ADMIN "
                'SQL &= " ,A.ROUND "
                'SQL &= " ,A.GRANDTOTAL "
                'SQL &= " ,A.DATE "
                'SQL &= " ,C.KDSOTRANSAKSI "

                'oComm.Connection = oConn
                'oComm.CommandText = SQL
                'oComm.CommandTimeout = 120
                'oComm.CommandType = CommandType.Text

                'da = New SqlDataAdapter(oComm)
                'da.Fill(ds, "ALL1")

                'For iLoop As Integer = 0 To ds.Tables("ALL1").Rows.Count - 1
                '    Dim dsRekap As New DataAccess.R_CASHIN

                '    With ds.Tables("ALL1")
                '        dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                '        dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                '        dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                '        dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                '        dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                '        dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                '        dsRekap.DPJP = .Rows(iLoop)("DPJP")
                '        dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                '        dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                '        dsRekap.KELAS = .Rows(iLoop)("KELAS")
                '        dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                '        dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                '        dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                '        dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                '        dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                '        dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                '        dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                '        dsRekap.ROUND = .Rows(iLoop)("ROUND")
                '        dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.ITEM = .Rows(iLoop)("ITEM")
                '        dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                '        dsRekap.QTY = .Rows(iLoop)("QTY")
                '        dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                '        listTranskasi.Add(dsRekap)
                '    End With
                'Next

            End If

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim rpt As New xtraCashIn

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub RincianKwitansiPenunjangToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RincianKwitansiPenunjangToolStripMenuItem.Click
        Try
            If grv.GetFocusedRowCellValue("KDCASHIN") = String.Empty Then Exit Sub
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang

            Dim dsCashin = oCashIn.GetData(grv.GetFocusedRowCellValue("KDCASHIN"))

            Dim dsDaftar = oPendaftaran.GetData(dsCashin.KDPENDAFTARAN)

            If dsDaftar.CATEGORY = 1 Then
                Dim dsPulang = oPulang.GetDatabyKD(dsDaftar.KDPENDAFTARAN)
                If dsPulang Is Nothing Then
                    MsgBox("Pasien belum Pulang", MsgBoxStyle.Exclamation, Me.Text)
                    Exit Sub
                Else
                    If dsCashin.COSTSHARE > 0 Then
                        sPrintGrandTotal = dsCashin.COSTSHARE
                        sCostShare = True

                    Else
                        sPrintGrandTotal = dsCashin.GRANDTOTAL
                        sCostShare = False
                    End If
                End If
            Else
                If dsCashin.COSTSHARE > 0 Then
                    sPrintGrandTotal = dsCashin.COSTSHARE
                    sCostShare = True

                Else
                    sPrintGrandTotal = dsCashin.GRANDTOTAL
                    sCostShare = False
                End If
            End If

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

            SQL = " SELECT  "
            SQL &= " A.KDCASHIN "
            SQL &= " ,CATEGORY = (SELECT CATEGORY FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KDPENDAFTARAN = (SELECT AA.KDPENDAFTARAN FROM S_PENDAFTARAN_H AA INNER JOIN S_PENDAFTARAN_KUNJUNGAN BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN WHERE C.KDKUNJUNGAN = BB.KDKUNJUNGAN) "
            SQL &= " ,TUJUAN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DEPARTMENT BB ON AA.KDDEPARTMENT = BB.KDDEPARTMENT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,DPJP = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_DOCTOR BB ON AA.KDDOCTOR = BB.KDDOCTOR WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN) "
            SQL &= " ,KDCUSTOMER = (SELECT KDCUSTOMER FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,PASIEN = (SELECT BB.NAME_DISPLAY FROM S_PENDAFTARAN_H AA INNER JOIN M_CUSTOMER BB ON AA.KDCUSTOMER = BB.KDCUSTOMER WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,ALAMAT = (SELECT TOP 1 ALAMAT FROM S_PENDAFTARAN_KUNJUNGAN WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,KELAS = (SELECT BB.MEMO FROM S_PENDAFTARAN_H AA INNER JOIN M_KELASRAWAT BB ON AA.KDKELASRAWAT = BB.KDKELASRAWAT WHERE A.KDPENDAFTARAN = AA.KDPENDAFTARAN)  "
            SQL &= " ,TANGGAL_DATANG = (SELECT DATE FROM S_PENDAFTARAN_H WHERE A.KDPENDAFTARAN = KDPENDAFTARAN) "
            SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT DATE FROM T_UPDATE_TANGGAL_PULANG WHERE A.KDPENDAFTARAN = KDPENDAFTARAN), GETDATE()) "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,ITEM = E.NMITEM2 "
            'If dsDaftar.M_DAFTAR_L1.MEMO = "BPJS" Then
            '    SQL &= " ,ITEM = E.NMITEM2 "
            'Else
            '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
            'End If
            SQL &= " ,QTY = SUM(D.QTY) "
            SQL &= " ,GRANDTOTAL_DETAIL = SUM(D.GRANDTOTAL)  "
            SQL &= " FROM F_CASHIN_H A  "
            SQL &= " INNER JOIN F_CASHIN_D B "
            SQL &= " ON A.KDCASHIN = B.KDCASHIN "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_H C  "
            SQL &= " ON B.NOINVOICE = C.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D D "
            SQL &= " ON C.KDSOTRANSAKSI = D.KDSOTRANSAKSI "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON D.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L3 F "
            SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3  "
            SQL &= " WHERE "
            SQL &= " A.KDCASHIN = '" & grv.GetFocusedRowCellValue("KDCASHIN") & "' "
            SQL &= "GROUP BY "
            SQL &= " A.KDCASHIN "
            SQL &= " ,A.KDPENDAFTARAN "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,A.KDPENDAFTARAN "
            'SQL &= " ,B.NOINVOICE "
            SQL &= " ,A.SUBTOTAL "
            SQL &= " ,A.COSTSHARE "
            SQL &= " ,A.DEPOSIT "
            SQL &= " ,A.ADMIN "
            SQL &= " ,A.ROUND "
            SQL &= " ,A.GRANDTOTAL "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,D.KDDOCTOR "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = dsDaftar.M_DAFTAR_L3.MEMO
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                    dsRekap.ROUND = .Rows(iLoop)("ROUND")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraCashIn_Kecil

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub FarmasiToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FarmasiToolStripMenuItem.Click
        Try
            Dim sKDPENDAFTARAN As String = grv.GetFocusedRowCellValue("KDPENDAFTARAN")
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(sKDPENDAFTARAN)
            Dim TanggalPulang As DateTime = Now

            If dsPendaftaran Is Nothing Then
                Exit Sub
            Else
                Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                Dim dsPulang = oPulang.GetDatabyKD(sKDPENDAFTARAN)
                If dsPulang IsNot Nothing Then
                    TanggalPulang = dsPulang.DATE
                End If
            End If

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

            SQL = "SELECT * FROM ( "
            SQL &= " SELECT  "
            SQL &= " D.KDPENDAFTARAN "
            SQL &= " ,DPJP = (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) "
            SQL &= " ,PASIEN = (SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE D.KDCUSTOMER = KDCUSTOMER) "
            'SQL &= " ,KELOMPOKPASIEN = ISNULL((SELECT MEMO FROM M_DAFTAR_L1 WHERE D.KDDAFTAR_L1 = KDDAFTAR_L1), '') "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,KELAS = (SELECT MEMO FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT) "
            SQL &= " ,TANGGAL_DATANG = D.DATE "
            SQL &= " ,TANGGAL_PULANG = ISNULL((SELECT TOP 1 DATE FROM T_UPDATE_TANGGAL_PULANG WHERE D.KDPENDAFTARAN = KDPENDAFTARAN), D.DATE) "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,NAMAKUNJUNGAN = (SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE C.KDDEPARTMENT = KDDEPARTMENT) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,TANGGAL_INPUT = A.DATE "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,F.KDITEM_L3 "
            SQL &= " ,ITEM = E.NMITEM2 "

            'If dsPendaftaran.M_DAFTAR_L2.MEMO = "BPJS" Then
            '    SQL &= " ,ITEM = E.NMITEM2 "
            'Else
            '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
            'End If
            SQL &= " ,QTY = SUM(B.QTY) "
            SQL &= " ,GRANDTOTAL = SUM(B.GRANDTOTAL) "
            SQL &= " FROM "
            SQL &= " S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B "
            SQL &= " ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= " ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
            SQL &= " INNER JOIN S_PENDAFTARAN_H D "
            SQL &= " ON C.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON B.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L3 F "
            SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3 "
            SQL &= " WHERE  "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN & "' "
            SQL &= " OR "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN_AWAL & "' "
            SQL &= " GROUP BY "
            SQL &= " D.KDPENDAFTARAN "
            SQL &= " ,D.KDDOCTOR "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,D.KDKELASRAWAT "
            SQL &= " ,D.DATE "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,C.KDDEPARTMENT  "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,F.MEMO "
            SQL &= " ,F.KDITEM_L3 "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.DATE "
            SQL &= " ,B.KDDOCTOR "
            'SQL &= " ORDER BY A.DATE "
            SQL &= " ) Z "
            SQL &= " WHERE  "
            SQL &= "Z.KDITEM_L3 = 'ITEM_L2_0000000016' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALLL")

            Dim listTranskasi As New List(Of R_BILLING)

            For iLoop As Integer = 0 To ds.Tables("ALLL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_BILLING

                With ds.Tables("ALLL")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.KELOMPOKPASIEN = dsPendaftaran.M_DAFTAR_L1.MEMO
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                    dsRekap.NAMAKUNJUNGAN = .Rows(iLoop)("NAMAKUNJUNGAN")
                    dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                    dsRekap.TANGGAL_INPUT = .Rows(iLoop)("TANGGAL_INPUT")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")

                    listTranskasi.Add(dsRekap)

                    sPrintGrandTotal = .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            If listTranskasi.Count > 0 Then
                Dim rpt As New xtraCashInBilling

                rpt.ShowPrintMarginsWarning = False
                rpt.Watermark.Text = sWATERMARK

                rpt.bindingSource.DataSource = listTranskasi
                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
            Else
                MsgBox("Rincian Farmasi Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If


        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub GabunganToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles GabunganToolStripMenuItem.Click
        Try
            Dim sKDPENDAFTARAN As String = grv.GetFocusedRowCellValue("KDPENDAFTARAN")
            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(sKDPENDAFTARAN)
            Dim TanggalPulang As DateTime = Now

            If dsPendaftaran Is Nothing Then
                Exit Sub
            Else
                Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                Dim dsPulang = oPulang.GetDatabyKD(sKDPENDAFTARAN)
                If dsPulang IsNot Nothing Then
                    TanggalPulang = dsPulang.DATE
                End If
            End If

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

            SQL = " SELECT  "
            SQL &= " D.KDPENDAFTARAN "
            SQL &= " ,DPJP = (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) "
            SQL &= " ,PASIEN = (SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE D.KDCUSTOMER = KDCUSTOMER) "
            'SQL &= " ,KELOMPOKPASIEN = ISNULL((SELECT MEMO FROM M_DAFTAR_L1 WHERE D.KDDAFTAR_L1 = KDDAFTAR_L1), '') "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,KELAS = (SELECT MEMO FROM M_KELASRAWAT WHERE D.KDKELASRAWAT = KDKELASRAWAT) "
            SQL &= " ,TANGGAL_DATANG = D.DATE "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,NAMAKUNJUNGAN = (SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE C.KDDEPARTMENT = KDDEPARTMENT) "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,TANGGAL_INPUT = A.DATE "
            SQL &= " ,ITEM_GROUP = F.MEMO "
            SQL &= " ,ITEM = E.NMITEM2 "
            'If dsPendaftaran.M_DAFTAR_L2.MEMO = "BPJS" Then
            '    'SQL &= " ,ITEM = E.NMITEM2 "
            '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
            'Else
            '    SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE B.KDDOCTOR = KDDOCTOR) + ')' "
            'End If
            SQL &= " ,QTY = SUM(B.QTY) "
            SQL &= " ,GRANDTOTAL = SUM(B.GRANDTOTAL) "
            SQL &= " FROM "
            SQL &= " S_SO_TRANSAKSI_H A "
            SQL &= " INNER JOIN S_SO_TRANSAKSI_D B "
            SQL &= " ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= " INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= " ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
            SQL &= " INNER JOIN S_PENDAFTARAN_H D "
            SQL &= " ON C.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= " INNER JOIN M_ITEM E "
            SQL &= " ON B.KDITEM = E.KDITEM "
            SQL &= " INNER JOIN M_ITEM_L3 F "
            SQL &= " ON E.KDITEM_L3 = F.KDITEM_L3 "
            SQL &= " WHERE  "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN & "' "
            SQL &= " OR "
            SQL &= " D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN_AWAL & "' "
            SQL &= " GROUP BY "
            SQL &= " D.KDPENDAFTARAN "
            SQL &= " ,D.KDDOCTOR "
            SQL &= " ,D.KDCUSTOMER "
            SQL &= " ,C.ALAMAT "
            SQL &= " ,D.KDKELASRAWAT "
            SQL &= " ,D.DATE "
            SQL &= " ,D.CATEGORY "
            SQL &= " ,C.KDKUNJUNGAN "
            SQL &= " ,C.KDDEPARTMENT  "
            SQL &= " ,A.KDSOTRANSAKSI "
            SQL &= " ,F.MEMO "
            SQL &= " ,E.NMITEM2 "
            SQL &= " ,A.DATE "
            SQL &= " ,B.KDDOCTOR "
            SQL &= " ORDER BY A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALLL")

            Dim listTranskasi As New List(Of R_BILLING)

            For iLoop As Integer = 0 To ds.Tables("ALLL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_BILLING

                With ds.Tables("ALLL")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.KELOMPOKPASIEN = dsPendaftaran.M_DAFTAR_L1.MEMO
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDKUNJUNGAN = .Rows(iLoop)("KDKUNJUNGAN")
                    dsRekap.NAMAKUNJUNGAN = .Rows(iLoop)("NAMAKUNJUNGAN")
                    dsRekap.KDSOTRANSAKSI = .Rows(iLoop)("KDSOTRANSAKSI")
                    dsRekap.TANGGAL_INPUT = .Rows(iLoop)("TANGGAL_INPUT")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.TUJUAN = dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY
                    listTranskasi.Add(dsRekap)

                    sPrintGrandTotal = .Rows(iLoop)("GRANDTOTAL")
                End With
            Next

            Dim rpt As New xtraCashInBilling

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#End Region
End Class