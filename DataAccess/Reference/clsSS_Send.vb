Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsSS_Send
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

            sMODUL = "SS_SEND"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SS_SEND
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SS_SEND
        End Function
        Public Function GetData(ByVal sKODESEND As String) As SS_SEND
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SS_SENDs.FirstOrDefault(Function(x) x.KODESEND = sKODESEND)
        End Function
        Public Function GetDataByRegisterCategory(ByVal sKDPENDAFTARAN As String, ByVal sCATEGORY As String) As SS_SEND
            If Not oConnection.GetConnection() Then
                GetDataByRegisterCategory = Nothing
                Exit Function
            End If
            GetDataByRegisterCategory = oConnection.db.SS_SENDs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = sKDPENDAFTARAN And x.CATEGORY = sCATEGORY)
        End Function
        Public Function InsertData(ByVal entity As SS_SEND) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODESEND
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SS_SENDs.InsertOnSubmit(entity)
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
    End Class
End Namespace