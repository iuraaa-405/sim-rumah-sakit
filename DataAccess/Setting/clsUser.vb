Namespace Setting
    Public Class clsUser
        Public oConnection As Setting.clsConnectionUser = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""

        Public Sub New(Optional ByVal sConnection As String = "")
            oConnection = New Setting.clsConnectionUser
            If sConnection = "" Then
                oError = New Setting.clsError
            Else
                oError = New Setting.clsError("TAX")
            End If
            sMODUL = "USER"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_USER
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_USER
        End Function
        Public Function GetData() As List(Of SET_USER)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_USERs.OrderBy(Function(x) x.KDUSER).ToList()
        End Function
        Public Function GetData(ByVal sKDUSER As String) As SET_USER
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)
        End Function
        Public Function IsExist(ByVal sKDUSER As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function GetDataImageKode(ByVal kode As String) As SET_IMAGE
            If Not oConnection.GetConnection() Then
                GetDataImageKode = Nothing
                Exit Function
            End If
            GetDataImageKode = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = kode)
        End Function
        Public Function GetDataKoneksiBPJS(ByVal sKDKONEKSI As String) As SET_BPJS_KONEKSI
            If Not oConnection.GetConnection() Then
                GetDataKoneksiBPJS = Nothing
                Exit Function
            End If
            GetDataKoneksiBPJS = oConnection.db.SET_BPJS_KONEKSIs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI And x.ISACTIVE = True)
        End Function
        Public Function InsertData(ByVal entity As SET_USER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUSER
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_USERs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As SET_USER) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDUSER
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = entity.KDUSER)

                Try
                    oConnection.db.SET_USERs.DeleteOnSubmit(ds)
                    oConnection.db.SET_USERs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDUSER As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDUSER
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_USERs.FirstOrDefault(Function(x) x.KDUSER = sKDUSER)

                Try
                    oConnection.db.SET_USERs.DeleteOnSubmit(ds)
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