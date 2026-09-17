Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq

Public Class frmAntrianDepanManual
#Region "Declaration"
    Private oFormMode As FORM_MODE = FORM_MODE.FORM_MODE_VIEW
    Private oAntrian As New AntrianRS.clsAntrian
    Private sNoid As String = String.Empty
    Private sBtnActive As Boolean = False
    Private sKDDEPARTMENT As String = String.Empty
    Private sKDDOCTOR As String = String.Empty
    Private sJAMPRAKTEK As String = String.Empty
    Private sESTIMASIDILAYANI As DateTime = Now
    Private sSISAKUOTAJKN As Integer = 0
    Private sKUOTAJKN As Integer = 0
    Private sSISAKUOTANONJKN As Integer = 0
    Private sKUOTANONJKN As Integer = 0
#End Region
#Region "Function"
    Public Sub LoadMe(ByVal FormMode As String, Optional ByVal NoId As String = "")
        oFormMode = FormMode
        sNoid = NoId
    End Sub
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        fn_LoadLanguage()
    End Sub
    Public Sub fn_LoadLanguage()
        Try
            Me.Text = "Antrean"
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Me.Dispose()
    End Sub
    Private Sub fn_ChangeFormState()
        fn_LoadDepartment()

        Select Case oFormMode
            Case FORM_MODE.FORM_MODE_VIEW
                fn_ViewMode(True)
                fn_LoadData()
            Case FORM_MODE.FORM_MODE_ADD
                fn_ViewMode(False)
                fn_EmptyMe()
            Case FORM_MODE.FORM_MODE_EDIT
                fn_ViewMode(False)
                fn_LoadData()
            Case Else
                fn_ViewMode(True)
        End Select
    End Sub
    Private Sub fn_ViewMode(ByVal Status As Boolean)
        'btnSaveNew.Enabled = Not Status
        'btnSaveClose.Enabled = Not Status

        'grdKDDOCTOR.Properties.ReadOnly = Status
        'grdKDDEPARTMENT.Properties.ReadOnly = Status
        'txtMEMO.Properties.ReadOnly = Status

        'grvDetail.OptionsBehavior.ReadOnly = Status

    End Sub
    Private Sub fn_EmptyMe()
        sBtnActive = False
        sKDDEPARTMENT = String.Empty
        sKDDOCTOR = String.Empty
        sJAMPRAKTEK = String.Empty
        sESTIMASIDILAYANI = Now
        sSISAKUOTAJKN = 0
        sKUOTAJKN = 0
        sSISAKUOTANONJKN = 0
        sKUOTANONJKN = 0
        lPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lDOKTER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lPILIHULANGPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
    End Sub
    Private Sub fn_LoadData()
        'Try
        '    ' ***** HEADER *****
        '    Dim ds = oAntrian.GetData(sNoId)

        '    With ds
        '        txtKDAntrian.Text = .KDAntrian
        '        grdKDDOCTOR.Text = .KDDOCTOR
        '        grdKDDEPARTMENT.Text = .KDDEPARTMENT
        '        txtMEMO.Text = .DESCRIPTION

        '        BindingSource.DataSource = oAntrian.GetDataDetail.Where(Function(x) x.KDAntrian = sNoId).OrderBy(Function(x) x.SEQ).ToList()
        '        grdDetail.DataSource = BindingSource
        '    End With
        'Catch oErr As Exception
        '    MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        'End Try
    End Sub
    Private Sub CetakAntrian(ByVal KODEBOOKING As String)
        Try
            If KODEBOOKING = String.Empty Then Exit Sub

            Dim rpt As New xtraAntrian

            rpt.ShowPrintMarginsWarning = False
            rpt.Watermark.Text = sWATERMARK
            Dim ds = oAntrian.GetData(KODEBOOKING)
            rpt.bindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)

            printTool.Print()

            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Function fn_Validate() As Boolean
        Try
            fn_Validate = True

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Function fn_Save(ByVal sJENISPASIEN_RS As String) As Boolean
        Try
            ' ***** HEADER *****
            'Dim oCustomer As New Reference.clsCustomer
            Dim ds = oAntrian.GetStructureHeader
            With ds
                Try
                    .DATECREATED = oAntrian.GetData(sNoid).DATECREATED
                Catch ex As Exception
                    .DATECREATED = Now
                End Try
                .DATEUPDATED = Now
                .KODEBOOKING = sNoid
                .JENISPASIEN_RS = sJENISPASIEN_RS
                .JENISPASIEN = IIf(sJENISPASIEN_RS = "C", "NON JKN", "JKN")
                .NOMORKARTU = String.Empty
                .NOHP = String.Empty
                .NIK = String.Empty
                .KDDEPARTMENT = sKDDEPARTMENT
                .PASIENBARU = 100
                .NORM = String.Empty
                .TANGGALPERIKSA = Now
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KDDOCTOR = sKDDOCTOR
                .JENISKUNJUNGAN = 100
                .NOMORREFERENSI = String.Empty
                Try
                    .NOMORANTREAN = oAntrian.GetData(sNoid).NOMORANTREAN
                Catch ex As Exception
                    .NOMORANTREAN = ""
                End Try
                Try
                    .ANGKAANTREAN = oAntrian.GetData(sNoid).ANGKAANTREAN
                Catch ex As Exception
                    .ANGKAANTREAN = 0
                End Try
                .JAMPRAKTEK = sJAMPRAKTEK
                .ESTIMASIDILAYANI = sESTIMASIDILAYANI
                .SISAKUOTAJKN = sSISAKUOTAJKN
                .KUOTAJKN = sKUOTAJKN
                .SISAKUOTANONJKN = sSISAKUOTANONJKN
                .KUOTANONJKN = sKUOTANONJKN
                .KETERANGAN = "Peserta harap 60 menit awal guna pencatatan administrasi"

                Try
                    .ISPANGGIL = oAntrian.GetData(sNoid).ISPANGGIL
                Catch ex As Exception
                    .ISPANGGIL = 0
                End Try
                Try
                    .ISONLINE = oAntrian.GetData(sNoid).ISONLINE
                Catch ex As Exception
                    .ISONLINE = False
                End Try
                Try
                    .KDSKD = oAntrian.GetData(sNoid).KDSKD
                Catch ex As Exception
                    .KDSKD = ""
                End Try
            End With

            If oFormMode = FORM_MODE.FORM_MODE_ADD Then
                Dim KODEBOOKING As String = oAntrian.InsertData(ds, True)
                If KODEBOOKING <> "" Then
                    fn_Save = True
                    CetakAntrian(KODEBOOKING)
                Else
                    fn_Save = False
                End If
            ElseIf oFormMode = FORM_MODE.FORM_MODE_EDIT Then
                fn_Save = oAntrian.UpdateData(ds)
            End If
        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
