
Namespace Digital
    Public Class clsS_DIGITAL_OK_SIGNATURE
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
            sMODUL = "IC"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_OK_02_SIGNATURE
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_OK_02_SIGNATURE
        End Function
        Public Function GetData() As List(Of S_DIGITAL_OK_02_SIGNATURE)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.OrderBy(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String, ByVal sSEQ As Integer) As S_DIGITAL_OK_02_SIGNATURE
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter And x.SEQ = sSEQ)
        End Function
        Public Function GetDataSignatuerPersetujuan(ByVal sParameter As String, ByVal sSEQ As Integer) As String
            If Not oConnection.GetConnection() Then
                GetDataSignatuerPersetujuan = ""
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter And x.SEQ = sSEQ)
            If ds IsNot Nothing Then
                GetDataSignatuerPersetujuan = "http://rsdustira.com/VerifikasiPersetujuan/" & sSEQ & "/" & ds.KDKUNJUNGAN
            Else
                GetDataSignatuerPersetujuan = ""
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_OK_02_SIGNATURE) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_OK_02_SIGNATURE) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_OK_02_SIGNATUREs.DeleteOnSubmit(ds)
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