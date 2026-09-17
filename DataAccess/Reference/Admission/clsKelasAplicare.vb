Imports DataAccess.My.Resources

Namespace Reference
    Public Class clsKelasAplicare
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

            sMODUL = "KELASAPLICARE"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As M_KELASAPLICARE
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New M_KELASAPLICARE
        End Function
        Public Function GetStructureDetail_UOM() As M_KELASAPLICARE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOM = Nothing
            End If
            GetStructureDetail_UOM = New M_KELASAPLICARE_DEPARTMENT
        End Function
        Public Function GetStructureDetail_UOMList() As List(Of M_KELASAPLICARE_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetStructureDetail_UOMList = Nothing
            End If
            GetStructureDetail_UOMList = New List(Of M_KELASAPLICARE_DEPARTMENT)
        End Function
        Public Function GetData() As List(Of M_KELASAPLICARE)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_KELASAPLICAREs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function GetData(ByVal sKDKELASAPLICARE As String) As M_KELASAPLICARE
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.M_KELASAPLICAREs.FirstOrDefault(Function(x) x.KDKELASAPLICARE = sKDKELASAPLICARE)
        End Function
        Public Function GetDataByDepartment(ByVal sKDDEPARTMENT As String) As M_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataByDepartment = Nothing
                Exit Function
            End If
            GetDataByDepartment = oConnection.db.M_DEPARTMENTs.FirstOrDefault(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT)
        End Function
        Public Function GetDataDetail_UOM(ByVal sKDKELASAPLICARE As String) As M_KELASAPLICARE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDKELASAPLICARE)
        End Function
        Public Function GetDataDetail_KelasDepartment(ByVal sKDKELASAPLICARE As String, ByVal sKDDEPARMENT As String) As M_KELASAPLICARE_DEPARTMENT
            If Not oConnection.GetConnection() Then
                GetDataDetail_KelasDepartment = Nothing
                Exit Function
            End If
            GetDataDetail_KelasDepartment = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDKELASAPLICARE = sKDKELASAPLICARE And x.KDDEPARTMENT = sKDDEPARMENT)
        End Function
        Public Function GetDataDetail_UOM() As List(Of M_KELASAPLICARE_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetDataDetail_UOM = Nothing
                Exit Function
            End If
            GetDataDetail_UOM = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.ToList()
        End Function
        Public Function GetDataDetail_Department(ByVal sKDDEPARTMENT As String) As List(Of M_KELASAPLICARE_DEPARTMENT)
            If Not oConnection.GetConnection() Then
                GetDataDetail_Department = Nothing
                Exit Function
            End If
            GetDataDetail_Department = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.Where(Function(x) x.KDDEPARTMENT = sKDDEPARTMENT).ToList()
        End Function
        Public Function GetDataSync() As List(Of M_KELASAPLICARE)
            If Not oConnection.GetConnection() Then
                GetDataSync = Nothing
                Exit Function
            End If
            GetDataSync = oConnection.db.M_KELASAPLICAREs.OrderBy(Function(x) x.MEMO).ToList()
        End Function
        Public Function IsExist(ByVal sMEMO As String) As Boolean
            If Not oConnection.GetConnection() Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.M_KELASAPLICAREs.FirstOrDefault(Function(x) x.MEMO = sMEMO)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As M_KELASAPLICARE, ByVal entityDetail_UOM As List(Of M_KELASAPLICARE_DEPARTMENT)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKELASAPLICARE
                sSTATUS = "INSERT"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    InsertData = False
                    Exit Function
                End If

                ''Generate Auto Number
                'Try
                '    sLASTNUMBER = CInt(oConnection.db.M_KELASAPLICAREs.OrderByDescending(Function(x) x.KDKELASAPLICARE).FirstOrDefault().KDKELASAPLICARE.Remove(0, (sMODUL & " _ ").Length)) + 1
                'Catch ex As Exception
                '    sLASTNUMBER = 1
                'End Try
                ''End Generate

                Try
                    'entity.KDKELASAPLICARE = sMODUL & "_" & AutoNumberCode(sLASTNUMBER)
                    oConnection.db.M_KELASAPLICAREs.InsertOnSubmit(entity)
                    oConnection.db.M_KELASAPLICARE_DEPARTMENTs.InsertAllOnSubmit(entityDetail_UOM)
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
        Public Function UpdateData(ByVal entity As M_KELASAPLICARE, ByVal entityDetail_UOM As List(Of M_KELASAPLICARE_DEPARTMENT)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKELASAPLICARE
                sSTATUS = "UPDATE"

                If CheckDefault(0, entity.ISDEFAULT) = False Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELASAPLICAREs.FirstOrDefault(Function(x) x.KDKELASAPLICARE = entity.KDKELASAPLICARE)
                Dim dsDetail_UOM = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.Where(Function(x) x.KDKELASAPLICARE = entity.KDKELASAPLICARE)

                Try
                    oConnection.db.M_KELASAPLICAREs.DeleteOnSubmit(ds)
                    oConnection.db.M_KELASAPLICAREs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.M_KELASAPLICARE_DEPARTMENTs.DeleteAllOnSubmit(dsDetail_UOM)
                    oConnection.db.M_KELASAPLICARE_DEPARTMENTs.InsertAllOnSubmit(entityDetail_UOM)
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
        Public Function DeleteData(ByVal sKDKELASAPLICARE As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDKELASAPLICARE
                sSTATUS = "DELETE"

                If CheckDefault(1, 0) = False Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.M_KELASAPLICAREs.FirstOrDefault(Function(x) x.KDKELASAPLICARE = sKDKELASAPLICARE)
                Dim dsDetail_UOM = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.Where(Function(x) x.KDKELASAPLICARE = sKDKELASAPLICARE)

                Try
                    oConnection.db.M_KELASAPLICAREs.DeleteOnSubmit(ds)
                    oConnection.db.M_KELASAPLICARE_DEPARTMENTs.DeleteAllOnSubmit(dsDetail_UOM)
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
        Public Function CheckDefault(ByVal sState As Integer, ByVal sDefault As Boolean) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    CheckDefault = False
                    Exit Function
                End If


                If sState = 0 Then
                    Dim ds = oConnection.db.M_KELASAPLICAREs.Where(Function(x) x.ISDEFAULT = True)
                    If sDefault = True Then
                        For Each iLoop In ds
                            iLoop.ISDEFAULT = False
                        Next
                    Else
                        If ds.Count < 1 Then
                            MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                            CheckDefault = False
                            Exit Function
                        End If
                    End If
                Else
                    Dim ds = oConnection.db.M_KELASAPLICAREs.Where(Function(x) x.ISDEFAULT = True)

                    If ds.Count < 1 Then
                        MsgBox(Statement.CheckDefault, MsgBoxStyle.Exclamation)
                        CheckDefault = False
                        Exit Function
                    End If
                End If
                CheckDefault = True
            Catch ex As Exception
                CheckDefault = False
                Throw ex
            End Try
        End Function
        Public Function UpdateIsAplicareTersedia(ByVal sKDKELASRAWAT As String, ByVal sTERSEDIA_LAKI As Decimal, ByVal sTERSEDIA_PEREMPUAN As Decimal, ByVal TERSEDIA_LAKIPEREMPUAN As Decimal) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateIsAplicareTersedia = False
                    Exit Function
                End If

                UpdateIsAplicareTersedia = True

                Dim ds = oConnection.db.M_KELASAPLICARE_DEPARTMENTs.FirstOrDefault(Function(x) x.KDUPDATE_APLICARE = sKDKELASRAWAT)

                ds.TERSEDIA_LAKI = sTERSEDIA_LAKI
                ds.TERSEDIA_PEREMPUAN = sTERSEDIA_PEREMPUAN
                ds.TERSEDIA_LAKIPEREMPUAN = TERSEDIA_LAKIPEREMPUAN

                oConnection.db.SubmitChanges()

            Catch ex As Exception
                UpdateIsAplicareTersedia = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace