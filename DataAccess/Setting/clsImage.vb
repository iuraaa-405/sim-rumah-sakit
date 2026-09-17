Namespace Setting
    Public Class clsImage
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
            sMODUL = "IMAGE"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As SET_IMAGE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_IMAGE
        End Function
        Public Function GetData() As List(Of SET_IMAGE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_IMAGEs.OrderBy(Function(x) x.KDIMAGE).ToList()
        End Function
        Public Function GetData(ByVal sKDIMAGE As String) As SET_IMAGE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = sKDIMAGE)
        End Function
        Public Function IsExist(ByVal sKDIMAGE As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = sKDIMAGE)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function GetDataImage(ByVal kode As String) As SET_IMAGE
            If Not oConnection.GetConnection() Then
                GetDataImage = Nothing
                Exit Function
            End If
            GetDataImage = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = kode)
        End Function
        Public Function InsertData(ByVal entity As SET_IMAGE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDIMAGE
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_IMAGEs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As SET_IMAGE) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDIMAGE
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = entity.KDIMAGE)

                Try
                    oConnection.db.SET_IMAGEs.DeleteOnSubmit(ds)
                    oConnection.db.SET_IMAGEs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDIMAGE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDIMAGE
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_IMAGEs.FirstOrDefault(Function(x) x.KDIMAGE = sKDIMAGE)

                Try
                    oConnection.db.SET_IMAGEs.DeleteOnSubmit(ds)
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