#End Region
#Region "Grid Method"

#End Region
#Region "Command Button"
    Private Sub frmAntrian_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Select Case e.KeyCode
            Case Keys.F12
                Me.Close()
                'Case Keys.F2
                '    If btnSaveNew.Enabled = True Then
                '        btnSaveNew_Click()
                '    End If
                'Case Keys.F3
                '    If btnSaveClose.Enabled = True Then
                '        btnSaveClose_Click()
                '    End If
        End Select
    End Sub
    Private Sub btnDinas_Click(sender As Object, e As EventArgs) Handles btnDinas.Click
        If sBtnActive = False Then
            MsgBox("Silahkan Pilih Dokter", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sSISAKUOTAJKN = 0 Then
                MsgBox("Kapasitas Sudah 0, Tidak dapat cetak antrian", MsgBoxStyle.Exclamation, Me.Text)
            Else
                If fn_Save("A") = True Then
                    Me.Close()
                Else
                    fn_EmptyMe()
                End If
            End If
        End If
    End Sub
    Private Sub btnNonDinas_Click(sender As Object, e As EventArgs) Handles btnNonDinas.Click
        If sBtnActive = False Then
            MsgBox("Silahkan Pilih Dokter", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sSISAKUOTAJKN = 0 Then
                MsgBox("Kapasitas Sudah 0, Tidak dapat cetak antrian", MsgBoxStyle.Exclamation, Me.Text)
            Else
                If fn_Save("B") = True Then
                    Me.Close()
                Else
                    fn_EmptyMe()
                End If
            End If
        End If
    End Sub
    Private Sub btnUmum_Click(sender As Object, e As EventArgs) Handles btnUmum.Click
        If sBtnActive = False Then
            MsgBox("Silahkan Pilih Dokter", MsgBoxStyle.Exclamation, Me.Text)
        Else
            If sSISAKUOTANONJKN = 0 Then
                MsgBox("Kapasitas Sudah 0, Tidak dapat cetak antrian", MsgBoxStyle.Exclamation, Me.Text)
            Else
                If fn_Save("C") = True Then
                    Me.Close()
                Else
                    fn_EmptyMe()
                End If
            End If
        End If
    End Sub
#End Region
#Region "Lookup / Event"
    Private Sub grvPoli_FocusedRowChanged(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles grvPoli.FocusedRowChanged

    End Sub
    Private Sub RepositoryItemButtonEdit1_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit1.ButtonClick
        If grvPoli.GetFocusedRowCellValue("KDDEPARTMENT") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        sKDDEPARTMENT = grvPoli.GetFocusedRowCellValue("KDDEPARTMENT")

        lPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lDOKTER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPILIHULANGPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        fn_LoadDoctor(grvPoli.GetFocusedRowCellValue("KDDEPARTMENT"))
    End Sub
    Private Sub RepositoryItemButtonEdit2_ButtonClick(sender As Object, e As DevExpress.XtraEditors.Controls.ButtonPressedEventArgs) Handles RepositoryItemButtonEdit2.ButtonClick
        If grvDokter.GetFocusedRowCellValue("KDDOCTOR") Is Nothing Then
            fn_EmptyMe()
            Exit Sub
        End If

        lButton.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always
        lPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lDOKTER.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        lPILIHULANGPOLI.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never
        sKDDOCTOR = grvDokter.GetFocusedRowCellValue("KDDOCTOR")
        sBtnActive = True

        fn_LoadText(grvDokter.GetFocusedRowCellValue("KDJADWALDOKTER"), grvDokter.GetFocusedRowCellValue("SEQ"))

    End Sub
    Private Sub SimpleButton0_Click(sender As Object, e As EventArgs) Handles SimpleButton0.Click
        fn_EmptyMe()
    End Sub
    Private Sub fn_LoadText(ByVal KDJADWAL As String, ByVal SEQ As Integer)
        Dim oJadwalDokter As New Inventory.clsJadwalDokter
        Dim dsJawdwalDokter = oJadwalDokter.GetDataDetail(KDJADWAL, SEQ)

        If dsJawdwalDokter IsNot Nothing Then
            sJAMPRAKTEK = dsJawdwalDokter.BUKA & "-" & dsJawdwalDokter.TUTUP
            sSISAKUOTAJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_JKN - oJadwalDokter.GetDataMonitoringSisaJKN(sKDDEPARTMENT, sKDDOCTOR) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_JKN - oJadwalDokter.GetDataMonitoringSisaJKN(sKDDEPARTMENT, sKDDOCTOR))
            sKUOTAJKN = dsJawdwalDokter.KAPASITASPASIEN_JKN
            sSISAKUOTANONJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oJadwalDokter.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT, sKDDOCTOR) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oJadwalDokter.GetDataMonitoringSisaNONJKN(sKDDEPARTMENT, sKDDOCTOR))
            sKUOTANONJKN = dsJawdwalDokter.KAPASITASPASIEN_NONJKN
            sESTIMASIDILAYANI = Now
            btnDinas.Text = "DINAS" & vbCrLf & "Sisa " & sSISAKUOTAJKN & " Dari " & sKUOTAJKN
            btnNonDinas.Text = "NON DINAS (JKN)" & vbCrLf & "Sisa " & sSISAKUOTAJKN & " Dari " & sKUOTAJKN
            btnUmum.Text = "UMUM" & vbCrLf & "Sisa " & sSISAKUOTANONJKN & " Dari " & sKUOTANONJKN

        Else
            fn_EmptyMe()
        End If
    End Sub
    Private Sub fn_LoadDepartment()
        Try
            Dim oDepartment As New Reference.clsDepartment

            Dim ds = From x In oDepartment.GetData
                     Where x.ISACTIVE = True And x.ANTRIAN <> ""
                     Select x.KDDEPARTMENT, Poli = x.NAME_DISPLAY, x.ANTRIAN
                     Order By ANTRIAN Ascending

            grdPOLI.DataSource = ds.ToList()

            grvPoli.Columns("KDDEPARTMENT").Visible = False
            grvPoli.BestFitColumns()
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub fn_LoadDoctor(ByVal Parameter As String)
        Try
            Dim Contoh As Date = Now
            Dim Hasil As String = ""

            Select Case Weekday(Contoh)
                Case 1
                    Hasil = "Minggu"
                Case 2
                    Hasil = "Senin"
                Case 3
                    Hasil = "Selasa"
                Case 4
                    Hasil = "Rabu"
                Case 5
                    Hasil = "Kamis"
                Case 6
                    Hasil = "Jumat"
                Case 7
                    Hasil = "Sabtu"
            End Select

            Dim oJadwal As New Inventory.clsJadwalDokter

            Dim ds = From x In oJadwal.GetData()
                     Join y In oJadwal.GetDataDetail()
                     On x.KDJADWALDOKTER Equals y.KDJADWALDOKTER
                     Where x.M_DOCTOR.ISACTIVE = True And x.KDDEPARTMENT = Parameter And y.HARI = Hasil And y.LIBUR = False
                     Select y.KDJADWALDOKTER, y.SEQ, x.KDDOCTOR, NAME_DISPLAY = x.M_DOCTOR.NAME_DISPLAY
                     Order By NAME_DISPLAY Ascending

            grdDokter.DataSource = ds.ToList()

            grvDokter.BestFitColumns()
        Catch oErr As Exception
            MsgBox("Preview Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub btnBatal_Click(sender As Object, e As EventArgs) Handles btnBatal.Click
        Me.Close()
    End Sub
    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click
        Me.Close()
    End Sub
#End Region

End Class