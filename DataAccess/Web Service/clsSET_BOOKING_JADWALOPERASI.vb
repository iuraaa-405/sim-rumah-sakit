Namespace WebService
    Public Class clsSET_BOOKING_JADWALOPERASI
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
            sMODUL = "JOP"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_BOOKING_JADWALOPERASI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_BOOKING_JADWALOPERASI
        End Function
        Public Function GetData() As List(Of SET_BOOKING_JADWALOPERASI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_JADWALOPERASIs.OrderByDescending(Function(x) x.KDBOOKINGJADWALOPERASI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As SET_BOOKING_JADWALOPERASI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = Parameter)
        End Function
        Public Function GetDataByKD(ByVal Parameter As String) As SET_BOOKING_JADWALOPERASI
            If Not oConnection.GetConnection() Then
                GetDataByKD = Nothing
                Exit Function
            End If
            GetDataByKD = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetKoneksiApi() As String
            Try
                If Not oConnection.GetConnection() Then
                    GetKoneksiApi = String.Empty
                    Exit Function
                End If

                Dim ds = oConnection.db.SET_SETTINGs.FirstOrDefault().KONEKSI_DATABASEAPI
                Try
                    GetKoneksiApi = ds
                Catch ex As Exception
                    GetKoneksiApi = String.Empty
                End Try
            Catch ex As Exception
                GetKoneksiApi = String.Empty
                Throw ex
            End Try
        End Function
        Public Function AutoNumber(ByVal sKDCOUNTER As String, ByVal sLASTNUMBER As Integer, ByVal sDATE As DateTime) As String
            Dim sMonth As String = String.Empty

            Select Case Month(sDATE)
                Case 1
                    sMonth = "A"
                Case 2
                    sMonth = "B"
                Case 3
                    sMonth = "C"
                Case 4
                    sMonth = "D"
                Case 5
                    sMonth = "E"
                Case 6
                    sMonth = "F"
                Case 7
                    sMonth = "G"
                Case 8
                    sMonth = "H"
                Case 9
                    sMonth = "I"
                Case 10
                    sMonth = "J"
                Case 11
                    sMonth = "K"
                Case 12
                    sMonth = "L"
            End Select

            If sLASTNUMBER < 10 Then
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00000" & sLASTNUMBER.ToString
            ElseIf sLASTNUMBER < 100 Then
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0000" & sLASTNUMBER.ToString
            ElseIf sLASTNUMBER < 1000 Then
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "000" & sLASTNUMBER.ToString
            ElseIf sLASTNUMBER < 10000 Then
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "00" & sLASTNUMBER.ToString
            ElseIf sLASTNUMBER < 100000 Then
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & "0" & sLASTNUMBER.ToString
            Else
                AutoNumber = sKDCOUNTER & Year(sDATE) & sMonth & sLASTNUMBER.ToString
            End If
        End Function
        Public Function InsertData(ByVal entity As SET_BOOKING_JADWALOPERASI, ByVal KDBOOKING As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBOOKINGJADWALOPERASI
                sSTATUS = "INSERT"

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALOPERASI)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.TANGGALOPERASI)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALOPERASI)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDBOOKINGJADWALOPERASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.TANGGALOPERASI)
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    Dim oPendaftaran As New Admission.clsPendaftaran

                    Dim dsIdentitasCek = oPendaftaran.GetDataIdentitas(entity.KDKUNJUNGAN)
                    If dsIdentitasCek Is Nothing Then
                        MsgBox("Identitas Kosong, Silahkan eEdit Kunjungan", MsgBoxStyle.Exclamation)
                    End If

                    entity.KDBOOKINGJADWALOPERASI = KDBOOKING
                    oConnection.db.SET_BOOKING_JADWALOPERASIs.InsertOnSubmit(entity)
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

                'Try
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.TANGGALOPERASI), Year(entity.TANGGALOPERASI))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                InsertData = True
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_BOOKING_JADWALOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDBOOKINGJADWALOPERASI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = entity.KDBOOKINGJADWALOPERASI)

                Try
                    oConnection.db.SET_BOOKING_JADWALOPERASIs.DeleteOnSubmit(ds)
                    oConnection.db.SET_BOOKING_JADWALOPERASIs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.Where(Function(x) x.KDBOOKINGJADWALOPERASI.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDBOOKINGJADWALOPERASI

                    Try
                        oConnection.db.SET_BOOKING_JADWALOPERASIs.DeleteOnSubmit(xLoop)
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

                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateIscheked(ByVal sKDBOOKINGJADWALOPERASI As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIscheked = False
                    Exit Function
                End If

                UpdateIscheked = True

                Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = sKDBOOKINGJADWALOPERASI)

                If ds.ISAPROVAL = 0 Then
                    ds.ISAPROVAL = 1
                Else
                    ds.ISAPROVAL = 0
                End If

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIscheked = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace