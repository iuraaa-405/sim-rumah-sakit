Imports DataAccess
Imports UI.WIN.MAIN.My.Resources

Public Class frmAntrianOflineNew
#Region "Declaration"
    Private oSet_Antrian_Simpan As New SettingAntrian.clsSetAntrian
    Private isLoad As Boolean
#End Region
#Region "Function"
    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        fn_ChangeFormState()
        isLoad = True
    End Sub
    Private Sub Form_Closing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

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
            ' ***** HEADER *****

            Dim ds = oSet_Antrian_Simpan.GetStructureHeader
            With ds
                .DATECREATED = Now
                .DATEUPDATED = Now
                .KDPENJAMIN = IIf(KODEANTRIAN = "C", "PENJAMIN_0000000001", "PENJAMIN_0000000004")
                .KODEBOOKING = ""
                .JENISPASIEN_RS = KODEANTRIAN
                .JENISPASIEN = IIf(KODEANTRIAN = "C", "NON JKN", "JKN")
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
                    Dim dsSisaCek = oSet_Antrian_Simpan.GetDataSisaByKode(KODEANTRIAN)

                    If dsSisaCek Is Nothing Then
                        Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                        With dsSisa
                            .KODE = KODEANTRIAN
                            .SISA = 1
                            .DESCRIPTION = sKDBOOKINGANTREAN
                            sBelumPanggil = .SISA
                        End With

                        oSet_Antrian_Simpan.InsertDataSisa(dsSisa)

                    Else
                        Dim dsSisa = oSet_Antrian_Simpan.GetStructureHeaderSisa
                        With dsSisa
                            .KODE = KODEANTRIAN
                            .SISA = oSet_Antrian_Simpan.GetDataBelumPanggil(Now, KODEANTRIAN)
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
    Private Sub LabelControl1_Click(sender As Object, e As EventArgs) Handles LabelControl1.Click
        Me.Close()
    End Sub
    Private Sub btnDINAS_Click(sender As Object, e As EventArgs) Handles btnDINAS.Click
        fn_Save(lblA.Text)
    End Sub
    Private Sub btnBPJS_Click(sender As Object, e As EventArgs) Handles btnBPJS.Click
        fn_Save(lblB.Text)
    End Sub
    Private Sub btnUMUM_Click(sender As Object, e As EventArgs) Handles btnUMUM.Click
        fn_Save(lblC.Text)
    End Sub
#End Region
End Class