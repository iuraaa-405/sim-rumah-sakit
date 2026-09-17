Imports System.Threading

Namespace Template
    Public Class clsCPPT_TemplateProsedur
        Private oData As New Grouper.clsR_Identitas_Grouper_Data
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            oCounter = New Setting.clsCounter
            sMODUL = "CTP"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE
        End Function
        Public Function GetStructureDetail() As R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR
        End Function
        Public Function GetStructureDetailList() As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.OrderByDescending(Function(x) x.KDTEMPLATE).ToList()
        End Function
        Public Function GetData(ByVal sKDTEMPLATE As String) As R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.FirstOrDefault(Function(x) x.KDTEMPLATE = sKDTEMPLATE)
        End Function
        Public Function GetDataDetail() As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.ToList()
        End Function
        Public Function GetDataDetail(ByVal sKDTEMPLATE As String) As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.Where(Function(x) x.KDTEMPLATE = sKDTEMPLATE).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE, ByVal entityDetail As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE
                sSTATUS = "INSERT"

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

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

                        entity.KDTEMPLATE = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)
                        For Each iLoop In entityDetail
                            iLoop.KDTEMPLATE = entity.KDTEMPLATE
                        Next
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try


                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.InsertOnSubmit(entity)
                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityDetail)
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
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
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
        Public Function UpdateData(ByVal entity As R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE, ByVal entityDetail As List(Of R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDUR)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTEMPLATE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.FirstOrDefault(Function(x) x.KDTEMPLATE = entity.KDTEMPLATE)

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.Where(Function(x) x.KDTEMPLATE = entity.KDTEMPLATE)

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATE_PROSEDURs.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDTEMPLATE As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_CPPT_PROSEDUR_TEMPLATEs.FirstOrDefault(Function(x) x.KDTEMPLATE = sKDTEMPLATE)

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEUPDATED = WaktuServer
                    ds.ISDELETE = True

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace