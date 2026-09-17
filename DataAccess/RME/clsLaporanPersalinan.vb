Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsLaporanPersalinan
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sREFERENCE As String = ""
        Public sMODUL As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "SLP"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_LAPORANPERSALINAN
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_LAPORANPERSALINAN
        End Function
        Public Function GetData(ByVal sKDLAPROANPERSALINAN As String) As S_DIGITAL_LAPORANPERSALINAN
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.FirstOrDefault(Function(x) x.KDLAPROANPERSALINAN = sKDLAPROANPERSALINAN And x.ISDELETE = False)
        End Function
        Public Function GetDataDetailbykdidentitasList(ByVal sKDIDENTITAS As Integer) As List(Of S_DIGITAL_LAPORANPERSALINAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailbykdidentitasList = Nothing
                Exit Function
            End If
            GetDataDetailbykdidentitasList = oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.Where(Function(x) x.KDIDENTITAS = sKDIDENTITAS And x.ISDELETE = False).ToList()
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_LAPORANPERSALINAN) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPROANPERSALINAN

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDLAPROANPERSALINAN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDLAPROANPERSALINAN
            Catch ex As Exception
                InsertData = ""
                oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_LAPORANPERSALINAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDLAPROANPERSALINAN

                Dim ds = oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.FirstOrDefault(Function(x) x.KDLAPROANPERSALINAN = entity.KDLAPROANPERSALINAN)

                Try
                    oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDelete(ByVal KDLAPROANPERSALINAN As String, ByVal sUSER As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDelete = False
                    Exit Function
                End If

                sREFERENCE = KDLAPROANPERSALINAN

                Try
                    Dim ds = oConnection.dbRME.S_DIGITAL_LAPORANPERSALINANs.FirstOrDefault(Function(x) x.KDLAPROANPERSALINAN = KDLAPROANPERSALINAN)

                    ds.ISDELETE = 0
                    ds.DATEDELETE = Now
                    ds.USERDELETE = sUSER

                    oConnection.dbRME.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "UPDATEDELETE", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDelete = True
            Catch ex As Exception
                UpdateDelete = False
                oError.InsertData("S_DIGITAL_LAPORANPERSALINAN", "UPDATEDELETE", ex.ToString, sREFERENCE)
                Throw ex
            End Try

        End Function
    End Class
End Namespace