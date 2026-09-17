Imports System.Threading

Namespace AntrianRS
    Public Class clsAntrian
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing
        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "RSG"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As ANTRIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New ANTRIAN
        End Function
        Public Function GetData() As List(Of ANTRIAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.ANTRIANs.OrderByDescending(Function(x) x.KODEBOOKING).ToList()
        End Function
        Public Function GetData(ByVal sKODEBOOKING As String) As ANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)
        End Function
        Public Function GetDataByDateBelumPanggil(ByVal sDATE As DateTime, ByVal sJENISPASIEN_RS As String) As ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByDateBelumPanggil = Nothing
                Exit Function
            End If

            Dim ds = oConnection.db.ANTRIANs.Where(Function(x) x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL = 0 And x.JENISPASIEN_RS = sJENISPASIEN_RS).OrderBy(Function(x) x.KODEBOOKING).ToList()

            If ds.Count > 0 Then
                GetDataByDateBelumPanggil = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = ds.FirstOrDefault.KODEBOOKING)
            Else
                GetDataByDateBelumPanggil = Nothing
            End If

        End Function
        Public Function GetDataByDatePending(ByVal sDATE As DateTime) As ANTRIAN
            If Not oConnection.GetConnection() Then
                GetDataByDatePending = Nothing
                Exit Function
            End If

            Dim ds = oConnection.db.ANTRIANs.Where(Function(x) x.TANGGALPERIKSA_TEXT = sDATE.ToString("ddMMyyyy") And x.ISPANGGIL = 2).OrderBy(Function(x) x.KODEBOOKING).ToList()

            If ds.Count > 0 Then
                GetDataByDatePending = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = ds.FirstOrDefault.KODEBOOKING)
            Else
                GetDataByDatePending = Nothing
            End If

        End Function
        Public Function InsertData(ByVal entity As ANTRIAN, ByVal isChekin As Boolean) As String
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "INSERT"

                sMODUL = sMODUL & entity.JENISPASIEN_RS

                'Generate Auto Number
                Try
                    sLASTNUMBER = oCounter.GetLastNumberDayAntrianRS(sMODUL, entity.TANGGALPERIKSA)

                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertDataAntrianRS(sMODUL, entity.TANGGALPERIKSA)
                            sLASTNUMBER = oCounter.GetLastNumberDayAntrianRS(sMODUL, entity.TANGGALPERIKSA)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KODEBOOKING = AutoNumberAntrian(entity.JENISPASIEN_RS, sLASTNUMBER + 1, entity.TANGGALPERIKSA)
                    entity.NOMORANTREAN = entity.JENISPASIEN_RS & "-" & sLASTNUMBER + 1
                    entity.ANGKAANTREAN = sLASTNUMBER + 1
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                'End Generate

                Try
                    oConnection.db.ANTRIANs.InsertOnSubmit(entity)
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
                    oCounter.UpdateDataAntrianRS(sMODUL, sLASTNUMBER + 1, Month(entity.TANGGALPERIKSA), Year(entity.TANGGALPERIKSA), Day(entity.TANGGALPERIKSA))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                If isChekin = True Then
                    Try
                        Dim oAntrianCheckin As New AntrianRS.clsAntrian_Checkin

                        Dim ds = oAntrianCheckin.GetStructureHeader

                        With ds
                            .DATECREATED = Now
                            .KODEBOOKING = entity.KODEBOOKING
                            .SEQ = 0
                            .WAKTU = Now
                            .KETERANGAN = "PENDAFTARAN"
                        End With

                        oAntrianCheckin.InsertData(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If
                InsertData = entity.KODEBOOKING
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODEBOOKING
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = entity.KODEBOOKING)

                Try
                    oConnection.db.ANTRIANs.DeleteOnSubmit(ds)
                    oConnection.db.ANTRIANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKODEBOOKING As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKODEBOOKING
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                Try
                    oConnection.db.ANTRIANs.DeleteOnSubmit(ds)
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
        Public Function UpdateIsPanggil(ByVal sKODEBOOKING As String, ByVal sISPANGGIL As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsPanggil = False
                    Exit Function
                End If

                UpdateIsPanggil = True

                Dim ds = oConnection.db.ANTRIANs.FirstOrDefault(Function(x) x.KODEBOOKING = sKODEBOOKING)

                ds.ISPANGGIL = sISPANGGIL

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIsPanggil = False
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
    End Class
End Namespace