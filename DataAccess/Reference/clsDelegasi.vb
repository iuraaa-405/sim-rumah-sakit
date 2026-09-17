Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsDelegasi
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
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            oCounter = New Setting.clsCounter
            sMODUL = "DELEGASI"
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
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.OrderBy(Function(x) x.KDDELEGASI).ToList()
        End Function
        Public Function GetDataKodeKunjungan(ByVal sKDKUNJUNGAN As String) As S_PENDAFTARAN_KUNJUNGAN_DELEGASI
            If Not oConnection.GetConnection() Then
                GetDataKodeKunjungan = Nothing
                Exit Function
            End If
            GetDataKodeKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetData(ByVal sKDDELEGASI As String) As S_PENDAFTARAN_KUNJUNGAN_DELEGASI
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI = sKDDELEGASI)
        End Function
        Public Function GetDataSync() As List(Of S_PENDAFTARAN_KUNJUNGAN_DELEGASI)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.OrderBy(Function(x) x.KDDELEGASI).ToList()
        End Function
        Public Function IsExist(ByVal sKDDELEGASI As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI = sKDDELEGASI)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_KUNJUNGAN_DELEGASI) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDELEGASI
                sSTATUS = "INSERT"

                'Generate Auto Number
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

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                'End Generate

                Try
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
        Public Function DeleteData(ByVal sKDDELEGASI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDELEGASI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.FirstOrDefault(Function(x) x.KDDELEGASI = sKDDELEGASI)

                Try
                    oConnection.db.S_PENDAFTARAN_KUNJUNGAN_DELEGASIs.DeleteOnSubmit(ds)
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
    End Class
End Namespace