Imports System.Threading

Namespace EMedrek
    Public Class clsLembarObservasi
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
            sMODUL = "OBS"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_LEMBAROBSERVASI_H
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_LEMBAROBSERVASI_H
        End Function
        Public Function GetStructureDetail() As S_LEMBAROBSERVASI_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_LEMBAROBSERVASI_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_LEMBAROBSERVASI_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_LEMBAROBSERVASI_D)
        End Function
        Public Function GetData() As List(Of S_LEMBAROBSERVASI_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_LEMBAROBSERVASI_Hs.OrderByDescending(Function(x) x.KDLEMBAROBSERVASI).ToList()
        End Function
        Public Function GetData(ByVal sKDLEMBAROBSERVASI As String) As S_LEMBAROBSERVASI_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_LEMBAROBSERVASI_Hs.FirstOrDefault(Function(x) x.KDLEMBAROBSERVASI = sKDLEMBAROBSERVASI)
        End Function
        Public Function GetDataDetail() As List(Of S_LEMBAROBSERVASI_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_LEMBAROBSERVASI_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDLEMBAROBSERVASI As String) As List(Of S_LEMBAROBSERVASI_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_LEMBAROBSERVASI_Ds.Where(Function(x) x.KDLEMBAROBSERVASI = sKDLEMBAROBSERVASI).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_LEMBAROBSERVASI_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.dbRME.S_LEMBAROBSERVASI_Hs.Where(Function(x) x.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDLEMBAROBSERVASI).ToList()
        End Function
        Public Function GetDataByRMTerakhir(ByVal kdcustomer As String) As List(Of S_LEMBAROBSERVASI_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRMTerakhir = Nothing
                Exit Function
            End If
            GetDataByRMTerakhir = oConnection.dbRME.S_LEMBAROBSERVASI_Ds.Where(Function(x) x.S_LEMBAROBSERVASI_H.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.DATE).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_LEMBAROBSERVASI_H, ByVal entityDetail As List(Of S_LEMBAROBSERVASI_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLEMBAROBSERVASI
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

                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    entity.KDLEMBAROBSERVASI = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        iLoop.KDLEMBAROBSERVASI = entity.KDLEMBAROBSERVASI
                    Next
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.S_LEMBAROBSERVASI_Hs.InsertOnSubmit(entity)
                    oConnection.dbRME.S_LEMBAROBSERVASI_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_LEMBAROBSERVASI_H, ByVal entityDetail As List(Of S_LEMBAROBSERVASI_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLEMBAROBSERVASI
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_LEMBAROBSERVASI_Hs.FirstOrDefault(Function(x) x.KDLEMBAROBSERVASI = entity.KDLEMBAROBSERVASI)

                Try
                    oConnection.dbRME.S_LEMBAROBSERVASI_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_LEMBAROBSERVASI_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.dbRME.S_LEMBAROBSERVASI_Ds.Where(Function(x) x.KDLEMBAROBSERVASI = entity.KDLEMBAROBSERVASI)

                Try
                    oConnection.dbRME.S_LEMBAROBSERVASI_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.S_LEMBAROBSERVASI_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal Parameter As String, ByVal sUser As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.dbRME.S_LEMBAROBSERVASI_Hs.FirstOrDefault(Function(x) x.KDLEMBAROBSERVASI = Parameter)
                Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEUPDATED = WaktuServer
                    ds.ISDELETE = True
                    ds.USERDELETE = sUser

                    oConnection.dbRME.SubmitChanges()

                End If

            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace