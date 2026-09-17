Imports System.Threading

Namespace Sales
    Public Class clsSalesKatalog
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "KATALOG"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_SO_KATALOG_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SO_KATALOG_H
        End Function
        Public Function GetStructureDetail() As S_SO_KATALOG_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_SO_KATALOG_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_SO_KATALOG_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_SO_KATALOG_D)
        End Function
        Public Function GetData() As List(Of S_SO_KATALOG_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_KATALOG_Hs.OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetData(ByVal sKDSOTRANSAKSI As String) As S_SO_KATALOG_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_KATALOG_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
        End Function
        Public Function GetDataDetail() As List(Of S_SO_KATALOG_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_KATALOG_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sNOAPOTIK As String) As List(Of S_SO_KATALOG_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_SO_KATALOG_Ds.Where(Function(x) x.NOAPOTIK = sNOAPOTIK).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_SO_KATALOG_H, ByVal entityDetail As List(Of S_SO_KATALOG_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_SO_KATALOG_Hs.InsertOnSubmit(entity)
                    oConnection.db.S_SO_KATALOG_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_SO_KATALOG_H, ByVal entityDetail As List(Of S_SO_KATALOG_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_SO_KATALOG_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_KATALOG_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_KATALOG_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.S_SO_KATALOG_Ds.Where(Function(x) x.NOAPOTIK = entity.NOAPOTIK)

                Try
                    oConnection.db.S_SO_KATALOG_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_SO_KATALOG_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDSOTRANSAKSI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_KATALOG_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)
                Dim dsDetail = oConnection.db.S_SO_KATALOG_Ds.Where(Function(x) x.NOAPOTIK = ds.NOAPOTIK)

                Try
                    oConnection.db.S_SO_KATALOG_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_KATALOG_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace