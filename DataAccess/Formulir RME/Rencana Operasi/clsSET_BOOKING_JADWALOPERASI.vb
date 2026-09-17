Imports System.Threading

Namespace Sales
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
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_BOOKING_JADWALOPERASI
        End Function
        Public Function GetData() As List(Of SET_BOOKING_JADWALOPERASI)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_JADWALOPERASIs.OrderByDescending(Function(x) x.KDBOOKINGJADWALOPERASI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As SET_BOOKING_JADWALOPERASI
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = Parameter)
        End Function
        Public Function GetDataByKDKUNJUNGAN(ByVal Parameter As String) As SET_BOOKING_JADWALOPERASI
            If Not oConnection.GetConnection Then
                GetDataByKDKUNJUNGAN = Nothing
                Exit Function
            End If
            GetDataByKDKUNJUNGAN = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function IsExistByPeriode(ByVal sDate As DateTime, Byval sKamar As String, Byval sSesi As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExistByPeriode = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.TANGGALOPERASI >= sDate.ToString("yyyy-MM-dd") & " 00:00:00" And x.TANGGALOPERASI <= sDate.ToString("yyyy-MM-dd") & " 23:59:59" And x.KAMAR = sKamar And x.SESI = sSesi)

            If ds IsNot Nothing Then
                IsExistByPeriode = True
            Else
                IsExistByPeriode = False
            End If
        End Function
        Public Function InsertData(ByVal entity As SET_BOOKING_JADWALOPERASI) As String
            Try
                If Not oConnection.GetConnection Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDBOOKINGJADWALOPERASI
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALOPERASI)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.TANGGALOPERASI)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.TANGGALOPERASI)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If
                    entity.KDBOOKINGJADWALOPERASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.TANGGALOPERASI)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.TANGGALOPERASI), Year(entity.TANGGALOPERASI))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
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

                InsertData = entity.KDBOOKINGJADWALOPERASI
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_BOOKING_JADWALOPERASI) As Boolean
            Try
                If Not oConnection.GetConnection Then
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
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = Parameter)

                If ds Is Nothing Then
                    DeleteData = False
                    Exit Function
                End If


                Try
                    oConnection.db.SET_BOOKING_JADWALOPERASIs.DeleteOnSubmit(ds)
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
        Public Function UpdateAproval(ByVal sKDBOOKINGJADWALOPERASI As String, ByVal isaproval As Integer) As String
            Try
                If Not oConnection.GetConnection Then
                    UpdateAproval = False
                    Exit Function
                End If

                sREFERENCE = sKDBOOKINGJADWALOPERASI
                sSTATUS = "ISAPROVAL"

                Try

                    Dim ds = oConnection.db.SET_BOOKING_JADWALOPERASIs.FirstOrDefault(Function(x) x.KDBOOKINGJADWALOPERASI = sKDBOOKINGJADWALOPERASI)

                    ds.ISAPROVAL = isaproval

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateAproval = True
            Catch ex As Exception
                UpdateAproval = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace