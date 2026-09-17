Imports DataAccess
Imports UI.WIN.MAIN.My.Resources
Imports System.Linq
Imports System.Data.SqlClient

Public Class PDF
    Public Sub LoadMe(ByVal sKDPENDAFTARAN As String)
        Try
            If Not IO.Directory.Exists("C:/SIMRS/TEMPLATE") Then
                IO.Directory.CreateDirectory("C:/SIMRS/TEMPLATE")
            End If

            PdfViewer1.CloseDocument()

            Dim oPendaftaran As New Admission.clsPendaftaran
            Dim dsPendaftaran = oPendaftaran.GetData(sKDPENDAFTARAN)
            Dim TanggalPulang As DateTime = Now

            If dsPendaftaran Is Nothing Then
                Exit Sub
            Else
                Dim oPulang As New Admission.clsUpdate_Tanggal_Pulang
                Dim dsPulang = oPulang.GetDatabyKD(sKDPENDAFTARAN)
                If dsPulang IsNot Nothing Then
                    TanggalPulang = dsPulang.DATE
                End If
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

            SQL = "SELECT "
            SQL &= "KDCASHIN = '" & sKDPENDAFTARAN & "' "
            SQL &= ",CATEGORY = '" & dsPendaftaran.CATEGORY & "' "
            SQL &= ",D.KDPENDAFTARAN "
            SQL &= ",C.KDKUNJUNGAN "
            SQL &= ",D.KDCUSTOMER "
            SQL &= ",TUJUAN = '" & dsPendaftaran.M_DEPARTMENT.NAME_DISPLAY & "' "
            SQL &= ",DPJP = '" & dsPendaftaran.M_DOCTOR.NAME_DISPLAY & "' "
            SQL &= ",PASIEN = '" & dsPendaftaran.M_CUSTOMER.NAME_DISPLAY & "' "
            SQL &= ",ALAMAT = '" & dsPendaftaran.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault.ALAMAT & "' "
            SQL &= ",KELAS = '" & dsPendaftaran.M_KELASRAWAT.MEMO & "' "
            SQL &= ",TANGGAL_DATANG = '" & dsPendaftaran.DATE & "' "
            SQL &= ",TANGGAL_PULANG = '" & TanggalPulang & "' "
            SQL &= ",NOINVOICE = '' "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",COSTSHARE = ISNULL((SELECT TOTAL_COSTSHARE FROM S_COSTSHARE WHERE D.KDPENDAFTARAN = KDPENDAFTARAN), 0)  "
            SQL &= ",DEPOSIT = ISNULL((SELECT TOTAL FROM F_DEPOSIT WHERE D.KDPENDAFTARAN = KDPENDAFTARAN), 0) "
            SQL &= ",ADMIN = A.DISCOUNT "
            SQL &= ",ROUND = A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= ",ITEM_GROUP = F.MEMO "
            If dsPendaftaran.M_DAFTAR_L1.MEMO = "BPJS" Then
                SQL &= " ,ITEM = E.NMITEM2 "
            Else
                SQL &= " ,ITEM = E.NMITEM2  + ' (' + (SELECT NAME_DISPLAY FROM M_DOCTOR WHERE D.KDDOCTOR = KDDOCTOR) + ')' "
            End If
            SQL &= ",QTY = SUM(B.QTY) "
            SQL &= ",GRANDTOTAL_DETAIL = SUM(B.GRANDTOTAL) "
            SQL &= "FROM "
            SQL &= "S_SO_TRANSAKSI_H A "
            SQL &= "INNER JOIN S_SO_TRANSAKSI_D B "
            SQL &= "ON A.KDSOTRANSAKSI = B.KDSOTRANSAKSI "
            SQL &= "INNER JOIN S_PENDAFTARAN_KUNJUNGAN C "
            SQL &= "ON A.KDKUNJUNGAN = C.KDKUNJUNGAN "
            SQL &= "INNER JOIN S_PENDAFTARAN_H D "
            SQL &= "ON C.KDPENDAFTARAN = D.KDPENDAFTARAN "
            SQL &= "INNER JOIN M_ITEM E "
            SQL &= "ON B.KDITEM = E.KDITEM "
            SQL &= "INNER JOIN M_ITEM_L2 F "
            SQL &= "ON E.KDITEM_L2 = F.KDITEM_L2 "
            SQL &= "WHERE "
            SQL &= "D.KDPENDAFTARAN = '" & sKDPENDAFTARAN & "' "
            SQL &= "OR "
            SQL &= "D.KDPENDAFTARAN = '" & dsPendaftaran.KDPENDAFTARAN_AWAL & "' "
            SQL &= "GROUP BY "
            SQL &= "D.KDPENDAFTARAN "
            SQL &= ",D.KDCUSTOMER "
            SQL &= ",A.SUBTOTAL "
            SQL &= ",A.DISCOUNT "
            SQL &= ",A.TAX "
            SQL &= ",A.GRANDTOTAL "
            SQL &= ",E.NMITEM2 "
            SQL &= ",F.MEMO "
            SQL &= ",A.DATE "
            SQL &= ",D.KDDOCTOR "
            SQL &= "ORDER BY A.DATE "

            oComm.Connection = oConn
            oComm.CommandText = SQL
            oComm.CommandTimeout = 120
            oComm.CommandType = CommandType.Text

            da = New SqlDataAdapter(oComm)
            da.Fill(ds, "ALLL")

            Dim listTranskasi As New List(Of R_CASHIN)

            For iLoop As Integer = 0 To ds.Tables("ALLL").Rows.Count - 1
                Dim dsRekap As New DataAccess.R_CASHIN

                With ds.Tables("ALLL")
                    dsRekap.KDCASHIN = .Rows(iLoop)("KDCASHIN")
                    dsRekap.CATEGORY = .Rows(iLoop)("CATEGORY")
                    dsRekap.KDPENDAFTARAN = .Rows(iLoop)("KDPENDAFTARAN")
                    dsRekap.KDCUSTOMER = .Rows(iLoop)("KDCUSTOMER")
                    dsRekap.TUJUAN = .Rows(iLoop)("TUJUAN")
                    dsRekap.DPJP = .Rows(iLoop)("DPJP")
                    dsRekap.PASIEN = .Rows(iLoop)("PASIEN")
                    dsRekap.ALAMAT = .Rows(iLoop)("ALAMAT")
                    dsRekap.KELAS = .Rows(iLoop)("KELAS")
                    dsRekap.TANGGAL_DATANG = .Rows(iLoop)("TANGGAL_DATANG")
                    dsRekap.TANGGAL_PULANG = .Rows(iLoop)("TANGGAL_PULANG")
                    dsRekap.NOINVOICE = ""
                    dsRekap.SUBTOTAL = .Rows(iLoop)("SUBTOTAL")
                    dsRekap.COSTSHARE = .Rows(iLoop)("COSTSHARE")
                    dsRekap.DEPOSIT = .Rows(iLoop)("DEPOSIT")
                    dsRekap.ADMIN = .Rows(iLoop)("ADMIN")
                    dsRekap.ROUND = .Rows(iLoop)("ROUND")
                    dsRekap.GRANDTOTAL = .Rows(iLoop)("GRANDTOTAL")
                    dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.ITEM = .Rows(iLoop)("ITEM")
                    'dsRekap.ITEM_GROUP = .Rows(iLoop)("ITEM_GROUP")
                    dsRekap.QTY = .Rows(iLoop)("QTY")
                    dsRekap.GRANDTOTAL_DETAIL = .Rows(iLoop)("GRANDTOTAL_DETAIL")

                    listTranskasi.Add(dsRekap)
                End With
            Next

            Dim rpt As New xtraCashInBilling

            'rpt.ShowPrintMarginsWarning = False
            'rpt.Watermark.Text = sWATERMARK

            'rpt.bindingSource.DataSource = listTranskasi
            'Dim printTool As New DevExpress.XtraReports.UI.ReportPrintTool(rpt)
            'printTool.ShowPreviewDialog(DevExpress.LookAndFeel.UserLookAndFeel.Default)

            rpt.bindingSource.DataSource = listTranskasi

            rpt.ExportToPdf("C:/SIMRS/TEMPLATE/RINCIAN.pdf")

            PdfViewer1.LoadDocument("C:/SIMRS/TEMPLATE/RINCIAN.pdf")

        Catch oErr As Exception
            MsgBox(Statement.ErrorStatement & vbCrLf & oErr.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

End Class