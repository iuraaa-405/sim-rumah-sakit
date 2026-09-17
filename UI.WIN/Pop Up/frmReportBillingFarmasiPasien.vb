Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports DevExpress.XtraPrinting
Imports System.Data
Imports System.Data.SqlClient


Public Class frmReportBillingFarmasiPasien

#Region "Function"
    Private sRekamMedis As String = String.Empty
    Private sKategori As String = String.Empty

    Public Sub fn_RekamMedis(ByVal KDCUSTOMER As String, ByVal Kategori As String)
        sRekamMedis = KDCUSTOMER
        sKategori = Kategori
    End Sub
    Private Sub Me_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sKODECPPTCOPY = ""
        Me.Text = "Riwayat Rekam Medis " & sRekamMedis & " " & sKategori
        fn_Preview()
    End Sub
    Private Sub Me_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'Try
        '    grv.SaveLayoutToRegistry("HKEY_CURRENT_USER\Software\SIMRS\SW\" & sUserID & "\" & Me.Text & "\" & cboTYPE.Text)
        'Catch oErr As Exception

        'End Try
    End Sub
    Private Sub fn_Print()
        Try
            printableComponentLink.Landscape = True
            printableComponentLink.PaperKind = Printing.PaperKind.A4

            Dim phf As PageHeaderFooter =
        TryCast(printableComponentLink.PageHeaderFooter, PageHeaderFooter)
            phf.Header.Content.Clear()
            phf.Header.Font = New Font("Times New Roman", 12, FontStyle.Bold)
            phf.Header.LineAlignment = BrickAlignment.Center
            phf.Footer.Font = New Font("Times New Roman", 9.75)
            phf.Footer.LineAlignment = BrickAlignment.Far
            phf.Footer.Content.AddRange(New String() _
        {sWATERMARK, "", Report.REPORT_PAGE & " : [Page # of Pages #]"})

            phf.Header.Content.AddRange(New String() _
{"", "Riwayat Rekam Medis " & sRekamMedis & " " & sKategori, ""})

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

            If sKategori = "Resep" Then
                fn_LoadDataResep()
            Else
                fn_LoadDataObat()
            End If

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

#Region "Master Detail"
    Private Sub fn_LoadDataResep()
        Try
            Dim oConn As New SqlConnection
            Dim oComm As New SqlCommand
            Dim da As SqlDataAdapter
            Dim ds As New DataSet
            Dim SQL As String

            Dim sConn As String = Decrypt(My.Computer.Registry.GetValue("HKEY_CURRENT_USER\Software\SIMRS\RME\", "Database", "").ToString())

            oConn = New SqlConnection(sConn)
            If oConn.State = ConnectionState.Closed Then
                oConn.Open()
            End If

            SQL = "SELECT "
            SQL &= "B.KDCPPT "
            SQL &= ",TANGGAL = B.DATE "
            SQL &= ",DOKTER = A.KDDOCTOR_NAMA "
            SQL &= ",TUJUAN = A.KDDEPARTMENT_NAMA "
            SQL &= ",C.NAMAOBAT "
            SQL &= ",C.JUMLAH "
            SQL &= ",C.SATUAN "
            SQL &= ",C.SIGNA "
            SQL &= ",C.CARAPAKAI "
            SQL &= "FROM "
            SQL &= "A_IDENTITASPASIEN_LIST A "
            SQL &= "INNER JOIN R_CPPT B "
            SQL &= "ON A.KDIDENTITAS = B.KDIDENTITAS "
            SQL &= "INNER JOIN R_CPPT_NONRACIKAN C "
            SQL &= "ON B.KDCPPT = C.KDCPPT "
            SQL &= "WHERE "
            SQL &= "A.KDCUSTOMER = '" & sRekamMedis & "' "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
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
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:n0}"
                grv.Columns(iLoop).AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far
            ElseIf grv.Columns(iLoop).ColumnType.Name = "DateTime" Then
                grv.Columns(iLoop).DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime
                grv.Columns(iLoop).DisplayFormat.FormatString = "{0:dd/MM/yyyy}"
            End If
        Next

        grv.Columns("KDCPPT").Group()
        grv.ExpandAllGroups()

        grv.BestFitColumns()
    End Sub
    Private Sub fn_LoadDataObat()
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
            SQL &= "KDCPPT = B.KDSOTRANSAKSI "
            SQL &= ",TANGGAL = B.DATE "
            SQL &= ",DOKTER = H.NAME_DISPLAY "
            SQL &= ",TUJUAN = I.NAME_DISPLAY "
            SQL &= ",NAMAOBAT = D.NMITEM2 "
            SQL &= ",JUMLAH = C.QTY "
            SQL &= ",SATUAN = G.MEMO "
            SQL &= ",SIGNA = E.MEMO "
            SQL &= ",CARAPAKAI = F.MEMO "
            SQL &= "FROM "
            SQL &= "S_PENDAFTARAN_KUNJUNGAN A "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_H B "
            SQL &= "ON A.KDKUNJUNGAN = B.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D C "
            SQL &= "ON B.KDSOTRANSAKSI = C.KDSOTRANSAKSI "
            SQL &= "INNER JOIN M_ITEM D "
            SQL &= "ON C.KDITEM = D.KDITEM "
            SQL &= "INNER JOIN M_SIGNA E "
            SQL &= "ON C.KDSIGNA = E.KDSIGNA "
            SQL &= "INNER JOIN M_CARAPAKAI F "
            SQL &= "ON C.KDCARAPAKAI = F.KDCARAPAKAI "
            SQL &= "INNER JOIN M_UOM G "
            SQL &= "ON C.KDUOM = G.KDUOM "
            SQL &= "INNER JOIN M_DOCTOR H "
            SQL &= "ON A.KDDOCTOR = H.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT I "
            SQL &= "ON A.KDDEPARTMENT = I.KDDEPARTMENT "
            SQL &= "INNER JOIN S_PENDAFTARAN_H J "
            SQL &= "ON A.KDPENDAFTARAN = J.KDPENDAFTARAN "
            SQL &= "WHERE "
            SQL &= "J.KDCUSTOMER = '" & sRekamMedis & "' "
            SQL &= "AND B.CATEGORY = 4 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "P_PO_H")

            grd.MainView = grv
            grd.DataSource = ds.Tables("P_PO_H")
            grd.ForceInitialize()

            fn_LoadFormatData()

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
    Private Sub AmbilDataToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AmbilDataToolStripMenuItem.Click
        sKODECPPTCOPY = grv.GetFocusedRowCellValue("KDCPPT")
        Me.Close()
    End Sub
    Private Sub grv_DoubleClick(sender As Object, e As EventArgs) Handles grv.DoubleClick
        sKODECPPTCOPY = grv.GetFocusedRowCellValue("KDCPPT")
        Me.Close()
    End Sub
#End Region
End Class