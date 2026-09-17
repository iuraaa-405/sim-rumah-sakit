Imports System.Threading

Namespace Digital
    Public Class clsSuaraKeFarmasi
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0

        Public oCounter As Setting.clsCounter = Nothing

        Public sKDITEM As New List(Of String)

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "SUARA"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_SUARA
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_SUARA
        End Function
        Public Function GetData() As List(Of A_SUARA)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_SUARAs.OrderByDescending(Function(x) x.KDSUARA).ToList()
        End Function
        Public Function GetData(ByVal sKDSUARA As Integer) As A_SUARA
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.KDSUARA = sKDSUARA)
        End Function
        'Public Function GetDataByFirstMemoAll(ByVal Memo1 As String, ByVal Memo2 As String) As A_SUARA
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataByFirstMemoAll = Nothing
        '        Exit Function
        '    End If
        '    GetDataByFirstMemoAll = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.MEMO.Contains(Memo1) Or x.MEMO.Contains(Memo2))
        'End Function
        'Public Function GetDataByFirstMemoAll(ByVal Memo As String) As A_SUARA
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataByFirstMemoAll = Nothing
        '        Exit Function
        '    End If
        '    GetDataByFirstMemoAll = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.MEMO.Contains(Memo))
        'End Function
        'Public Function GetDataByFirstMemoAll() As A_SUARA
        '    If Not oConnection.GetConnectionRME() Then
        '        GetDataByFirstMemoAll = Nothing
        '        Exit Function
        '    End If
        '    GetDataByFirstMemoAll = oConnection.dbRME.A_SUARAs.OrderByDescending(Function(x) x.KDSUARA).FirstOrDefault()
        'End Function
        Public Function GetDataPanggil() As List(Of A_SUARA)
            If Not oConnection.GetConnectionRME() Then
                GetDataPanggil = Nothing
                Exit Function
            End If
            GetDataPanggil = oConnection.dbRME.A_SUARAs.OrderByDescending(Function(x) x.KDSUARA).ToList()
        End Function
        Public Function InsertData(ByVal entity As A_SUARA) As String
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = ""
                    Exit Function
                End If

                sREFERENCE = entity.KDSUARA
                sSTATUS = "INSERT"

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.A_SUARAs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                InsertData = entity.KDSUARA
            Catch ex As Exception
                InsertData = ""
                'oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As A_SUARA) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSUARA
                sSTATUS = "UPDATE"

                Dim ds = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.KDSUARA = entity.KDSUARA)

                Try
                    Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATEUPDATED = WaktuServer

                    oConnection.dbRME.A_SUARAs.DeleteOnSubmit(ds)
                    oConnection.dbRME.A_SUARAs.InsertOnSubmit(entity)
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
                Try
                    oConnection.dbRME.SubmitChanges()
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
        Public Function DeleteData(ByVal sKDSUARA As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSUARA
                sSTATUS = "DELETE"

                Dim ds = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.KDSUARA = sKDSUARA)

                If ds IsNot Nothing Then
                    Try
                        oConnection.dbRME.A_SUARAs.DeleteOnSubmit(ds)
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                    Try
                        oConnection.dbRME.SubmitChanges()
                    Catch ex As Exception
                        oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                        Throw ex
                    End Try
                End If

                DeleteData = True
            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
        Public Function HapusSudahPanggil(ByVal sKDSUARA As Integer) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    HapusSudahPanggil = False
                    Exit Function
                End If

                Dim ds = oConnection.dbRME.A_SUARAs.FirstOrDefault(Function(x) x.KDSUARA = sKDSUARA)

                If ds IsNot Nothing Then
                    oConnection.dbRME.A_SUARAs.DeleteOnSubmit(ds)
                    oConnection.dbRME.SubmitChanges()
                End If

                HapusSudahPanggil = True
            Catch ex As Exception
                HapusSudahPanggil = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace