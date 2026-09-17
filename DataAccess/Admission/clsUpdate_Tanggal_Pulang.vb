Imports System.Threading

Namespace Admission
    Public Class clsUpdate_Tanggal_Pulang
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
            sMODUL = "TGL_PULANG"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As T_UPDATE_TANGGAL_PULANG
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New T_UPDATE_TANGGAL_PULANG
        End Function
        Public Function GetData() As List(Of T_UPDATE_TANGGAL_PULANG)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.T_UPDATE_TANGGAL_PULANGs.OrderByDescending(Function(x) x.KDUPDATE_TANGGAL_PULANG).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As T_UPDATE_TANGGAL_PULANG
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault(Function(x) x.KDUPDATE_TANGGAL_PULANG = Parameter)
        End Function
        Public Function GetDatabyKD(ByVal Parameter As String) As T_UPDATE_TANGGAL_PULANG
            If Not oConnection.GetConnection() Then
                GetDatabyKD = Nothing
                Exit Function
            End If
            GetDatabyKD = oConnection.db.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function GetDataLast(ByVal sKDPENDAFTARAN As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection Then
                GetDataLast = Nothing
                Exit Function
            End If

            Dim ds = (From x In oConnection.db.S_PENDAFTARAN_KUNJUNGANs.Where(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN)
                      Select x).ToList()

            GetDataLast = ds.OrderByDescending(Function(x) x.DATE).FirstOrDefault()

        End Function
        Public Function InsertData(ByVal entity As T_UPDATE_TANGGAL_PULANG) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUPDATE_TANGGAL_PULANG
                sSTATUS = "INSERT"

                'Try
                '    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '    If sLASTNUMBER = 0 Then
                '        Try
                '            oCounter.InsertData(sMODUL, entity.DATE)
                '            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                '        Catch ex As Exception
                '            sLASTNUMBER = 0
                '        End Try
                '    End If

                '    entity.KDUPDATE_TANGGAL_PULANG = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                'Catch ex As Exception
                '    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                Try
                    entity.KDUPDATE_TANGGAL_PULANG = entity.KDPENDAFTARAN
                    oConnection.db.T_UPDATE_TANGGAL_PULANGs.InsertOnSubmit(entity)
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
                '    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
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
        Public Function UpdateData(ByVal entity As T_UPDATE_TANGGAL_PULANG) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUPDATE_TANGGAL_PULANG
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault(Function(x) x.KDUPDATE_TANGGAL_PULANG = entity.KDUPDATE_TANGGAL_PULANG)

                Try
                    oConnection.db.T_UPDATE_TANGGAL_PULANGs.DeleteOnSubmit(ds)
                    oConnection.db.T_UPDATE_TANGGAL_PULANGs.InsertOnSubmit(entity)

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

                Dim ds = oConnection.db.T_UPDATE_TANGGAL_PULANGs.FirstOrDefault(Function(x) x.KDUPDATE_TANGGAL_PULANG.Contains(Parameter))

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.T_UPDATE_TANGGAL_PULANGs.DeleteOnSubmit(ds)
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
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.T_UPDATE_TANGGAL_PULANGs

        '        For Each iLoop In entity
        '            Dim sKDUPDATE_TANGGAL_PULANG = iLoop.KDUPDATE_TANGGAL_PULANG
        '            Dim entityDetail = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDUPDATE_TANGGAL_PULANG = sKDUPDATE_TANGGAL_PULANG And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.F_PENDAFTARAN_Ds.Where(Function(x) x.KDUPDATE_TANGGAL_PULANG = sKDUPDATE_TANGGAL_PULANG And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace