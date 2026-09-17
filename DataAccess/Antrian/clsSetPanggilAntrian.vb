Imports System.Threading

Namespace AntrianRS
    Public Class clsSetPanggilSET_PANGGIL_ANTRIAN
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
            sMODUL = "SET_PANGGIL_ANTRIAN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_PANGGIL_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_PANGGIL_ANTRIAN
        End Function
        Public Function GetStructureHeaderPoli() As SET_PANGGIL_ANTRIAN_POLI
            If Not oConnection.GetConnection() Then
                GetStructureHeaderPoli = Nothing
            End If
            GetStructureHeaderPoli = New SET_PANGGIL_ANTRIAN_POLI
        End Function
        Public Function GetStructureHeaderFarmasi() As SET_PANGGIL_ANTRIAN_FARMASI
            If Not oConnection.GetConnection() Then
                GetStructureHeaderFarmasi = Nothing
            End If
            GetStructureHeaderFarmasi = New SET_PANGGIL_ANTRIAN_FARMASI
        End Function
        Public Function GetData() As List(Of SET_PANGGIL_ANTRIAN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_PANGGIL_ANTRIANs.OrderByDescending(Function(x) x.LOKET).ToList()
        End Function
        Public Function GetData(ByVal sLOKET As String) As SET_PANGGIL_ANTRIAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.LOKET = sLOKET)
        End Function
        Public Function GetDataPoli(ByVal sLOKET As String) As SET_PANGGIL_ANTRIAN_POLI
            If Not oConnection.GetConnection() Then
                GetDataPoli = Nothing
                Exit Function
            End If
            GetDataPoli = oConnection.db.SET_PANGGIL_ANTRIAN_POLIs.FirstOrDefault(Function(x) x.LOKET = sLOKET)
        End Function
        Public Function GetDataFarmasi(ByVal sLOKET As String) As SET_PANGGIL_ANTRIAN_FARMASI
            If Not oConnection.GetConnection() Then
                GetDataFarmasi = Nothing
                Exit Function
            End If
            GetDataFarmasi = oConnection.db.SET_PANGGIL_ANTRIAN_FARMASIs.FirstOrDefault(Function(x) x.LOKET = sLOKET)
        End Function
        Public Function InsertDataPoli(ByVal entity As SET_PANGGIL_ANTRIAN_POLI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataPoli = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_PANGGIL_ANTRIAN_POLIs.InsertOnSubmit(entity)
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

                InsertDataPoli = True
            Catch ex As Exception
                InsertDataPoli = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataFarmasi(ByVal entity As SET_PANGGIL_ANTRIAN_FARMASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertDataFarmasi = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_PANGGIL_ANTRIAN_FARMASIs.InsertOnSubmit(entity)
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

                InsertDataFarmasi = True
            Catch ex As Exception
                InsertDataFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertData(ByVal entity As SET_PANGGIL_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "INSERT"

                ''Generate Auto Number
                'Try
                '    sLASTNUMBER = oCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertDataSET_PANGGIL_ANTRIAN(sMODUL, entity.TANGGALPERIKSA)
                '            sLASTNUMBER = oCounter.GetLastNumberDay(sMODUL, entity.TANGGALPERIKSA)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.LOKET = AutoNumberSET_PANGGIL_ANTRIAN(entity.JENISPASIEN_RS, sLASTNUMBER + 1, entity.TANGGALPERIKSA)
                '    entity.NOMORANTREAN = entity.JENISPASIEN_RS & "-" & sLASTNUMBER + 1
                '    entity.ANGKAANTREAN = sLASTNUMBER + 1
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                ''End Generate

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.InsertOnSubmit(entity)
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
                '    oCounter.UpdateDataSET_PANGGIL_ANTRIAN(sMODUL, sLASTNUMBER + 1, Month(entity.TANGGALPERIKSA), Year(entity.TANGGALPERIKSA), Day(entity.TANGGALPERIKSA))
                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataPoli(ByVal entity As SET_PANGGIL_ANTRIAN_POLI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataPoli = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIAN_POLIs.FirstOrDefault(Function(x) x.LOKET = entity.LOKET)

                Try
                    oConnection.db.SET_PANGGIL_ANTRIAN_POLIs.DeleteOnSubmit(ds)
                    oConnection.db.SET_PANGGIL_ANTRIAN_POLIs.InsertOnSubmit(entity)
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

                UpdateDataPoli = True
            Catch ex As Exception
                UpdateDataPoli = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataFarmasi(ByVal entity As SET_PANGGIL_ANTRIAN_FARMASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateDataFarmasi = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIAN_FARMASIs.FirstOrDefault(Function(x) x.LOKET = entity.LOKET)

                Try
                    oConnection.db.SET_PANGGIL_ANTRIAN_FARMASIs.DeleteOnSubmit(ds)
                    oConnection.db.SET_PANGGIL_ANTRIAN_FARMASIs.InsertOnSubmit(entity)
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

                UpdateDataFarmasi = True
            Catch ex As Exception
                UpdateDataFarmasi = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_PANGGIL_ANTRIAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.LOKET = entity.LOKET)

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.DeleteOnSubmit(ds)
                    oConnection.db.SET_PANGGIL_ANTRIANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sLOKET As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sLOKET
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.LOKET = sLOKET)

                Try
                    oConnection.db.SET_PANGGIL_ANTRIANs.DeleteOnSubmit(ds)
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
        Public Function UpdateIsPanggil(ByVal sLOKET As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsPanggil = False
                    Exit Function
                End If

                UpdateIsPanggil = True

                Dim ds = oConnection.db.SET_PANGGIL_ANTRIANs.FirstOrDefault(Function(x) x.LOKET = sLOKET)

                ds.ISPANGGIL = True

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIsPanggil = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace