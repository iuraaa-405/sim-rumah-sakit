Namespace Grouper
    Public Class clsStatusGrouperHasil
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
            sMODUL = "STATUSGROUPERHASIL"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG
        End Function
        Public Function GetStructureDetail() As R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP
        End Function
        Public Function GetStructureDetailList() As List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP)
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.OrderByDescending(Function(x) x.kodegrouper).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUPs.Where(Function(x) x.kodegrouper = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG, ByVal entityDetail As List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "INSERT"

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.InsertOnSubmit(entity)
                    If entityDetail.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUPs.InsertAllOnSubmit(entityDetail)
                    End If
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
        Public Function UpdateData(ByVal entity As R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG, ByVal entityDetail As List(Of R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUP)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.kodegrouper
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.FirstOrDefault(Function(x) x.kodegrouper = entity.kodegrouper)
                Dim dsDetail = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUPs.Where(Function(x) x.kodegrouper = entity.kodegrouper)

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.DeleteOnSubmit(ds)
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.InsertOnSubmit(entity)

                    If dsDetail.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUPs.DeleteAllOnSubmit(dsDetail)
                    End If
                    If entityDetail.Count > 0 Then
                        oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRG_TOPUPs.InsertAllOnSubmit(entityDetail)
                    End If
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
        Public Function DeleteData(ByVal Parameter As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)

                Try
                    oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.DeleteOnSubmit(ds)
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
        Public Function UpdateDataStage2(ByVal skodegrouper As String, ByVal code As String, ByVal tarif As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataStage2 = False
                    Exit Function
                End If

                UpdateDataStage2 = True

                Dim ds = oConnection.db.R_IDENTITAS_GROUPER_DATA_HASILGROUPINGIDRGs.FirstOrDefault(Function(x) x.kodegrouper = skodegrouper)

                If ds IsNot Nothing Then
                    ds.TopUpCostWeight_codeA = code
                    ds.TopUpCostWeight = tarif
                    oConnection.db.SubmitChanges()
                End If
            Catch ex As Exception
                UpdateDataStage2 = False
                Throw ex
            End Try
        End Function

    End Class
End Namespace