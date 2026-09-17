Imports System.Threading

Namespace Transaksi
    Public Class clsTransferInternal
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
            sMODUL = "TFI"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_TRANSFER_INTERNAL
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_TRANSFER_INTERNAL
        End Function
        Public Function GetData() As List(Of S_TRANSFER_INTERNAL)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_TRANSFER_INTERNALs.OrderByDescending(Function(x) x.KDTRANSFERINTERNAL).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_TRANSFER_INTERNAL)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.dbRME.S_TRANSFER_INTERNALs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDTRANSFERINTERNAL).ToList()
        End Function
        Public Function GetDataByregister(ByVal Parameter As String) As S_TRANSFER_INTERNAL
            If Not oConnection.GetConnectionRME() Then
                GetDataByregister = Nothing
                Exit Function
            End If
            GetDataByregister = oConnection.dbRME.S_TRANSFER_INTERNALs.FirstOrDefault(Function(x) x.KDREG = Parameter)
        End Function
        Public Function GetData(ByVal Parameter As String) As S_TRANSFER_INTERNAL
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_TRANSFER_INTERNALs.FirstOrDefault(Function(x) x.KDTRANSFERINTERNAL = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_TRANSFER_INTERNAL) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDTRANSFERINTERNAL
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

                    entity.KDTRANSFERINTERNAL = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

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

                Try
                    oConnection.dbRME.S_TRANSFER_INTERNALs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDTRANSFERINTERNAL
            Catch ex As Exception
                InsertData = ""
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_TRANSFER_INTERNAL) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTRANSFERINTERNAL
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_TRANSFER_INTERNALs.FirstOrDefault(Function(x) x.KDTRANSFERINTERNAL = entity.KDTRANSFERINTERNAL)

                Try
                    oConnection.dbRME.S_TRANSFER_INTERNALs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_TRANSFER_INTERNALs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
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
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_TRANSFER_INTERNALs.FirstOrDefault(Function(x) x.KDTRANSFERINTERNAL = Parameter)

                Try
                    oConnection.dbRME.S_TRANSFER_INTERNALs.DeleteOnSubmit(ds)
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            Finally
                oConnection.dbRME.Dispose()
            End Try
        End Function
        Public Function UpdateDeleteTransferInternal(ByVal sKDTRANSFERINTERNAL As String, ByVal sUSER As String, ByVal sPesan As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDeleteTransferInternal = False
                    Exit Function
                End If

                sREFERENCE = sKDTRANSFERINTERNAL

                Try
                    Dim ds = oConnection.dbRME.S_TRANSFER_INTERNALs.FirstOrDefault(Function(x) x.KDTRANSFERINTERNAL = sKDTRANSFERINTERNAL)

                    ds.TEXT56 = "1"
                    ds.TEXT57 = Now.ToString("dd-MM-yyyy HH:mm:ss")
                    ds.TEXT58 = sUSER & " " & sPesan

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_TRANSFER_INTERNAL", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDeleteTransferInternal = True
            Catch ex As Exception
                UpdateDeleteTransferInternal = False
                oError.InsertData("S_TRANSFER_INTERNAL", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace