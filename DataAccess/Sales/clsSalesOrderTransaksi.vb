Namespace Sales
    Public Class clsSalesOrderTransaksi
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oItem As Reference.clsItem = Nothing
        Public sKDITEM As New List(Of String)

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
                oItem = New Reference.clsItem
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")
                oItem = New Reference.clsItem("TAX")
            End If

            sMODUL = "SO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SO_TRANSAKSI_H
        End Function
        Public Function GetStructureHeaderHapus() As S_SO_TRANSAKSI_HAPUS_H
            If Not oConnection.GetConnection() Then
                GetStructureHeaderHapus = Nothing
            End If
            GetStructureHeaderHapus = New S_SO_TRANSAKSI_HAPUS_H
        End Function
        Public Function GetStructureHeaderTelaahResep() As S_SO_TRANSAKSI_H_TELAAHRESEP
            If Not oConnection.GetConnection() Then
                GetStructureHeaderTelaahResep = Nothing
            End If
            GetStructureHeaderTelaahResep = New S_SO_TRANSAKSI_H_TELAAHRESEP
        End Function
        Public Function GetStructureHeaderTelaahObat1() As S_SO_TRANSAKSI_H_TELAAHOBAT1
            If Not oConnection.GetConnection() Then
                GetStructureHeaderTelaahObat1 = Nothing
            End If
            GetStructureHeaderTelaahObat1 = New S_SO_TRANSAKSI_H_TELAAHOBAT1
        End Function
        Public Function GetStructureHeaderTelaahObat2() As S_SO_TRANSAKSI_H_TELAAHOBAT2
            If Not oConnection.GetConnection() Then
                GetStructureHeaderTelaahObat2 = Nothing
            End If
            GetStructureHeaderTelaahObat2 = New S_SO_TRANSAKSI_H_TELAAHOBAT2
        End Function
        Public Function GetStructureHeaderEtiket() As R_ETIKET
            If Not oConnection.GetConnection() Then
                GetStructureHeaderEtiket = Nothing
            End If
            GetStructureHeaderEtiket = New R_ETIKET
        End Function
        Public Function GetStructureHeaderCekData() As R_CEKDATA
            If Not oConnection.GetConnection() Then
                GetStructureHeaderCekData = Nothing
            End If
            GetStructureHeaderCekData = New R_CEKDATA
        End Function
        Public Function GetStructureHeaderResep() As R_RESEP
            If Not oConnection.GetConnection() Then
                GetStructureHeaderResep = Nothing
            End If
            GetStructureHeaderResep = New R_RESEP
        End Function
        Public Function GetStructureHeaderPCR() As S_SO_TRANSAKSI_D_PCR
            If Not oConnection.GetConnection() Then
                GetStructureHeaderPCR = Nothing
            End If
            GetStructureHeaderPCR = New S_SO_TRANSAKSI_D_PCR
        End Function
        Public Function GetStructureDetail() As S_SO_TRANSAKSI_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_SO_TRANSAKSI_D
        End Function
        Public Function GetStructureDetailCekdata() As R_CEKDATA
            If Not oConnection.GetConnection() Then
                GetStructureDetailCekdata = Nothing
            End If
            GetStructureDetailCekdata = New R_CEKDATA
        End Function
        Public Function GetStructureDetailHapus() As S_SO_TRANSAKSI_HAPUS_D
            If Not oConnection.GetConnection() Then
                GetStructureDetailHapus = Nothing
            End If
            GetStructureDetailHapus = New S_SO_TRANSAKSI_HAPUS_D
        End Function
        Public Function GetStructureDetailEtiket() As R_ETIKET
            If Not oConnection.GetConnection() Then
                GetStructureDetailEtiket = Nothing
            End If
            GetStructureDetailEtiket = New R_ETIKET
        End Function
        Public Function GetStructureDetailList() As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_SO_TRANSAKSI_D)
        End Function
        Public Function GetStructureDetailListCek() As List(Of R_CEKDATA)
            If Not oConnection.GetConnection() Then
                GetStructureDetailListCek = Nothing
            End If
            GetStructureDetailListCek = New List(Of R_CEKDATA)
        End Function
        Public Function GetStructureDetailHapusList() As List(Of S_SO_TRANSAKSI_HAPUS_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailHapusList = Nothing
            End If
            GetStructureDetailHapusList = New List(Of S_SO_TRANSAKSI_HAPUS_D)
        End Function
        Public Function GetStructureDetailEtiketList() As List(Of R_ETIKET)
            If Not oConnection.GetConnection() Then
                GetStructureDetailEtiketList = Nothing
            End If
            GetStructureDetailEtiketList = New List(Of R_ETIKET)
        End Function
        Public Function GetData() As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_TRANSAKSI_Hs.OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetDataByCategoryBilling(ByVal Category As Integer, ByVal sDATEFROM As DateTime, sDATETO As DateTime) As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetDataByCategoryBilling = Nothing
                Exit Function
            End If
            GetDataByCategoryBilling = oConnection.db.S_SO_TRANSAKSI_Hs.Where(Function(x) x.CATEGORY = Category And x.DATE >= sDATEFROM.ToString("yyyy-MM-dd") & " 00:00:00" And x.DATE <= sDATETO.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetDataByRM(ByVal KDCUSTOMER As String) As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.db.S_SO_TRANSAKSI_Hs.Where(Function(x) x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER = KDCUSTOMER).OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetDataByCategoryBillingPCR(ByVal sDATEFROM As DateTime, sDATETO As DateTime) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataByCategoryBillingPCR = Nothing
                Exit Function
            End If
            GetDataByCategoryBillingPCR = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.M_ITEM.NMITEM1 = "SWAB" And x.S_SO_TRANSAKSI_H.DATE >= sDATEFROM.ToString("yyyy-MM-dd") & " 00:00:00" And x.S_SO_TRANSAKSI_H.DATE <= sDATETO.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetData(ByVal sKDSOTRANSAKSI As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
        End Function
        Public Function GetDataPCR(ByVal sKDSOTRANSAKSI As String, ByVal sKDITEM As String) As S_SO_TRANSAKSI_D_PCR
            If Not oConnection.GetConnection() Then
                GetDataPCR = Nothing
                Exit Function
            End If
            GetDataPCR = oConnection.db.S_SO_TRANSAKSI_D_PCRs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataKDITEM(ByVal sKDSOTRANSAKSI As String, ByVal sKDITEM As String) As S_SO_TRANSAKSI_D
            If Not oConnection.GetConnection() Then
                GetDataKDITEM = Nothing
                Exit Function
            End If
            GetDataKDITEM = oConnection.db.S_SO_TRANSAKSI_Ds.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataHargaINACBG(ByVal sKDPENDAFTARAN As String) As R_DIAGNOSA_MASTER
            If Not oConnection.GetConnection() Then
                GetDataHargaINACBG = Nothing
                Exit Function
            End If

            Dim dsDiagnosa = oConnection.db.S_PENDAFTARAN_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)

            If dsDiagnosa IsNot Nothing Then
                GetDataHargaINACBG = oConnection.db.R_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDDIAGNOSA1 = dsDiagnosa.KDDIAGNOSA1)
            Else
                GetDataHargaINACBG = Nothing
            End If

        End Function
        Public Function GetDataByKD(ByVal sKDPENDAFTARAN As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByKD = Nothing
                Exit Function
            End If
            GetDataByKD = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_KUNJUNGAN.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataKunjunganByKD(ByVal sKDKUNJUNGAN As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataKunjunganByKD = Nothing
                Exit Function
            End If
            GetDataKunjunganByKD = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataMasterDiagnosaByKD(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARAN_DIAGNOSA_MASTER
            If Not oConnection.GetConnection() Then
                GetDataMasterDiagnosaByKD = Nothing
                Exit Function
            End If
            GetDataMasterDiagnosaByKD = oConnection.db.S_PENDAFTARAN_DIAGNOSA_MASTERs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataByKDkunjungan(ByVal sKDKUNJUNGAN As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjungan = Nothing
                Exit Function
            End If
            GetDataByKDkunjungan = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDataByKDkunjunganFarmasi(ByVal sKDKUNJUNGAN As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjunganFarmasi = Nothing
                Exit Function
            End If
            GetDataByKDkunjunganFarmasi = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN And x.CATEGORY = 4)
        End Function
        Public Function GetDataByKDPendaftaranFarmasi(ByVal sKDPENDAFTARAN As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByKDPendaftaranFarmasi = Nothing
                Exit Function
            End If
            GetDataByKDPendaftaranFarmasi = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.S_PENDAFTARAN_KUNJUNGAN.KDPENDAFTARAN = sKDPENDAFTARAN And x.CATEGORY = 4)
        End Function
        Public Function GetDataByKDPendaftaranFarmasiList(ByVal sKDPENDAFTARAN As String) As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetDataByKDPendaftaranFarmasiList = Nothing
                Exit Function
            End If
            GetDataByKDPendaftaranFarmasiList = oConnection.db.S_SO_TRANSAKSI_Hs.Where(Function(x) x.S_PENDAFTARAN_KUNJUNGAN.KDPENDAFTARAN = sKDPENDAFTARAN And x.CATEGORY = 4).ToList()
        End Function
        Public Function GetDataByKDkunjunganFarmasiList(ByVal sKDKUNJUNGAN As String) As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjunganFarmasiList = Nothing
                Exit Function
            End If
            GetDataByKDkunjunganFarmasiList = oConnection.db.S_SO_TRANSAKSI_Hs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN And x.CATEGORY = 4).ToList()
        End Function
        Public Function GetDataByKDkunjunganLabList(ByVal sKDKUNJUNGAN As String) As List(Of S_SO_TRANSAKSI_H)
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjunganLabList = Nothing
                Exit Function
            End If
            GetDataByKDkunjunganLabList = oConnection.db.S_SO_TRANSAKSI_Hs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN And x.CATEGORY = 2).ToList()
        End Function
        Public Function GetDataByKDkunjunganDetailRadiologiList(ByVal sKDSOTRANSKASI As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjunganDetailRadiologiList = Nothing
                Exit Function
            End If
            GetDataByKDkunjunganDetailRadiologiList = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.S_SO_TRANSAKSI_H.KDKUNJUNGAN = sKDSOTRANSKASI And x.S_SO_TRANSAKSI_H.CATEGORY = 3).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataByKDkunjunganDetailList(ByVal sKDSOTRANSKASI As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataByKDkunjunganDetailList = Nothing
                Exit Function
            End If
            GetDataByKDkunjunganDetailList = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSKASI).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDSOTRANSAKSI As String, ByVal sKDITEM As String, ByVal sKDUOM As String, ByVal sSEQ As Integer) As S_SO_TRANSAKSI_D
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_TRANSAKSI_Ds.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.KDITEM = sKDITEM And x.KDUOM = sKDUOM And sSEQ = sSEQ)
        End Function
        Public Function GetDataDetail() As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_TRANSAKSI_Ds.ToList()
        End Function
        Public Function GetDataDetailTanpaObat(ByVal sNoinvoice As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailTanpaObat = Nothing
                Exit Function
            End If
            GetDataDetailTanpaObat = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sNoinvoice And x.S_SO_TRANSAKSI_H.CATEGORY <> 4).ToList()
        End Function
        Public Function GetDataDetailObat(ByVal sNoinvoice As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailObat = Nothing
                Exit Function
            End If
            GetDataDetailObat = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sNoinvoice And x.S_SO_TRANSAKSI_H.CATEGORY = 4).ToList()
        End Function
        Public Function GetDataDetailpendafataranTanpaObat(ByVal sNoinvoice As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetailpendafataranTanpaObat = Nothing
                Exit Function
            End If
            GetDataDetailpendafataranTanpaObat = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN = sNoinvoice And x.S_SO_TRANSAKSI_H.CATEGORY <> 4).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDSOTRANSAKSI As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDSOTRANSAKSI As String, ByVal sGroupRacik As Integer) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.GROUPRACIK = sGroupRacik).ToList()
        End Function
        Public Function GetDataByGroupTarif(ByVal sKKUNJUNGAN As String) As List(Of S_SO_TRANSAKSI_D)
            If Not oConnection.GetConnection() Then
                GetDataByGroupTarif = Nothing
                Exit Function
            End If
            GetDataByGroupTarif = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.S_SO_TRANSAKSI_H.KDKUNJUNGAN = sKKUNJUNGAN And x.M_ITEM.M_ITEM_L4.MEMO = "VALIDASI").ToList()
        End Function
        Public Function GetSisaObat_Generik(ByVal sKDPENDAFFTARAN As String, ByVal sKDPENDAFFTARAN_AWAL As String) As Decimal
            If Not oConnection.GetConnection Then
                GetSisaObat_Generik = 0
                Exit Function
            End If

            Try
                Dim ds = (From x In oConnection.db.S_SO_TRANSAKSI_Hs
                          Join y In oConnection.db.S_PENDAFTARAN_KUNJUNGANs
                          On x.KDKUNJUNGAN Equals y.KDKUNJUNGAN
                          Where y.KDPENDAFTARAN = sKDPENDAFFTARAN Or y.KDPENDAFTARAN = sKDPENDAFFTARAN_AWAL
                          Select x.GRANDTOTAL).ToList

                GetSisaObat_Generik = ds.Sum()

            Catch ex As Exception
                GetSisaObat_Generik = 0
            End Try

        End Function
        Public Function GetTotalObat(ByVal sKDPENDAFFTARAN As String, ByVal sKDPENDAFFTARAN_AWAL As String) As Decimal
            If Not oConnection.GetConnection Then
                GetTotalObat = 0
                Exit Function
            End If

            Try
                Dim ds = (From x In oConnection.db.S_SO_TRANSAKSI_Hs
                          Join y In oConnection.db.S_PENDAFTARAN_KUNJUNGANs
                          On x.KDKUNJUNGAN Equals y.KDKUNJUNGAN
                          Where y.KDPENDAFTARAN = sKDPENDAFFTARAN And x.KDSOTRANSAKSI.Contains("SOFM") Or y.KDPENDAFTARAN = sKDPENDAFFTARAN_AWAL And x.KDSOTRANSAKSI.Contains("SOFM")
                          Select x.GRANDTOTAL).ToList

                GetTotalObat = ds.Sum()

            Catch ex As Exception
                GetTotalObat = 0
            End Try
        End Function
        Public Function GetDataItem(ByVal sKDUSER As String) As List(Of M_ITEM_USER)
            If Not oConnection.GetConnection() Then
                GetDataItem = Nothing
                Exit Function
            End If
            GetDataItem = oConnection.db.M_ITEM_USERs.Where(Function(x) x.KDUSER = sKDUSER).ToList
        End Function
        Public Function GetDataByKDPENDAFTARAN(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataByKDPENDAFTARAN = Nothing
                Exit Function
            End If
            GetDataByKDPENDAFTARAN = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetTotalByKdPendftaran(ByVal sKDPENDAFFTARAN As String, ByVal sKDPENDAFFTARAN_AWAL As String) As Decimal
            If Not oConnection.GetConnection Then
                GetTotalByKdPendftaran = 0
                Exit Function
            End If

            Try
                Dim ds = (From x In oConnection.db.S_SO_TRANSAKSI_Hs
                          Where x.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDPENDAFTARAN = sKDPENDAFFTARAN
                          Select x.GRANDTOTAL).ToList

                Dim ds2 = (From x In oConnection.db.S_SO_TRANSAKSI_Hs
                           Where x.S_PENDAFTARAN_KUNJUNGAN.KDPENDAFTARAN = sKDPENDAFFTARAN_AWAL
                           Select x.GRANDTOTAL).ToList

                GetTotalByKdPendftaran = ds.Sum() + ds2.Sum()

            Catch ex As Exception
                GetTotalByKdPendftaran = 0
            End Try
        End Function
        Public Function GetDataByCategory(ByVal sKDSOTRANSAKSI As String, ByVal sCategory As Integer) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByCategory = Nothing
                Exit Function
            End If
            GetDataByCategory = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.CATEGORY = sCategory)
        End Function
        Public Function GetDataTelaahResep(ByVal sKDSOTRANSAKSI As String) As S_SO_TRANSAKSI_H_TELAAHRESEP
            If Not oConnection.GetConnection() Then
                GetDataTelaahResep = Nothing
                Exit Function
            End If
            GetDataTelaahResep = oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
        End Function
        Public Function GetDataTelaahObat1(ByVal sKDSOTRANSAKSI As String) As S_SO_TRANSAKSI_H_TELAAHOBAT1
            If Not oConnection.GetConnection() Then
                GetDataTelaahObat1 = Nothing
                Exit Function
            End If
            GetDataTelaahObat1 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
        End Function
        Public Function GetDataTelaahObat2(ByVal sKDSOTRANSAKSI As String) As S_SO_TRANSAKSI_H_TELAAHOBAT2
            If Not oConnection.GetConnection() Then
                GetDataTelaahObat2 = Nothing
                Exit Function
            End If
            GetDataTelaahObat2 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
        End Function
        'Public Function GetDataByOrder(ByVal sKDORDER As String) As S_SO_TRANSAKSI_H
        '    If Not oConnection.GetConnection() Then
        '        GetDataByOrder = Nothing
        '        Exit Function
        '    End If
        '    GetDataByOrder = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER)
        'End Function
        Public Function GetDataByOrderLab(ByVal sKDORDER As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByOrderLab = Nothing
                Exit Function
            End If

            If sKDORDER = "" Then
                sKDORDER = "XXXXXX"
            End If
            GetDataByOrderLab = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER And x.CATEGORY = 2)
        End Function
        Public Function GetDataByOrderFarmasi(ByVal sKDORDER As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataByOrderFarmasi = Nothing
                Exit Function
            End If

            If sKDORDER = "" Then
                sKDORDER = "XXXXXX"
            End If
            GetDataByOrderFarmasi = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDORDER = sKDORDER And x.CATEGORY = 4)
        End Function
        Public Function InsertDataHapus(ByVal entity As S_SO_TRANSAKSI_HAPUS_H, ByVal entityDetail As List(Of S_SO_TRANSAKSI_HAPUS_D)) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataHapus = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_SO_TRANSAKSI_HAPUS_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_SO_TRANSAKSI_HAPUS_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataHapus = entity.KDSOTRANSAKSI
            Catch ex As Exception
                InsertDataHapus = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As S_SO_TRANSAKSI_H, ByVal entityDetail As List(Of S_SO_TRANSAKSI_D)) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "INSERT"

                Dim Auot As Boolean = False

                If entity.KDSOTRANSAKSI = "" Then
                    Auot = True

                    If entity.CATEGORY = 0 Then
                        sMODUL = "SORJ"
                    ElseIf entity.CATEGORY = 1 Then
                        sMODUL = "SORI"
                    ElseIf entity.CATEGORY = 2 Then
                        sMODUL = "SOLB"
                    ElseIf entity.CATEGORY = 3 Then
                        sMODUL = "SORD"
                    ElseIf entity.CATEGORY = 4 Then
                        sMODUL = "SOFM"
                    ElseIf entity.CATEGORY = 5 Then
                        sMODUL = "SOBD"
                    End If

                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATE)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                        entity.KDSOTRANSAKSI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                        For Each iLoop In entityDetail
                            iLoop.KDSOTRANSAKSI = entity.KDSOTRANSAKSI
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                Try
                    oConnection.db.S_SO_TRANSAKSI_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_SO_TRANSAKSI_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If Auot = True Then
                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                If entity.ISBHP = True Then
                    Try
                        For Each iLoop In entityDetail
                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                                Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                                dsStock.AMOUNT -= iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                Dim dsStock As New M_ITEM_WAREHOUSE
                                With dsStock
                                    .DATECREATED = entity.DATECREATED
                                    .DATEUPDATED = entity.DATEUPDATED
                                    .KDWAREHOUSE = entity.KDWAREHOUSE
                                    .KDITEM = sKDITEM
                                    .KDUOM = sKDUOM
                                    .AMOUNT = -iLoop.QTY
                                End With

                                oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                                oConnection.db.SubmitChanges()
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    'Try
                    '    For Each iLoop In entityDetail
                    '        sKDITEM.Add(iLoop.KDITEM)
                    '    Next
                    '    Dim oAverage As New Accounting.clsStockCard
                    '    oAverage.PostingAverage(sKDITEM)
                    'Catch ex As Exception
                    '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    '    Throw ex
                    'End Try
                End If

                InsertData = entity.KDSOTRANSAKSI
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
                'Finally
                '    If entity.CATEGORY <> 4 Or entity.CATEGORY = 0 Then
                '        oConnection.db.Dispose()

                '        oConnection = Nothing
                '        oError = Nothing
                '        oCounter = Nothing
                '    End If

            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_SO_TRANSAKSI_H, ByVal entityDetail As List(Of S_SO_TRANSAKSI_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_TRANSAKSI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)

                If ds.ISBHP = True Then
                    Try
                        For Each iLoop In dsDetail
                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            If oItem.GetDataDetail_WAREHOUSE(sKDITEM, ds.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                                Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)
                                dsStock.AMOUNT += iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                Dim dsStock As New M_ITEM_WAREHOUSE
                                With dsStock
                                    .DATECREATED = entity.DATECREATED
                                    .DATEUPDATED = entity.DATEUPDATED
                                    .KDWAREHOUSE = ds.KDWAREHOUSE
                                    .KDITEM = sKDITEM
                                    .KDUOM = sKDUOM
                                    .AMOUNT = iLoop.QTY
                                End With

                                oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                                oConnection.db.SubmitChanges()
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    Try
                        For Each iLoop In dsDetail
                            sKDITEM.Add(iLoop.KDITEM)
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                Try
                    If dsDetail.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If ds.ISBHP = True Then
                    Try
                        For Each iLoop In entityDetail
                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            If oItem.GetDataDetail_WAREHOUSE(sKDITEM, entity.KDWAREHOUSE, sKDUOM) IsNot Nothing Then
                                Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = entity.KDWAREHOUSE And x.KDUOM = sKDUOM)
                                dsStock.AMOUNT -= iLoop.QTY

                                oConnection.db.SubmitChanges()
                            Else
                                Dim dsStock As New M_ITEM_WAREHOUSE
                                With dsStock
                                    .DATECREATED = entity.DATECREATED
                                    .DATEUPDATED = entity.DATEUPDATED
                                    .KDWAREHOUSE = entity.KDWAREHOUSE
                                    .KDITEM = sKDITEM
                                    .KDUOM = sKDUOM
                                    .AMOUNT = -iLoop.QTY
                                End With

                                oConnection.db.M_ITEM_WAREHOUSEs.InsertOnSubmit(dsStock)
                                oConnection.db.SubmitChanges()
                            End If
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    'Try
                    '    For Each iLoop In entityDetail
                    '        sKDITEM.Add(iLoop.KDITEM)
                    '    Next
                    '    Dim oAverage As New Accounting.clsStockCard
                    '    oAverage.PostingAverage(sKDITEM)
                    'Catch ex As Exception
                    '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    '    Throw ex
                    'End Try
                End If
                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
                'Finally
                '    If entity.CATEGORY <> 4 Then
                '        oConnection.db.Dispose()

                '        oConnection = Nothing
                '        oError = Nothing
                '        oCounter = Nothing
                '    End If
            End Try
        End Function
        Public Function DeleteData(ByVal sKDSOTRANSAKSI As String, ByVal userhapus As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsDetail = oConnection.db.S_SO_TRANSAKSI_Ds.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsTelaahResep = oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsTelaahObat1 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsTelaahObat2 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsRadiologi = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)

                Dim oSalesOrderTransaksiHapus As New Sales.clsSalesOrderTransaksi

                Dim dsHapus = oSalesOrderTransaksiHapus.GetStructureHeaderHapus
                ' ***** HEADER *****
                With dsHapus
                    Try
                        .DATECREATED = ds.DATECREATED
                    Catch oErr As Exception
                        .DATECREATED = Now
                    End Try
                    .DATEUPDATED = Now

                    .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                    .CATEGORY = ds.CATEGORY
                    .DATE = ds.DATE
                    .KDKUNJUNGAN = ds.KDKUNJUNGAN
                    .KDWAREHOUSE = ds.KDWAREHOUSE
                    .ISBHP = ds.ISBHP
                    .SUBTOTAL = CDec(ds.SUBTOTAL)
                    .DISCOUNT = CDec(ds.DISCOUNT)
                    .TAX = CDec(ds.TAX)
                    .GRANDTOTAL = CDec(ds.GRANDTOTAL)
                    .PAYAMOUNT = CDec(ds.PAYAMOUNT)
                    .MEMO = ds.MEMO
                    .KDUSER = ds.KDUSER & " " & userhapus
                    .KDSHIFT = ds.KDSHIFT
                    .KDDOCTOR = ds.KDDOCTOR
                    .TUSLAH = CDec(ds.TUSLAH)
                    .KDCPPT = ds.KDCPPT
                    .KDORDER = ds.KDORDER
                End With

                ' ***** DETIL *****
                Dim arrDetailHapus = oSalesOrderTransaksiHapus.GetStructureDetailHapusList
                For Each xloop In oSalesOrderTransaksiHapus.GetDataDetail(ds.KDSOTRANSAKSI)
                    Dim dsDetailHapus = oSalesOrderTransaksiHapus.GetStructureDetailHapus
                    With dsDetailHapus
                        .DATECREATED = xloop.DATECREATED
                        .DATEUPDATED = xloop.DATEUPDATED
                        .SEQ = xloop.SEQ
                        .KDSOTRANSAKSI = ds.KDSOTRANSAKSI
                        .KDITEM = xloop.KDITEM
                        .KDUOM = xloop.KDUOM
                        .KDSIGNA = xloop.KDSIGNA
                        .KDCARAPAKAI = xloop.KDCARAPAKAI
                        .QTY = CDec(xloop.QTY)
                        .ISRACIK = CBool(xloop.ISRACIK)
                        .PRICE = CDec(xloop.PRICE)
                        .SUBTOTAL = CDec(xloop.SUBTOTAL)
                        .DISCOUNT = CDec(xloop.DISCOUNT)
                        .GRANDTOTAL = CDec(xloop.GRANDTOTAL)
                        .KDDOCTOR = xloop.KDDOCTOR
                        .KDDEPARTMENT = xloop.KDDEPARTMENT
                        .REMARKS = xloop.REMARKS
                        .ISCETAKETIKET = xloop.ISCETAKETIKET
                        .GROUPRACIK = xloop.GROUPRACIK
                    End With
                    arrDetailHapus.Add(dsDetailHapus)
                Next

                Try
                    oConnection.db.S_SO_TRANSAKSI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_Ds.DeleteAllOnSubmit(dsDetail)
                    If dsTelaahResep.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.DeleteAllOnSubmit(dsTelaahResep)
                    End If
                    If dsTelaahObat1.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.DeleteAllOnSubmit(dsTelaahObat1)
                    End If
                    If dsTelaahObat2.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.DeleteAllOnSubmit(dsTelaahObat2)
                    End If
                    If dsRadiologi.Count > 0 Then
                        oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.DeleteAllOnSubmit(dsRadiologi)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                If ds.ISBHP = True Then
                    Try
                        For Each iLoop In dsDetail
                            Dim sKDITEM = iLoop.KDITEM
                            Dim sKDUOM = iLoop.KDUOM

                            Dim dsStock = oConnection.db.M_ITEM_WAREHOUSEs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM And x.KDWAREHOUSE = ds.KDWAREHOUSE And x.KDUOM = sKDUOM)

                            dsStock.AMOUNT += iLoop.QTY
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    Try
                        For Each iLoop In dsDetail
                            sKDITEM.Add(iLoop.KDITEM)
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True

                InsertDataHapus(dsHapus, arrDetailHapus)

            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataPCR(ByVal entity As S_SO_TRANSAKSI_D_PCR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataPCR = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_PCRs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataPCR = True
            Catch ex As Exception
                InsertDataPCR = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPCR(ByVal entity As S_SO_TRANSAKSI_D_PCR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataPCR = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_D_PCRs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_PCRs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_D_PCRs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataPCR = True
            Catch ex As Exception
                UpdateDataPCR = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateMemo(ByVal sKDSOTRANSAKSI As String, ByVal memo As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateMemo = False
                    Exit Function
                End If

                UpdateMemo = True

                Dim ds = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)

                If ds IsNot Nothing Then
                    ds.MEMO = memo

                    oConnection.db.SubmitChanges()

                End If

            Catch ex As Exception
                UpdateMemo = False
                Throw ex
            End Try
        End Function
        Public Function DeleteDataPCR(ByVal sKDSOTRANSAKSI As String, ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteDataPCR = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_D_PCRs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.KDITEM = sKDITEM)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_PCRs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                DeleteDataPCR = True
            Catch ex As Exception
                DeleteDataPCR = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDPJP(ByVal kdpendaftaran As String, ByVal kddoctor As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDPJP = False
                    Exit Function
                End If

                UpdateDPJP = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.KDDOCTOR = kddoctor

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDPJP = False
                Throw ex
            End Try
        End Function
        Public Function UpdateKodeOrder(ByVal sKDTRANSAKSI As String, ByVal sKDORDER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateKodeOrder = False
                    Exit Function
                End If

                UpdateKodeOrder = True

                Dim ds = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDTRANSAKSI)

                ds.KDORDER = sKDORDER

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateKodeOrder = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDokter(ByVal KDSO As String, ByVal KDITEM As String, ByVal KDUOM As String, ByVal SEQ As Integer, ByVal KDDOCOTOR As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDokter = False
                    Exit Function
                End If

                UpdateDokter = True

                Dim ds = oConnection.db.S_SO_TRANSAKSI_Ds.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = KDSO And x.KDITEM = KDITEM And x.KDUOM = KDUOM And x.SEQ = SEQ)

                ds.KDDOCTOR = KDDOCOTOR

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDokter = False
                Throw ex
            End Try
        End Function
        Public Function InsertDataTelaahResep(ByVal entity As S_SO_TRANSAKSI_H_TELAAHRESEP, ByVal entityObat1 As S_SO_TRANSAKSI_H_TELAAHOBAT1, ByVal entityObat2 As S_SO_TRANSAKSI_H_TELAAHOBAT2) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataTelaahResep = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.InsertOnSubmit(entity)
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.InsertOnSubmit(entityObat1)
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.InsertOnSubmit(entityObat2)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataTelaahResep = True
            Catch ex As Exception
                InsertDataTelaahResep = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataTelaahResep(ByVal entity As S_SO_TRANSAKSI_H_TELAAHRESEP, ByVal entityObat1 As S_SO_TRANSAKSI_H_TELAAHOBAT1, ByVal entityObat2 As S_SO_TRANSAKSI_H_TELAAHOBAT2) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataTelaahResep = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)
                Dim dsObat1 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)
                Dim dsObat2 = oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHRESEPs.InsertOnSubmit(entity)

                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.DeleteOnSubmit(dsObat1)
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT1s.InsertOnSubmit(entityObat1)

                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.DeleteOnSubmit(dsObat2)
                    oConnection.db.S_SO_TRANSAKSI_H_TELAAHOBAT2s.InsertOnSubmit(entityObat2)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataTelaahResep = True
            Catch ex As Exception
                UpdateDataTelaahResep = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace