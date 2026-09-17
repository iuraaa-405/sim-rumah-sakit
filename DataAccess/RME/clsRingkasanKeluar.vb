Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsRingkasanKeluar
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0
        Private oData As New Grouper.clsR_Identitas_Grouper_Data

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "RKINAP"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_RINGKASANKELUARRAWATINAP
        End Function
        Public Function GetStructureDetail_1() As S_RINGKASANKELUARRAWATINAP_D
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail_1 = Nothing
            End If
            GetStructureDetail_1 = New S_RINGKASANKELUARRAWATINAP_D
        End Function
        Public Function GetStructureDetail_1List() As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail_1List = Nothing
            End If
            GetStructureDetail_1List = New List(Of S_RINGKASANKELUARRAWATINAP_D)
        End Function
        Public Function GetStructureDetail_2() As S_RINGKASANKELUARRAWATINAP_P
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail_2 = Nothing
            End If
            GetStructureDetail_2 = New S_RINGKASANKELUARRAWATINAP_P
        End Function
        Public Function GetStructureDetail_2List() As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail_2List = Nothing
            End If
            GetStructureDetail_2List = New List(Of S_RINGKASANKELUARRAWATINAP_P)
        End Function
        Public Function GetData(ByVal sKDREG As String) As S_RINGKASANKELUARRAWATINAP
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = sKDREG)
        End Function
        Public Function GetDataDetail_1() As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail_1 = Nothing
                Exit Function
            End If
            GetDataDetail_1 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.ToList()
        End Function
        Public Function GetDataDetail_1(ByVal sKDREG As String) As List(Of S_RINGKASANKELUARRAWATINAP_D)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail_1 = Nothing
                Exit Function
            End If
            GetDataDetail_1 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDREG = sKDREG).ToList()
        End Function
        Public Function GetDataDetail_2() As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail_2 = Nothing
                Exit Function
            End If
            GetDataDetail_2 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.ToList()
        End Function
        Public Function GetDataDetail_2(ByVal sKDREG As String) As List(Of S_RINGKASANKELUARRAWATINAP_P)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail_2 = Nothing
                Exit Function
            End If
            GetDataDetail_2 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDREG = sKDREG).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_RINGKASANKELUARRAWATINAP, ByVal entityDetail_1 As List(Of S_RINGKASANKELUARRAWATINAP_D), ByVal entityDetail_2 As List(Of S_RINGKASANKELUARRAWATINAP_P)) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDREG

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)
                    If entityDetail_1.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetail_1)
                    End If
                    If entityDetail_2.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetail_2)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_RINGKASANKELUARRAWATINAP", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_RINGKASANKELUARRAWATINAP", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDREG
            Catch ex As Exception
                InsertData = ""
                oError.InsertData("S_RINGKASANKELUARRAWATINAP", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_RINGKASANKELUARRAWATINAP, ByVal entityDetail_1 As List(Of S_RINGKASANKELUARRAWATINAP_D), ByVal entityDetail_2 As List(Of S_RINGKASANKELUARRAWATINAP_P)) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDREG

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetail_1 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.Where(Function(x) x.KDREG = entity.KDREG)
                Dim dsDetail_2 = oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.Where(Function(x) x.KDREG = entity.KDREG)

                Try
                    oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.InsertOnSubmit(entity)

                    If dsDetail_1.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.DeleteAllOnSubmit(dsDetail_1)
                    End If
                    If entityDetail_1.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ds.InsertAllOnSubmit(entityDetail_1)
                    End If
                    If dsDetail_2.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.DeleteAllOnSubmit(dsDetail_2)
                    End If
                    If entityDetail_2.Count > 0 Then
                        oConnection.dbRME.S_RINGKASANKELUARRAWATINAP_Ps.InsertAllOnSubmit(entityDetail_2)
                    End If
                Catch ex As Exception
                    oError.InsertData("S_RINGKASANKELUARRAWATINAP", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_RINGKASANKELUARRAWATINAP", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_RINGKASANKELUARRAWATINAP", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDelete(ByVal KDREG As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDelete = False
                    Exit Function
                End If

                sREFERENCE = KDREG

                Try
                    Dim ds = oConnection.dbRME.S_RINGKASANKELUARRAWATINAPs.FirstOrDefault(Function(x) x.KDREG = KDREG)

                    ds.ISDELETE = 0
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_RINGKASANKELUARRAWATINAP", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDelete = True
            Catch ex As Exception
                UpdateDelete = False
                oError.InsertData("S_RINGKASANKELUARRAWATINAP", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace