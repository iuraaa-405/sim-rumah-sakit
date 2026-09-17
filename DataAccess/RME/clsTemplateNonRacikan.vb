Imports System.Data.SqlClient
Imports System.Threading

Namespace EMedrek
    Public Class clsTemplateNonRacikan
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
            sMODUL = "TEMPLATEOBAT"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_CPPT_NONRACIKAN_TEMPLATE_H
            If Not oConnection.GetConnectionRME Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_CPPT_NONRACIKAN_TEMPLATE_H
        End Function
        Public Function GetStructureDetail() As R_CPPT_NONRACIKAN_TEMPLATE
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_CPPT_NONRACIKAN_TEMPLATE
        End Function
        Public Function GetStructureDetailList() As List(Of R_CPPT_NONRACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_CPPT_NONRACIKAN_TEMPLATE)
        End Function
        Public Function GetStructureDetailRacikan() As R_CPPT_RACIKAN_TEMPLATE
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailRacikan = Nothing
            End If
            GetStructureDetailRacikan = New R_CPPT_RACIKAN_TEMPLATE
        End Function
        Public Function GetStructureDetailRacikanList() As List(Of R_CPPT_RACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetStructureDetailRacikanList = Nothing
            End If
            GetStructureDetailRacikanList = New List(Of R_CPPT_RACIKAN_TEMPLATE)
        End Function
        'Public Function GetData(ByVal sKDCPPTTEMPLATE As String) As R_CPPT_NONRACIKAN_TEMPLATE
        '    If Not oConnection.GetConnectionRME() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.FirstOrDefault(Function(x) x.KDCPPTTEMPLATE = sKDCPPTTEMPLATE)
        'End Function
        Public Function GetDataHeader(ByVal sKDCPPTTEMPLATE As String) As R_CPPT_NONRACIKAN_TEMPLATE_H
            If Not oConnection.GetConnectionRME() Then
                GetDataHeader = Nothing
                Exit Function
            End If
            GetDataHeader = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDCPPTTEMPLATE = sKDCPPTTEMPLATE)
        End Function
        Public Function GetDataDetail() As List(Of R_CPPT_NONRACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.ToList()
        End Function
        Public Function GetDataDetailRacikan() As List(Of R_CPPT_RACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailRacikan = Nothing
                Exit Function
            End If
            GetDataDetailRacikan = oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDCPPTTEMPLATE As String) As List(Of R_CPPT_NONRACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = sKDCPPTTEMPLATE).ToList()
        End Function
        Public Function GetDataDetailList() As List(Of R_CPPT_NONRACIKAN_TEMPLATE_H)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailList = Nothing
                Exit Function
            End If
            GetDataDetailList = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs().ToList()
        End Function
        Public Function GetDataDetailRacikan1(ByVal sKDCPPTTEMPLATE As String) As List(Of R_CPPT_RACIKAN_TEMPLATE)
            If Not oConnection.GetConnectionRME() Then
                GetDataDetailRacikan1 = Nothing
                Exit Function
            End If
            GetDataDetailRacikan1 = oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = sKDCPPTTEMPLATE).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_CPPT_NONRACIKAN_TEMPLATE_H, ByVal entityDetail As List(Of R_CPPT_NONRACIKAN_TEMPLATE)) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPTTEMPLATE

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

                    entity.KDCPPTTEMPLATE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDCPPTTEMPLATE
            Catch ex As Exception
                InsertData = ""
                oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function InsertDataRacikan(ByVal entity As R_CPPT_NONRACIKAN_TEMPLATE_H, ByVal entityDetail As List(Of R_CPPT_RACIKAN_TEMPLATE)) As String
            Try
                If Not oConnection.GetConnectionRME Then
                    InsertDataRacikan = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPTTEMPLATE

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

                    entity.KDCPPTTEMPLATE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                    For Each iLoop In entityDetail
                        iLoop.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE
                    Next
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertDataRacikan = entity.KDCPPTTEMPLATE
            Catch ex As Exception
                InsertDataRacikan = ""
                oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "INSERTDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As R_CPPT_NONRACIKAN_TEMPLATE_H, ByVal entityDetail As List(Of R_CPPT_NONRACIKAN_TEMPLATE)) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPTTEMPLATE

                Dim ds = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE)
                Dim dsDetail = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE)

                Try
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataRacikan(ByVal entity As R_CPPT_NONRACIKAN_TEMPLATE_H, ByVal entityDetail As List(Of R_CPPT_RACIKAN_TEMPLATE)) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    UpdateDataRacikan = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPTTEMPLATE

                Dim ds = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE)
                Dim dsDetail = oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = entity.KDCPPTTEMPLATE)

                Try
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.InsertAllOnSubmit(entityDetail)
                    End If
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateDataRacikan = True
            Catch ex As Exception
                UpdateDataRacikan = False
                oError.InsertData("R_CPPT_NONRACIKAN_TEMPLATE", "UPDATEDATA", ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDITEM_L3 As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM_L3
                'sSTATUS = "DELETE"
                Dim ds = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.FirstOrDefault(Function(x) x.KDCPPTTEMPLATE = sKDITEM_L3)
                Dim dsDetail = oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = sKDITEM_L3)
                Dim dsDetailRacikan = oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.Where(Function(x) x.KDCPPTTEMPLATE = sKDITEM_L3)

                Try
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATE_Hs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CPPT_NONRACIKAN_TEMPLATEs.DeleteAllOnSubmit(dsDetail)
                    oConnection.dbRME.R_CPPT_RACIKAN_TEMPLATEs.DeleteAllOnSubmit(dsDetailRacikan)
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