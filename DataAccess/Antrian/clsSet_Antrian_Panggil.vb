Namespace SettingAntrian
    Public Class clsSet_Antrian_Panggil
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

            sMODUL = "ANTRIANSIMPAN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_ANTRIAN_PANGGIL
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_ANTRIAN_PANGGIL
        End Function
        Public Function GetData() As List(Of SET_ANTRIAN_PANGGIL)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_ANTRIAN_PANGGILs.OrderBy(Function(x) x.LOKET).ToList()
        End Function
        Public Function GetDataByIschekd() As List(Of SET_ANTRIAN_PANGGIL)
            If Not oConnection.GetConnection() Then
                GetDataByIschekd = Nothing
                Exit Function
            End If
            GetDataByIschekd = oConnection.db.SET_ANTRIAN_PANGGILs.OrderBy(Function(x) x.LOKET).ToList()
        End Function
        Public Function GetData(ByVal sLOKET As String) As SET_ANTRIAN_PANGGIL
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_ANTRIAN_PANGGILs.FirstOrDefault(Function(x) x.LOKET = sLOKET)
        End Function
        Public Function InsertData(ByVal entity As SET_ANTRIAN_PANGGIL) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_ANTRIAN_PANGGILs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As SET_ANTRIAN_PANGGIL) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.LOKET
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_ANTRIAN_PANGGILs.FirstOrDefault(Function(x) x.LOKET = entity.LOKET)

                Try
                    oConnection.db.SET_ANTRIAN_PANGGILs.DeleteOnSubmit(ds)
                    oConnection.db.SET_ANTRIAN_PANGGILs.InsertOnSubmit(entity)
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
        Public Function UpdateDataIsCheked(ByVal sLOKET As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataIsCheked = False
                    Exit Function
                End If

                UpdateDataIsCheked = True

                Dim ds = oConnection.db.SET_ANTRIAN_PANGGILs.FirstOrDefault(Function(x) x.LOKET = sLOKET)

                ds.ISPANGGIL = True

                oConnection.db.SubmitChanges()
            Catch ex As Exception
                UpdateDataIsCheked = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace