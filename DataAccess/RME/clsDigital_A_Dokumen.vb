Imports System.Threading

Namespace Digital
    Public Class clsDigital_A_Dokumen
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
            sMODUL = "DOKUMEN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As A_DOKUMEN
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New A_DOKUMEN
        End Function
        Public Function GetData() As List(Of A_DOKUMEN)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_DOKUMENs.OrderByDescending(Function(x) x.KDDKUMEN).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of A_DOKUMEN)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.dbRME.A_DOKUMENs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDDKUMEN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As A_DOKUMEN
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.A_DOKUMENs.FirstOrDefault(Function(x) x.KDDKUMEN = Parameter)
        End Function
        Public Function GetDataByKunjungan(ByVal sKDKUNJUNGAN As String) As List(Of A_DOKUMEN)
            If Not oConnection.GetConnectionRME() Then
                GetDataByKunjungan = Nothing
                Exit Function
            End If
            GetDataByKunjungan = oConnection.dbRME.A_DOKUMENs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDKUNJUNGAN = sKDKUNJUNGAN).OrderByDescending(Function(x) x.KDDKUMEN).ToList()
        End Function
        Public Function InsertData(ByVal entity As A_DOKUMEN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDKUMEN
                sSTATUS = "INSERT"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer

                Try
                    sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                    If sLASTNUMBER = 0 Then
                        Try
                            oCounter.InsertData(sMODUL, entity.DATECREATED)
                            sLASTNUMBER = oCounter.GetLastNumber(sMODUL, entity.DATECREATED)
                        Catch ex As Exception
                            sLASTNUMBER = 0
                        End Try
                    End If

                    entity.KDDKUMEN = AutoNumber(sMODUL, sLASTNUMBER + 1, entity.DATECREATED)

                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try

                Try
                    oConnection.dbRME.A_DOKUMENs.InsertOnSubmit(entity)
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

                InsertData = True

                Try
                    oCounter.UpdateData(sMODUL, sLASTNUMBER + 1, Month(entity.DATECREATED), Year(entity.DATECREATED))
                Catch ex As Exception
                    oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                    Throw ex
                End Try
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As A_DOKUMEN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDDKUMEN
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.A_DOKUMENs.FirstOrDefault(Function(x) x.KDDKUMEN = entity.KDDKUMEN)

                Try
                    oConnection.dbRME.A_DOKUMENs.DeleteOnSubmit(ds)
                    oConnection.dbRME.A_DOKUMENs.InsertOnSubmit(entity)
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
        Public Function DeleteData(ByVal Parameter As String, ByVal sUser As String) As Boolean
            Try
                If Not oConnection.GetConnectionRME Then
                    DeleteData = False
                    Exit Function
                End If

                DeleteData = True

                Dim ds = oConnection.dbRME.A_DOKUMENs.FirstOrDefault(Function(x) x.KDUSER = Parameter)
                Dim oData As New Grouper.clsR_Identitas_Grouper_Data

                If ds IsNot Nothing Then
                    Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                    ds.DATEUPDATED = WaktuServer

                    oConnection.dbRME.SubmitChanges()

                End If

            Catch ex As Exception
                DeleteData = False
                Throw ex
            End Try
        End Function
    End Class
End Namespace