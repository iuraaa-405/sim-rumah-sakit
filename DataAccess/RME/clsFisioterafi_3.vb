Imports System.Threading

Namespace EMedrek
    Public Class clsFisioterafi_3
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
            sMODUL = "FISIOTERAFI3"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_FISIOTERAFI_3_H
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_FISIOTERAFI_3_H
        End Function
        Public Function GetStructureDetail() As S_FISIOTERAFI_3_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_FISIOTERAFI_3_D
        End Function
        Public Function GetStructureDetailList() As List(Of S_FISIOTERAFI_3_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_FISIOTERAFI_3_D)
        End Function
        Public Function GetData() As List(Of S_FISIOTERAFI_3_H)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_FISIOTERAFI_3_Hs.OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_FISIOTERAFI_3_H
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_FISIOTERAFI_3_Hs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDatabykodeKunjungan(ByVal Parameter As String) As S_PENDAFTARAN_KUNJUNGAN
            If Not oConnection.GetConnection() Then
                GetDatabykodeKunjungan = Nothing
                Exit Function
            End If
            GetDatabykodeKunjungan = oConnection.db.S_PENDAFTARAN_KUNJUNGANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_FISIOTERAFI_3_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.S_FISIOTERAFI_3_Ds.Where(Function(x) x.KDKUNJUNGAN = Parameter).ToList()
        End Function
        Public Function GetDataTerakhir(ByVal Parameter As String) As S_FISIOTERAFI_3_H
            If Not oConnection.GetConnectionRME() Then
                GetDataTerakhir = Nothing
                Exit Function
            End If
            GetDataTerakhir = oConnection.dbRME.S_FISIOTERAFI_3_Hs.Where(Function(x) x.KDCUSTOMER = Parameter).OrderByDescending(Function(x) x.DATE).FirstOrDefault()
        End Function
        Public Function InsertData(ByVal entity As S_FISIOTERAFI_3_H, ByVal entityDetail As List(Of S_FISIOTERAFI_3_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer

                Try
                    For Each iLoop In entityDetail
                        iLoop.KDKUNJUNGAN = entity.KDKUNJUNGAN
                    Next

                    oConnection.dbRME.S_FISIOTERAFI_3_Hs.InsertOnSubmit(entity)
                    If entityDetail IsNot Nothing Then
                        oConnection.dbRME.S_FISIOTERAFI_3_Ds.InsertAllOnSubmit(entityDetail)
                    End If
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_FISIOTERAFI_3_H, ByVal entityDetail As List(Of S_FISIOTERAFI_3_D)) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_FISIOTERAFI_3_Hs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Dim dsDetail = oConnection.dbRME.S_FISIOTERAFI_3_Ds.Where(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.dbRME.S_FISIOTERAFI_3_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_FISIOTERAFI_3_Hs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.dbRME.S_FISIOTERAFI_3_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.S_FISIOTERAFI_3_Ds.InsertAllOnSubmit(entityDetail)
                    End If
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

                Dim ds = oConnection.dbRME.S_FISIOTERAFI_3_Hs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
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