Namespace Grouper
    Public Class clsR_Identitas_Grouper
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
            sMODUL = "GROUPER"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER
        End Function
        Public Function GetStructureHeaderJkn() As R_IDENTITAS_JKN
            If Not oConnection.GetConnection() Then
                GetStructureHeaderJkn = Nothing
            End If
            GetStructureHeaderJkn = New R_IDENTITAS_JKN
        End Function
        Public Function GetStructureDetailPDFList() As List(Of R_IDENTITAS_GROUPER_PDF)
            If Not oConnection.GetConnection() Then
                GetStructureDetailPDFList = Nothing
            End If
            GetStructureDetailPDFList = New List(Of R_IDENTITAS_GROUPER_PDF)
        End Function
        Public Function GetStructureDetailPDF() As R_IDENTITAS_GROUPER_PDF
            If Not oConnection.GetConnection() Then
                GetStructureDetailPDF = Nothing
            End If
            GetStructureDetailPDF = New R_IDENTITAS_GROUPER_PDF
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPERs.OrderByDescending(Function(x) x.kodegrouper).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
        End Function
        Public Function GetDataByNosep(ByVal Parameter As String) As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetDataByNosep = Nothing
                Exit Function
            End If
            GetDataByNosep = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.noSep = Parameter)
        End Function
        Public Function GetDataByNoRec(ByVal Parameter As String) As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetDataByNoRec = Nothing
                Exit Function
            End If
            GetDataByNoRec = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.norec = Parameter)
        End Function
        Public Function GetDataByNoSepJkn(ByVal Parameter As String) As R_IDENTITAS_JKN
            If Not oConnection.GetConnection() Then
                GetDataByNoSepJkn = Nothing
                Exit Function
            End If
            GetDataByNoSepJkn = oConnection.db.R_IDENTITAS_JKNs.FirstOrDefault(Function(x) x.noSep = Parameter)
        End Function
        Public Function GetDataByKartuTanggal(ByVal snoKartu As String, ByVal sDATE As String) As R_IDENTITAS_JKN
            If Not oConnection.GetConnection() Then
                GetDataByKartuTanggal = Nothing
                Exit Function
            End If
            GetDataByKartuTanggal = oConnection.db.R_IDENTITAS_JKNs.FirstOrDefault(Function(x) x.noKartu = snoKartu And x.tglSep_Text = sDATE)
        End Function
        Public Function GetDataDetailByRM(ByVal Parameter As String) As List(Of R_IDENTITAS_GROUPER)
            If Not oConnection.GetConnection() Then
                GetDataDetailByRM = Nothing
                Exit Function
            End If
            GetDataDetailByRM = oConnection.db.R_IDENTITAS_GROUPERs.Where(Function(x) x.noRm = Parameter).ToList()
        End Function
        'Public Function GetDataGrouperPDFList(ByVal kodegoruper As Integer) As List(Of R_IDENTITAS_GROUPER_PDF)
        '    If Not oConnection.GetConnection() Then
        '        GetDataGrouperPDFList = Nothing
        '        Exit Function
        '    End If
        '    GetDataGrouperPDFList = oConnection.db.R_IDENTITAS_GROUPER_PDFs().Where(Function(x) x.kodegrouper = kodegoruper).OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        'Public Function GetDataGrouperPDF(ByVal Parameter As Integer) As List(Of R_IDENTITAS_GROUPER_PDF)
        '    If Not oConnection.GetConnection() Then
        '        GetDataGrouperPDF = Nothing
        '        Exit Function
        '    End If
        '    GetDataGrouperPDF = oConnection.db.R_IDENTITAS_GROUPER_PDFs.Where(Function(x) x.kodegrouper = Parameter And x.ISCHEKED = True).OrderBy(Function(x) x.SEQ)
        'End Function
        Public Function GetDataGrouperPDFByKdPendaftaaranList(ByVal sKDPENDAFTATARAN As String) As List(Of S_PENDAFTARAN_PDF)
            If Not oConnection.GetConnection() Then
                GetDataGrouperPDFByKdPendaftaaranList = Nothing
                Exit Function
            End If
            GetDataGrouperPDFByKdPendaftaaranList = oConnection.db.S_PENDAFTARAN_PDFs().Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTATARAN).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataByKartuByTglLahir(ByVal snoKartu As String) As R_IDENTITAS_GROUPER
            If Not oConnection.GetConnection() Then
                GetDataByKartuByTglLahir = Nothing
                Exit Function
            End If
            GetDataByKartuByTglLahir = oConnection.db.R_IDENTITAS_GROUPERs.Where(Function(x) x.noKartu = snoKartu).OrderByDescending(Function(x) x.kodegrouper).FirstOrDefault()
        End Function
        Public Function GetDataBykdpendaftaranList(ByVal Parameter As String) As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetDataBykdpendaftaranList = Nothing
                Exit Function
            End If
            GetDataBykdpendaftaranList = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = Parameter).ToList()
        End Function
        Public Function GetDataByKodeKunjungan(ByVal Parameter As String) As A_IDENTITASPASIEN_LIST
            If Not oConnection.GetConnectionRME() Then
                GetDataByKodeKunjungan = Nothing
                Exit Function
            End If
            GetDataByKodeKunjungan = oConnection.dbRME.A_IDENTITASPASIEN_LISTs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPERs.InsertOnSubmit(entity)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_IDENTITAS_GROUPER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.kodegrouper = entity.kodegrouper)
                Try
                    oConnection.db.R_IDENTITAS_GROUPERs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPERs.InsertOnSubmit(entity)
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
            Catch ex As Exception
                UpdateData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPERs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)

                Try
                    oConnection.db.R_IDENTITAS_GROUPERs.DeleteOnSubmit(ds)
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
        Public Function InsertDataJKN(ByVal entity As R_IDENTITAS_JKN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataJKN = False
                    Exit Function
                End If

                sREFERENCE = entity.noSep
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_JKNs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataJKN = True
            Catch ex As Exception
                InsertDataJKN = False
                oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataJKN(ByVal entity As R_IDENTITAS_JKN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataJKN = False
                    Exit Function
                End If

                sREFERENCE = entity.noSep
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_JKNs.FirstOrDefault(Function(x) x.noSep = entity.noSep)
                Try
                    oConnection.db.R_IDENTITAS_JKNs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_JKNs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataJKN = True
            Catch ex As Exception
                UpdateDataJKN = False
                oError.InsertData("R_IDENTITAS_JKN", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataPDF(ByVal entity As List(Of R_IDENTITAS_GROUPER_PDF)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataPDF = False
                    Exit Function
                End If

                sREFERENCE = entity.FirstOrDefault.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_PDFs.InsertAllOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataPDF = True
            Catch ex As Exception
                InsertDataPDF = False
                oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPDF(ByVal entity As List(Of R_IDENTITAS_GROUPER_PDF)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataPDF = False
                    Exit Function
                End If

                sREFERENCE = entity.FirstOrDefault.kodegrouper
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_PDFs.Where(Function(x) x.kodegrouper = entity.FirstOrDefault.kodegrouper)
                Try
                    oConnection.db.R_IDENTITAS_GROUPER_PDFs.DeleteAllOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPER_PDFs.InsertAllOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataPDF = True
            Catch ex As Exception
                UpdateDataPDF = False
                oError.InsertData("R_IDENTITAS_GROUPER_PDF", sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPDFIscheked(ByVal skodegrouper As Integer, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataPDFIscheked = False
                    Exit Function
                End If

                UpdateDataPDFIscheked = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_PDFs.FirstOrDefault(Function(x) x.kodegrouper = skodegrouper And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.ISCHEKED = False
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataPDFIscheked = False
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPDFIsUploadKlaim(ByVal skodegrouper As Integer, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataPDFIsUploadKlaim = False
                    Exit Function
                End If

                UpdateDataPDFIsUploadKlaim = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_PDFs.FirstOrDefault(Function(x) x.kodegrouper = skodegrouper And x.SEQ = sSEQ)

                If ds IsNot Nothing Then
                    ds.ISUPLOADKLAIM = True
                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateDataPDFIsUploadKlaim = False
                Throw ex
            End Try
        End Function
        Public Function cbo_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    cbo_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_COBs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    cbo_Default = ds.KDCOB
                Else
                    cbo_Default = String.Empty
                End If
            Catch ex As Exception
                cbo_Default = String.Empty
                Throw ex
            End Try
        End Function
        Public Function CaraKeluar_Default() As String
            Try
                If Not oConnection.GetConnection() Then
                    CaraKeluar_Default = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.M_CARAKELUARs.FirstOrDefault(Function(x) x.ISDEFAULT = True)
                If ds IsNot Nothing Then
                    CaraKeluar_Default = ds.KDCARAKELUAR
                Else
                    CaraKeluar_Default = String.Empty
                End If
            Catch ex As Exception
                CaraKeluar_Default = String.Empty
                Throw ex
            End Try
        End Function
    End Class
End Namespace