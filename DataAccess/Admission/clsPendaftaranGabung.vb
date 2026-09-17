Imports System.Threading

Namespace Admission
    Public Class clsPendaftaranGabung
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
            sMODUL = "GABUNG"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_PENDAFTARAN_GABUNG
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_PENDAFTARAN_GABUNG
        End Function
        Public Function GetData() As List(Of S_PENDAFTARAN_GABUNG)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_GABUNGs.OrderByDescending(Function(x) x.KDPENDAFTARAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String, ByVal Parameter2 As String) As S_PENDAFTARAN_GABUNG
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_PENDAFTARAN_GABUNGs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.SEQ = Parameter2)
        End Function
        Public Function GetDataList(ByVal Parameter As String) As List(Of S_PENDAFTARAN_GABUNG)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_PENDAFTARAN_GABUNGs.Where(Function(x) x.KDPENDAFTARAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_PENDAFTARAN_GABUNG) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_PENDAFTARAN_GABUNGs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_PENDAFTARAN_GABUNG) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDPENDAFTARAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_PENDAFTARAN_GABUNGs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = entity.KDPENDAFTARAN And x.SEQ = entity.SEQ)

                Try
                    oConnection.db.S_PENDAFTARAN_GABUNGs.DeleteOnSubmit(ds)
                    oConnection.db.S_PENDAFTARAN_GABUNGs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String, ByVal KDPENDAFTARANPERTAMA As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_PENDAFTARAN_GABUNGs.FirstOrDefault(Function(x) x.KDPENDAFTARAN = Parameter And x.SEQ = KDPENDAFTARANPERTAMA)

                If ds IsNot Nothing Then
                    Try
                        oConnection.db.S_PENDAFTARAN_GABUNGs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try

                End If

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