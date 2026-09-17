Namespace Admission
    Public Class clsPendaftaran
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "PENDAFTARAN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_H
        End Function
        Public Function GetStructureHeaderList() As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeaderList = Nothing
            End If
            GetStructureHeaderList = New A_IDENTITASPASIEN_LIST
        End Function
        Public Function GetStructureHeader_Kunjungan() As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader_Kunjungan = Nothing
            End If
            GetStructureHeader_Kunjungan = New S_PENDAFTARAN_KUNJUNGAN
        End Function
        Public Function GetStructureHeader_PenanggungJawab() As S_PENDAFTARAN_PENANGGUNGJAWAB
            If Not oConnection.GetConnection() Then
                GetStructureHeader_PenanggungJawab = Nothing
            End If
            GetStructureHeader_PenanggungJawab = New S_PENDAFTARAN_PENANGGUNGJAWAB
        End Function
        Public Function GetStructureHeader_Identitas() As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetStructureHeader_Identitas = Nothing
            End If
            GetStructureHeader_Identitas = New R_IDENTITAS_PASIEN
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_Hs.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataByRekamMedisRawatJalan(ByVal Parameter As String) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataByRekamMedisRawatJalan = Nothing
                Exit Function
            End If
            GetDataByRekamMedisRawatJalan = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = Parameter And x.CATEGORY = 0).ToList()
        End Function
        Public Function GetDataCashin(ByVal Parameter As String) As F_CASHIN_H
            If Not oConnection.GetConnection() Then
                GetDataCashin = Nothing
                Exit Function
            End If
            GetDataCashin = oConnection.db.F_CASHIN_Hs.FirstOrDefault(Function(x) x.KDCASHIN = Parameter)
        End Function
        Public Function GetDataMasterPasien(ByVal Parameter As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetDataMasterPasien = Nothing
                Exit Function
            End If
            GetDataMasterPasien = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = Parameter)
        End Function
        Public Function GetDataNomorSEP(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataNomorSEP = Nothing
                Exit Function
            End If
            GetDataNomorSEP = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.NOMORSEP = Parameter)
        End Function
        Public Function GetDataIdentitas(ByVal Parameter As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataIdentitas = Nothing
                Exit Function
            End If
            GetDataIdentitas = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDataList(ByVal Parameter As String) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDPENDAFTARAN = Parameter).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataListrm(ByVal Parameter As String) As List(Of F_CASHIN_H)
            If Not oConnection.GetConnection() Then
                GetDataListrm = Nothing
                Exit Function
            End If
            GetDataListrm = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.S_PENDAFTARAN_H.KDCUSTOMER = Parameter).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataListnama(ByVal Parameter As String) As List(Of F_CASHIN_H)
            If Not oConnection.GetConnection() Then
                GetDataListnama = Nothing
                Exit Function
            End If
            GetDataListnama = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter)).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataListKDREG(ByVal Parameter As String) As List(Of F_CASHIN_H)
            If Not oConnection.GetConnection() Then
                GetDataListKDREG = Nothing
                Exit Function
            End If
            GetDataListKDREG = oConnection.db.F_CASHIN_Hs.Where(Function(x) x.S_PENDAFTARAN_H.KDPENDAFTARAN = Parameter).OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetDataKunjungan() As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If
            GetDataKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetDataKunjunganSalesOrder(ByVal KDPENDAFTARAN_1 As String, ByVal KDPENDAFTARAN_2 As String) As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetDataKunjunganSalesOrder = Nothing
                Exit Function
            End If
            GetDataKunjunganSalesOrder = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = KDPENDAFTARAN_1 Or x.KDPENDAFTARAN = KDPENDAFTARAN_2).OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        'Public Function GetDataKunjungan_List(ByVal sKDKUNJUNGAN As String) As List(Of S_PENDAFTARAN_KUNJUNGAN)
        '    If Not oConnection.GetConnection() Then
        '        GetDataKunjungan_List = Nothing
        '        Exit Function
        '    End If
        '    GetDataKunjungan_List = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN).OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        'End Function
        Public Function GetDataBySKD(ByVal Parameter As String, ByVal Category As Integer) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataBySKD = Nothing
                Exit Function
            End If
            GetDataBySKD = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) IIf(Category = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Category = 1, x.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter), x.KDPENDAFTARAN.Contains(Parameter)))).ToList()
        End Function
        Public Function GetDataByPendaftaranRJ(ByVal Parameter As String, ByVal Category As Integer) As List(Of S_PENDAFTARAN_H)
            If Not oConnection.GetConnection() Then
                GetDataByPendaftaranRJ = Nothing
                Exit Function
            End If
            GetDataByPendaftaranRJ = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) IIf(Category = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Category = 1, x.NOMORRUJUKAN.Contains(Parameter), IIf(Category = 2, x.KARTUBPJS.Contains(Parameter), IIf(Category = 4, x.KTP.Contains(Parameter), IIf(Category = 5, x.M_CUSTOMER.NAME_DISPLAY.Contains(Parameter), IIf(Category = 6, x.M_CUSTOMER.ALAMAT.Contains(Parameter), x.KDPENDAFTARAN.Contains(Parameter)))))))).ToList()
        End Function
        'Public Function GetDataNomorPendaftaranRawatJalan(ByVal sKDCUSTOMER As String, sDATE As DateTime) As List(Of S_PENDAFTARAN_H)
        '    If Not oConnection.GetConnection() Then
        '        GetDataNomorPendaftaranRawatJalan = Nothing
        '        Exit Function
        '    End If
        '    GetDataNomorPendaftaranRawatJalan = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.CATEGORY = 0 And x.DATE >= sDATE.ToString("yyyy-MM-dd") & " 00:00:00").OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        'End Function
        Public Function GetDataByRMUnitDate(ByVal sKDCUSTOMER As String, ByVal sKDDEPARTMENT As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByRMUnitDate = Nothing
                Exit Function
            End If
            GetDataByRMUnitDate = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.KDDEPARTMENT = sKDDEPARTMENT And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE))
        End Function
        Public Function GetDataByRMPasienBaru(ByVal sKDCUSTOMER As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByRMPasienBaru = Nothing
                Exit Function
            End If
            GetDataByRMPasienBaru = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.DATE <= sDATE.ToString("yyyy-MM-dd") & " 00:00:00")
        End Function
        Public Function GetDataByKodeBooking(ByVal sKODEBOOKING As String) As ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByKodeBooking = Nothing
                Exit Function
            End If
            GetDataByKodeBooking = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataBySuratKontrol(ByVal sKDSKD As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataBySuratKontrol = Nothing
                Exit Function
            End If
            GetDataBySuratKontrol = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.NOMORSKDP = sKDSKD)
        End Function
        Public Function GetDataPendaftaranByNomorRujukan(ByVal sKDSKD As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranByNomorRujukan = Nothing
                Exit Function
            End If
            GetDataPendaftaranByNomorRujukan = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.NOMORRUJUKAN = sKDSKD).OrderByDescending(Function(x) x.DATE).FirstOrDefault
        End Function
        Public Function GetDataPendaftaranByKodeBooking(ByVal sKODEBOOKING As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaranByKodeBooking = Nothing
                Exit Function
            End If
            GetDataPendaftaranByKodeBooking = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataKunjunganByKodepndafatran(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataKunjunganByKodepndafatran = Nothing
                Exit Function
            End If
            GetDataKunjunganByKodepndafatran = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
        End Function
        Public Function GetDataByRMKunjunganTerkahir(ByVal sKDCUSTOMER As String) As S_PENDAFTARAN_H
            'If Not oConnection.GetConnection() Then
            '    GetDataByRMKunjunganTerkahir = Nothing
            '    Exit Function
            'End If
            'GetDataByRMKunjunganTerkahir = oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.CATEGORY = 0).OrderByDescending(Function(x) x.DATE)


            If Not oConnection.GetConnection Then
                GetDataByRMKunjunganTerkahir = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_Hs.Where(Function(x) x.KDCUSTOMER = sKDCUSTOMER And x.CATEGORY = 0 And x.M_DEPARTMENT.VCLAIM_KODEPOLI <> "IGD")
                      Select x).ToList()

            GetDataByRMKunjunganTerkahir = ds.OrderByDescending(Function(x) x.DATE).FirstOrDefault()

        End Function
        Public Function GetDataByRMDateRawatInap(ByVal sKDCUSTOMER As String, ByVal sDATE As DateTime) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataByRMDateRawatInap = Nothing
                Exit Function
            End If
            GetDataByRMDateRawatInap = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.CATEGORY = 1 And x.KDCUSTOMER = sKDCUSTOMER And x.DATE.Year = Year(sDATE) And x.DATE.Month = Month(sDATE) And x.DATE.Day = Day(sDATE))
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataKunjungan(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataKunjungan = Nothing
                Exit Function
            End If
            GetDataKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDataKunjunganByPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDataKunjunganByPendaftaran = Nothing
                Exit Function
            End If
            GetDataKunjunganByPendaftaran = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDatPenanggungJawabByPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_PENANGGUNGJAWAB
            If Not oConnection.GetConnection() Then
                GetDatPenanggungJawabByPendaftaran = Nothing
                Exit Function
            End If
            GetDatPenanggungJawabByPendaftaran = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataPenanggungJawabByPendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_PENANGGUNGJAWAB
            If Not oConnection.GetConnection() Then
                GetDataPenanggungJawabByPendaftaran = Nothing
                Exit Function
            End If
            GetDataPenanggungJawabByPendaftaran = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataKunjunganBySKD(ByVal Parameter As String) As S_PENDAFTARAN_SKD
            If Not oConnection.GetConnection() Then
                GetDataKunjunganBySKD = Nothing
                Exit Function
            End If
            GetDataKunjunganBySKD = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataSetting() As SET_SETTING
            If Not oConnection.GetConnection() Then
                GetDataSetting = Nothing
                Exit Function
            End If
            GetDataSetting = oConnection.db.SET_SETTINGs.FirstOrDefault()
        End Function
        Public Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
            Dim years As Long
            Dim months As Long
            Dim days As Long
            Dim yearWord As String
            Dim monthWord As String
            Dim dayWord As String

            ' menghitung tahun
            years = DateDiff("yyyy", tgllahir, dateNow)
            If Month(tgllahir) > Month(dateNow) Then
                years = years - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day > dateNow.Day Then
                years = years - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day = dateNow.Day Then
                'GoTo Finish ' jika bulan dan tanggal sama maka perhitungan selesai
            End If
            ' menghitung bulan
            tgllahir = DateAdd("yyyy", years, tgllahir)
            months = DateDiff("m", tgllahir, dateNow)
            If tgllahir.Day > dateNow.Day Then
                months = months - 1
            ElseIf Month(tgllahir) = Month(dateNow) And tgllahir.Day >= dateNow.Day Then
                months = months - 1
            End If
            tgllahir = DateAdd("m", months, tgllahir)
            ' menghitung hari
            days = DateDiff("d", tgllahir, dateNow)

            yearWord = IIf(years = 0, "", years & " Tahun ")
            monthWord = IIf(months = 0, "", months & " Bulan ")
            dayWord = IIf(days = 0, "", days & " Hari ")
            'calculateAge = yearWord & monthWord & dayWord
            'calculateAge = Trim(calculateAge)

            GetUmurPasien = yearWord & " " & monthWord & " " & dayWord
        End Function
        Public Function InsertDataR_identitas(ByVal entity As R_IDENTITAS_PASIEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataR_identitas = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_PASIENs.InsertOnSubmit(entity)
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


                InsertDataR_identitas = True
            Catch ex As Exception
                InsertDataR_identitas = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataR_Identitas(ByVal entity As R_IDENTITAS_PASIEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataR_Identitas = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.db.R_IDENTITAS_PASIENs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_PASIENs.InsertOnSubmit(entity)
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

                UpdateDataR_Identitas = True
            Catch ex As Exception
                UpdateDataR_Identitas = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_H, ByVal entityKunjungan As S_PENDAFTARAN_KUNJUNGAN, ByVal entityPenanggungJawab As S_PENDAFTARAN_PENANGGUNGJAWAB, ByVal entityR_Identitas As R_IDENTITAS_PASIEN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                If entity.CATEGORY = 0 Then
                    sMODUL = "RJ"
                Else
                    sMODUL = "RI"
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

                    entity.KDPENDAFTARAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    entityKunjungan.KDPENDAFTARAN = entity.KDPENDAFTARAN


                    Dim sLASTNUMBERKUNJUNGAN As Integer = 0

                    For Each iLoop In oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                        sLASTNUMBERKUNJUNGAN += 1
                    Next

                    entityKunjungan.KDKUNJUNGAN = entity.KDPENDAFTARAN & "-" & entity.KDDEPARTMENT & "." & sLASTNUMBERKUNJUNGAN

                    If entityPenanggungJawab IsNot Nothing Then
                        entityPenanggungJawab.KDPENDAFTARAN = entity.KDPENDAFTARAN
                    End If

                    entityR_Identitas.KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN
                    entityR_Identitas.KDPENDAFTARAN = entity.KDPENDAFTARAN
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.S_PENDAFTARAN_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANs.InsertOnSubmit(entityKunjungan)

                    If entityPenanggungJawab IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.InsertOnSubmit(entityPenanggungJawab)
                    End If

                    If entityR_Identitas IsNot Nothing Then
                        oConnection.db.R_IDENTITAS_PASIENs.InsertOnSubmit(entityR_Identitas)
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

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    Dim oKoneksi As New Brigging.clsSetKoneksi
                    oKoneksi.UpdatePemetaan(False, entity.KDPENDAFTARAN, entity.KDUPDATE_APLICARE, entity.M_CUSTOMER.KDJENISKELAMIN)
                Catch ex As Exception
                    oError.InsertData("PEMETAAN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If entity.CATEGORY = 1 Then
                    Try
                        UpdatePendaftaranRI(entity.KDPENDAFTARAN_AWAL, entity.KDPENDAFTARAN)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

                InsertData = entity.KDPENDAFTARAN

                Dim dsList = GetStructureHeaderList()
                With dsList
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATECREATED
                    .KDIDENTITAS = 0
                    .CATEGORY = entityKunjungan.S_PENDAFTARAN_H.CATEGORY
                    .DATE = entity.DATE
                    .KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN
                    .KDDAFTAR_L1 = entityKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L1
                    .KDDAFTAR_L1_NAMA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                    .KDCUSTOMER = entityKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                    .NAMAPASIEN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    .KDDOCTOR = entityKunjungan.KDDOCTOR
                    .KDDOCTOR_NAMA = entityKunjungan.M_DOCTOR.NAME_DISPLAY
                    .KDDEPARTMENT = entityKunjungan.KDDEPARTMENT
                    .KDDEPARTMENT_NAMA = entityKunjungan.M_DEPARTMENT.NAME_DISPLAY
                    .CATATAN = ""
                    .STATUSWARNA = ""
                    .TANGGALLAHIR = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                    .JENISKELAMIN = IIf(entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                    .UMUR = GetUmurPasien(.DATE, .TANGGALLAHIR)
                    .PANGKAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                    .KESATUAN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                    .KELAS = entityKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                    .JENISPESERTA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                    .ALAMAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                    .AGAMA = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                    .NIK = entityKunjungan.S_PENDAFTARAN_H.KTP
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                End With

                InsertDataListRME(dsList, entityKunjungan)
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_H, ByVal entityKunjungan As S_PENDAFTARAN_KUNJUNGAN, ByVal entityPenanggungJawab As S_PENDAFTARAN_PENANGGUNGJAWAB, ByVal entityR_Identitas As R_IDENTITAS_PASIEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN)
                Dim dsPenanggunJawab = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                Dim dsIdentitas = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN)

                Try
                    oConnection.db.S_PENDAFTARAN_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_Hs.InsertOnSubmit(entity)

                    If dsKunjungan IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANs.DeleteOnSubmit(dsKunjungan)
                    End If

                    oConnection.db.S_PENDAFTARAN_KUNJUNGANs.InsertOnSubmit(entityKunjungan)

                    If dsPenanggunJawab IsNot Nothing Then
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.DeleteOnSubmit(dsPenanggunJawab)
                        oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.InsertOnSubmit(entityPenanggungJawab)
                    End If
                    If dsIdentitas IsNot Nothing Then
                        oConnection.db.R_IDENTITAS_PASIENs.DeleteOnSubmit(dsIdentitas)
                        oConnection.db.R_IDENTITAS_PASIENs.InsertOnSubmit(entityR_Identitas)
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

                Try
                    Dim oKoneksi As New Brigging.clsSetKoneksi
                    oKoneksi.UpdatePemetaan(False, entity.KDPENDAFTARAN, entity.KDUPDATE_APLICARE, entity.M_CUSTOMER.KDJENISKELAMIN)
                    oKoneksi.UpdatePemetaan(True, ds.KDPENDAFTARAN, ds.KDUPDATE_APLICARE, ds.M_CUSTOMER.KDJENISKELAMIN)
                Catch ex As Exception
                    oError.InsertData("PEMETAAN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If ds.CATEGORY = 1 Then
                    Try

                        UpdatePendaftaranRI(ds.KDPENDAFTARAN_AWAL, "")
                        UpdatePendaftaranRI(entity.KDPENDAFTARAN_AWAL, entity.KDPENDAFTARAN)

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                UpdateData = True

                Dim dsList = GetStructureHeaderList()
                With dsList
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATECREATED
                    .KDIDENTITAS = 0
                    .CATEGORY = entityKunjungan.S_PENDAFTARAN_H.CATEGORY
                    .DATE = entity.DATE
                    .KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN
                    .KDDAFTAR_L1 = entityKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L1
                    .KDDAFTAR_L1_NAMA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                    .KDCUSTOMER = entityKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                    .NAMAPASIEN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    .KDDOCTOR = entityKunjungan.KDDOCTOR
                    .KDDOCTOR_NAMA = entityKunjungan.M_DOCTOR.NAME_DISPLAY
                    .KDDEPARTMENT = entityKunjungan.KDDEPARTMENT
                    .KDDEPARTMENT_NAMA = entityKunjungan.M_DEPARTMENT.NAME_DISPLAY
                    .CATATAN = ""
                    .STATUSWARNA = ""
                    .TANGGALLAHIR = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                    .JENISKELAMIN = IIf(entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                    .UMUR = GetUmurPasien(.DATE, .TANGGALLAHIR)
                    .PANGKAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                    .KESATUAN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                    .KELAS = entityKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                    .JENISPESERTA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                    .ALAMAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                    .AGAMA = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                    .NIK = entityKunjungan.S_PENDAFTARAN_H.KTP
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                End With

                InsertDataListRME(dsList, entityKunjungan)
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter1 As String, ByVal Parameter2 As String, ByVal Parameter3 As String, ByVal Parameter4 As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter1
                sSTATUS = "DELETE"
                Dim listKDKUNJUNGAN As New List(Of String)

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter1)
                Dim dsKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = Parameter1)
                Dim dsSKD = oConnection.db.S_PENDAFTARAN_SKDs.FirstOrDefault(Function(x) x.KDSKD = Parameter3)
                Dim dsPenanggunJawab = oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter4)
                Dim dsIdentitas = oConnection.db.R_IDENTITAS_PASIENs.Where(Function(x) x.KDPENDAFTARAN = Parameter1)

                If ds IsNot Nothing Then
                    Try
                        For Each xloop In dsKunjungan
                            listKDKUNJUNGAN.Add(xloop.KDKUNJUNGAN)
                            Dim dsKunjunganDelete = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = xloop.KDKUNJUNGAN)
                            oConnection.db.S_PENDAFTARAN_KUNJUNGANs.DeleteOnSubmit(dsKunjunganDelete)
                        Next
                        For Each xloop In dsIdentitas
                            Dim dsIdentitasDelete = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = xloop.KDKUNJUNGAN)
                            oConnection.db.R_IDENTITAS_PASIENs.DeleteOnSubmit(dsIdentitasDelete)
                        Next
                        If dsSKD IsNot Nothing Then
                            oConnection.db.S_PENDAFTARAN_SKDs.DeleteOnSubmit(dsSKD)
                        End If
                        If dsPenanggunJawab IsNot Nothing Then
                            oConnection.db.S_PENDAFTARAN_PENANGGUNGJAWABs.DeleteOnSubmit(dsPenanggunJawab)
                        End If

                        oConnection.db.S_PENDAFTARAN_Hs.DeleteOnSubmit(ds)

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

                Try
                    Dim oKoneksi As New Brigging.clsSetKoneksi
                    oKoneksi.UpdatePemetaan(True, ds.KDPENDAFTARAN, ds.KDUPDATE_APLICARE, ds.M_CUSTOMER.KDJENISKELAMIN)
                Catch ex As Exception
                    oError.InsertData("PEMETAAN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If ds.CATEGORY = 1 Then
                    Try

                        UpdatePendaftaranRI(ds.KDPENDAFTARAN_AWAL, "")

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                DeleteData = True

                For Each xloop In listKDKUNJUNGAN
                    UpdateListMedrek(xloop)
                Next
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataListRME(ByVal entity As A_IDENTITASPASIEN_LIST, ByVal entityKunjungan As S_PENDAFTARAN_KUNJUNGAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertDataListRME = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Dim ds = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                If ds Is Nothing Then
                    Try
                        oConnection.dbRME.A_IDENTITASPASIEN_LISTs.InsertOnSubmit(entity)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                Else
                    Try
                        ds.DATECREATED = entity.DATECREATED
                        ds.DATEUPDATED = entity.DATECREATED
                        ds.CATEGORY = entityKunjungan.S_PENDAFTARAN_H.CATEGORY
                        ds.DATE = entity.DATE
                        ds.KDKUNJUNGAN = entityKunjungan.KDKUNJUNGAN
                        ds.KDDAFTAR_L1 = entityKunjungan.S_PENDAFTARAN_H.KDDAFTAR_L1
                        ds.KDDAFTAR_L1_NAMA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                        ds.KDCUSTOMER = entityKunjungan.S_PENDAFTARAN_H.KDCUSTOMER
                        ds.NAMAPASIEN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                        ds.KDDOCTOR = entityKunjungan.KDDOCTOR
                        ds.KDDOCTOR_NAMA = entityKunjungan.M_DOCTOR.NAME_DISPLAY
                        ds.KDDEPARTMENT = entityKunjungan.KDDEPARTMENT
                        ds.KDDEPARTMENT_NAMA = entityKunjungan.M_DEPARTMENT.NAME_DISPLAY
                        ds.STATUSWARNA = ds.STATUSWARNA
                        ds.CATATAN = ds.CATATAN
                        ds.TANGGALLAHIR = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                        ds.JENISKELAMIN = IIf(entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                        ds.UMUR = GetUmurPasien(entityKunjungan.S_PENDAFTARAN_H.DATE, entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR)
                        ds.PANGKAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                        ds.KESATUAN = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                        ds.KELAS = entityKunjungan.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                        ds.JENISPESERTA = entityKunjungan.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                        ds.ALAMAT = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                        ds.AGAMA = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                        ds.NIK = entityKunjungan.S_PENDAFTARAN_H.KTP
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataListRME = True

            Catch ex As Exception
                InsertDataListRME = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataListRME(ByVal entity As A_IDENTITASPASIEN_LIST) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertDataListRME = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Try
                    oConnection.dbRME.A_IDENTITASPASIEN_LISTs.InsertOnSubmit(entity)
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataListRME = True

            Catch ex As Exception
                InsertDataListRME = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateListMedrek(ByVal kdkunjungan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateListMedrek = False
                    Exit Function
                End If

                UpdateListMedrek = True

                Dim ds = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = kdkunjungan)

                If ds IsNot Nothing Then
                    ds.DATEUPDATED = Now
                    ds.STATUSWARNA = "BATAL"

                    oConnection.db.SubmitChanges()
                End If


            Catch ex As Exception
                UpdateListMedrek = False
                Throw ex
            End Try
        End Function
        Public Function Daftar_L1_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L1_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L1s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L1_Default = ds.KDDAFTAR_L1
                Else
                    Daftar_L1_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L1_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L2_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L2_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L2s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L2_Default = ds.KDDAFTAR_L2
                Else
                    Daftar_L2_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L2_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L3_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L3_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L3s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L3_Default = ds.KDDAFTAR_L3
                Else
                    Daftar_L3_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L3_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L4_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L4_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L4s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L4_Default = ds.KDDAFTAR_L4
                Else
                    Daftar_L4_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L4_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_L5_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_L5_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DAFTAR_L5s.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_L5_Default = ds.KDDAFTAR_L5
                Else
                    Daftar_L5_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_L5_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Depatment_Default(ByVal sKDDOCTOR As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    Depatment_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DOCTORs.FirstOrDefault(Function(x) x.KDDOCTOR = sKDDOCTOR)
                If ds IsNot Nothing Then
                    Depatment_Default = ds.KDDEPARTMENT
                Else
                    Depatment_Default = String.Empty
                End If
            Catch ex As Exception
                Depatment_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Diagnosa_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Diagnosa_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_DIAGNOSAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Diagnosa_Default = ds.KDDIAGNOSA
                Else
                    Diagnosa_Default = String.Empty
                End If
            Catch ex As Exception
                Diagnosa_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_COB_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_COB_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_COBs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_COB_Default = ds.KDCOB
                Else
                    Daftar_COB_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_COB_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_PPK_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_PPK_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PPKs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_PPK_Default = ds.KDPPK
                Else
                    Daftar_PPK_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_PPK_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function Daftar_KELASRAWAT_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    Daftar_KELASRAWAT_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELASRAWATs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Daftar_KELASRAWAT_Default = ds.KDKELASRAWAT
                Else
                    Daftar_KELASRAWAT_Default = String.Empty
                End If
            Catch ex As Exception
                Daftar_KELASRAWAT_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UpdateDataBatal(ByVal kdpendaftaran As String, ByVal kduser As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataBatal = False
                    Exit Function
                End If

                UpdateDataBatal = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                If ds.STATUSDAFTAR = 0 Then
                    ds.STATUSDAFTAR = 1
                    ds.KDUSER = kduser
                Else
                    ds.STATUSDAFTAR = 0
                    ds.KDUSER = kduser
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataBatal = False
                Throw ex
            End Try
        End Function
        Public Function UpdateSEPTransaksi(ByVal kdpendaftaran As String, ByVal nomorsep As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateSEPTransaksi = False
                    Exit Function
                End If

                UpdateSEPTransaksi = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.NOMORSEP = nomorsep

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateSEPTransaksi = False
                Throw ex
            End Try
        End Function
        Public Function UpdateSEPTransaksiGrouper(ByVal kdpendaftaran As String, ByVal nomorsep As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateSEPTransaksiGrouper = False
                    Exit Function
                End If

                UpdateSEPTransaksiGrouper = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.norec = kdpendaftaran)

                ds.noSep = nomorsep

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateSEPTransaksiGrouper = False
                Throw ex
            End Try
        End Function
        Public Function UpdateSEP2(ByVal kdpendaftaran As String, ByVal nomorsep As String, ByVal sReq As String, ByVal sRespon As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateSEP2 = False
                    Exit Function
                End If

                UpdateSEP2 = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.NOMORSEP = nomorsep
                ds.REQUEST = sReq
                ds.RESPON = sRespon

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateSEP2 = False
                Throw ex
            End Try
        End Function
        Public Function UpdatePendaftaranRI(ByVal kdpendaftaran As String, ByVal kdpendaftaran_ As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePendaftaranRI = False
                    Exit Function
                End If

                UpdatePendaftaranRI = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                If ds IsNot Nothing Then
                    ds.KDPENDAFTARAN_AWAL = kdpendaftaran_
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatePendaftaranRI = False
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal kdpendaftaran As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                UpdateCetak = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.CETAK += 1

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateCetak = False
                Throw ex
            End Try
        End Function
        Public Function UpdateSEP(ByVal kdpendaftaran As String, ByVal nomorsep As String, ByVal sREQ As String, ByVal sRESPONSE As String, ByVal KDUSER As String, ByVal sKDSKDP As String, ByVal sINFORMASIPRB As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateSEP = False
                    Exit Function
                End If

                UpdateSEP = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                ds.DATEUPDATED = Now
                ds.NOMORSEP = nomorsep
                ds.REQUEST = sREQ
                ds.RESPON = sRESPONSE
                ds.KDUSER = KDUSER
                ds.NOMORSKDP = sKDSKDP
                ds.ISOFFLINE = False
                ds.INFORMASIPRB = sINFORMASIPRB

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateSEP = False
                Throw ex
            End Try
        End Function
        'Public Function UpdateKodeBooking(ByVal kdpendaftaran As String, ByVal kodebooking As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateKodeBooking = False
        '            Exit Function
        '        End If

        '        UpdateKodeBooking = True

        '        Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

        '        ds.DATEUPDATED = Now
        '        ds.KODEBOOKING = kodebooking

        '        Dim dsNomor = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = kodebooking)

        '        If dsNomor IsNot Nothing Then
        '            ds.KDBOOKING = dsNomor.NOMORANTREAN
        '        End If

        '        oConnection.db.SubmitChanges()

        '    Catch ex As Exception
        '        UpdateKodeBooking = False
        '        Throw ex
        '    End Try
        'End Function
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.S_PENDAFTARAN_Hs

        '        For Each iLoop In entity
        '            Dim sKDPENDAFTARAN = iLoop.KDPENDAFTARAN
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace