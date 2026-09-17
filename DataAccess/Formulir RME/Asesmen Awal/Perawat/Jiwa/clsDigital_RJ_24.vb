Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsDigital_RJ_24
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing

        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing

        Public Sub New(Optional ByVal sConnection As String = "")
            If sConnection = "" Then
                oConnection = New Setting.clsConnectionMain
                oError = New Setting.clsError
                oCounter = New Setting.clsCounter
            Else
                oConnection = New Setting.clsConnectionMain("TAX")
                oError = New Setting.clsError("TAX")
                oCounter = New Setting.clsCounter
            End If

            sMODUL = "S_DIGITAL_RJ_24"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RJ_24
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RJ_24
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_RJ_24_DETIL
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_RJ_24_DETIL
        End Function

        Public Function GetStructureDetailList() As List(Of S_DIGITAL_RJ_24_DETIL)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_RJ_24_DETIL)
        End Function

        Public Function GetData() As List(Of S_DIGITAL_RJ_24)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RJ_24s.OrderBy(Function(x) x.DATECREATED).ToList()
        End Function

        Public Function GetData(ByVal sKDKUNJUNGAN As String) As S_DIGITAL_RJ_24
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RJ_24s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function GetDatabyKDREG(ByVal sParameter As String) As List(Of S_DIGITAL_RJ_24)
            If Not oConnection.GetConnection Then
                GetDatabyKDREG = Nothing
                Exit Function
            End If
            GetDatabyKDREG = oConnection.db.S_DIGITAL_RJ_24s.Where(Function(x) x.R_IDENTITAS_PASIEN.KDPENDAFTARAN = sParameter).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function

        Public Function GetDataDetail() As List(Of S_DIGITAL_RJ_24_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RJ_24_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_RJ_24_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RJ_24_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter).ToList()
        End Function

        Public Function InsertData(ByVal entity As S_DIGITAL_RJ_24, ByVal entityDetail As List(Of S_DIGITAL_RJ_24_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                If entityDetail IsNot Nothing Then
                    For Each iLoop In entityDetail
                        iLoop.KDKUNJUNGAN = entity.KDKUNJUNGAN
                    Next
                End If

                Try
                    oConnection.db.S_DIGITAL_RJ_24s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RJ_24_DETILs.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal sKDKUNJUNGAN As String, ByVal entity As S_DIGITAL_RJ_24, ByVal entityDetail As List(Of S_DIGITAL_RJ_24_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_RJ_24s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_RJ_24_DETILs.Where(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)

                Try
                    oConnection.db.S_DIGITAL_RJ_24s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RJ_24s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_RJ_24_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RJ_24_DETILs.InsertAllOnSubmit(entityDetail)
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

        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_RJ_24s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_RJ_24_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RJ_24s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RJ_24_DETILs.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, Parameter)
                Throw ex
            End Try
        End Function

        Public Function GetDataDetailDiagnosa(ByVal sKDKUNJUNGAN As String, ByVal sKDITEMDIAGNOSAPERAWAT As String) As S_DIGITAL_RJ_24_DETIL
            If Not oConnection.GetConnection() Then
                GetDataDetailDiagnosa = Nothing
                Exit Function
            End If
            GetDataDetailDiagnosa = oConnection.db.S_DIGITAL_RJ_24_DETILs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN And x.KDITEMDIAGNOSAPERAWAT = sKDITEMDIAGNOSAPERAWAT)
        End Function

        Public Function GetDataItemDetil(ByVal Parameter As String) As List(Of M_ITEM_DIAGNOSA_PERAWAT_D)
            If Not oConnection.GetConnection() Then
                GetDataItemDetil = Nothing
                Exit Function
            End If
            GetDataItemDetil = oConnection.db.M_ITEM_DIAGNOSA_PERAWAT_Ds.Where(Function(x) x.KDITEMDIAGNOSAPERAWAT = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
    End Class
End Namespace