Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsKelasAplicareBed
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureDetail() As M_KELASAPLICARE_DEPARTMENT_BED
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_KELASAPLICARE_DEPARTMENT_BED
        End Function
        Public Function GetStructureDetailList() As List(Of M_KELASAPLICARE_DEPARTMENT_BED)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_KELASAPLICARE_DEPARTMENT_BED)
        End Function
        'Public Function GetData(ByVal sKDUPDATE_APLICARE As String) As M_KELASAPLICARE_DEPARTMENT_BED
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)
        'End Function
        Public Function GetDatadefaultKelasAplicare(ByVal sKDUPDATE_APLICARE As String) As M_KELASAPLICARE_DEPARTMENT_BED
            If Not oConnection.GetConnection() Then
                GetDatadefaultKelasAplicare = Nothing
                Exit Function
            End If
            GetDatadefaultKelasAplicare = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)
        End Function
        Public Function GetDataAda() As M_KELASAPLICARE_DEPARTMENT_BED
            If Not oConnection.GetConnection() Then
                GetDataAda = Nothing
                Exit Function
            End If
            GetDataAda = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault()
        End Function
        Public Function GetDataByKdPendaftaran(ByVal sKDPENDAFTARAN As String) As M_KELASAPLICARE_DEPARTMENT_BED
            If Not oConnection.GetConnection() Then
                GetDataByKdPendaftaran = Nothing
                Exit Function
            End If
            GetDataByKdPendaftaran = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDPENDAFTARAN <> "")
        End Function
        'Public Function GetDataByBedKdUpdateAplicare(ByVal sBED As String, ByVal sKDUPDATE_APLICARE As String) As M_KELASAPLICARE_DEPARTMENT_BED
        '    If Not oConnection.GetConnection() Then
        '        GetDataByBedKdUpdateAplicare = Nothing
        '        Exit Function
        '    End If
        '    GetDataByBedKdUpdateAplicare = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.BED = sBED And x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE)
        'End Function
        Public Function GetDataKode(ByVal sKODEBED As String) As M_KELASAPLICARE_DEPARTMENT_BED
            If Not oConnection.GetConnection() Then
                GetDataKode = Nothing
                Exit Function
            End If
            GetDataKode = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.KODEBED = sKODEBED)
        End Function
        Public Function GetDataDetailAplicare(ByVal sKDUPDATE_APLICARE As String) As List(Of M_KELASAPLICARE_DEPARTMENT_BED)
            If Not oConnection.GetConnection() Then
                GetDataDetailAplicare = Nothing
                Exit Function
            End If
            GetDataDetailAplicare = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.Where(Function(x) x.KDUPDATE_APLICARE = sKDUPDATE_APLICARE).ToList()
        End Function
        Public Function GetDataDetailByKdPendaftaran(ByVal sKDPENDAFTARAN As String) As List(Of M_KELASAPLICARE_DEPARTMENT_BED)
            If Not oConnection.GetConnection() Then
                GetDataDetailByKdPendaftaran = Nothing
                Exit Function
            End If
            GetDataDetailByKdPendaftaran = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDPENDAFTARAN <> "").ToList()
        End Function
        Public Function GetDataDetailByKdPendaftaranKunjungan(ByVal sKDPENDAFTARAN As String) As List(Of S_PENDAFTARAN_KUNJUNGAN)
            If Not oConnection.GetConnection() Then
                GetDataDetailByKdPendaftaranKunjungan = Nothing
                Exit Function
            End If
            GetDataDetailByKdPendaftaranKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.KDPENDAFTARAN <> "").ToList()
        End Function
        'Public Function GetDataDetailByRuangan(ByVal sKDDEPARTMENT As String) As List(Of M_KELASAPLICARE_DEPARTMENT_BED)
        '    If Not oConnection.GetConnection() Then
        '        GetDataDetailByRuangan = Nothing
        '        Exit Function
        '    End If
        '    GetDataDetailByRuangan = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.Where(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT).ToList()
        'End Function
        Public Function InsertData(ByVal entityDetail As List(Of M_KELASAPLICARE_DEPARTMENT_BED)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                'For Each iLoop In entityDetail
                '    If iLoop.KODEBED = "" Then

                '    End If
                'Next


                oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.InsertAllOnSubmit(entityDetail)
                oConnection.db.SubmitChanges()

                InsertData = True
            Catch ex As Exception
                InsertData = False
                Throw ex
            End Try
        End Function
        'Public Function UpdateData(ByVal entityDetail As List(Of M_KELASAPLICARE_DEPARTMENT_BED)) As Boolean
        '    Try
        '        If Not oConnection.GetConnection() Then
        '            UpdateData = False
        '            Exit Function
        '        End If

        '        Dim dsDetail = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.Where(Function(x) x.KDUPDATE_APLICARE = entityDetail.FirstOrDefault.KDUPDATE_APLICARE)

        '        oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.DeleteAllOnSubmit(dsDetail)
        '        oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.InsertAllOnSubmit(entityDetail)

        '        oConnection.db.SubmitChanges()

        '        UpdateData = True
        '    Catch ex As Exception
        '        UpdateData = False
        '        Throw ex
        '    End Try
        'End Function
        Public Function UpdateData(ByVal entityDetail As List(Of M_KELASAPLICARE_DEPARTMENT_BED)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                'sREFERENCE = entity.KDITEM
                'sSTATUS = "UPDATE"

                Dim dsDetail = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.Where(Function(x) x.KDUPDATE_APLICARE = entityDetail.FirstOrDefault.KDUPDATE_APLICARE)

                Try
                    oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPemetaan(ByVal sKODEBED As String, ByVal KDPENDAFTARAN As String, ByVal sSTATUS As String, ByVal MEMO As String) As String
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataPemetaan = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELASAPLICARE_DEPARTMENT_BEDs.FirstOrDefault(Function(x) x.KODEBED = sKODEBED)

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                ds.DATEUPDATED = WaktuServer
                ds.KDPENDAFTARAN = KDPENDAFTARAN
                ds.MEMO = MEMO
                ds.STATUS = sSTATUS

                oConnection.db.SubmitChanges()

                UpdateDataPemetaan = True

            Catch ex As Exception
                UpdateDataPemetaan = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace