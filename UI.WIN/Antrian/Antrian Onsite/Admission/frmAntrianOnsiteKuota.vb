Imports UI.WIN.MAIN.My.Resources
Imports System.Data.SqlClient
Imports DataAccess
Imports MySql.Data.MySqlClient

Public Class frmAntrianOnsiteKuota
    Private oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
    Private oDepartment As New Reference.clsDepartment
    Private sKode As String = ""
    Private sLanjut As Boolean = False

    Private Sub frmLogin_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        SimpleButton1.Text = sCompany & vbCrLf & sAddress
        SimpleButton1.Focus()
    End Sub
    Private Function fn_Save(ByVal Penjamin As String, ByVal KDDPERTMENT As String, ByVal KDDOCTOR As String) As Boolean
        Try
            Dim dsdepartment = oDepartment.GetData(KDDPERTMENT)
            If dsdepartment IsNot Nothing Then
                ' ***** HEADER *****
                Dim ds = oSet_Antrian_Simpan.GetStructureHeader
                With ds
                    .DATECREATED = Now
                    .DATEUPDATED = Now
                    .KDPENJAMIN = IIf(Penjamin = "UMUM", "PENJAMIN_0000000001", "PENJAMIN_0000000004")
                    .KODEBOOKING = ""
                    .JENISPASIEN_RS = IIf(Penjamin = "UMUM", "A", "B")
                    .JENISPASIEN = IIf(Penjamin = "UMUM", "NON JKN", "JKN")
                    .NOMORKARTU = ""
                    .NOHP = ""
                    .NIK = ""
                    '.KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                    '.NAMAPOLI = dsdepartment.NAME_DISPLAY
                    .KODEPOLI = dsdepartment.VCLAIM_KODEPOLI
                    .NAMAPOLI = dsdepartment.NAME_DISPLAY
                    .PASIENBARU = 0
                    .NORM = ""
                    .TANGGALPERIKSA = Now
                    .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                    .KODEDOKTER = KDDOCTOR
                    .NAMADOKTER = ""
                    .JAMPRAKTEK = ""
                    .JENISKUNJUNGAN = 10
                    .NOMORREFERENSI = ""
                    .NOMORANTREAN = ""
                    .ANGKAANTREAN = 0
                    .ESTIMASIDILAYANI = Now.ToString("yyyy-MM-dd") & "00:00"
                    .SISAKUOTAJKN = 0
                    .KUOTAJKN = 0
                    .SISAKUOTANONJKN = 0
                    .SISAKUOTANONJKN = 0
                    .ISPANGGIL = 0
                    .ISONLINE = False
                    .KETERANGAN = "OFLINE"
                    .KDSKD = ""
                End With

                Dim sKDBOOKINGANTREAN As String = ""

                Try
                    sKDBOOKINGANTREAN = oSet_Antrian_Simpan.InsertData(ds)

                    If sKDBOOKINGANTREAN = "" Then
                        fn_Save = False
                    Else
                        fn_Save = True
                    End If

                Catch ex As Exception
                    MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
                End Try

                If fn_Save = True Then
                    Try
                        Dim dsSisaCek = oSet_Antrian_Simpan.GetDataSisaByKode(ds.JENISPASIEN_RS)

                        If dsSisaCek Is Nothing Then
                            Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                            With dsSisa
                                .KODE = ds.JENISPASIEN_RS
                                .SISA = 1
                                .DESCRIPTION = sKDBOOKINGANTREAN
                                sBelumPanggil = .SISA
                            End With

                            oSet_Antrian_Simpan.InsertDataSisa(dsSisa)

                        Else
                            Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                            With dsSisa
                                .KODE = ds.JENISPASIEN_RS
                                .SISA = oSet_Antrian_Simpan.GetDataBelumPanggil(Now, ds.JENISPASIEN_RS)
                                .DESCRIPTION = sKDBOOKINGANTREAN
                                sBelumPanggil = .SISA
                            End With

                            oSet_Antrian_Simpan.UpdateDataSisa(dsSisa)

                        End If

                    Catch oErr As Exception
                        MsgBox("Simpan Data Sisa: " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
                    End Try

                    fn_PrintStruk7(sKDBOOKINGANTREAN)

                End If
            Else
                MsgBox("poli Tidak ditemukan", MsgBoxStyle.Exclamation, Me.Text)
            End If
        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean
        Try
            Dim rpt As New xtraAntrianManual
            Dim ds = oSet_Antrian_Simpan.GetData(sCode)
            rpt.BindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub Query_LoaDataKlinik(ByVal JKN As Boolean)
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

            Dim sHARI As String = String.Empty

            Select Case Weekday(Now)
                Case 1
                    sHARI = "Minggu"
                Case 2
                    sHARI = "Senin"
                Case 3
                    sHARI = "Selasa"
                Case 4
                    sHARI = "Rabu"
                Case 5
                    sHARI = "Kamis"
                Case 6
                    sHARI = "Jumat"
                Case 7
                    sHARI = "Sabtu"
            End Select


            SQL = "SELECT "
            SQL &= "X.KDDEPARTMENT "
            SQL &= ",NAME_DISPLAY = X.NAME_DISPLAY + ' (' + convert(nvarchar(50), SUM(X.TOTAL)) + ')' "
            SQL &= ",TOTAL = SUM(X.TOTAL) "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDDEPARTMENT "
            SQL &= ",D.NOMOR "
            SQL &= ",NAME_DISPLAY = D.NAME_DISPLAY "
            If JKN = True Then
                SQL &= ",TOTAL = C.KAPASITASPASIEN_JKN - ISNULL((SELECT COUNT(KODEBOOKING) FROM SET_BOOKING_ANTRIAN WHERE KODEPOLI = D.VCLAIM_KODEPOLI AND CONVERT(VARCHAR(8), TANGGALPERIKSA, 112) = '" & Now.ToString("yyyyMMdd") & "' AND ISPANGGIL <> 99 AND JENISPASIEN = 'JKN'), 0) "
            Else
                SQL &= ",TOTAL = C.KAPASITASPASIEN_NONJKN - ISNULL((SELECT COUNT(KODEBOOKING) FROM SET_BOOKING_ANTRIAN WHERE KODEPOLI = D.VCLAIM_KODEPOLI And CONVERT(VARCHAR(8), TANGGALPERIKSA, 112) = '" & Now.ToString("yyyyMMdd") & "' AND ISPANGGIL <> 99 AND JENISPASIEN <> 'JKN'), 0) "
            End If
            SQL &= "FROM M_DOCTOR A "
            SQL &= "INNER JOIN M_DOCTOR_JADWAL C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT D "
            SQL &= "ON A.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= "WHERE C.HARI = '" & sHARI & "' "
            SQL &= "AND D.NOMOR <> 0 "
            SQL &= ") X "
            SQL &= "WHERE X.TOTAL > 0 "
            SQL &= "GROUP BY X.KDDEPARTMENT, X.NAME_DISPLAY, X.NOMOR  "
            SQL &= "ORDER BY X.NOMOR "


            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DEPARTMENT")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd1.MainView = grv1
            grd1.DataSource = ds.Tables("M_DEPARTMENT")
            grd1.ForceInitialize()

            'grv1.Columns("KDDEPARTMENT").VisibleIndex = -1
            'grv1.Columns("TOTAL").VisibleIndex = -1
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Query_LoaDataDokter(ByVal KDDEPARTMENT As String, ByVal JKN As Boolean)
        Try
            If sKode = "" Then Exit Sub

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

            Dim sHARI As String = String.Empty

            Select Case Weekday(Now)
                Case 1
                    sHARI = "Minggu"
                Case 2
                    sHARI = "Senin"
                Case 3
                    sHARI = "Selasa"
                Case 4
                    sHARI = "Rabu"
                Case 5
                    sHARI = "Kamis"
                Case 6
                    sHARI = "Jumat"
                Case 7
                    sHARI = "Sabtu"
            End Select

            SQL = "SELECT "
            SQL &= "X.KDDEPARTMENT "
            SQL &= ",X.KDDOCTOR "
            SQL &= ",NAME_DISPLAY = X.NAME_DISPLAY + ' (' + convert(nvarchar(50), X.TOTAL) + ')' "
            SQL &= ",TOTAL = X.TOTAL "
            SQL &= "FROM ( "
            SQL &= "SELECT "
            SQL &= "A.KDDEPARTMENT "
            SQL &= ",A.KDDOCTOR "
            SQL &= ",D.NOMOR "
            SQL &= ",NAME_DISPLAY = A.NAME_DISPLAY "
            If JKN = True Then
                SQL &= ",TOTAL = C.KAPASITASPASIEN_JKN - ISNULL((SELECT COUNT(KODEBOOKING) FROM SET_BOOKING_ANTRIAN WHERE KODEPOLI = D.VCLAIM_KODEPOLI AND CONVERT(VARCHAR(8), TANGGALPERIKSA, 112) = '" & Now.ToString("yyyyMMdd") & "' AND ISPANGGIL <> 99 AND JENISPASIEN = 'JKN'), 0) "
            Else
                SQL &= ",TOTAL = C.KAPASITASPASIEN_NONJKN - ISNULL((SELECT COUNT(KODEBOOKING) FROM SET_BOOKING_ANTRIAN WHERE KODEPOLI = D.VCLAIM_KODEPOLI And CONVERT(VARCHAR(8), TANGGALPERIKSA, 112) = '" & Now.ToString("yyyyMMdd") & "' AND ISPANGGIL <> 99 AND JENISPASIEN <> 'JKN'), 0) "
            End If
            SQL &= "FROM M_DOCTOR A "
            SQL &= "INNER JOIN M_DOCTOR_JADWAL C "
            SQL &= "ON A.KDDOCTOR = C.KDDOCTOR "
            SQL &= "INNER JOIN M_DEPARTMENT D "
            SQL &= "ON A.KDDEPARTMENT = D.KDDEPARTMENT "
            SQL &= "WHERE C.HARI = '" & sHARI & "' "
            SQL &= "AND D.NOMOR <> 0 "
            SQL &= "AND A.KDDEPARTMENT = '" & KDDEPARTMENT & "' "
            SQL &= ") X "
            SQL &= "WHERE X.TOTAL > 0 "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "M_DOCTOR")

            If oConn.State = ConnectionState.Open Then
                oConn.Close()
            End If

            grd1.MainView = grv1
            grd1.DataSource = ds.Tables("M_DOCTOR")
            grd1.ForceInitialize()
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnBATAL_Click(sender As Object, e As EventArgs) Handles btnBATAL.Click
        Me.Close()
    End Sub
    Private Sub grv1Click_Click(sender As Object, e As EventArgs) Handles grv1.Click
        If sLanjut = False Then
            If grv1.GetFocusedRowCellValue("KDDEPARTMENT") Is Nothing Then
                Exit Sub
            End If

            sLanjut = True
            Query_LoaDataDokter(grv1.GetFocusedRowCellValue("KDDEPARTMENT"), IIf(sKode = "BPJS", True, False))
        Else
            sLanjut = False

            If grv1.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
                Exit Sub
            Else
                If sKode = "" Then
                    MsgBox("Kode Masih Kosong!!!", MsgBoxStyle.Exclamation, Me.Text)
                Else
                    fn_Save(sKode, grv1.GetFocusedRowCellValue("KDDEPARTMENT"), grv1.GetFocusedRowCellValue("KDDOCTOR"))
                End If
            End If
        End If
    End Sub
    Private Sub btnBPJSKESEHATAN_Click(sender As Object, e As EventArgs) Handles btnBPJSKESEHATAN.Click
        sKode = "BPJS"

        btnBPJSKESEHATAN.Appearance.BackColor = Color.Lime
        btnUMUM.Appearance.BackColor = Color.Transparent

        Query_LoaDataKlinik(True)
    End Sub
    Private Sub btnUMUM_Click(sender As Object, e As EventArgs) Handles btnUMUM.Click
        sKode = "UMUM"

        btnBPJSKESEHATAN.Appearance.BackColor = Color.Transparent
        btnUMUM.Appearance.BackColor = Color.Lime

        Query_LoaDataKlinik(False)
    End Sub
End Class