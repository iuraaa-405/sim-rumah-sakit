Imports System.Threading

Namespace Finance
    Public Class clsCashinBPJSBayar
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
            sMODUL = "CSBPJSBAYAR"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As F_CASHINBPJSBAYAR_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New F_CASHINBPJSBAYAR_H
        End Function
        Public Function GetStructureDetail() As F_CASHINBPJSBAYAR_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New F_CASHINBPJSBAYAR_D
        End Function
        Public Function GetStructureDetailList() As List(Of F_CASHINBPJSBAYAR_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of F_CASHINBPJSBAYAR_D)
        End Function
        Public Function GetData() As List(Of F_CASHINBPJSBAYAR_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHINBPJSBAYAR_Hs.OrderByDescending(Function(x) x.KDCASHINBAYARBPJS).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As F_CASHINBPJSBAYAR_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.F_CASHINBPJSBAYAR_Hs.FirstOrDefault(Function(x) x.KDCASHINBAYARBPJS = Parameter)
        End Function
        Public Function GetDataJudul(ByVal KDJUDUL As String) As List(Of F_CASHINBPJSBAYAR_H)
            If Not oConnection.GetConnection() Then
                GetDataJudul = Nothing
                Exit Function
            End If
            GetDataJudul = oConnection.db.F_CASHINBPJSBAYAR_Hs.Where(Function(x) x.JUDULJASABAYAR = KDJUDUL).ToList()
        End Function
        Public Function GetDataSEP(ByVal Parameter As String) As F_CASHINBPJSBAYAR_D
            If Not oConnection.GetConnection() Then
                GetDataSEP = Nothing
                Exit Function
            End If
            GetDataSEP = oConnection.db.F_CASHINBPJSBAYAR_Ds.FirstOrDefault(Function(x) x.NOSEP = Parameter)
        End Function
        Public Function GetDataHeader() As List(Of F_CASHINBPJSBAYAR_H)
            If Not oConnection.GetConnection() Then
                GetDataHeader = Nothing
                Exit Function
            End If
            GetDataHeader = oConnection.db.F_CASHINBPJSBAYAR_Hs.ToList()
        End Function
        Public Function GetDataDetail() As List(Of F_CASHINBPJSBAYAR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHINBPJSBAYAR_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of F_CASHINBPJSBAYAR_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.F_CASHINBPJSBAYAR_Ds.Where(Function(x) x.KDCASHINBPJSBAYAR = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As F_CASHINBPJSBAYAR_H, ByVal entityDetail As List(Of F_CASHINBPJSBAYAR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHINBAYARBPJS
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

                    entity.KDCASHINBAYARBPJS = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDCASHINBPJSBAYAR = entity.KDCASHINBAYARBPJS
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.F_CASHINBPJSBAYAR_Hs.InsertOnSubmit(entity)
                    oConnection.db.F_CASHINBPJSBAYAR_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As F_CASHINBPJSBAYAR_H, ByVal entityDetail As List(Of F_CASHINBPJSBAYAR_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCASHINBAYARBPJS
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.F_CASHINBPJSBAYAR_Hs.FirstOrDefault(Function(x) x.KDCASHINBAYARBPJS = entity.KDCASHINBAYARBPJS)

                Try
                    oConnection.db.F_CASHINBPJSBAYAR_Hs.DeleteOnSubmit(ds)
                    oConnection.db.F_CASHINBPJSBAYAR_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.F_CASHINBPJSBAYAR_Ds.Where(Function(x) x.KDCASHINBPJSBAYAR = entity.KDCASHINBAYARBPJS)

                Try
                    oConnection.db.F_CASHINBPJSBAYAR_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.F_CASHINBPJSBAYAR_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.db.F_CASHINBPJSBAYAR_Hs.Where(Function(x) x.KDCASHINBAYARBPJS.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDCASHINBAYARBPJS

                    Dim dsDetail = oConnection.db.F_CASHINBPJSBAYAR_Ds.Where(Function(x) x.KDCASHINBPJSBAYAR = sNOCASH)

                    Try
                        oConnection.db.F_CASHINBPJSBAYAR_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.F_CASHINBPJSBAYAR_Ds.DeleteAllOnSubmit(dsDetail)
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
        Public Function UpdateNomorSEPKDREG(ByVal sKDCASHINBAYARBPJS As String, ByVal sKDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateNomorSEPKDREG = False
                    Exit Function
                End If

                UpdateNomorSEPKDREG = True

                Dim ds = oConnection.db.F_CASHINBPJSBAYAR_Hs.FirstOrDefault(Function(x) x.KDCASHINBAYARBPJS = sKDCASHINBAYARBPJS)

                ds.MEMO = "Telah diperbaharui Tanggal : " & Now & " Oleh User : " & sKDUSER

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateNomorSEPKDREG = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace