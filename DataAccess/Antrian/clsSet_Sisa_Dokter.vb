Namespace SettingAntrian
    Public Class clsSet_Sisa_Dokter
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

            sMODUL = "sisadokter"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_SISA_DOKTER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_SISA_DOKTER
        End Function
        Public Function GetData() As List(Of SET_SISA_DOKTER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_SISA_DOKTERs.ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As SET_SISA_DOKTER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_SISA_DOKTERs.FirstOrDefault(Function(x) x.KODEDOKTER = Parameter)
        End Function
        Public Function GetDataSisa(ByVal sKODEDOKTER As String, ByVal sDATE_TEXT As String) As SET_SISA_DOKTER
            If Not oConnection.GetConnection() Then
                GetDataSisa = Nothing
                Exit Function
            End If
            GetDataSisa = oConnection.db.SET_SISA_DOKTERs.FirstOrDefault(Function(x) x.KODEDOKTER = sKODEDOKTER And x.DATE_TEXT = sDATE_TEXT)
        End Function
        Public Function GetDataKuotaJKN(ByVal sHARI As String) As M_DOCTOR_JADWAL
            If Not oConnection.GetConnection() Then
                GetDataKuotaJKN = Nothing
                Exit Function
            End If
            GetDataKuotaJKN = oConnection.db.M_DOCTOR_JADWALs.FirstOrDefault(Function(x) x.HARI = sHARI And x.LIBUR = False)
        End Function
        Public Function InsertData(ByVal sKODEDOKTER As String, ByVal sDATE_TEXT As String, ByVal sSISA_JKN As Integer, ByVal sSISA_NONJKN As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKODEDOKTER
                sSTATUS = "INSERT"

                Try
                    Dim entity As New SET_SISA_DOKTER
                    With entity
                        .KODEDOKTER = sKODEDOKTER
                        .DATE_TEXT = sDATE_TEXT
                        .SISA_JKN = sSISA_JKN
                        .SISA_NONJKN = sSISA_NONJKN
                    End With

                    oConnection.db.SET_SISA_DOKTERs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal sKODEDOKTER As String, ByVal sDATE_TEXT As String, ByVal sSISA_JKN As Integer, ByVal sSISA_NONJKN As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = sMODUL & " - " & sKODEDOKTER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_SISA_DOKTERs.FirstOrDefault(Function(x) x.KODEDOKTER = sKODEDOKTER And x.DATE_TEXT = sDATE_TEXT)

                With ds
                    .SISA_JKN = sSISA_JKN
                    .SISA_NONJKN = sSISA_NONJKN
                End With

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
    End Class
End Namespace