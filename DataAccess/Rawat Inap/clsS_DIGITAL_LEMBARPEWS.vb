Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_LEMBARPEWS
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sLASTNUMBER As Integer = 0
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "PEWS"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_LEMBARPEWS_H
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_LEMBARPEWS_H
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_LEMBARPEWS_D
            If Not oConnection.GetConnectionRME Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_LEMBARPEWS_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_LEMBARPEWS_D)
            If Not oConnection.GetConnectionRME Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_LEMBARPEWS_D)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_LEMBARPEWS_H)
            If Not oConnection.GetConnectionRME Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.OrderBy(Function(x) x.KDPEWS).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_LEMBARPEWS_H
            If Not oConnection.GetConnectionRME Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.FirstOrDefault(Function(x) x.KDPEWS = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_LEMBARPEWS_D)
            If Not oConnection.GetConnectionRME Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_LEMBARPEWS_D)
            If Not oConnection.GetConnectionRME Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.Where(Function(x) x.KDPEWS = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnectionRME Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.FirstOrDefault(Function(x) x.KDPEWS = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_LEMBARPEWS_H, ByVal entityDetail As List(Of S_DIGITAL_LEMBARPEWS_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPEWS
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

                    entity.KDPEWS = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDPEWS = entity.KDPEWS
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_LEMBARPEWS_H, ByVal entityDetail As List(Of S_DIGITAL_LEMBARPEWS_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPEWS
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.FirstOrDefault(Function(x) x.KDPEWS = entity.KDPEWS)

                Try
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.Where(Function(x) x.KDPEWS = entity.KDPEWS)

                Try
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.InsertAllOnSubmit(entityDetail)
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

                Dim ds = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.FirstOrDefault(Function(x) x.KDPEWS = Parameter)
                Dim dsDetail = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.Where(Function(x) x.KDPEWS = Parameter)

                Try
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Ds.DeleteAllOnSubmit(dsDetail)

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

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function UpdateCetak(ByVal sKDPEWS As String, ByVal sKode As String) As Boolean
        '    Try
        '        If Not oConnection.GetConnectionRME Then
        '            UpdateCetak = False
        '            Exit Function
        '        End If

        '        Try
        '            Dim ds = oConnection.dbRME.S_DIGITAL_LEMBARPEWS_Hs.FirstOrDefault(Function(x) x.KDPEWS = sKDPEWS And x.KODE = sKode)

        '            ds.CETAK += 1

        '            oConnection.dbRME.SubmitChanges()

        '        Catch ex As Exception
        '            oError.InsertData("S_DIGITAL_LEMBARPEWS_H", "UPDATECETAK", ex.ToString, sKDPEWS)
        '            Throw ex
        '        End Try

        '        UpdateCetak = True
        '    Catch ex As Exception
        '        UpdateCetak = False
        '        oError.InsertData("S_DIGITAL_LEMBARPEWS_H", "UPDATECETAK", ex.ToString, sKDPEWS)
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace