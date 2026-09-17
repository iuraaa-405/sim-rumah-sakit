Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_RI_36
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public oCounter As Setting.clsCounter = Nothing
        Public sLASTNUMBER As Integer = 0
        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_RI_36
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_RI_36
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_RI_36_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_RI_36_DETIL
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_RI_36_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_RI_36_DETIL)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_RI_36)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_36s.OrderBy(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_RI_36
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)
        End Function
        Public Function GetDataByKDREG(ByVal sParameter As String, ByVal sParameter2 As String) As S_DIGITAL_RI_36
            If Not oConnection.GetConnection Then
                GetDataByKDREG = Nothing
                Exit Function
            End If
            GetDataByKDREG = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) IIf(sParameter2 <> "", x.KDPENDAFTARAN = sParameter Or x.KDPENDAFTARAN = sParameter2, x.KDPENDAFTARAN = sParameter))
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_RI_36_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_36_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_RI_36_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_RI_36_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter).OrderBy(Function(x) x.SDIGITALRI36D_1).ToList()
        End Function
        Public Function GetDataDetailByNoRM(ByVal Parameter As String) As List(Of S_DIGITAL_RI_36_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetailByNoRM = Nothing
                Exit Function
            End If
            GetDataDetailByNoRM = oConnection.db.S_DIGITAL_RI_36_DETILs.Where(Function(x) x.S_DIGITAL_RI_36.R_IDENTITAS_PASIEN.KDCUSTOMER = Parameter).OrderBy(Function(x) x.DATE).ToList()
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_RI_36, ByVal entityDetail As List(Of S_DIGITAL_RI_36_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_RI_36s.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_RI_36_DETILs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try

                InsertData = True
            Catch ex As Exception
                InsertData = False
                oError.InsertData("S_DIGITAL_RI_36", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_RI_36, ByVal entityDetail As List(Of S_DIGITAL_RI_36_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_36_DETILs.Where(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Try
                    oConnection.db.S_DIGITAL_RI_36s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_36s.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_RI_36_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_RI_36_DETILs.InsertAllOnSubmit(entityDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try

                UpdateData = True
            Catch ex As Exception
                UpdateData = False
                oError.InsertData("S_DIGITAL_RI_36", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                Throw ex
            End Try
        End Function
        Public Function DeleteData(ByVal Parameter As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    DeleteData = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_RI_36_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_RI_36s.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_36_DETILs.DeleteAllOnSubmit(dsDetail)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "DELETEDATA", ex.ToString, Parameter)
                    Throw ex
                End Try

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData("S_DIGITAL_RI_36", "DELETEDATA", ex.ToString, Parameter)
                Throw ex
            End Try
        End Function
        Public Function UpdateCetak(ByVal sKDKUNJUNGAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_RI_36s.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36", "UPDATECETAK", ex.ToString, sKDKUNJUNGAN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_RI_36", "UPDATECETAK", ex.ToString, sKDKUNJUNGAN)
                Throw ex
            End Try
        End Function
        Public Function InsertDataDetail(ByVal entity As S_DIGITAL_RI_36_DETIL) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertDataDetail = False
                    Exit Function
                End If

                Try
                    oConnection.db.S_DIGITAL_RI_36_DETILs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36_DETIL", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36_DETIL", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try

                InsertDataDetail = True
            Catch ex As Exception
                InsertDataDetail = False
                oError.InsertData("S_DIGITAL_RI_36_DETIL", "INSERTDATA", ex.ToString, entity.KDKUNJUNGAN)
                Throw ex
            End Try
        End Function
        Public Function UpdateDataDetail(ByVal entity As S_DIGITAL_RI_36_DETIL) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateDataDetail = False
                    Exit Function
                End If

                Dim ds = oConnection.db.S_DIGITAL_RI_36_DETILs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN And x.SEQ = entity.SEQ)
                Try
                    oConnection.db.S_DIGITAL_RI_36_DETILs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_RI_36_DETILs.InsertOnSubmit(entity)

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36_DETIL", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try
                Try
                    oConnection.db.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_RI_36_DETIL", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                    Throw ex
                End Try

                UpdateDataDetail = True
            Catch ex As Exception
                UpdateDataDetail = False
                oError.InsertData("S_DIGITAL_RI_36_DETIL", "UPDATEDATA", ex.ToString, entity.KDKUNJUNGAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace