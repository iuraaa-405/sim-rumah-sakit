Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class frmSetorList
    Implements ILanguage

    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSetor As New Finance.clsSetor

#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = Setor.TITLE

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
                      Where x.MODUL = "SETOR" _
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
            Me.Text = Setor.TITLE

            fn_LoadLanguageMaster()
            fn_LoadLanguageDetail()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Public Sub fn_LoadLanguageMaster()
        Try
            grv.Columns("KDSETOR").Caption = Setor.KDSETOR
            grv.Columns("DATE").Caption = Setor.TANGGAL
            'grv.Columns("SHIFT").Caption = Setor.SHIFT
            grv.Columns("GRANDTOTAL").Caption = Setor.GRANDTOTAL
            grv.Columns("MEMO").Caption = Setor.MEMO
            grv.Columns("KDUSER").Caption = Caption.User
        Catch oErr As Exception

        End Try
    End Sub
    Public Sub fn_LoadLanguageDetail()
        Try
            grv1.Columns("NOINVOICE").Caption = Setor.DETAIL_NOINVOICE
            grv1.Columns("AMOUNTPAYMENT").Caption = Setor.DETAIL_AMOUNTPAYMENT
            grv1.Columns("REMARKS").Caption = Setor.DETAIL_REMARKS
        Catch oErr As Exception

        End Try
    End Sub
    Private Sub fn_LoadData()
        Try
            Dim ds = From x In oSetor.GetData
                     Select x.KDSETOR, x.DATE, x.GRANDTOTAL, x.KDUSER, x.MEMO, Details = x.F_SETOR_Ds
            grd.DataSource = ds.ToList

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
        grv1.Columns("KDSETOR").Visible = False
        grv1.Columns("F_SETOR_H").Visible = False

        grv1.Columns("DATECREATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("DATEUPDATED").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("SEQ").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("KDSETOR").OptionsColumn.ShowInCustomizationForm = False
        grv1.Columns("F_SETOR_H").OptionsColumn.ShowInCustomizationForm = False
    End Sub
    Private Sub MasterColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles MasterColumnChooserToolStripMenuItem.Click
        grv.ShowCustomization()
    End Sub
    Private Sub DetailColumnChooserToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles DetailColumnChooserToolStripMenuItem.Click
        grv1.ShowCustomization()
    End Sub
    Private Function fn_DeleteData(ByVal sKDSetor As String) As Boolean
        Try
            oSetor.DeleteData(sKDSetor)

            fn_DeleteData = True
        Catch oErr As Exception
            fn_DeleteData = False
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub grv_RowStyle(sender As System.Object, e As DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs) Handles grv.RowStyle
        'If grv.IsFilterRow(e.RowHandle) Then Exit Sub
        'If CBool(grv.GetRowCellValue(e.RowHandle, "ISDELETE")) = True Then
        '    e.Appearance.BackColor = Color.LightGray
        'Else
        '    e.Appearance.BackColor = Color.LightGreen
        'End If
    End Sub
    Private Sub MenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picPrint.Click
        'If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then Exit Sub

        Try
            If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
                Exit Sub
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
            SQL &= " A.KDSETOR "
            SQL &= " ,B.CATEGORY "
            SQL &= " ,TANGGAL = B.DATE "
            SQL &= " ,URAIAN = ISNULL((SELECT CC.NAME_DISPLAY FROM F_CASHIN_H AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN INNER JOIN M_CUSTOMER CC ON BB.KDCUSTOMER = CC.KDCUSTOMER WHERE AA.KDCASHIN = A.NOINVOICE GROUP BY CC.NAME_DISPLAY), (SELECT NAMAPASIEN FROM S_SO_TANPARESEP_H WHERE A.NOINVOICE = KDSOTANPARESEP)) "
            SQL &= " ,A.AMOUNTPAYMENT "
            SQL &= " ,B.GRANDTOTAL  "
            SQL &= " FROM  "
            SQL &= " F_SETOR_D A  "
            SQL &= " INNER JOIN F_SETOR_H B "
            SQL &= " ON A.KDSETOR = B.KDSETOR "
            SQL &= " WHERE A.KDSETOR = '" & grv.GetFocusedRowCellValue("KDSETOR") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_SETOR_D")

            Dim listTranskasi As New List(Of R_SETOR_01)

            For iLoop As Integer = 0 To ds.Tables("F_SETOR_D").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_SETOR_01

                With ds.Tables("F_SETOR_D")
                    dsRekap.KDSETOR = .Rows(iLoop)("KDSETOR")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.TANGGAL = .Rows(iLoop)("TANGGAL")
                    dsRekap.URAIAN = .Rows(iLoop)("URAIAN")
                    dsRekap.AMOUNTPAYMENT = .Rows(iLoop)("AMOUNTPAYMENT")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraSetor

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi

            Dim oKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetor = oSetor.GetData(grv.GetFocusedRowCellValue("KDSETOR"))

            If dsSetor IsNot Nothing Then
                'Dim KDREPORT As String = String.Empty
                'If dsSetor.CATEGORY = 0 Then
                '    KDREPORT = "R_0001"
                'ElseIf dsSetor.CATEGORY = 1 Then
                '    KDREPORT = "R_0002"
                'ElseIf dsSetor.CATEGORY = 2 Then
                '    KDREPORT = "R_0003"
                'ElseIf dsSetor.CATEGORY = 3 Then
                '    KDREPORT = "R_0004"
                'Else
                '    KDREPORT = "R_0005"
                'End If

                'Dim dsKoneksi = oKoneksi.GetDataFooter(KDREPORT)

                'If dsKoneksi IsNot Nothing Then
                '    sCATEGORYSETOR = dsKoneksi.CATEGORY
                '    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = dsKoneksi.PANGKAT_1
                '    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = dsKoneksi.PANGKAT_2
                '    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                '    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = sTempatTTD & ", " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                '    rpt.Parameters.Item("YANGMENERIMA").Value = dsKoneksi.NAME_DISPLAY_1
                '    rpt.Parameters.Item("YANGMENYETORKAN").Value = dsKoneksi.NAME_DISPLAY_2
                'Else
                '    sCATEGORYSETOR = ""
                '    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = ""
                '    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = ""
                '    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                '    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = sTempatTTD & ", " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                '    rpt.Parameters.Item("YANGMENERIMA").Value = ""
                '    rpt.Parameters.Item("YANGMENYETORKAN").Value = ""
                'End If

                rpt.Parameters.Item("KDUSER").Value = dsSetor.KDUSER
                'rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            Else
                sCATEGORYSETOR = ""
                MsgBox("Setor Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
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
    Private Sub grv_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs)
        If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
            Exit Sub
        End If

        Dim frmSetor As New frmSetor
        Try
            frmSetor.LoadMe(FORM_MODE.FORM_MODE_VIEW, grv.GetFocusedRowCellValue("KDSETOR"))
            frmSetor.ShowDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picAdd_Click() Handles picAdd.Click
        Dim frmSetor As New frmSetor
        Try
            frmSetor.LoadMe(FORM_MODE.FORM_MODE_ADD)
            frmSetor.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSetor Is Nothing Then frmSetor.Dispose()
            frmSetor = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSETOR"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picUpdate_Click() Handles picUpdate.Click
        If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
            Exit Sub
        End If
        Dim frmSetor As New frmSetor
        Try
            frmSetor.LoadMe(FORM_MODE.FORM_MODE_EDIT, grv.GetFocusedRowCellValue("KDSETOR"))
            frmSetor.ShowDialog(Me)
            fn_LoadSecurity()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        Finally
            If Not frmSetor Is Nothing Then frmSetor.Dispose()
            frmSetor = Nothing

            Dim rowHandle As Integer = grv.LocateByValue(rowHandle, grv.Columns("KDSETOR"), sCode)
            If rowHandle > 0 Then grv.FocusedRowHandle = rowHandle

            If sStatusSave = "NEW" Then
                sStatusSave = "NONE"
                picAdd_Click()
            End If
        End Try
    End Sub
    Private Sub picDelete_Click() Handles picDelete.Click
        If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
            Exit Sub
        End If
        If MsgBox(Statement.DeleteQuestion, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, Me.Text) = MsgBoxResult.No Then Exit Sub
        If fn_DeleteData(grv.GetFocusedRowCellValue("KDSETOR")) = False Then
            MsgBox(Statement.DeleteFail, MsgBoxStyle.Exclamation, Me.Text)
            Exit Sub
        End If
        MsgBox(Statement.DeleteSuccess, MsgBoxStyle.Information, Me.Text)
        fn_LoadSecurity()
    End Sub
    Private Function fn_Bulan(ByVal TANGGAL As DateTime) As String
        Dim Bulan As String = ""

        If TANGGAL.ToString("MM") = "01" Then
            Bulan = "Januari"
        ElseIf TANGGAL.ToString("MM") = "02" Then
            Bulan = "Pebruari"
        ElseIf TANGGAL.ToString("MM") = "03" Then
            Bulan = "Maret"
        ElseIf TANGGAL.ToString("MM") = "04" Then
            Bulan = "April"
        ElseIf TANGGAL.ToString("MM") = "05" Then
            Bulan = "Mei"
        ElseIf TANGGAL.ToString("MM") = "06" Then
            Bulan = "Juni"
        ElseIf TANGGAL.ToString("MM") = "07" Then
            Bulan = "Juli"
        ElseIf TANGGAL.ToString("MM") = "08" Then
            Bulan = "Agustus"
        ElseIf TANGGAL.ToString("MM") = "09" Then
            Bulan = "September"
        ElseIf TANGGAL.ToString("MM") = "10" Then
            Bulan = "Oktober"
        ElseIf TANGGAL.ToString("MM") = "11" Then
            Bulan = "Nopember"
        ElseIf TANGGAL.ToString("MM") = "12" Then
            Bulan = "Desember"
        End If

        fn_Bulan = Bulan

    End Function
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_LoadSecurity()
    End Sub
    Private Sub Cetak2ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
                Exit Sub
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
            SQL &= " A.KDSETOR "
            SQL &= " ,B.CATEGORY "
            SQL &= " ,TANGGAL = B.DATE "
            SQL &= " ,URAIAN = ISNULL((SELECT CC.NAME_DISPLAY FROM F_CASHIN_H AA INNER JOIN S_PENDAFTARAN_H BB ON AA.KDPENDAFTARAN = BB.KDPENDAFTARAN INNER JOIN M_CUSTOMER CC ON BB.KDCUSTOMER = CC.KDCUSTOMER WHERE AA.KDCASHIN = A.NOINVOICE GROUP BY CC.NAME_DISPLAY), (SELECT NAMAPASIEN FROM S_SO_TANPARESEP_H WHERE A.NOINVOICE = KDSOTANPARESEP)) "
            SQL &= " ,A.AMOUNTPAYMENT "
            SQL &= " ,B.GRANDTOTAL  "
            SQL &= " FROM  "
            SQL &= " F_SETOR_D A  "
            SQL &= " INNER JOIN F_SETOR_H B "
            SQL &= " ON A.KDSETOR = B.KDSETOR "
            SQL &= " WHERE A.KDSETOR = '" & grv.GetFocusedRowCellValue("KDSETOR") & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "F_SETOR_D")

            Dim listTranskasi As New List(Of R_SETOR_01)

            For iLoop As Integer = 0 To ds.Tables("F_SETOR_D").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_SETOR_01

                With ds.Tables("F_SETOR_D")
                    dsRekap.KDSETOR = .Rows(iLoop)("KDSETOR")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.TANGGAL = .Rows(iLoop)("TANGGAL")
                    dsRekap.URAIAN = .Rows(iLoop)("URAIAN")
                    dsRekap.AMOUNTPAYMENT = .Rows(iLoop)("AMOUNTPAYMENT")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraSetor

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = listTranskasi

            Dim oKoneksi As New Brigging.clsSetKoneksi

            Dim dsSetor = oSetor.GetData(grv.GetFocusedRowCellValue("KDSETOR"))

            If dsSetor IsNot Nothing Then
                Dim KDREPORT As String = String.Empty
                If dsSetor.CATEGORY = 0 Then
                    KDREPORT = "R_0001"
                ElseIf dsSetor.CATEGORY = 1 Then
                    KDREPORT = "R_0002"
                ElseIf dsSetor.CATEGORY = 2 Then
                    KDREPORT = "R_0003"
                ElseIf dsSetor.CATEGORY = 3 Then
                    KDREPORT = "R_0004"
                Else
                    KDREPORT = "R_0005"
                End If

                Dim dsKoneksi = oKoneksi.GetDataFooter(KDREPORT)

                If dsKoneksi IsNot Nothing Then
                    sCATEGORYSETOR = dsKoneksi.CATEGORY
                    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = dsKoneksi.PANGKAT_1
                    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = dsKoneksi.PANGKAT_2
                    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = "Garut, " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("YANGMENERIMA").Value = dsKoneksi.NAME_DISPLAY_1
                    rpt.Parameters.Item("YANGMENYETORKAN").Value = dsKoneksi.NAME_DISPLAY_2
                Else
                    sCATEGORYSETOR = ""
                    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = ""
                    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = ""
                    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = "Garut, " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("YANGMENERIMA").Value = ""
                    rpt.Parameters.Item("YANGMENYETORKAN").Value = ""
                End If

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            Else
                sCATEGORYSETOR = ""
                MsgBox("Setor Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Cetak1ToolStripMenuItem_Click(sender As Object, e As EventArgs)
        Try
            If grv.GetFocusedRowCellValue("KDSETOR") Is Nothing Then
                Exit Sub
            End If

            Dim dsSetor = oSetor.GetData(grv.GetFocusedRowCellValue("KDSETOR"))

            Dim rpt As New xtraBuktiPenyetoran
            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK

            rpt.bindingSource.DataSource = dsSetor

            Dim oKoneksi As New Brigging.clsSetKoneksi

            If dsSetor IsNot Nothing Then
                sPrintGrandTotal = dsSetor.GRANDTOTAL

                Dim KDREPORT As String = String.Empty
                If dsSetor.CATEGORY = 0 Then
                    KDREPORT = "R_0001"
                    sCATEGORYSETOR_ = dsSetor.CATEGORY & " Poliklinik"
                ElseIf dsSetor.CATEGORY = 1 Then
                    KDREPORT = "R_0002"
                ElseIf dsSetor.CATEGORY = 2 Then
                    KDREPORT = "R_0003"
                ElseIf dsSetor.CATEGORY = 3 Then
                    KDREPORT = "R_0004"
                Else
                    KDREPORT = "R_0005"
                End If

                Dim dsKoneksi = oKoneksi.GetDataFooter(KDREPORT)

                If dsKoneksi IsNot Nothing Then
                    sCATEGORYSETOR = dsKoneksi.CATEGORY
                    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = dsKoneksi.PANGKAT_1
                    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = dsKoneksi.PANGKAT_2
                    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = "Garut, " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("YANGMENERIMA").Value = dsKoneksi.NAME_DISPLAY_1
                    rpt.Parameters.Item("YANGMENYETORKAN").Value = dsKoneksi.NAME_DISPLAY_2
                Else
                    sCATEGORYSETOR = ""
                    rpt.Parameters.Item("PANGKATYANGMENERIMA").Value = ""
                    rpt.Parameters.Item("PANGKATYANGMENYETORKAN").Value = ""
                    rpt.Parameters.Item("TANGGAL").Value = dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("TANGGALTANDATANGAN").Value = "Garut, " & dsSetor.DATE.ToString("dd") & " " & fn_Bulan(dsSetor.DATE) & " " & dsSetor.DATE.ToString("yyyy")
                    rpt.Parameters.Item("YANGMENERIMA").Value = ""
                    rpt.Parameters.Item("YANGMENYETORKAN").Value = ""
                End If

                Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
                printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            Else
                sCATEGORYSETOR = ""
                MsgBox("Setor Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region
End Class