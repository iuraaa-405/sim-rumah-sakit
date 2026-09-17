Imports DataAccess
Imports DevExpress.XtraReports.UI
Imports System.Drawing
'Imports ZXing

Public Class xtraHasilLabSementara
    Private Sub xtraHasilLabSementara_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Me.BeforePrint
        If sPictureLogo IsNot Nothing Then
            XrPictureBox2.Image = sPictureLogo
        End If

        lNPWP.Text = sCompany
        lbl1.Text = sAddress
        lbl2.Text = sPhone

        lblUSIA.Text = sUSIA

        'Dim value1 As Object = GetCurrentColumnValue("KDDOCTOR")

        'If value1 IsNot Nothing Then
        '    Try
        '        Dim oDoctor As New Reference.clsDoctor

        '        Dim dsDoctor = oDoctor.GetData(value1.ToString())
        '        If dsDoctor IsNot Nothing Then
        '            If dsDoctor.KODETTD <> "" Then
        '                Dim Alamat As String = sALAMATTTD & dsDoctor.KODETTD & ".jpg"
        '                If FileIO.FileSystem.FileExists(Alamat) Then
        '                    XrPictureBox1.Image = Image.FromFile(Alamat)
        '                End If
        '            End If
        '        End If
        '    Catch ex As Exception
        '        'MsgBox("Alamat Tidak Ada", MsgBoxStyle.Exclamation, Me.Text)
        '    End Try

        '    lblUSIA.Text = sUSIA
        'End If

        'If sSIPNIP <> "" Then
        '    Try
        '        Dim writer As New BarcodeWriter()
        '        writer.Format = BarcodeFormat.DATA_MATRIX ' atau .PDF_417 / .AZTEC

        '        Dim result = writer.Write(sSIPNIP)
        '        XrPictureBox1.Image = result
        '    Catch ex As Exception

        '    End Try
        'End If
        Try
            Dim oHasilLab As New Grouper.clsHasiLab
            Dim value1 As Object = GetCurrentColumnValue("KDSOTRANSAKSI")

            If value1 IsNot Nothing Then
                Dim ds = oHasilLab.GetDataDetailTransaksiFirst(value1.ToString())

                If ds IsNot Nothing Then
                    Dim oUStaff As New Reference.clsStaff
                    Dim dsStaff = oUStaff.GetDataByNoidUser(ds.KDUSER)
                    If dsStaff IsNot Nothing Then
                        XrLabel11.Text = "STR " & dsStaff.NOMOR_NIP
                    Else
                        XrLabel11.Text = "STR"
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
    Private Sub Detail_BeforePrint(sender As Object, e As System.Drawing.Printing.PrintEventArgs) Handles Detail.BeforePrint
        Dim value1 As Object = GetCurrentColumnValue("WARNA")
        Dim value2 As Object = GetCurrentColumnValue("APPROVE")
        Dim value3 As Object = GetCurrentColumnValue("HASIL")

        If value1 IsNot Nothing Then
            If value1.ToString() = "M" Then
                lblHasil.ForeColor = Color.Red
            Else
                lblHasil.ForeColor = Color.Black
            End If
        End If

        If value2 IsNot Nothing Then
            If CBool(value2.ToString()) = True Then
                XrTableCell48.Font = New Font("Arial", 10, FontStyle.Bold Or FontStyle.Underline)
            Else
                XrTableCell48.Font = New Font("Arial", 10)
            End If
        End If
    End Sub
End Class