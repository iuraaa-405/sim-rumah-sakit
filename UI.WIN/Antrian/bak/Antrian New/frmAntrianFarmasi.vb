Imports DataAccess

Public Class frmAntrianFarmasi
#Region "Declaration"
    Private oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrianFarmasi
    Private isLoad As Boolean
    Private sNOMORANTRIAN As String = String.Empty
#End Region
#Region "Function"
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
        sCode = String.Empty
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        sCode = sNOMORANTRIAN
    End Sub
    Private Overloads Sub Dispose()
        MyBase.Dispose()
        Me.Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
    Private Sub fn_ChangeFormState()
        'fn_LoadData()
    End Sub
    Private Function fn_Save(ByVal KODEANTRIAN As String) As Boolean
        Try
            'Dim estimasi As DateTime = DateTime.Parse(deDATEKONTROL.DateTime.ToString("yyyy-MM-dd") & " " & dsJawdwalDokter.BUKA)
            '.ESTIMASIDILAYANI = (estimasi.AddMinutes(oDoctor.GetData(grdKDDOCTOR.EditValue).ESTIMASI_MENIT * oDoctor.GetDataMonitoringTotal(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, deDATEKONTROL.DateTime)).ToString("yyyy-MM-dd HH:mm"))
            '.SISAKUOTAJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, deDATEKONTROL.DateTime) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_JKN - oDoctor.GetDataMonitoringSisaJKN(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, deDATEKONTROL.DateTime))
            '.KUOTAJKN = dsJawdwalDokter.KAPASITASPASIEN_JKN
            '.SISAKUOTANONJKN = IIf(dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, deDATEKONTROL.DateTime) < 0, 0, dsJawdwalDokter.KAPASITASPASIEN_NONJKN - oDoctor.GetDataMonitoringSisaNONJKN(grdKDDEPARTMENT.EditValue, grdKDDOCTOR.EditValue, deDATEKONTROL.DateTime))
            '.KUOTANONJKN = dsJawdwalDokter.KAPASITASPASIEN_NONJKN

            ' ***** HEADER *****

            Dim ds = oSet_Antrian_Simpan.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENJAMIN = "PENJAMIN_0000000001"
                .KODEBOOKING_FARMASI = ""
                .JENISPASIEN_RS = KODEANTRIAN
                .JENISPASIEN = "JKN"
                .NOMORKARTU = ""
                .NOHP = ""
                .NIK = ""
                .KODEPOLI = ""
                .NAMAPOLI = ""
                .PASIENBARU = 0
                .NORM = ""
                .TANGGALPERIKSA = Now
                .TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy")
                .KODEDOKTER = ""
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
                .ISPANGGIL = 1
                .ISONLINE = False
                .KETERANGAN = "OFLINE"
                .KDSKD = ""
            End With

            Try
                sNOMORANTRIAN = oSet_Antrian_Simpan.InsertData(ds)

                If sNOMORANTRIAN = "" Then
                    fn_Save = False
                Else
                    fn_Save = True
                    fn_PrintStruk7(sNOMORANTRIAN)
                End If

            Catch ex As Exception
                MsgBox("Simpan Data : " & vbCrLf & ex.Message, MsgBoxStyle.Exclamation, Me.Text)
            End Try

        Catch oErr As Exception
            MsgBox("Simpan Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
            fn_Save = False
        End Try
    End Function
    Private Function fn_PrintStruk7(ByVal sCode As String) As Boolean
        Try
            Dim rpt As New xtraAntrianFarmasi
            Dim ds = oSet_Antrian_Simpan.GetData(sCode)
            rpt.BindingSource.DataSource = ds
            Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            printTool.Print()
        Catch oErr As Exception
            MsgBox("Print Data : " & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Function
    Private Sub LabelControl2_Click(sender As Object, e As EventArgs) Handles LabelControl2.Click
        fn_Save(lblA.Text)
        Me.Close()
    End Sub
    Private Sub LabelControl6_Click(sender As Object, e As EventArgs) Handles LabelControl6.Click
        fn_Save(lblB.Text)
        Me.Close()
    End Sub
    Private Sub LabelControl4_Click(sender As Object, e As EventArgs) Handles LabelControl4.Click
        fn_Save(lblC.Text)
        Me.Close()
    End Sub
    Private Sub picConfirm_Click(sender As Object, e As EventArgs) Handles picConfirm.Click
        Me.Close()
    End Sub
#End Region
End Class