Imports System.Linq
Imports DataAccess

Namespace SettingAntrian
    Public Class clsSetAntrian
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "ANTRIANSIMPAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_BOOKING_ANTRIAN
        End Function
        Public Function GetStructureHeaderKeterangan() As SET_BOOKING_ANTRIAN_KETERANGAN
            If Not oConnection.GetConnection() Then
                GetStructureHeaderKeterangan = Nothing
            End If
            GetStructureHeaderKeterangan = New SET_BOOKING_ANTRIAN_KETERANGAN
        End Function
        Public Function GetStructureHeaderSisa() As SET_PANGGIL_SISA
            If Not oConnection.GetConnection() Then
                GetStructureHeaderSisa = Nothing
            End If
            GetStructureHeaderSisa = New SET_PANGGIL_SISA
        End Function
        Public Function GetStructureHeaderWaktuTunggu() As SET_WAKTUTUNGGU
            If Not oConnection.GetConnection() Then
                GetStructureHeaderWaktuTunggu = Nothing
            End If
            GetStructureHeaderWaktuTunggu = New SET_WAKTUTUNGGU
        End Function
        Public Function GetData() As List(Of SET_BOOKING_ANTRIAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_ANTRIANs.OrderBy(Function(x) x.KODEBOOKING).ToList()
        End Function
        Public Function GetData(ByVal sKODEBOOKING As String) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataKeterangan(ByVal sKODEBOOKING As String) As SET_BOOKING_ANTRIAN_KETERANGAN
            If Not oConnection.GetConnection() Then
                GetDataKeterangan = Nothing
                Exit Function
            End If
            GetDataKeterangan = oConnection.db.SET_BOOKING_ANTRIAN_KETERANGANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataPendaftaran(ByVal sKODEBOOKING As String) As S_PENDAFTARAN_H
            If Not oConnection.GetConnection() Then
                GetDataPendaftaran = Nothing
                Exit Function
            End If
            GetDataPendaftaran = oConnection.db.S_PENDAFTARAN_Hs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataBySKD(ByVal sKDSKD As String) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataBySKD = Nothing
                Exit Function
            End If
            GetDataBySKD = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KDSKD = sKDSKD)
        End Function
        Public Function GetData(ByVal sKODEBOOKING As String, ByVal sDATE As DateTime) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy"))
        End Function
        Public Function GetDataByKartuBPJSTanggal(ByVal sNOMORKARTU As String, ByVal sDATE As DateTime) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByKartuBPJSTanggal = Nothing
                Exit Function
            End If
            GetDataByKartuBPJSTanggal = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.NOMORKARTU = sNOMORKARTU And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL <> 99)
        End Function
        Public Function GetDataByRMTanggal(ByVal sNORM As String, ByVal sDATE As DateTime) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByRMTanggal = Nothing
                Exit Function
            End If
            GetDataByRMTanggal = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.NORM = sNORM And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL <> 99)
        End Function
        Public Function GetDataByKTPTanggal(ByVal sKTP As String, ByVal sDATE As DateTime) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByKTPTanggal = Nothing
                Exit Function
            End If
            GetDataByKTPTanggal = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.NIK = sKTP And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL <> 99)
        End Function
        Public Function GetDataByRMTanggalContains(ByVal sNORM As String, ByVal sDATE As DateTime) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByRMTanggalContains = Nothing
                Exit Function
            End If
            GetDataByRMTanggalContains = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.NORM.Contains(sNORM) And x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL <> 99)
        End Function
        Public Function GetDataSisaSetBooking(ByVal sDate As Date, ByVal sKODE As String, ByVal sISOLINE As Boolean) As Integer
            Try
                If Not oConnection.GetConnection Then
                    GetDataSisaSetBooking = Nothing
                    Exit Function
                End If

                GetDataSisaSetBooking = 1

                Dim ds = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.JENISPASIEN_RS = sKODE And x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy") And x.ISONLINE = sISOLINE And x.ISPANGGIL = 0)
                          Select x).ToList()

                GetDataSisaSetBooking = ds.Count()
            Catch ex As Exception
                GetDataSisaSetBooking = 0
            End Try
        End Function
        Public Function GetDataLasAntrianPanggil(ByVal sDate As Date, ByVal sKODE As String, ByVal sISOLINE As Boolean) As SET_BOOKING_ANTRIAN
            If Not oConnection.GetConnection Then
                GetDataLasAntrianPanggil = Nothing
                Exit Function
            End If

            If sKODE = "O" Then
                Dim ds = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy") And x.ISONLINE = sISOLINE And x.ISPANGGIL <= 1)
                          Select x).ToList()

                If ds.Count > 0 Then
                    GetDataLasAntrianPanggil = ds.OrderBy(Function(x) x.KODEBOOKING).FirstOrDefault()
                Else
                    GetDataLasAntrianPanggil = Nothing
                End If
            Else
                Dim ds = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.JENISPASIEN_RS = sKODE And x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy") And x.ISONLINE = sISOLINE And x.ISPANGGIL = 0)
                          Select x).ToList()

                If ds.Count > 0 Then
                    GetDataLasAntrianPanggil = ds.OrderBy(Function(x) x.KODEBOOKING).FirstOrDefault()
                Else
                    GetDataLasAntrianPanggil = Nothing
                End If
            End If
        End Function
        'Public Function GetDataAllData(ByVal sDate As Date, ByVal sKODE As String) As Decimal
        '    If Not oConnection.GetConnection Then
        '        GetDataAllData = 0
        '        Exit Function
        '    End If

        '    Dim dsAll As Decimal = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.JENISPASIEN_RS = sKODE And x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy"))).Count()
        '    Dim dsSudahDipanggil As Decimal = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.JENISPASIEN_RS = sKODE And x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy") And x.ISPANGGIL = 2)).Count()

        '    GetDataAllData = dsAll - dsSudahDipanggil

        'End Function
        Public Function GetDataSisaByKode(ByVal sKODE As String) As SET_PANGGIL_SISA
            If Not oConnection.GetConnection() Then
                GetDataSisaByKode = Nothing
                Exit Function
            End If
            GetDataSisaByKode = oConnection.db.SET_PANGGIL_SISAs.FirstOrDefault(Function(x) x.KODE = sKODE)
        End Function
        Public Function GetDataBelumPanggil(ByVal sDate As Date, ByVal sKODE As String) As Integer
            If Not oConnection.GetConnection Then
                GetDataBelumPanggil = 0
                Exit Function
            End If

            Try
                GetDataBelumPanggil = (From x In oConnection.db.SET_BOOKING_ANTRIANs.Where(Function(x) x.JENISPASIEN_RS = sKODE And x.TANGGALPERIKSA_TEXT = sDate.ToString("ddMMyyyy") And x.ISPANGGIL = 0 And x.KETERANGAN = "OFLINE")).Count()
            Catch ex As Exception
                GetDataBelumPanggil = 0
            End Try
        End Function
        Public Function GetDataBySaveWaktuTunggu(ByVal sKODEBOOKIING As String, ByVal sISPANGGIL As Integer) As SET_WAKTUTUNGGU
            If Not oConnection.GetConnection() Then
                GetDataBySaveWaktuTunggu = Nothing
                Exit Function
            End If
            GetDataBySaveWaktuTunggu = oConnection.db.SET_WAKTUTUNGGUs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKIING And x.ISPANGGIL = sISPANGGIL)
        End Function
        Public Function GetDataBySaveWaktuTungguLastKirimBPJS(ByVal sKODEBOOKIING As String) As SET_WAKTUTUNGGU
            If Not oConnection.GetConnection() Then
                GetDataBySaveWaktuTungguLastKirimBPJS = Nothing
                Exit Function
            End If
            GetDataBySaveWaktuTungguLastKirimBPJS = oConnection.db.SET_WAKTUTUNGGUs.Where(Function(x) x.KODEBOOKING = sKODEBOOKIING And x.REMARKS = "200-Ok").OrderByDescending(Function(x) x.ISPANGGIL).FirstOrDefault()
        End Function
        Public Function GetDataBySaveWaktuTungguLastKirimBPJSPasienLama(ByVal sKODEBOOKIING As String) As SET_WAKTUTUNGGU
            If Not oConnection.GetConnection() Then
                GetDataBySaveWaktuTungguLastKirimBPJSPasienLama = Nothing
                Exit Function
            End If
            GetDataBySaveWaktuTungguLastKirimBPJSPasienLama = oConnection.db.SET_WAKTUTUNGGUs.Where(Function(x) x.KODEBOOKING = sKODEBOOKIING And x.REMARKS = "200-Ok" And x.ISPANGGIL >= 3).OrderByDescending(Function(x) x.ISPANGGIL).FirstOrDefault()
        End Function
        Public Function InsertDataBookingNew(ByVal entity As SET_BOOKING_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataBookingNew = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_BOOKING_ANTRIANs.InsertOnSubmit(entity)
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

                InsertDataBookingNew = True

            Catch ex As Exception
                InsertDataBookingNew = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As SET_BOOKING_ANTRIAN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "INSERT"

                Dim oSetCounter As New SettingAntrian.clsSet_CounterAntrian
                'Dim dsSetConter = oSetCounter.GetData(entity.JENISPASIEN_RS)

                ' Counter Admisi

                sMODUL = entity.JENISPASIEN_RS

                Try
                    sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)

                    If sLASTNUMBER = 0 Then
                        Try
                            oSetCounter.InsertData(sMODUL, entity.TANGGALPERIKSA)
                            sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KODEBOOKING = AutoNumberAntrian(entity.JENISPASIEN_RS, sLASTNUMBER + 1, entity.TANGGALPERIKSA)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oSetCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.TANGGALPERIKSA), Month(entity.TANGGALPERIKSA), Year(entity.TANGGALPERIKSA))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                ' Counter Poli dan Dokter

                'sMODUL = IIf(entity.JENISPASIEN_RS = "B" Or entity.JENISPASIEN_RS = "D", "A" & entity.KODEPOLI, "B" & entity.KODEPOLI)
                'If entity.KODEPOLI <> "" Then
                '    sMODUL = entity.KODEPOLI

                '    Try
                '        sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)

                '        If sLASTNUMBER = 0 Then
                '            Try
                '                oSetCounter.InsertData(sMODUL, entity.DATECREATED)
                '                sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)
                '            Catch ex As Exception
                '                sLASTNUMBER = 0
                '            End Try
                '        End If

                '        entity.NOMORANTREAN = sMODUL & IIf(entity.JENISPASIEN_RS = "A", "A", "B") & "-" & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")
                '        entity.ANGKAANTREAN = sLASTNUMBER + 1

                '    Catch ex As Exception
                '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '        Throw ex
                '    End Try

                '    Try
                '        oSetCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Day(entity.TANGGALPERIKSA), Month(entity.TANGGALPERIKSA), Year(entity.TANGGALPERIKSA))
                '    Catch ex As Exception
                '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '        Throw ex
                '    End Try
                'End If

                'If entity.KODEDOKTER <> "" Then
                '    Dim oDoctor As New Reference.clsDoctor
                '    Dim dsDoctor = oDoctor.GetData(entity.KODEDOKTER)
                '    Dim sMODULDOKTER As String = String.Empty

                '    If dsDoctor IsNot Nothing Then
                '        If dsDoctor.MEMO <> "" Then
                '            sMODULDOKTER = dsDoctor.MEMO
                '        End If
                '    End If

                '    If sMODULDOKTER <> "" Then
                '        Try
                '            sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, entity.TANGGALPERIKSA)

                '            If sLASTNUMBER = 0 Then
                '                Try
                '                    oSetCounter.InsertData(sMODULDOKTER, entity.DATECREATED)
                '                    sLASTNUMBER = oSetCounter.GetLastNumberDay(sMODULDOKTER, entity.TANGGALPERIKSA)
                '                Catch ex As Exception
                '                    sLASTNUMBER = 0
                '                End Try
                '            End If

                '            entity.NOMORANTREAN = sMODULDOKTER & (sLASTNUMBER + 1).ToString.PadLeft(3, "0")
                '            entity.ANGKAANTREAN = sLASTNUMBER + 1

                '        Catch ex As Exception
                '            Throw ex
                '        End Try

                '        Try
                '            oSetCounter.UpdateData(sMODULDOKTER, sLASTNUMBER + 1, Day(entity.TANGGALPERIKSA), Month(entity.TANGGALPERIKSA), Year(entity.TANGGALPERIKSA))
                '        Catch ex As Exception
                '            Throw ex
                '        End Try
                '    End If
                'End If

                Try
                    oConnection.db.SET_BOOKING_ANTRIANs.InsertOnSubmit(entity)
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

                InsertData = entity.KODEBOOKING

                'Dim dsWaktuTunggu = GetStructureHeaderWaktuTunggu()

                'With dsWaktuTunggu
                '    .DATECREATED = entity.DATECREATED
                '    .DATEUPDATED = entity.DATEUPDATED
                '    .KODEBOOKING = entity.KODEBOOKING
                '    .ISPANGGIL = IIf(entity.ISPANGGIL = 0, 1, entity.ISPANGGIL)
                '    .DATE = entity.TANGGALPERIKSA
                '    .DATE_TEXT = entity.TANGGALPERIKSA_TEXT
                '    .KDCUSTOMER = entity.NORM
                '    .REMARKS = "INSERT"
                'End With

                'InsertDataWaktuTunggu(dsWaktuTunggu)

            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataSisa(ByVal entity As SET_PANGGIL_SISA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataSisa = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_PANGGIL_SISAs.InsertOnSubmit(entity)
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

                InsertDataSisa = True
            Catch ex As Exception
                InsertDataSisa = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataKeterangan(ByVal entity As SET_BOOKING_ANTRIAN_KETERANGAN) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataKeterangan = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "INSERT SETWAKTUTUNGGU"

                Try
                    oConnection.db.SET_BOOKING_ANTRIAN_KETERANGANs.InsertOnSubmit(entity)
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

                InsertDataKeterangan = entity.KODEBOOKING
            Catch ex As Exception
                InsertDataKeterangan = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataKeterangan(ByVal entity As SET_BOOKING_ANTRIAN_KETERANGAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataKeterangan = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_BOOKING_ANTRIAN_KETERANGANs.FirstOrDefault(Function(x) x.KODEBOOKING = entity.KODEBOOKING)

                Try
                    oConnection.db.SET_BOOKING_ANTRIAN_KETERANGANs.DeleteOnSubmit(ds)
                    oConnection.db.SET_BOOKING_ANTRIAN_KETERANGANs.InsertOnSubmit(entity)
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

                UpdateDataKeterangan = True
            Catch ex As Exception
                UpdateDataKeterangan = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataWaktuTungguSemuaKesini(ByVal entity As SET_WAKTUTUNGGU) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataWaktuTungguSemuaKesini = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING & entity.ISPANGGIL
                sSTATUS = "INSERT SETWAKTUTUNGGU"

                Try
                    oConnection.db.SET_WAKTUTUNGGUs.InsertOnSubmit(entity)
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

                InsertDataWaktuTungguSemuaKesini = entity.KODEBOOKING
            Catch ex As Exception
                InsertDataWaktuTungguSemuaKesini = ""
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataWaktuTungguSemuaKesini(ByVal entity As SET_WAKTUTUNGGU) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataWaktuTungguSemuaKesini = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING & entity.ISPANGGIL
                sSTATUS = "UPDATE SETWAKTUTUNGGU"

                Dim ds = oConnection.db.SET_WAKTUTUNGGUs.FirstOrDefault(Function(x) x.KODEBOOKING = entity.KODEBOOKING And x.ISPANGGIL = entity.ISPANGGIL)

                Try
                    oConnection.db.SET_WAKTUTUNGGUs.DeleteOnSubmit(ds)
                    oConnection.db.SET_WAKTUTUNGGUs.InsertOnSubmit(entity)
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

                UpdateDataWaktuTungguSemuaKesini = True
            Catch ex As Exception
                UpdateDataWaktuTungguSemuaKesini = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_BOOKING_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = entity.KODEBOOKING)

                Try
                    oConnection.db.SET_BOOKING_ANTRIANs.DeleteOnSubmit(ds)
                    oConnection.db.SET_BOOKING_ANTRIANs.InsertOnSubmit(entity)
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

                UpdateData = True

                'Dim dsWaktuTunggu = GetStructureHeaderWaktuTunggu()

                'With dsWaktuTunggu
                '    .DATECREATED = entity.DATECREATED
                '    .DATEUPDATED = entity.DATEUPDATED
                '    .KODEBOOKING = entity.KODEBOOKING
                '    .ISPANGGIL = entity.ISPANGGIL
                '    .DATE = entity.TANGGALPERIKSA
                '    .DATE_TEXT = entity.TANGGALPERIKSA_TEXT
                '    .KDCUSTOMER = entity.NORM
                '    .REMARKS = "UPDATE"
                'End With

                'UpdateDataWaktuTunggu(dsWaktuTunggu)
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataSisa(ByVal entity As SET_PANGGIL_SISA) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataSisa = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_PANGGIL_SISAs.FirstOrDefault(Function(x) x.KODE = entity.KODE)

                Try
                    oConnection.db.SET_PANGGIL_SISAs.DeleteOnSubmit(ds)
                    oConnection.db.SET_PANGGIL_SISAs.InsertOnSubmit(entity)
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

                UpdateDataSisa = True

            Catch ex As Exception
                UpdateDataSisa = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Private Function UpdateDataWaktuTunggu(ByVal entity As SET_WAKTUTUNGGU) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataWaktuTunggu = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING & entity.ISPANGGIL
                sSTATUS = "UPDATE SETWAKTUTUNGGU"

                Dim ds = oConnection.db.SET_WAKTUTUNGGUs.FirstOrDefault(Function(x) x.KODEBOOKING = entity.KODEBOOKING And x.ISPANGGIL)

                Try
                    oConnection.db.SET_WAKTUTUNGGUs.DeleteOnSubmit(ds)
                    oConnection.db.SET_WAKTUTUNGGUs.InsertOnSubmit(entity)
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

                UpdateDataWaktuTunggu = True
            Catch ex As Exception
                UpdateDataWaktuTunggu = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsCheked(ByVal sKODEBOOKING As String, ByVal sISPANGGIL As Integer, ByVal Keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsCheked = False
                    Exit Function
                End If

                UpdateDataIsCheked = True

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                ds.ISPANGGIL = sISPANGGIL
                If ds.KETERANGAN = "" Then
                    ds.DATEUPDATED = Now
                    ds.KETERANGAN = Keterangan
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsCheked = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsChekedNewKeteranganTaksIdWaktu(ByVal sKODEBOOKING As String, ByVal sISPANGGIL As Integer, ByVal waktu As DateTime) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsChekedNewKeteranganTaksIdWaktu = False
                    Exit Function
                End If

                UpdateDataIsChekedNewKeteranganTaksIdWaktu = True

                Dim ds = oConnection.db.SET_WAKTUTUNGGUs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING And x.ISPANGGIL = sISPANGGIL)

                If ds IsNot Nothing Then
                    ds.DATEUPDATED = Now
                    ds.DATE = waktu
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataIsChekedNewKeteranganTaksIdWaktu = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsChekedNewKeteranganTaksId(ByVal sKODEBOOKING As String, ByVal sISPANGGIL As Integer, ByVal Keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsChekedNewKeteranganTaksId = False
                    Exit Function
                End If

                UpdateDataIsChekedNewKeteranganTaksId = True

                Dim ds = oConnection.db.SET_WAKTUTUNGGUs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING And x.ISPANGGIL = sISPANGGIL)

                If ds IsNot Nothing Then
                    ds.DATEUPDATED = Now
                    ds.REMARKS = Keterangan
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataIsChekedNewKeteranganTaksId = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsChekedNewKeterangan(ByVal sKODEBOOKING As String, ByVal Keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsChekedNewKeterangan = False
                    Exit Function
                End If

                UpdateDataIsChekedNewKeterangan = True

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                ds.DATEUPDATED = Now
                ds.KETERANGAN = Keterangan

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsChekedNewKeterangan = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsChekedNew(ByVal sKODEBOOKING As String, ByVal sISPANGGIL As Integer, ByVal Keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsChekedNew = False
                    Exit Function
                End If

                UpdateDataIsChekedNew = True

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                ds.ISPANGGIL = sISPANGGIL
                ds.DATEUPDATED = Now
                ds.KETERANGAN = Keterangan

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsChekedNew = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsChekedGagal(ByVal sKODEBOOKING As String, ByVal Keterangan As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsChekedGagal = False
                    Exit Function
                End If

                UpdateDataIsChekedGagal = True

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                If ds.KETERANGAN = "" Then
                    ds.DATEUPDATED = Now
                    ds.KETERANGAN = Keterangan
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateDataIsChekedGagal = False
                Throw ex
            End Try
        End Function
        Public Function DeleteDataALL() As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteDataALL = False
                    Exit Function
                End If

                sREFERENCE = "ANTRIAN ALL"
                sSTATUS = "DELETE"

                Dim ds_SET_BOOKING_ANTRIAN_PANGGIL = oConnection.db.SET_ANTRIAN_PANGGILs.ToList()
                Dim ds_SET_BOOKING_ANTRIAN_SISA = oConnection.db.SET_ANTRIAN_SISAs.ToList()

                Try
                    oConnection.db.SET_ANTRIAN_PANGGILs.DeleteAllOnSubmit(ds_SET_BOOKING_ANTRIAN_PANGGIL)
                    oConnection.db.SET_ANTRIAN_SISAs.DeleteAllOnSubmit(ds_SET_BOOKING_ANTRIAN_SISA)
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

                DeleteDataALL = True
            Catch ex As Exception
                DeleteDataALL = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataIsKeterangan(ByVal sKODEBOOKING As String, ByVal sKETERANGAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsKeterangan = False
                    Exit Function
                End If

                UpdateDataIsKeterangan = True

                Dim ds = oConnection.db.SET_BOOKING_ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                If ds IsNot Nothing Then
                    ds.KETERANGAN = sKETERANGAN
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataIsKeterangan = False
                Throw ex
            End Try
        End Function
        Public Function DeletePanggilAntrian() As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeletePanggilAntrian = False
                    Exit Function
                End If

                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.ToList()

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.DeleteAllOnSubmit(ds)
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

                DeletePanggilAntrian = True
            Catch ex As Exception
                DeletePanggilAntrian = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeletePanggilAntrianSisa() As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeletePanggilAntrianSisa = False
                    Exit Function
                End If

                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_PANGGIL_SISAs.ToList()

                Try
                    oConnection.db.SET_PANGGIL_SISAs.DeleteAllOnSubmit(ds)
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

                DeletePanggilAntrianSisa = True
            Catch ex As Exception
                DeletePanggilAntrianSisa = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace