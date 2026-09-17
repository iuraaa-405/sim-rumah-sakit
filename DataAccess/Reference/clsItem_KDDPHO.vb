Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsItem_KDDPHO
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

            sMODUL = "KDPHO"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_ITEM_KDDPHO
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_ITEM_KDDPHO
        End Function
        Public Function GetData() As List(Of M_ITEM_KDDPHO)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_KDDPHOs.OrderBy(Function(x) x.namaobat).ToList()
        End Function
        Public Function GetData(ByVal sKDITEM As String) As M_ITEM_KDDPHO
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_ITEM_KDDPHOs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
        End Function
        Public Function GetDataKodeDPHO(ByVal sKDOBATDPHO As String) As M_ITEM
            If Not oConnection.GetConnection() Then
                GetDataKodeDPHO = Nothing
                Exit Function
            End If
            GetDataKodeDPHO = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDOBATDPHO = sKDOBATDPHO)
        End Function
        Public Function InsertData(ByVal entity As M_ITEM_KDDPHO) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "INSERT"

                Try
                    oConnection.db.M_ITEM_KDDPHOs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As M_ITEM_KDDPHO) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDITEM
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.M_ITEM_KDDPHOs.FirstOrDefault(Function(x) x.KDITEM = entity.KDITEM)

                Try
                    oConnection.db.M_ITEM_KDDPHOs.DeleteOnSubmit(ds)
                    oConnection.db.M_ITEM_KDDPHOs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDITEM As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.M_ITEM_KDDPHOs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)

                Try
                    oConnection.db.M_ITEM_KDDPHOs.DeleteOnSubmit(ds)
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
        Public Function UpdateItem(ByVal sKDITEM As String, ByVal kodeobat As String, ByVal namaobat As String, ByVal prb As String, ByVal kronis As String, ByVal kemo As String, ByVal harga As String, ByVal restriksi As String, ByVal generik As String, ByVal aktif As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateItem = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "UPDATE"

                'Try
                '    Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                '    If ds IsNot Nothing Then
                '        ds.KDOBATDPHO = kodeobat
                '        oConnection.db.SubmitChanges()
                '    End If
                'Catch ex As Exception
                '    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                '    Throw ex
                'End Try

                UpdateItem = True

                Dim dsItemDPHO = GetData(sKDITEM)

                If dsItemDPHO Is Nothing Then
                    Dim dsDPHO = GetStructureHeader()

                    With dsDPHO
                        .KDITEM = sKDITEM
                        .kodeobat = kodeobat
                        .namaobat = namaobat
                        .prb = prb
                        .kronis = kronis
                        .kemo = kemo
                        .harga = harga
                        .restriksi = restriksi
                        .generik = generik
                        .aktif = "aktif"
                    End With

                    InsertData(dsDPHO)
                Else
                    dsItemDPHO.kodeobat = kodeobat
                    dsItemDPHO.namaobat = namaobat
                    dsItemDPHO.prb = prb
                    dsItemDPHO.kronis = kronis
                    dsItemDPHO.kemo = kemo
                    dsItemDPHO.harga = harga
                    dsItemDPHO.restriksi = restriksi
                    dsItemDPHO.generik = generik
                    dsItemDPHO.aktif = "aktif"

                    oConnection.db.SubmitChanges()
                End If

            Catch ex As Exception
                UpdateItem = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateItemaktifdpho(ByVal sKDITEM As String, ByVal aktif As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateItemaktifdpho = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "UPDATE"

                Try
                    Dim ds = oConnection.db.M_ITEM_KDDPHOs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                    If ds IsNot Nothing Then
                        ds.aktif = aktif
                        oConnection.db.SubmitChanges()
                    End If
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateItemaktifdpho = True

            Catch ex As Exception
                UpdateItemaktifdpho = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateItemaktifdphoMaster(ByVal sKDITEM As String, ByVal sKDOBATDPHO As String) As String
            Try
                If Not oConnection.GetConnection() Then
                    UpdateItemaktifdphoMaster = False
                    Exit Function
                End If

                sREFERENCE = sKDITEM
                sSTATUS = "UPDATE"

                Try
                    Dim ds = oConnection.db.M_ITEMs.FirstOrDefault(Function(x) x.KDITEM = sKDITEM)
                    If ds IsNot Nothing Then
                        ds.KDOBATDPHO = sKDOBATDPHO
                        oConnection.db.SubmitChanges()
                    End If
                Catch ex As Exception
                    'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                UpdateItemaktifdphoMaster = True

            Catch ex As Exception
                UpdateItemaktifdphoMaster = False
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
    End Class
End Namespace