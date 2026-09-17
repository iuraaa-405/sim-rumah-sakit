Namespace Reference
    Public Class clsCustomer
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter

            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter("TAX")

            End If

            sMODUL = "RM"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_CUSTOMER
        End Function
        Public Function GetData() As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMERs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataListKartu(ByVal Nomor As String) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataListKartu = Nothing
                Exit Function
            End If
            GetDataListKartu = oConnection.db.M_CUSTOMERs.Where(Function(x) x.KARTUBPJS = Nomor).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataList(ByVal Parameter As String, ByVal Category As Integer) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.M_CUSTOMERs.Where(Function(x) IIf(Category = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Category = 1, x.KTP.Contains(Parameter), IIf(Category = 2, x.NAME_DISPLAY.Contains(Parameter), IIf(Category = 3, x.KARTUBPJS.Contains(Parameter), x.ALAMAT.Contains(Parameter)))))).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetDataCustomerByApproval(ByVal Parameter As String, ByVal Categori As Integer) As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataCustomerByApproval = Nothing
                Exit Function
            End If
            GetDataCustomerByApproval = oConnection.db.M_CUSTOMERs.Where(Function(x) IIf(Categori = 0, x.KDCUSTOMER.Contains(Parameter), IIf(Categori = 1, x.NAME_DISPLAY.Contains(Parameter), x.KARTUBPJS.Contains(Parameter)))).OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function GetData(ByVal sKDCUSTOMER As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)
        End Function
        Public Function GetDataKARTUBPJS(ByVal sKARTUBPJS As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetDataKARTUBPJS = Nothing
                Exit Function
            End If
            GetDataKARTUBPJS = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KARTUBPJS = sKARTUBPJS)
        End Function
        Public Function GetDataKTP(ByVal sKTP As String) As M_CUSTOMER
            If Not oConnection.GetConnection() Then
                GetDataKTP = Nothing
                Exit Function
            End If
            GetDataKTP = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KTP = sKTP)
        End Function
        Public Function GetDataSync() As List(Of M_CUSTOMER)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_CUSTOMERs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        End Function
        Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function GetDataSetting() As SET_SETTING
            If Not oConnection.GetConnection() Then
                GetDataSetting = Nothing
                Exit Function
            End If
            GetDataSetting = oConnection.db.SET_SETTINGs.FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As M_CUSTOMER, ByVal KDCUSTOMER As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER
                sSTATUS = "INSERT"

                If KDCUSTOMER = "" Then
                    'Generate Auto Number
                    Try
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                        If sLASTNUMBER = 0 Then
                            Try
                                oCounter.InsertData(sMODUL, entity.DATECREATED)
                                sLASTNUMBER = oCounter.GetLastNumber(sMODUL)
                            Catch ex As Exception
                                sLASTNUMBER = 0
                            End Try
                        End If

                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    Try
                        oCounter.UpdateData(sMODUL, sLASTNUMBER + 1)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                    KDCUSTOMER = AutoNumberRM(sLASTNUMBER + 1)
                    entity.KDCUSTOMER_LAMA = ""
                    'End Generate
                Else
                    entity.KDCUSTOMER_LAMA = KDCUSTOMER
                End If

                entity.KDCUSTOMER = KDCUSTOMER

                'Try
                '    entity.KDCUSTOMER = KDCUSTOMER
                '    oConnection.db.M_CUSTOMERs.InsertOnSubmit(entity)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try
                'Try
                '    oConnection.db.SubmitChanges()
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try


                Try
                    entity.KDCUSTOMER = KDCUSTOMER

                    ' Ganti InsertOnSubmit dengan ExecuteCommand
                    Dim sql As String = "
        INSERT INTO M_CUSTOMER (
            DATECREATED, DATEUPDATED, KDCUSTOMER, KDCOA, KTP, NAME_DISPLAY, 
            EMAIL, PHONE, MOBILE, FAX, OTHER, WEBSITE, MEMO, ISACTIVE, 
            ALAMAT, KDKELURAHAN, KODEPOS, KDPENJAMIN, KDKESATUAN, KDPANGKAT, 
            KDGOLONGAN, KDPENDIDIKAN, KDPEKERJAAN, KDPERUSAHAAN, KDAGAMA, 
            KDJENISKELAMIN, KDGOLONGANDARAH, KDSTATUSKAWIN, KDSUKU, KDSTATUSHIDUP, 
            NRP, NAMAKELUARGA, KDSTATUSKELUARGA, TEMPATLAHIR, TANGGALLAHIR, 
            WNI, NEGARA, KARTUBPJS, KDCUSTOMER_LAMA
        ) VALUES (
            @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, 
            @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20, @p21, @p22, 
            @p23, @p24, @p25, @p26, @p27, @p28, @p29, @p30, @p31, @p32, @p33, 
            @p34, @p35, @p36, @p37, @p38
        )"

                    oConnection.db.CommandTimeout = 300
                    oConnection.db.ExecuteCommand(sql,
        entity.DATECREATED, entity.DATEUPDATED, entity.KDCUSTOMER, entity.KDCOA,
        entity.KTP, entity.NAME_DISPLAY, entity.EMAIL, entity.PHONE, entity.MOBILE,
        entity.FAX, entity.OTHER, entity.WEBSITE, entity.MEMO, entity.ISACTIVE,
        entity.ALAMAT, entity.KDKELURAHAN, entity.KODEPOS, entity.KDPENJAMIN,
        entity.KDKESATUAN, entity.KDPANGKAT, entity.KDGOLONGAN, entity.KDPENDIDIKAN,
        entity.KDPEKERJAAN, entity.KDPERUSAHAAN, entity.KDAGAMA, entity.KDJENISKELAMIN,
        entity.KDGOLONGANDARAH, entity.KDSTATUSKAWIN, entity.KDSUKU, entity.KDSTATUSHIDUP,
        entity.NRP, entity.NAMAKELUARGA, entity.KDSTATUSKELUARGA, entity.TEMPATLAHIR,
        entity.TANGGALLAHIR, entity.WNI, entity.NEGARA, entity.KARTUBPJS, entity.KDCUSTOMER_LAMA
    )

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDCUSTOMER
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As M_CUSTOMER) As Boolean
            'Try
            '    If Not oConnection.GetConnection() Then
            '        UpdateData = False
            '        Exit Function
            '    End If

            '    sREFERENCE = entity.KDCUSTOMER
            '    sSTATUS = "UPDATE"

            '    Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = entity.KDCUSTOMER)

            '    Try
            '        oConnection.db.M_CUSTOMERs.DeleteOnSubmit(ds)
            '        oConnection.db.M_CUSTOMERs.InsertOnSubmit(entity)
            '    Catch ex As Exception
            '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
            '        Throw ex
            '    End Try

            '    Try
            '        oConnection.db.SubmitChanges()
            '    Catch ex As Exception
            '        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
            '        Throw ex
            '    End Try

            '    UpdateData = True
            'Catch ex As Exception
            '    UpdateData = False
            '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
            '    Throw ex
            'End Try
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCUSTOMER
                sSTATUS = "UPDATE"

                Try
                    ' ===== SQL UPDATE menggunakan ExecuteCommand =====
                    Dim sql As String = "
                UPDATE M_CUSTOMER SET 
                    DATECREATED = @p0,
                    DATEUPDATED = @p1,
                    KDCUSTOMER = @p2,
                    KDCOA = @p3,
                    KTP = @p4,
                    NAME_DISPLAY = @p5,
                    EMAIL = @p6,
                    PHONE = @p7,
                    MOBILE = @p8,
                    FAX = @p9,
                    OTHER = @p10,
                    WEBSITE = @p11,
                    MEMO = @p12,
                    ISACTIVE = @p13,
                    ALAMAT = @p14,
                    KDKELURAHAN = @p15,
                    KODEPOS = @p16,
                    KDPENJAMIN = @p17,
                    KDKESATUAN = @p18,
                    KDPANGKAT = @p19,
                    KDGOLONGAN = @p20,
                    KDPENDIDIKAN = @p21,
                    KDPEKERJAAN = @p22,
                    KDPERUSAHAAN = @p23,
                    KDAGAMA = @p24,
                    KDJENISKELAMIN = @p25,
                    KDGOLONGANDARAH = @p26,
                    KDSTATUSKAWIN = @p27,
                    KDSUKU = @p28,
                    KDSTATUSHIDUP = @p29,
                    NRP = @p30,
                    NAMAKELUARGA = @p31,
                    KDSTATUSKELUARGA = @p32,
                    TEMPATLAHIR = @p33,
                    TANGGALLAHIR = @p34,
                    WNI = @p35,
                    NEGARA = @p36,
                    KARTUBPJS = @p37,
                    KDCUSTOMER_LAMA = @p38
                WHERE KDCUSTOMER = @p39"

                    ' Set timeout
                    oConnection.db.CommandTimeout = 300

                    ' Eksekusi UPDATE
                    Dim rowsAffected = oConnection.db.ExecuteCommand(sql,
                        entity.DATECREATED,           ' @p0
                        entity.DATEUPDATED,           ' @p1
                        entity.KDCUSTOMER,            ' @p2
                        entity.KDCOA,                 ' @p3
                        entity.KTP,                   ' @p4
                        entity.NAME_DISPLAY,          ' @p5
                        entity.EMAIL,                 ' @p6
                        entity.PHONE,                 ' @p7
                        entity.MOBILE,                ' @p8
                        entity.FAX,                   ' @p9
                        entity.OTHER,                 ' @p10
                        entity.WEBSITE,               ' @p11
                        entity.MEMO,                  ' @p12
                        entity.ISACTIVE,              ' @p13
                        entity.ALAMAT,                ' @p14
                        entity.KDKELURAHAN,           ' @p15
                        entity.KODEPOS,               ' @p16
                        entity.KDPENJAMIN,            ' @p17
                        entity.KDKESATUAN,            ' @p18
                        entity.KDPANGKAT,             ' @p19
                        entity.KDGOLONGAN,            ' @p20
                        entity.KDPENDIDIKAN,          ' @p21
                        entity.KDPEKERJAAN,           ' @p22
                        entity.KDPERUSAHAAN,          ' @p23
                        entity.KDAGAMA,               ' @p24
                        entity.KDJENISKELAMIN,        ' @p25
                        entity.KDGOLONGANDARAH,       ' @p26
                        entity.KDSTATUSKAWIN,         ' @p27
                        entity.KDSUKU,                ' @p28
                        entity.KDSTATUSHIDUP,         ' @p29
                        entity.NRP,                   ' @p30
                        entity.NAMAKELUARGA,          ' @p31
                        entity.KDSTATUSKELUARGA,      ' @p32
                        entity.TEMPATLAHIR,           ' @p33
                        entity.TANGGALLAHIR,          ' @p34
                        entity.WNI,                   ' @p35
                        entity.NEGARA,                ' @p36
                        entity.KARTUBPJS,             ' @p37
                        entity.KDCUSTOMER_LAMA,       ' @p38
                        entity.KDCUSTOMER               ' @p39 (WHERE clause)
                    )

                    UpdateData = True

                    If rowsAffected = 0 Then
                        ' Tidak ada data yang diupdate (KDCUSTOMER tidak ditemukan)
                        UpdateData = False
                        oError.InsertData(sMODUL, sSTATUS, "Data tidak ditemukan untuk KDCUSTOMER: " & entity.KDCUSTOMER, sREFERENCE)
                        Return False
                    End If

                    ' ========================================================

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDCUSTOMER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCUSTOMER
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = sKDCUSTOMER)

                Try
                    oConnection.db.M_CUSTOMERs.DeleteOnSubmit(ds)
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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function AccountDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AccountDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().COA_CUSTOMER
                Try
                    AccountDefault = ds
                Catch ex As Exception
                    AccountDefault = String.Empty
                End Try
            Catch ex As Exception
                AccountDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PangkatDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    PangkatDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PANGKATs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PangkatDefault = ds.KDPANGKAT
                Else
                    PangkatDefault = String.Empty
                End If
            Catch ex As Exception
                PangkatDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function GolonganDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    GolonganDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_GOLONGANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    GolonganDefault = ds.KDGOLONGAN
                Else
                    GolonganDefault = String.Empty
                End If
            Catch ex As Exception
                GolonganDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PendidikanDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    PendidikanDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PENDIDIKANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PendidikanDefault = ds.KDPENDIDIKAN
                Else
                    PendidikanDefault = String.Empty
                End If
            Catch ex As Exception
                PendidikanDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PekerjaanDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    PekerjaanDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PEKERJAANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PekerjaanDefault = ds.KDPEKERJAAN
                Else
                    PekerjaanDefault = String.Empty
                End If
            Catch ex As Exception
                PekerjaanDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PerusahaanDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    PerusahaanDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PERUSAHAANs.FirstOrDefault(Function(x) x.NAME_DISPLAY = "-")
                If ds IsNot Nothing Then
                    PerusahaanDefault = ds.KDPERUSAHAAN
                Else
                    PerusahaanDefault = String.Empty
                End If
            Catch ex As Exception
                PerusahaanDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function KesatuanDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    KesatuanDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KESATUANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    KesatuanDefault = ds.KDKESATUAN
                Else
                    KesatuanDefault = String.Empty
                End If
            Catch ex As Exception
                KesatuanDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function PenjaminDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    PenjaminDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_PENJAMINs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    PenjaminDefault = ds.KDPENJAMIN
                Else
                    PenjaminDefault = String.Empty
                End If
            Catch ex As Exception
                PenjaminDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function AgamaDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    AgamaDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_AGAMAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    AgamaDefault = ds.KDAGAMA
                Else
                    AgamaDefault = String.Empty
                End If
            Catch ex As Exception
                AgamaDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function SukuDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    SukuDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_SUKUs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    SukuDefault = ds.KDSUKU
                Else
                    SukuDefault = String.Empty
                End If
            Catch ex As Exception
                SukuDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function StatusKeluargaDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    StatusKeluargaDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_STATUSKELUARGAs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    StatusKeluargaDefault = ds.KDSTATUSKELUARGA
                Else
                    StatusKeluargaDefault = String.Empty
                End If
            Catch ex As Exception
                StatusKeluargaDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function StatusKelurahanDefault() As String
            Try
                If Not oConnection.GetConnection() Then
                    StatusKelurahanDefault = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELURAHANs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    StatusKelurahanDefault = ds.KDKELURAHAN
                Else
                    StatusKelurahanDefault = String.Empty
                End If
            Catch ex As Exception
                StatusKelurahanDefault = String.Empty
                Throw ex
            End Try
        End Function
        Public Function UpdateNomorTelepon(ByVal kdcustomer As String, ByVal nomortelepon As String, ByVal KTP As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateNomorTelepon = False
                    Exit Function
                End If

                UpdateNomorTelepon = True

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = kdcustomer)

                ds.PHONE = nomortelepon
                ds.KTP = KTP

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateNomorTelepon = False
                Throw ex
            End Try
        End Function
        Public Function UpdatejenisKelamin(ByVal kdcustomer As String, ByVal Jeniskelamin As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatejenisKelamin = False
                    Exit Function
                End If

                UpdatejenisKelamin = True

                Dim ds = oConnection.db.M_CUSTOMERs.FirstOrDefault(Function(x) x.KDCUSTOMER = kdcustomer)

                ds.KDJENISKELAMIN = Jeniskelamin

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatejenisKelamin = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace