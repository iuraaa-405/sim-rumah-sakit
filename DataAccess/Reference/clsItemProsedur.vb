Namespace Reference
    Public Class clsItemProsedur
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

            sMODUL = "ITEM"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEM_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEM_PROSEDUR
        End Function
        Public Function GetData() As List(Of M_ITEM_PROSEDUR)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_PROSEDURs.OrderBy(Function(x) x.KDITEM).ToList()
        End Function
        Public Function GetData(ByVal sKDITEM As String) As M_ITEM_PROSEDUR
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_PROSEDURs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        'Public Function GetDataSnowmed(ByVal sKDITEM As String) As M_ITEM_PROSEDUR
        '    If Not oConnection.GetConnection() Then
        '        GetDataSnowmed = Nothing
        '        Exit Function
        '    End If
        '    GetDataSnowmed = oConnection.db.M_ITEM_PROSEDURs.FirstOrDefault(Function(x) x.KDSNOMED_CT = sKDITEM)
        'End Function
        Public Function InsertData(ByVal entity As M_ITEM_PROSEDUR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_ITEM_PROSEDURs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_ITEM_PROSEDUR) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEM_PROSEDURs.FirstOrDefault(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_PROSEDURs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEM_PROSEDURs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ITEM_PROSEDURs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)

                Try
                    oConnection.db.M_ITEM_PROSEDURs.DeleteOnSubmit(ds)
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