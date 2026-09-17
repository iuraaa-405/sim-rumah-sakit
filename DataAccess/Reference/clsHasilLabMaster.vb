Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsHasilLabMaster
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

            sMODUL = "HASILLAB"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_HASILLAB_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_HASILLAB_H
        End Function
        Public Function GetStructureDetail() As M_HASILLAB_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New M_HASILLAB_D
        End Function
        Public Function GetStructureDetailList() As List(Of M_HASILLAB_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of M_HASILLAB_D)
        End Function
        Public Function GetData() As List(Of M_HASILLAB_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_HASILLAB_Hs.OrderBy(Function(x) x.JUDUL).ToList()
        End Function
        Public Function GetData(ByVal sKDHASILLAB As String) As M_HASILLAB_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_HASILLAB_Hs.FirstOrDefault(Function(x) x.KDHASILLAB = sKDHASILLAB)
        End Function
        Public Function GetDataByKDITEM(ByVal sKDITEM As String) As M_HASILLAB_H
            If Not oConnection.GetConnection() Then
                GetDataByKDITEM = Nothing
                Exit Function
            End If
            GetDataByKDITEM = oConnection.db.M_HASILLAB_Hs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataDetail(ByVal sKDHASILLAB As String) As List(Of M_HASILLAB_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.M_HASILLAB_Ds.Where(Function(x) x.KDHASILLAB = sKDHASILLAB).ToList()
        End Function
        Public Function InsertData(ByVal entity As M_HASILLAB_H, ByVal entityDetail As List(Of M_HASILLAB_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDHASILLAB
                sSTATUS = "INSERT"

                'Generate Auto Number
                Try
                    sLASTNUMBER = CInt(oConnection.db.M_HASILLAB_Hs.OrderByDescending(Function(x) x.KDHASILLAB).FirstOrDefault().KDHASILLAB.Remove(0, (sMODUL & " _ ").Length)) + 1
                Catch ex As Exception
                    sLASTNUMBER = 1
                End Try
                'End Generate

                Try
                    entity.KDHASILLAB = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_HASILLAB_Hs.InsertOnSubmit(entity)

                    If entityDetail IsNot Nothing Then
                        For Each iLoop In entityDetail
                            iLoop.KDHASILLAB = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                        Next

                        oConnection.db.M_HASILLAB_Ds.InsertAllOnSubmit(entityDetail)
                    End If
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
        Public Function UpdateData(ByVal entity As M_HASILLAB_H, ByVal entityDetail As List(Of M_HASILLAB_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDHASILLAB
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_HASILLAB_Hs.FirstOrDefault(Function(x) x.KDHASILLAB = entity.KDHASILLAB)

                Try
                    oConnection.db.M_HASILLAB_Hs.DeleteOnSubmit(ds)
                    oConnection.db.M_HASILLAB_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.M_HASILLAB_Ds.Where(Function(x) x.KDHASILLAB = entity.KDHASILLAB)

                Try
                    oConnection.db.M_HASILLAB_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.M_HASILLAB_Ds.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDHASILLAB As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDHASILLAB
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_HASILLAB_Hs.FirstOrDefault(Function(x) x.KDHASILLAB = sKDHASILLAB)
                Dim dsDetail = oConnection.db.M_HASILLAB_Ds.Where(Function(x) x.KDHASILLAB = sKDHASILLAB)

                Try
                    oConnection.db.M_HASILLAB_Hs.DeleteOnSubmit(ds)
                    If dsDetail.Count > 0 Then
                        oConnection.db.M_HASILLAB_Ds.DeleteAllOnSubmit(dsDetail)
                    End If
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