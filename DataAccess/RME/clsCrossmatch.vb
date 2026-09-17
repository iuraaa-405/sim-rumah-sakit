Imports System.Threading

Namespace Order
    Public Class clsCrossmatch
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
            sMODUL = "CROSSMATCH"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_CROSSMATCH
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_CROSSMATCH
        End Function
        Public Function GetStructureDetail() As R_CROSSMATCH_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_CROSSMATCH_D
        End Function
        Public Function GetStructureDetailList() As List(Of R_CROSSMATCH_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_CROSSMATCH_D)
        End Function
        Public Function GetData() As List(Of R_CROSSMATCH)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CROSSMATCHes.OrderByDescending(Function(x) x.KDCROSSMATCH).ToList()
        End Function
        Public Function GetData(ByVal sKDCROSSMATCH As String) As R_CROSSMATCH
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CROSSMATCHes.FirstOrDefault(Function(x) x.KDCROSSMATCH = sKDCROSSMATCH)
        End Function
        Public Function GetDataBy(ByVal sKDCROSSMATCH As String) As R_CROSSMATCH
            If Not oConnection.GetConnectionRME() Then
                GetDataBy = Nothing
                Exit Function
            End If
            GetDataBy = oConnection.dbRME.R_CROSSMATCHes.FirstOrDefault(Function(x) x.MemoEdit9 = sKDCROSSMATCH)
        End Function
        Public Function GetDataDetail() As List(Of R_CROSSMATCH_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_CROSSMATCH_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDCROSSMATCH As String) As List(Of R_CROSSMATCH_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_CROSSMATCH_Ds.Where(Function(x) x.KDCROSSMATCH = sKDCROSSMATCH).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_CROSSMATCH, ByVal entityDetail As List(Of R_CROSSMATCH_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCROSSMATCH
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDCROSSMATCH = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDCROSSMATCH = entity.KDCROSSMATCH
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_CROSSMATCHes.InsertOnSubmit(entity)
                    oConnection.dbRME.R_CROSSMATCH_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_CROSSMATCH, ByVal entityDetail As List(Of R_CROSSMATCH_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCROSSMATCH
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_CROSSMATCHes.FirstOrDefault(Function(x) x.KDCROSSMATCH = entity.KDCROSSMATCH)

                Try
                    oConnection.dbRME.R_CROSSMATCHes.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CROSSMATCHes.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.R_CROSSMATCH_Ds.Where(Function(x) x.KDCROSSMATCH = entity.KDCROSSMATCH)

                Try
                    oConnection.dbRME.R_CROSSMATCH_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.R_CROSSMATCH_Ds.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDCROSSMATCH As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCROSSMATCH
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.R_CROSSMATCHes.FirstOrDefault(Function(x) x.KDCROSSMATCH = sKDCROSSMATCH)
                Dim dsDetail = oConnection.dbRME.R_CROSSMATCH_Ds.Where(Function(x) x.KDCROSSMATCH = sKDCROSSMATCH)

                Try
                    oConnection.dbRME.R_CROSSMATCHes.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CROSSMATCH_Ds.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace