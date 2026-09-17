Imports DataAccess

Namespace Grouper
    Public Class clsExpertise
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
        Public Function GetStructureHeader() As S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H
            If Not oConnection.GetConnection() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H
        End Function
        Public Function GetData() As List(Of S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H)
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetDataList(ByVal NoRM As String) As List(Of S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H)
            If Not oConnection.GetConnection() Then
                GetDataList = Nothing
                Exit Function
            End If
            GetDataList = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.Where(Function(x) x.S_SO_TRANSAKSI_H.S_PENDAFTARAN_KUNJUNGAN.S_PENDAFTARAN_H.KDCUSTOMER = NoRM).OrderByDescending(Function(x) x.KDSOTRANSAKSI).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String, ByVal sSEQ As Integer) As S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H
            If Not oConnection.GetConnection() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = Parameter And x.SEQ = sSEQ)
        End Function
        Public Function GetDataFirts(ByVal Parameter As String) As Integer
            If Not oConnection.GetConnection() Then
                GetDataFirts = 0
                Exit Function
            End If
            Dim ds = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.Where(Function(x) x.KDSOTRANSAKSI = Parameter).OrderByDescending(Function(x) x.SEQ).FirstOrDefault()
            If ds IsNot Nothing Then
                GetDataFirts = ds.SEQ + 1
            Else
                GetDataFirts = 0
            End If
        End Function
        Public Function InsertData(ByVal entity As S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI & entity.SEQ
                sSTATUS = "INSERT"

                Try
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    entity.DATECREATED = WaktuServer
                    entity.DATEUPDATED = WaktuServer

                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.InsertOnSubmit(entity)
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
        Public Function UpdateData(ByVal entity As S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_H) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDSOTRANSAKSI & entity.SEQ
                sSTATUS = "UPDATE"

                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = entity.KDSOTRANSAKSI And x.SEQ = entity.SEQ)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.DeleteOnSubmit(ds)
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal sKDSOTRANSAKSI As String, ByVal sSEQ As Integer) As Boolean
            Try
                If Not oConnection.GetConnection() Then
                    DeleteData = False
                    Exit Function
                End If

                sREFERENCE = sKDSOTRANSAKSI & sSEQ
                sSTATUS = "DELETE"

                Dim ds = oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.FirstOrDefault(Function(x) x.KDSOTRANSAKSI = sKDSOTRANSAKSI And x.SEQ = sSEQ)

                Try
                    oConnection.db.S_SO_TRANSAKSI_D_HASIL_RADIOLOGI_Hs.DeleteOnSubmit(ds)
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