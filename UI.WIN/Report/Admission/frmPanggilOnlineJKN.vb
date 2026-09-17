Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports Newtonsoft.Json.Linq
Imports System.Data.SqlClient

Public Class frmPanggilOnlineJKN
#Region "Function"
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Text = "Panggil Antrian JKN Mobile"

        sKODEBOOKINGONLINEPANGGIL = ""
        chkPanggil.Checked = True
        lDATEFROM.Text = "Tanggal :"

        fn_LoadDepartment()
        deDATEFrom.DateTime = Now

        picRefresh_Click()
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 14, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Panggil Antrian JKN Mobile", ""})

            printableComponentLink.Component = grd
            printableComponentLink.CreateDocument()
            printableComponentLink.ShowPreviewDialog(Me)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_Preview()
        Try
            grv.Columns.Clear()
            grd.DataSource = Nothing
            grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways
            grv1.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways

            fn_LoadData(grdKDDEPARTMENT.EditValue)

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData(ByVal Poli As String)
        Try
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
            SQL &= "A.KODEBOOKING "
            SQL &= ",A.NOMORANTREAN "
            SQL &= ",A.NORM "
            SQL &= ",NAMA = ISNULL((SELECT NAME_DISPLAY FROM M_CUSTOMER WHERE KDCUSTOMER = NORM), '') "
            'SQL &= ",A.JENISPASIEN_RS "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.NOHP "
            SQL &= ",A.NAMAPOLI "
            SQL &= ",A.NAMADOKTER "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",STATUSPANGGIL = (SELECT CASE A.ISPANGGIL WHEN 0 THEN 'BELUM DI PANGGIL' WHEN 1 THEN '1 mulai waktu tunggu admisi' WHEN 2 THEN '2 akhir waktu tunggu admisi/mulai waktu layan admisi' WHEN 3 THEN '3 akhir waktu layan admisi/mulai waktu tunggu poli' WHEN 4 THEN '4 akhir waktu tunggu poli/mulai waktu layan poli' WHEN 5 THEN '5 akhir waktu layan poli/mulai waktu tunggu farmasi' WHEN 6 THEN '6 akhir waktu tunggu farmasi/mulai waktu layan farmasi membuat obat' WHEN 7 THEN '7 akhir waktu obat selesai dibuat' ELSE '99 tidak hadir/batal' END) "
            SQL &= ",A.KETERANGAN "
            SQL &= "FROM "
            SQL &= "SET_BOOKING_ANTRIAN A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "'  "
            If chkPanggil.Checked = True Then
                SQL &= "AND "
                SQL &= "A.ISPANGGIL = 0 "
            End If
            If Poli <> "" Then
                SQL &= "AND "
                SQL &= "KODEPOLI = '" & Poli & "' "
            End If
            SQL &= "ORDER BY A.KODEBOOKING "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "R_ANTRIAN_JKN")

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd.MainView = grv
            grd.DataSource = ds.Tables("R_ANTRIAN_JKN")
            grd.ForceInitialize()
            fn_LoadFormatData()

            sPoliDefault = grdKDDEPARTMENT.EditValue

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#Region "Master Detail"
    Private Sub fn_LoadFormatData()
        For iLoop As Integer = 0 To grv.Columns.Count - 1
            If grv.Columns(iLoop).ColumnType.Name = "Decimal" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n2}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy HH:mm:ss}"
            End If
        Next

        grv.Columns("KODEBOOKING").VisibleIndex = -1
    End Sub
    Private Sub fn_LoadDepartment()
        Dim oDepartment As New Reference.clsDepartment
        Try
            grdKDDEPARTMENT.Properties.DataSource = oDepartment.GetData.Where(Function(x) x.ISACTIVE = True And x.VCLAIM_KODEPOLI <> "").ToList()
            grdKDDEPARTMENT.Properties.ValueMember = "VCLAIM_KODEPOLI"
            grdKDDEPARTMENT.Properties.DisplayMember = "NAME_DISPLAY"

            grdKDDEPARTMENT.Text = sPoliDefault
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
#End Region

#End Region
#Region "Command Button"
    Private Sub frmMember_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.Escape
                Me.Close()
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
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_Preview()
    End Sub
    Private Sub btnSemua_Click(sender As Object, e As EventArgs) Handles btnSemua.Click
        grdKDDEPARTMENT.ResetText()
    End Sub
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        If grv.GetFocusedRowCellValue("KODEBOOKING") Is Nothing Then
            Exit Sub
        End If

        sKODEBOOKINGONLINEPANGGIL = grv.GetFocusedRowCellValue("KODEBOOKING")
        Me.Close()
    End Sub
#End Region
End Class