Imports System.Threading

Namespace Inventory
    Public Class clsJadwalDokter
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "JADWALDOKTER"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_JADWALDOKTER_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_JADWALDOKTER_H
        End Function
        Public Function GetStructureDetail() As I_JADWALDOKTER_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_JADWALDOKTER_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_JADWALDOKTER_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_JADWALDOKTER_D)
        End Function
        Public Function GetData() As List(Of I_JADWALDOKTER_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_JADWALDOKTER_Hs.OrderByDescending(Function(x) x.KDJADWALDOKTER).ToList()
        End Function
        Public Function GetData(ByVal sKDJADWALDOKTER As String) As I_JADWALDOKTER_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_JADWALDOKTER_Hs.FirstOrDefault(Function(x) x.KDJADWALDOKTER = sKDJADWALDOKTER)
        End Function
        Public Function GetDataMonitoring(ByVal sKDDEPARTMENT As String, ByVal sKDDOCTOR As String, ByVal sHARI As String, ByVal JAMPRAKTEK As String) As I_JADWALDOKTER_D
            If Not oConnection.GetConnection() Then
                GetDataMonitoring = Nothing
                Exit Function
            End If
            GetDataMonitoring = oConnection.db.I_JADWALDOKTER_Ds.FirstOrDefault(Function(x) x.I_JADWALDOKTER_H.KDDEPARTMENT = sKDDEPARTMENT And x.I_JADWALDOKTER_H.KDDOCTOR = sKDDOCTOR And x.HARI = sHARI And x.LIBUR = 0 And x.BUKA & "-" & x.TUTUP = JAMPRAKTEK)
        End Function
        Public Function GetDataMonitoringSisaJKN(ByVal sKDDEPARTMENT As String, ByVal sKDDOCTOR As String) As Integer
            If Not oConnection.GetConnection() Then
                GetDataMonitoringSisaJKN = Nothing
                Exit Function
            End If
            GetDataMonitoringSisaJKN = oConnection.db.ANTRIANs.Where(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT And x.KDDOCTOR = sKDDOCTOR And x.TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy") And x.JENISPASIEN = "JKN").Count()
        End Function
        Public Function GetDataMonitoringSisaNONJKN(ByVal sKDDEPARTMENT As String, ByVal sKDDOCTOR As String) As Integer
            If Not oConnection.GetConnection() Then
                GetDataMonitoringSisaNONJKN = Nothing
                Exit Function
            End If
            GetDataMonitoringSisaNONJKN = oConnection.db.ANTRIANs.Where(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT And x.KDDOCTOR = sKDDOCTOR And x.TANGGALPERIKSA_TEXT = Now.ToString("ddMMyyyy") And x.JENISPASIEN <> "JKN").Count()
        End Function
        Public Function GetDataDetail() As List(Of I_JADWALDOKTER_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_JADWALDOKTER_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDJADWALDOKTER As String) As List(Of I_JADWALDOKTER_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_JADWALDOKTER_Ds.Where(Function(x) x.KDJADWALDOKTER = sKDJADWALDOKTER).ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDJADWALDOKTER As String, ByVal sSEQ As Integer) As I_JADWALDOKTER_D
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_JADWALDOKTER_Ds.FirstOrDefault(Function(x) x.KDJADWALDOKTER = sKDJADWALDOKTER And x.SEQ = sSEQ)
        End Function
        Public Function InsertData(ByVal entity As I_JADWALDOKTER_H, ByVal entityDetail As List(Of I_JADWALDOKTER_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJADWALDOKTER
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.I_JADWALDOKTER_Hs.OrderByDescending(Function(x) x.KDJADWALDOKTER).FirstOrDefault().KDJADWALDOKTER.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDJADWALDOKTER = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.I_JADWALDOKTER_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    If entityDetail IsNot Nothing Then
                        For Each iLoop In entityDetail
                            iLoop.KDJADWALDOKTER = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                        Next
                        oConnection.db.I_JADWALDOKTER_Ds.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.I_JADWALDOKTER_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_JADWALDOKTER_Ds.InsertAllOnSubmit(entityDetail)
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

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As I_JADWALDOKTER_H, ByVal entityDetail As List(Of I_JADWALDOKTER_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJADWALDOKTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_JADWALDOKTER_Hs.FirstOrDefault(Function(x) x.KDJADWALDOKTER = entity.KDJADWALDOKTER)

                Try
                    oConnection.db.I_JADWALDOKTER_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_JADWALDOKTER_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_JADWALDOKTER_Ds.Where(Function(x) x.KDJADWALDOKTER = entity.KDJADWALDOKTER)

                Try
                    oConnection.db.I_JADWALDOKTER_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_JADWALDOKTER_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDJADWALDOKTER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDJADWALDOKTER
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_JADWALDOKTER_Hs.FirstOrDefault(Function(x) x.KDJADWALDOKTER = sKDJADWALDOKTER)
                Dim dsDetail = oConnection.db.I_JADWALDOKTER_Ds.Where(Function(x) x.KDJADWALDOKTER = sKDJADWALDOKTER)

                Try
                    oConnection.db.I_JADWALDOKTER_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_JADWALDOKTER_Ds.DeleteAllOnSubmit(dsDetail)

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