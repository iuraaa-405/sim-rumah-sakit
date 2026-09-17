Imports DataAccess.My.Resources

Namespace StaffAbsensi
    Public Class clsStaffAbsensi
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

            sMODUL = "STAFFABSENSI"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As H_ABSEN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New H_ABSEN
        End Function
        Public Function GetData() As List(Of H_ABSEN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.H_ABSENs.OrderBy(Function(x) x.KDABSEN).ToList()
        End Function
        Public Function GetData(ByVal sKDABSEN As Integer) As H_ABSEN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.H_ABSENs.FirstOrDefault(Function(x) x.KDABSEN = sKDABSEN)
        End Function
        Public Function GetDataByBelumSelesai(ByVal sKDSTAFF As Integer, ByVal sCATEGORY As Integer) As H_ABSEN
            If Not oConnection.GetConnection() Then
                GetDataByBelumSelesai = Nothing
                Exit Function
            End If
            GetDataByBelumSelesai = oConnection.db.H_ABSENs.FirstOrDefault(Function(x) x.KDSTAFF = sKDSTAFF And x.CATEGORY = sCATEGORY And x.DESCRIPTION = "")
        End Function
        Public Function GetDataSync() As List(Of H_ABSEN)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.H_ABSENs.OrderBy(Function(x) x.KDABSEN).ToList()
        End Function
        Public Function IsExist(ByVal sKDABSEN As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.H_ABSENs.FirstOrDefault(Function(x) x.KDABSEN = sKDABSEN)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As H_ABSEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSTAFF
                sSTATUS = "INSERT"

                ''Generate Auto Number
                'Try
                '    sLASTNUMBER = CInt(oConnection.db.H_ABSENs.OrderByDescending(Function(x) x.KDSTAFF).FirstOrDefault().KDSTAFF.Remove(0, (sMODUL & " _ ").Length)) + 1
                'Catch ex As Exception
                '    sLASTNUMBER = 1
                'End Try
                ''End Generate

                Try
                    oConnection.db.H_ABSENs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDABSENSI As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDABSENSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.H_ABSENs.FirstOrDefault(Function(x) x.KDABSEN = sKDABSENSI)

                Try
                    oConnection.db.H_ABSENs.DeleteOnSubmit(ds)
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
        Public Function UpdatePulang(ByVal sKDABSEN As Integer) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdatePulang = False
                    Exit Function
                End If

                UpdatePulang = True

                Dim ds = oConnection.db.H_ABSENs.FirstOrDefault(Function(x) x.KDABSEN = sKDABSEN)

                ds.JAMKELUAR = Now
                ds.DESCRIPTION = "SELESAI"

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdatePulang = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace