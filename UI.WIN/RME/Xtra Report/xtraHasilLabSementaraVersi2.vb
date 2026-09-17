Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing
'Imports ZXing
Imports DevExpress.XtraPrinting

Public Class xtraHasilLabSementaraVersi2
    Private Sub xtraHasilLabSementara_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox2.Image = sPictureLogo
        End If

        lblJUDUL1.Text = sJudulLab1
        lblJUDUL2.Text = sJudulLab2
        lblJUDUL3.Text = sJudulLab3
        lblALAMAT.Text = sJudulLab4

        Try
            Dim oHasilLab As New Grouper.clsHasiLab
            Dim oOrder As New Order.clsOrderRanapLab
            Dim diagnosa As String = ""

            Dim value1 As Object = GetCurrentColumnValue("KDSOTRANSAKSI")
            Dim value2 As Object = GetCurrentColumnValue("KDORDER")

            If value1 IsNot Nothing Then
                Dim ds = oHasilLab.GetDataDetailTransaksiFirst(value1.ToString())

                If ds IsNot Nothing Then
                    Dim oUStaff As New Reference.clsStaff
                    Dim dsStaff = oUStaff.GetDataByNoidUser(ds.KDUSER)
                    If dsStaff IsNot Nothing Then
                        XrLabel11.Text = dsStaff.NOMOR_NIP
                    Else
                        XrLabel11.Text = "-"
                    End If

                    lblTanggalLahirUsia.Text = ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR.ToString("yyyy-MM-dd") & " / " & sUSIA
                    diagnosa = ds.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.M_DIAGNOSA.MEMO

                    lblTanggal.Text = ds.S_SO_TRANSAKSI_H.DATE.ToString("dd MMMM yyyy")
                    lblJam.Text = ds.S_SO_TRANSAKSI_H.DATE.ToString("HH:mm")
                End If
            End If

            Dim diganosacek As Boolean = False

            If value2 IsNot Nothing Then
                Dim dsOrder = oOrder.GetData(value2.ToString())
                If dsOrder IsNot Nothing Then
                    lblDiagnosa.Text = dsOrder.R_ORDER_LAINNYA.MEMO1
                    If dsOrder.R_ORDER_LAINNYA.MEMO1 <> "" Then
                        diganosacek = True
                    End If
                End If
            End If
            If diganosacek = False Then
                lblDiagnosa.Text = diagnosa
            End If
        Catch ex As Exception

        End Try

        Try
            ' Buat objek watermark baru
            'Dim watermark As New PageWatermark()

            ' Set gambar watermark dari file
            Watermark.Image = sPictureLogo

            ' Opsional: Atur transparansi (0-255, 255 = solid)
            Watermark.ImageTransparency = 150

            ' Opsional: Atur posisi (Center, Stretch, Tile)
            'watermark.ImageViewMode = ImageViewMode.Center

            '' Terapkan watermark ke dokumen
            'printingSystem.Watermarks.Add(watermark)

            '' Untuk menerapkan ke semua halaman
            'For Each page As Page In printingSystem.Pages
            '    page.WatermarkId = watermark.Id
            'Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Detail_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        'Dim value1 As Object = GetCurrentColumnValue("WARNA")
        'Dim value2 As Object = GetCurrentColumnValue("APPROVE")
        'Dim value3 As Object = GetCurrentColumnValue("HASIL")

        'If value1 IsNot Nothing Then
        '    If value1.ToString() = "M" Then
        '        lblHasil.ForeColor = Color.Red
        '    Else
        '        lblHasil.ForeColor = Color.Black
        '    End If
        'End If

        'If value2 IsNot Nothing Then
        '    If CBool(value2.ToString()) = True Then
        '        XrTableCell48.Font = New Font("Arial", 10, FontStyle.Bold Or FontStyle.Underline)
        '    Else
        '        XrTableCell48.Font = New Font("Arial", 10)
        '    End If
        'End If
    End Sub
End Class