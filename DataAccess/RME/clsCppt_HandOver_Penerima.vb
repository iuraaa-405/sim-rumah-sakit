Imports DataAccess.My.Resources

Namespace EMedrek
    Public Class clsCppt_HandOver_Penerima
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
            End If

            sMODUL = "CPPTHANDOVERPENERIMA"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_CPPT_HANDOVER_PENERIMA
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_CPPT_HANDOVER_PENERIMA
        End Function
        Public Function GetData() As List(Of R_CPPT_HANDOVER_PENERIMA)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDCPPT As String) As R_CPPT_HANDOVER_PENERIMA
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.FirstOrDefault(Function(x) x.KDCPPT = sKDCPPT)
        End Function
        Public Function GetDataSync() As List(Of R_CPPT_HANDOVER_PENERIMA)
            If Not oConnection.GetConnectionRME() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnectionRME() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As R_CPPT_HANDOVER_PENERIMA) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "INSERT"

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As R_CPPT_HANDOVER_PENERIMA) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCPPT
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.FirstOrDefault(Function(x) x.KDCPPT = entity.KDCPPT)

                Try
                    oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.DeleteOnSubmit(ds)
                    oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDCPPT As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCPPT
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.FirstOrDefault(Function(x) x.KDCPPT = sKDCPPT)

                Try
                    oConnection.dbRME.R_CPPT_HANDOVER_PENERIMAs.DeleteOnSubmit(ds)
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
    End Class
End Namespace