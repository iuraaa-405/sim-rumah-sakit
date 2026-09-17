Namespace Reference
    Public Class clsSimpanPACS
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

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

            sMODUL = "PACS"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_SO_TRANSAKSI_PACSSIMPAN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SO_TRANSAKSI_PACSSIMPAN
        End Function
        'Public Function GetData() As List(Of S_SO_TRANSAKSI_PACSSIMPAN)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        'End Function
        Public Function GetData(ByVal sKDSOTRANSAKSI As String, ByVal sSEQ As Integer) As S_SO_TRANSAKSI_PACSSIMPAN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.SEQ = sSEQ)
        End Function
        Public Function GetDataByStudi(ByVal sSTUDY As String) As S_SO_TRANSAKSI_PACSSIMPAN
            If Not oConnection.GetConnection() Then
                GetDataByStudi = Nothing
                Exit Function
            End If
            GetDataByStudi = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.FirstOrDefault(Function(x) x.ID = sSTUDY)
        End Function
        'Public Function GetDataSync() As List(Of S_SO_TRANSAKSI_PACSSIMPAN)
        '    If Not oConnection.GetConnection() Then
        '        GetDataSync = Nothing
        '        Exit Function
        '    End If
        '    GetDataSync = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.OrderBy(Function(x) x.NAME_DISPLAY).ToList()
        'End Function
        'Public Function IsExist(ByVal sNAME_DISPLAY As String) As Boolean
        '    If Not oConnection.GetConnection() Then
        '        IsExist = False
        '        Exit Function
        '    End If

        '    Dim ds = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.FirstOrDefault(Function(x) x.NAME_DISPLAY = sNAME_DISPLAY)

        '    If ds IsNot Nothing Then
        '        IsExist = True
        '    Else
        '        IsExist = False
        '    End If
        'End Function
        Public Function InsertData(ByVal entity As S_SO_TRANSAKSI_PACSSIMPAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.SEQ
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_SO_TRANSAKSI_PACSSIMPAN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI And x.SEQ = entity.SEQ)

                Try
                    oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDSOTRANSAKSI As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.SEQ = sSEQ)

                Try
                    oConnection.db.S_SO_TRANSAKSI_PACSSIMPANs.DeleteOnSubmit(ds)
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