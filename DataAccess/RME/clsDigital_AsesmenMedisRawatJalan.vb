Imports System.Threading

Namespace EMedrek
    Public Class clsDigital_AsesmenMedisRawatJalan
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
            sMODUL = "DIGITAL_ASESMENRAWATJALAN"

            oCounter = New Setting.clsCounter
        End Sub
        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            oConnection = Nothing
            GC.SuppressFinalize(Me)
        End Sub
        Public Function GetStructureHeader() As S_DIGITAL_ASESMENMEDISRAWATJALAN
            If Not oConnection.GetConnectionRME() Then
                GetStructureHeader = Nothing
            End If
            GetStructureHeader = New S_DIGITAL_ASESMENMEDISRAWATJALAN
        End Function
        Public Function GetData() As List(Of S_DIGITAL_ASESMENMEDISRAWATJALAN)
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetDataByRM(ByVal kdcustomer As String) As List(Of S_DIGITAL_ASESMENMEDISRAWATJALAN)
            If Not oConnection.GetConnectionRME() Then
                GetDataByRM = Nothing
                Exit Function
            End If
            GetDataByRM = oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.Where(Function(x) x.A_IDENTITASPASIEN_LIST.KDCUSTOMER = kdcustomer).OrderByDescending(Function(x) x.KDKUNJUNGAN).ToList()
        End Function
        Public Function GetData(ByVal Parameter As String) As S_DIGITAL_ASESMENMEDISRAWATJALAN
            If Not oConnection.GetConnectionRME() Then
                GetData = Nothing
                Exit Function
            End If
            GetData = oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = Parameter)
        End Function
        Public Function InsertData(ByVal entity As S_DIGITAL_ASESMENMEDISRAWATJALAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    InsertData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "INSERT"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATECREATED = WaktuServer
                entity.DATEUPDATED = WaktuServer

                Try
                    oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.InsertOnSubmit(entity)
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
            Catch ex As Exception
                InsertData = False
                oError.InsertData(sMODUL, sSTATUS, ex.ToString, sREFERENCE)
                Throw ex
            End Try
        End Function
        Public Function UpdateData(ByVal entity As S_DIGITAL_ASESMENMEDISRAWATJALAN) As Boolean
            Try
                If Not oConnection.GetConnectionRME() Then
                    UpdateData = False
                    Exit Function
                End If

                sREFERENCE = entity.KDKUNJUNGAN
                sSTATUS = "UPDATE"

                Dim oData As New Grouper.clsR_Identitas_Grouper_Data
                Dim WaktuServer As DateTime = oData.fn_LoadWaktuServer()
                entity.DATEUPDATED = WaktuServer

                Dim ds = oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.FirstOrDefault(Function(x) x.KDKUNJUNGAN = entity.KDKUNJUNGAN)

                Try
                    oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.DeleteOnSubmit(ds)
                    oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.InsertOnSubmit(entity)
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

                Dim ds = oConnection.dbRME.S_DIGITAL_ASESMENMEDISRAWATJALANs.FirstOrDefault(Function(x) x.KDUSER = Parameter)
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