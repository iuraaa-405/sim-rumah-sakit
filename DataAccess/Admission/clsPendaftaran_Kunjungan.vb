Imports System.Threading

Namespace Admission
    Public Class clsPendaftaran_Kunjungan
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
            sMODUL = "KUNJUNGAN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_KUNJUNGAN
        End Function
        Private Function GetStructureHeaderList() As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeaderList = Nothing
            End If
            GetStructureHeaderList = New A_IDENTITASPASIEN_LIST
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDatabykd(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDatabykd = Nothing
                Exit Function
            End If
            GetDatabykd = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDatabyRegisterdanDeparment(ByVal Parameter1 As String, ByVal Parameter2 As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDatabyRegisterdanDeparment = Nothing
                Exit Function
            End If
            GetDatabyRegisterdanDeparment = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter1 And x.KDDEPARTMENT = Parameter2)
        End Function
        Public Function GetDatabyKodeKunjungan(ByVal Parameter As String) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDatabyKodeKunjungan = Nothing
                Exit Function
            End If
            GetDatabyKodeKunjungan = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDatabyIdentitas(ByVal Parameter As Integer) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDatabyIdentitas = Nothing
                Exit Function
            End If
            GetDatabyIdentitas = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDIDENTITAS = Parameter)
        End Function
        'Public Function GetDatabypendaftaran(ByVal Parameter As String) As S_PENDAFTARAN_H
        '    If Not oConnection.GetConnection() Then
        '        GetDatabypendaftaran = Nothing
        '        Exit Function
        '    End If
        '    GetDatabypendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        'End Function
        Public Function GetDataLast(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection Then
                GetDataLast = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
                      Select x).ToList()

            GetDataLast = ds.OrderByDescending(Function(x) x.DATE).FirstOrDefault()

        End Function
        Public Function GetDataByDate(ByVal sDATEFROM As DateTime, sDATETO As DateTime) As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetDataByDate = Nothing
                Exit Function
            End If
            GetDataByDate = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.S_PENDAFTARAN_H.DATE >= sDATEFROM.ToString("yyyy-MM-dd") & " 00:00:00" And x.S_PENDAFTARAN_H.DATE <= sDATETO.ToString("yyyy-MM-dd") & " 23:59:59").OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetDataListByKodePendaftaran(ByVal sKDPENDAFTARAN As String) As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection Then
                GetDataListByKodePendaftaran = Nothing
                Exit Function
            End If
            GetDataListByKodePendaftaran = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Private Function GetUmurPasien(ByVal dateNow As Date, ByVal tgllahir As Date) As String
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
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KUNJUNGAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Try
                    Dim sLASTNUMBERKUNJUNGAN As Integer = 0

                    For Each iLoop In oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                        sLASTNUMBERKUNJUNGAN += 1
                    Next

                    entity.KDKUNJUNGAN = entity.KDPENDAFTARAN & "-" & entity.KDDEPARTMENT & "." & sLASTNUMBERKUNJUNGAN

                    oConnection.db.S_PENDAFTARAN_KUNJUNGANs.InsertOnSubmit(entity)
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
                    Dim dsPendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN)
                    If dsPendaftaran IsNot Nothing Then
                        oKoneksi.UpdatePemetaan(True, dsPendaftaran.KDPENDAFTARAN, dsPendaftaran.KDUPDATE_APLICARE, dsPendaftaran.M_CUSTOMER.KDJENISKELAMIN)
                    End If
                    oKoneksi.UpdatePemetaan(False, entity.KDPENDAFTARAN, entity.KDUPDATE_APLICARE, entity.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN)
                Catch ex As Exception
                    oError.InsertData("PEMETAAN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True

                Dim dsList = GetStructureHeaderList()
                With dsList
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATECREATED
                    .KDIDENTITAS = 0
                    .CATEGORY = entity.S_PENDAFTARAN_H.CATEGORY
                    .DATE = entity.DATE
                    .KDKUNJUNGAN = entity.KDKUNJUNGAN
                    .KDDAFTAR_L1 = entity.S_PENDAFTARAN_H.KDDAFTAR_L1
                    .KDDAFTAR_L1_NAMA = entity.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                    .KDCUSTOMER = entity.S_PENDAFTARAN_H.KDCUSTOMER
                    .NAMAPASIEN = entity.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    .KDDOCTOR = entity.KDDOCTOR
                    .KDDOCTOR_NAMA = entity.M_DOCTOR.NAME_DISPLAY
                    .KDDEPARTMENT = entity.KDDEPARTMENT
                    .KDDEPARTMENT_NAMA = entity.M_DEPARTMENT.NAME_DISPLAY
                    .CATATAN = ""
                    .STATUSWARNA = ""
                    .TANGGALLAHIR = entity.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                    .JENISKELAMIN = IIf(entity.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                    .UMUR = GetUmurPasien(.DATE, .TANGGALLAHIR)
                    .PANGKAT = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                    .KESATUAN = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                    .KELAS = entity.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                    .JENISPESERTA = entity.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                    .ALAMAT = entity.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                    .NIK = entity.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                    .AGAMA = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                End With

                InsertDataListRME(dsList, entity)

            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_KUNJUNGAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_KUNJUNGANs.InsertOnSubmit(entity)

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
                    oKoneksi.UpdatePemetaan(False, entity.KDPENDAFTARAN, entity.KDUPDATE_APLICARE, entity.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN)
                    oKoneksi.UpdatePemetaan(True, ds.KDPENDAFTARAN, ds.KDUPDATE_APLICARE, ds.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN)
                Catch ex As Exception
                    oError.InsertData("PEMETAAN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True

                Dim dsList = GetStructureHeaderList()
                With dsList
                    .KDPENDAFTARAN = entity.KDPENDAFTARAN
                    .DATECREATED = entity.DATECREATED
                    .DATEUPDATED = entity.DATECREATED
                    .KDIDENTITAS = 0
                    .CATEGORY = entity.S_PENDAFTARAN_H.CATEGORY
                    .DATE = entity.DATE
                    .KDKUNJUNGAN = entity.KDKUNJUNGAN
                    .KDDAFTAR_L1 = entity.S_PENDAFTARAN_H.KDDAFTAR_L1
                    .KDDAFTAR_L1_NAMA = entity.S_PENDAFTARAN_H.M_DAFTAR_L1.MEMO
                    .KDCUSTOMER = entity.S_PENDAFTARAN_H.KDCUSTOMER
                    .NAMAPASIEN = entity.S_PENDAFTARAN_H.M_CUSTOMER.NAME_DISPLAY
                    .KDDOCTOR = entity.KDDOCTOR
                    .KDDOCTOR_NAMA = entity.M_DOCTOR.NAME_DISPLAY
                    .KDDEPARTMENT = entity.KDDEPARTMENT
                    .KDDEPARTMENT_NAMA = entity.M_DEPARTMENT.NAME_DISPLAY
                    .CATATAN = ""
                    .STATUSWARNA = ""
                    .TANGGALLAHIR = entity.S_PENDAFTARAN_H.M_CUSTOMER.TANGGALLAHIR
                    .JENISKELAMIN = IIf(entity.S_PENDAFTARAN_H.M_CUSTOMER.KDJENISKELAMIN = 1, "LAKI-LAKI", "PEREMPUAN")
                    .UMUR = GetUmurPasien(.DATE, .TANGGALLAHIR)
                    .PANGKAT = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_PANGKAT.MEMO
                    .KESATUAN = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_KESATUAN.MEMO
                    .KELAS = entity.S_PENDAFTARAN_H.M_KELASRAWAT.MEMO
                    .JENISPESERTA = entity.S_PENDAFTARAN_H.M_DAFTAR_L2.MEMO
                    .ALAMAT = entity.S_PENDAFTARAN_H.M_CUSTOMER.ALAMAT
                    .NIK = entity.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                    .AGAMA = entity.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
                End With

                InsertDataListRME(dsList, entity)
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String, ByVal KDKUNJUNGANPERTAMA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_KUNJUNGANs.DeleteOnSubmit(ds)
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

                UpdateListMedrek(Parameter, "BATAL")

            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
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

                ds.KDPENDAFTARAN_AWAL = kdpendaftaran_

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatePendaftaranRI = False
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
                        ds.NIK = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.KTP
                        ds.AGAMA = entityKunjungan.S_PENDAFTARAN_H.M_CUSTOMER.M_AGAMA.MEMO
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
        Public Function UpdateListMedrek(ByVal kdkunjungan As String, ByVal STATUSWRANA As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateListMedrek = False
                    Exit Function
                End If

                UpdateListMedrek = True

                Dim ds = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = kdkunjungan)

                If ds IsNot Nothing Then
                    ds.DATEUPDATED = Now
                    ds.STATUSWARNA = STATUSWRANA

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateListMedrek = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDokterKunjungan(ByVal kdkunjungan As String, ByVal KDDOCTOR As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDokterKunjungan = False
                    Exit Function
                End If

                UpdateDokterKunjungan = True

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = kdkunjungan)

                If ds IsNot Nothing Then
                    ds.KDDOCTOR = KDDOCTOR
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDokterKunjungan = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDokterKunjunganIdentitas(ByVal kdkunjungan As String, ByVal KDDOCTOR As String, ByVal KDDOCTOR_NAMA As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDokterKunjunganIdentitas = False
                    Exit Function
                End If

                UpdateDokterKunjunganIdentitas = True

                Dim ds = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = kdkunjungan)

                If ds IsNot Nothing Then
                    ds.KDDOCTOR = KDDOCTOR
                    ds.KDDOCTOR_NAMA = KDDOCTOR_NAMA
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDokterKunjunganIdentitas = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace