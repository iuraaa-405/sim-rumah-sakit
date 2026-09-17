Imports System.Threading

Namespace Admission
    Public Class clsPendaftaran_Kunjungan_Delegasi
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
            sMODUL = "KUNJUNGANDELEGASI"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_KUNJUNGAN_DELEGASI
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_KUNJUNGAN_DELEGASI
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_KUNJUNGAN_DELEGASI)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.OrderByDescending(Function(x) x.KDDELEGASI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN_DELEGASI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI = Parameter)
        End Function
        Public Function GetDatabykd(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN_DELEGASI
            If Not oConnection.GetConnection() Then
                GetDatabykd = Nothing
                Exit Function
            End If
            GetDatabykd = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KUNJUNGAN_DELEGASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDELEGASI
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDDELEGASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                    oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.InsertOnSubmit(entity)
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_KUNJUNGAN_DELEGASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDELEGASI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI = entity.KDDELEGASI)

                Try
                    oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String, ByVal KDDELEGASIPERTAMA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.DeleteOnSubmit(ds)
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

            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace