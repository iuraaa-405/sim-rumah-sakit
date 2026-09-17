Imports DataAccess.My.Resources

Namespace EMedrek
    Public Class clsTriagePonek
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

            sMODUL = "TRIAGEPONEK"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_TRIAGEPONEK
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_TRIAGEPONEK
        End Function
        Public Function GetData() As List(Of S_DIGITAL_TRIAGEPONEK)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.OrderBy(Function(x) x.KDTRIAGEPONEK).ToList()
        End Function
        Public Function GetData(ByVal sKDTRIAGEPONEK As String) As S_DIGITAL_TRIAGEPONEK
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.FirstOrDefault(Function(x) x.KDTRIAGEPONEK = sKDTRIAGEPONEK)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_TRIAGEPONEK) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTRIAGEPONEK
                sSTATUS = "INSERT"

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_TRIAGEPONEK) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDTRIAGEPONEK
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.FirstOrDefault(Function(x) x.KDTRIAGEPONEK = entity.KDTRIAGEPONEK)

                Try
                    oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDTRIAGEPONEK As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDTRIAGEPONEK
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.FirstOrDefault(Function(x) x.KDTRIAGEPONEK = sKDTRIAGEPONEK)

                Try
                    oConnection.dbRME.S_DIGITAL_TRIAGEPONEKs.DeleteOnSubmit(ds)
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