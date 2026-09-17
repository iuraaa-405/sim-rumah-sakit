Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient
Imports DevExpress.XtraPrinting

Public Class frmReportAntrianPoli
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oSET_BOOKING_ANTRIAN As New Antrian.clsSetBookingAntrian
    Private oSet_Panggilan As New Antrian.clsSetPanggilAntrian

#Region "Function"
    Private Sub fn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        deDATEFrom.DateTime = Now
        lblKode.ResetText()
        lblPanggil.ResetText()

        fn_LoadSecurity()
    End Sub
    Private Sub fn_LoadSecurity()
        Try
            Dim oOtority As New Setting.clsOtority
            Dim oUser As New Setting.clsUser

            Dim ds = (From x In oOtority.GetDataDetail
                      Join y In oUser.GetData
                     On x.KDOTORITY Equals y.KDOTORITY
                      Where x.MODUL = "ANTRIAN" _
                     And y.KDUSER = sUserID
                      Select x.ISADD, x.ISDELETE, x.ISUPDATE, x.ISPRINT, x.ISVIEW).FirstOrDefault

            Try
                picPrint.Enabled = ds.ISPRINT
                picRefresh.Enabled = ds.ISVIEW

                If ds.ISVIEW = True Then
                    fn_Preview()
                End If
            Catch oErr As Exception
                MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)

                picPrint.Enabled = False
                picRefresh.Enabled = False
            End Try
        Catch oErr As Exception
            MsgBox(Statement.SecurityNotInstalled, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        fn_Validate = True
    End Function
    Private Sub fn_Print()
        If fn_Validate() Then
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
            {"", "", "Halaman: [Page # of Pages #]"})

                Select Case cboType.SelectedIndex
                    Case 0
                        phf.Header.Content.AddRange(New String() _
            {"", "LAPORAN ANTRIAN ONLINE " & cboType.Text.ToString.Trim.ToUpper, ""})

                End Select

                printableComponentLink.CreateDocument()
                printableComponentLink.ShowPreview()
            Catch ex As Exception
                MsgBox("Print Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        End If
    End Sub
    Private Sub fn_Preview()
        If fn_Validate() Then
            Try
                grv.Columns.Clear()
                grd.DataSource = Nothing
                grv.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleIfExpanded

                fn_LoadCari()

                Select Case cboType.SelectedIndex
                    Case 0
                        fn_LoadData01()
                    Case 1
                        fn_LoadData02()
                    Case 2
                        fn_LoadData03()
                    Case 3
                        fn_LoadData04()
                End Select
            Catch oErr As Exception
                MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try
        End If
    End Sub
    Private Sub fn_LoadCari()
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
            SQL &= "A.KDBOOKINGANTREAN "
            SQL &= ",NOMORANTRIAN = CONVERT(INT, RIGHT(NOMORANTREAN, 5)) "
            SQL &= ",POLI = ISNULL((SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE A.KODEPOLI = KDPOLIBPJS), '-') "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",PENDING = (SELECT CASE A.JENISREQUEST WHEN 3 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM SET_BOOKING_ANTRIAN AS A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            'SQL &= "AND A.ISAPROVAL = 0 "
            SQL &= "ORDER BY A.KDBOOKINGANTREAN, A.TANGGALPERIKSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_ANTRIAN")

            bindingSource.DataSource = ds.Tables("SET_BOOKING_ANTRIAN")

            grd.DataSource = bindingSource
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            Dim sBanyakAntrian As Integer = 0
            Dim sSisaAntrian As Integer = 0
            Dim sPending As Integer = 0

            For iLoop As Integer = 0 To ds.Tables("SET_BOOKING_ANTRIAN").Rows.Count - 1
                With ds.Tables("SET_BOOKING_ANTRIAN")
                    sBanyakAntrian += 1

                    If .Rows(iLoop)("ISAPROVAL") = False Then
                        sSisaAntrian += 1
                    End If
                    If .Rows(iLoop)("PENDING") = True Then
                        sPending += 1
                    End If
                End With
            Next

            lblBanyakAntrian.Text = sBanyakAntrian
            lblSisaAntrian.Text = sSisaAntrian
            lblPending.Text = sPending

        Catch oErr As Exception
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadData01()
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
            SQL &= "A.KDBOOKINGANTREAN "
            SQL &= ",NOMORANTRIAN = CONVERT(INT, RIGHT(NOMORANTREAN, 5)) "
            SQL &= ",POLI = ISNULL((SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE A.KODEPOLI = KDPOLIBPJS), '-') "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",PENDING = (SELECT CASE A.JENISREQUEST WHEN 3 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM SET_BOOKING_ANTRIAN AS A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.ISAPROVAL = 0 "
            SQL &= "ORDER BY A.KDBOOKINGANTREAN, A.TANGGALPERIKSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_ANTRIAN")

            bindingSource.DataSource = ds.Tables("SET_BOOKING_ANTRIAN")

            grd.DataSource = bindingSource
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub fn_LoadData02()
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
            SQL &= "A.KDBOOKINGANTREAN "
            SQL &= ",NOMORANTRIAN = CONVERT(INT, RIGHT(NOMORANTREAN, 5)) "
            SQL &= ",POLI = ISNULL((SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE A.KODEPOLI = KDPOLIBPJS), '-') "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",PENDING = (SELECT CASE A.JENISREQUEST WHEN 3 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM SET_BOOKING_ANTRIAN AS A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.JENISREQUEST = 3 "
            SQL &= "ORDER BY A.KDBOOKINGANTREAN, A.TANGGALPERIKSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_ANTRIAN")

            bindingSource.DataSource = ds.Tables("SET_BOOKING_ANTRIAN")

            grd.DataSource = bindingSource
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub fn_LoadData03()
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
            SQL &= "A.KDBOOKINGANTREAN "
            SQL &= ",NOMORANTRIAN = CONVERT(INT, RIGHT(NOMORANTREAN, 5)) "
            SQL &= ",POLI = ISNULL((SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE A.KODEPOLI = KDPOLIBPJS), '-') "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",PENDING = (SELECT CASE A.JENISREQUEST WHEN 3 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM SET_BOOKING_ANTRIAN AS A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "AND A.ISAPROVAL = 1 "
            SQL &= "ORDER BY A.KDBOOKINGANTREAN, A.TANGGALPERIKSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_ANTRIAN")

            bindingSource.DataSource = ds.Tables("SET_BOOKING_ANTRIAN")

            grd.DataSource = bindingSource
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub fn_LoadData04()
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
            SQL &= "A.KDBOOKINGANTREAN "
            SQL &= ",NOMORANTRIAN = CONVERT(INT, RIGHT(NOMORANTREAN, 5)) "
            SQL &= ",POLI = ISNULL((SELECT NAME_DISPLAY FROM M_DEPARTMENT WHERE A.KODEPOLI = KDPOLIBPJS), '-') "
            SQL &= ",A.NOMORREFERENSI "
            SQL &= ",A.NOMORKARTU "
            SQL &= ",A.NIK "
            SQL &= ",A.ISAPROVAL "
            SQL &= ",PENDING = (SELECT CASE A.JENISREQUEST WHEN 3 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END) "
            SQL &= "FROM SET_BOOKING_ANTRIAN AS A "
            SQL &= "WHERE CONVERT(VARCHAR(8), A.TANGGALPERIKSA, 112) = '" & deDATEFrom.DateTime.ToString("yyyyMMdd") & "' "
            SQL &= "ORDER BY A.KDBOOKINGANTREAN, A.TANGGALPERIKSA "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "SET_BOOKING_ANTRIAN")

            bindingSource.DataSource = ds.Tables("SET_BOOKING_ANTRIAN")

            grd.DataSource = bindingSource
            grd.ForceInitialize()

            fn_SetFormat()

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

        Catch oErr As Exception
            MsgBox("Load Antrian Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub fn_SetFormat()
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
    Private Function fn_SaveTransaksi(ByVal NomorAntrian As Integer, ByVal kode As String) As Boolean
        Try
            fn_SaveTransaksi = True

            Dim dsSetPanggilan = oSet_Panggilan.GetData(cboLoket.Text)

            ' ***** HEADER *****

            Dim ds = oSet_Panggilan.GetStructureHeader

            With ds
                .LOKET = cboLoket.Text
                .KODE = kode
                .NOMORANTRIAN = NomorAntrian
                .DESCRIPTION = ""
                .ISPANGGIL = False
            End With

            If dsSetPanggilan Is Nothing Then
                fn_SaveTransaksi = oSet_Panggilan.InsertData(ds)
            Else
                fn_SaveTransaksi = oSet_Panggilan.UpdateData(ds)
            End If

        Catch oErr As Exception
            MsgBox("Simpan Panggil: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_SaveTransaksi = False
        End Try
    End Function
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
            Case Keys.P
                If e.Alt = True And picPanggil.Enabled = True Then
                    picPanggil_Click()
                End If
            Case Keys.U
                If e.Alt = True And picUlang.Enabled = True Then
                    picUlang_Click()
                End If
            Case Keys.F5
                If e.Alt = True And picPending.Enabled = True Then
                    picPending_Click()
                End If
        End Select
    End Sub
    Private Sub picPrint_Click() Handles picPrint.Click
        Try
            fn_Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub picRefresh_Click() Handles picRefresh.Click
        fn_Preview()
    End Sub
    Private Sub picPanggil_Click() Handles picPanggil.Click
        If cboType.SelectedIndex = 0 Then
            Dim dsSET_BOOKING_ANTRIAN = oSET_BOOKING_ANTRIAN.GetDataLastUser(deDATEFrom.DateTime)

            If dsSET_BOOKING_ANTRIAN IsNot Nothing Then
                lblKode.Text = dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN
                lblPanggil.Text = "Nomor : " & dsSET_BOOKING_ANTRIAN.NOMORANTREAN

                oSET_BOOKING_ANTRIAN.UpdateAproval(dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN)

                fn_SaveTransaksi(CInt(Microsoft.VisualBasic.Right(dsSET_BOOKING_ANTRIAN.NOMORANTREAN, 5)), IIf(dsSET_BOOKING_ANTRIAN.KODEPOLI <> "A" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "B" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "C" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "D", "A", dsSET_BOOKING_ANTRIAN.KODEPOLI))
            Else
                lblKode.ResetText()
                lblPanggil.ResetText()
                MsgBox("Data Tidak ada", MsgBoxStyle.Information, Me.Text)
            End If
        Else
            If grv.GetFocusedRowCellValue("KDBOOKINGANTREAN") Is Nothing Then
                MsgBox("Data Tidak ada", MsgBoxStyle.Information, Me.Text)
            Else
                Dim dsSET_BOOKING_ANTRIAN = oSET_BOOKING_ANTRIAN.GetData(grv.GetFocusedRowCellValue("KDBOOKINGANTREAN"))

                If dsSET_BOOKING_ANTRIAN IsNot Nothing Then
                    lblKode.Text = dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN
                    lblPanggil.Text = "Nomor : " & dsSET_BOOKING_ANTRIAN.NOMORANTREAN

                    oSET_BOOKING_ANTRIAN.UpdatePending(dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN)

                    fn_SaveTransaksi(CInt(Microsoft.VisualBasic.Right(dsSET_BOOKING_ANTRIAN.NOMORANTREAN, 5)), IIf(dsSET_BOOKING_ANTRIAN.KODEPOLI <> "A" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "B" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "C" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "D", "A", dsSET_BOOKING_ANTRIAN.KODEPOLI))
                Else
                    lblKode.ResetText()
                    lblPanggil.ResetText()
                    MsgBox("Data Tidak ada", MsgBoxStyle.Information, Me.Text)
                End If
            End If

        End If

        fn_Preview()

    End Sub
    Private Sub picUlang_Click() Handles picUlang.Click
        Dim dsSET_BOOKING_ANTRIAN = oSET_BOOKING_ANTRIAN.GetData(lblKode.Text)
        If dsSET_BOOKING_ANTRIAN IsNot Nothing Then
            lblKode.Text = dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN
            lblPanggil.Text = "Nomor : " & dsSET_BOOKING_ANTRIAN.NOMORANTREAN

            fn_SaveTransaksi(CInt(Microsoft.VisualBasic.Right(dsSET_BOOKING_ANTRIAN.NOMORANTREAN, 5)), IIf(dsSET_BOOKING_ANTRIAN.KODEPOLI <> "A" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "B" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "C" Or dsSET_BOOKING_ANTRIAN.KODEPOLI <> "D", "A", dsSET_BOOKING_ANTRIAN.KODEPOLI))

        Else
            lblKode.ResetText()
            lblPanggil.ResetText()
            MsgBox("Data Tidak ada", MsgBoxStyle.Information, Me.Text)
        End If

        fn_Preview()

    End Sub
    Private Sub picPending_Click() Handles picPending.Click
        Dim dsSET_BOOKING_ANTRIAN = oSET_BOOKING_ANTRIAN.GetData(lblKode.Text)
        If dsSET_BOOKING_ANTRIAN IsNot Nothing Then
            lblKode.Text = dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN
            lblPanggil.Text = "Nomor : " & dsSET_BOOKING_ANTRIAN.NOMORANTREAN

            oSET_BOOKING_ANTRIAN.UpdatePending(dsSET_BOOKING_ANTRIAN.KDBOOKINGANTREAN)

            lblKode.ResetText()
            lblPanggil.ResetText()
        Else
            lblKode.ResetText()
            lblPanggil.ResetText()
            MsgBox("Data Tidak ada", MsgBoxStyle.Information, Me.Text)
        End If

        fn_Preview()
    End Sub
    Private Sub picReset_Click() Handles picReset.Click

    End Sub
#End Region
    Private Sub timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timer.Tick
        If grv.SelectedRowsCount < 1 Then
            fn_Preview()
        End If
    End Sub
    Private Sub chkIsAuto_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) 
        If chkIsAuto.Checked Then
            timer.Enabled = True
            timer.Start()
        Else
            timer.Enabled = False
            timer.Stop()
        End If
    End Sub
    Private Sub cboType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboType.SelectedIndexChanged
        lblKode.ResetText()
        lblPanggil.ResetText()

        fn_Preview()
    End Sub
End Class