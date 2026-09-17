Imports System.Threading

Namespace Flowchart
    Public Class clsCurveList
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
            sMODUL = "CURVELIST"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_CURVELIST_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_CURVELIST_H
        End Function
        Public Function GetStructureDetail_1() As S_DIGITAL_CURVELIST_01
            If Not oConnection.GetConnection() Then
                GetStructureDetail_1 = Nothing
            End If
            GetStructureDetail_1 = New S_DIGITAL_CURVELIST_01
        End Function
        Public Function GetStructureDetailList_1() As List(Of S_DIGITAL_CURVELIST_01)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_1 = Nothing
            End If
            GetStructureDetailList_1 = New List(Of S_DIGITAL_CURVELIST_01)
        End Function
        Public Function GetStructureDetail_2() As S_DIGITAL_CURVELIST_02
            If Not oConnection.GetConnection() Then
                GetStructureDetail_2 = Nothing
            End If
            GetStructureDetail_2 = New S_DIGITAL_CURVELIST_02
        End Function
        Public Function GetStructureDetailList_2() As List(Of S_DIGITAL_CURVELIST_02)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList_2 = Nothing
            End If
            GetStructureDetailList_2 = New List(Of S_DIGITAL_CURVELIST_02)
        End Function
        Public Function GetData() As List(Of S_DIGITAL_CURVELIST_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_CURVELIST_Hs.OrderByDescending(Function(x) x.KDCURVELIST).ToList()
        End Function
        Public Function GetData(ByVal sKDCURVELIST As String) As S_DIGITAL_CURVELIST_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_DIGITAL_CURVELIST_Hs.FirstOrDefault(Function(x) x.KDCURVELIST = sKDCURVELIST)
        End Function
        Public Function GetDataDetail_1(ByVal sKDCURVELIST As String) As List(Of S_DIGITAL_CURVELIST_01)
            If Not oConnection.GetConnection() Then
                GetDataDetail_1 = Nothing
                Exit Function
            End If
            GetDataDetail_1 = oConnection.db.S_DIGITAL_CURVELIST_01s.Where(Function(x) x.KDCURVELIST = sKDCURVELIST).ToList()
        End Function
        Public Function GetDataDetail_2(ByVal sKDCURVELIST As String) As List(Of S_DIGITAL_CURVELIST_02)
            If Not oConnection.GetConnection() Then
                GetDataDetail_2 = Nothing
                Exit Function
            End If
            GetDataDetail_2 = oConnection.db.S_DIGITAL_CURVELIST_02s.Where(Function(x) x.KDCURVELIST = sKDCURVELIST).ToList()
        End Function
        Public Function GetDataIdentitas(ByVal sKDKUNJUNGAN As String) As R_IDENTITAS_PASIEN
            If Not oConnection.GetConnection() Then
                GetDataIdentitas = Nothing
                Exit Function
            End If
            GetDataIdentitas = oConnection.db.R_IDENTITAS_PASIENs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = sKDKUNJUNGAN)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_CURVELIST_H, ByVal entityDetail_1 As List(Of S_DIGITAL_CURVELIST_01), ByVal entityDetail_2 As List(Of S_DIGITAL_CURVELIST_02)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCURVELIST
                sSTATUS = "INSERT"

                sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                If sLASTNUMBER = 0 Then
                    Try
                        oCounter.InsertData(sMODUL, entity.DATE)
                        sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    Catch ex As Exception
                        sLASTNUMBER = 0
                    End Try
                End If

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                entity.KDCURVELIST = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)

                For Each iLoop In entityDetail_1
                    iLoop.KDCURVELIST = entity.KDCURVELIST
                Next

                For Each iLoop In entityDetail_2
                    iLoop.KDCURVELIST = entity.KDCURVELIST
                Next

                Try
                    oConnection.db.S_DIGITAL_CURVELIST_Hs.InsertOnSubmit(entity)
                    If entityDetail_1 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_CURVELIST_01s.InsertAllOnSubmit(entityDetail_1)
                    End If
                    If entityDetail_2 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_CURVELIST_02s.InsertAllOnSubmit(entityDetail_2)
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
        Public Function UpdateData(ByVal entity As S_DIGITAL_CURVELIST_H, Optional ByVal entityDetail_1 As List(Of S_DIGITAL_CURVELIST_01) = Nothing, Optional ByVal entityDetail_2 As List(Of S_DIGITAL_CURVELIST_02) = Nothing) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDCURVELIST
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.S_DIGITAL_CURVELIST_Hs.FirstOrDefault(Function(x) x.KDCURVELIST = entity.KDCURVELIST)

                Try
                    oConnection.db.S_DIGITAL_CURVELIST_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_DIGITAL_CURVELIST_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail_1 = oConnection.db.S_DIGITAL_CURVELIST_01s.Where(Function(x) x.KDCURVELIST = entity.KDCURVELIST)
                If dsDetail_1 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_CURVELIST_01s.DeleteAllOnSubmit(dsDetail_1)
                End If
                If entityDetail_1 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_CURVELIST_01s.InsertAllOnSubmit(entityDetail_1)
                End If

                Dim dsDetail_2 = oConnection.db.S_DIGITAL_CURVELIST_02s.Where(Function(x) x.KDCURVELIST = entity.KDCURVELIST)
                If dsDetail_2 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_CURVELIST_02s.DeleteAllOnSubmit(dsDetail_2)
                End If
                If entityDetail_2 IsNot Nothing Then
                    oConnection.db.S_DIGITAL_CURVELIST_02s.InsertAllOnSubmit(entityDetail_2)
                End If

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
        Public Function DeleteData(ByVal sKDCURVELIST As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDCURVELIST
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_DIGITAL_CURVELIST_Hs.FirstOrDefault(Function(x) x.KDCURVELIST = sKDCURVELIST)
                Dim dsDetail_1 = oConnection.db.S_DIGITAL_CURVELIST_01s.Where(Function(x) x.KDCURVELIST = sKDCURVELIST)
                Dim dsDetail_2 = oConnection.db.S_DIGITAL_CURVELIST_02s.Where(Function(x) x.KDCURVELIST = sKDCURVELIST)

                Try
                    oConnection.db.S_DIGITAL_CURVELIST_Hs.DeleteOnSubmit(ds)
                    If dsDetail_1 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_CURVELIST_01s.DeleteAllOnSubmit(dsDetail_1)
                    End If
                    If dsDetail_2 IsNot Nothing Then
                        oConnection.db.S_DIGITAL_CURVELIST_02s.DeleteAllOnSubmit(dsDetail_2)
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