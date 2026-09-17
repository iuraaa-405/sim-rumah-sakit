Imports System.Threading

Namespace Finance
    Public Class clsJasa
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
            sMODUL = "JS"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As I_JASA_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New I_JASA_H
        End Function
        Public Function GetStructureDetail() As I_JASA_D
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New I_JASA_D
        End Function
        Public Function GetStructureDetailList() As List(Of I_JASA_D)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of I_JASA_D)
        End Function
        Public Function GetData() As List(Of I_JASA_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_JASA_Hs.OrderByDescending(Function(x) x.KDJASA).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As I_JASA_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.I_JASA_Hs.FirstOrDefault(Function(x) x.KDJASA = Parameter)
        End Function
        Public Function GetDataSEP(ByVal Parameter As String) As I_JASA_H
            If Not oConnection.GetConnection() Then
                GetDataSEP = Nothing
                Exit Function
            End If
            GetDataSEP = oConnection.db.I_JASA_Hs.FirstOrDefault(Function(x) x.NOSEP = Parameter)
        End Function
        Public Function GetDataSetting() As SET_SETTING
            If Not oConnection.GetConnection() Then
                GetDataSetting = Nothing
                Exit Function
            End If
            GetDataSetting = oConnection.db.SET_SETTINGs.FirstOrDefault()
        End Function
        Public Function GetDataDetail() As List(Of I_JASA_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_JASA_Ds.ToList()
        End Function
        Public Function GetDataDetail(ByVal Parameter As String) As List(Of I_JASA_D)
            If Not oConnection.GetConnection() Then
                GetDataDetail = Nothing
                Exit Function
            End If
            GetDataDetail = oConnection.db.I_JASA_Ds.Where(Function(x) x.KDJASA = Parameter).ToList()
        End Function
        Public Function InsertData(ByVal entity As I_JASA_H, ByVal entityDetail As List(Of I_JASA_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJASA
                sSTATUS = "INSERT"

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATE)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATE)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDJASA = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATE)
                    For Each iLoop In entityDetail
                        Dim TES = entity.KDJASA
                        iLoop.KDJASA = entity.KDJASA
                    Next

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.db.I_JASA_Hs.InsertOnSubmit(entity)
                    oConnection.db.I_JASA_Ds.InsertAllOnSubmit(entityDetail)
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

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATE), Year(entity.DATE))
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
        Public Function UpdateData(ByVal entity As I_JASA_H, ByVal entityDetail As List(Of I_JASA_D)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDJASA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.db.I_JASA_Hs.FirstOrDefault(Function(x) x.KDJASA = entity.KDJASA)

                Try
                    oConnection.db.I_JASA_Hs.DeleteOnSubmit(ds)
                    oConnection.db.I_JASA_Hs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Dim dsDetail = oConnection.db.I_JASA_Ds.Where(Function(x) x.KDJASA = entity.KDJASA)

                Try
                    oConnection.db.I_JASA_Ds.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.I_JASA_Ds.InsertAllOnSubmit(entityDetail)
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
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = Parameter
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.I_JASA_Hs.Where(Function(x) x.KDJASA.Contains(Parameter))

                For Each xLoop In ds
                    Dim sNOCASH = xLoop.KDJASA

                    Dim dsDetail = oConnection.db.I_JASA_Ds.Where(Function(x) x.KDJASA = sNOCASH)

                    Try
                        oConnection.db.I_JASA_Hs.DeleteOnSubmit(xLoop)
                        oConnection.db.I_JASA_Ds.DeleteAllOnSubmit(dsDetail)
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
                Next

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        'Public Function UpdateDataFix(ByVal isNew As Boolean) As Boolean
        '    Try
        '        If Not oConnection.GetConnection Then
        '            UpdateDataFix = False
        '            Exit Function
        '        End If


        '        Dim entity = oConnection.db.I_JASA_Hs

        '        For Each iLoop In entity
        '            Dim sKDJASA = iLoop.KDJASA
        '            Dim entityDetail = oConnection.db.I_JASA_Ds.Where(Function(x) x.KDJASA = sKDJASA And x.SEQ < 100)
        '            Dim entityDetail_R = oConnection.db.I_JASA_Ds.Where(Function(x) x.KDJASA = sKDJASA And x.SEQ >= 100)

        '            Try
        '                If Not AutoJournal(iLoop, isNew) Then
        '                    Return False
        '                End If
        '            Catch ex As Exception
        '                Throw ex
        '            End Try

        '            Thread.Sleep(100)
        '        Next

        '        UpdateDataFix = True
        '    Catch ex As Exception
        '        UpdateDataFix = False
        '        Throw ex
        '    End Try
        'End Function
    End Class
End Namespace