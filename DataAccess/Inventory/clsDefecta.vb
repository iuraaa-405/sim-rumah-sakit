Namespace Inventory
    Public Class clsDefecta
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
            sMODUL = "DEFECTA"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_DEFECTA_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_DEFECTA_H
        End Function
        Public Function GetStructureDetail() As I_DEFECTA_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_DEFECTA_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_DEFECTA_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_DEFECTA_D)
        End Function
        Public Function GetData() As List(Of I_DEFECTA_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_DEFECTA_Hs.OrderByDescending(Function(x) x.KDDEFECTA).ToList()
        End Function
        Public Function GetData(ByVal sKDDefecta As String) As I_DEFECTA_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_DEFECTA_Hs.FirstOrDefault(Function(x) x.KDDEFECTA = sKDDefecta)
        End Function
        Public Function GetDataDetail() As List(Of I_DEFECTA_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_DEFECTA_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDDefecta As String) As List(Of I_DEFECTA_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_DEFECTA_Ds.Where(Function(x) x.KDDEFECTA = sKDDefecta).ToList()
        End Function
        Public Function InsertData(ByVal entity As I_DEFECTA_H, ByVal entityDetail As List(Of I_DEFECTA_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEFECTA
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

                    entity.KDDEFECTA = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDDEFECTA = entity.KDDEFECTA
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.I_DEFECTA_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_DEFECTA_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As I_DEFECTA_H, ByVal entityDetail As List(Of I_DEFECTA_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDEFECTA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_DEFECTA_Hs.FirstOrDefault(Function(x) x.KDDEFECTA = entity.KDDEFECTA)

                Try
                    oConnection.db.I_DEFECTA_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_DEFECTA_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_DEFECTA_Ds.Where(Function(x) x.KDDEFECTA = entity.KDDEFECTA)


                Try
                    oConnection.db.I_DEFECTA_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_DEFECTA_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDDefecta As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDDefecta
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_DEFECTA_Hs.FirstOrDefault(Function(x) x.KDDEFECTA = sKDDefecta)
                Dim dsDetail = oConnection.db.I_DEFECTA_Ds.Where(Function(x) x.KDDEFECTA = sKDDefecta)

                Try
                    oConnection.db.I_DEFECTA_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_DEFECTA_Ds.DeleteAllOnSubmit(dsDetail)

                    'ds.ISDELETE = True
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