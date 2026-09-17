Namespace Setting
    Public Class clsSatuSehatKoneksiToken
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
        Public Function GetStructureHeader() As SET_SATUSEHAT_KONEKSI_TOKEN
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New SET_SATUSEHAT_KONEKSI_TOKEN
        End Function
        Public Function GetData() As List(Of SET_SATUSEHAT_KONEKSI_TOKEN)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.OrderBy(Function(x) x.KDKONEKSI).ToList()
        End Function
        Public Function GetData(ByVal sKDKONEKSI As String, ByVal sSEQ As Integer) As SET_SATUSEHAT_KONEKSI_TOKEN
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI And x.SEQ = sSEQ)
        End Function
        Public Function GetDataSEQ(ByVal sKDKONEKSI As String) As SET_SATUSEHAT_KONEKSI_TOKEN
            If Not oConnection.GetConnection() Then
                GetDataSEQ = Nothing
                Exit Function
            End If
            GetDataSEQ = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.Where(Function(x) x.KDKONEKSI = sKDKONEKSI).OrderByDescending(Function(x) x.SEQ).FirstOrDefault()
        End Function
        Public Function IsExist(ByVal sKDKONEKSI As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As SET_SATUSEHAT_KONEKSI_TOKEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONEKSI
                sSTATUS = "INSERT"

                Try
                    oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As SET_SATUSEHAT_KONEKSI_TOKEN) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKONEKSI
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.FirstOrDefault(Function(x) x.KDKONEKSI = entity.KDKONEKSI And x.SEQ = entity.SEQ)

                Try
                    oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.DeleteOnSubmit(ds)
                    oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.InsertOnSubmit(entity)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal sKDKONEKSI As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDKONEKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.FirstOrDefault(Function(x) x.KDKONEKSI = sKDKONEKSI And x.SEQ = sSEQ)

                Try
                    oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.DeleteOnSubmit(ds)
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateToken(ByVal kdkoneksi As String, ByVal seq As Integer, ByVal memo As String, ByVal generat As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateToken = False
                    Exit Function
                End If

                UpdateToken = True

                Dim ds = oConnection.db.SET_SATUSEHAT_KONEKSI_TOKENs.FirstOrDefault(Function(x) x.KDKONEKSI = kdkoneksi And x.SEQ = seq)

                If ds IsNot Nothing Then
                    ds.DATEUPDATED = Now
                    ds.MEMO = memo
                    ds.GENERATETOKEN = generat

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateToken = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace