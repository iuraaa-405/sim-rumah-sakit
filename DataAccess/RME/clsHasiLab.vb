Imports DataAccess

Namespace Grouper
    Public Class clsHasiLab
        Public oConnection As Setting.clsConnectionMain = Nothing
        Public oError As Setting.clsError = Nothing
        Public sMODUL As String = ""
        Public sREFERENCE As String = ""
        Public sSTATUS As String = ""
        Public sLASTNUMBER As Integer = 0
        Public oCounter As Setting.clsCounter = Nothing
        Private oData As New Grouper.clsR_Identitas_Grouper_Data

        Public Sub New()
            oConnection = New Setting.clsConnectionMain
            oError = New Setting.clsError
            sMODUL = "EXPERTISE"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureDetail() As S_SO_TRANSAKSI_D_HASIL_LABORATORIUM
            If Not oConnection.GetConnection() Then
                GetStructureDetail = Nothing
            End If
            GetStructureDetail = New S_SO_TRANSAKSI_D_HASIL_LABORATORIUM
        End Function
        Public Function GetStructureDetailList() As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)
            If Not oConnection.GetConnection() Then
                GetStructureDetailList = Nothing
            End If
            GetStructureDetailList = New List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)
        End Function
        'Public Function GetData(ByVal Parameter As String, ByVal sSEQ As Integer) As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)
        '    If Not oConnection.GetConnection() Then
        '        GetData = Nothing
        '        Exit Function
        '    End If
        '    GetData = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = Parameter And x.SEQ_SO = sSEQ).OrderBy(Function(x) x.SEQ).ToList()
        'End Function
        Public Function GetDataDetailTransaksi(ByVal Parameter As String) As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)
            If Not oConnection.GetConnection() Then
                GetDataDetailTransaksi = Nothing
                Exit Function
            End If
            GetDataDetailTransaksi = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = Parameter).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTransaksikditem(ByVal Parameter1 As String, ByVal Parameter2 As String) As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)
            If Not oConnection.GetConnection() Then
                GetDataDetailTransaksikditem = Nothing
                Exit Function
            End If
            GetDataDetailTransaksikditem = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = Parameter1 And x.KDITEM = Parameter2).OrderBy(Function(x) x.SEQ).ToList()
        End Function
        Public Function GetDataDetailTransaksiFirst(ByVal Parameter As String) As S_SO_TRANSAKSI_D_HASIL_LABORATORIUM
            If Not oConnection.GetConnection() Then
                GetDataDetailTransaksiFirst = Nothing
                Exit Function
            End If
            GetDataDetailTransaksiFirst = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = Parameter)
        End Function
        Public Function GetDataTransaksi(ByVal Parameter As String) As S_SO_TRANSAKSI_H
            If Not oConnection.GetConnection() Then
                GetDataTransaksi = Nothing
                Exit Function
            End If
            GetDataTransaksi = oConnection.db.S_SO_TRANSAKSI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = Parameter)
        End Function
        Public Function InsertData(ByVal entityDetail As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entityDetail.FirstOrDefault.KDSOTRANSAKSI & entityDetail.FirstOrDefault.SEQ_SO
                sSTATUS = "INSERT"

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()

                    For Each iLoop In entityDetail
                        iLoop.DATECREATED = WaktuServer
                        iLoop.DATEUPDATED = WaktuServer
                    Next

                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.InsertAllOnSubmit(entityDetail)
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
        Public Function UpdateData(ByVal entityDetail As List(Of S_SO_TRANSAKSI_D_HASIL_LABORATORIUM)) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entityDetail.FirstOrDefault.KDSOTRANSAKSI & entityDetail.FirstOrDefault.SEQ_SO
                sSTATUS = "UPDATE"

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                For Each iLoop In entityDetail
                    iLoop.DATEUPDATED = WaktuServer
                Next

                'Dim dsDetail = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = entityDetail.FirstOrDefault.KDSOTRANSAKSI And x.SEQ_SO = entityDetail.FirstOrDefault.SEQ_SO)
                Dim dsDetail = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = entityDetail.FirstOrDefault.KDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.DeleteAllOnSubmit(dsDetail)
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.InsertAllOnSubmit(entityDetail)
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
        Public Function DeleteData(ByVal sKDSOTRANSAKSI As String) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.Where(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_LABORATORIUMs.DeleteAllOnSubmit(ds)
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