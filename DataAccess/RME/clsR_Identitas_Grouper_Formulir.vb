Imports DataAccess

Namespace Grouper
    Public Class clsR_Identitas_Grouper_Formulir
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing
        Private oData As New Grouper.clsR_Identitas_Grouper_Data

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "FORMULIR"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As R_IDENTITAS_GROUPER_FORMULIR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New R_IDENTITAS_GROUPER_FORMULIR
        End Function
        Public Function GetData() As List(Of R_IDENTITAS_GROUPER_FORMULIR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_FORMULIRs.OrderByDescending(Function(x) x.KODE).ToList()
        End Function
        Public Function GetData(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER_FORMULIR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.R_IDENTITAS_GROUPER_FORMULIRs.FirstOrDefault(Function(x) x.KODE = Parameter)
        End Function
        Public Function GetDatabyKodeFormulir(ByVal Parameter As String) As R_IDENTITAS_GROUPER_FORMULIR
            If Not oConnection.GetConnection() Then
                GetDatabyKodeFormulir = Nothing
                Exit Function
            End If
            GetDatabyKodeFormulir = oConnection.db.R_IDENTITAS_GROUPER_FORMULIRs.FirstOrDefault(Function(x) x.KDFORMULIR = Parameter)
        End Function
        Public Function GetDatabyKodeGrouper(ByVal Parameter As Integer) As R_IDENTITAS_GROUPER_FORMULIR
            If Not oConnection.GetConnection() Then
                GetDatabyKodeGrouper = Nothing
                Exit Function
            End If
            GetDatabyKodeGrouper = oConnection.db.R_IDENTITAS_GROUPER_FORMULIRs.FirstOrDefault(Function(x) x.kodegrouper = Parameter)
        End Function
        Public Function InsertData(ByVal entity As R_IDENTITAS_GROUPER_FORMULIR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KODE
                sSTATUS = "INSERT"

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.db.R_IDENTITAS_GROUPER_FORMULIRs.InsertOnSubmit(entity)
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