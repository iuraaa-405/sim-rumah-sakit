Imports System.Data.SqlClient
Imports DataAccess.My.Resources

Namespace Digital
    Public Class clsS_DIGITAL_ASMEDOBGYN
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing
        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError

            sMODUL = "S_DIGITAL_ASMEDOBGYN"
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_ASMEDOBGYN
            If Not oConnection.GetConnection Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_ASMEDOBGYN
        End Function
        Public Function GetStructureDetail() As S_DIGITAL_ASMEDOBGYN_DETIL
            If Not oConnection.GetConnection Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_DIGITAL_ASMEDOBGYN_DETIL
        End Function
        Public Function GetStructureDetailList() As List(Of S_DIGITAL_ASMEDOBGYN_DETIL)
            If Not oConnection.GetConnection Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_DIGITAL_ASMEDOBGYN_DETIL)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_ASMEDOBGYN)
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ASMEDOBGYNs.OrderBy(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal sParameter As String) As S_DIGITAL_ASMEDOBGYN
            If Not oConnection.GetConnection Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_ASMEDOBGYNs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)
        End Function
        Public Function GetDataDetail() As List(Of S_DIGITAL_ASMEDOBGYN_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of S_DIGITAL_ASMEDOBGYN_DETIL)
            If Not oConnection.GetConnection Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function IsExist(ByVal sParameter As String) As Boolean
            If Not oConnection.GetConnection Then
                IsExist = False
                Exit Function
            End If

            Dim ds = oConnection.db.S_DIGITAL_ASMEDOBGYNs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sParameter)

            If ds IsNot Nothing Then
                IsExist = True
            Else
                IsExist = False
            End If
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_ASMEDOBGYN, ByVal entityDetail As List(Of S_DIGITAL_ASMEDOBGYN_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Try
                    oConnection.db.S_DIGITAL_ASMEDOBGYNs.InsertOnSubmit(entity)
                    oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_ASMEDOBGYN, ByVal entityDetail As List(Of S_DIGITAL_ASMEDOBGYN_DETIL)) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_ASMEDOBGYNs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Dim dsDetail = oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.Where(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)
                Try
                    oConnection.db.S_DIGITAL_ASMEDOBGYNs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_ASMEDOBGYNs.InsertOnSubmit(entity)

                    oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.InsertAllOnSubmit(entityDetail)
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


                Dim ds = oConnection.db.S_DIGITAL_ASMEDOBGYNs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
                Dim dsDetail = oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.Where(Function(x) x.KDKUNJUNGAN = Parameter)

                Try
                    oConnection.db.S_DIGITAL_ASMEDOBGYNs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_ASMEDOBGYN_DETILs.DeleteAllOnSubmit(dsDetail)
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
        Public Function UpdateCetak(ByVal sKDKUNJUNGAN As String) As Boolean
            Try
                If Not oConnection.GetConnection Then
                    UpdateCetak = False
                    Exit Function
                End If

                Try
                    Dim ds = oConnection.db.S_DIGITAL_ASMEDOBGYNs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)

                    ds.CETAK += 1

                    oConnection.db.SubmitChanges()

                Catch ex As Exception
                    oError.InsertData("S_DIGITAL_ASMEDOBGYN", "UPDATECETAK", ex.ToString, sKDKUNJUNGAN)
                    Throw ex
                End Try

                UpdateCetak = True
            Catch ex As Exception
                UpdateCetak = False
                oError.InsertData("S_DIGITAL_ASMEDOBGYN", "UPDATECETAK", ex.ToString, sKDKUNJUNGAN)
                Throw ex
            End Try
        End Function
    End Class
End Namespace