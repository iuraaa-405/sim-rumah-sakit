Imports DataAccess

Namespace Grouper
    Public Class clsR_CPPT
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing
        Private oData As New Grouper.clsR_Identitas_Grouper_Data
        Private oDataFormulir As New Grouper.clsR_Identitas_Grouper_Formulir

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "CPPT"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_CPPT
        End Function
        Public Function GetStructureDetailDiagnosaList() As List(Of R_CPPT_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosaList = Nothing
            End If
            GetStructureDetailDiagnosaList = New List(Of R_CPPT_DIAGNOSA)
        End Function
        Public Function GetStructureDetailDiagnosa() As R_CPPT_DIAGNOSA
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailDiagnosa = Nothing
            End If
            GetStructureDetailDiagnosa = New R_CPPT_DIAGNOSA
        End Function
        Public Function GetStructureDetailTindakanList() As List(Of R_CPPT_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanList = Nothing
            End If
            GetStructureDetailTindakanList = New List(Of R_CPPT_PROSEDUR)
        End Function
        Public Function GetStructureDetailTindakan() As R_CPPT_PROSEDUR
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakan = Nothing
            End If
            GetStructureDetailTindakan = New R_CPPT_PROSEDUR
        End Function
        Public Function GetStructureDetailTindakanTerjadwalList() As List(Of R_ORDER_TERJADWAL)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanTerjadwalList = Nothing
            End If
            GetStructureDetailTindakanTerjadwalList = New List(Of R_ORDER_TERJADWAL)
        End Function
        Public Function GetStructureDetailTindakanTerjadawal() As R_ORDER_TERJADWAL
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailTindakanTerjadawal = Nothing
            End If
            GetStructureDetailTindakanTerjadawal = New R_ORDER_TERJADWAL
        End Function
        Public Function GetStructureDetailNonRacikanList() As List(Of R_CPPT_NONRACIKAN)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailNonRacikanList = Nothing
            End If
            GetStructureDetailNonRacikanList = New List(Of R_CPPT_NONRACIKAN)
        End Function
        Public Function GetStructureDetailNonRacikan() As R_CPPT_NONRACIKAN
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailNonRacikan = Nothing
            End If
            GetStructureDetailNonRacikan = New R_CPPT_NONRACIKAN
        End Function
        Public Function GetStructureDetailRacikanList() As List(Of R_CPPT_RACIKAN)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailRacikanList = Nothing
            End If
            GetStructureDetailRacikanList = New List(Of R_CPPT_RACIKAN)
        End Function
        Public Function GetStructureDetailRacikan() As R_CPPT_RACIKAN
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailRacikan = Nothing
            End If
            GetStructureDetailRacikan = New R_CPPT_RACIKAN
        End Function
        Public Function GetData() As List(Of R_CPPT)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CPPTs.OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataListNakes(ByVal NoRM As String) As List(Of R_CPPT)
            If Not oConnection.GetConnectionRME() Then
                GetDataListNakes = Nothing
                Exit Function
            End If
            GetDataListNakes = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = NoRM And x.KDPROFESI <> "PROFESI_0000000001").OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetDataListMedis(ByVal NoRM As String) As List(Of R_CPPT)
            If Not oConnection.GetConnectionRME() Then
                GetDataListMedis = Nothing
                Exit Function
            End If
            GetDataListMedis = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = NoRM And x.KDPROFESI = "PROFESI_0000000001").OrderByDescending(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)
        End Function
        Public Function GetDataIdentitas(ByVal Parameter As Integer) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDataIdentitas = Nothing
                Exit Function
            End If
            GetDataIdentitas = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDIDENTITAS = Parameter)
        End Function
        Public Function GetDataByKdKunjunganCPPT(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKdKunjunganCPPT = Nothing
                Exit Function
            End If
            GetDataByKdKunjunganCPPT = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDataByKdKunjunganCPPTPerawat(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKdKunjunganCPPTPerawat = Nothing
                Exit Function
            End If
            GetDataByKdKunjunganCPPTPerawat = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter And x.KDPROFESI = "PROFESI_0000000003")
        End Function
        Public Function GetDataByKdKunjunganCPPTDokter(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKdKunjunganCPPTDokter = Nothing
                Exit Function
            End If
            GetDataByKdKunjunganCPPTDokter = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter And x.KDPROFESI = "PROFESI_0000000001")
        End Function
        Public Function GetDataByKdKunjungan(ByVal Parameter As String) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDataByKdKunjungan = Nothing
                Exit Function
            End If
            GetDataByKdKunjungan = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        'Public Function GetDatakodegrouperanddokterRawatJalan(ByVal Parameter As String) As R_CPPT
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDatakodegrouperanddokterRawatJalan = Nothing
        '        Exit Function
        '    End If
        '    GetDatakodegrouperanddokterRawatJalan = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter And x.R_CPPT_M_PROFESI.MEMO = "DOKTER" And x.A_IDENTITASPASIEN_LIST.CATEGORY = 0)
        'End Function
        Public Function GetDataValidasiRawatJalanUntukResume(ByVal Parameter As Integer) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataValidasiRawatJalanUntukResume = Nothing
                Exit Function
            End If
            GetDataValidasiRawatJalanUntukResume = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.KDIDENTITAS = Parameter And x.R_CPPT_M_PROFESI.MEMO = "DOKTER" And x.A_IDENTITASPASIEN_LIST.CATEGORY = 0 And x.ISDELETE = False)
        End Function
        Public Function GetDataCPPTByIdentitas(ByVal Parameter As Integer) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataCPPTByIdentitas = Nothing
                Exit Function
            End If
            GetDataCPPTByIdentitas = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.KDIDENTITAS = Parameter And x.R_CPPT_M_PROFESI.MEMO = "DOKTER" And x.ISDELETE = False)
        End Function
        Public Function GetDataIdentitasList(ByVal Parameter As Integer) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDataIdentitasList = Nothing
                Exit Function
            End If
            GetDataIdentitasList = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDIDENTITAS = Parameter)
        End Function
        'Public Function GetDataKunjunganFirst(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataKunjunganFirst = Nothing
        '        Exit Function
        '    End If
        '    GetDataKunjunganFirst = oConnection.dbRME.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        'End Function
        Public Function GetDataByRmTerakhirTTV(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByRmTerakhirTTV = Nothing
                Exit Function
            End If
            GetDataByRmTerakhirTTV = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = Parameter).OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByRmTerakhirTTVAlergi(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByRmTerakhirTTVAlergi = Nothing
                Exit Function
            End If
            GetDataByRmTerakhirTTVAlergi = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = Parameter And x.SUBJEKTIF_ALERGI_YA = True And x.R_CPPT_M_PROFESI.MEMO = "DOKTER").OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByRMUntukDokter(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByRMUntukDokter = Nothing
                Exit Function
            End If
            GetDataByRMUntukDokter = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = Parameter And x.R_CPPT_M_PROFESI.MEMO.Contains("DOKTER")).OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByKodeKunjunganTerakhirTTVPerawat(ByVal Parameter As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodeKunjunganTerakhirTTVPerawat = Nothing
                Exit Function
            End If
            GetDataByKodeKunjunganTerakhirTTVPerawat = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter And Not x.R_CPPT_M_PROFESI.MEMO.Contains("DOKTER")).OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByKodeKunjungandanKodeProfesiTerakhirTTVPerawat(ByVal Parameter1 As String, ByVal Parameter2 As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodeKunjungandanKodeProfesiTerakhirTTVPerawat = Nothing
                Exit Function
            End If
            GetDataByKodeKunjungandanKodeProfesiTerakhirTTVPerawat = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = Parameter1 And Not x.KDPROFESI = Parameter2).OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByKodePendaftaranRawatJalanDokter(ByVal Parameter1 As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodePendaftaranRawatJalanDokter = Nothing
                Exit Function
            End If
            GetDataByKodePendaftaranRawatJalanDokter = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.A_IDENTITASPASIEN_LIST.KDPENDAFTARAN = Parameter1 And x.KDPROFESI = "PROFESI_0000000001")
        End Function
        Public Function GetDataByRmTerakhirByUserAndDokter(ByVal Parameter1 As String, ByVal Parameter2 As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByRmTerakhirByUserAndDokter = Nothing
                Exit Function
            End If
            GetDataByRmTerakhirByUserAndDokter = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = Parameter1 And x.KDUSER = Parameter2 And x.R_CPPT_M_PROFESI.MEMO = "DOKTER").OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataByRmTerakhirByDokter(ByVal Parameter1 As String, ByVal Parameter2 As String) As R_CPPT
            If Not oConnection.GetConnectionRME() Then
                GetDataByRmTerakhirByDokter = Nothing
                Exit Function
            End If
            GetDataByRmTerakhirByDokter = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = Parameter1 And x.A_IDENTITASPASIEN_LIST.KDDOCTOR = Parameter2).OrderByDescending(Function(x) x.KDCPPT).FirstOrDefault()
        End Function
        Public Function GetDataDetailDiagnosa(ByVal sKDCPPT As String) As List(Of R_CPPT_DIAGNOSA)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.dbRME.R_CPPT_DIAGNOSAs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTindakan(ByVal sKDCPPT As String) As List(Of R_CPPT_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakan = Nothing
                Exit Function
            End If
            GetDataDetailTindakan = oConnection.dbRME.R_CPPT_PROSEDURs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTindakanByKdKunjungan(ByVal sKDKUNJUNGAN As String) As List(Of R_CPPT_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakanByKdKunjungan = Nothing
                Exit Function
            End If
            GetDataDetailTindakanByKdKunjungan = oConnection.dbRME.R_CPPT_PROSEDURs.Where(Function(x) x.R_CPPT.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = sKDKUNJUNGAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTindakanByKdPendaftaran(ByVal sKDPENDAFTARAN As String) As List(Of R_CPPT_PROSEDUR)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTindakanByKdPendaftaran = Nothing
                Exit Function
            End If
            GetDataDetailTindakanByKdPendaftaran = oConnection.dbRME.R_CPPT_PROSEDURs.Where(Function(x) x.R_CPPT.A_IDENTITASPASIEN_LIST.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailNonRacikan(ByVal sKDCPPT As String) As List(Of R_CPPT_NONRACIKAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailNonRacikan = Nothing
                Exit Function
            End If
            GetDataDetailNonRacikan = oConnection.dbRME.R_CPPT_NONRACIKANs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailRacikan(ByVal sKDCPPT As String) As List(Of R_CPPT_RACIKAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailRacikan = Nothing
                Exit Function
            End If
            GetDataDetailRacikan = oConnection.dbRME.R_CPPT_RACIKANs.Where(Function(x) x.KDCPPT = sKDCPPT).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTerjadwal(ByVal sKDCPPT As String) As List(Of R_ORDER_TERJADWAL)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailTerjadwal = Nothing
                Exit Function
            End If
            GetDataDetailTerjadwal = oConnection.dbRME.R_ORDER_TERJADWALs.Where(Function(x) x.KDORDER = sKDCPPT).OrderBy(Function(x) x.DATECREATED).ToList()
        End Function
        Public Function GetDataByRekamMedis(ByVal snoRm As String) As List(Of R_CPPT)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRekamMedis = Nothing
                Exit Function
            End If
            GetDataByRekamMedis = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = snoRm And x.ISDELETE = False).OrderByDescending(Function(x) x.DATE).ToList()
        End Function
        'Public Function GetDataByNoRecRawatJalanTerakhirDokter(ByVal snorec As String) As R_CPPT
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataByNoRecRawatJalanTerakhirDokter = Nothing
        '        Exit Function
        '    End If
        '    GetDataByNoRecRawatJalanTerakhirDokter = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.R_IDENTITAS_GROUPER.norec = snorec And x.ISDELETE = True And x.M_PROFESI.MEMO = "DOKTER" And x.R_IDENTITAS_GROUPER.jnsPelayanan = "R.Jalan")
        'End Function
        Public Function GetDataDetailCPPTByKDKUNJUNGAN(ByVal sKDKUNJUNGAN As String) As List(Of R_CPPT)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailCPPTByKDKUNJUNGAN = Nothing
                Exit Function
            End If
            GetDataDetailCPPTByKDKUNJUNGAN = oConnection.dbRME.R_CPPTs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = sKDKUNJUNGAN).OrderBy(Function(x) x.KDCPPT).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_CPPT, ByVal entityDiagnosa As List(Of R_CPPT_DIAGNOSA), ByVal entityTindakan As List(Of R_CPPT_PROSEDUR), ByVal entityNonRacikan As List(Of R_CPPT_NONRACIKAN), ByVal entityRacikan As List(Of R_CPPT_RACIKAN), ByVal entityOrderTerjadwal As List(Of R_ORDER_TERJADWAL)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "INSERT"

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDCPPT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDiagnosa
                        iLoop.DATECREATED = entity.DATECREATED
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                        iLoop.KDCPPT = entity.KDCPPT
                    Next
                    For Each iLoop In entityTindakan
                        iLoop.DATECREATED = entity.DATECREATED
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                        iLoop.KDCPPT = entity.KDCPPT
                    Next
                    For Each iLoop In entityNonRacikan
                        iLoop.DATECREATED = entity.DATECREATED
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                        iLoop.KDCPPT = entity.KDCPPT
                    Next
                    For Each iLoop In entityRacikan
                        iLoop.DATECREATED = entity.DATECREATED
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                        iLoop.KDCPPT = entity.KDCPPT
                    Next

                    If entityOrderTerjadwal IsNot Nothing Then
                        If entityOrderTerjadwal.Count > 0 Then
                            For Each iLoop In entityOrderTerjadwal
                                iLoop.DATECREATED = entity.DATECREATED
                                iLoop.DATEUPDATED = entity.DATEUPDATED
                                iLoop.KDORDER = entity.KDCPPT
                                iLoop.NOMORREFERENCE = entity.KDCPPT
                            Next
                        End If

                    End If

                    oConnection.dbRME.R_CPPTs.InsertOnSubmit(entity)

                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.R_CPPT_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityTindakan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_PROSEDURs.InsertAllOnSubmit(entityTindakan)
                    End If
                    If entityNonRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKANs.InsertAllOnSubmit(entityNonRacikan)
                    End If
                    If entityRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKANs.InsertAllOnSubmit(entityRacikan)
                    End If
                    If entityOrderTerjadwal IsNot Nothing Then
                        If entityOrderTerjadwal.Count > 0 Then
                            oConnection.dbRME.R_ORDER_TERJADWALs.InsertAllOnSubmit(entityOrderTerjadwal)
                        End If
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))

                InsertData = entity.KDCPPT

                'Try
                '    Dim dsFormulir = oDataFormulir.GetStructureHeader
                '    With dsFormulir
                '        .DATECREATED = entity.DATECREATED
                '        .DATEUPDATED = entity.DATEUPDATED
                '        .KODE = 0
                '        .kodegrouper = entity.kodegrouper
                '        .KDFORMULIR = entity.KDCPPT
                '        .NAMAFORMULIR = "CPPT"
                '        .AKSI = "ADD"
                '        .DESCRIPTION = "CPPT " & entity.R_IDENTITAS_GROUPER.jnsPelayanan.Trim.ToUpper
                '        .KDUSER = entity.KDUSER
                '    End With

                '    oDataFormulir.InsertData(dsFormulir)
                'Catch ex As Exception

                'End Try
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataAsesmen(ByVal entity As R_CPPT, ByVal entityDiagnosa As List(Of R_CPPT_DIAGNOSA), ByVal entityTindakan As List(Of R_CPPT_PROSEDUR), ByVal entityNonRacikan As List(Of R_CPPT_NONRACIKAN), ByVal entityRacikan As List(Of R_CPPT_RACIKAN), ByVal entityOrderTerjadwal As List(Of R_ORDER_TERJADWAL)) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertDataAsesmen = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "INSERT"

                Try
                    'Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    'entity.DATECREATED = WaktuServer
                    'entity.DATEUPDATED = WaktuServer

                    'sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    'If sLASTNUMBER = 0 Then
                    '    Try
                    '        oCounter.InsertData(sMODUL, entity.DATECREATED)
                    '        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    '    Catch ex As Exception
                    '        sLASTNUMBER = 0
                    '    End Try
                    'End If

                    'entity.KDCPPT = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    'For Each iLoop In entityDiagnosa
                    '    iLoop.DATECREATED = entity.DATECREATED
                    '    iLoop.DATEUPDATED = entity.DATEUPDATED
                    '    iLoop.KDCPPT = entity.KDCPPT
                    'Next
                    'For Each iLoop In entityTindakan
                    '    iLoop.DATECREATED = entity.DATECREATED
                    '    iLoop.DATEUPDATED = entity.DATEUPDATED
                    '    iLoop.KDCPPT = entity.KDCPPT
                    'Next
                    'For Each iLoop In entityNonRacikan
                    '    iLoop.DATECREATED = entity.DATECREATED
                    '    iLoop.DATEUPDATED = entity.DATEUPDATED
                    '    iLoop.KDCPPT = entity.KDCPPT
                    'Next
                    'For Each iLoop In entityRacikan
                    '    iLoop.DATECREATED = entity.DATECREATED
                    '    iLoop.DATEUPDATED = entity.DATEUPDATED
                    '    iLoop.KDCPPT = entity.KDCPPT
                    'Next

                    'If entityOrderTerjadwal IsNot Nothing Then
                    '    If entityOrderTerjadwal.Count > 0 Then
                    '        For Each iLoop In entityOrderTerjadwal
                    '            iLoop.DATECREATED = entity.DATECREATED
                    '            iLoop.DATEUPDATED = entity.DATEUPDATED
                    '            iLoop.KDORDER = entity.KDCPPT
                    '            iLoop.NOMORREFERENCE = entity.KDCPPT
                    '        Next
                    '    End If

                    'End If

                    oConnection.dbRME.R_CPPTs.InsertOnSubmit(entity)

                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.R_CPPT_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If entityTindakan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_PROSEDURs.InsertAllOnSubmit(entityTindakan)
                    End If
                    If entityNonRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKANs.InsertAllOnSubmit(entityNonRacikan)
                    End If
                    If entityRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKANs.InsertAllOnSubmit(entityRacikan)
                    End If
                    If entityOrderTerjadwal IsNot Nothing Then
                        If entityOrderTerjadwal.Count > 0 Then
                            oConnection.dbRME.R_ORDER_TERJADWALs.InsertAllOnSubmit(entityOrderTerjadwal)
                        End If
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))

                InsertDataAsesmen = entity.KDCPPT

                'Try
                '    Dim dsFormulir = oDataFormulir.GetStructureHeader
                '    With dsFormulir
                '        .DATECREATED = entity.DATECREATED
                '        .DATEUPDATED = entity.DATEUPDATED
                '        .KODE = 0
                '        .kodegrouper = entity.kodegrouper
                '        .KDFORMULIR = entity.KDCPPT
                '        .NAMAFORMULIR = "CPPT"
                '        .AKSI = "ADD"
                '        .DESCRIPTION = "CPPT " & entity.R_IDENTITAS_GROUPER.jnsPelayanan.Trim.ToUpper
                '        .KDUSER = entity.KDUSER
                '    End With

                '    oDataFormulir.InsertData(dsFormulir)
                'Catch ex As Exception

                'End Try
            Catch ex As Exception
                InsertDataAsesmen = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_CPPT, ByVal entityDiagnosa As List(Of R_CPPT_DIAGNOSA), ByVal entityTindakan As List(Of R_CPPT_PROSEDUR), ByVal entityNonRacikan As List(Of R_CPPT_NONRACIKAN), ByVal entityRacikan As List(Of R_CPPT_RACIKAN), ByVal entityOrderTerjadwal As List(Of R_ORDER_TERJADWAL)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "UPDATE"

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsDiagnosa = oConnection.dbRME.R_CPPT_DIAGNOSAs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsTindakan = oConnection.dbRME.R_CPPT_PROSEDURs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsNonRacikan = oConnection.dbRME.R_CPPT_NONRACIKANs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsRacikan = oConnection.dbRME.R_CPPT_RACIKANs.Where(Function(x) x.KDCPPT = entity.KDCPPT)
                Dim dsOrderTerjadwal = oConnection.dbRME.R_ORDER_TERJADWALs.Where(Function(x) x.KDORDER = entity.KDCPPT)

                If entityDiagnosa IsNot Nothing Then
                    For Each iLoop In entityDiagnosa
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                    Next
                End If

                If entityTindakan IsNot Nothing Then
                    For Each iLoop In entityTindakan
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                    Next
                End If
                If entityNonRacikan IsNot Nothing Then
                    For Each iLoop In entityNonRacikan
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                    Next
                End If
                If entityRacikan IsNot Nothing Then
                    For Each iLoop In entityRacikan
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                    Next
                End If
                If entityOrderTerjadwal IsNot Nothing Then
                    For Each iLoop In entityOrderTerjadwal
                        iLoop.DATEUPDATED = entity.DATEUPDATED
                    Next
                End If

                Try
                    oConnection.dbRME.R_CPPTs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CPPTs.InsertOnSubmit(entity)

                    If dsDiagnosa.Count > 0 Then
                        oConnection.dbRME.R_CPPT_DIAGNOSAs.DeleteAllOnSubmit(dsDiagnosa)
                    End If
                    If entityDiagnosa.Count > 0 Then
                        oConnection.dbRME.R_CPPT_DIAGNOSAs.InsertAllOnSubmit(entityDiagnosa)
                    End If
                    If dsTindakan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_PROSEDURs.DeleteAllOnSubmit(dsTindakan)
                    End If
                    If entityTindakan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_PROSEDURs.InsertAllOnSubmit(entityTindakan)
                    End If
                    If dsNonRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKANs.DeleteAllOnSubmit(dsNonRacikan)
                    End If
                    If entityNonRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKANs.InsertAllOnSubmit(entityNonRacikan)
                    End If
                    If dsRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKANs.DeleteAllOnSubmit(dsRacikan)
                    End If
                    If entityRacikan.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKANs.InsertAllOnSubmit(entityRacikan)
                    End If
                    If dsOrderTerjadwal.Count > 0 Then
                        oConnection.dbRME.R_ORDER_TERJADWALs.DeleteAllOnSubmit(dsOrderTerjadwal)
                    End If
                    If entityOrderTerjadwal IsNot Nothing Then
                        If entityOrderTerjadwal.Count > 0 Then
                            oConnection.dbRME.R_ORDER_TERJADWALs.InsertAllOnSubmit(entityOrderTerjadwal)
                        End If
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True

                'If UpdateData = True Then
                '    Try
                '        Dim dsFormulir = oDataFormulir.GetStructureHeader
                '        With dsFormulir
                '            .DATECREATED = entity.DATECREATED
                '            .DATEUPDATED = entity.DATEUPDATED
                '            .KODE = 0
                '            .kodegrouper = entity.kodegrouper
                '            .KDFORMULIR = entity.KDCPPT
                '            .NAMAFORMULIR = "CPPT"
                '            .AKSI = "UPDATE"
                '            .DESCRIPTION = "CPPT " & entity.R_IDENTITAS_GROUPER.jnsPelayanan.Trim.ToUpper
                '            .KDUSER = entity.KDUSER
                '        End With

                '        oDataFormulir.InsertData(dsFormulir)
                '    Catch ex As Exception

                '    End Try
                'End If
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.dbRME.R_CPPTs.FirstOrDefault(Function(x) x.KDCPPT = Parameter)

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEDELETE = WaktuServer
                    ds.ISDELETE = True
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                    'If DeleteData = True Then
                    '    Try
                    '        Dim dsFormulir = oDataFormulir.GetStructureHeader
                    '        With dsFormulir
                    '            .DATECREATED = ds.DATECREATED
                    '            .DATEUPDATED = WaktuServer
                    '            .KODE = 0
                    '            .kodegrouper = ds.kodegrouper
                    '            .KDFORMULIR = ds.KDCPPT
                    '            .NAMAFORMULIR = "CPPT"
                    '            .AKSI = "DELETE"
                    '            .DESCRIPTION = "CPPT " & ds.R_IDENTITAS_GROUPER.jnsPelayanan.Trim.ToUpper & " ALASAN DELETE " & sAlasanDelete
                    '            .KDUSER = ds.KDUSER
                    '        End With

                    '        oDataFormulir.InsertData(dsFormulir)
                    '    Catch ex As Exception

                    '    End Try
                    'End If
                End If

                'If DeleteData = True Then
                '    Dim dsAdd = oConnection.dbRME.R_IDENTITAS_GROUPER_FORMULIRs.FirstOrDefault(Function(x) x.KDFORMULIR = Parameter And x.AKSI = "ADD")
                '    If dsAdd IsNot Nothing Then
                '        dsAdd.AKSI = dsAdd.AKSI & " (DELETE)"
                '        oConnection.dbRME.SubmitChanges()
                '    End If
                'End If
            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsBaca(ByVal Parameter As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateIsBaca = False
                    Exit Function
                End If

                UpdateIsBaca = True

                Dim ds = oConnection.dbRME.R_CPPT_PROSEDURs.FirstOrDefault(Function(x) x.KDCPPT = Parameter And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.ISBACA = 1
                    oConnection.dbRME.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateIsBaca = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsBacaNonRacikan(ByVal Parameter As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateIsBacaNonRacikan = False
                    Exit Function
                End If

                UpdateIsBacaNonRacikan = True

                Dim ds = oConnection.dbRME.R_CPPT_NONRACIKANs.FirstOrDefault(Function(x) x.KDCPPT = Parameter And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.ISBACA = 1
                    oConnection.dbRME.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateIsBacaNonRacikan = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsBacaRacikan(ByVal Parameter As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateIsBacaRacikan = False
                    Exit Function
                End If

                UpdateIsBacaRacikan = True

                Dim ds = oConnection.dbRME.R_CPPT_RACIKANs.FirstOrDefault(Function(x) x.KDCPPT = Parameter And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.ISBACA = 1
                    oConnection.dbRME.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateIsBacaRacikan = False
                Throw ex
            End Try
        End Function
        Public Function Profesi_Default() As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    Profesi_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.dbRME.R_CPPT_M_PROFESIs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    Profesi_Default = ds.KDPROFESI
                Else
                    Profesi_Default = String.Empty
                End If
            Catch ex As Exception
                Profesi_Default = String.Empty
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
        Public Function UpdateDaftarL6(ByVal kdpendaftaran As String, ByVal kddaftarl6 As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDaftarL6 = False
                    Exit Function
                End If

                UpdateDaftarL6 = True

                Dim ds = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = kdpendaftaran)

                If ds IsNot Nothing Then
                    ds.KDDAFTAR_L6 = kddaftarl6
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDaftarL6 = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace